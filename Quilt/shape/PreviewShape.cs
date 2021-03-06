﻿// tiled layout handling, Layout biasing/CDU.
using color;
using Error;
using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quilt
{
    public class PreviewShape
    {
        public int elementIndex; // originating element.
        public int linkedElementIndex; // for tracking decomposed entries to allow for recombination later.
        private int layoutLayer; // coming from layout originated elements, to export back to same layer/datatype.
        private int layoutDatatype; // coming from layout originated elements, to export back to same layer/datatype.

        // Class for our preview shapes.
        List<GeoLibPointF[]> previewPoints; // list of polygons defining the shape(s) that will be drawn. In the complex case, we populate this from complexPoints.
        public List<Int32> sourceIndices;
        public List<GeoLibPointF[]> getPoints()
        {
            return pGetPoints();
        }

        List<GeoLibPointF[]> pGetPoints()
        {
            return previewPoints;
        }

        public GeoLibPointF[] getPoints(int index)
        {
            return pGetPoints(index);
        }

        GeoLibPointF[] pGetPoints(int index)
        {
            return previewPoints[index];
        }

        public void move(int poly, int pt, double x, double y)
        {
            pMove(poly, pt, x, y);
        }

        void pMove(int poly, int pt, double x, double y)
        {
            previewPoints[poly][pt].X += x;
            previewPoints[poly][pt].Y += y;
        }

        public void move(double x, double y, int startPolyIndex = -1, int endPolyIndex = Int32.MaxValue, int startPtIndex = -1, int endPtIndex = Int32.MaxValue)
        {
            pMove(x, y, startPolyIndex, endPolyIndex, startPtIndex);
        }

        void pMove(double x, double y, int startPolyIndex, int endPolyIndex, int startPtIndex)
        {
            int polyStart = Math.Max(startPolyIndex, 0);
            int polyEnd = Math.Min(endPolyIndex, previewPoints.Count);

            int ptStart = Math.Max(startPtIndex, 0);

#if QUILTTHREADED
            Parallel.For(polyStart, polyEnd, (poly) =>
#else
            for (int poly = polyStart; poly < polyEnd; poly++)
#endif
            {
                int ptEnd = Math.Max(startPtIndex, previewPoints[poly].Count());

#if QUILTTHREADED
                Parallel.For(ptStart, ptEnd, (pt) =>
#else
                for (int pt = ptStart; pt < ptEnd; pt++)
#endif
                {
                    previewPoints[poly][pt].X += x;
                    previewPoints[poly][pt].Y += y;
                }
#if QUILTTHREADED
                );
#endif
            }
#if QUILTTHREADED
            );
#endif
        }

        public void addPoints(GeoLibPointF[] poly, bool drawn, bool text = false)
        {
            pAddPoints(poly, drawn, text);
        }

        void pAddPoints(GeoLibPointF[] poly, bool drawn, bool text = false)
        {
            pAddPoints(poly);
            drawnPoly.Add(drawn);
            textEntity.Add(text);
        }

        public void removePoly(int index)
        {
            pRemovePoly(index);
        }

        void pRemovePoly(int index)
        {
            previewPoints.RemoveAt(index);
            sourceIndices.RemoveAt(index);
            drawnPoly.RemoveAt(index);
            textEntity.RemoveAt(index);
        }
        public void addPoints(GeoLibPointF[] poly)
        {
            pAddPoints(poly);
        }

        void pAddPoints(GeoLibPointF[] poly)
        {
            previewPoints.Add(poly.ToArray());
            sourceIndices.Add(elementIndex);
        }

        public void setPoints(List<GeoLibPointF[]> newPoints)
        {
            pSetPoints(newPoints);
        }

        void pSetPoints(List<GeoLibPointF[]> newPoints)
        {
            previewPoints = newPoints.ToList();
            for (int i = 0; i < newPoints.Count; i++)
            {
                sourceIndices.Add(elementIndex);
            }
        }

        public void clearPoints()
        {
            pClearPoints();
        }

        void pClearPoints()
        {
            previewPoints.Clear();
            sourceIndices.Clear();
        }

        List<bool> textEntity;

        public bool isText(int index)
        {
            return pIsText(index);
        }

        bool pIsText(int index)
        {
            return textEntity[index];
        }
        List<bool> drawnPoly; // to track drawn vs enabled polygons. Can then use for filtering elsewhere.

        public bool getDrawnPoly(int index)
        {
            return pGetDrawnPoly(index);
        }

        bool pGetDrawnPoly(int index)
        {
            return drawnPoly[index];
        }

        MyColor color;

        public MyColor getColor()
        {
            return pGetColor();
        }

        MyColor pGetColor()
        {
            return color;
        }

        public void setColor(MyColor c)
        {
            pSetColor(c);
        }

        void pSetColor(MyColor c)
        {
            color = new MyColor(c);
        }

        double xOffset;
        double yOffset;

        void pRectangle_offset(PatternElement patternElement)
        {
            string posInSubShapeString = ((CommonVars.subShapeLocations)patternElement.getInt(PatternElement.properties_i.posIndex)).ToString();
            double tmp_xOffset = 0;
            double tmp_yOffset = 0;

            if ((posInSubShapeString == "TL") ||
                (posInSubShapeString == "TR") ||
                (posInSubShapeString == "TS") ||
                (posInSubShapeString == "RS") ||
                (posInSubShapeString == "LS") ||
                (posInSubShapeString == "C"))
            {
                // Vertical offset needed to put reference corner at world center
                tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));

                // Half the value for a vertical centering requirement
                if ((posInSubShapeString == "RS") ||
                    (posInSubShapeString == "LS") ||
                    (posInSubShapeString == "C"))
                {
                    tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                }
            }
            yOffset -= tmp_yOffset;

            if ((posInSubShapeString == "TR") ||
                (posInSubShapeString == "BR") ||
                (posInSubShapeString == "TS") ||
                (posInSubShapeString == "RS") ||
                (posInSubShapeString == "BS") ||
                (posInSubShapeString == "C"))
            {
                tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));

                // Half the value for horizontal centering conditions
                if ((posInSubShapeString == "TS") ||
                    (posInSubShapeString == "BS") ||
                    (posInSubShapeString == "C"))
                {
                    tmp_xOffset = Convert.ToDouble(tmp_xOffset / 2);
                }
            }
            xOffset += tmp_xOffset;
        }

        void pLShape_offset(PatternElement patternElement)
        {
            string posInSubShapeString = ((CommonVars.subShapeLocations)patternElement.getInt(PatternElement.properties_i.posIndex)).ToString();
            double tmp_xOffset = 0;
            double tmp_yOffset = 0;

            if ((posInSubShapeString == "TL") ||
                (posInSubShapeString == "TR") ||
                (posInSubShapeString == "TS") ||
                (posInSubShapeString == "RS") ||
                (posInSubShapeString == "LS") ||
                (posInSubShapeString == "C"))
            {
                // Vertical offset needed to put reference corner at world center
                if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                {
                    tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));
                }
                else
                {
                    tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1));
                }

                // Half the value for a vertical centering requirement
                if ((posInSubShapeString == "RS") ||
                    (posInSubShapeString == "LS") ||
                    (posInSubShapeString == "C"))
                {
                    tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                }
            }
            yOffset -= tmp_yOffset;

            if ((patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 1) && ((posInSubShapeString == "LS") || (posInSubShapeString == "BL") || (posInSubShapeString == "TL")))
            {
                tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0)); // essentially the same in X as the RS for subshape 1.
            }
            else
            {
                if ((posInSubShapeString == "TR") ||
                    (posInSubShapeString == "BR") ||
                    (posInSubShapeString == "TS") ||
                    (posInSubShapeString == "RS") ||
                    (posInSubShapeString == "BS") ||
                    (posInSubShapeString == "C"))
                {
                    if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));
                    }
                    else
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1));
                    }

                    // Half the value for horizontal centering conditions
                    if ((posInSubShapeString == "TS") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                        {
                            tmp_xOffset = Convert.ToDouble(tmp_xOffset / 2);
                        }
                        else
                        {
                            tmp_xOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
                        }
                    }
                }
            }

            xOffset += tmp_xOffset;
        }

        void pTShape_offset(PatternElement patternElement)
        {
            string posInSubShapeString = ((CommonVars.subShapeLocations)patternElement.getInt(PatternElement.properties_i.posIndex)).ToString();
            double tmp_xOffset = 0;
            double tmp_yOffset = 0;

            if ((patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 1) && ((posInSubShapeString == "BR") || (posInSubShapeString == "BL") || (posInSubShapeString == "BS")))
            {
                tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 1));
            }
            else
            {
                if ((posInSubShapeString == "TL") ||
                (posInSubShapeString == "TR") ||
                (posInSubShapeString == "TS") ||
                (posInSubShapeString == "RS") ||
                (posInSubShapeString == "LS") ||
                (posInSubShapeString == "C"))
                {
                    if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                    {
                        tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));
                        // Half the value for a vertical centering requirement
                        if ((posInSubShapeString == "RS") ||
                            (posInSubShapeString == "LS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                        }
                    }
                    else
                    {
                        tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1));
                        // Half the value for a vertical centering requirement
                        if ((posInSubShapeString == "RS") ||
                            (posInSubShapeString == "LS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                        }
                        tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 1));
                    }

                }
            }
            yOffset -= tmp_yOffset;

            if ((patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 1) && ((posInSubShapeString == "LS") || (posInSubShapeString == "BL") || (posInSubShapeString == "TL")))
            {
                tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0)); // essentially the same in X as the RS for subshape 1.
            }
            else
            {
                if ((posInSubShapeString == "TR") ||
                    (posInSubShapeString == "BR") ||
                    (posInSubShapeString == "TS") ||
                    (posInSubShapeString == "RS") ||
                    (posInSubShapeString == "BS") ||
                    (posInSubShapeString == "C"))
                {
                    if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));
                    }
                    else
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1));
                    }

                    // Half the value for horizontal centering conditions
                    if ((posInSubShapeString == "TS") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                        {
                            tmp_xOffset = Convert.ToDouble(tmp_xOffset / 2);
                        }
                        else
                        {
                            tmp_xOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
                        }
                    }
                }
            }

            xOffset += tmp_xOffset;
        }

        void pXShape_offset(PatternElement patternElement)
        {
            string posInSubShapeString = ((CommonVars.subShapeLocations)patternElement.getInt(PatternElement.properties_i.posIndex)).ToString();
            double tmp_xOffset = 0;
            double tmp_yOffset = 0;

            if ((patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 1) && ((posInSubShapeString == "BR") || (posInSubShapeString == "BL") || (posInSubShapeString == "BS")))
            {
                tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 1));
            }
            else
            {
                if ((posInSubShapeString == "TL") ||
                    (posInSubShapeString == "TR") ||
                    (posInSubShapeString == "TS") ||
                    (posInSubShapeString == "RS") ||
                    (posInSubShapeString == "LS") ||
                    (posInSubShapeString == "C"))
                {
                    // Vertical offset needed to put reference corner at world center
                    if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                    {
                        tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));
                        // Half the value for a vertical centering requirement
                        if ((posInSubShapeString == "RS") ||
                            (posInSubShapeString == "LS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                        }
                    }
                    else
                    {
                        tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1));
                        // Half the value for a vertical centering requirement
                        if ((posInSubShapeString == "RS") ||
                            (posInSubShapeString == "LS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                        }
                        tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 1));
                    }

                }
            }
            yOffset -= tmp_yOffset;

            if ((patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 1) && ((posInSubShapeString == "LS") || (posInSubShapeString == "BL") || (posInSubShapeString == "TL")))
            {
                tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 1));
            }
            else
            {
                if ((posInSubShapeString == "TR") ||
                    (posInSubShapeString == "BR") ||
                    (posInSubShapeString == "TS") ||
                    (posInSubShapeString == "RS") ||
                    (posInSubShapeString == "BS") ||
                    (posInSubShapeString == "C"))
                {
                    if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));
                    }
                    else
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1));
                    }

                    // Half the value for horizontal centering conditions
                    if ((posInSubShapeString == "TS") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
                        {
                            tmp_xOffset = Convert.ToDouble(tmp_xOffset / 2);
                        }
                        else
                        {
                            tmp_xOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
                        }
                    }

                    if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 1)
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 1));
                    }
                }
            }

            xOffset += tmp_xOffset;
        }

        void pUShape_offset(PatternElement patternElement)
        {
            string posInSubShapeString = ((CommonVars.subShapeLocations)patternElement.getInt(PatternElement.properties_i.posIndex)).ToString();
            double tmp_xOffset = 0;
            double tmp_yOffset = 0;

            if (patternElement.getInt(PatternElement.properties_i.subShapeIndex) == 0)
            {
                if ((posInSubShapeString == "TL") ||
                    (posInSubShapeString == "TR") ||
                    (posInSubShapeString == "TS") ||
                    (posInSubShapeString == "RS") ||
                    (posInSubShapeString == "LS") ||
                    (posInSubShapeString == "C"))
                {
                    tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));

                    // Half the value for a vertical centering requirement
                    if ((posInSubShapeString == "RS") ||
                        (posInSubShapeString == "LS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                    }
                }
                yOffset -= tmp_yOffset;

                if ((posInSubShapeString == "TR") ||
                    (posInSubShapeString == "BR") ||
                    (posInSubShapeString == "TS") ||
                    (posInSubShapeString == "RS") ||
                    (posInSubShapeString == "BS") ||
                    (posInSubShapeString == "C"))
                {
                    tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));

                    // Half the value for horizontal centering conditions
                    if ((posInSubShapeString == "TS") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_xOffset = Convert.ToDouble(tmp_xOffset / 2);
                    }
                }
            }
            else
            {
                // Subshape 2 is always docked against top edge of subshape 1 in U.
                if ((posInSubShapeString == "TL") ||
                    (posInSubShapeString == "TR") ||
                    (posInSubShapeString == "TS") ||
                    (posInSubShapeString == "RS") ||
                    (posInSubShapeString == "LS") ||
                    (posInSubShapeString == "BL") ||
                    (posInSubShapeString == "BR") ||
                    (posInSubShapeString == "BS") ||
                    (posInSubShapeString == "C"))
                {
                    tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));

                    // Half the value for a vertical centering requirement
                    if ((posInSubShapeString == "RS") ||
                        (posInSubShapeString == "LS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_yOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1) / 2);
                    }

                    // Subtract the value for a subshape 2 bottom edge requirement
                    if ((posInSubShapeString == "BL") ||
                        (posInSubShapeString == "BR") ||
                        (posInSubShapeString == "BS"))
                    {
                        tmp_yOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1));
                    }
                }
                yOffset -= tmp_yOffset;

                // Subshape 2 is always H-centered in U. Makes it easy.
                tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0) / 2);

                if ((posInSubShapeString == "TR") ||
                    (posInSubShapeString == "BR") ||
                    (posInSubShapeString == "RS"))
                {
                    tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
                }

                if ((posInSubShapeString == "TL") ||
                    (posInSubShapeString == "BL") ||
                    (posInSubShapeString == "LS"))
                {
                    tmp_xOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
                }
            }
            xOffset += tmp_xOffset;
        }

        void pSShape_offset(PatternElement patternElement)
        {
            string posInSubShapeString = ((CommonVars.subShapeLocations)patternElement.getInt(PatternElement.properties_i.posIndex)).ToString();
            double tmp_xOffset = 0;
            double tmp_yOffset = 0;

            switch (patternElement.getInt(PatternElement.properties_i.subShapeIndex))
            {
                case 0:
                    if ((posInSubShapeString == "TL") ||
                        (posInSubShapeString == "TR") ||
                        (posInSubShapeString == "TS") ||
                        (posInSubShapeString == "RS") ||
                        (posInSubShapeString == "LS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));

                        // Half the value for a vertical centering requirement
                        if ((posInSubShapeString == "RS") ||
                            (posInSubShapeString == "LS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_yOffset = Convert.ToDouble(tmp_yOffset / 2);
                        }
                    }

                    if ((posInSubShapeString == "TR") ||
                        (posInSubShapeString == "BR") ||
                        (posInSubShapeString == "TS") ||
                        (posInSubShapeString == "RS") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));

                        // Half the value for horizontal centering conditions
                        if ((posInSubShapeString == "TS") ||
                            (posInSubShapeString == "BS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_xOffset = Convert.ToDouble(tmp_xOffset / 2);
                        }
                    }
                    break;

                case 1:
                    // Subshape 2 is always vertically offset relative to bottom edge of subshape 1 in S.
                    if ((posInSubShapeString == "TL") ||
                        (posInSubShapeString == "TR") ||
                        (posInSubShapeString == "TS") ||
                        (posInSubShapeString == "RS") ||
                        (posInSubShapeString == "LS") ||
                        (posInSubShapeString == "BL") ||
                        (posInSubShapeString == "BR") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 1));

                        // Half the value for a vertical centering requirement
                        if ((posInSubShapeString == "RS") ||
                            (posInSubShapeString == "LS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1) / 2);
                        }

                        // Subtract the value for a subshape 2 bottom edge requirement
                        if ((posInSubShapeString == "TL") ||
                            (posInSubShapeString == "TR") ||
                            (posInSubShapeString == "TS"))
                        {
                            tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1));
                        }
                    }

                    // Subshape 2 is always pinned to left edge in S. Makes it easy.

                    if ((posInSubShapeString == "TR") ||
                        (posInSubShapeString == "BR") ||
                        (posInSubShapeString == "RS") ||
                        (posInSubShapeString == "TS") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1));
                        if ((posInSubShapeString == "TS") ||
                            (posInSubShapeString == "C") ||
                            (posInSubShapeString == "BS"))
                        {
                            tmp_xOffset /= 2;
                        }
                    }

                    break;

                case 2:
                    tmp_yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0));
                    // Subshape 3 is always offset relative to top edge of subshape 1 in S.
                    if ((posInSubShapeString == "TL") ||
                        (posInSubShapeString == "TR") ||
                        (posInSubShapeString == "TS") ||
                        (posInSubShapeString == "RS") ||
                        (posInSubShapeString == "LS") ||
                        (posInSubShapeString == "BL") ||
                        (posInSubShapeString == "BR") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_yOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 2));

                        // Half the value for a vertical centering requirement
                        if ((posInSubShapeString == "RS") ||
                            (posInSubShapeString == "LS") ||
                            (posInSubShapeString == "C"))
                        {
                            tmp_yOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2) / 2);
                        }

                        // Subtract the value for a subshape 2 bottom edge requirement
                        if ((posInSubShapeString == "BL") ||
                            (posInSubShapeString == "BR") ||
                            (posInSubShapeString == "BS"))
                        {
                            tmp_yOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2));
                        }
                    }

                    // Subshape 3 is always pinned to right edge in S. Makes it easy.
                    tmp_xOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0));

                    if ((posInSubShapeString == "TL") ||
                        (posInSubShapeString == "BL") ||
                        (posInSubShapeString == "LS"))
                    {
                        tmp_xOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 2));
                    }

                    if ((posInSubShapeString == "TS") ||
                        (posInSubShapeString == "BS") ||
                        (posInSubShapeString == "C"))
                    {
                        tmp_xOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horLength, 2) / 2);
                    }
                    break;
            }

            yOffset -= tmp_yOffset;
            xOffset += tmp_xOffset;
        }

        void pDoOffsets(PatternElement patternElement)
        {
            // Use our shape-specific offset calculation methods :
            xOffset = 0;
            yOffset = 0;

            switch (patternElement.getInt(PatternElement.properties_i.shapeIndex))
            {
                case (Int32)CentralProperties.typeShapes.rectangle:
                case (Int32)CentralProperties.typeShapes.text:
                case (Int32)CentralProperties.typeShapes.bounding:
                    pRectangle_offset(patternElement);
                    break;
                case (Int32)CentralProperties.typeShapes.L:
                    pLShape_offset(patternElement);
                    break;
                case (Int32)CentralProperties.typeShapes.T:
                    pTShape_offset(patternElement);
                    break;
                case (Int32)CentralProperties.typeShapes.X:
                    pXShape_offset(patternElement);
                    break;
                case (Int32)CentralProperties.typeShapes.U:
                    pUShape_offset(patternElement);
                    break;
                case (Int32)CentralProperties.typeShapes.S:
                    pSShape_offset(patternElement);
                    break;
            }

            // Now for global offset.
            xOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.gHorOffset));
            yOffset -= Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.gVerOffset));
        }

        public PreviewShape()
        {
            pInit();
        }

        void pInit()
        {
            // Stub to enable direct drive of preview data, primarily for the implant system.
            previewPoints = new List<GeoLibPointF[]>();
            sourceIndices = new List<Int32>();
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

        void pInit(PreviewShape source)
        {
            previewPoints = source.previewPoints.ToList();
            sourceIndices = source.sourceIndices.ToList();
            layoutLayer = source.layoutLayer;
            layoutDatatype = source.layoutDatatype;
            drawnPoly = source.drawnPoly.ToList();
            textEntity = source.textEntity.ToList();
            color = new MyColor(source.color);
            elementIndex = source.elementIndex;
            linkedElementIndex = source.elementIndex;
        }

        public PreviewShape(Pattern pattern, Int32 settingsIndex)
        {
            xOffset = 0;
            yOffset = 0;
            pInit(pattern, settingsIndex);
        }

        GeoLibPointF[] pGetPointArray(Pattern pattern, int index, bool doRotation= true)
        {
            GeoLibPointF[] tempArray;

            PatternElement patternElement = pattern.getPatternElement(index);

            string shapeString = ((CentralProperties.typeShapes)patternElement.getInt(PatternElement.properties_i.shapeIndex)).ToString();

            double x;
            double y;

            switch (shapeString)
            {
                case "complex":
                    tempArray = new GeoLibPointF[patternElement.getInt(PatternElement.properties_i.externalGeoVertexCount)];
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        x = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.xPos));
                        y = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.yPos));
                        x += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.externalGeoCoordX, i));
                        y += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.externalGeoCoordY, i));
                        tempArray[i] = new GeoLibPointF(x, y);
                    }
                    break;
                case "bounding":
                    tempArray = new GeoLibPointF[15];
                    // We store some values here, but the actual dimensions will be adjusted later when the extents are known.
                    decimal left = patternElement.getDecimal(PatternElement.properties_decimal.boundingLeft);
                    decimal right = patternElement.getDecimal(PatternElement.properties_decimal.boundingRight);
                    decimal top = patternElement.getDecimal(PatternElement.properties_decimal.boundingTop);
                    decimal bottom = patternElement.getDecimal(PatternElement.properties_decimal.boundingBottom);

                    tempArray[0] = new GeoLibPointF((double)left, (double)bottom);
                    tempArray[1] = new GeoLibPointF((double)left, (double)top);
                    tempArray[2] = new GeoLibPointF((double)right, (double)top);
                    tempArray[3] = new GeoLibPointF((double)right, (double)bottom);

