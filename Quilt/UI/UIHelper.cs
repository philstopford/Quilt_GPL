using System.Threading.Tasks;
using Clipper2Lib;
using color;
using Eto.Drawing;

namespace Quilt;

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

    private static PointF myPointFToPointF(PointD sourcePoint)
    {
        return new PointF((float)sourcePoint.x, (float)sourcePoint.y);
    }

    private static PointD pointFTomyPointF(PointF sourcePoint)
    {
        return new PointD(sourcePoint.X, sourcePoint.Y);
    }

    public static PointF[] myPointFArrayToPointFArray(PathD sourceArray)
    {
        PointF[] returnArray = new PointF[sourceArray.Count];
#if !QUILTSINGLETHREADED
        Parallel.For(0, returnArray.Length, i =>
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

    public static PathD pointFArrayTomyPointFArray(PointF[] sourceArray)
    {
        PathD returnArray = Helper.initedPathD(sourceArray.Length);
#if !QUILTSINGLETHREADED
        Parallel.For(0, returnArray.Count, i =>
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