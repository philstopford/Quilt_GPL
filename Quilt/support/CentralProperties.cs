using System;

namespace Quilt
{
    public static class CentralProperties
    {
        public const string productName = "Quilt";
        public const string author = "Phil Stopford";
        public const string version = "2.2.4";
        public enum typeShapes { none, rectangle, L, T, X, U, S, text, bounding };

        public const Int32 scaleFactorForOperation = 1000;
        public const Int32 timer_interval = 100;
    }
}
