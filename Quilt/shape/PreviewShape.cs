// tiled layout handling, Layout biasing/CDU.
using color;
using Error;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clipper2Lib;
using shapeEngine;

namespace Quilt;

public class PreviewShape
{
    public int elementIndex; // originating element.
    public int linkedElementIndex; // for tracking decomposed entries to allow for recombination later.
    private int layoutLayer; // coming from layout originated elements, to export back to same layer/datatype.
    private int layoutDatatype; // coming from layout originated elements, to export back to same layer/datatype.

    // Class for our preview shapes.
    private PathsD previewPoints; // list of polygons defining the shape(s) that will be drawn. In the complex case, we populate this from complexPoints.
    public List<int> sourceIndices;
    public PathsD getPoints()
    {
        return pGetPoints();
    }

    private PathsD pGetPoints()
    {
        return previewPoints;
    }

    public PathD getPoints(int index)
    {
        return pGetPoints(index);
    }

    private PathD pGetPoints(int index)
    {
        return previewPoints[index];
    }

    public void move(int poly, int pt, double x, double y)
    {
        pMove(poly, pt, x, y);
    }

    private void pMove(int poly, int pt, double x, double y)
    {
        previewPoints[poly][pt] = new (previewPoints[poly][pt].x + x, previewPoints[poly][pt].y + y);
    }

    public void move(double x, double y, int startPolyIndex = -1, int endPolyIndex = int.MaxValue, int startPtIndex = -1, int endPtIndex = int.MaxValue)
    {
        pMove(x, y, startPolyIndex, endPolyIndex, startPtIndex);
    }

    private void pMove(double x, double y, int startPolyIndex, int endPolyIndex, int startPtIndex)
    {
        int polyStart = Math.Max(startPolyIndex, 0);
        int polyEnd = Math.Min(endPolyIndex, previewPoints.Count);

        int ptStart = Math.Max(startPtIndex, 0);

#if !QUILTSINGLETHREADED
        Parallel.For(polyStart, polyEnd, poly =>
#else
            for (int poly = polyStart; poly < polyEnd; poly++)
#endif
            {
                int ptEnd = Math.Max(startPtIndex, previewPoints[poly].Count);

#if !QUILTSINGLETHREADED
                Parallel.For(ptStart, ptEnd, pt =>
#else
                for (int pt = ptStart; pt < ptEnd; pt++)
#endif
                    {
                        previewPoints[poly][pt] = new (previewPoints[poly][pt].x + x, previewPoints[poly][pt].y + y);
                    }
#if !QUILTSINGLETHREADED
                );
#endif
            }
#if !QUILTSINGLETHREADED
        );
#endif
    }

    public void addPoints(PathD poly, bool drawn, bool text = false)
    {
        pAddPoints(poly, drawn, text);
    }

    private void pAddPoints(PathD poly, bool drawn, bool text = false)
    {
        pAddPoints(poly);
        drawnPoly.Add(drawn);
        textEntity.Add(text);
    }

    public void removePoly(int index)
    {
        pRemovePoly(index);
    }

    private void pRemovePoly(int index)
    {
        previewPoints.RemoveAt(index);
        sourceIndices.RemoveAt(index);
        drawnPoly.RemoveAt(index);
        textEntity.RemoveAt(index);
    }
    public void addPoints(PathD poly)
    {
        pAddPoints(poly);
    }

    private void pAddPoints(PathD poly)
    {
        previewPoints.Add(new(poly));
        sourceIndices.Add(elementIndex);
    }

    public void setPoints(PathsD newPoints)
    {
        pSetPoints(newPoints);
    }

    private void pSetPoints(PathsD newPoints)
    {
        previewPoints = new(newPoints);
        for (int i = 0; i < newPoints.Count; i++)
        {
            sourceIndices.Add(elementIndex);
        }
    }

    public void clearPoints()
    {
        pClearPoints();
    }

    private void pClearPoints()
    {
        previewPoints.Clear();
        sourceIndices.Clear();
    }

