using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quilt
{
    public partial class Pattern
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
            foreach (PatternElement t in patternElements)
            {
                code = code ^ t.GetHashCode();
            }

            return code;
        }

        // To get the bounding box for our list of preview shapes
        public class BoundingBox
        {
            List<GeoLibPointF> points;
            GeoLibPointF midPoint;
            
            public double minX { get; private set; }
            public double maxX { get; private set; }
            public double minY { get; private set; }
            public double maxY { get; private set; }
            
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

                if (pPoints.Count == 0)
                {
                    minX = 0;
                    maxX = 0;
                    minY = 0;
                    maxY = 0;
                    points = new List<GeoLibPointF>(4) { new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0) };
                    midPoint = new GeoLibPointF(0, 0);
                    return;
                }

                BoundingBox test;

                if (pPoints[0].Any())
                {
                    test = new BoundingBox(pPoints[0]);
                }
                else
                {
                    test = new BoundingBox(new[] { new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0), new GeoLibPointF(0, 0) });
                }

                points = test.points.ToList();

                foreach (PreviewShape t in previewShapes)
                {
                    List<GeoLibPointF[]> polys = t.getPoints();
                    foreach (GeoLibPointF[] t1 in polys)
                    {
                        test = new BoundingBox(t1);

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

            private BoundingBox(GeoLibPointF[] incomingPoints)
            {
                pBoundingBox(incomingPoints);
            }

            void pBoundingBox(GeoLibPointF[] incomingPoints)
            {
                points = new List<GeoLibPointF>();
                if ((incomingPoints == null) || (incomingPoints.Length == 0))
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
            
            return bb;
        }

        
        List<PreviewShape> previewShapes;

        public Pattern(ref QuiltContext context, List<PatternElement> p)
        {
            quiltContext = context;
            pInit(p);
        }

        void pInit(List<PatternElement> p)
        {
            patternElements = new List<PatternElement>();
            foreach (PatternElement t in p)
            {
                patternElements.Add(new PatternElement(t));
            }
        }

        private Pattern(Pattern source)
        {
            quiltContext = source.quiltContext;
            pInit(source.patternElements);
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
        
        List<GeoLibPointF> pBBDims(int index)
        {
            PreviewShape pShape1 = new PreviewShape(this, index);
            BoundingBox bb = new BoundingBox(new List<PreviewShape> { pShape1 });

            return bb.getPoints();
        }

        enum bbDims { width, height }

        double pBBDimension(bbDims prop, int index)
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

        decimal pDoX(int i)
        {
            decimal x_ = 0m;

            int xRef = pGetRef(i, PatternElement.properties_i.xPosRef);

            // We have a relative positioning situation.
            // Note that this could be cascading so we need to walk the stack
            if (xRef >= 0)
            {
                int sRef = pGetPatternElement(i).getInt(PatternElement.properties_i.xPosSubShapeRef);

                int sPosRef = pGetPatternElement(i).getInt(PatternElement.properties_i.xPosSubShapeRefPos);

                bool refFlipped = pGetPatternElement(xRef).getInt(PatternElement.properties_i.flipH) == 1;
                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.xPos);

                // In the case of an array shape type for the reference, we may still have subshape relative references. To check this, we need to query for an array. If we don't have an array, we're good.
                // Then we also need to query whether the subshape reference is a higher value than the number of subshapes in the shape; the 'array' subshape reference is last in the list.
                bool doX = !pGetPatternElement(xRef).isXArray() || (pGetPatternElement(xRef).isXArray() && (sRef < pGetPatternElement(xRef).getSubShapeCount()));

                int aXRef = xRef;
                // Is the array reference a relative array?
                if (pGetPatternElement(xRef).getInt(PatternElement.properties_i.arrayRef) != 0)
                {
                    aXRef = pGetPatternElement(xRef).getInt(PatternElement.properties_i.arrayRef) - 1;

                    int PSSR = pGetPatternElement(i).getInt(PatternElement.properties_i.xPosSubShapeRef);
                    int PSSC = pGetPatternElement(xRef).getSubShapeCount();
                    if (PSSR == PSSC)
                    {
                        doX = false;
                    }
                }

                if (doX)
                {
                    if (sRef == 1)
                    {
                        x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 0);
                        if (pGetPatternElement(xRef).getInt(PatternElement.properties_i.shapeIndex) != (int)CommonVars.shapeNames.Sshape)
                        {
                            if ((pGetPatternElement(xRef).getInt(PatternElement.properties_i.shapeIndex) != (int)CommonVars.shapeNames.Ushape) && (pGetPatternElement(xRef).getInt(PatternElement.properties_i.shapeIndex) != (int)CommonVars.shapeNames.Xshape))
                            {
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 0);
                            }
                            else
                            {
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 1);
                            }
                        }
                    }
                    if (sRef == 2)
                    {
                        x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 0);
                        x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 2);
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
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 0);
                                break;
                            case 1:
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 1);
                                break;
                            case 2:
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 2);
                                break;
                        }
                    }
                    else
                    {
                        x_ += Convert.ToDecimal(pBBDimension(bbDims.width, aXRef));
                    }
                }

                if (sPosRef == (int)CommonVars.subShapeHorLocs.M)
                {
                    if (doX)
                    {
                        switch (sRef)
                        {
                            case 0:
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 0) / 2;
                                break;
                            case 1:
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 1) / 2;
                                break;
                            case 2:
                                x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 2) / 2;
                                break;
                        }
                    }
                    else
                    {
                        x_ += Convert.ToDecimal(pBBDimension(bbDims.width, aXRef)) / 2;
                    }
                }

                // Is the reference flipped? If so, we need to flip our value to get the correct result
                if (refFlipped)
                {
                    ShapeLibrary t = new ShapeLibrary(pGetPatternElement(xRef));
                    GeoLibPointF pivot = t.getPivotPoint();

                    bool alignX = pGetPatternElement(xRef).getInt(PatternElement.properties_i.alignX) == 1;
                    bool alignY = pGetPatternElement(xRef).getInt(PatternElement.properties_i.alignY) == 1;
                    x_ = Convert.ToDecimal(GeoWrangler.flip(true, false, alignX, alignY, pivot, new[] { new GeoLibPointF(Convert.ToDouble(x_), 0) })[0].X);
                }

                if (doX)
                {
                    switch (sRef)
                    {
                        case 0:
                            x_ += pGetPatternElement(i).getDecimal(PatternElement.properties_decimal.horOffset, 0);
                            break;
                        case 1:
                            x_ += pGetPatternElement(i).getDecimal(PatternElement.properties_decimal.horOffset, 1);
                            break;
                        case 2:
                            x_ += pGetPatternElement(i).getDecimal(PatternElement.properties_decimal.horOffset, 2);
                            break;
                    }
                }

                // xRef = pGetRef(xRef, PatternElement.properties_i.xPosRef);
            }

            return x_;
        }

        decimal pDoY(int i)
        {
            decimal y_ = 0m;

            int yRef = pGetRef(i, PatternElement.properties_i.yPosRef);

            // We have a relative positioning situation.
            // Note that this could be cascading so we need to walk the stack

            if (yRef >= 0)
            {
                int sRef = pGetPatternElement(i).getInt(PatternElement.properties_i.yPosSubShapeRef);

                int sPosRef = pGetPatternElement(i).getInt(PatternElement.properties_i.yPosSubShapeRefPos);

                bool refFlipped = pGetPatternElement(yRef).getInt(PatternElement.properties_i.flipV) == 1;

                y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.yPos);

                // In the case of an array shape type for the reference, we may still have subshape relative references. To check this, we need to query for an array. If we don't have an array, we're good.
                // Then we also need to query whether the subshape reference is a higher value than the number of subshapes in the shape; the 'array' subshape reference is last in the list.
                bool doY = !pGetPatternElement(yRef).isYArray() || (pGetPatternElement(yRef).isYArray() && (sRef < pGetPatternElement(yRef).getSubShapeCount()));

                int aYRef = yRef;

                // Is the array reference a relative array?
                if (pGetPatternElement(yRef).getInt(PatternElement.properties_i.arrayRef) != 0)
                {
                    aYRef = pGetPatternElement(yRef).getInt(PatternElement.properties_i.arrayRef) - 1;

                    int PSSR = pGetPatternElement(i).getInt(PatternElement.properties_i.yPosSubShapeRef);
                    int PSSC = pGetPatternElement(yRef).getSubShapeCount();
                    if (PSSR == PSSC)
                    {
                        doY = false;
                    }
                }

                if (doY)
                {
                    y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verOffset, 0);
                    if (sRef == 1)
                    {
                        y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verOffset, 1);
                    }
                    if (sRef == 2)
                    {
                        y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 0);
                        y_ -= pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 2);
                        y_ -= pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verOffset, 2);
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
                                y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 0);
                                break;
                            case 1:
                                y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 1);
                                break;
                            case 2:
                                y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 2);
                                break;
                        }
                    }
                    else
                    {
                        y_ += Convert.ToDecimal(pBBDimension(bbDims.height, aYRef));
                    }
                }

                if (sPosRef == (int)CommonVars.subShapeVerLocs.M)
                {
                    if (doY)
                    {
                        switch (sRef)
                        {
                            case 0:
                                y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 0) / 2;
                                break;
                            case 1:
                                y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 1) / 2;
                                break;
                            case 2:
                                y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 2) / 2;
                                break;
                        }
                    }
                    else
                    {
                        y_ += Convert.ToDecimal(pBBDimension(bbDims.height, aYRef)) / 2;
                    }
                }

                // Is the reference flipped? If so, we need to flip our value to get the correct result
                if (refFlipped)
                {
                    ShapeLibrary t = new ShapeLibrary(pGetPatternElement(yRef));
                    GeoLibPointF pivot = t.getPivotPoint();

                    bool alignX = pGetPatternElement(yRef).getInt(PatternElement.properties_i.alignX) == 1;
                    bool alignY = pGetPatternElement(yRef).getInt(PatternElement.properties_i.alignY) == 1;
                    y_ = Convert.ToDecimal(GeoWrangler.flip(false, true, alignX, alignY, pivot, new[] { new GeoLibPointF(0, Convert.ToDouble(y_)) })[0].Y);
                }

                if (doY)
                {
                    switch (sRef)
                    {
                        case 0:
                            y_ += pGetPatternElement(i).getDecimal(PatternElement.properties_decimal.verOffset, 0);
                            break;
                        case 1:
                            y_ += pGetPatternElement(i).getDecimal(PatternElement.properties_decimal.verOffset, 1);
                            break;
                        case 2:
                            break;
                    }
                }

                // yRef = pGetRef(yRef, PatternElement.properties_i.yPosRef);
            }

            return y_;

        }

        void pArrangeElements()
        {
            // We need to apply the relative transforms.
            // We need to keep these separate until we're done, to avoid trouble.
            decimal[,] positions = new decimal[patternElements.Count, 2];
#if !QUILTSINGLETHREADED
            Parallel.For(0, patternElements.Count, (i) =>
#else
            for (int i = 0; i < patternElements.Count; i++)
#endif
            {
                decimal x_ = pDoX(i);
                decimal y_ = pDoY(i);

                positions[i, 0] = x_;
                positions[i, 1] = y_;
            }
#if !QUILTSINGLETHREADED
            );
#endif

            // Set our positions
#if !QUILTSINGLETHREADED
            Parallel.For(0, patternElements.Count, (i) =>
#else
            for (int i = 0; i < patternElements.Count; i++)
#endif
            {
                decimal x1_ = pGetPatternElement(i).getDecimal(PatternElement.properties_decimal.xPos) + positions[i, 0];
                int xRef = pGetRef(i, PatternElement.properties_i.xPosRef);

                if (xRef >= 0)
                {
                    bool xCyclical = pCyclicalCheck(xRef, PatternElement.properties_i.xPosRef);

                    if (!xCyclical)
                    {
                        while (xRef >= 0)
                        {
                            x1_ += positions[xRef, 0];
                            xRef = pGetRef(xRef, PatternElement.properties_i.xPosRef);
                        }
                    }
                }

                decimal y1_ = pGetPatternElement(i).getDecimal(PatternElement.properties_decimal.yPos) + positions[i, 1];
                int yRef = pGetRef(i, PatternElement.properties_i.yPosRef);

                if (yRef >= 0)
                {
                    bool yCyclical = pCyclicalCheck(yRef, PatternElement.properties_i.yPosRef);

                    if (!yCyclical)
                    {
                        while (yRef >= 0)
                        {
                            y1_ += positions[yRef, 1];
                            yRef = pGetRef(yRef, PatternElement.properties_i.yPosRef);
                        }
                    }
                }
                pGetPatternElement(i).setDecimal(PatternElement.properties_decimal.xPos, x1_);
                pGetPatternElement(i).setDecimal(PatternElement.properties_decimal.yPos, y1_);
            }
#if !QUILTSINGLETHREADED
            );
#endif
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
                ret = pGetPatternElement(e).equivalence(pattern.getPatternElement(e));

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
            if (index >= patternElements.Count)
            {
                throw new Exception("Exceeded length of array!");
            }
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
                pBBEvaluation();
            }
            return previewShapes;
        }

        void pBBEvaluation()
        {
            // Get the BB and shoe-horn it into a shape.
            BoundingBox bb = new BoundingBox(previewShapes);

            foreach (int t1 in bbShapes)
            {
                // This is an ugly hack because of the positional reference for the settings.
                Pattern tPattern = new Pattern(this);

                // Use our bounding box shape as the basis for the temporary pattern element; this ensures we capture rotation, flip, etc. values.
                PatternElement t = new PatternElement(pGetPatternElement(t1));
                t.setInt(PatternElement.properties_i.shapeIndex, (int)CommonVars.shapeNames.rect);

                decimal leftMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingLeft);

                decimal rightMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingRight);

                decimal bottomMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingBottom);

                decimal topMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingTop);

                decimal width = (decimal)(bb.maxX - bb.minX) + leftMargin + rightMargin;

                decimal height = (decimal)(bb.maxY - bb.minY) + bottomMargin + topMargin;

                decimal xPos = (decimal)(bb.minX) - leftMargin;
                decimal xOffset = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.xPos);
                t.setDecimal(PatternElement.properties_decimal.xPos, xPos + xOffset);

                decimal yPos = (decimal)(bb.minY) - bottomMargin;
                decimal yOffset = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.yPos);
                t.setDecimal(PatternElement.properties_decimal.yPos, yPos + yOffset);

                t.setDecimal(PatternElement.properties_decimal.horLength, width, 0);

                t.setDecimal(PatternElement.properties_decimal.verLength, height, 0);

                tPattern.patternElements[t1] = t;

                PreviewShape tShape = new PreviewShape(tPattern, t1);
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
            string[] element_descriptions = new string[patternElements.Count];
#if !QUILTSINGLETHREADED
            Parallel.For(0, patternElements.Count, (p, loopstate ) =>
#else
            for (int p = 0; p < patternElements.Count; p++)
#endif
            {
                element_descriptions[p] = p + ";" + pGetPatternElement(p).getDescription();
            }
#if !QUILTSINGLETHREADED
);
#endif
            return string.Join(';', element_descriptions);
        }
    }
}
