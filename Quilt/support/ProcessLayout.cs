using geoCoreLib;
using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quilt
{
    public class ProcessLayout
    {
        public delegate GeoCore GetGeoCore();
        public GetGeoCore getGeoCore { get; set; }

        public List<PatternElement> pattElements;
        List<string> structureLDs;
        Dictionary<string, string> structureLDNames;

        public ProcessLayout(List<string> structureLDs_, Dictionary<string, string> structureLDNames_)
        {
            structureLDNames = structureLDNames_;
            structureLDs = structureLDs_;
            pattElements = new List<PatternElement>();
        }

        void pAddPatternElement(string name)
        {
            pattElements.Add(new PatternElement());
            pattElements[^1].setString(PatternElement.properties_s.name, name);
        }

        // ReSharper disable once UnusedMember.Local
        void pDumpToDisk(List<GeoLibPointF[]> geoData)
        {
            for (int poly = 0; poly < geoData.Count; poly++)
            {
                List<string> lines = new List<string>();
                for (int pt = 0; pt < geoData[poly].Length; pt++)
                {
                    string line = "new GeoLibPoint(" + geoData[poly][pt].X + ", " + geoData[poly][pt].Y + ")";
                    if (pt > 0)
                    {
                        line += ",";
                    }
                    lines.Add(line);
                }
                File.WriteAllLines("d://debug_" + poly + ".txt", lines);
            }
        }

        public bool processLayout(ref bool abortLoad, double tolerance, bool vertical)
        {
            return pProcessLayout(ref abortLoad, tolerance, vertical);
        }

        bool pProcessLayout(ref bool abortLoad, double tolerance, bool vertical)
        {
            // Iterate our string list to extract geometry for each layer.
            double scaling = getGeoCore.Invoke().scaling;

            for (int i = 0; i < structureLDs.Count; i++)
            {
                if (abortLoad)
                {
                    pattElements.Clear();
                    return false;
                }
                // Update geoCore with the active layer-datatype for our active structure.
                getGeoCore?.Invoke().updateGeometry(getGeoCore().activeStructure, i);

                // Get geometry from geoCore. This is a flat list of geometry with references flattened.
                // Would be neat to get array data to map to Quilt in a future build.
                List<GeoLibPointF[]> polydata = getGeoCore.Invoke().points(true).ToList();

                // For debug / dev purposes.
                // dumpToDisk(polydata);

                // Clean up the geometry for processing.
                polydata = GeoWrangler.stripColinear(polydata, tolerance);

                List<bool> isText = getGeoCore.Invoke().isText().ToList();
                List<string> names = getGeoCore.Invoke().names().ToList();

                // Let GeoWrangler clean it up - unions, keyholing if needed, clockwise orientation and re-order.
                polydata = GeoWrangler.clockwiseAndReorder(polydata);

                // Does the LD combination have a name we should use?
                int mergeIndex = pattElements.Count;
                string layerName = structureLDs[i];

                // We need the layer and datatype numeric values here so that the pattern elements can have their target layer/datatype values set.
                string ldString = structureLDNames.FirstOrDefault(x => x.Value == layerName).Key;
                string[] ldTokens = ldString.Split('L')[1].Split('D');
                int layoutLayer = Convert.ToInt32(ldTokens[0]);
                int layoutDatatype = Convert.ToInt32(ldTokens[1]);

                try
                {
                    layerName = structureLDNames[layerName];
                }
                catch (Exception)
                {
                    // Look-up failed, stick with what we have as a base.
                }


                // Each pattern entry is a single polygon, so we need to iterate our list here.
                // Strictly, this needs to be orthogonal polygon data to avoid breaking things.
                List<GeoLibArray> arrayParms = getGeoCore.Invoke().getArrayParameters();
                bool flattened = getGeoCore.Invoke().nestedCellRef(getGeoCore().activeStructure);
                if (flattened)
                {
                    arrayParms = null;
                }
                for (int p = 0; p < polydata.Count; p++)
                {
                    if (abortLoad)
                    {
                        pattElements.Clear();
                        return false;
                    }
                    int elementIndex = pattElements.Count;

                    bool text = isText[p];
                    bool breakOut = false;
                    // Take the text as the layer name.
                    string name = text ? names[p] : layerName;
                    pAddPatternElement(name);

                    // So this is where it gets tricky. We need to analyze our point data. We may be able to map it into a primitive type already known to us. Defer to the pattern element to sort it out.
                    pattElements[elementIndex].parsePoints(ref abortLoad, polydata[p], layoutLayer, layoutDatatype, isText:text, vertical: vertical);
                    try
                    {
                        if ((arrayParms != null) && (arrayParms[p] != null))
                        {
                            // The layout value differs from what we need - layout refers to a pitch; we need a space.
                            double width = polydata[p].Max(q => q.X) - polydata[p].Min(q => q.X);
                            double height = polydata[p].Max(q => q.Y) - polydata[p].Min(q => q.Y);

                            pattElements[elementIndex].setDecimal(PatternElement.properties_decimal.arrayMinXSpace, Convert.ToDecimal((arrayParms[p].pitch.X - width) * scaling));
                            pattElements[elementIndex].setDecimal(PatternElement.properties_decimal.arrayMinYSpace, Convert.ToDecimal((arrayParms[p].pitch.Y - height) * scaling));
                            pattElements[elementIndex].setInt(PatternElement.properties_i.arrayMinXCount, arrayParms[p].count.X);
                            pattElements[elementIndex].setInt(PatternElement.properties_i.arrayMinYCount, arrayParms[p].count.Y);
                            breakOut = true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    // Check for decomposed entities needing to be handled.
                    int offset = 0;
                    try
                    {
                        while (pattElements[elementIndex].decomposedPolys.Any())
                        {
                            if (abortLoad)
                            {
                                pattElements.Clear();
                                return false;
                            }
                            pAddPatternElement(name);
                            offset++;
                            pattElements[elementIndex + offset].parsePoints(ref abortLoad, pattElements[elementIndex].decomposedPolys[0], layer:layoutLayer, datatype: layoutDatatype, isText:text, vertical:vertical);
                            if (offset > 1)
                            {
                                // Set relative rotation pivot flag here.
                                pattElements[elementIndex + offset].setInt(PatternElement.properties_i.refPivot, 1);
                            }
                            pattElements[elementIndex].decomposedPolys.RemoveAt(0);
                            // Register our 'parent' element for the decomposed entries.
                            pattElements[elementIndex + offset].setInt(PatternElement.properties_i.linkedElementIndex, mergeIndex);
                        }

                        // In the case of multi-islands, we need to ensure the merge flag is set correctly on the anchor for the additional islands, to allow them to be merged back.
                        // The two conditions are reflected below - is there decomp involved (is offset non-zero), and is there more than one island (p index greater than 0).
                        if ((offset > 0) && (p > 0))
                        {
                            // Need to adjust the reference for the initially added element so that we see the chain the multiple islands back together.
                            pattElements[elementIndex].setInt(PatternElement.properties_i.linkedElementIndex, mergeIndex);
                        }

                        // Any non-ortho cases? The 1 looks weird here, but is necessary to avoid capturing the non-orthogonal case twice.
                        while (pattElements[elementIndex].nonOrthoGeometry.Count > 1)
                        {
                            if (abortLoad)
                            {
                                pattElements.Clear();
                                return false;
                            }
                            pAddPatternElement(name);
                            offset++;
                            pattElements[elementIndex + offset].parsePoints(ref abortLoad, pattElements[elementIndex].nonOrthoGeometry[0], layer:layoutLayer, datatype: layoutDatatype, isText:false, vertical:vertical);
                            if (offset > 1)
                            {
                                // Set relative rotation pivot flag here.
                                pattElements[elementIndex + offset].setInt(PatternElement.properties_i.refPivot, 1);
                            }
                            pattElements[elementIndex].nonOrthoGeometry.RemoveAt(0);
                            // Register our 'parent' element for the decomposed entries.
                            pattElements[elementIndex + offset].setInt(PatternElement.properties_i.linkedElementIndex, mergeIndex);
                        }

                        // Reverse direction due to the order of dependency
                        for (int offsetFromReference = offset; offsetFromReference > 0; offsetFromReference--)
                        {
                            if (abortLoad)
                            {
                                pattElements.Clear();
                                return false;
                            }
                            int elementReferenceIndex = offsetFromReference + elementIndex;
                            // Is previous element a rectangle? If not, don't try and make a relative position.
                            if (pattElements[elementReferenceIndex - 1].getInt(PatternElement.properties_i.shapeIndex) != (int)CentralProperties.typeShapes.rectangle)
                            {
                                continue;
                            }

                            // Reference position and width
                            decimal rX = pattElements[elementReferenceIndex - 1].getDecimal(PatternElement.properties_decimal.minXPos);
                            decimal rY = pattElements[elementReferenceIndex - 1].getDecimal(PatternElement.properties_decimal.minYPos);
                            decimal rW = pattElements[elementReferenceIndex - 1].getDecimal(PatternElement.properties_decimal.minHorLength, 0);
                            decimal rH = pattElements[elementReferenceIndex - 1].getDecimal(PatternElement.properties_decimal.minVerLength, 0);

                            // Calculate new position values.
                            decimal eX = pattElements[elementReferenceIndex].getDecimal(PatternElement.properties_decimal.minXPos);
                            decimal eY = pattElements[elementReferenceIndex].getDecimal(PatternElement.properties_decimal.minYPos);

                            eX -= rX;
                            pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.xPosSubShapeRefPos, (int)CommonVars.subShapeHorLocs.L);
                            if (eX >= rW)
                            {
                                eX -= rW;
                                pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.xPosSubShapeRefPos, (int)CommonVars.subShapeHorLocs.R);
                            }

                            eY -= rY;
                            pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.yPosSubShapeRefPos, (int)CommonVars.subShapeVerLocs.B);
                            if (eY >= rH)
                            {
                                eY -= rH;
                                pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.yPosSubShapeRefPos, (int)CommonVars.subShapeVerLocs.T);
                            }

                            // Set relative position for the current element.
                            pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.xPosRef, elementReferenceIndex);
                            pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.yPosRef, elementReferenceIndex);
                            pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.rotationRef, elementReferenceIndex);
                            pattElements[elementReferenceIndex].setInt(PatternElement.properties_i.refPivot, 1);

                            // Set new position values.
                            pattElements[elementReferenceIndex].setDecimal(PatternElement.properties_decimal.minXPos, eX);
                            pattElements[elementReferenceIndex].setDecimal(PatternElement.properties_decimal.minYPos, eY);
                        }
                    }
                    catch (Exception)
                    {
                        // Harmless, so catch and move on.
                    }
                    elementIndex += offset;
                    elementIndex++;

                    if (breakOut)
                    {
                        break;
                    }
                }
            }
            
            return true;
        }
    }
}
