using geoLib;
using geoWrangler;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quilt
{
    public class ShapeLibrary
    {
        Int32 shapeIndex;
        public Boolean shapeValid { get; set; }
        public MyVertex[] Vertex { get; set; }
        public MyRound[] round1 { get; set; }
        public Boolean[] tips { get; set; }
        PatternElement patternelement;

        public ShapeLibrary(PatternElement patternElement_)
        {
            init(patternElement_);
        }

        public ShapeLibrary(Int32 shapeIndex, PatternElement patternElement_)
        {
            init(shapeIndex, patternElement_);
        }

        void init(PatternElement patternElement_)
        {
            shapeValid = false;
            init(patternElement_.getInt(PatternElement.properties_i.shapeIndex), patternElement_);
        }

        void init(Int32 shapeIndex, PatternElement patternElement_)
        {
            this.shapeIndex = shapeIndex;
            shapeValid = false;
            patternelement = patternElement_;
            setShape(shapeIndex);
        }

        public GeoLibPointF getPivotPoint()
        {
            return pGetPivotPoint();
        }

        GeoLibPointF pGetPivotPoint()
        {
            int limit = Vertex.Length - 1;
            GeoLibPointF[] t = new GeoLibPointF[limit]; // closed shape, we don't need the final point
#if SLPARALLEL
            Parallel.For(0, limit, (i) =>
#else
            for (int i = 0; i < t.Length; i++)
#endif
            {
                t[i] = new GeoLibPointF(Vertex[i].X, Vertex[i].Y);
            }
#if SLPARALLEL
            );
#endif
            GeoLibPointF pivot = GeoWrangler.midPoint(t);

            return pivot;
        }

        // 2 point array, bottom-left, top-right
        // This is just the shape bounding box - nothing more.
        public static GeoLibPointF[] getBoundingBox(PatternElement pe)
        {
            return pGetBoundingBox(pe);
        }

        static GeoLibPointF[] pGetBoundingBox(PatternElement pe)
        {
            ShapeLibrary tmp = new ShapeLibrary(pe);

            double minX = 0;
            double maxX = 0;

            double minY = 0;
            double maxY = 0;

            // Last vertex is not defined.
            try
            {
                minX = tmp.Vertex.Take(tmp.Vertex.Length - 1).Min(p => p.X);
                maxX = tmp.Vertex.Take(tmp.Vertex.Length - 1).Max(p => p.X);

                minY = tmp.Vertex.Take(tmp.Vertex.Length - 1).Min(p => p.Y);
                maxY = tmp.Vertex.Take(tmp.Vertex.Length - 1).Max(p => p.Y);
            }
            catch (Exception)
            {

            }

            return new GeoLibPointF[] { new GeoLibPointF(minX, minY), new GeoLibPointF(maxX, maxY) };
        }

        public void setShape(Int32 shapeIndex, GeoLibPointF[] sourcePoly = null)
        {
            pSetShape(shapeIndex, sourcePoly);
        }

        void pSetShape(Int32 shapeIndex, GeoLibPointF[] sourcePoly = null)
        {
            try
            {
                this.shapeIndex = shapeIndex;
                switch (shapeIndex)
                {
                    case (Int32)CommonVars.shapeNames.rect:
                    case (Int32)CommonVars.shapeNames.text:
                    case (Int32)CommonVars.shapeNames.bounding:
                        rectangle();
                        break;
                    case (Int32)CommonVars.shapeNames.Lshape:
                        Lshape();
                        break;
                    case (Int32)CommonVars.shapeNames.Tshape:
                        Tshape();
                        break;
                    case (Int32)CommonVars.shapeNames.Xshape:
                        crossShape();
                        break;
                    case (Int32)CommonVars.shapeNames.Ushape:
                        Ushape();
                        break;
                    case (Int32)CommonVars.shapeNames.Sshape:
                        Sshape();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
        }

        public static Int32 getSubShapeCount(int index)
        {
            return pGetSubShapeCount(index);
        }

        static Int32 pGetSubShapeCount(int index)
        {
            switch (index)
            {
                case (Int32)CommonVars.shapeNames.Lshape:
                case (Int32)CommonVars.shapeNames.Tshape:
                case (Int32)CommonVars.shapeNames.Xshape:
                case (Int32)CommonVars.shapeNames.Ushape:
                    return 2;
                case (Int32)CommonVars.shapeNames.Sshape:
                    return 3;
                default:
                    return 1;
            }
        }

        void configureArrays()
        {
            double vertexCount = 0;
            switch (shapeIndex)
            {
                case (Int32)CommonVars.shapeNames.rect: // rectangle
                case (Int32)CommonVars.shapeNames.text:
                case (Int32)CommonVars.shapeNames.bounding:
                    vertexCount = 9;
                    break;
                case (Int32)CommonVars.shapeNames.Lshape: // L
                    vertexCount = 13;
                    break;
                case (Int32)CommonVars.shapeNames.Tshape: // T
                    vertexCount = 17;
                    break;
                case (Int32)CommonVars.shapeNames.Xshape: // Cross
                    vertexCount = 25;
                    break;
                case (Int32)CommonVars.shapeNames.Ushape: // U
                    vertexCount = 17;
                    break;
                case (Int32)CommonVars.shapeNames.Sshape: // S
                    vertexCount = 25;
                    break;
                default:
                    break;
            }
            int limit = (Int32)vertexCount;
            Vertex = new MyVertex[limit];
            tips = new Boolean[limit];
            Parallel.For(0, limit, (i) =>
            // for (Int32 i = 0; i < limit; i++)
            {
                tips[i] = false;
            });
            limit = (Int32)Math.Floor(vertexCount / 2) + 1;
            round1 = new MyRound[limit];
#if SLPARALLEL
            Parallel.For(0, limit, (i) =>
#else
            for (Int32 i = 0; i < limit; i++)
#endif
            {
                round1[i] = new MyRound();
            }
#if SLPARALLEL
            );
#endif
        }

        void rectangle()
        {
            configureArrays();

            // Sort out the tips by setting 'true' to center vertex that defines tip in each case.
            switch (patternelement.getInt(PatternElement.properties_i.shape0Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[1] = true;
                    tips[8] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[1] = true;
                    tips[8] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[3] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[3] = true;
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[1] = true;
                    tips[3] = true;
                    tips[8] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[3] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[1] = true;
                    tips[3] = true;
                    tips[5] = true;
                    tips[8] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[1] = true;
                    tips[7] = true;
                    tips[8] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[5] = true;
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[1] = true;
                    tips[5] = true;
                    tips[7] = true;
                    tips[8] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[3] = true;
                    tips[7] = true;
                    tips[1] = true;
                    tips[8] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[3] = true;
                    tips[7] = true;
                    tips[5] = true;
                    break;
                default: // All
                    tips[3] = true;
                    tips[7] = true;
                    tips[1] = true;
                    tips[8] = true;
                    tips[5] = true;
                    break;
            }

            // NOTE:
            // Subshape offsets are applied later to simplify ellipse generation
            // Horizontal and vertical global offsets are applied in callsite.
            // Repositioning with respect to subshape reference is also applied in callsite.

            double lowerX = 0.0;
            double lowerY = 0.0;
            double width = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            double height = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength));

            double tmpX = lowerX;
            double tmpY = lowerY;

            Vertex[0] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += 0.5 * height;
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = height;
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += 0.5 * width;
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = lowerX + width;
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= 0.5 * height;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = lowerY;
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX -= 0.5 * width;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            processEdgesForRounding();

            shapeValid = true;
        }

        void Tshape()
        {
            configureArrays();
            // Sort out the tips by setting 'true' to center vertex that defines tip in each case.
            switch (patternelement.getInt(PatternElement.properties_i.shape0Tip))
            {
                case (int)CommonVars.tipLocations.none: // None
                    break;
                case (int)CommonVars.tipLocations.L: // Left
                    tips[1] = true;
                    break;
                case (int)CommonVars.tipLocations.R: // Right
                    tips[5] = true;
                    tips[13] = true;
                    break;
                case (int)CommonVars.tipLocations.LR: // Left and right
                    tips[1] = true;
                    tips[5] = true;
                    tips[13] = true;
                    break;
                case (int)CommonVars.tipLocations.T: // Top
                    tips[3] = true;
                    break;
                case (int)CommonVars.tipLocations.B: // Bottom
                    tips[15] = true;
                    break;
                case (int)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[3] = true;
                    tips[15] = true;
                    break;
                case (int)CommonVars.tipLocations.TL: // Top and Left
                    tips[3] = true;
                    tips[1] = true;
                    break;
                case (int)CommonVars.tipLocations.TR: // Top and Right
                    tips[3] = true;
                    tips[5] = true;
                    tips[13] = true;
                    break;
                case (int)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[1] = true;
                    tips[3] = true;
                    tips[5] = true;
                    tips[13] = true;
                    break;
                case (int)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[15] = true;
                    tips[1] = true;
                    break;
                case (int)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[15] = true;
                    tips[5] = true;
                    tips[13] = true;
                    break;
                case (int)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[1] = true;
                    tips[15] = true;
                    tips[5] = true;
                    tips[13] = true;
                    break;
                case (int)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[1] = true;
                    tips[3] = true;
                    tips[15] = true;
                    break;
                case (int)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[3] = true;
                    tips[5] = true;
                    tips[13] = true;
                    tips[15] = true;
                    break;
                default: // All
                    tips[1] = true;
                    tips[3] = true;
                    tips[5] = true;
                    tips[13] = true;
                    tips[15] = true;
                    break;
            }
            switch (patternelement.getInt(PatternElement.properties_i.shape1Tip))
            {
                case (int)CommonVars.tipLocations.none: // None
                    break;
                case (int)CommonVars.tipLocations.L: // Left
                    break;
                case (int)CommonVars.tipLocations.R: // Right
                    tips[9] = true;
                    break;
                case (int)CommonVars.tipLocations.LR: // Left and right
                    tips[9] = true;
                    break;
                case (int)CommonVars.tipLocations.T: // Top
                    tips[7] = true;
                    break;
                case (int)CommonVars.tipLocations.B: // Bottom
                    tips[11] = true;
                    break;
                case (int)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[7] = true;
                    tips[11] = true;
                    break;
                case (int)CommonVars.tipLocations.TL: // Top and Left
                    tips[7] = true;
                    break;
                case (int)CommonVars.tipLocations.TR: // Top and Right
                    tips[7] = true;
                    tips[9] = true;
                    break;
                case (int)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[7] = true;
                    tips[9] = true;
                    break;
                case (int)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[11] = true;
                    break;
                case (int)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[11] = true;
                    tips[9] = true;
                    break;
                case (int)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[11] = true;
                    tips[9] = true;
                    break;
                case (int)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[7] = true;
                    tips[11] = true;
                    break;
                case (int)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[7] = true;
                    tips[9] = true;
                    tips[11] = true;
                    break;
                default: // All
                    tips[7] = true;
                    tips[9] = true;
                    tips[11] = true;
                    break;
            }

            // NOTE:
            // Subshape offsets are applied later to simplify ellipse generation
            // Horizontal and vertical global offsets are applied in callsite.

            double tmpX = 0.0;
            double tmpY = 0.0;
            Vertex[0] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2);
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) +
                (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerOffset))))) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset));
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength) / 2);
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength) / 2);
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength) / 2);
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength) / 2);
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength) / 2);
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength) / 2);
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset)) / 2;
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = 0;
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength) / 2);
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            processEdgesForRounding();

            shapeValid = true;
        }

        void Lshape()
        {
            configureArrays();
            // Sort out the tips by setting 'true' to center vertex that defines tip in each case.
            switch (patternelement.getInt(PatternElement.properties_i.shape0Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[1] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[1] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[3] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[11] = true;
                    tips[3] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[1] = true;
                    tips[3] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[3] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[1] = true;
                    tips[3] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[11] = true;
                    tips[1] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[11] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[1] = true;
                    tips[5] = true;
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[1] = true;
                    tips[11] = true;
                    tips[3] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[11] = true;
                    tips[3] = true;
                    tips[5] = true;
                    break;
                default: // All
                    tips[1] = true;
                    tips[5] = true;
                    tips[11] = true;
                    tips[3] = true;
                    break;
            }
            switch (patternelement.getInt(PatternElement.properties_i.shape1Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                                                       // this.tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                                                        // this.tips[5] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[7] = true;
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[7] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[7] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[11] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[11] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[7] = true;
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[7] = true;
                    tips[11] = true;
                    tips[9] = true;
                    break;
                default: // All
                    tips[9] = true;
                    tips[7] = true;
                    tips[11] = true;
                    break;
            }

            // NOTE:
            // Subshape offsets are applied later to simplify ellipse generation
            // Horizontal and vertical global offsets are applied in callsite.
            // Repositioning with respect to subshape reference is also applied in callsite.
            double tmpX = 0.0;
            double tmpY = 0.0;
            Vertex[0] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) / 2);
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2);
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength))) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength))) / 2;
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) / 2;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength))) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            processEdgesForRounding();

            shapeValid = true;
        }

        void Ushape()
        {
            configureArrays();
            // Sort out the tips by setting 'true' to center vertex that defines tip in each case.
            switch (patternelement.getInt(PatternElement.properties_i.shape0Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[1] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[13] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[1] = true;
                    tips[13] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[3] = true;
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[3] = true;
                    tips[11] = true;
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[1] = true;
                    tips[3] = true;
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[3] = true;
                    tips[11] = true;
                    tips[13] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[1] = true;
                    tips[3] = true;
                    tips[11] = true;
                    tips[13] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[1] = true;
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[13] = true;
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[1] = true;
                    tips[13] = true;
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[1] = true;
                    tips[3] = true;
                    tips[11] = true;
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[3] = true;
                    tips[11] = true;
                    tips[13] = true;
                    tips[15] = true;
                    break;
                default: // All
                    tips[1] = true;
                    tips[3] = true;
                    tips[11] = true;
                    tips[13] = true;
                    tips[15] = true;
                    break;
            }
            switch (patternelement.getInt(PatternElement.properties_i.shape1Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[5] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                                                       // this.tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[7] = true;
                    // this.tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[5] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[5] = true;
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[7] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[5] = true;
                    tips[7] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[7] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[7] = true;
                    tips[9] = true;
                    break;
                default: // All
                    tips[5] = true;
                    tips[7] = true;
                    tips[9] = true;
                    break;
            }

            // NOTE:
            // Subshape offsets are applied later to simplify ellipse generation
            // Horizontal and vertical global offsets are applied in callsite.
            // Repositioning with respect to subshape reference is also applied in callsite.
            double tmpX = 0.0;
            double tmpY = 0.0;
            Vertex[0] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorOffset)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorOffset)) / 2);
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) / 2;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength));
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorOffset)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)))) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = tmpY / 2;
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = 0;
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = tmpX / 2;
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            processEdgesForRounding();

            shapeValid = true;
        }

        void crossShape()
        {
            configureArrays();
            // Sort out the tips.
            switch (patternelement.getInt(PatternElement.properties_i.shape0Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[1] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[1] = true;
                    tips[9] = true;
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[11] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[1] = true;
                    tips[9] = true;
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[11] = true;
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[1] = true;
                    tips[9] = true;
                    tips[11] = true;
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[1] = true;
                    tips[9] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[13] = true;
                    tips[21] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[1] = true;
                    tips[9] = true;
                    tips[13] = true;
                    tips[21] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[11] = true;
                    tips[1] = true;
                    tips[9] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[13] = true;
                    tips[11] = true;
                    tips[23] = true;
                    tips[21] = true;
                    break;
                default: // All
                    tips[1] = true;
                    tips[9] = true;
                    tips[13] = true;
                    tips[21] = true;
                    tips[11] = true;
                    tips[23] = true;
                    break;
            }
            switch (patternelement.getInt(PatternElement.properties_i.shape1Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[5] = true;
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[7] = true;
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[3] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[3] = true;
                    tips[7] = true;
                    tips[15] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[5] = true;
                    tips[7] = true;
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[7] = true;
                    tips[15] = true;
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[5] = true;
                    tips[7] = true;
                    tips[15] = true;
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[3] = true;
                    tips[5] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[3] = true;
                    tips[17] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[3] = true;
                    tips[5] = true;
                    tips[17] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[5] = true;
                    tips[3] = true;
                    tips[7] = true;
                    tips[15] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[3] = true;
                    tips[7] = true;
                    tips[15] = true;
                    tips[17] = true;
                    tips[19] = true;
                    break;
                default: // All
                    tips[5] = true;
                    tips[17] = true;
                    tips[3] = true;
                    tips[7] = true;
                    tips[15] = true;
                    tips[19] = true;
                    break;
            }

            // NOTE:
            // Subshape offsets are applied later to simplify ellipse generation
            // Horizontal and vertical global offsets are applied in callsite.

            double tmpX = 0.0;
            double tmpY = 0.0;
            Vertex[0] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorOffset));
            tmpX = tmpX / 2;
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = tmpX * 2;
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2);
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2);
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = tmpX / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = 0.0;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset))) / 2);
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength));
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset))) / 2);
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength));
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            // Need midpoint of edge
            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            tmpX += ((Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength))) / 2) / 2; // midpoint
            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorOffset));
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength));
            // tmpX += Convert.ToDouble(layerSettings.getDecimal(PatternElement.properties_decimal.s0HorLength)); // full distance
            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorOffset));
            Vertex[16] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[17] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[18] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            // Need midpoint of edge
            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            tmpX += ((Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength))) / 2) / 2; // midpoint
            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorOffset));
            Vertex[19] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            Vertex[20] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset)) / 2);
            Vertex[21] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = 0.0;
            Vertex[22] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2);
            Vertex[23] = new MyVertex(tmpX, tmpY, typeDirection.down1, true, false, typeVertex.center);

            processEdgesForRounding();

            shapeValid = true;
        }

        void Sshape()
        {
            configureArrays();
            // Sort out the tips by setting 'true' to center vertex that defines tip in each case.
            switch (patternelement.getInt(PatternElement.properties_i.shape0Tip))
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[1] = true;
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[1] = true;
                    tips[9] = true;
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[11] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[1] = true;
                    tips[9] = true;
                    tips[11] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[11] = true;
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[1] = true;
                    tips[9] = true;
                    tips[11] = true;
                    tips[13] = true;
                    tips[21] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[1] = true;
                    tips[9] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[13] = true;
                    tips[21] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[1] = true;
                    tips[9] = true;
                    tips[13] = true;
                    tips[21] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[1] = true;
                    tips[9] = true;
                    tips[11] = true;
                    tips[23] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[11] = true;
                    tips[13] = true;
                    tips[21] = true;
                    tips[23] = true;
                    break;
                default: // All
                    tips[1] = true;
                    tips[9] = true;
                    tips[11] = true;
                    tips[13] = true;
                    tips[21] = true;
                    tips[23] = true;
                    break;
            }
            switch (patternelement.getInt(PatternElement.properties_i.shape1Tip)) // Bottom notch
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[3] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[3] = true;
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[3] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[3] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[3] = true;
                    tips[5] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[5] = true;
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[5] = true;
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[3] = true;
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[3] = true;
                    tips[5] = true;
                    tips[7] = true;
                    break;
                default: // All
                    tips[3] = true;
                    tips[5] = true;
                    tips[7] = true;
                    break;
            }
            switch (patternelement.getInt(PatternElement.properties_i.shape2Tip)) // Top notch
            {
                case (Int32)CommonVars.tipLocations.none: // None
                    break;
                case (Int32)CommonVars.tipLocations.L: // Left
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.T: // Top
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[15] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TL: // Top and Left
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TR: // Top and Right
                    tips[17] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TLR: // Top and Left and Right
                    tips[17] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BL: // Bottom and Left
                    tips[15] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BR: // Bottom and Right
                    tips[15] = true;
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.BLR: // Bottom and Left and Right
                    tips[15] = true;
                    tips[17] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBL: // Top and Bottom and Left
                    tips[17] = true;
                    tips[19] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TBR: // Top and Bottom and Right
                    tips[15] = true;
                    tips[17] = true;
                    tips[19] = true;
                    break;
                default: // All
                    tips[15] = true;
                    tips[17] = true;
                    tips[19] = true;
                    break;
            }

            // NOTE:
            // Subshape offsets are applied later to simplify ellipse generation
            // Horizontal and vertical global offsets are applied in callsite.
            // Repositioning with respect to subshape reference is also applied in callsite.
            double tmpX = 0.0;
            double tmpY = 0.0;
            Vertex[0] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength));
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength)) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerOffset)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1VerLength));
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s1HorLength)) / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = 0;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - tmpY) / 2;
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength));
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);
            // Center so no rounding definition

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = tmpY - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2VerOffset)) / 2);
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);
            // Center so no rounding definition

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2VerOffset));
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = tmpX - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2HorLength) / 2);
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2HorOffset));
            Vertex[16] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2VerLength) / 2);
            Vertex[17] = new MyVertex(tmpX, tmpY, typeDirection.right1, false, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0VerLength)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2VerLength)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2VerOffset));
            Vertex[18] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s2HorLength) / 2);
            Vertex[19] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength));
            Vertex[20] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY /= 2;
            Vertex[21] = new MyVertex(tmpX, tmpY, typeDirection.right1, false, false, typeVertex.center);

            tmpY = 0;
            Vertex[22] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.s0HorLength)) / 2;
            Vertex[23] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            processEdgesForRounding();

            shapeValid = true;
        }

        void processEdgesForRounding()
        {
            int horEdge = Vertex.Length - 2; // deal with padding.
            int verEdge = 1;
            for (int r = 0; r < round1.Length - 1; r++)
            {
                try
                {
                    round1[r].index = r * 2;
                    round1[r].horFace = horEdge;
                    round1[r].verFace = verEdge;

                    // Figure out our corner type. First is a special case.
                    if (r == 0)
                    {
                        round1[r].direction = typeRound.exter;
                        round1[r].MaxRadius = 0; // Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.oCR));
                    }
                    else
                    {
                        if (
                            (Vertex[round1[r].verFace].direction == typeDirection.right1) && (Vertex[round1[r].horFace].direction == typeDirection.up1) && (horEdge < verEdge) ||
                            (Vertex[round1[r].verFace].direction == typeDirection.left1) && (Vertex[round1[r].horFace].direction == typeDirection.up1) && (horEdge > verEdge) ||
                            (Vertex[round1[r].verFace].direction == typeDirection.right1) && (Vertex[round1[r].horFace].direction == typeDirection.down1) && (horEdge > verEdge) ||
                            (Vertex[round1[r].verFace].direction == typeDirection.left1) && (Vertex[round1[r].horFace].direction == typeDirection.down1) && (horEdge < verEdge)
                           )
                        {
                            round1[r].direction = typeRound.exter;
                            round1[r].MaxRadius = 0; // Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.oCR));
                        }
                        else
                        {
                            round1[r].direction = typeRound.inner;
                            round1[r].MaxRadius = 0; // Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.iCR));
                        }
                    }

                    // Small fudge for the 0 case
                    if (r == 0)
                    {
                        horEdge = -1;
                    }

                    // Change our edge configuration for the next loop. We need to handle overflow references as well
                    if (r % 2 == 0)
                    {
                        horEdge += 4;
                        horEdge = horEdge % Vertex.Length;
                    }
                    else
                    {
                        verEdge += 4;
                        verEdge = verEdge % Vertex.Length;
                    }
                }
                catch (Exception e)
                {
                    string t = e.ToString();
                }
            }

            // First and last are the same.
            round1[round1.Length - 1] = round1[0];
        }
    }
}
