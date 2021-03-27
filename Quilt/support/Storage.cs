using Error;
using geoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quilt
{
    public class Storage
    {
        public delegate void PreLoadUI();
        public PreLoadUI preLoadUI { get; set; }
        public delegate void PostLoadUI(string loadedFile);
        public PostLoadUI postLoadUI { get; set; }

        public delegate void UpdateUIProgress(int val, int max);
        public UpdateUIProgress updateUIProgress { get; set; }

        double padding;
        int showDrawn;
        PatternElement[] loadedElements;

        public double getPadding()
        {
            return pGetPadding();
        }

        double pGetPadding()
        {
            return padding;
        }

        public int getShowInput()
        {
            return pGetShowDrawn();
        }

        int pGetShowDrawn()
        {
            return showDrawn;
        }

        public List<PatternElement> getElements()
        {
            return pGetElements();
        }

        List<PatternElement> pGetElements()
        {
            return loadedElements.ToList();
        }

        // Callbacks for viewport values.
        public delegate void viewportLoadCallback(double[] camInfo);
        public viewportLoadCallback viewportLoad { get; set; }
        public delegate double[] viewportSaveCallback(); // 2 fields : X and Y, zoom
        public viewportSaveCallback viewportSave { get; set; }

        public Storage()
        {
            pStorage();
        }

        void pStorage()
        {
        }

        List<GeoLibPointF[]> fileDataFromString(string fileDataString)
        {
            List<GeoLibPointF[]> returnList = new List<GeoLibPointF[]>();

            char[] polySep = new char[] { ';' };
            char[] coordSep = new char[] { ',' };

            if (fileDataString.Length > 0)
            {
                List<string> hashList = new List<string>();

                string[] polyStringArray = fileDataString.Split(polySep);
                for (int poly = 0; poly < polyStringArray.Count(); poly++)
                {
                    string[] pointStringArray = polyStringArray[poly].Split(coordSep);
                    GeoLibPointF[] polyData = new GeoLibPointF[pointStringArray.Count() / 2]; // since we have two coord values per point (X,Y)
                    int pt = 0;
                    while (pt < pointStringArray.Count())
                    {
                        polyData[pt / 2] = new GeoLibPointF((float)Convert.ToDouble(pointStringArray[pt]), (float)Convert.ToDouble(pointStringArray[pt + 1]));
                        pt += 2;
                    }

                    // Avoid duplicated geometry - this is insurance against older projects files that may have doubled-up polygons included.
                    string p_Hash = utility.Utils.GetMD5Hash(polyData);
                    if (hashList.IndexOf(p_Hash) == -1)
                    {
                        hashList.Add(p_Hash);
                        returnList.Add(polyData);
                    }
                }
            }
            else
            {
                returnList.Add(new GeoLibPointF[] { new GeoLibPointF(0, 0) });
                returnList.Add(new GeoLibPointF[] { new GeoLibPointF(0, 0) });
                returnList.Add(new GeoLibPointF[] { new GeoLibPointF(0, 0) });
            }
            return returnList;
        }

        string stringFromFileData(List<GeoLibPointF[]> fileData)
        {
            string returnString = "";
            if ((fileData != null) && (fileData.Count != 0))
            {
                int poly = 0;
                int pt = 0;
                returnString += fileData[poly][pt].X.ToString() + "," + fileData[poly][pt].Y.ToString();
                pt++;
                while (pt < fileData[poly].Count())
                {
                    returnString += "," + fileData[poly][pt].X.ToString() + "," + fileData[poly][pt].Y.ToString();
                    pt++;
                }
                poly++;
                while (poly < fileData.Count())
                {
                    returnString += ";";
                    pt = 0;
                    returnString += fileData[poly][0].X.ToString() + "," + fileData[poly][0].Y.ToString();
                    pt++;
                    while (pt < fileData[poly].Count())
                    {
                        returnString += "," + fileData[poly][pt].X.ToString() + "," + fileData[poly][pt].Y.ToString();
                        pt++;
                    }
                    poly++;
                }
            }
            else
            {
            }
            return returnString;
        }

        public bool saveQuiltSettings(string filename, ref Stitcher quilt)
        {
            return pSaveQuiltSettings(filename, ref quilt);
        }

        bool pSaveQuiltSettings(string filename, ref Stitcher quilt)
        {
            double[] camParameters;
            XDocument doc = new XDocument(new XElement(CentralProperties.productName));
            doc.Root.Add(new XElement("version", CentralProperties.version));
            doc.Root.Add(new XElement("elementCount", quilt.getPatternElements(0).Count));
            for (int i = 0; i < quilt.getPatternElements(0).Count; i++)
            {
                PatternElement tmp = quilt.getPatternElement(patternIndex: 0, i);
                XElement xelement = new XElement("layer" + (i + 1).ToString(),
                    new XElement("name", tmp.getString(PatternElement.properties_s.name)),

                    new XElement("shapeIndex", tmp.getInt(PatternElement.properties_i.shapeIndex)),

                    new XElement("s0MinHorLength", tmp.getDecimal(PatternElement.properties_decimal.s0MinHorLength)),
                    new XElement("s0MinHorOffset", tmp.getDecimal(PatternElement.properties_decimal.s0MinHorOffset)),
                    new XElement("s0MinVerLength", tmp.getDecimal(PatternElement.properties_decimal.s0MinVerLength)),
                    new XElement("s0MinVerOffset", tmp.getDecimal(PatternElement.properties_decimal.s0MinVerOffset)),
                    new XElement("s0HorLengthInc", tmp.getDecimal(PatternElement.properties_decimal.s0HorLengthInc)),
                    new XElement("s0HorOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.s0HorOffsetInc)),
                    new XElement("s0VerLengthInc", tmp.getDecimal(PatternElement.properties_decimal.s0VerLengthInc)),
                    new XElement("s0VerOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.s0VerOffsetInc)),
                    new XElement("s0HorLengthSteps", tmp.getInt(PatternElement.properties_i.s0HorLengthSteps)),
                    new XElement("s0HorOffsetSteps", tmp.getInt(PatternElement.properties_i.s0HorOffsetSteps)),
                    new XElement("s0VerLengthSteps", tmp.getInt(PatternElement.properties_i.s0VerLengthSteps)),
                    new XElement("s0VerOffsetSteps", tmp.getInt(PatternElement.properties_i.s0VerOffsetSteps)),

                    new XElement("s1MinHorLength", tmp.getDecimal(PatternElement.properties_decimal.s1MinHorLength)),
                    new XElement("s1MinHorOffset", tmp.getDecimal(PatternElement.properties_decimal.s1MinHorOffset)),
                    new XElement("s1MinVerLength", tmp.getDecimal(PatternElement.properties_decimal.s1MinVerLength)),
                    new XElement("s1MinVerOffset", tmp.getDecimal(PatternElement.properties_decimal.s1MinVerOffset)),
                    new XElement("s1HorLengthInc", tmp.getDecimal(PatternElement.properties_decimal.s1HorLengthInc)),
                    new XElement("s1HorOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.s1HorOffsetInc)),
                    new XElement("s1VerLengthInc", tmp.getDecimal(PatternElement.properties_decimal.s1VerLengthInc)),
                    new XElement("s1VerOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.s1VerOffsetInc)),
                    new XElement("s1HorLengthSteps", tmp.getInt(PatternElement.properties_i.s1HorLengthSteps)),
                    new XElement("s1HorOffsetSteps", tmp.getInt(PatternElement.properties_i.s1HorOffsetSteps)),
                    new XElement("s1VerLengthSteps", tmp.getInt(PatternElement.properties_i.s1VerLengthSteps)),
                    new XElement("s1VerOffsetSteps", tmp.getInt(PatternElement.properties_i.s1VerOffsetSteps)),

                    new XElement("s2MinHorLength", tmp.getDecimal(PatternElement.properties_decimal.s2MinHorLength)),
                    new XElement("s2MinHorOffset", tmp.getDecimal(PatternElement.properties_decimal.s2MinHorOffset)),
                    new XElement("s2MinVerLength", tmp.getDecimal(PatternElement.properties_decimal.s2MinVerLength)),
                    new XElement("s2MinVerOffset", tmp.getDecimal(PatternElement.properties_decimal.s2MinVerOffset)),
                    new XElement("s2HorLengthInc", tmp.getDecimal(PatternElement.properties_decimal.s2HorLengthInc)),
                    new XElement("s2HorOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.s2HorOffsetInc)),
                    new XElement("s2VerLengthInc", tmp.getDecimal(PatternElement.properties_decimal.s2VerLengthInc)),
                    new XElement("s2VerOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.s2VerOffsetInc)),
                    new XElement("s2HorLengthSteps", tmp.getInt(PatternElement.properties_i.s2HorLengthSteps)),
                    new XElement("s2HorOffsetSteps", tmp.getInt(PatternElement.properties_i.s2HorOffsetSteps)),
                    new XElement("s2VerLengthSteps", tmp.getInt(PatternElement.properties_i.s2VerLengthSteps)),
                    new XElement("s2VerOffsetSteps", tmp.getInt(PatternElement.properties_i.s2VerOffsetSteps)),

                    new XElement("boundingLeft", tmp.getDecimal(PatternElement.properties_decimal.boundingLeft)),
                    new XElement("boundingLeftInc", tmp.getDecimal(PatternElement.properties_decimal.boundingLeftInc)),
                    new XElement("boundingLeftSteps", tmp.getInt(PatternElement.properties_i.boundingLeftSteps)),

                    new XElement("boundingRight", tmp.getDecimal(PatternElement.properties_decimal.boundingRight)),
                    new XElement("boundingRightInc", tmp.getDecimal(PatternElement.properties_decimal.boundingRightInc)),
                    new XElement("boundingRightSteps", tmp.getInt(PatternElement.properties_i.boundingRightSteps)),

                    new XElement("boundingBottom", tmp.getDecimal(PatternElement.properties_decimal.boundingBottom)),
                    new XElement("boundingBottomInc", tmp.getDecimal(PatternElement.properties_decimal.boundingBottomInc)),
                    new XElement("boundingBottomSteps", tmp.getInt(PatternElement.properties_i.boundingBottomSteps)),

                    new XElement("boundingTop", tmp.getDecimal(PatternElement.properties_decimal.boundingTop)),
                    new XElement("boundingTopInc", tmp.getDecimal(PatternElement.properties_decimal.boundingTopInc)),
                    new XElement("boundingTopSteps", tmp.getInt(PatternElement.properties_i.boundingTopSteps)),

                    new XElement("subShapeIndex", tmp.getInt(PatternElement.properties_i.subShapeIndex)),

                    new XElement("posIndex", tmp.getInt(PatternElement.properties_i.posIndex)),

                    new XElement("minXPos", tmp.getDecimal(PatternElement.properties_decimal.minXPos)),
                    new XElement("xPosInc", tmp.getDecimal(PatternElement.properties_decimal.xPosInc)),
                    new XElement("xPosSteps", tmp.getInt(PatternElement.properties_i.xPosSteps)),
                    new XElement("xPosRef", tmp.getInt(PatternElement.properties_i.xPosRef)),
                    new XElement("xPosSubShapeRef", tmp.getInt(PatternElement.properties_i.xPosSubShapeRef)),
                    new XElement("xPosSubShapeRefPos", tmp.getInt(PatternElement.properties_i.xPosSubShapeRefPos)),

                    new XElement("minYPos", tmp.getDecimal(PatternElement.properties_decimal.minYPos)),
                    new XElement("yPosInc", tmp.getDecimal(PatternElement.properties_decimal.yPosInc)),
                    new XElement("yPosSteps", tmp.getInt(PatternElement.properties_i.yPosSteps)),
                    new XElement("yPosRef", tmp.getInt(PatternElement.properties_i.yPosRef)),
                    new XElement("yPosSubShapeRef", tmp.getInt(PatternElement.properties_i.yPosSubShapeRef)),
                    new XElement("yPosSubShapeRefPos", tmp.getInt(PatternElement.properties_i.yPosSubShapeRefPos)),

                    new XElement("minRotation", tmp.getDecimal(PatternElement.properties_decimal.minRotation)),
                    new XElement("rotationInc", tmp.getDecimal(PatternElement.properties_decimal.rotationInc)),
                    new XElement("rotationSteps", tmp.getInt(PatternElement.properties_i.rotationSteps)),
                    new XElement("rotationRef", tmp.getInt(PatternElement.properties_i.rotationRef)),
                    new XElement("rotRefUseArray", tmp.getInt(PatternElement.properties_i.rotRefUseArray)),
                    new XElement("refBoundsAfterRotation", tmp.getInt(PatternElement.properties_i.refBoundsAfterRotation)),
                    new XElement("refPivot", tmp.getInt(PatternElement.properties_i.refPivot)),

                    new XElement("flipH", tmp.getInt(PatternElement.properties_i.flipH)),
                    new XElement("flipV", tmp.getInt(PatternElement.properties_i.flipV)),
                    new XElement("alignX", tmp.getInt(PatternElement.properties_i.alignX)),
                    new XElement("alignY", tmp.getInt(PatternElement.properties_i.alignY)),

                    new XElement("arrayRef", tmp.getInt(PatternElement.properties_i.arrayRef)),

                    new XElement("arrayMinXCount", tmp.getInt(PatternElement.properties_i.arrayMinXCount)),
                    new XElement("arrayXInc", tmp.getInt(PatternElement.properties_i.arrayXInc)),
                    new XElement("arrayXSteps", tmp.getInt(PatternElement.properties_i.arrayXSteps)),
                    new XElement("arrayMinXSpace", tmp.getDecimal(PatternElement.properties_decimal.arrayMinXSpace)),
                    new XElement("arrayXSpaceInc", tmp.getDecimal(PatternElement.properties_decimal.arrayXSpaceInc)),
                    new XElement("arrayXSpaceSteps", tmp.getInt(PatternElement.properties_i.arrayXSpaceSteps)),
                    new XElement("arrayMinYCount", tmp.getInt(PatternElement.properties_i.arrayMinYCount)),
                    new XElement("arrayYInc", tmp.getInt(PatternElement.properties_i.arrayYInc)),
                    new XElement("arrayYSteps", tmp.getInt(PatternElement.properties_i.arrayYSteps)),
                    new XElement("arrayMinYSpace", tmp.getDecimal(PatternElement.properties_decimal.arrayMinYSpace)),
                    new XElement("arrayYSpaceInc", tmp.getDecimal(PatternElement.properties_decimal.arrayYSpaceInc)),
                    new XElement("arrayYSpaceSteps", tmp.getInt(PatternElement.properties_i.arrayYSpaceSteps)),

                    new XElement("minArrayRotation", tmp.getDecimal(PatternElement.properties_decimal.minArrayRotation)),
                    new XElement("arrayRotationInc", tmp.getDecimal(PatternElement.properties_decimal.arrayRotationInc)),
                    new XElement("arrayRotationSteps", tmp.getInt(PatternElement.properties_i.arrayRotationSteps)),
                    new XElement("arrayRotationRef", tmp.getInt(PatternElement.properties_i.arrayRotationRef)),
                    new XElement("arrayRotRefUseArray", tmp.getInt(PatternElement.properties_i.arrayRotRefUseArray)),
                    new XElement("refArrayBoundsAfterRotation", tmp.getInt(PatternElement.properties_i.refArrayBoundsAfterRotation)),
                    new XElement("refArrayPivot", tmp.getInt(PatternElement.properties_i.refArrayPivot)),

                    new XElement("linkedElementIndex", tmp.getInt(PatternElement.properties_i.linkedElementIndex)),
                    new XElement("nonOrthoGeo", stringFromFileData(tmp.nonOrthoGeometry))
                    );
                doc.Root.Add(xelement);
            }

            doc.Root.Add(new XElement("quilt",
                new XElement("padding", quilt.getPadding()),
                new XElement("showDrawn", quilt.getShowInput())
            ));

            camParameters = viewportSave?.Invoke();
            doc.Root.Add(new XElement("viewport",
                new XElement("displayZoomFactor", (camParameters != null) ? camParameters[2] : 1),
                new XElement("viewportX", (camParameters != null) ? camParameters[0] : 0),
                new XElement("viewportY", (camParameters != null) ? camParameters[1] : 0)
            ));


            bool savedOK = true;
            try
            {
                doc.Save(filename);
            }
            catch (Exception)
            {
                savedOK = false;
            }
            return savedOK;
        }

        public string loadQuilt(string filename, ref Stitcher quilt, QuiltContext quiltContext)
        {
            return pLoadQuiltSettings(filename, ref quilt, quiltContext:quiltContext);
        }

        void pLoadSubShapeSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref, QuiltContext quiltContext)
        {
            readSettings.defaultInt(PatternElement.properties_i.shape0Tip);
            readSettings.defaultInt(PatternElement.properties_i.shape1Tip);
            readSettings.defaultInt(PatternElement.properties_i.shape2Tip);

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0MinHorLength, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0MinHorLength").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0MinHorLength);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0HorLengthInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0HorLengthInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0HorLengthInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s0HorLengthSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s0HorLengthSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s0HorLengthSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0MinHorOffset, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0MinHorOffset").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0MinHorOffset);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0HorOffsetInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0HorOffsetInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0HorOffsetInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s0HorOffsetSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s0HorOffsetSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s0HorOffsetSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0MinVerLength, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0MinVerLength").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0MinVerLength);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0VerLengthInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0VerLengthInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0VerLengthInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s0VerLengthSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s0VerLengthSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s0VerLengthSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0MinVerOffset, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0MinVerOffset").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0MinVerOffset);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s0VerOffsetInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s0VerOffsetInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s0VerOffsetInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s0VerOffsetSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s0VerOffsetSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s0VerOffsetSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1MinHorLength, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1MinHorLength").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1MinHorLength);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1HorLengthInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1HorLengthInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1HorLengthInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s1HorLengthSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s1HorLengthSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s1HorLengthSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1MinHorOffset").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1MinHorOffset);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1HorOffsetInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1HorOffsetInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s1HorOffsetSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s1HorOffsetSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1MinVerLength, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1MinVerLength").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1MinVerLength);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1VerLengthInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1VerLengthInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1VerLengthInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s1VerLengthSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s1VerLengthSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s1VerLengthSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1MinVerOffset").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1MinVerOffset);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s1VerOffsetInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s1VerOffsetInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s1VerOffsetSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s1VerOffsetSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2MinHorLength, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2MinHorLength").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2MinHorLength);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2HorLengthInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2HorLengthInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2HorLengthInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s2HorLengthSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s2HorLengthSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s2HorLengthSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2MinHorOffset, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2MinHorOffset").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2MinHorOffset);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2HorOffsetInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2HorOffsetInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2HorOffsetInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s2HorOffsetSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s2HorOffsetSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s2HorOffsetSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2MinVerLength, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2MinVerLength").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2MinVerLength);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2VerLengthInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2VerLengthInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2VerLengthInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s2VerLengthSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s2VerLengthSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s2VerLengthSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2MinVerOffset, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2MinVerOffset").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2MinVerOffset);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.s2VerOffsetInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s2VerOffsetInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.s2VerOffsetInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.s2VerOffsetSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s2VerOffsetSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.s2VerOffsetSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingLeft, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingLeft").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingLeft);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingLeftInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingLeftInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingLeftInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.boundingLeftSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("boundingLeftSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.boundingLeftSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingRight, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingRight").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingRight);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingRightInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingRightInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingRightInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.boundingRightSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("boundingRightSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.boundingRightSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingTop, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingTop").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingTop);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingTopInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingTopInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingTopInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.boundingTopSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("boundingTopSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.boundingTopSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingBottom, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingBottom").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingBottom);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.boundingBottomInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("boundingBottomInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.boundingBottomInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.boundingBottomSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("boundingBottomSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.boundingBottomSteps);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.linkedElementIndex, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("linkedElementIndex").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.linkedElementIndex);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.layoutLayer, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("layoutLayer").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.layoutLayer);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.layoutDataType, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("layoutDataType").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.layoutDataType);
            }

            try
            {
                // Get geometry back from string in XML, then process only the first polygon (should only have one polygon anyway). This is not text, so mark as false.
                readSettings.parsePoints(fileDataFromString(simulationFromFile.Descendants(layerref).Descendants("nonOrthoGeo").First().Value)[0], layer: readSettings.getInt(PatternElement.properties_i.layoutLayer), datatype: readSettings.getInt(PatternElement.properties_i.layoutDataType), isText:false, vertical:quiltContext.verticalRectDecomp);
            }
            catch (Exception)
            {

            }
        }

        void pLoadPositionRotationSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref)
        {
            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.minXPos, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("minXPos").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.minXPos);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.xPosInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("xPosInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.xPosInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.xPosSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("xPosSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.xPosSteps);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.xPosRef, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("xPosRef").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.xPosRef);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.xPosSubShapeRef, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("xPosSubShapeRef").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.xPosSubShapeRef);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.xPosSubShapeRefPos, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("xPosSubShapeRefPos").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.xPosSubShapeRefPos);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.minYPos, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("minYPos").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.minYPos);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.yPosInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("yPosInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.yPosInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.yPosSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("yPosSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.yPosSteps);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.yPosRef, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("yPosRef").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.yPosRef);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.yPosSubShapeRef, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("yPosSubShapeRef").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.yPosSubShapeRef);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.yPosSubShapeRefPos, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("yPosSubShapeRefPos").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.yPosSubShapeRefPos);
            }


            try
            {
                readSettings.setInt(PatternElement.properties_i.posIndex, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("posIndex").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.posIndex);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.flipH, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("flipH").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.flipH);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.flipV, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("flipV").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.flipV);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.alignX, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("align").First().Value));
                readSettings.setInt(PatternElement.properties_i.alignY, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("align").First().Value));
            }
            catch (Exception)
            {
                try
                {
                    readSettings.setInt(PatternElement.properties_i.alignX, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("alignX").First().Value));
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.alignX);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.alignY, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("alignY").First().Value));
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.alignY);
                }
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.minRotation, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("minRotation").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.minRotation);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.rotationInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("rotationInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.rotationInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.rotationSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("rotationSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.rotationSteps);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.rotationRef, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("rotationRef").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.rotationRef);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.rotRefUseArray, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("rotRefUseArray").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.rotRefUseArray);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayRef, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayRef").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayRef);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayMinXCount, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayXCount").First().Value));
            }
            catch (Exception)
            {
                try
                {
                    readSettings.setInt(PatternElement.properties_i.arrayMinXCount, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayMinXCount").First().Value));
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.arrayMinXCount);
                }
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayXSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayXSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayXSteps);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayXInc, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayXInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayXInc);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.arrayMinXSpace, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("arrayMinXSpace").First().Value));
            }
            catch (Exception)
            {
                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.arrayMinXSpace, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("arrayXSpace").First().Value));
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.arrayMinXSpace);
                }
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayXSpaceSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayXSpaceSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayXSpaceSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.arrayXSpaceInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("arrayXSpaceInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.arrayXSpaceInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayMinYCount, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayYCount").First().Value));
            }
            catch (Exception)
            {
                try
                {
                    readSettings.setInt(PatternElement.properties_i.arrayMinYCount, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayMinYCount").First().Value));
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.arrayMinYCount);
                }
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayYSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayYSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayYSteps);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayYInc, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayYInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayYInc);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.arrayMinYSpace, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("arrayMinYSpace").First().Value));
            }
            catch (Exception)
            {
                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.arrayMinYSpace, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("arrayYSpace").First().Value));
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.arrayMinYSpace);
                }
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayYSpaceSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayYSpaceSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayYSpaceSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.arrayYSpaceInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("arrayYSpaceInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.arrayYSpaceInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayRotationSteps, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayRotationSteps").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayRotationSteps);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.minArrayRotation, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("minArrayRotation").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.minArrayRotation);
            }

            try
            {
                readSettings.setDecimal(PatternElement.properties_decimal.arrayRotationInc, Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("arrayRotationInc").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultDecimal(PatternElement.properties_decimal.arrayRotationInc);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayRotationRef, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayRotationRef").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayRotationRef);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.arrayRotRefUseArray, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayRotRefUseArray").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.arrayRotRefUseArray);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.refBoundsAfterRotation, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("refBoundsAfterRotation").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.refBoundsAfterRotation);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.refPivot, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("refPivot").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.refPivot);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.refArrayBoundsAfterRotation, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("refArrayBoundsAfterRotation").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.refArrayBoundsAfterRotation);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.refArrayPivot, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("refArrayPivot").First().Value));
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.refArrayPivot);
            }
        }

        string pLoadQuiltSettings(string filename, ref Stitcher quilt, QuiltContext quiltContext)
        {

            string returnString = "";
            bool error = false;
            XElement simulationFromFile;
            try
            {
                simulationFromFile = XElement.Load(filename);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            if (simulationFromFile.Name != CentralProperties.productName)
            {
                error = true;
                return "This is not a " + CentralProperties.productName + " project file.";
            }

            string version = simulationFromFile.Descendants("version").First().Value;
            string[] tokenVersion = version.Split(new char[] { '.' });

            if (version != CentralProperties.version)
            {
                ErrorReporter.showMessage_OK("Settings file for version " + version, "Legacy import");
            }

            preLoadUI?.Invoke();

            int elementCount = Convert.ToInt32(simulationFromFile.Descendants("elementCount").First().Value);
            loadedElements = new PatternElement[elementCount];

            int updateSample = elementCount / 100;

            if (updateSample < 1)
            {
                updateSample = 1;
            }

            Parallel.For(0, elementCount, (layer, loopstate)  =>

            //for (int layer = 0; layer < elementCount; layer++)
            {
                PatternElement readSettings = new PatternElement();

                string layerref = "layer" + (layer + 1).ToString();

                try
                {
                    readSettings.setString(PatternElement.properties_s.name, simulationFromFile.Descendants(layerref).Descendants("name").First().Value);
                }
                catch (Exception)
                {
                    readSettings.setString(PatternElement.properties_s.name, layerref);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.shapeIndex, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("shapeIndex").First().Value));
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.shapeIndex);
                }

                pLoadSubShapeSettings(ref readSettings, ref simulationFromFile, layerref, quiltContext);

                pLoadPositionRotationSettings(ref readSettings, ref simulationFromFile, layerref);

                loadedElements[layer] = readSettings;

            });

            try
            {
                try
                {
                    padding = Convert.ToDouble(simulationFromFile.Descendants("quilt").Descendants("padding").First().Value);
                }
                catch (Exception)
                {
                    padding = 0;
                }

                try
                {
                    showDrawn = Convert.ToInt32(simulationFromFile.Descendants("quilt").Descendants("showDrawn").First().Value);
                }
                catch (Exception)
                {
                    showDrawn = 1;
                }

                double x = 0;
                double y = 0;
                double zoom = 1;
                try
                {
                    x = Convert.ToDouble(simulationFromFile.Descendants("viewport").Descendants("viewportX").First().Value);
                    y = Convert.ToDouble(simulationFromFile.Descendants("viewport").Descendants("viewportY").First().Value);
                    zoom = Convert.ToDouble(simulationFromFile.Descendants("viewport").Descendants("displayZoomFactor").First().Value);
                }
                catch (Exception)
                {
                }
                viewportLoad?.Invoke(new double[] { x, y, zoom });
            }
            catch (Exception e)
            {
                ErrorReporter.showMessage_OK("Failed loading: " + e.ToString(), "Error");
                error = true;
            }

            postLoadUI?.Invoke(filename);
            if (error)
            {
                loadedElements = null;// new List<PatternElement>();
                padding = 0;
                showDrawn = 1;
                returnString = "";
            }
            return returnString;
        }
    }
}
