using Eto.Forms;
using geoCoreLib;
using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        void addPatternElement(string name)
        {
            pattElements.Add(new PatternElement());
            pattElements[pattElements.Count - 1].setString(PatternElement.properties_s.name, name);
        }

        public bool processLayout(double tolerance, bool vertical)
        {
            bool ret = false;
            // Iterate our string list to extract geometry for each layer.
            int elementIndex = 0;
            double scaling = getGeoCore.Invoke().scaling;

            for (int i = 0; i < structureLDs.Count; i++)
            {
                // Update geoCore with the active layer-datatype for our active structure.
                getGeoCore?.Invoke().updateGeometry(getGeoCore().activeStructure, i);

                // Get geometry from geoCore. This is a flat list of geometry with references flattened.
                // Would be neat to get array data to map to Quilt in a future build.
                List<GeoLibPointF[]> polydata = getGeoCore.Invoke().points(true).ToList();

                // Clean up the geometry for processing.
                polydata = GeoWrangler.stripColinear(polydata, tolerance);

                List<bool> isText = getGeoCore.Invoke().isText().ToList();
                List<string> names = getGeoCore.Invoke().names().ToList();

                // Let GeoWrangler clean it up - unions, keyholing if needed, clockwise orientatiton and re-order.
                polydata = GeoWrangler.clockwiseAndReorder(polydata);

                // Does the LD combination have a name we should use?
                string layerName = structureLDs[i];
                try
                {
                    layerName = structureLDNames[layerName];
                }
                catch (Exception e)
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

                    bool text = isText[p];
                    bool breakOut = false;
                    string name = "";
                    if (text)
                    {
                        // Take the text as the layer name.
                        name = names[p];
                    }
                    else
                    {
                        name = layerName;
                    }
                    addPatternElement(name);
                    // So this is where it gets tricky. We need to analyze our point data. We may be able to map it into a primitive type already known to us. Defer to the pattern element to sort it out.
                    pattElements[elementIndex].parsePoints(polydata[p], isText:text, vertical: vertical);
                    try
                    {
                        if ((arrayParms != null) && (arrayParms[p] != null))
                        {
                            // The layout value differs from what we need - layout refers to a pitch; we need a space.
                            double width = polydata[p].Max(q => q.X) - polydata[p].Min(q => q.X);
                            double height = polydata[p].Max(q => q.Y) - polydata[p].Min(q => q.Y);

                            pattElements[elementIndex].setDecimal(PatternElement.properties_decimal.arrayXSpace, Convert.ToDecimal((arrayParms[p].pitch.X - width) * scaling));
                            pattElements[elementIndex].setDecimal(PatternElement.properties_decimal.arrayYSpace, Convert.ToDecimal((arrayParms[p].pitch.Y - height) * scaling));
                            pattElements[elementIndex].setInt(PatternElement.properties_i.arrayXCount, arrayParms[p].count.X);
                            pattElements[elementIndex].setInt(PatternElement.properties_i.arrayYCount, arrayParms[p].count.Y);
                            breakOut = true;
                        }
                    }
                    catch (Exception)
                    {
                        // debug
                        int debug___ = 1;
                    }
                    // Check for decomposed entities needing to be handled.
                    int offset = 0;
                    try
                    {
                        while (pattElements[elementIndex].decomposedPolys.Count() > 0)
                        {
                            addPatternElement(name);
                            offset++;
                            pattElements[elementIndex + offset].parsePoints(pattElements[elementIndex].decomposedPolys[0], isText:text, vertical:vertical);
                            if (offset > 1)
                            {
                                // Set relative rotation pivot flag here.
                                pattElements[elementIndex + offset].setInt(PatternElement.properties_i.refPivot, 1);
                            }
                            pattElements[elementIndex].decomposedPolys.RemoveAt(0);
                            // Register our 'parent' element for the decomposed entries.
                            pattElements[elementIndex + offset].setInt(PatternElement.properties_i.linkedElementIndex, elementIndex);
                        }

                        // Any non-ortho cases? The 1 looks weird here, but is necessary to avoid capturing the non-orthogonal case twice.
                        while (pattElements[elementIndex].nonOrthoGeometry.Count > 1)
                        {
                            addPatternElement(name);
                            offset++;
                            pattElements[elementIndex + offset].parsePoints(pattElements[elementIndex].nonOrthoGeometry[0], isText:false, vertical:vertical);
                            if (offset > 1)
                            {
                                // Set relative rotation pivot flag here.
                                pattElements[elementIndex + offset].setInt(PatternElement.properties_i.refPivot, 1);
                            }
                            pattElements[elementIndex].nonOrthoGeometry.RemoveAt(0);
                            // Register our 'parent' element for the decomposed entries.
                            pattElements[elementIndex + offset].setInt(PatternElement.properties_i.linkedElementIndex, elementIndex);
                        }

                        // Reverse direction due to the order of dependency
                        for (int e = offset; e > 0; e--)
                        {
                            // Is previous element a rectangle? If not, don't try and make a relative position.
                            if (pattElements[e - 1].getInt(PatternElement.properties_i.shapeIndex) != (int)CentralProperties.typeShapes.rectangle)
                            {
                                continue;
                            }

                            // Reference position and width
                            decimal rX = pattElements[e - 1].getDecimal(PatternElement.properties_decimal.minXPos);
                            decimal rY = pattElements[e - 1].getDecimal(PatternElement.properties_decimal.minYPos);
                            decimal rW = pattElements[e - 1].getDecimal(PatternElement.properties_decimal.s0MinHorLength);
                            decimal rH = pattElements[e - 1].getDecimal(PatternElement.properties_decimal.s0MinVerLength);

                            // Calculate new position values.
                            decimal eX = pattElements[e].getDecimal(PatternElement.properties_decimal.minXPos);
                            decimal eY = pattElements[e].getDecimal(PatternElement.properties_decimal.minYPos);

                            eX -= rX;
                            pattElements[e].setInt(PatternElement.properties_i.xPosSubShapeRefPos, (int)CommonVars.subShapeHorLocs.L);
                            if (eX >= rW)
                            {
                                eX -= rW;
                                pattElements[e].setInt(PatternElement.properties_i.xPosSubShapeRefPos, (int)CommonVars.subShapeHorLocs.R);
                            }

                            eY -= rY;
                            pattElements[e].setInt(PatternElement.properties_i.yPosSubShapeRefPos, (int)CommonVars.subShapeVerLocs.B);
                            if (eY >= rH)
                            {
                                eY -= rH;
                                pattElements[e].setInt(PatternElement.properties_i.yPosSubShapeRefPos, (int)CommonVars.subShapeVerLocs.T);
                            }

                            // Set relative position for the current element.
                            pattElements[e].setInt(PatternElement.properties_i.xPosRef, e);
                            pattElements[e].setInt(PatternElement.properties_i.yPosRef, e);
                            pattElements[e].setInt(PatternElement.properties_i.rotationRef, e);
                            pattElements[e].setInt(PatternElement.properties_i.refPivot, 1);

                            // Set new position values.
                            pattElements[e].setDecimal(PatternElement.properties_decimal.minXPos, eX);
                            pattElements[e].setDecimal(PatternElement.properties_decimal.minYPos, eY);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    elementIndex += offset;
                    elementIndex++;

                    if (breakOut)
                    {
                        break;
                    }
                }
            }

            ret = true;

            return ret;
        }
    }
}
