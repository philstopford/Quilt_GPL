using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clipper2Lib;
using shapeEngine;

namespace Quilt;

public partial class Pattern
{
    public override bool Equals(object obj)
    {
        return equivalence((Pattern)obj);
    }

    private QuiltContext quiltContext;
    private List<PatternElement> patternElements;
    private double x, y;

    private bool bbEvaluationNeeded;
    private List<int> bbShapes;

    public override int GetHashCode()
    {
        return patternElements.Aggregate(0, (current, t) => current ^ t.GetHashCode());
    }

    // To get the bounding box for our list of preview shapes
    public class BoundingBox
    {
        private PathD points;
        private PointD midPoint;
            
        public double minX { get; private set; }
        public double maxX { get; private set; }
        public double minY { get; private set; }
        public double maxY { get; private set; }
            
        public BoundingBox(List<PreviewShape> previewShapes)
        {
            pBoundingBox(previewShapes);
        }

        private void pBoundingBox(List<PreviewShape> previewShapes)
        {
            if (previewShapes == null || previewShapes.Count == 0)
            {
                minX = 0;
                maxX = 0;
                minY = 0;
                maxY = 0;
                points = new () { new(0, 0), new(0, 0), new(0, 0), new(0, 0) };
                midPoint = new (0, 0);
                return;
            }

            PathsD pPoints = previewShapes[0].getPoints();

            if (pPoints.Count == 0)
            {
                minX = 0;
                maxX = 0;
                minY = 0;
                maxY = 0;
                points = new () { new(0, 0), new(0, 0), new(0, 0), new(0, 0) };
                midPoint = new (0, 0);
                return;
            }

            BoundingBox test = pPoints[0].Any() ? new BoundingBox(pPoints[0]) : new BoundingBox(new PathD() { new (0, 0), new (0, 0), new (0, 0), new (0, 0) });

            points = new (test.points);

            foreach (PathD t1 in previewShapes.Select(t => t.getPoints()).SelectMany(polys => polys))
            {
                test = new BoundingBox(t1);

                minX = test.points.Min(p => p.x);
                if (minX < points[0].x)
                {
                    // Reposition our min X points
                    points[0] = new (minX, points[0].y);
                    points[1] = new (minX, points[1].y);
                }
                minY = test.points.Min(p => p.y);
                if (minY < points[0].y)
                {
                    // Reposition our min Y points
                    points[0] = new (points[0].x, minY);
                    points[3] = new (points[3].x, minY);
                }
                maxX = test.points.Max(p => p.x);
                if (maxX > points[2].x)
                {
                    // Reposition our max X points
                    points[2] = new (maxX, points[2].y);
                    points[3] = new (maxX, points[3].y);
                }
                maxY = test.points.Max(p => p.y);
                if (!(maxY > points[2].y))
                {
                    continue;
                }

                // Reposition our max Y points
                points[1] = new (points[1].x, maxY);
                points[2] = new (points[2].x, maxY);
            }
            minX = points.Min(p => p.x);
            minY = points.Min(p => p.y);
            maxX = points.Max(p => p.x);
            maxY = points.Max(p => p.y);
            midPoint = new (minX + (maxX - minX) / 2.0f, minY + (maxY - minY) / 2.0f);
        }

        public PathD getPoints()
        {
            return pGetPoints();
        }

        private PathD pGetPoints()
        {
            return points;
        }

        public PointD getMidPoint()
        {
            return pGetMidPoint();
        }

        private PointD pGetMidPoint()
        {
            return midPoint;
        }

        private BoundingBox(PathD incomingPoints)
        {
            pBoundingBox(incomingPoints);
        }