    private List<bool> textEntity;

    public bool isText(int index)
    {
        return pIsText(index);
    }

    private bool pIsText(int index)
    {
        return textEntity[index];
    }

    private List<bool> drawnPoly; // to track drawn vs enabled polygons. Can then use for filtering elsewhere.

    public bool getDrawnPoly(int index)
    {
        return pGetDrawnPoly(index);
    }

    private bool pGetDrawnPoly(int index)
    {
        return drawnPoly[index];
    }

    private MyColor color;

    public MyColor getColor()
    {
        return pGetColor();
    }

    private MyColor pGetColor()
    {
        return color;
    }

    public void setColor(MyColor c)
    {
        pSetColor(c);
    }

    private void pSetColor(MyColor c)
    {
        color = new MyColor(c);
    }

    public PreviewShape()
    {
        pInit();
    }

    private void pInit()
    {
        // Stub to enable direct drive of preview data, primarily for the implant system.
        previewPoints = new ();
        sourceIndices = new List<int>();
        layoutLayer = -1;
        layoutDatatype = -1;
        drawnPoly = new List<bool>();
        textEntity = new List<bool>();
        color = MyColor.Black;
        linkedElementIndex = -1;
    }

    public PreviewShape(PreviewShape source)
    {
        pInit(source);
    }

    private void pInit(PreviewShape source)
    {
        previewPoints = new(source.previewPoints);
        sourceIndices = source.sourceIndices.ToList();
        layoutLayer = source.layoutLayer;
        layoutDatatype = source.layoutDatatype;
        drawnPoly = source.drawnPoly.ToList();
        textEntity = source.textEntity.ToList();
        color = new MyColor(source.color);
        elementIndex = source.elementIndex;
        linkedElementIndex = source.elementIndex;
    }

    public PreviewShape(Pattern pattern, int settingsIndex)
    {
        pInit(pattern, settingsIndex);
    }

    private PathD pGetPointArray(Pattern pattern, int index, bool doRotation= true)
    {
        PathD tempArray;

        PatternElement patternElement = pattern.getPatternElement(index);

        string shapeString = ((ShapeSettings.typeShapes_mode1)patternElement.getInt(PatternElement.properties_i.shapeIndex)).ToString();

        double x;
        double y;

        switch (shapeString)
        {
            case "complex":
                tempArray = Helper.initedPathD (patternElement.getInt(PatternElement.properties_i.externalGeoVertexCount));
                for (int i = 0; i < tempArray.Count; i++)
                {
                    x = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.xPos));
                    y = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.yPos));
                    x += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.externalGeoCoordX, i));
                    y += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.externalGeoCoordY, i));
                    tempArray[i] = new (x, y);
                }
                break;
            case "bounding":
                tempArray = Helper.initedPathD(15);
                // We store some values here, but the actual dimensions will be adjusted later when the extents are known.
                decimal left = patternElement.getDecimal(PatternElement.properties_decimal.boundingLeft);
                decimal right = patternElement.getDecimal(PatternElement.properties_decimal.boundingRight);
                decimal top = patternElement.getDecimal(PatternElement.properties_decimal.boundingTop);
                decimal bottom = patternElement.getDecimal(PatternElement.properties_decimal.boundingBottom);

                tempArray[0] = new ((double)left, (double)bottom);
                tempArray[1] = new ((double)left, (double)top);
                tempArray[2] = new ((double)right, (double)top);
                tempArray[3] = new ((double)right, (double)bottom);

#if !QUILTSINGLETHREADED
                Parallel.For(0, tempArray.Count, i =>
#else
                    for (int i = 4; i < tempArray.Length; i++)
#endif
                    {
                        tempArray[i] = new (tempArray[0]);
                    }
#if !QUILTSINGLETHREADED
                );
