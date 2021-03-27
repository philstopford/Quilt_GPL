using System;

namespace Quilt
{
    public static class CentralProperties
    {
        public const string productName = "Quilt";
        public const string author = "Phil Stopford";
        public const string version = "3.1";
        public enum typeShapes { none, rectangle, L, T, X, U, S, text, bounding, complex };

        public const Int32 scaleFactorForOperation = 1000;
        public const Int32 timer_interval = 100;
    }
}