#if QUILTTHREADED
                Parallel.For(0, tempArray.Length, (i) =>
#else
                    for (int i = 4; i < tempArray.Length; i++)
#endif
                    {
                        tempArray[i] = new GeoLibPointF(tempArray[0]);
                    }
#if QUILTTHREADED
                );
#endif
                    break;

                default:
                    x = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.xPos));
                    y = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.yPos));
                    tempArray = new GeoLibPointF[15];
                    decimal bottom_leftX_1 = 0;
                    decimal bottom_leftY_1 = 0;
                    decimal top_leftX_1 = 0;
                    decimal top_leftY_1 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0);
                    decimal top_rightX_1 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0);
                    decimal top_rightY_1 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0);
                    decimal bottom_rightX_1 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 0);
                    decimal bottom_rightY_1 = 0;
                    double _xOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 0));
                    double _yOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 0));

                    xOffset = _xOffset;
                    yOffset = _yOffset;

                    // Populate array.
                    tempArray[0] = new GeoLibPointF((double)bottom_leftX_1, (double)bottom_leftY_1);
                    tempArray[1] = new GeoLibPointF((double)top_leftX_1, (double)top_leftY_1);
                    tempArray[2] = new GeoLibPointF((double)top_rightX_1, (double)top_rightY_1);
                    tempArray[3] = new GeoLibPointF((double)bottom_rightX_1, (double)bottom_rightY_1);
                    tempArray[4] = new GeoLibPointF(tempArray[0]);

                    // Apply our deltas
