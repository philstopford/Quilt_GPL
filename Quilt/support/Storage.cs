using Error;
using geoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
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

        public delegate void UpdateUIStatus(string text);
        public UpdateUIStatus updateUIstatus { get; set; }


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

            char[] polySep = new[] { ';' };
            char[] coordSep = new[] { ',' };

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
                returnList.Add(new[] { new GeoLibPointF(0, 0) });
                returnList.Add(new[] { new GeoLibPointF(0, 0) });
                returnList.Add(new[] { new GeoLibPointF(0, 0) });
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
                returnString += fileData[poly][pt].X + "," + fileData[poly][pt].Y;
                pt++;
                while (pt < fileData[poly].Count())
                {
                    returnString += "," + fileData[poly][pt].X + "," + fileData[poly][pt].Y;
                    pt++;
                }
                poly++;
                while (poly < fileData.Count())
                {
                    returnString += ";";
                    pt = 0;
                    returnString += fileData[poly][0].X + "," + fileData[poly][0].Y;
                    pt++;
                    while (pt < fileData[poly].Count())
                    {
                        returnString += "," + fileData[poly][pt].X + "," + fileData[poly][pt].Y;
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
            XDocument doc = new XDocument(new XElement(CentralProperties.productName));
            // ReSharper disable once PossibleNullReferenceException
            doc.Root.Add(new XElement("version", CentralProperties.version));
            doc.Root.Add(new XElement("elementCount", quilt.getPatternElements(0).Count));

            int elementCount = quilt.getPatternElements(0).Count;
            
            int progressInterval = elementCount / 100;
            int maxProgress = Math.Max(1, elementCount);
            if (progressInterval < 1)
            {
                progressInterval = 1;
            }
            
            for (int i = 0; i < elementCount; i++)
            {
                PatternElement tmp = quilt.getPatternElement(patternIndex: 0, i);
                XElement xelement = new XElement("layer" + (i + 1),
                    new XElement("name", tmp.getString(PatternElement.properties_s.name)),

                    new XElement("shapeIndex", tmp.getInt(PatternElement.properties_i.shapeIndex)),

                    new XElement("s0MinHorLength", tmp.getDecimal(PatternElement.properties_decimal.minHorLength, 0)),
                    new XElement("s0MinHorOffset", tmp.getDecimal(PatternElement.properties_decimal.minHorOffset, 0)),
                    new XElement("s0MinVerLength", tmp.getDecimal(PatternElement.properties_decimal.minVerLength, 0)),
                    new XElement("s0MinVerOffset", tmp.getDecimal(PatternElement.properties_decimal.minVerOffset, 0)),
                    new XElement("s0HorLengthInc", tmp.getDecimal(PatternElement.properties_decimal.horLengthInc, 0)),
                    new XElement("s0HorOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.horOffsetInc, 0)),
                    new XElement("s0VerLengthInc", tmp.getDecimal(PatternElement.properties_decimal.verLengthInc, 0)),
                    new XElement("s0VerOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.verOffsetInc, 0)),
                    new XElement("s0HorLengthSteps", tmp.getInt(PatternElement.properties_i.horLengthSteps, 0)),
                    new XElement("s0HorOffsetSteps", tmp.getInt(PatternElement.properties_i.horOffsetSteps, 0)),
                    new XElement("s0VerLengthSteps", tmp.getInt(PatternElement.properties_i.verLengthSteps, 0)),
                    new XElement("s0VerOffsetSteps", tmp.getInt(PatternElement.properties_i.verOffsetSteps, 0)),
                    
                    new XElement("s1MinHorLength", tmp.getDecimal(PatternElement.properties_decimal.minHorLength, 1)),
                    new XElement("s1MinHorOffset", tmp.getDecimal(PatternElement.properties_decimal.minHorOffset, 1)),
                    new XElement("s1MinVerLength", tmp.getDecimal(PatternElement.properties_decimal.minVerLength, 1)),
                    new XElement("s1MinVerOffset", tmp.getDecimal(PatternElement.properties_decimal.minVerOffset, 1)),
                    new XElement("s1HorLengthInc", tmp.getDecimal(PatternElement.properties_decimal.horLengthInc, 1)),
                    new XElement("s1HorOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.horOffsetInc, 1)),
                    new XElement("s1VerLengthInc", tmp.getDecimal(PatternElement.properties_decimal.verLengthInc, 1)),
                    new XElement("s1VerOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.verOffsetInc, 1)),
                    new XElement("s1HorLengthSteps", tmp.getInt(PatternElement.properties_i.horLengthSteps, 1)),
                    new XElement("s1HorOffsetSteps", tmp.getInt(PatternElement.properties_i.horOffsetSteps, 1)),
                    new XElement("s1VerLengthSteps", tmp.getInt(PatternElement.properties_i.verLengthSteps, 1)),
                    new XElement("s1VerOffsetSteps", tmp.getInt(PatternElement.properties_i.verOffsetSteps, 1)),

                    new XElement("s2MinHorLength", tmp.getDecimal(PatternElement.properties_decimal.minHorLength, 2)),
                    new XElement("s2MinHorOffset", tmp.getDecimal(PatternElement.properties_decimal.minHorOffset, 2)),
                    new XElement("s2MinVerLength", tmp.getDecimal(PatternElement.properties_decimal.minVerLength, 2)),
                    new XElement("s2MinVerOffset", tmp.getDecimal(PatternElement.properties_decimal.minVerOffset, 2)),
                    new XElement("s2HorLengthInc", tmp.getDecimal(PatternElement.properties_decimal.horLengthInc, 2)),
                    new XElement("s2HorOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.horOffsetInc, 2)),
                    new XElement("s2VerLengthInc", tmp.getDecimal(PatternElement.properties_decimal.verLengthInc, 2)),
                    new XElement("s2VerOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.verOffsetInc, 2)),
                    new XElement("s2HorLengthSteps", tmp.getInt(PatternElement.properties_i.horLengthSteps, 2)),
                    new XElement("s2HorOffsetSteps", tmp.getInt(PatternElement.properties_i.horOffsetSteps, 2)),
                    new XElement("s2VerLengthSteps", tmp.getInt(PatternElement.properties_i.verLengthSteps, 2)),
                    new XElement("s2VerOffsetSteps", tmp.getInt(PatternElement.properties_i.verOffsetSteps, 2)),

                    new XElement("s0MinHorLengthRef", tmp.getInt(PatternElement.properties_i.MinHLRef, 0)),
                    new XElement("s0MinHorLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHLSubShapeRef, 0)),
                    new XElement("s0HorLengthRefFinal", tmp.getInt(PatternElement.properties_i.HLRefFinal, 0)),
                    new XElement("s0HorLengthIncRef", tmp.getInt(PatternElement.properties_i.HLIncRef, 0)),
                    new XElement("s0HorLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HLIncSubShapeRef, 0)),
                    new XElement("s0HorLengthStepsRef", tmp.getInt(PatternElement.properties_i.HLStepsRef, 0)),
                    new XElement("s0HorLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HLStepsSubShapeRef, 0)),

                    new XElement("s1MinHorLengthRef", tmp.getInt(PatternElement.properties_i.MinHLRef, 1)),
                    new XElement("s1MinHorLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHLSubShapeRef, 1)),
                    new XElement("s1HorLengthRefFinal", tmp.getInt(PatternElement.properties_i.HLRefFinal, 1)),
                    new XElement("s1HorLengthIncRef", tmp.getInt(PatternElement.properties_i.HLIncRef, 1)),
                    new XElement("s1HorLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HLIncSubShapeRef, 1)),
                    new XElement("s1HorLengthStepsRef", tmp.getInt(PatternElement.properties_i.HLStepsRef, 1)),
                    new XElement("s1HorLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HLStepsSubShapeRef, 1)),

                    new XElement("s2MinHorLengthRef", tmp.getInt(PatternElement.properties_i.MinHLRef, 2)),
                    new XElement("s2MinHorLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHLSubShapeRef, 2)),
                    new XElement("s2HorLengthRefFinal", tmp.getInt(PatternElement.properties_i.HLRefFinal, 2)),
                    new XElement("s2HorLengthIncRef", tmp.getInt(PatternElement.properties_i.HLIncRef, 2)),
                    new XElement("s2HorLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HLIncSubShapeRef, 2)),
                    new XElement("s2HorLengthStepsRef", tmp.getInt(PatternElement.properties_i.HLStepsRef, 2)),
                    new XElement("s2HorLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HLStepsSubShapeRef, 2)),
                    
                    new XElement("s0MinVerLengthRef", tmp.getInt(PatternElement.properties_i.MinVLRef, 0)),
                    new XElement("s0MinVerLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVLSubShapeRef, 0)),
                    new XElement("s0VerLengthRefFinal", tmp.getInt(PatternElement.properties_i.VLRefFinal, 0)),
                    new XElement("s0VerLengthIncRef", tmp.getInt(PatternElement.properties_i.VLIncRef, 0)),
                    new XElement("s0VerLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VLIncSubShapeRef, 0)),
                    new XElement("s0VerLengthStepsRef", tmp.getInt(PatternElement.properties_i.VLStepsRef, 0)),
                    new XElement("s0VerLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VLStepsSubShapeRef, 0)),

                    new XElement("s1MinVerLengthRef", tmp.getInt(PatternElement.properties_i.MinVLRef, 1)),
                    new XElement("s1MinVerLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVLSubShapeRef, 1)),
                    new XElement("s1VerLengthRefFinal", tmp.getInt(PatternElement.properties_i.VLRefFinal, 1)),
                    new XElement("s1VerLengthIncRef", tmp.getInt(PatternElement.properties_i.VLIncRef, 1)),
                    new XElement("s1VerLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VLIncSubShapeRef, 1)),
                    new XElement("s1VerLengthStepsRef", tmp.getInt(PatternElement.properties_i.VLStepsRef, 1)),
                    new XElement("s1VerLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VLStepsSubShapeRef, 1)),

                    new XElement("s2MinVerLengthRef", tmp.getInt(PatternElement.properties_i.MinVLRef, 2)),
                    new XElement("s2MinVerLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVLSubShapeRef, 2)),
                    new XElement("s2VerLengthRefFinal", tmp.getInt(PatternElement.properties_i.VLRefFinal, 2)),
                    new XElement("s2VerLengthIncRef", tmp.getInt(PatternElement.properties_i.VLIncRef, 2)),
                    new XElement("s2VerLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VLIncSubShapeRef, 2)),
                    new XElement("s2VerLengthStepsRef", tmp.getInt(PatternElement.properties_i.VLStepsRef, 2)),
                    new XElement("s2VerLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VLStepsSubShapeRef, 2)),

                    new XElement("s0MinHorOffsetRef", tmp.getInt(PatternElement.properties_i.MinHORef, 0)),
                    new XElement("s0MinHorOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHOSubShapeRef, 0)),
                    new XElement("s0HorOffsetRefFinal", tmp.getInt(PatternElement.properties_i.HORefFinal, 0)),
                    new XElement("s0HorOffsetIncRef", tmp.getInt(PatternElement.properties_i.HOIncRef, 0)),
                    new XElement("s0HorOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HOIncSubShapeRef, 0)),
                    new XElement("s0HorOffsetStepsRef", tmp.getInt(PatternElement.properties_i.HOStepsRef, 0)),
                    new XElement("s0HorOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HOStepsSubShapeRef, 0)),

                    new XElement("s1MinHorOffsetRef", tmp.getInt(PatternElement.properties_i.MinHORef, 1)),
                    new XElement("s1MinHorOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHOSubShapeRef, 1)),
                    new XElement("s1HorOffsetRefFinal", tmp.getInt(PatternElement.properties_i.HORefFinal, 1)),
                    new XElement("s1HorOffsetIncRef", tmp.getInt(PatternElement.properties_i.HOIncRef, 1)),
                    new XElement("s1HorOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HOIncSubShapeRef, 1)),
                    new XElement("s1HorOffsetStepsRef", tmp.getInt(PatternElement.properties_i.HOStepsRef, 1)),
                    new XElement("s1HorOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HOStepsSubShapeRef, 1)),

                    new XElement("s2MinHorOffsetRef", tmp.getInt(PatternElement.properties_i.MinHORef, 2)),
                    new XElement("s2MinHorOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHOSubShapeRef, 2)),
                    new XElement("s2HorOffsetRefFinal", tmp.getInt(PatternElement.properties_i.HORefFinal, 2)),
                    new XElement("s2HorOffsetIncRef", tmp.getInt(PatternElement.properties_i.HOIncRef, 2)),
                    new XElement("s2HorOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HOIncSubShapeRef, 2)),
                    new XElement("s2HorOffsetStepsRef", tmp.getInt(PatternElement.properties_i.HOStepsRef, 2)),
                    new XElement("s2HorOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HOStepsSubShapeRef, 2)),

                    new XElement("s0MinVerOffsetRef", tmp.getInt(PatternElement.properties_i.MinVORef, 0)),
                    new XElement("s0MinVerOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVOSubShapeRef, 0)),
                    new XElement("s0VerOffsetRefFinal", tmp.getInt(PatternElement.properties_i.VORefFinal, 0)),
                    new XElement("s0VerOffsetIncRef", tmp.getInt(PatternElement.properties_i.VOIncRef, 0)),
                    new XElement("s0VerOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VOIncSubShapeRef, 0)),
                    new XElement("s0VerOffsetStepsRef", tmp.getInt(PatternElement.properties_i.VOStepsRef, 0)),
                    new XElement("s0VerOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VOStepsSubShapeRef, 0)),

                    new XElement("s1MinVerOffsetRef", tmp.getInt(PatternElement.properties_i.MinVORef, 1)),
                    new XElement("s1MinVerOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVOSubShapeRef, 1)),
                    new XElement("s1VerOffsetRefFinal", tmp.getInt(PatternElement.properties_i.VORefFinal, 1)),
                    new XElement("s1VerOffsetIncRef", tmp.getInt(PatternElement.properties_i.VOIncRef, 1)),
                    new XElement("s1VerOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VOIncSubShapeRef, 1)),
                    new XElement("s1VerOffsetStepsRef", tmp.getInt(PatternElement.properties_i.VOStepsRef, 1)),
                    new XElement("s1VerOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VOStepsSubShapeRef, 1)),

                    new XElement("s2MinVerOffsetRef", tmp.getInt(PatternElement.properties_i.MinVORef, 2)),
                    new XElement("s2MinVerOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVOSubShapeRef, 2)),
                    new XElement("s2VerOffsetRefFinal", tmp.getInt(PatternElement.properties_i.VORefFinal, 2)),
                    new XElement("s2VerOffsetIncRef", tmp.getInt(PatternElement.properties_i.VOIncRef, 2)),
                    new XElement("s2VerOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VOIncSubShapeRef, 2)),
                    new XElement("s2VerOffsetStepsRef", tmp.getInt(PatternElement.properties_i.VOStepsRef, 2)),
                    new XElement("s2VerOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VOStepsSubShapeRef, 2)),
                    
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

                if (i + 1 % progressInterval == 0)
                {
                    updateUIProgress?.Invoke(i + 1, maxProgress);
                }
            }

            doc.Root.Add(new XElement("quilt",
                new XElement("padding", quilt.getPadding()),
                new XElement("showDrawn", quilt.getShowInput())
            ));

            double[] camParameters = viewportSave?.Invoke();
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
            return pLoadQuiltSettings(filename, quiltContext:quiltContext);
        }

        void pLoadSubShapeReferenceDimensionSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinHLRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"MinHorLengthRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinHLRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinHLSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"MinHorLengthSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinHLSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HLRefFinal,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"HorLengthRefFinal")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HLRefFinal, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HLIncRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"HorLengthIncRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HLIncRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HLIncSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"HorLengthIncSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HLIncSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HLStepsRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"HorLengthStepsRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HLStepsRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HLStepsSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"HorLengthStepsSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HLStepsSubShapeRef, i);
                }
                
                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinVLRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"MinVerLengthRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinVLRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinVLSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"MinVerLengthSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinVLSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VLRefFinal,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"VerLengthRefFinal")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VLRefFinal, i);
                }


                try
                {
                    readSettings.setInt(PatternElement.properties_i.VLIncRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"VerLengthIncRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VLIncRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VLIncSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"VerLengthIncSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VLIncSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VLStepsRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"VerLengthStepsRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VLStepsRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VLStepsSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"VerLengthStepsSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VLStepsSubShapeRef, i);
                }
            }
        }

        void pLoadSubShapeReferenceOffsetSettings(ref PatternElement readSettings,
            ref XElement simulationFromFile, string layerref)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinHORef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"MinHorOffsetRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinHORef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinHOSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"MinHorOffsetSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinHOSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HORefFinal,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"HorOffsetRefFinal")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HORefFinal, i);
                }
                
                try
                {
                    readSettings.setInt(PatternElement.properties_i.HOIncRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"HorOffsetIncRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HOIncRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HOIncSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"HorOffsetIncSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HOIncSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HOStepsRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"HorOffsetStepsRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HOStepsRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.HOStepsSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"HorOffsetStepsSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.HOStepsSubShapeRef, i);
                }
                
                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinVORef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"MinVerOffsetRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinVORef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.MinVOSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"MinVerOffsetSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.MinVOSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VORefFinal,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"VerOffsetRefFinal")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VORefFinal, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VOIncRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"VerOffsetIncRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VOIncRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VOIncSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"VerOffsetIncSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VOIncSubShapeRef, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VOStepsRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s"+i+"VerOffsetStepsRef")
                            .First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VOStepsRef);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.VOStepsSubShapeRef,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref)
                            .Descendants("s"+i+"VerOffsetStepsSubShapeRef").First().Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.VOStepsSubShapeRef, i);
                }

            }
        }
        
        void pLoadSubShapeDimensionSettings(ref PatternElement readSettings, ref XElement simulationFromFile,
            string layerref)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.minHorLength,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "MinHorLength").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.minHorLength, i);
                }

                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.horLengthInc,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "HorLengthInc").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.horLengthInc, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.horLengthSteps,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s" + i + "HorLengthSteps").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.horLengthSteps, i);
                }

                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.minVerLength,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "MinVerLength").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.minVerLength, i);
                }

                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.verLengthInc,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "VerLengthInc").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.verLengthInc, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.verLengthSteps,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s" + i + "VerLengthSteps").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.verLengthSteps, i);
                }
            }
        }

        void pLoadSubShapeOffsetSettings(ref PatternElement readSettings, ref XElement simulationFromFile,
            string layerref)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.minHorOffset,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "MinHorOffset").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.minHorOffset, i);
                }

                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.horOffsetInc,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "HorOffsetInc").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.horOffsetInc, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.horOffsetSteps,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s" + i + "HorOffsetSteps").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.horOffsetSteps, i);
                }

                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.minVerOffset,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "MinVerOffset").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.minVerOffset, i);
                }

                try
                {
                    readSettings.setDecimal(PatternElement.properties_decimal.verOffsetInc,
                        Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("s" + i + "VerOffsetInc").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultDecimal(PatternElement.properties_decimal.verOffsetInc, i);
                }

                try
                {
                    readSettings.setInt(PatternElement.properties_i.verOffsetSteps,
                        Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s" + i + "VerOffsetSteps").First()
                            .Value), i);
                }
                catch (Exception)
                {
                    readSettings.defaultInt(PatternElement.properties_i.verOffsetSteps, i);
                }
            }

        }

        void pLoadBoundingBoxSettings(ref PatternElement readSettings, ref XElement simulationFromFile,
            string layerref)
        {
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
        }
        
        void pLoadSubShapeSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref, QuiltContext quiltContext)
        {
            readSettings.defaultInt(PatternElement.properties_i.shape0Tip);
            readSettings.defaultInt(PatternElement.properties_i.shape1Tip);
            readSettings.defaultInt(PatternElement.properties_i.shape2Tip);

            pLoadSubShapeDimensionSettings(ref readSettings, ref simulationFromFile, layerref);
            pLoadSubShapeOffsetSettings(ref readSettings, ref simulationFromFile, layerref);

            pLoadSubShapeReferenceDimensionSettings(ref readSettings, ref simulationFromFile, layerref);
            pLoadSubShapeReferenceOffsetSettings(ref readSettings, ref simulationFromFile, layerref);
            
            pLoadBoundingBoxSettings(ref readSettings, ref simulationFromFile, layerref);
            
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
                bool abortLoad = false;
                readSettings.parsePoints(ref abortLoad, fileDataFromString(simulationFromFile.Descendants(layerref).Descendants("nonOrthoGeo").First().Value)[0], layer: readSettings.getInt(PatternElement.properties_i.layoutLayer), datatype: readSettings.getInt(PatternElement.properties_i.layoutDataType), isText:false, vertical:quiltContext.verticalRectDecomp);
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

        string pLoadQuiltSettings(string filename, QuiltContext quiltContext)
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
                return ex.Message;
            }

            if (simulationFromFile.Name != CentralProperties.productName)
            {
                return "This is not a " + CentralProperties.productName + " project file.";
            }

            string version = simulationFromFile.Descendants("version").First().Value;

            if (version != CentralProperties.version)
            {
                ErrorReporter.showMessage_OK("Settings file for version " + version, "Legacy import");
            }

            preLoadUI?.Invoke();

            int elementCount = Convert.ToInt32(simulationFromFile.Descendants("elementCount").First().Value);
            loadedElements = new PatternElement[elementCount];

            int progressInterval = elementCount / 100;
            int maxProgress = Math.Max(1, elementCount);
            if (progressInterval < 1)
            {
                progressInterval = 1;
            }

            int progressCounter = 1;
            
            updateUIstatus?.Invoke("Loading...");
            
#if QUILTTHREADED
            Parallel.For(0, elementCount, (layer, loopstate)  =>
#else
            for (int layer = 0; layer < elementCount; layer++)
#endif
            {
                PatternElement readSettings = new PatternElement();

                string layerref = "layer" + (layer + 1);

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
#if QUILTTHREADED
                Interlocked.Increment(ref progressCounter);
#else
                progressCounter++;      
#endif
                if (progressCounter % progressInterval == 0)
                {
                    updateUIProgress?.Invoke(progressCounter, maxProgress);
                }

            }
#if QUILTTHREADED
            );
#endif

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
                viewportLoad?.Invoke(new[] { x, y, zoom });
            }
            catch (Exception e)
            {
                ErrorReporter.showMessage_OK("Failed loading: " + e, "Error");
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
