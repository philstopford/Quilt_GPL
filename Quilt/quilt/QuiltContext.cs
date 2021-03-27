using color;
using System;
using System.IO;
using System.Reflection;
using Veldrid;

namespace Quilt
{
    public class QuiltContext
    {
        // This is intended to hold application context settings to allow for cleaner handling of
        // commandline options related to headless mode, XML file loading from the command line, viewport switches

        public object previewLock;
        public string xmlFileArg;
        public Int32 openGLZoomFactor;
        public double FGOpacity;
        public double BGOpacity;
        public double angularTolerance;
        public bool AA;
        public bool filledPolygons;
        public bool drawPoints;
        public bool drawExtents;
        public bool verticalRectDecomp;
        public Colors colors;
        public Int32 HTCount;
        public GraphicsBackend backend;

        public string licenceName;

        public QuiltContext(string xmlFileArg_, GraphicsBackend _backend)
        {
            makeContext(xmlFileArg_, _backend);
        }

        void makeContext(string xmlFileArg_, GraphicsBackend _backend)
        {
            previewLock = new object();
            xmlFileArg = xmlFileArg_;
            openGLZoomFactor = 1;
            filledPolygons = false;
            drawPoints = false;
            drawExtents = true;
            verticalRectDecomp = true;
            AA = true;
            FGOpacity = 0.7;
            BGOpacity = 0.3;
            angularTolerance = 0.2;
            colors = new Colors();
            backend = _backend;
            licenceName = "internal";
        }
    }
}