        private void pBoundingBox(PathD incomingPoints)
        {
            points = new ();
            if (incomingPoints == null || incomingPoints.Count == 0)
            {
                minX = 0;
                maxX = 0;
                minY = 0;
                maxY = 0;
                points.Add(new (0.0f, 0.0f));
                points.Add(new (0.0f, 0.0f));
                points.Add(new (0.0f, 0.0f));
                points.Add(new (0.0f, 0.0f));
                midPoint = new (0.0f, 0.0f);
            }
            else
            {
                // Compile a list of our points.
                PathD iPoints = new (incomingPoints);
                minX = iPoints.Min(p => p.x);
                minY = iPoints.Min(p => p.y);
                maxX = iPoints.Max(p => p.x);
                maxY = iPoints.Max(p => p.y);
                points.Add(new (minX, minY));
                points.Add(new (minX, maxY));
                points.Add(new (maxX, maxY));
                points.Add(new (maxX, minY));
                midPoint = new (minX + (maxX - minX) / 2.0f, minY + (maxY - minY) / 2.0f);
            }
        }
    }

    public BoundingBox boundingBox()
    {
        BoundingBox bb = new(previewShapes);
            
        return bb;
    }


    private List<PreviewShape> previewShapes;

    public Pattern(ref QuiltContext context, List<PatternElement> p)
    {
        quiltContext = context;
        pInit(p);
    }

    private void pInit(List<PatternElement> p)
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

    private void pSetPos(double px, double py)
    {
        x = px;
        y = py;
    }

    public PointD getPos()
    {
        return pGetPos();
    }

    private PointD pGetPos()
    {
        return new (x, y);
    }

    private PathD pBBDims(int index)
    {
        PreviewShape pShape1 = new(this, index);
        BoundingBox bb = new(new List<PreviewShape> { pShape1 });

        return bb.getPoints();
    }

    private enum bbDims { width, height }

    private double pBBDimension(bbDims prop, int index)
    {
        double ret = 0;
        PathD bb = pBBDims(index);
        switch (prop)
        {
            case bbDims.width:
                ret = bb.Max(p => p.x) - bb.Min(p => p.x);
                break;
            case bbDims.height:
                ret = bb.Max(p => p.y) - bb.Min(p => p.y);
                break;
        }

        return ret;
    }

    private decimal pDoX(int i)
    {
        decimal x_ = 0m;

        int xRef = pGetRef(i, PatternElement.properties_i.xPosRef);

        // We have a relative positioning situation.
        // Note that this could be cascading so we need to walk the stack
        if (xRef < 0)
        {
            return x_;
        }

        int sRef = pGetPatternElement(i).getInt(PatternElement.properties_i.xPosSubShapeRef);

        int sPosRef = pGetPatternElement(i).getInt(PatternElement.properties_i.xPosSubShapeRefPos);

        bool refFlipped = pGetPatternElement(xRef).getInt(PatternElement.properties_i.flipH) == 1;
        x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.xPos);

