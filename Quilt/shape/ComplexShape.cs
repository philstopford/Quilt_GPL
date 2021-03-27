using Error;
using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quilt
{
    public class ComplexShape
    {
        GeoLibPointF[] points;

        public GeoLibPointF[] getPoints()
        {
            return pGetPoints();
        }

        GeoLibPointF[] pGetPoints()
        {
            return points;
        }

        // Provided for external access via methods to allow drawn shapes to be moved to same location as enabled shapes.
        double xOverlayVal;
        double yOverlayVal;

        public GeoLibPointF getOffset()
        {
            return pGetOffset();
        }

        GeoLibPointF pGetOffset()
        {
            return new GeoLibPointF(xOverlayVal, yOverlayVal);
        }

        MyVertex[] Vertex; // container for our extended point properties information.
        MyRound[] round1; // container for the rounding data.

        public ComplexShape(List<PatternElement> patternElements, Int32 settingsIndex, ShapeLibrary shape = null)
        {
            makeEntropyShape(patternElements, settingsIndex, shape);
        }

        List<GeoLibPointF> makeShape(bool returnEarly, ShapeLibrary shape)
        {
            List<GeoLibPointF> mcPoints = new List<GeoLibPointF>(); // overall points container. We'll use this to populate and send back our Point array later. Ints only...

            Vertex = shape.Vertex;

            // If we have a complex shape, we cannot infer anything from it, so just take the geometry as-is and ship it out.
            if (shape.shapeIndex == (int)CommonVars.shapeNames.complex)
            {
                mcPoints.Clear();
                for (Int32 i = 0; i < Vertex.Count(); i++)
                {
                    if (Vertex[i] != null)
                    {
                        mcPoints.Add(new GeoLibPointF(Vertex[i].X, Vertex[i].Y));
                    }
                }
                return GeoWrangler.close(mcPoints);
            }

            round1 = shape.round1;

            // Iterate the corners to apply the bias from the edges.
#if QUILTTHREADED
            Parallel.For(0, round1.Count(), (corner) =>
#else
            for (Int32 corner = 0; corner < round1.Count(); corner++)
#endif
            {
                Vertex[round1[corner].index].X = Vertex[round1[corner].verFace].X;
                Vertex[round1[corner].index].Y = Vertex[round1[corner].horFace].Y;
            }
#if QUILTTHREADED
            );
#endif
            Vertex[Vertex.Count() - 1] = Vertex[0]; // close the shape.
            round1[round1.Count() - 1] = round1[0];

            // Set the midpoints of the edges to the average between the two corners
            for (Int32 corner = 0; corner < round1.Count(); corner++)
            {
                double currentEdgeLength = Math.Abs(
                    GeoWrangler.distanceBetweenPoints(new GeoLibPointF(Vertex[round1[corner].index].X, Vertex[round1[corner].index].Y),
                                                    new GeoLibPointF(Vertex[round1[(corner + 1) % (round1.Count() - 1)].index].X, Vertex[round1[(corner + 1) % (round1.Count() - 1)].index].Y))
                );

                double offset = 0.5f * currentEdgeLength;

                if (corner % 2 == 0)
                {
                    // Get our associated vertical edge Y position
                    double yPoint1 = 0.0;
                    double yPoint2 = Vertex[round1[(corner + 1) % (round1.Count() - 1)].horFace].Y;
                    if (corner == 0)
                    {
                        // Need to wrap around for bias look-up
                        yPoint1 = Vertex[round1[round1.Count() - 1].horFace].Y;
                    }
                    else
                    {
                        yPoint1 = Vertex[round1[corner].horFace].Y;
                    }

                    if (yPoint1 < yPoint2)
                    {
                        Vertex[round1[corner].verFace].Y = yPoint2 - offset;
                    }
                    else
                    {
                        Vertex[round1[corner].verFace].Y = yPoint2 + offset;
                    }
                }
                else
                {
                    // Tweak horizontal edge
                    double xPoint1 = Vertex[round1[corner].verFace].X;
                    double xPoint2 = 0.0;
                    xPoint2 = Vertex[round1[(corner + 1) % (round1.Count() - 1)].verFace].X;

                    if (xPoint1 < xPoint2)
                    {
                        Vertex[round1[corner].horFace].X = xPoint2 - offset;
                    }
                    else
                    {
                        Vertex[round1[corner].horFace].X = xPoint2 + offset;
                    }
                }
            }

            if (returnEarly)
            {
                mcPoints.Clear();
                for (Int32 i = 0; i < Vertex.Count(); i++)
                {
                    mcPoints.Add(new GeoLibPointF(Vertex[i].X, Vertex[i].Y));
                }
                return mcPoints;
            }

            List<GeoLibPointF> mcHorEdgePoints = new List<GeoLibPointF>(); // corner coordinates list, used as a temporary container for each iteration
            List<List<GeoLibPointF>> mcHorEdgePointsList = new List<List<GeoLibPointF>>(); // Hold our lists of doubles for each corner in the shape, in order. We cast these to Ints in the mcPoints list.

            // OK. We need to walk the corner and associated edge for each case.

            for (Int32 round = 0; round < round1.Count() - 1; round++)
            {
                // Derive our basic coordinates for the three vertices on the edge.
                double start_x = Vertex[round1[round].index].X;
                double start_y = Vertex[round1[round].index].Y;
                double currentHorEdge_mid_x = Vertex[round1[round].horFace].X;
                double end_x = Vertex[round1[round + 1].index].X;
                double end_y = Vertex[round1[round + 1].index].Y;

                // Test whether we have a vertical edge or not. We only process horizontal edges to avoid doubling up
                if (start_y == end_y)
                {
                    mcHorEdgePoints.Add(new GeoLibPointF(start_x, start_y));

                    // Add our midpoint.
                    mcHorEdgePoints.Add(new GeoLibPointF(currentHorEdge_mid_x, start_y));

                    // Add our midpoint.
                    mcHorEdgePoints.Add(new GeoLibPointF(end_x, start_y));

                    mcHorEdgePointsList.Add(mcHorEdgePoints.ToList()); // make a deep copy of the points.
                    mcHorEdgePoints.Clear(); // clear our list of points to use on the next pass.
                }
            }

            // Now we have our corners, let's process the vertical edges. We need the corners in order to get our start/end on each vertical edge.
            for (int edge = 0; edge < mcHorEdgePointsList.Count(); edge++)
            {
                mcPoints.AddRange(mcHorEdgePointsList[edge]);
                double y = 0;
                if (edge < mcHorEdgePointsList.Count - 1)
                {
                    y = mcHorEdgePointsList[edge][2].Y + ((mcHorEdgePointsList[edge + 1][0].Y - mcHorEdgePointsList[edge][2].Y) / 2);
                }
                else
                {
                    y = mcHorEdgePointsList[edge][2].Y + ((mcHorEdgePointsList[0][0].Y - mcHorEdgePointsList[edge][2].Y) / 2);
                }
                mcPoints.Add(new GeoLibPointF(mcHorEdgePointsList[edge][2].X, y));
            }

            // Add trailing point.
            mcPoints.Add(new GeoLibPointF(mcPoints[0]));

            return mcPoints;
        }

        void makeEntropyShape(List<PatternElement> patternElements, Int32 settingsIndex, ShapeLibrary shape = null)
        {
            bool returnEarly = false;

            xOverlayVal = 0.0f;
            yOverlayVal = 0.0f;

            if (shape == null)
            {
                shape = new ShapeLibrary(patternElements[settingsIndex]);
                shape.setShape(patternElements[settingsIndex].getInt(PatternElement.properties_i.shapeIndex));
            }

            List<GeoLibPointF> mcPoints = makeShape(returnEarly, shape);
            if (returnEarly)
            {
                points = mcPoints.ToArray();
                return;
            }

            // Get offset value.

            double xOffset = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.s0HorOffset));
            double yOffset = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.s0VerOffset));

            // Sort out our overlay values.

            xOverlayVal = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.xPos));
            yOverlayVal = Convert.ToDouble(patternElements[settingsIndex].getDecimal(PatternElement.properties_decimal.yPos));

            mcPoints = GeoWrangler.move(mcPoints, xOverlayVal + xOffset, yOverlayVal + yOffset);

            // Error handling (failSafe) for no points or no subshape  - safety measure.
            if (mcPoints.Count() == 0)
            {
                mcPoints.Add(new GeoLibPointF(0.0f, 0.0f));
            }

            points = mcPoints.ToArray();

            if ((points[0].X != points[points.Count() - 1].X) || (points[0].Y != points[points.Count() - 1].Y))
            {
                ErrorReporter.showMessage_OK("Start and end not the same - entropyShape", "Oops");
            }
        }
    }
}