#endif
                break;

            default:
                x = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.xPos));
                y = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.yPos));

                decimal tipY = patternElement.getDecimal(ShapeSettings.properties_decimal.vTBias);
                decimal tipX = patternElement.getDecimal(ShapeSettings.properties_decimal.hTBias);

                tempArray = Helper.initedPathD (15);
                bool doTipLeft = patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.all;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.BL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.BLR;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.L;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.LR;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TLR;

                bool doTipRight = patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.all;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.BR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.BLR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.R;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.LR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TLR;

                bool doTipTop = patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.all;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.T;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TB;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TL;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TR;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TLR;
                
                bool doTipBottom = patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.all;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.B;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TB;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.BL;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.BR;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape0Tip) == (int)ShapeSettings.tipLocations.BLR;
                
                decimal bottom_leftX_1 = 0;
                decimal top_leftX_1 = 0;
                if (doTipLeft)
                {
                    top_leftX_1 -= tipX;
                    bottom_leftX_1 -= tipX;
                }

                decimal bottom_leftY_1 = 0;
                decimal bottom_rightY_1 = 0;
                if (doTipBottom)
                {
                    bottom_leftY_1 -= tipY;
                    bottom_rightY_1 -= tipY;
                }

                decimal top_leftY_1 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0);
                decimal top_rightY_1 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0);
                if (doTipTop)
                {
                    top_leftY_1 += tipY;
                    top_rightY_1 += tipY;
                }

                decimal bottom_rightX_1 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0);
                decimal top_rightX_1 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0);
                if (doTipRight)
                {
                    bottom_rightX_1 += tipX;
                    top_rightX_1 += tipX;
                }
                
                double _xOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 0));
                double _yOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 0));

                double xOffset = _xOffset;
                double yOffset = _yOffset;

                // Populate array.
                tempArray[0] = new ((double)bottom_leftX_1, (double)bottom_leftY_1);
                tempArray[1] = new ((double)top_leftX_1, (double)top_leftY_1);
                tempArray[2] = new ((double)top_rightX_1, (double)top_rightY_1);
                tempArray[3] = new ((double)bottom_rightX_1, (double)bottom_rightY_1);
                tempArray[4] = new (tempArray[0]);

                // Apply our deltas
#if !QUILTSINGLETHREADED
                var offset = xOffset;
                var offset1 = yOffset;
                Parallel.For(0, 5, i =>
#else
                    for (Int32 i = 0; i < 5; i++)
#endif
                    {
                        tempArray[i] = new (tempArray[i].x + offset + x, tempArray[i].y + offset1 + y);
                    }
#if !QUILTSINGLETHREADED
                );
#endif

                doTipLeft = patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.all;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.BL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.BLR;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.L;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.LR;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TLR;

                doTipRight = patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.all;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.BR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.BLR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.R;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.LR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TLR;

                doTipTop = patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.all;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.T;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TB;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TL;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TR;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TLR;
                
                doTipBottom = patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.all;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.B;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TB;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.BL;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.BR;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape1Tip) == (int)ShapeSettings.tipLocations.BLR;

                decimal bottom_leftX_2 = 0;
                decimal top_leftX_2 = 0;
                if (doTipLeft)
                {
                    bottom_leftX_2 -= tipX;
                    top_leftX_2 -= tipX;
                }
                decimal bottom_leftY_2 = 0;
                decimal bottom_rightY_2 = 0;
                if (doTipBottom)
                {
                    bottom_leftY_2 -= tipY;
                    bottom_rightY_2 -= tipY;
                }

                decimal top_rightX_2 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1);
                decimal bottom_rightX_2 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1);
                if (doTipRight)
                {
                    bottom_rightX_2 += tipX;
                    top_rightX_2 += tipX;
                }

                decimal top_leftY_2 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1);
                decimal top_rightY_2 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1);
                if (doTipTop)
                {
                    top_leftY_2 += tipY;
                    top_rightY_2 += tipY;
                }

                xOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 1)) + _xOffset;
                yOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) + _yOffset;

                // Populate array.
                tempArray[5 + 0] = new ((double)bottom_leftX_2, (double)bottom_leftY_2);
                tempArray[5 + 1] = new ((double)top_leftX_2, (double)top_leftY_2);
                tempArray[5 + 2] = new ((double)top_rightX_2, (double)top_rightY_2);
                tempArray[5 + 3] = new ((double)bottom_rightX_2, (double)bottom_rightY_2);
                tempArray[5 + 4] = new (tempArray[5 + 0]);

                // Apply our deltas
