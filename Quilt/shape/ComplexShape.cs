using Error;
using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using shapeEngine;

namespace Quilt;

public class ComplexShape
{
    private GeoLibPointF[] points;

    public GeoLibPointF[] getPoints()
    {
        return pGetPoints();
    }

    private GeoLibPointF[] pGetPoints()
    {
        return points;
    }

    // Provided for external access via methods to allow drawn shapes to be moved to same location as enabled shapes.
    private double xOverlayVal;
    private double yOverlayVal;

    public GeoLibPointF getOffset()
    {
        return pGetOffset();
    }

    private GeoLibPointF pGetOffset()
    {
        return new GeoLibPointF(xOverlayVal, yOverlayVal);
    }

    public ComplexShape(List<PatternElement> patternElements, int settingsIndex, ShapeLibrary shape = null)
    {
        pMakeEntropyShape(patternElements, settingsIndex, shape);
    }

    private List<GeoLibPointF> pMakeShape(bool returnEarly, ShapeLibrary shape, double hTipBias, double vTipBias)
    {
        List<GeoLibPointF> mcPoints = new();

        if (shape.shapeIndex == (int)ShapeLibrary.shapeNames_all.complex)
        {
            foreach (var t in shape.Vertex)
            {
                mcPoints.Add(new GeoLibPointF(t.X, t.Y));
            }
        }
        else
        {
            // Build our shape.
            // This seems like overkill, but we're consistent with Variance by taking this approach, which helps.
            const double globalBias_Sides = 0;
            const double globalBias_Tips = 0;
            
            const double vTipBiasType = 0;
            const double vTipBiasNegVar = 0;
            const double vTipBiasPosVar = 0;

            const double hTipBiasType = 0;
            const double hTipBiasNegVar = 0;
            const double hTipBiasPosVar = 0;

            shape.computeTips(globalBias_Tips, hTipBias, hTipBiasType, hTipBiasNegVar, hTipBiasPosVar, vTipBias,
                vTipBiasType, vTipBiasNegVar, vTipBiasPosVar);

            shape.computeBias(globalBias_Sides);

            shape.biasCorners();

            // Return early, with no rounding.
            if (returnEarly)
            {
                mcPoints.Clear();
                mcPoints.AddRange(shape.Vertex.Select(t => new GeoLibPointF(t.X, t.Y)));
                mcPoints = GeoWrangler.stripColinear(mcPoints);

                return mcPoints;
            }

            double iCR = 0;
            double oCR = 0;
            double iCV = 0;
            double oCV = 0;
            double iCVariation = 0;
            double oCVariation = 0;
            int cornerSegments = 90;
            int optimizeCorners = 1;
            double resolution = 0.01;
            bool icPA = false;
            bool ocPA = false;
            double s0HO = 0;
            double s0VO = 0;

            mcPoints = shape.processCorners(false, false, false, s0HO, s0VO, iCR, iCV, iCVariation, icPA, oCR, oCV,
                oCVariation, ocPA, cornerSegments, optimizeCorners, resolution,
                CentralProperties.scaleFactorForOperation);
        }

        return GeoWrangler.close(GeoWrangler.stripColinear(mcPoints));
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
        
        const bool returnEarly = false; //debug

        // Force an early return from make shape, avoiding rounding.
        List<GeoLibPointF> mcPoints = pMakeShape(true, shape, Convert.ToDouble(patternElements[settingsIndex].getDecimal(ShapeSettings.properties_decimal.hTBias)), Convert.ToDouble(patternElements[settingsIndex].getDecimal(ShapeSettings.properties_decimal.vTBias)));
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (returnEarly)
        {
            points = mcPoints.ToArray();
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
            mcPoints.Add(new GeoLibPointF(0.0f, 0.0f));
        }

        points = mcPoints.ToArray();

        if (Math.Abs(points[0].X - points[^1].X) > double.Epsilon || Math.Abs(points[0].Y - points[^1].Y) > double.Epsilon)
        {
            ErrorReporter.showMessage_OK("Start and end not the same - entropyShape", "Oops");
        }
    }
}