using Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Clipper2Lib;

namespace Quilt;

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


    private double padding;
    private int showDrawn;
    private PatternElement[] loadedElements;

    public double getPadding()
    {
        return pGetPadding();
    }

    private double pGetPadding()
    {
        return padding;
    }

    public int getShowInput()
    {
        return pGetShowDrawn();
    }

    private int pGetShowDrawn()
    {
        return showDrawn;
    }

    public List<PatternElement> getElements()
    {
        return pGetElements();
    }

    private List<PatternElement> pGetElements()
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

    private static void pStorage()
    {
    }

    private static PathsD fileDataFromString(string fileDataString)
    {
        PathsD returnList = new();

        char[] polySep = { ';' };
        char[] coordSep = { ',' };

        if (fileDataString.Length > 0)
        {
            List<string> hashList = new();

            string[] polyStringArray = fileDataString.Split(polySep);
            foreach (string t in polyStringArray)
            {
                string[] pointStringArray = t.Split(coordSep);
                PathD polyData = Helper.initedPathD(pointStringArray.Length / 2); // since we have two coord values per point (X,Y)
                int pt = 0;
                while (pt < pointStringArray.Length)
                {
                    polyData[pt / 2] = new (Convert.ToDouble(pointStringArray[pt]), Convert.ToDouble(pointStringArray[pt + 1]));
                    pt += 2;
                }

                // Avoid duplicated geometry - this is insurance against older projects files that may have doubled-up polygons included.
                string p_Hash = utility.Utils.GetMD5Hash(polyData);
                if (hashList.IndexOf(p_Hash) != -1)
                {
                    continue;
                }

                hashList.Add(p_Hash);
                returnList.Add(polyData);
            }
        }
        else
        {
            returnList.Add(new() { new (0, 0) });
            returnList.Add(new() { new (0, 0) });
            returnList.Add(new() { new (0, 0) });
        }
        return returnList;
    }

    private static string stringFromFileData(PathsD fileData)
    {
        string returnString = "";
        if (fileData == null || fileData.Count == 0)
        {
            return returnString;
        }

        int poly = 0;
        int pt = 0;
        returnString += fileData[poly][pt].x + "," + fileData[poly][pt].y;
        pt++;
        while (pt < fileData[poly].Count)
        {
            returnString += "," + fileData[poly][pt].x + "," + fileData[poly][pt].y;
            pt++;
        }
        poly++;
        while (poly < fileData.Count)
        {
            returnString += ";";
            pt = 0;
            returnString += fileData[poly][0].x + "," + fileData[poly][0].y;
            pt++;
            while (pt < fileData[poly].Count)
            {
                returnString += "," + fileData[poly][pt].x + "," + fileData[poly][pt].y;
                pt++;
            }
            poly++;
        }

        return returnString;
    }

    public bool saveQuiltSettings(string filename, ref Stitcher quilt)
    {
        return pSaveQuiltSettings(filename, ref quilt);
    }

    private void pSaveQuiltSettings_element_subshapes(ref XElement xelement, ref PatternElement tmp)
    {
        for (int i = 0; i < 3; i++)
        {
            xelement.Add(
                new XElement("s" + i + "MinHorLength", tmp.getDecimal(PatternElement.properties_decimal.minHorLength, i)),
                new XElement("s" + i + "MinHorOffset", tmp.getDecimal(PatternElement.properties_decimal.minHorOffset, i)),
                new XElement("s" + i + "MinVerLength", tmp.getDecimal(PatternElement.properties_decimal.minVerLength, i)),
                new XElement("s" + i + "MinVerOffset", tmp.getDecimal(PatternElement.properties_decimal.minVerOffset, i)),
                new XElement("s" + i + "HorLengthInc", tmp.getDecimal(PatternElement.properties_decimal.horLengthInc, i)),
                new XElement("s" + i + "HorOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.horOffsetInc, i)),
                new XElement("s" + i + "VerLengthInc", tmp.getDecimal(PatternElement.properties_decimal.verLengthInc, i)),
                new XElement("s" + i + "VerOffsetInc", tmp.getDecimal(PatternElement.properties_decimal.verOffsetInc, i)),
                new XElement("s" + i + "HorLengthSteps", tmp.getInt(PatternElement.properties_i.horLengthSteps, i)),
                new XElement("s" + i + "HorOffsetSteps", tmp.getInt(PatternElement.properties_i.horOffsetSteps, i)),
                new XElement("s" + i + "VerLengthSteps", tmp.getInt(PatternElement.properties_i.verLengthSteps, i)),
                new XElement("s" + i + "VerOffsetSteps", tmp.getInt(PatternElement.properties_i.verOffsetSteps, i)),

                new XElement("s" + i + "MinHorLengthRef", tmp.getInt(PatternElement.properties_i.MinHLRef, i)),
                new XElement("s" + i + "MinHorLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHLSubShapeRef, i)),
                new XElement("s" + i + "HorLengthRefFinal", tmp.getInt(PatternElement.properties_i.HLRefFinal, i)),
                new XElement("s" + i + "HorLengthIncRef", tmp.getInt(PatternElement.properties_i.HLIncRef, i)),
                new XElement("s" + i + "HorLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HLIncSubShapeRef, i)),
                new XElement("s" + i + "HorLengthStepsRef", tmp.getInt(PatternElement.properties_i.HLStepsRef, i)),
                new XElement("s" + i + "HorLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HLStepsSubShapeRef, i)),
                new XElement("s" + i + "MinVerLengthRef", tmp.getInt(PatternElement.properties_i.MinVLRef, i)),
                new XElement("s" + i + "MinVerLengthSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVLSubShapeRef, i)),
                new XElement("s" + i + "VerLengthRefFinal", tmp.getInt(PatternElement.properties_i.VLRefFinal, i)),
                new XElement("s" + i + "VerLengthIncRef", tmp.getInt(PatternElement.properties_i.VLIncRef, i)),
                new XElement("s" + i + "VerLengthIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VLIncSubShapeRef, i)),
                new XElement("s" + i + "VerLengthStepsRef", tmp.getInt(PatternElement.properties_i.VLStepsRef, i)),
                new XElement("s" + i + "VerLengthStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VLStepsSubShapeRef, i)),

                new XElement("s" + i + "MinHorOffsetRef", tmp.getInt(PatternElement.properties_i.MinHORef, i)),
                new XElement("s" + i + "MinHorOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinHOSubShapeRef, i)),
                new XElement("s" + i + "HorOffsetRefFinal", tmp.getInt(PatternElement.properties_i.HORefFinal, i)),
                new XElement("s" + i + "HorOffsetIncRef", tmp.getInt(PatternElement.properties_i.HOIncRef, i)),
                new XElement("s" + i + "HorOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.HOIncSubShapeRef, i)),
                new XElement("s" + i + "HorOffsetStepsRef", tmp.getInt(PatternElement.properties_i.HOStepsRef, i)),
                new XElement("s" + i + "HorOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.HOStepsSubShapeRef, i)),
                new XElement("s" + i + "MinVerOffsetRef", tmp.getInt(PatternElement.properties_i.MinVORef, i)),
                new XElement("s" + i + "MinVerOffsetSubShapeRef", tmp.getInt(PatternElement.properties_i.MinVOSubShapeRef, i)),
                new XElement("s" + i + "VerOffsetRefFinal", tmp.getInt(PatternElement.properties_i.VORefFinal, i)),
                new XElement("s" + i + "VerOffsetIncRef", tmp.getInt(PatternElement.properties_i.VOIncRef, i)),
                new XElement("s" + i + "VerOffsetIncSubShapeRef", tmp.getInt(PatternElement.properties_i.VOIncSubShapeRef, i)),
                new XElement("s" + i + "VerOffsetStepsRef", tmp.getInt(PatternElement.properties_i.VOStepsRef, i)),
                new XElement("s" + i + "VerOffsetStepsSubShapeRef", tmp.getInt(PatternElement.properties_i.VOStepsSubShapeRef, i)),

                new XElement("s" + i + "TipRef", tmp.getInt(PatternElement.properties_i.tipRef, i)),
                new XElement("s" + i + "TipSubShapeRef", tmp.getInt(PatternElement.properties_i.tipSubShapeRef, i))

            );
        }

        xelement.Add(new XElement("s0Tip", tmp.getInt(PatternElement.properties_i.shape0Tip)));
        xelement.Add(new XElement("s1Tip", tmp.getInt(PatternElement.properties_i.shape1Tip)));
        xelement.Add(new XElement("s2Tip", tmp.getInt(PatternElement.properties_i.shape2Tip)));

    }

    private void pSaveQuiltSettings_element_tips(ref XElement xelement, ref PatternElement tmp)
    {
        xelement.Add(new XElement("MinHorTipLength", tmp.getDecimal(PatternElement.properties_decimal.minHorTipLength)));
        xelement.Add(new XElement("HorTipInc", tmp.getDecimal(PatternElement.properties_decimal.horTipLengthInc)));
        xelement.Add(new XElement("HorTipSteps", tmp.getInt(PatternElement.properties_i.HorTipSteps)));
        xelement.Add(new XElement("MinHorTipRef", tmp.getInt(PatternElement.properties_i.MinHTRef)));
        xelement.Add(new XElement("HorTipIncRef", tmp.getInt(PatternElement.properties_i.HTIncRef)));
        xelement.Add(new XElement("HorTipStepsRef", tmp.getInt(PatternElement.properties_i.HTStepsRef)));
        xelement.Add(new XElement("HorTipRefFinal", tmp.getInt(PatternElement.properties_i.HTRefFinal)));

        xelement.Add(new XElement("MinVerTipLength", tmp.getDecimal(PatternElement.properties_decimal.minVerTipLength)));
        xelement.Add(new XElement("VerTipInc", tmp.getDecimal(PatternElement.properties_decimal.verTipLengthInc)));
        xelement.Add(new XElement("VerTipSteps", tmp.getInt(PatternElement.properties_i.VerTipSteps)));
        xelement.Add(new XElement("MinVerTipRef", tmp.getInt(PatternElement.properties_i.MinVTRef)));
        xelement.Add(new XElement("VerTipIncRef", tmp.getInt(PatternElement.properties_i.VTIncRef)));
        xelement.Add(new XElement("VerTipStepsRef", tmp.getInt(PatternElement.properties_i.VTStepsRef)));
        xelement.Add(new XElement("VerTipRefFinal", tmp.getInt(PatternElement.properties_i.VTRefFinal)));
    }

    private void pSaveQuiltSettings_element_BBox(ref XElement xelement, ref PatternElement tmp)
    {
        xelement.Add(new XElement("boundingLeft", tmp.getDecimal(PatternElement.properties_decimal.boundingLeft)),
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
            new XElement("boundingTopSteps", tmp.getInt(PatternElement.properties_i.boundingTopSteps)));
    }

    private void pSaveQuiltSettings_element_Pos(ref XElement xelement, ref PatternElement tmp)
    {
        xelement.Add(new XElement("posIndex", tmp.getInt(PatternElement.properties_i.posIndex)),

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
            new XElement("alignY", tmp.getInt(PatternElement.properties_i.alignY)));
    }
    
    private void pSaveQuiltSettings_element_array(ref XElement xelement, ref PatternElement tmp)
    {
            xelement.Add(
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
                new XElement("refArrayPivot", tmp.getInt(PatternElement.properties_i.refArrayPivot))
            );
    }

    private void pSaveQuiltSettings_element(ref XDocument doc, ref Stitcher quilt, int i)
    {
            PatternElement tmp = quilt.getPatternElement(patternIndex: 0, i);
            XElement xelement = new("layer" + (i + 1),
                new XElement("name", tmp.getString(PatternElement.properties_s.name)),
                new XElement("shapeIndex", tmp.getInt(PatternElement.properties_i.shapeIndex)),
                new XElement("subShapeIndex", tmp.getInt(PatternElement.properties_i.subShapeIndex)),
                new XElement("linkedElementIndex", tmp.getInt(PatternElement.properties_i.linkedElementIndex))
                );

            pSaveQuiltSettings_element_subshapes(ref xelement, ref tmp);

            pSaveQuiltSettings_element_tips(ref xelement, ref tmp);

            pSaveQuiltSettings_element_BBox(ref xelement, ref tmp);

            pSaveQuiltSettings_element_Pos(ref xelement, ref tmp);

            pSaveQuiltSettings_element_array(ref xelement, ref tmp);

            xelement.Add(
                new XElement("nonOrthoGeo", stringFromFileData(tmp.nonOrthoGeometry))
            );
            doc.Root!.Add(xelement);        
    }
    
    private bool pSaveQuiltSettings(string filename, ref Stitcher quilt)
    {
        XDocument doc = new(new XElement(CentralProperties.productName));
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
            pSaveQuiltSettings_element(ref doc, ref quilt, i);

            if (i + 1 % progressInterval == 0)
            {
                updateUIProgress?.Invoke(i + 1, maxProgress);
            }
        }

        doc.Root!.Add(new XElement("quilt",
            new XElement("padding", quilt.getPadding()),
            new XElement("showDrawn", quilt.getShowInput())
        ));

        double[] camParameters = viewportSave?.Invoke();
        doc.Root.Add(new XElement("viewport",
            new XElement("displayZoomFactor", camParameters != null ? camParameters[2] : 1),
            new XElement("viewportX", camParameters != null ? camParameters[0] : 0),
            new XElement("viewportY", camParameters != null ? camParameters[1] : 0)
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

    public void loadQuilt(string filename, QuiltContext quiltContext)
    {
        pLoadQuiltSettings(filename, quiltContext:quiltContext);
    }

    private static void pLoadSubShapeReferenceDimensionSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref)
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

    private static void pLoadReferenceTipSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref)
    {
        try
        {
            readSettings.setInt(PatternElement.properties_i.MinHTRef,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("MinHorTipRef")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.MinHTRef);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.HTIncRef,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("HorTipIncRef")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.HTIncRef);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.HTStepsRef,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("HorTipStepsRef")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.HTStepsRef);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.MinVTRef,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("MinVerTipRef")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.MinVTRef);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.VTIncRef,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("VerTipIncRef")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.VTIncRef);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.VTStepsRef,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("VerTipStepsRef")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.VTStepsRef);
        }
        
        try
        {
            readSettings.setInt(PatternElement.properties_i.HTRefFinal,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("HorTipRefFinal")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.HTRefFinal);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.VTRefFinal,
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("VerTipRefFinal")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.VTRefFinal);
        }


        for (int i = 0; i < 3; i++)
        {
            try
            {
                readSettings.setInt(PatternElement.properties_i.tipRef,
                    Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("s" + i + "TipRef")
                        .First().Value), i);
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.tipRef, i);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.tipSubShapeRef,
                    Convert.ToInt32(simulationFromFile.Descendants(layerref)
                        .Descendants("s" + i +"TipSubShapeRef").First().Value), i);
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.tipSubShapeRef, i);
            }
        }
    }

    private static void pLoadTipSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref)
    {
        try
        {
            readSettings.setInt(PatternElement.properties_i.shape0Tip,
                Convert.ToInt32(simulationFromFile.Descendants(layerref)
                    .Descendants("s0Tip").First().Value), 0);
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.shape0Tip, 0);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.shape1Tip,
                Convert.ToInt32(simulationFromFile.Descendants(layerref)
                    .Descendants("s1Tip").First().Value), 1);
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.shape1Tip, 1);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.shape2Tip,
                Convert.ToInt32(simulationFromFile.Descendants(layerref)
                    .Descendants("s2Tip").First().Value), 2);
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.shape2Tip, 2);
        }

        for (int i = 0; i < 3; i++)
        {
            try
            {
                readSettings.setInt(PatternElement.properties_i.tipRef,
                    Convert.ToInt32(simulationFromFile.Descendants(layerref)
                        .Descendants("s" + i + "TipRef").First().Value), i);
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.tipRef, i);
            }

            try
            {
                readSettings.setInt(PatternElement.properties_i.tipSubShapeRef,
                    Convert.ToInt32(simulationFromFile.Descendants(layerref)
                        .Descendants("s" + i + "TipSubShapeRef").First().Value), i);
            }
            catch (Exception)
            {
                readSettings.defaultInt(PatternElement.properties_i.tipSubShapeRef, i);
            }
        }
        
        pLoadTipSettings_Hor(ref readSettings, ref simulationFromFile, layerref);
        pLoadTipSettings_Ver(ref readSettings, ref simulationFromFile, layerref);
    }
    private static void pLoadTipSettings_Hor(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref)
    {
        try

        {
            readSettings.setDecimal(PatternElement.properties_decimal.minHorTipLength,
                Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("MinHorTipLength")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultDecimal(PatternElement.properties_decimal.minHorTipLength);
        }

        try
        {
            readSettings.setDecimal(PatternElement.properties_decimal.horTipLengthInc,
                Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("HorTipInc")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultDecimal(PatternElement.properties_decimal.horTipLengthInc);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.HorTipSteps,
                Convert.ToInt32(simulationFromFile.Descendants(layerref)
                    .Descendants("HorTipSteps").First().Value), 2);
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.HorTipSteps, 0);
        }

        try
        {
            readSettings.setInt(PatternElement.properties_i.HTRefFinal,
                Convert.ToInt32(simulationFromFile.Descendants(layerref)
                    .Descendants("HorTipRefFinal").First().Value), 0);
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.HTRefFinal, 0);
        }
    }

    private static void pLoadTipSettings_Ver(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref)
    {
        try
        {
            readSettings.setDecimal(PatternElement.properties_decimal.minVerTipLength,
                Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("MinVerTipLength")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultDecimal(PatternElement.properties_decimal.minVerTipLength);
        }

        try
        {
            readSettings.setDecimal(PatternElement.properties_decimal.verTipLengthInc,
                Convert.ToDecimal(simulationFromFile.Descendants(layerref).Descendants("VerTipInc")
                    .First().Value));
        }
        catch (Exception)
        {
            readSettings.defaultDecimal(PatternElement.properties_decimal.verTipLengthInc);
        }
        
        try
        {
            readSettings.setInt(PatternElement.properties_i.VerTipSteps,
                Convert.ToInt32(simulationFromFile.Descendants(layerref)
                    .Descendants("VerTipSteps").First().Value), 0);
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.VerTipSteps, 0);
        }
        
        try
        {
            readSettings.setInt(PatternElement.properties_i.VTRefFinal,
                Convert.ToInt32(simulationFromFile.Descendants(layerref)
                    .Descendants("VerTipRefFinal").First().Value), 0);
        }
        catch (Exception)
        {
            readSettings.defaultInt(PatternElement.properties_i.VTRefFinal, 0);
        }
    }

    private static void pLoadSubShapeReferenceOffsetSettings(ref PatternElement readSettings,
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

    private static void pLoadSubShapeDimensionSettings(ref PatternElement readSettings, ref XElement simulationFromFile,
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

    private static void pLoadSubShapeOffsetSettings(ref PatternElement readSettings, ref XElement simulationFromFile,
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

    private static void pLoadBoundingBoxSettings(ref PatternElement readSettings, ref XElement simulationFromFile,
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

    private static void pLoadSubShapeSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref, QuiltContext quiltContext)
    {
        readSettings.defaultInt(PatternElement.properties_i.shape0Tip);
        readSettings.defaultInt(PatternElement.properties_i.shape1Tip);
        readSettings.defaultInt(PatternElement.properties_i.shape2Tip);

        pLoadSubShapeDimensionSettings(ref readSettings, ref simulationFromFile, layerref);
        pLoadSubShapeOffsetSettings(ref readSettings, ref simulationFromFile, layerref);

        pLoadSubShapeReferenceDimensionSettings(ref readSettings, ref simulationFromFile, layerref);
        pLoadSubShapeReferenceOffsetSettings(ref readSettings, ref simulationFromFile, layerref);
        
        pLoadTipSettings(ref readSettings, ref simulationFromFile, layerref);
        pLoadReferenceTipSettings(ref readSettings, ref simulationFromFile, layerref);
        
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
        catch
        {
            // ignored
        }
    }

    private static void pLoadPositionRotationSettings(ref PatternElement readSettings, ref XElement simulationFromFile, string layerref, string[] tokenVersion)
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
            int rotRefIndex =
                Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("rotationRef").First().Value);
            readSettings.setInt(PatternElement.properties_i.rotationRef, rotRefIndex);
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
            int arrayRotRefIndex = Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("arrayRotationRef").First().Value);
            readSettings.setInt(PatternElement.properties_i.arrayRotationRef, arrayRotRefIndex);
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

    private string pLoadQuiltSettings(string filename, QuiltContext quiltContext)
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
        string[] tokenVersion = version.Split(new[] { '.' });

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
            
#if !QUILTSINGLETHREADED
        Parallel.For(0, elementCount, (layer)  =>
#else
            for (int layer = 0; layer < elementCount; layer++)
#endif
            {
                loadedElements[layer] = new();
                string layerref = "layer" + (layer + 1);

                try
                {
                    loadedElements[layer].setString(PatternElement.properties_s.name, simulationFromFile.Descendants(layerref).Descendants("name").First().Value);
                }
                catch (Exception)
                {
                    loadedElements[layer].setString(PatternElement.properties_s.name, layerref);
                }

                try
                {
                    loadedElements[layer].setInt(PatternElement.properties_i.shapeIndex, Convert.ToInt32(simulationFromFile.Descendants(layerref).Descendants("shapeIndex").First().Value));
                }
                catch (Exception)
                {
                    loadedElements[layer].defaultInt(PatternElement.properties_i.shapeIndex);
                }

                pLoadSubShapeSettings(ref loadedElements[layer], ref simulationFromFile, layerref, quiltContext);

                pLoadPositionRotationSettings(ref loadedElements[layer], ref simulationFromFile, layerref, tokenVersion);

#if !QUILTSINGLETHREADED
                Interlocked.Increment(ref progressCounter);
#else
                progressCounter++;      
#endif
                if (progressCounter % progressInterval == 0)
                {
                    updateUIProgress?.Invoke(progressCounter, maxProgress);
                }

            }
#if !QUILTSINGLETHREADED
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
            catch
            {
                // ignored
            }

            viewportLoad?.Invoke(new[] { x, y, zoom });
        }
        catch (Exception e)
        {
            ErrorReporter.showMessage_OK("Failed loading: " + e, "Error");
            error = true;
        }

        postLoadUI?.Invoke(filename);
        if (!error)
        {
            return returnString;
        }

        loadedElements = null;// new List<PatternElement>();
        padding = 0;
        showDrawn = 1;
        returnString = "";
        return returnString;
    }
}