#if !QUILTSINGLETHREADED
                var xOffset1 = xOffset;
                var yOffset1 = yOffset;
                Parallel.For(0, 5, i =>
#else
                    for (Int32 i = 0; i < 5; i++)
#endif
                    {
                        tempArray[5 + i] = new (tempArray[5 + i].x + (xOffset1 + x), tempArray[5 + i].y + (yOffset1 + y));
                    }
#if !QUILTSINGLETHREADED
                );
#endif

                doTipLeft = patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.all;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.BL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.BLR;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.L;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.LR;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipLeft = doTipLeft || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TLR;

                doTipRight = patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.all;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.BR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.BLR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.R;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.LR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipRight = doTipRight || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TLR;

                doTipTop = patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.all;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.T;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TB;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TL;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TR;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipTop = doTipTop || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TLR;
                
                doTipBottom = patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.all;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.B;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TB;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.BL;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.BR;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TBL;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.TBR;
                doTipBottom = doTipBottom || patternElement.getInt(PatternElement.properties_i.shape2Tip) == (int)ShapeSettings.tipLocations.BLR;
                
                decimal bottom_leftX_3 = 0;
                decimal top_leftX_3 = 0;
                if (doTipLeft)
                {
                    bottom_leftX_3 -= tipX;
                    top_leftX_3 -= tipX;
                }

                decimal bottom_leftY_3 = 0;
                decimal bottom_rightY_3 = 0;
                if (doTipBottom)
                {
                    bottom_leftY_3 -= tipY;
                    bottom_rightY_3 -= tipY;
                }

                decimal top_rightX_3 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 2);
                decimal bottom_rightX_3 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 2);
                if (doTipRight)
                {
                    bottom_rightX_3 += tipX;
                    top_rightX_3 += tipX;
                }

                decimal top_leftY_3 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2);
                decimal top_rightY_3 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2);
                if (doTipTop)
                {
                    top_leftY_3 += tipY;
                    top_rightY_3 += tipY;
                }

                xOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 2)) + _xOffset;
                yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 2) + patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2)) + _yOffset;
                if (shapeString == "S")
                {
                    yOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0)); // offset our subshape to put it in the correct place in the UI.
                }

                // Populate array.
                tempArray[10 + 0] = new ((double)bottom_leftX_3, (double)bottom_leftY_3);
                tempArray[10 + 1] = new ((double)top_leftX_3, (double)top_leftY_3);
                tempArray[10 + 2] = new ((double)top_rightX_3, (double)top_rightY_3);
                tempArray[10 + 3] = new ((double)bottom_rightX_3, (double)bottom_rightY_3);
                tempArray[10 + 4] = new (tempArray[10 + 0]);

                // Apply our deltas
#if !QUILTSINGLETHREADED
                Parallel.For(0, 5, i =>
#else
                    for (Int32 i = 0; i < 5; i++)
#endif
                    {
                        tempArray[10 + i] = new (tempArray[10 + i].x + (xOffset + x), tempArray[10 + i].y + (yOffset + y));
                    }
#if !QUILTSINGLETHREADED
                );