#if QUILTTHREADED
                Parallel.For(0, 5, (i) =>
#else
                    for (Int32 i = 0; i < 5; i++)
#endif
                    {
                        tempArray[i].X += xOffset + x;
                        tempArray[i].Y += yOffset + y;
                    }
#if QUILTTHREADED
                );
#endif
                    decimal bottom_leftX_2 = 0;
                    decimal bottom_leftY_2 = 0;
                    decimal top_leftX_2 = 0;
                    decimal top_leftY_2 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1);
                    decimal top_rightX_2 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1);
                    decimal top_rightY_2 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 1);
                    decimal bottom_rightX_2 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 1);
                    decimal bottom_rightY_2 = 0;
                    xOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 1)) + _xOffset;
                    yOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) + _yOffset;

                    // Populate array.
                    tempArray[5 + 0] = new GeoLibPointF((double)bottom_leftX_2, (double)bottom_leftY_2);
                    tempArray[5 + 1] = new GeoLibPointF((double)top_leftX_2, (double)top_leftY_2);
                    tempArray[5 + 2] = new GeoLibPointF((double)top_rightX_2, (double)top_rightY_2);
                    tempArray[5 + 3] = new GeoLibPointF((double)bottom_rightX_2, (double)bottom_rightY_2);
                    tempArray[5 + 4] = new GeoLibPointF(tempArray[5 + 0]);

                    // Apply our deltas
