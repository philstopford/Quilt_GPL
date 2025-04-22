using Error;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using Clipper2Lib;
using shapeEngine;

namespace Quilt;

public class ComplexShape
{
    private PathD points;

    public PathD getPoints()
    {
        return pGetPoints();
    }

    private PathD pGetPoints()
    {
        return points;
    }

    // Provided for external access via methods to allow drawn shapes to be moved to same location as enabled shapes.
    private double xOverlayVal;
    private double yOverlayVal;

    public PointD getOffset()
    {
        return pGetOffset();
    }

    private PointD pGetOffset()
    {
        return new PointD(xOverlayVal, yOverlayVal);
    }

    public ComplexShape(List<PatternElement> patternElements, int settingsIndex, ShapeLibrary shape = null)
    {
        pMakeEntropyShape(patternElements, settingsIndex, shape);
    }

    private PathD pMakeShape(bool returnEarly, ShapeLibrary shape, double hTipBias, double vTipBias)
    {
        PathD mcPoints = new();

        if (shape.shapeIndex == (int)ShapeLibrary.shapeNames_all.complex)
        {
            foreach (var t in shape.Vertex)
            {
                mcPoints.Add(new (t.X, t.Y));
            }
        }
        else
        {
            // Return early, with no rounding.
            if (returnEarly)
            {
                mcPoints.Clear();
                mcPoints.AddRange(shape.Vertex.Select(t => new PointD (t.X, t.Y)));
                mcPoints = GeoWrangler.stripCollinear(mcPoints);

                return mcPoints;
            }

            int cornerSegments = 90;
            int optimizeCorners = 1;
            double resolution = 0.01;

            mcPoints = shape.processCorners(false, false, cornerSegments, optimizeCorners, resolution);
        }

        return GeoWrangler.close(GeoWrangler.stripCollinear(mcPoints));
    }

    private void pMakeEntropyShape(List<PatternElement> patternElements, int settingsIndex, ShapeLibrary shape = null)
    {
        xOverlayVal = 0.0f;
        yOverlayVal = 0.0f;

        if (shape == null)
        {
            shape = new ShapeLibrary(CentralProperties.shapeTable, patternElements[settingsIndex]);
            shape.setShape(patternElements[settingsIndex].getInt(PatternElement.properties_i.shapeIndex));
        }
        shape.computeCage();
        
        const bool returnEarly = false; //debug

        // Force an early return from make shape, avoiding rounding.
        PathD mcPoints = pMakeShape(true, shape, Convert.ToDouble(patternElements[settingsIndex].getDecimal(ShapeSettings.properties_decimal.hTBias)), Convert.ToDouble(patternElements[settingsIndex].getDecimal(ShapeSettings.properties_decimal.vTBias)));
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (returnEarly)
        {
            points = new(mcPoints);
            return;
        }

        // Get offset value.
        double xOffset = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.horOffset, 0));
        double yOffset = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.verOffset, 0));

        // Offset by position values.
        xOverlayVal = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.xPos));
        yOverlayVal = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.yPos));

        mcPoints = GeoWrangler.move(mcPoints, xOverlayVal + xOffset, yOverlayVal + yOffset);

        // Error handling (failSafe) for no points or no subshape  - safety measure.
        if (!mcPoints.Any())
        {
            mcPoints.Add(new (0.0f, 0.0f));
        }

        points = new (mcPoints);

        if (Math.Abs(points[0].x - points[^1].x) > Constants.tolerance || Math.Abs(points[0].y - points[^1].y) > Constants.tolerance)
        {
            ErrorReporter.showMessage_OK("Start and end not the same - entropyShape", "Oops");
        }
    }
}