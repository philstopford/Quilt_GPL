using System.Threading.Tasks;
using color;
using Eto.Drawing;
using geoLib;

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

        private static PointF myPointFToPointF(GeoLibPointF sourcePoint)
        {
            return new PointF((float)sourcePoint.X, (float)sourcePoint.Y);
        }

        private static GeoLibPointF pointFTomyPointF(PointF sourcePoint)
        {
            return new GeoLibPointF(sourcePoint.X, sourcePoint.Y);
        }

        public static PointF[] myPointFArrayToPointFArray(GeoLibPointF[] sourceArray)
        {
            PointF[] returnArray = new PointF[sourceArray.Length];
#if !QUILTSINGLETHREADED
            Parallel.For(0, returnArray.Length, (i) =>
#else
            for (int i = 0; i < returnArray.Length; i++)
#endif
            {
                returnArray[i] = myPointFToPointF(sourceArray[i]);
            }
#if !QUILTSINGLETHREADED
            );
#endif
            return returnArray;
        }

        public static GeoLibPointF[] pointFArrayTomyPointFArray(PointF[] sourceArray)
        {
            GeoLibPointF[] returnArray = new GeoLibPointF[sourceArray.Length];
#if !QUILTSINGLETHREADED
            Parallel.For(0, returnArray.Length, (i) =>
#else
            for (int i = 0; i < returnArray.Length; i++)
#endif
            {
                returnArray[i] = pointFTomyPointF(sourceArray[i]);
            }
#if !QUILTSINGLETHREADED
            );
#endif
            return returnArray;
        }
    }
}