#if QUILTTHREADED
                Parallel.For(0, 5, (i) =>
#else
                    for (Int32 i = 0; i < 5; i++)
#endif
                    {
                        tempArray[5 + i].X += xOffset + x;
                        tempArray[5 + i].Y += yOffset + y;
                    }
#if QUILTTHREADED
                );
#endif
                    decimal bottom_leftX_3 = 0;
                    decimal bottom_leftY_3 = 0;
                    decimal top_leftX_3 = 0;
                    decimal top_leftY_3 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2);
                    decimal top_rightX_3 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 2);
                    decimal top_rightY_3 = patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2);
                    decimal bottom_rightX_3 = patternElement.getDecimal(PatternElement.properties_decimal.horLength, 2);
                    decimal bottom_rightY_3 = 0;
                    xOffset = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.horOffset, 2)) + _xOffset;
                    yOffset = -Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verOffset, 2) + patternElement.getDecimal(PatternElement.properties_decimal.verLength, 2)) + _yOffset;
                    if (shapeString == "S")
                    {
                        yOffset += Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.verLength, 0)); // offset our subshape to put it in the correct place in the UI.
                    }

                    // Populate array.
                    tempArray[10 + 0] = new GeoLibPointF((double)bottom_leftX_3, (double)bottom_leftY_3);
                    tempArray[10 + 1] = new GeoLibPointF((double)top_leftX_3, (double)top_leftY_3);
                    tempArray[10 + 2] = new GeoLibPointF((double)top_rightX_3, (double)top_rightY_3);
                    tempArray[10 + 3] = new GeoLibPointF((double)bottom_rightX_3, (double)bottom_rightY_3);
                    tempArray[10 + 4] = new GeoLibPointF(tempArray[10 + 0]);

                    // Apply our deltas
