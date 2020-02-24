using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quilt
{
    public class Pattern
    {
        public override bool Equals(object obj)
        {
            return equivalence((Pattern)obj);
        }

        QuiltContext quiltContext;
        List<PatternElement> patternElements;
        double x, y;

        bool bbEvaluationNeeded;
        List<int> bbShapes;

        public override int GetHashCode()
        {
            int code = 0;
            for (int i = 0; i < patternElements.Count; i++)
            {
                code = code ^ patternElements[i].GetHashCode();
            }

            return code;
        }

        // To get the bounding box for our list of preview shapes
        public class BoundingBox
        {
            List<GeoLibPointF> points;
            GeoLibPointF midPoint;
            
            double _minX = 0, _maxX = 0, _minY = 0, _maxY = 0;

            public double minX
            {
                get
                {
                    return _minX;
                }
                set
                {
                    _minX = value;
                }
            }

            public double minY
            {
                get
                {
                    return _minY;
                }
                set
                {
                    _minY = value;
                }
            }

            public double maxX
            {
                get
                {
                    return _maxX;
                }
                set
                {
                    _maxX = value;
                }
            }

            public double maxY
            {
                get
                {
                    return _maxY;
                }
                set
                {
                    _maxY = value;
                }
            }

            public BoundingBox(List<PreviewShape> previewShapes)
            {
                pBoundingBox(previewShapes);
            }

            void pBoundingBox(List<PreviewShape> previewShapes)
            {
                if ((previewShapes == null) || (previewShapes.Count == 0))
                {
                    minX = 0;
                    maxX = 0;
                    minY = 0;
                    maxY = 0;
                    points = new List<GeoLibPointF>(4) { new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0) };
                    midPoint = new GeoLibPointF(0, 0);
                    return;
                }

                List<GeoLibPointF[]> pPoints = previewShapes[0].getPoints();

                BoundingBox test;

                if (pPoints[0].Length != 0)
                {
                    test = new BoundingBox(pPoints[0]);
                }
                else
                {
                    test = new BoundingBox(new GeoLibPointF[4] { new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0) });
                }

                points = test.points.ToList();

                for (int i = 0; i < previewShapes.Count; i++)
                {
                    List<GeoLibPointF[]> polys = previewShapes[i].getPoints();
                    for (int poly = 0; poly < polys.Count; poly++)
                    {
                        test = new BoundingBox(polys[poly]);

                        minX = test.points.Min(p => p.X);
                        if (minX < points[0].X)
                        {
                            // Reposition our min X points
                            points[0].X = minX;
                            points[1].X = minX;
                        }
                        minY = test.points.Min(p => p.Y);
                        if (minY < points[0].Y)
                        {
                            // Reposition our min Y points
                            points[0].Y = minY;
                            points[3].Y = minY;
                        }
                        maxX = test.points.Max(p => p.X);
                        if (maxX > points[2].X)
                        {
                            // Reposition our max X points
                            points[2].X = maxX;
                            points[3].X = maxX;
                        }
                        maxY = test.points.Max(p => p.Y);
                        if (maxY > points[2].Y)
                        {
                            // Reposition our max Y points
                            points[1].Y = maxY;
                            points[2].Y = maxY;
                        }
                    }
                }
                minX = points.Min(p => p.X);
                minY = points.Min(p => p.Y);
                maxX = points.Max(p => p.X);
                maxY = points.Max(p => p.Y);
                midPoint = new GeoLibPointF(minX + ((maxX - minX) / 2.0f), minY + ((maxY - minY) / 2.0f));
            }

            public List<GeoLibPointF> getPoints()
            {
                return pGetPoints();
            }

            List<GeoLibPointF> pGetPoints()
            {
                return points;
            }

            public GeoLibPointF getMidPoint()
            {
                return pGetMidPoint();
            }

            GeoLibPointF pGetMidPoint()
            {
                return midPoint;
            }

            public BoundingBox(GeoLibPointF[] incomingPoints)
            {
                pBoundingBox(incomingPoints);
            }

            void pBoundingBox(GeoLibPointF[] incomingPoints)
            {
                points = new List<GeoLibPointF>();
                if (incomingPoints == null)
                {
                    minX = 0;
                    maxX = 0;
                    minY = 0;
                    maxY = 0;
                    points.Add(new GeoLibPointF(0.0f, 0.0f));
                    points.Add(new GeoLibPointF(0.0f, 0.0f));
                    points.Add(new GeoLibPointF(0.0f, 0.0f));
                    points.Add(new GeoLibPointF(0.0f, 0.0f));
                    midPoint = new GeoLibPointF(0.0f, 0.0f);
                }
                else
                {
                    // Compile a list of our points.
                    List<GeoLibPointF> iPoints = new List<GeoLibPointF>();
                    for (int i = 0; i < incomingPoints.Count(); i++)
                    {
                        iPoints.Add(incomingPoints[i]);
                    }
                    minX = iPoints.Min(p => p.X);
                    minY = iPoints.Min(p => p.Y);
                    maxX = iPoints.Max(p => p.X);
                    maxY = iPoints.Max(p => p.Y);
                    points.Add(new GeoLibPointF(minX, minY));
                    points.Add(new GeoLibPointF(minX, maxY));
                    points.Add(new GeoLibPointF(maxX, maxY));
                    points.Add(new GeoLibPointF(maxX, minY));
                    midPoint = new GeoLibPointF(minX + ((maxX - minX) / 2.0f), minY + ((maxY - minY) / 2.0f));
                }
            }
        }

        public BoundingBox boundingBox()
        {
            BoundingBox bb = new BoundingBox(previewShapes);

            if (bb.getPoints().Count() != 4)
            {
                // Just for debug - something went wrong.
                int xx = 2;
            }

            return bb;
        }

        public bool rotCyclicalCheck(int cYRef)
        {
            return pRotCyclicalCheck(cYRef);
        }

        bool pRotCyclicalCheck(int cYRef)
        {
            return pRotCyclicalCheck_int(cYRef) != -1;
        }

        int pRotCyclicalCheck_int(int cRotRef)
        {
            List<int> rotRefs = new List<int>();
            rotRefs.Add(cRotRef);

            bool rotCyclical = false;
            int collisionIndex = -1;

            while (cRotRef >= 0 && !rotCyclical)
            {
                cRotRef = pGetPatternElement(cRotRef).getInt(PatternElement.properties_i.yPosRef) - 1;
                if (rotRefs.IndexOf(cRotRef) == -1)
                {
                    rotRefs.Add(cRotRef);
                }
                else
                {
                    rotCyclical = true;
                    collisionIndex = cRotRef;
                }
            }

            return collisionIndex;
        }

        List<PreviewShape> previewShapes;

        public Pattern(ref QuiltContext context, List<PatternElement> p)
        {
            quiltContext = context;
            patternElements = new List<PatternElement>();
            for (int i = 0; i < p.Count; i++)
            {
                patternElements.Add(new PatternElement(p[i]));
            }
        }

        public Pattern(Pattern source)
        {
            quiltContext = source.quiltContext;
            patternElements = new List<PatternElement>();
            for (int i = 0; i < source.patternElements.Count; i++)
            {
                patternElements.Add(new PatternElement(source.patternElements[i]));
            }
        }

        public void setPos(double px, double py)
        {
            pSetPos(px, py);
        }

        void pSetPos(double px, double py)
        {
            x = px;
            y = py;
        }

        public GeoLibPointF getPos()
        {
            return pGetPos();
        }

        GeoLibPointF pGetPos()
        {
            return new GeoLibPointF(x, y);
        }

        bool pXCyclicalCheck(int cXRef)
        {
            return pXCyclicalCheck_int(cXRef) != -1;
        }

        int pXCyclicalCheck_int(int cXRef)
        {
            List<int> xRefs = new List<int>();
            xRefs.Add(cXRef);

            bool xCyclical = false;
            int collisionIndex = -1;

            while ((cXRef >= 0) && !xCyclical)
            {
                cXRef = patternElements[cXRef].getInt(PatternElement.properties_i.xPosRef) - 1;
                if (xRefs.IndexOf(cXRef) == -1)
                {
                    xRefs.Add(cXRef);
                }
                else
                {
                    xCyclical = true;
                    collisionIndex = cXRef;
                }
            }

            return collisionIndex;
        }

        bool pYCyclicalCheck(int cYRef)
        {
            return pYCyclicalCheck_int(cYRef) != -1;
        }

        int pYCyclicalCheck_int(int cYRef)
        {
            List<int> yRefs = new List<int>();
            yRefs.Add(cYRef);

            bool yCyclical = false;
            int collisionIndex = -1;

            while (cYRef >= 0 && !yCyclical)
            {
                cYRef = patternElements[cYRef].getInt(PatternElement.properties_i.yPosRef) - 1;
                if (yRefs.IndexOf(cYRef) == -1)
                {
                    yRefs.Add(cYRef);
                }
                else
                {
                    yCyclical = true;
                    collisionIndex = cYRef;
                }
            }

            return collisionIndex;
        }

        List<GeoLibPointF> pBBDims(int index)
        {
            PreviewShape pShape1 = new PreviewShape(this, index);
            BoundingBox bb = new BoundingBox(new List<PreviewShape> { pShape1 });

            return bb.getPoints();
        }

        enum bbDims { width, height }

        double bbDimension(bbDims prop, int index)
        {
            double ret = 0;
            List<GeoLibPointF> bb = pBBDims(index);
            switch (prop)
            {
                case bbDims.width:
                    ret = bb.Max(p => p.X) - bb.Min(p => p.X);
                    break;
                case bbDims.height:
                    ret = bb.Max(p => p.Y) - bb.Min(p => p.Y);
                    break;
            }

            return ret;
        }

        decimal doX(int i)
        {
            decimal x_ = 0m;

            // Here things get slightly tricky - we have 'world origin' injected at the 0-index, so we need to compensate for the layer look-ups.
            int xRef = patternElements[i].getInt(PatternElement.properties_i.xPosRef) - 1;

            int sRef = patternElements[i].getInt(PatternElement.properties_i.xPosSubShapeRef);

            int sPosRef = patternElements[i].getInt(PatternElement.properties_i.xPosSubShapeRefPos);

            // We have a relative positioning situation.
            // Note that this could be cascading so we need to walk the stack

            if (xRef >= 0)
            {
                bool refFlipped = patternElements[xRef].getInt(PatternElement.properties_i.flipH) == 1;
                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.xPos);

                // In the case of an array shape type for the reference, we may still have subshape relative references. To check this, we need to query for an array. If we don't have an array, we're good.
                // Then we also need to query whether the subshape reference is a higher value than the number of subshapes in the shape; the 'array' subshape reference is last in the list.
                bool doX = !patternElements[xRef].isXArray() || (patternElements[xRef].isXArray() && (sRef < patternElements[xRef].getSubShapeCount()));

                int aXRef = xRef;
                // Is the array reference a relative array?
                if (patternElements[xRef].getInt(PatternElement.properties_i.arrayRef) != 0)
                {
                    aXRef = patternElements[xRef].getInt(PatternElement.properties_i.arrayRef) - 1;

                    int PSSR = patternElements[i].getInt(PatternElement.properties_i.xPosSubShapeRef);
                    int PSSC = patternElements[xRef].getSubShapeCount();
                    if (PSSR == PSSC)
                    {
                        doX = false;
                    }
                }

                if (doX)
                {
                    if (sRef == 1)
                    {
                        x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s0HorOffset);
                        if (patternElements[xRef].getInt(PatternElement.properties_i.shapeIndex) != (int)CommonVars.shapeNames.Sshape)
                        {
                            if ((patternElements[xRef].getInt(PatternElement.properties_i.shapeIndex) != (int)CommonVars.shapeNames.Ushape) && (patternElements[xRef].getInt(PatternElement.properties_i.shapeIndex) != (int)CommonVars.shapeNames.Xshape))
                            {
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s0HorLength);
                            }
                            else
                            {
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s1HorOffset);
                            }
                        }
                    }
                    if (sRef == 2)
                    {
                        x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s0HorOffset);
                        x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s2HorOffset);
                    }
                }

                // Process the side case.
                if ((!refFlipped && (sPosRef == (int)CommonVars.subShapeHorLocs.R)) || (refFlipped && (sPosRef == (int)CommonVars.subShapeHorLocs.L)))
                {
                    if (doX)
                    {
                        switch (sRef)
                        {
                            case 0:
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s0HorLength);
                                break;
                            case 1:
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s1HorLength);
                                break;
                            case 2:
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s2HorLength);
                                break;
                        }
                    }
                    else
                    {
                        x_ += Convert.ToDecimal(bbDimension(bbDims.width, aXRef));
                    }
                }

                if (sPosRef == (int)CommonVars.subShapeHorLocs.M)
                {
                    if (doX)
                    {
                        switch (sRef)
                        {
                            case 0:
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s0HorLength) / 2;
                                break;
                            case 1:
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s1HorLength) / 2;
                                break;
                            case 2:
                                x_ += patternElements[xRef].getDecimal(PatternElement.properties_decimal.s2HorLength) / 2;
                                break;
                        }
                    }
                    else
                    {
                        x_ += Convert.ToDecimal(bbDimension(bbDims.width, aXRef)) / 2;
                    }
                }

                // Is the reference flipped? If so, we need to flip our value to get the correct result
                if (refFlipped)
                {
                    ShapeLibrary t = new ShapeLibrary(patternElements[xRef]);
                    GeoLibPointF pivot = t.getPivotPoint();

                    bool alignX = patternElements[xRef].getInt(PatternElement.properties_i.alignX) == 1;
                    bool alignY = patternElements[xRef].getInt(PatternElement.properties_i.alignY) == 1;
                    x_ = Convert.ToDecimal(GeoWrangler.flip(true, false, alignX, alignY, pivot, new GeoLibPointF[] { new GeoLibPointF(Convert.ToDouble(x_), 0) })[0].X);
                }

                if (doX)
                {
                    switch (sRef)
                    {
                        case 0:
                            x_ += patternElements[i].getDecimal(PatternElement.properties_decimal.s0HorOffset);
                            break;
                        case 1:
                            x_ += patternElements[i].getDecimal(PatternElement.properties_decimal.s1HorOffset);
                            break;
                        case 2:
                            x_ += patternElements[i].getDecimal(PatternElement.properties_decimal.s2HorOffset);
                            break;
                    }
                }

                xRef = patternElements[xRef].getInt(PatternElement.properties_i.xPosRef) - 1;
            }

            return x_;
        }

        decimal doY(int i)
        {
            decimal y_ = 0m;

            // Here things get slightly tricky - we have 'world origin' injected at the 0-index, so we need to compensate for the layer look-ups.
            int yRef = patternElements[i].getInt(PatternElement.properties_i.yPosRef) - 1;

            int sRef = patternElements[i].getInt(PatternElement.properties_i.yPosSubShapeRef);

            int sPosRef = patternElements[i].getInt(PatternElement.properties_i.yPosSubShapeRefPos);

            // We have a relative positioning situation.
            // Note that this could be cascading so we need to walk the stack

            if (yRef >= 0)
            {
                bool refFlipped = patternElements[yRef].getInt(PatternElement.properties_i.flipV) == 1;

                y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.yPos);

                // In the case of an array shape type for the reference, we may still have subshape relative references. To check this, we need to query for an array. If we don't have an array, we're good.
                // Then we also need to query whether the subshape reference is a higher value than the number of subshapes in the shape; the 'array' subshape reference is last in the list.
                bool doY = !patternElements[yRef].isYArray() || (patternElements[yRef].isYArray() && (sRef < patternElements[yRef].getSubShapeCount()));

                int aYRef = yRef;

                // Is the array reference a relative array?
                if (patternElements[yRef].getInt(PatternElement.properties_i.arrayRef) != 0)
                {
                    aYRef = patternElements[yRef].getInt(PatternElement.properties_i.arrayRef) - 1;

                    int PSSR = patternElements[i].getInt(PatternElement.properties_i.yPosSubShapeRef);
                    int PSSC = patternElements[yRef].getSubShapeCount();
                    if (PSSR == PSSC)
                    {
                        doY = false;
                    }
                }

                if (doY)
                {
                    y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s0VerOffset);
                    if (sRef == 1)
                    {
                        y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s1VerOffset);
                    }
                    if (sRef == 2)
                    {
                        y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s0VerLength);
                        y_ -= patternElements[yRef].getDecimal(PatternElement.properties_decimal.s2VerLength);
                        y_ -= patternElements[yRef].getDecimal(PatternElement.properties_decimal.s2VerOffset);
                    }
                }

                // Process the side case.
                if ((!refFlipped && (sPosRef == (int)CommonVars.subShapeVerLocs.T)) || (refFlipped && (sPosRef == (int)CommonVars.subShapeVerLocs.B)))
                {
                    if (doY)
                    {
                        switch (sRef)
                        {
                            case 0:
                                y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s0VerLength);
                                break;
                            case 1:
                                y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s1VerLength);
                                break;
                            case 2:
                                y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s2VerLength);
                                break;
                        }
                    }
                    else
                    {
                        y_ += Convert.ToDecimal(bbDimension(bbDims.height, aYRef));
                    }
                }

                if (sPosRef == (int)CommonVars.subShapeVerLocs.M)
                {
                    if (doY)
                    {
                        switch (sRef)
                        {
                            case 0:
                                y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s0VerLength) / 2;
                                break;
                            case 1:
                                y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s1VerLength) / 2;
                                break;
                            case 2:
                                y_ += patternElements[yRef].getDecimal(PatternElement.properties_decimal.s2VerLength) / 2;
                                break;
                        }
                    }
                    else
                    {
                        y_ += Convert.ToDecimal(bbDimension(bbDims.height, aYRef)) / 2;
                    }
                }

                // Is the reference flipped? If so, we need to flip our value to get the correct result
                if (refFlipped)
                {
                    ShapeLibrary t = new ShapeLibrary(patternElements[yRef]);
                    GeoLibPointF pivot = t.getPivotPoint();

                    bool alignX = patternElements[yRef].getInt(PatternElement.properties_i.alignX) == 1;
                    bool alignY = patternElements[yRef].getInt(PatternElement.properties_i.alignY) == 1;
                    y_ = Convert.ToDecimal(GeoWrangler.flip(false, true, alignX, alignY, pivot, new GeoLibPointF[] { new GeoLibPointF(0, Convert.ToDouble(y_)) })[0].Y);
                }

                if (doY)
                {
                    switch (sRef)
                    {
                        case 0:
                            y_ += patternElements[i].getDecimal(PatternElement.properties_decimal.s0VerOffset);
                            break;
                        case 1:
                            y_ += patternElements[i].getDecimal(PatternElement.properties_decimal.s1VerOffset);
                            break;
                        case 2:
                            break;
                    }
                }

                yRef = patternElements[yRef].getInt(PatternElement.properties_i.yPosRef) - 1;
            }

            return y_;

        }

        void pArrangeElements()
        {
            // We need to apply the relative transforms.
            // We need to keep these separate until we're done, to avoid trouble.
            decimal[,] positions = new decimal[patternElements.Count, 2];

            Parallel.For(0, patternElements.Count, (i) =>
            // for (int i = 0; i < patternElements.Count; i++)
            {
                decimal x_ = doX(i);
                decimal y_ = doY(i);

                positions[i, 0] = x_;
                positions[i, 1] = y_;
            });

            // Set our positions
            Parallel.For(0, patternElements.Count, (i) =>
            // for (int i = 0; i < patternElements.Count; i++)
            {
                decimal x1_ = patternElements[i].getDecimal(PatternElement.properties_decimal.xPos) + positions[i, 0];
                int xRef = patternElements[i].getInt(PatternElement.properties_i.xPosRef) - 1;

                if (xRef >= 0)
                {
                    bool xCyclical = pXCyclicalCheck(xRef);

                    if (!xCyclical)
                    {
                        while (xRef >= 0)
                        {
                            x1_ += positions[xRef, 0];
                            xRef = patternElements[xRef].getInt(PatternElement.properties_i.xPosRef) - 1;
                        }
                    }
                }

                decimal y1_ = patternElements[i].getDecimal(PatternElement.properties_decimal.yPos) + positions[i, 1];
                int yRef = patternElements[i].getInt(PatternElement.properties_i.yPosRef) - 1;

                if (yRef >= 0)
                {
                    bool yCyclical = pYCyclicalCheck(yRef);

                    if (!yCyclical)
                    {
                        while (yRef >= 0)
                        {
                            y1_ += positions[yRef, 1];
                            yRef = patternElements[yRef].getInt(PatternElement.properties_i.yPosRef) - 1;
                        }
                    }
                }
                patternElements[i].setDecimal(PatternElement.properties_decimal.xPos, x1_);
                patternElements[i].setDecimal(PatternElement.properties_decimal.yPos, y1_);
            });
        }

        public bool equivalence(Pattern pattern)
        {
            return pEquivalence(pattern);
        }

        bool pEquivalence(Pattern pattern)
        {

            if (pattern.getPatternElements().Count != patternElements.Count)
            {
                return false;
            }

            bool ret = true;

            for (int e = 0; e < patternElements.Count; e++)
            {
                ret = patternElements[e].equivalence(pattern.getPatternElement(e));

                if (!ret)
                {
                    break;
                }
            }

            return ret;
        }

        public PatternElement getPatternElement(int index)
        {
            return pGetPatternElement(index);
        }

        PatternElement pGetPatternElement(int index)
        {
            return patternElements[index];
        }

        public List<PatternElement> getPatternElements()
        {
            return pGetPatternElements();
        }

        List<PatternElement> pGetPatternElements()
        {
            return patternElements;
        }

        // Responsible for generating our preview shapes per the settings.
        // All offsets need to be applied here - previewPanel takes what it is given and draws that.
        public List<PreviewShape> generate_shapes()
        {
            return pGenerate_shapes();
        }

        PreviewShape pGenerate_shapes(int index)
        {
            PreviewShape pShape1 = new PreviewShape(this, index);
            pShape1.setColor(quiltContext.colors.subshape1_Color);
            return pShape1;
        }

        List<PreviewShape> pGenerate_shapes()
        {
            bbEvaluationNeeded = false;
            bbShapes = new List<int>();

            previewShapes = new List<PreviewShape>();

            pArrangeElements();

            for (int index = 0; index < patternElements.Count; index++)
            {
                string shapeString = ((CentralProperties.typeShapes)pGetPatternElement(index).getInt(PatternElement.properties_i.shapeIndex)).ToString();
                // User has a shape chosen so we can draw a preview, except for bounding which is deferred as the overall bounding box is needed for the pattern.
                if ((shapeString != "none") && (shapeString != "bounding"))
                {
                    previewShapes.Add(pGenerate_shapes(index));
                }
                else
                {
                    if (shapeString == "bounding")
                    {
                        bbEvaluationNeeded = true;
                        // Need to defer this until we have the extents arranged.
                        bbShapes.Add(index);
                    }
                    // No preview - no shape chosen.
                }
            }

            if (bbEvaluationNeeded)
            {
                bbEvaluation();
            }
            return previewShapes;
        }

        void bbEvaluation()
        {
            // Get the BB and shoe-horn it into a shape.
            BoundingBox bb = new BoundingBox(previewShapes);

            for (int i = 0; i < bbShapes.Count; i++)
            {
                // This is an ugly hack because of the positional reference for the settings.
                Pattern tPattern = new Pattern(this);

                // Use our bounding box shape as the basis for the temporary pattern element; this ensures we capture rotation, flip, etc. values.
                PatternElement t = new PatternElement(patternElements[bbShapes[i]]);
                t.setInt(PatternElement.properties_i.shapeIndex, (int)CommonVars.shapeNames.rect);

                decimal leftMargin = patternElements[bbShapes[i]].getDecimal(PatternElement.properties_decimal.boundingLeft);

                decimal rightMargin = patternElements[bbShapes[i]].getDecimal(PatternElement.properties_decimal.boundingRight);

                decimal bottomMargin = patternElements[bbShapes[i]].getDecimal(PatternElement.properties_decimal.boundingBottom);

                decimal topMargin = patternElements[bbShapes[i]].getDecimal(PatternElement.properties_decimal.boundingTop);

                decimal width = (decimal)(bb.maxX - bb.minX) + leftMargin + rightMargin;

                decimal height = (decimal)(bb.maxY - bb.minY) + bottomMargin + topMargin;

                decimal xPos = (decimal)(bb.minX) - leftMargin;
                decimal xOffset = patternElements[bbShapes[i]].getDecimal(PatternElement.properties_decimal.xPos);
                t.setDecimal(PatternElement.properties_decimal.xPos, xPos + xOffset);

                decimal yPos = (decimal)(bb.minY) - bottomMargin;
                decimal yOffset = patternElements[bbShapes[i]].getDecimal(PatternElement.properties_decimal.yPos);
                t.setDecimal(PatternElement.properties_decimal.yPos, yPos + yOffset);

                t.setDecimal(PatternElement.properties_decimal.s0HorLength, width);

                t.setDecimal(PatternElement.properties_decimal.s0VerLength, height);

                tPattern.patternElements[bbShapes[i]] = t;

                PreviewShape tShape = new PreviewShape(tPattern, bbShapes[i]);
                tShape.setColor(quiltContext.colors.subshape1_Color);
                previewShapes.Add(tShape);
            }
        }

        public string getDescription()
        {
            return pGetDescription();
        }

        string pGetDescription()
        {
            string description = "";
            for (int p = 0; p < patternElements.Count; p++)
            {
                description += p.ToString() + ";" + patternElements[p].getDescription();
                if (p < patternElements.Count - 1)
                {
                    description += ";";
                }
            }

            return description;
        }
    }
}