#endif
                break;
        }

        PointD bb = new(double.NaN, double.NaN);
        
        PathD temp = pTransformed(tempArray, pattern, index, bb, doRotation: doRotation);
        
        // If we flipped in X xor Y, the 1st and 3rd subshapes got moved in the flip. We need to re-locate them.
        if ((patternElement.getInt(PatternElement.properties_i.flipH) == 1) ^
            (patternElement.getInt(PatternElement.properties_i.flipV) == 1))
        {
            //GeoLibPointF[] t1 = temp.Take(5).ToArray();
            //GeoLibPointF[] t2 = temp.Skip(5).Take(5).ToArray();
            //GeoLibPointF[] t3 = temp.Skip(10).Take(5).ToArray();
            temp.Reverse();
        }
        return temp;
    }

    // This deals with arrayed geometry....
    private PathsD pTransformed(PathsD source, Pattern pattern, int index, bool doRotation = true)
    {
        PatternElement patternElement = pattern.getPatternElement(index);

        double rotAngle = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.arrayRotation));
        // Self and 'World Origin'
        int rotRef = patternElement.getInt(PatternElement.properties_i.arrayRotationRef) - 1;

        /* Above, rotRef == 0 means the world reference. We decrement the value by 1 to compensate.

        However, the active layer isn't in our list of reference layers. This causes trouble now because we need to detect and handle this.
        Consider the active layer as '1', the list is then (world, 0, 2, 3, 4) as (0th, 1st, 2nd, 3rd, 4th members).
        
        Decrementing the index means we have :
        -1 => world
         0 => 0
         1 => 2
         2 => 3
         3 => 4

        To compensate, if the reduced layer index is equal to, or more than the active index, we should increase the value by 1 to sort the look-up out.
        */

        if (rotRef >= index)
        {
            rotRef++; // The reduction below caused our reference to land 
        }

        int rotRefUseArray = patternElement.getInt(PatternElement.properties_i.arrayRotRefUseArray);

        // Array flip not provided at this time.
        bool refPivot = patternElement.getInt(PatternElement.properties_i.refArrayPivot) == 1;
        PointD bb = GeoWrangler.midPoint(source);

        if (patternElement.getInt(PatternElement.properties_i.refArrayPivot) == 2)
        {
            bb = new(0.0, 0.0);
        }
        
        return new (source.Select(t => pTransformed(t, pattern, index, bb, rotAngle, rotRef, rotRefUseArray, flipH: false, flipV: false, alignX: false, alignY: false, refPivot, doRotation)));
    }

    // This deals with non-arrayed geometry
    private PathD pTransformed(PathD tempArray, Pattern pattern, int index, PointD bb, bool doRotation = true)
    {
        PatternElement patternElement = pattern.getPatternElement(index);

        double rotAngle = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.rotation));
        if (!doRotation)
        {
            rotAngle = 0.0f;
        }

        int rotRef = pattern.getRef(index, PatternElement.properties_i.rotationRef);

        int rotRefUseArray = patternElement.getInt(PatternElement.properties_i.rotRefUseArray);

        bool flipH = patternElement.getInt(PatternElement.properties_i.flipH) == 1;
        bool flipV = patternElement.getInt(PatternElement.properties_i.flipV) == 1;
        bool alignX = patternElement.getInt(PatternElement.properties_i.alignX) == 1;
        bool alignY = patternElement.getInt(PatternElement.properties_i.alignY) == 1;

        bool refPivot = patternElement.getInt(PatternElement.properties_i.refPivot) == 1;
        if (patternElement.getInt(PatternElement.properties_i.refPivot) == 2)
        {
            bb = new(0.0, 0.0);
        }
        
        return pTransformed(tempArray, pattern, index, bb, rotAngle, rotRef, rotRefUseArray, flipH, flipV, alignX, alignY, refPivot, doRotation);
    }

    // Note that this has the corrected rotRef (0 is the first pattern element, -1 is world)
    private PathD pTransformed(PathD tempArray, Pattern pattern, int index, PointD bb, double rotAngle, int rotRef, int rotRefUseArray, bool flipH, bool flipV, bool alignX, bool alignY, bool refPivot, bool doRotation)
    {
        PathD transformed = new (tempArray);
        
        // ReSharper disable once ConvertIfStatementToNullCoalescingAssignment
        if (double.IsNaN(bb.x) || double.IsNaN(bb.y))
        {
            bb = GeoWrangler.midPoint(tempArray);
        }

        PointD pivot = new(bb.x, bb.y);

        if (!doRotation)
        {
            rotAngle = 0.0f;
        }

        if (Math.Abs(rotAngle) > Constants.tolerance)
        {
            transformed = GeoWrangler.Rotate(pivot, tempArray, rotAngle);
        }

        if (rotRef >= 0)
        {
            bool rCyclical = pattern.cyclicalCheck(rotRef, PatternElement.properties_i.rotationRef);

            if (!rCyclical)
            {
                while (rotRef >= 0)
                {
                    // Is the reference an array; if it is, do we want to get the array rotation or something else.
                    bool refIsArray = pattern.getPatternElement(rotRef).isXArray() || pattern.getPatternElement(rotRef).isYArray();
                    refIsArray = refIsArray || pattern.getPatternElement(rotRef).getInt(PatternElement.properties_i.arrayRef) > 0;

                    PointD r_pivot = new(pivot.x, pivot.y);

                    bool done = false;

                    if (refIsArray)
                    {
                        if (rotRefUseArray == 1)
                        {
                            rotAngle = Convert.ToDouble(pattern.getPatternElement(rotRef).getDecimal(PatternElement.properties_decimal.arrayRotation));

                            // The bounds evaluation has transforms applied (e.g. rotation) if the below is true. This can be a problem.
                            bool boundsAfterRotation = pattern.getPatternElement(index).getInt(PatternElement.properties_i.refArrayBoundsAfterRotation) == 1;

                            if (refPivot)
                            {
                                switch (pattern.getPatternElement(rotRef).getInt(PatternElement.properties_i.refPivot))
                                {
                                    case 0:
                                        // We need to actually figure out our reference array midpoint now in order to use it.
                                        double[] bounds = pShapeBounds(pattern, rotRef, doRotation: boundsAfterRotation);
                                        double width = bounds[2] - bounds[0];
                                        double height = bounds[3] - bounds[1];
                                        double xSpace = Convert.ToDouble(pattern.getPatternElement(rotRef)
                                            .getDecimal(PatternElement.properties_decimal.arrayXSpace));
                                        double ySpace = Convert.ToDouble(pattern.getPatternElement(rotRef)
                                            .getDecimal(PatternElement.properties_decimal.arrayYSpace));
                                        int xCount = pattern.getPatternElement(rotRef)
                                            .getInt(PatternElement.properties_i.arrayXCount);
                                        int yCount = pattern.getPatternElement(rotRef)
                                            .getInt(PatternElement.properties_i.arrayYCount);

                                        double arrayWidth = xCount * width + (xCount - 1) * xSpace;
                                        double arrayHeight = yCount * height + (yCount - 1) * ySpace;

                                        r_pivot = new (bounds[0] + arrayWidth / 2.0f,
                                            bounds[1] + arrayHeight / 2.0f);
                                        break;
                                    case 1:
                                        // Get rotation pivot point from reference....
                                        // NEEDS REVIEW
                                        r_pivot = pattern.getPatternElement(rotRef).getMidPoint();
                                        break;
                                    case 2:
                                        r_pivot = new (0.0, 0.0);
                                        break;
                                }
                            }

                            done = true;
                        }
                    }

                    // However, we may want rotation relative to a non-array case.
                    if (!done)
                    {
                        if (refPivot)
                        {
                            bool boundsAfterRotation = pattern.getPatternElement(index).getInt(PatternElement.properties_i.refBoundsAfterRotation) == 1;
                            if (pattern.getPatternElement(rotRef).midPointSet())
                            {
                                r_pivot = new (pattern.getPatternElement(rotRef).getMidPoint());
                            }
                            else
                            {
                                r_pivot = new (GeoWrangler.midPoint(pGetPointArray(pattern, rotRef, doRotation: boundsAfterRotation)));
                                pattern.getPatternElement(rotRef).setMidPoint(r_pivot);
                            }
                            
                            // Override for the world origin case.... Still allow the above for the 'cached' midpoint evaluation.
                            if (pattern.getPatternElement(rotRef).getInt(PatternElement.properties_i.refPivot) == 2)
                            {
                                r_pivot = new(0, 0);
                            }
                        }

                        rotAngle = Convert.ToDouble(pattern.getPatternElement(rotRef).getDecimal(PatternElement.properties_decimal.rotation));
                    }

                    if (Math.Abs(rotAngle) > Constants.tolerance)
                    {
                        transformed = GeoWrangler.Rotate(r_pivot, transformed, rotAngle);
                    }
                    rotRef = pattern.getRef(rotRef, PatternElement.properties_i.rotationRef);
                }
            }
        }

        // Flip after rotation.
        if (flipH || flipV)
        {
            bb = GeoWrangler.midPoint(transformed);
            transformed = GeoWrangler.flip(flipH, flipV, alignX, alignY, bb, transformed);
        }
        
        return new(transformed);
    }

    private double[] pShapeBounds(Pattern pattern, int settingsIndex, bool doRotation = true)
    {
        PathD inputPoints = pGetPointArray(pattern, settingsIndex, doRotation: doRotation);
        // Apply any transformations to get the bounding box post-rotation, etc.
        inputPoints = pTransformed(new PathsD { inputPoints }, pattern, settingsIndex, doRotation: doRotation)[0];
        return pShapeBounds(inputPoints);
    }

    private static double[] pShapeBounds(PathD inputPoints)
    {
        double minX = inputPoints.Min(p => p.x);
        double maxX = inputPoints.Max(p => p.x);

        double minY = inputPoints.Min(p => p.y);
        double maxY = inputPoints.Max(p => p.y);

        return new [] { minX, minY, maxX, maxY };
    }

    private void pInit(Pattern pattern, int settingsIndex)
    {
        try
        {
            previewPoints = new ();
            sourceIndices = new List<int>();
            drawnPoly = new List<bool>();
            textEntity = new List<bool>();
            color = MyColor.Black; // overridden later.

            PatternElement patternElement = pattern.getPatternElement(settingsIndex);

            elementIndex = settingsIndex;
            linkedElementIndex = patternElement.getInt(PatternElement.properties_i.linkedElementIndex);

            int shapeType = patternElement.getInt(PatternElement.properties_i.shapeIndex);
            bool textShape = shapeType == (int)CentralProperties.shapeNames.text;
            bool layoutShape = shapeType == (int)CentralProperties.shapeNames.complex;

            PathD outputPoints;

            ComplexShape complexPoints;

            PathD inputPoints = pGetPointArray(pattern, settingsIndex);

            if (!layoutShape)
            {
                complexPoints = new ComplexShape(pattern.getPatternElements(), settingsIndex);
                outputPoints = complexPoints.getPoints();

                PointD bb = new(double.NaN, double.NaN);
                
                outputPoints = pTransformed(outputPoints, pattern, settingsIndex, bb);

                previewPoints.Add(new(inputPoints.Take(5)));
                drawnPoly.Add(true);
                previewPoints.Add(new(inputPoints.Skip(5).Take(5)));
                drawnPoly.Add(true);
                previewPoints.Add(new(inputPoints.Skip(10).Take(5)));
                drawnPoly.Add(true);

                previewPoints.Add(outputPoints);
                drawnPoly.Add(false);
            }
            else
            {
                ShapeLibrary shape = new(CentralProperties.shapeTable, shapeType, patternElement);
                inputPoints = GeoWrangler.close(inputPoints);

                previewPoints.Add(new(inputPoints));
                drawnPoly.Add(true);

                inputPoints = GeoWrangler.move(inputPoints, -patternElement.getDecimal(PatternElement.properties_decimal.xPos), -patternElement.getDecimal(PatternElement.properties_decimal.yPos));
                shape.setShape(shapeType, inputPoints);
                complexPoints = new ComplexShape(pattern.getPatternElements(), settingsIndex, shape);

                outputPoints = complexPoints.getPoints();
                previewPoints.Add(new (outputPoints));
                drawnPoly.Add(false);
            }
            int arrayRef = patternElement.getInt(PatternElement.properties_i.arrayRef) - 1; // due to 'self' reference, we need to offset the value by 1

            // Query shape for bounds.
            double[] bounds;

            // The bounds evaluation has transforms applied (e.g. rotation) if the below is true. This can be a problem.
            bool boundsAfterRotation;

            if (arrayRef >= 0)
            {
                boundsAfterRotation = patternElement.getInt(PatternElement.properties_i.refArrayBoundsAfterRotation) == 1;
                bounds = pShapeBounds(pattern, arrayRef, doRotation: boundsAfterRotation);
            }
            else
            {
                boundsAfterRotation = patternElement.getInt(PatternElement.properties_i.refBoundsAfterRotation) == 1;
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (!boundsAfterRotation)
                {
                    bounds = pShapeBounds(pGetPointArray(pattern, settingsIndex, doRotation: false));
                }
                else
                {
                    bounds = pShapeBounds(inputPoints);
                }
            }

            decimal width = Convert.ToDecimal(bounds[2] - bounds[0]);
            decimal height = Convert.ToDecimal(bounds[3] - bounds[1]);

            decimal xPitch = 0;
            decimal yPitch = 0;
            int xCount = 1;
            int yCount = 1;

            bool doArray = patternElement.isXArray() || patternElement.isYArray();
            if (arrayRef >= 0)
            {
                xPitch = width + pattern.getPatternElement(arrayRef).getDecimal(PatternElement.properties_decimal.arrayXSpace);
                yPitch = height + pattern.getPatternElement(arrayRef).getDecimal(PatternElement.properties_decimal.arrayYSpace);
                xCount = pattern.getPatternElement(arrayRef).getInt(PatternElement.properties_i.arrayXCount);
                yCount = pattern.getPatternElement(arrayRef).getInt(PatternElement.properties_i.arrayYCount);
            }
            else if (doArray)
            {
                xPitch = width + patternElement.getDecimal(PatternElement.properties_decimal.arrayXSpace);
                yPitch = height + patternElement.getDecimal(PatternElement.properties_decimal.arrayYSpace);
                xCount = patternElement.getInt(PatternElement.properties_i.arrayXCount);
                yCount = patternElement.getInt(PatternElement.properties_i.arrayYCount);
            }

            previewPoints = GeoWrangler.makeArray(previewPoints, xCount, xPitch, yCount, yPitch);
            
            previewPoints = pTransformed(previewPoints, pattern, settingsIndex);

            List<bool> oDP = new();

            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    oDP.AddRange(drawnPoly);
                }
            }

            drawnPoly = new List<bool>(oDP);

            // Get our offsets configured.
            PointD offset = shapeOffsets.doOffsets(0, patternElement);
            double xOffset = offset.x;
            double yOffset = offset.y;
                
            sourceIndices.Clear();
            for (int poly = 0; poly < previewPoints.Count; poly++)
            {
                textEntity.Add(textShape); // To track for output to layout.
                sourceIndices.Add(settingsIndex);
                int pCount = previewPoints[poly].Count;
                int poly1 = poly;
#if !QUILTSINGLETHREADED
                Parallel.For(0, pCount, point =>
#else
                    for (Int32 point = 0; point < pCount; point++)
#endif
                    {
                        double px = previewPoints[poly1][point].x + xOffset;
                        double py = previewPoints[poly1][point].y - yOffset;

                        previewPoints[poly1][point] = new (px, py);
                    }
#if !QUILTSINGLETHREADED
                );
#endif
                if (Math.Abs(previewPoints[poly][0].x - previewPoints[poly][previewPoints[poly].Count - 1].x) > Constants.tolerance ||
                    Math.Abs(previewPoints[poly][0].y - previewPoints[poly][previewPoints[poly].Count - 1].y) > Constants.tolerance)
                {
                    ErrorReporter.showMessage_OK("Start and end not the same - previewShape", "Oops");
                }
            }
        }
        catch
        {
            // ignored
        }
    }
}