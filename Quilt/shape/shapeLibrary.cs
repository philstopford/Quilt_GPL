using geoLib;
using geoWrangler;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quilt
{
    public class ShapeLibrary
    {
        public Int32 shapeIndex { get; private set; }
        public MyVertex[] Vertex { get; private set; }
        public MyRound[] round1 { get; private set; }
        private Boolean[] tips { get; set; }
        PatternElement patternelement;

        public ShapeLibrary(PatternElement patternElement_)
        {
            pInit(patternElement_);
        }

        public ShapeLibrary(Int32 shapeIndex, PatternElement patternElement_)
        {
            pInit(shapeIndex, patternElement_);
        }

        void pInit(PatternElement patternElement_)
        {
            pInit(patternElement_.getInt(PatternElement.properties_i.shapeIndex), patternElement_);
        }

        void pInit(Int32 shapeIndex_, PatternElement patternElement_)
        {
            shapeIndex = shapeIndex_;
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
#if SHAPELIBTHREADED
            Parallel.For(0, limit, (i) =>
#else
            for (int i = 0; i < t.Length; i++)
#endif
            {
                t[i] = new GeoLibPointF(Vertex[i].X, Vertex[i].Y);
            }
#if SHAPELIBTHREADED
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

            return new [] { new GeoLibPointF(minX, minY), new GeoLibPointF(maxX, maxY) };
        }

        public void setShape(Int32 shapeIndex_, GeoLibPointF[] sourcePoly = null)
        {
            pSetShape(shapeIndex_, sourcePoly);
        }

        void pSetShape(Int32 shapeIndex_, GeoLibPointF[] sourcePoly = null)
        {
            try
            {
                shapeIndex = shapeIndex_;
                switch (shapeIndex)
                {
                    case (Int32)CommonVars.shapeNames.rect:
                    case (Int32)CommonVars.shapeNames.text:
                    case (Int32)CommonVars.shapeNames.bounding:
                        pRectangle();
                        break;
                    case (Int32)CommonVars.shapeNames.Lshape:
                        pLshape();
                        break;
                    case (Int32)CommonVars.shapeNames.Tshape:
                        pTshape();
                        break;
                    case (Int32)CommonVars.shapeNames.Xshape:
                        pXShape();
                        break;
                    case (Int32)CommonVars.shapeNames.Ushape:
                        pUshape();
                        break;
                    case (Int32)CommonVars.shapeNames.Sshape:
                        pSshape();
                        break;
                    case (Int32)CommonVars.shapeNames.complex:
                        pCustomShape(sourcePoly);
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

        void pConfigureArrays()
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
            }
            int limit = (Int32)vertexCount;
            Vertex = new MyVertex[limit];
            tips = new Boolean[limit];
#if SHAPELIBTHREADED
            Parallel.For(0, limit, (i) =>
#else
            for (Int32 i = 0; i < limit; i++)
#endif
            {
                tips[i] = false;
            }
#if SHAPELIBTHREADED
            );
#endif
            limit = (Int32)Math.Floor(vertexCount / 2) + 1;
            round1 = new MyRound[limit];
#if SHAPELIBTHREADED
            Parallel.For(0, limit, (i) =>
#else
            for (Int32 i = 0; i < limit; i++)
#endif
            {
                round1[i] = new MyRound();
            }
#if SHAPELIBTHREADED
            );
#endif
        }

        void pRectangle()
        {
            pConfigureArrays();

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
            double width = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            double height = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0));

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

            pProcessEdgesForRounding();
        }

        void pTshape()
        {
            pConfigureArrays();
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

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2);
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) +
                (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 0))))) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1));
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1) / 2);
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1) / 2);
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1) / 2);
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) / 2;
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = 0;
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0) / 2);
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            pProcessEdgesForRounding();
        }

        void pLshape()
        {
            pConfigureArrays();
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
                    break;
                case (Int32)CommonVars.tipLocations.R: // Right
                    tips[9] = true;
                    break;
                case (Int32)CommonVars.tipLocations.LR: // Left and right
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

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) / 2);
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2);
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1))) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1))) / 2;
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) / 2;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1))) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            pProcessEdgesForRounding();
        }

        void pUshape()
        {
            pConfigureArrays();
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
                    break;
                case (Int32)CommonVars.tipLocations.B: // Bottom
                    tips[7] = true;
                    break;
                case (Int32)CommonVars.tipLocations.TB: // Top and Bottom
                    tips[7] = true;
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

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 1)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 1)) / 2);
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) / 2;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0));
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 1)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)))) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = tmpY / 2;
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = 0;
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = tmpX / 2;
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            pProcessEdgesForRounding();
        }

        void pXShape()
        {
            pConfigureArrays();
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

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 1));
            tmpX = tmpX / 2;
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = tmpX * 2;
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2);
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2);
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = tmpX / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = 0.0;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1))) / 2);
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0));
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1))) / 2);
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1));
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            // Need midpoint of edge
            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            tmpX += ((Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0))) / 2) / 2; // midpoint
            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 1));
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1));
            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 1));
            Vertex[16] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[17] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[18] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            // Need midpoint of edge
            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            tmpX += ((Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0))) / 2) / 2; // midpoint
            tmpX -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 1));
            Vertex[19] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            Vertex[20] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) / 2);
            Vertex[21] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);

            tmpY = 0.0;
            Vertex[22] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2);
            Vertex[23] = new MyVertex(tmpX, tmpY, typeDirection.down1, true, false, typeVertex.center);

            pProcessEdgesForRounding();
        }

        void pSshape()
        {
            pConfigureArrays();
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

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) / 2);
            Vertex[1] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1));
            Vertex[2] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) / 2);
            Vertex[3] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1));
            Vertex[4] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1)) / 2;
            Vertex[5] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 1)) + Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 1));
            Vertex[6] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 1)) / 2;
            Vertex[7] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = 0;
            Vertex[8] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - tmpY) / 2;
            Vertex[9] = new MyVertex(tmpX, tmpY, typeDirection.left1, true, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0));
            Vertex[10] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2;
            Vertex[11] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);
            // Center so no rounding definition

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            Vertex[12] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY = tmpY - (Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 2)) / 2);
            Vertex[13] = new MyVertex(tmpX, tmpY, typeDirection.right1, true, false, typeVertex.center);
            // Center so no rounding definition

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 2));
            Vertex[14] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = tmpX - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 2) / 2);
            Vertex[15] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horOffset, 2));
            Vertex[16] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY -= Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 2) / 2);
            Vertex[17] = new MyVertex(tmpX, tmpY, typeDirection.right1, false, false, typeVertex.center);

            tmpY = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 0)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verLength, 2)) - Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.verOffset, 2));
            Vertex[18] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX += Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 2) / 2);
            Vertex[19] = new MyVertex(tmpX, tmpY, typeDirection.up1, false, false, typeVertex.center);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0));
            Vertex[20] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpY /= 2;
            Vertex[21] = new MyVertex(tmpX, tmpY, typeDirection.right1, false, false, typeVertex.center);

            tmpY = 0;
            Vertex[22] = new MyVertex(tmpX, tmpY, typeDirection.tilt1, true, false, typeVertex.corner);

            tmpX = Convert.ToDouble(patternelement.getDecimal(PatternElement.properties_decimal.horLength, 0)) / 2;
            Vertex[23] = new MyVertex(tmpX, tmpY, typeDirection.down1, false, false, typeVertex.center);

            pProcessEdgesForRounding();
        }

        void pProcessEdgesForRounding()
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
                        round1[r].MaxRadius = 0;
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
                            round1[r].MaxRadius = 0;
                        }
                        else
                        {
                            round1[r].direction = typeRound.inner;
                            round1[r].MaxRadius = 0;
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
                        // ReSharper disable once ConvertToCompoundAssignment
                        horEdge = horEdge % Vertex.Length;
                    }
                    else
                    {
                        verEdge += 4;
                        // ReSharper disable once ConvertToCompoundAssignment
                        verEdge = verEdge % Vertex.Length;
                    }
                }
                catch (Exception)
                {
                }
            }

            // First and last are the same.
            round1[^1] = round1[0];
        }

        // Intended to take geometry from an external source and map it into our shape engine.
        void pCustomShape(GeoLibPointF[] sourcePoly)
        {
            if (sourcePoly == null)
            {
                return;
            }

            // Note that we assume the point order matches our general primitives; might need upstream review to ensure this is being
            // fed correctly.
            // Upstream should trim array to ensure end point is different from start point, but we'll force the issue here for robustness.
            sourcePoly = GeoWrangler.stripTerminators(sourcePoly, true);
            sourcePoly = GeoWrangler.stripColinear(sourcePoly);
            //  Strip the terminator again to meet the requirements below.
            sourcePoly = GeoWrangler.stripTerminators(sourcePoly, false);
            sourcePoly = GeoWrangler.clockwise(sourcePoly);

            // We need to look at our incoming shape to see whether it's orthogonal and suitable for contouring.
            bool geoCoreShapeOrthogonal = GeoWrangler.orthogonal(sourcePoly, angularTolerance: 0);

            if (!geoCoreShapeOrthogonal)
            {
                pCustomShape_nonOrthogonal(sourcePoly);
            }
            else
            {
                pCustomShape_orthogonal(sourcePoly);
            }
        }

        void pCustomShape_nonOrthogonal(GeoLibPointF[] sourcePoly)
        {
            int sCount = sourcePoly.Length;
            Vertex = new MyVertex[sCount + 1]; // add one to close.

            // Assign shape vertices to Vertex and move on. EntropyShape will know what to do.
#if SHAPELIBTHREADED
            Parallel.For(0, sCount, (pt) => 
#else
            for (int pt = 0; pt < sCount; pt++)
#endif
            {
                Vertex[pt] = new MyVertex(sourcePoly[pt].X, sourcePoly[pt].Y, typeDirection.tilt1, false, false, typeVertex.corner);
            }
#if SHAPELIBTHREADED
            );
#endif
            // Close the shape.
            Vertex[^1] = new MyVertex(Vertex[0]);
        }

        void pCustomShape_orthogonal(GeoLibPointF[] sourcePoly)
        {
            int sCount = sourcePoly.Length;
            Int32 vertexCount = (sCount * 2) + 1; // assumes no point in midpoint of edges, and 1 to close.
            Vertex = new MyVertex[vertexCount];
            tips = new Boolean[vertexCount];
            Int32 vertexCounter = 0; // set up our vertex counter.

#if SHAPELIBTHREADED
            Parallel.For(0, vertexCount, (i) =>
#else
            for (Int32 i = 0; i < vertexCount; i++)
#endif
            {
                tips[i] = false;
            }
#if SHAPELIBTHREADED
            );
#endif

            Int32 roundCount = sourcePoly.Length + 1;
            round1 = new MyRound[roundCount];
#if SHAPELIBTHREADED
            Parallel.For(0, roundCount, (i) =>
#else
            for (Int32 i = 0; i < roundCount; i++)
#endif
            {
                round1[i] = new MyRound();
            }
#if SHAPELIBTHREADED
            );
#endif
            // Set up first rounding entry
            round1[0].direction = typeRound.exter;
            round1[0].MaxRadius = 0;
            round1[0].verFace = 1;
            round1[0].horFace = vertexCount - 2;
            round1[^1] = round1[0]; // close the loop

            // Set up first vertex.
            Vertex[0] = new MyVertex(sourcePoly[0].X, sourcePoly[0].Y, typeDirection.tilt1, false, false, typeVertex.corner);
            vertexCounter++;
            // Set up first midpoint.
            Vertex[1] = new MyVertex((sourcePoly[0].X + sourcePoly[1].X) / 2.0f, (sourcePoly[0].Y + sourcePoly[1].Y) / 2.0f, typeDirection.left1, true, false, typeVertex.center);
            vertexCounter++;

            // Also set our end points
            Vertex[vertexCount - 2] = new MyVertex((sourcePoly[0].X + sourcePoly[^1].X) / 2.0f,
                                                  (sourcePoly[0].Y + sourcePoly[^1].Y) / 2.0f, typeDirection.down1, false, false, typeVertex.center);

            // Figure out our rounding characteristics.

            // First edge is always vertical, left facing.
            bool left = true;
            bool up = false;

            for (int pt = 1; pt < roundCount - 1; pt++)
            {
                // Link to our vertical/horizontal edges
                round1[pt].index = vertexCounter;
                if (pt % 2 == 1)
                {
                    round1[pt].verFace = vertexCounter - 1;
                    round1[pt].horFace = vertexCounter + 1;
                }
                else
                {
                    round1[pt].verFace = vertexCounter + 1;
                    round1[pt].horFace = vertexCounter - 1;
                }

                // Register our corner point into the vertex array.
                Vertex[vertexCounter] = new MyVertex(sourcePoly[pt].X, sourcePoly[pt].Y, typeDirection.tilt1, false, false, typeVertex.corner);
                vertexCounter++;

                // Now we have to wrangle the midpoint.

                Int32 next = (pt + 1) % sourcePoly.Length; // wrap to polygon length

                // Find the normal for the edge to the next point.

                double dx = sourcePoly[next].X - sourcePoly[pt].X;
                double dy = sourcePoly[next].Y - sourcePoly[pt].Y;

                // Set up our midpoint for convenience.
                GeoLibPointF midPt = new GeoLibPointF(sourcePoly[pt].X + dx / 2.0f, sourcePoly[pt].Y + dy / 2.0f);

                // The normal, to match convention in the distance calculation is assessed from this point to the next point.

                // Get average angle for this vertex based on angles from line segments.
                // http://stackoverflow.com/questions/1243614/how-do-i-calculate-the-normal-vector-of-a-line-segmen
                GeoLibPointF normalPt = new GeoLibPointF(-dy, dx);

                // Vertical edge has a normal with an X value non-zero and Y value ~0.
                // treating a 0.01 difference as being ~0
                bool vertical = Math.Abs(normalPt.X) > 0.01;

                // Assess the normal to establish direction
                if (vertical)
                {
                    // left facing vertical edge has normal with negative X value.
                    left = normalPt.X < 0;
                }
                else
                {
                    // down facing horizontal edge has normal with negative Y value.
                    up = !(normalPt.Y < 0);
                }

                if (!vertical)
                {
                    if (up)
                    {
                        Vertex[vertexCounter] = new MyVertex(midPt.X, midPt.Y, typeDirection.up1, vertical:false, false, typeVertex.center);
                    }
                    else
                    {
                        Vertex[vertexCounter] = new MyVertex(midPt.X, midPt.Y, typeDirection.down1, vertical:false, false, typeVertex.center);
                    }
                }
                else
                {
                    if (left)
                    {
                        Vertex[vertexCounter] = new MyVertex(midPt.X, midPt.Y, typeDirection.left1, vertical:true, false, typeVertex.center);
                    }
                    else
                    {
                        Vertex[vertexCounter] = new MyVertex(midPt.X, midPt.Y, typeDirection.right1, vertical:true, false, typeVertex.center);
                    }
                }
                vertexCounter++;
            }

            // Reprocess our corners for inner/outer rounding based on horFace/verFace directions
#if SHAPELIBTHREADED
            Parallel.For(0, roundCount, (pt) => 
#else
            for (int pt = 0; pt < roundCount; pt++)
#endif
            {
                bool outerVertex = (pt == 0) || (pt == round1.Length - 1) ||
                                   ((round1[pt].verFace < round1[pt].horFace) &&
                                    ((Vertex[round1[pt].verFace].direction == typeDirection.left1) && (Vertex[round1[pt].horFace].direction == typeDirection.up1))) ||
                                   ((round1[pt].verFace > round1[pt].horFace) &&
                                    ((Vertex[round1[pt].horFace].direction == typeDirection.up1) && (Vertex[round1[pt].verFace].direction == typeDirection.right1))) ||
                                   ((round1[pt].verFace < round1[pt].horFace) &&
                                    ((Vertex[round1[pt].verFace].direction == typeDirection.right1) && (Vertex[round1[pt].horFace].direction == typeDirection.down1))) ||
                                   ((round1[pt].verFace > round1[pt].horFace) &&
                                    ((Vertex[round1[pt].horFace].direction == typeDirection.down1) && (Vertex[round1[pt].verFace].direction == typeDirection.left1)));

                // Only certain changes in direction correspond to an outer vertex, for a clockwise ordered series of points.

                if (outerVertex)
                {
                    round1[pt].direction = typeRound.exter;
                    round1[pt].MaxRadius = 0;
                }
                else
                {
                    round1[pt].direction = typeRound.inner;
                    round1[pt].MaxRadius = 0;
                }

                Vertex[round1[pt].index].inner = !outerVertex;
            }
#if SHAPELIBTHREADED
            );
#endif
        }
    }
}
