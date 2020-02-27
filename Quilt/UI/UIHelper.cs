using color;
using Eto.Drawing;
using geoLib;
using System.Threading.Tasks;

namespace Quilt
{
    public static class UIHelper
    {
        public static Color myColorToColor(MyColor sourceColor)
        {
            Color returnColor;
            returnColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return returnColor;
        }

        public static MyColor colorToMyColor(Color sourceColor)
        {
            return new MyColor(sourceColor.R, sourceColor.G, sourceColor.B);
        }

        public static PointF myPointFToPointF(GeoLibPointF sourcePoint)
        {
            return new PointF((float)sourcePoint.X, (float)sourcePoint.Y);
        }

        public static GeoLibPointF pointFTomyPointF(PointF sourcePoint)
        {
            return new GeoLibPointF(sourcePoint.X, sourcePoint.Y);
        }

        public static PointF[] myPointFArrayToPointFArray(GeoLibPointF[] sourceArray)
        {
            PointF[] returnArray = new PointF[sourceArray.Length];
#if QUILTMT
            Parallel.For(0, returnArray.Length, (i) =>
#else
            for (int i = 0; i < returnArray.Length; i++)
#endif
            {
                returnArray[i] = myPointFToPointF(sourceArray[i]);
            }
#if QUILTMT
            );
#endif
            return returnArray;
        }

        public static GeoLibPointF[] pointFArrayTomyPointFArray(PointF[] sourceArray)
        {
            GeoLibPointF[] returnArray = new GeoLibPointF[sourceArray.Length];
#if QUILTMT
            Parallel.For(0, returnArray.Length, (i) =>
#else
            for (int i = 0; i < returnArray.Length; i++)
#endif
            {
                returnArray[i] = pointFTomyPointF(sourceArray[i]);
            }
#if QUILTMT
            );
#endif
            return returnArray;
        }
    }
}