#if QUILTTHREADED
                Parallel.For(0, 5, (i) =>
#else
                    for (Int32 i = 0; i < 5; i++)
#endif
                    {
                        tempArray[10 + i].X += xOffset + x;
                        tempArray[10 + i].Y += yOffset + y;
                    }
#if QUILTTHREADED
                );
#endif
                    break;
            }

            return pTransformed(tempArray, pattern, index, doRotation: doRotation);
        }

        List<GeoLibPointF[]> pTransformed(List<GeoLibPointF[]> source, Pattern pattern, int index, bool doRotation = true)
        {
            GeoLibPointF bb = GeoWrangler.midPoint(source);

            List<GeoLibPointF[]> transformed = new List<GeoLibPointF[]>();

            PatternElement patternElement = pattern.getPatternElement(index);

            double rotAngle = Convert.ToDouble(patternElement.getDecimal(PatternElement.properties_decimal.arrayRotation));
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

            foreach (var t in source)
            {
                transformed.Add(pTransformed(t.ToArray(), pattern, index, bb, rotAngle, rotRef, rotRefUseArray, flipH:false, flipV:false, alignX:false, alignY:false, refPivot, doRotation));
            }

            return transformed;
        }

        GeoLibPointF[] pTransformed(GeoLibPointF[] tempArray, Pattern pattern, int index, GeoLibPointF bb = null, bool doRotation = true)
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

            return pTransformed(tempArray, pattern, index, bb, rotAngle, rotRef, rotRefUseArray, flipH, flipV, alignX, alignY, refPivot, doRotation);
        }

        // Note that this has the corrected rotRef (0 is the first pattern element, -1 is world)
        GeoLibPointF[] pTransformed(GeoLibPointF[] tempArray, Pattern pattern, int index, GeoLibPointF bb, double rotAngle, int rotRef, int rotRefUseArray, bool flipH, bool flipV, bool alignX, bool alignY, bool refPivot, bool doRotation)
        {
            GeoLibPointF[] transformed = tempArray.ToArray();

            // ReSharper disable once ConvertIfStatementToNullCoalescingAssignment
            if (bb == null)
            {
                bb = GeoWrangler.midPoint(tempArray);
            }

            GeoLibPointF pivot = new GeoLibPointF(bb.X, bb.Y);

            if (!doRotation)
            {
                rotAngle = 0.0f;
            }

            if (Math.Abs(rotAngle) > double.Epsilon)
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
                        refIsArray = refIsArray || (pattern.getPatternElement(rotRef).getInt(PatternElement.properties_i.arrayRef) > 0);

                        GeoLibPointF r_pivot = new GeoLibPointF(pivot.X, pivot.Y);

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
                                    // We need to actually figure out our reference array midpoint now in order to use it.
                                    double[] bounds = pShapeBounds(pattern, rotRef, doRotation: boundsAfterRotation);
                                    double width = bounds[2] - bounds[0];
                                    double height = bounds[3] - bounds[1];
                                    double xSpace = Convert.ToDouble(pattern.getPatternElement(rotRef).getDecimal(PatternElement.properties_decimal.arrayXSpace));
                                    double ySpace = Convert.ToDouble(pattern.getPatternElement(rotRef).getDecimal(PatternElement.properties_decimal.arrayYSpace));
                                    int xCount = pattern.getPatternElement(rotRef).getInt(PatternElement.properties_i.arrayXCount);
                                    int yCount = pattern.getPatternElement(rotRef).getInt(PatternElement.properties_i.arrayYCount);

                                    double arrayWidth = (xCount * width) + ((xCount - 1) * xSpace);
                                    double arrayHeight = (yCount * height) + ((yCount - 1) * ySpace);

                                    r_pivot =new GeoLibPointF(bounds[0] + (arrayWidth / 2.0f), bounds[1] + (arrayHeight / 2.0f));
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
                                    r_pivot = new GeoLibPointF(pattern.getPatternElement(rotRef).getMidPoint());
                                }
                                else
                                {
                                    r_pivot = new GeoLibPointF(GeoWrangler.midPoint(pGetPointArray(pattern, rotRef, doRotation: boundsAfterRotation)));
                                    pattern.getPatternElement(rotRef).setMidPoint(r_pivot);
                                }
                            }

                            rotAngle = Convert.ToDouble(pattern.getPatternElement(rotRef).getDecimal(PatternElement.properties_decimal.rotation));
                        }

                        if (Math.Abs(rotAngle) > double.Epsilon)
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

            tempArray = transformed.ToArray();

            return tempArray;
        }

        double[] pShapeBounds(Pattern pattern, Int32 settingsIndex, bool doRotation = true)
        {
            GeoLibPointF[] inputPoints = pGetPointArray(pattern, settingsIndex, doRotation: doRotation);
            // Apply any transformations to get the bounding box post-rotation, etc.
            inputPoints = pTransformed(new List<GeoLibPointF[]> { inputPoints }, pattern, settingsIndex, doRotation: doRotation)[0];
            return pShapeBounds(inputPoints);
        }

        double[] pShapeBounds(GeoLibPointF[] inputPoints)
        {
            double minX = inputPoints.Min(p => p.X);
            double maxX = inputPoints.Max(p => p.X);

            double minY = inputPoints.Min(p => p.Y);
            double maxY = inputPoints.Max(p => p.Y);

            return new [] { minX, minY, maxX, maxY };
        }

        void pInit(Pattern pattern, Int32 settingsIndex)
        {
            try
            {
                previewPoints = new List<GeoLibPointF[]>();
                sourceIndices = new List<Int32>();
                drawnPoly = new List<bool>();
                textEntity = new List<bool>();
                color = MyColor.Black; // overridden later.

                PatternElement patternElement = pattern.getPatternElement(settingsIndex);

                elementIndex = settingsIndex;
                linkedElementIndex = patternElement.getInt(PatternElement.properties_i.linkedElementIndex);

                int shapeType = patternElement.getInt(PatternElement.properties_i.shapeIndex);
                bool textShape = shapeType == (int)CommonVars.shapeNames.text;
                bool layoutShape = shapeType == (int)CommonVars.shapeNames.complex;

                GeoLibPointF[] outputPoints;

                ComplexShape complexPoints;

                GeoLibPointF[] inputPoints = pGetPointArray(pattern, settingsIndex);

                if (!layoutShape)
                {
                    complexPoints = new ComplexShape(pattern.getPatternElements(), settingsIndex);
                    outputPoints = complexPoints.getPoints();

                    outputPoints = pTransformed(outputPoints, pattern, settingsIndex);

                    previewPoints.Add(inputPoints.Take(5).ToArray());
                    drawnPoly.Add(true);
                    previewPoints.Add(inputPoints.Skip(5).Take(5).ToArray());
                    drawnPoly.Add(true);
                    previewPoints.Add(inputPoints.Skip(10).Take(5).ToArray());
                    drawnPoly.Add(true);

                    previewPoints.Add(outputPoints);
                    drawnPoly.Add(false);
                }
                else
                {
                    ShapeLibrary shape = new ShapeLibrary(shapeType, patternElement);
                    inputPoints = GeoWrangler.close(inputPoints);

                    previewPoints.Add(inputPoints.ToArray());
                    drawnPoly.Add(true);

                    inputPoints = GeoWrangler.move(inputPoints, -patternElement.getDecimal(PatternElement.properties_decimal.xPos), -patternElement.getDecimal(PatternElement.properties_decimal.yPos));
                    shape.setShape(shapeType, inputPoints);
                    complexPoints = new ComplexShape(pattern.getPatternElements(), settingsIndex, shape);

                    outputPoints = complexPoints.getPoints();
                    previewPoints.Add(outputPoints.ToArray());
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

                bool doArray = (patternElement.isXArray() || patternElement.isYArray());
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

                List<bool> oDP = new List<bool>();

                for (int x = 0; x < xCount; x++)
                {
                    for (int y = 0; y < yCount; y++)
                    {
                        oDP.AddRange(drawnPoly);
                    }
                }

                drawnPoly = new List<bool>(oDP);

                // Get our offsets configured.
                pDoOffsets(patternElement);
                
                sourceIndices.Clear();
                for (Int32 poly = 0; poly < previewPoints.Count(); poly++)
                {
                    textEntity.Add(textShape); // To track for output to layout.
                    sourceIndices.Add(settingsIndex);
                    int pCount = previewPoints[poly].Count();
                    int poly1 = poly;
#if QUILTTHREADED
                    Parallel.For(0, pCount, (point) =>
#else
                    for (Int32 point = 0; point < pCount; point++)
#endif
                    {
                        double px = previewPoints[poly1][point].X + xOffset;
                        double py = previewPoints[poly1][point].Y - yOffset;

                        previewPoints[poly1][point] = new GeoLibPointF(px, py);
                    }
#if QUILTTHREADED
                    );
#endif
                    if ((Math.Abs(previewPoints[poly][0].X - previewPoints[poly][previewPoints[poly].Count() - 1].X) > Double.Epsilon) ||
                        (Math.Abs(previewPoints[poly][0].Y - previewPoints[poly][previewPoints[poly].Count() - 1].Y) > Double.Epsilon))
                    {
                        ErrorReporter.showMessage_OK("Start and end not the same - previewShape", "Oops");
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