        // In the case of an array shape type for the reference, we may still have subshape relative references. To check this, we need to query for an array. If we don't have an array, we're good.
        // Then we also need to query whether the subshape reference is a higher value than the number of subshapes in the shape; the 'array' subshape reference is last in the list.
        bool doX = !pGetPatternElement(xRef).isXArray() || pGetPatternElement(xRef).isXArray() && sRef < pGetPatternElement(xRef).getSubShapeCount();

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
            switch (sRef)
            {
                case 1:
                {
                    x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 0);
                    if (pGetPatternElement(xRef).getInt(PatternElement.properties_i.shapeIndex) != (int)CentralProperties.shapeNames.Sshape)
                    {
                        if (pGetPatternElement(xRef).getInt(PatternElement.properties_i.shapeIndex) != (int)CentralProperties.shapeNames.Ushape && pGetPatternElement(xRef).getInt(PatternElement.properties_i.shapeIndex) != (int)CentralProperties.shapeNames.Xshape)
                        {
                            x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horLength, 0);
                        }
                        else
                        {
                            x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 1);
                        }
                    }

                    break;
                }
                case 2:
                    x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 0);
                    x_ += pGetPatternElement(xRef).getDecimal(PatternElement.properties_decimal.horOffset, 2);
                    break;
            }
        }

        // Process the side case.
        if (!refFlipped && sPosRef == (int)ShapeSettings.subShapeHorLocs.R || refFlipped && sPosRef == (int)ShapeSettings.subShapeHorLocs.L)
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

        if (sPosRef == (int)ShapeSettings.subShapeHorLocs.M)
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
            ShapeLibrary t = new(CentralProperties.shapeTable, pGetPatternElement(xRef));
            PointD pivot = t.getPivotPoint();

            bool alignX = pGetPatternElement(xRef).getInt(PatternElement.properties_i.alignX) == 1;
            bool alignY = pGetPatternElement(xRef).getInt(PatternElement.properties_i.alignY) == 1;
            x_ = Convert.ToDecimal(GeoWrangler.flip(true, false, alignX, alignY, pivot, new() { new (Convert.ToDouble(x_), 0) })[0].x);
        }

        if (!doX)
        {
            return x_;
        }

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

        // xRef = pGetRef(xRef, PatternElement.properties_i.xPosRef);

        return x_;
    }

    private decimal pDoY(int i)
    {
        decimal y_ = 0m;

        int yRef = pGetRef(i, PatternElement.properties_i.yPosRef);

        // We have a relative positioning situation.
        // Note that this could be cascading so we need to walk the stack

        if (yRef < 0)
        {
            return y_;
        }

        int sRef = pGetPatternElement(i).getInt(PatternElement.properties_i.yPosSubShapeRef);

        int sPosRef = pGetPatternElement(i).getInt(PatternElement.properties_i.yPosSubShapeRefPos);

        bool refFlipped = pGetPatternElement(yRef).getInt(PatternElement.properties_i.flipV) == 1;

        y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.yPos);

        // In the case of an array shape type for the reference, we may still have subshape relative references. To check this, we need to query for an array. If we don't have an array, we're good.
        // Then we also need to query whether the subshape reference is a higher value than the number of subshapes in the shape; the 'array' subshape reference is last in the list.
        bool doY = !pGetPatternElement(yRef).isYArray() || pGetPatternElement(yRef).isYArray() && sRef < pGetPatternElement(yRef).getSubShapeCount();

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
            switch (sRef)
            {
                case 1:
                    y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verOffset, 1);
                    break;
                case 2:
                    y_ += pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 0);
                    y_ -= pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verLength, 2);
                    y_ -= pGetPatternElement(yRef).getDecimal(PatternElement.properties_decimal.verOffset, 2);
                    break;
            }
        }

        // Process the side case.
        if (!refFlipped && sPosRef == (int)ShapeSettings.subShapeVerLocs.T || refFlipped && sPosRef == (int)ShapeSettings.subShapeVerLocs.B)
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

        if (sPosRef == (int)ShapeSettings.subShapeVerLocs.M)
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
            ShapeLibrary t = new(CentralProperties.shapeTable, pGetPatternElement(yRef));
            PointD pivot = t.getPivotPoint();

            bool alignX = pGetPatternElement(yRef).getInt(PatternElement.properties_i.alignX) == 1;
            bool alignY = pGetPatternElement(yRef).getInt(PatternElement.properties_i.alignY) == 1;
            y_ = Convert.ToDecimal(GeoWrangler.flip(false, true, alignX, alignY, pivot, new () { new (0, Convert.ToDouble(y_)) })[0].y);
        }

        if (!doY)
        {
            return y_;
        }

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

        // yRef = pGetRef(yRef, PatternElement.properties_i.yPosRef);

        return y_;

    }

    private void pArrangeElements()
    {
        // We need to apply the relative transforms.
        // We need to keep these separate until we're done, to avoid trouble.
        decimal[,] positions = new decimal[patternElements.Count, 2];
#if !QUILTSINGLETHREADED
        Parallel.For(0, patternElements.Count, i =>
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
        Parallel.For(0, patternElements.Count, i =>
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

    private bool pEquivalence(Pattern pattern)
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

    private PatternElement pGetPatternElement(int index)
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

    private List<PatternElement> pGetPatternElements()
    {
        return patternElements;
    }

    // Responsible for generating our preview shapes per the settings.
    // All offsets need to be applied here - previewPanel takes what it is given and draws that.
    public List<PreviewShape> generate_shapes()
    {
        return pGenerate_shapes();
    }

    private PreviewShape pGenerate_shapes(int index)
    {
        PreviewShape pShape1 = new(this, index);
        pShape1.setColor(quiltContext.colors.subshape1_Color);
        return pShape1;
    }

    private List<PreviewShape> pGenerate_shapes()
    {
        bbEvaluationNeeded = false;
        bbShapes = new List<int>();

        previewShapes = new List<PreviewShape>();

        pArrangeElements();

        for (int index = 0; index < patternElements.Count; index++)
        {
            string shapeString = ((ShapeSettings.typeShapes_mode1)pGetPatternElement(index).getInt(PatternElement.properties_i.shapeIndex)).ToString();
            // User has a shape chosen so we can draw a preview, except for bounding which is deferred as the overall bounding box is needed for the pattern.
            if (shapeString != "none" && shapeString != "bounding")
            {
                previewShapes.Add(pGenerate_shapes(index));
            }
            else
            {
                if (shapeString != "bounding")
                {
                    continue;
                }

                bbEvaluationNeeded = true;
                // Need to defer this until we have the extents arranged.
                bbShapes.Add(index);
                // No preview - no shape chosen.
            }
        }

        if (bbEvaluationNeeded)
        {
            pBBEvaluation();
        }
        return previewShapes;
    }

    private void pBBEvaluation()
    {
        // Get the BB and shoe-horn it into a shape.
        BoundingBox bb = new(previewShapes);

        foreach (int t1 in bbShapes)
        {
            // This is an ugly hack because of the positional reference for the settings.
            Pattern tPattern = new(this);

            // Use our bounding box shape as the basis for the temporary pattern element; this ensures we capture rotation, flip, etc. values.
            PatternElement t = new(pGetPatternElement(t1));
            t.setInt(PatternElement.properties_i.shapeIndex, (int)CentralProperties.shapeNames.rect);

            decimal leftMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingLeft);

            decimal rightMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingRight);

            decimal bottomMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingBottom);

            decimal topMargin = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.boundingTop);

            decimal width = (decimal)(bb.maxX - bb.minX) + leftMargin + rightMargin;

            decimal height = (decimal)(bb.maxY - bb.minY) + bottomMargin + topMargin;

            decimal xPos = (decimal)bb.minX - leftMargin;
            decimal xOffset = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.xPos);
            t.setDecimal(PatternElement.properties_decimal.xPos, xPos + xOffset);

            decimal yPos = (decimal)bb.minY - bottomMargin;
            decimal yOffset = pGetPatternElement(t1).getDecimal(PatternElement.properties_decimal.yPos);
            t.setDecimal(PatternElement.properties_decimal.yPos, yPos + yOffset);

            t.setDecimal(PatternElement.properties_decimal.horLength, width, 0);

            t.setDecimal(PatternElement.properties_decimal.verLength, height, 0);

            tPattern.patternElements[t1] = t;

            PreviewShape tShape = new(tPattern, t1);
            tShape.setColor(quiltContext.colors.subshape1_Color);
            previewShapes.Add(tShape);
        }
    }

    public string getDescription()
    {
        return pGetDescription();
    }

    private string pGetDescription()
    {
        string[] element_descriptions = new string[patternElements.Count];
#if !QUILTSINGLETHREADED
        Parallel.For(0, patternElements.Count, (p) =>
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