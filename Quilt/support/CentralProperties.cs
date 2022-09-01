using shapeEngine;

namespace Quilt;

public static class CentralProperties
{
    public const string productName = "Quilt";
    public const string author = "Phil Stopford";
    public const string version = "4.0";

    public const int scaleFactorForOperation = 1000;
    public const int timer_interval = 100;

    public enum shapeNames
    {
        none,
        rect,
        Lshape,
        Tshape,
        Xshape,
        Ushape,
        Sshape,
        text,
        bounding,
        complex
    }

    public static int[] shapeTable = new[] {
        (int)ShapeLibrary.shapeNames_all.none,
        (int)ShapeLibrary.shapeNames_all.rect,
        (int)ShapeLibrary.shapeNames_all.Lshape,
        (int)ShapeLibrary.shapeNames_all.Tshape,
        (int)ShapeLibrary.shapeNames_all.Xshape,
        (int)ShapeLibrary.shapeNames_all.Ushape,
        (int)ShapeLibrary.shapeNames_all.Sshape,
        (int)ShapeLibrary.shapeNames_all.text,
        (int)ShapeLibrary.shapeNames_all.bounding,
        (int)ShapeLibrary.shapeNames_all.complex
    };
    
}