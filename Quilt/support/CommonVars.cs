using color;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Quilt
{
    public class CommonVars
    {
        public Stitcher stitcher;

        public Storage storage { get; set; }

        QuiltContext quiltContext;

        public string projectFileName { get; set; }

        public string titleText = CentralProperties.productName + " " + CentralProperties.version;

        string[] shapesShortNames = new string[] { "NONE", "RECT", "L", "T", "X", "U", "S", "Text", "Bounding" };
        public enum shapeNames { none, rect, Lshape, Tshape, Xshape, Ushape, Sshape, text, bounding };

        List<string> availableShapes;
        public ObservableCollection<string> subshapes { get; set; }
        public ObservableCollection<string> xPosRefSubShapeList { get; set; }
        public ObservableCollection<string> yPosRefSubShapeList { get; set; }
        
        List<string> availableSubShapePositions;
        List<string> availableHorShapePositions = new List<string>() { "Left", "Middle", "Right" };
        List<string> availableVerShapePositions = new List<string>() { "Bottom", "Middle", "Top" };
        public enum subShapeLocations { TL, TR, BL, BR, TS, RS, BS, LS, C };
        public enum subShapeHorLocs { L, M, R };
        public enum subShapeVerLocs { B, M, T };

        public List<string> openGLModeList { get; set; }

        // Retaining this, to allow use of Variance components, but the tips will not be used in Quilt.
        public enum tipLocations { none, L, R, LR, T, B, TB, TL, TR, TLR, BL, BR, BLR, TBL, TBR, all };

        public CommonVars(ref QuiltContext context)
        {
            init(ref context);
        }

        void init(ref QuiltContext context)
        {
            quiltContext = context;
            availableShapes = new List<string>() { "(None)", "Rectangle/Square", "L-shape", "T-shape", "X-shape", "U-shape", "S-shape", "Text", "Bounding" };
            availableSubShapePositions = new List<string>() { "Corner: Top Left", "Corner: Top Right", "Corner: Bottom Left", "Corner: Bottom Right",
                                                             "Middle: Top Side", "Middle: Right Side", "Middle: Bottom Side", "Middle: Left Side",
                                                             "Center"};
            colors = new Colors();
            openGLModeList = new List<string>() { "VBO", "Immediate" };

            subshapes = new ObservableCollection<string>() { "1" };
            xPosRefSubShapeList = new ObservableCollection<string>() { "1" };
            yPosRefSubShapeList = new ObservableCollection<string>() { "1" };
            stitcher = new Stitcher(ref quiltContext);

            titleText += " (" + quiltContext.licenceName + ")";
            storage = new Storage();

            reset();
        }

        public void reset()
        {
            pReset();
        }

        void pReset()
        {
            projectFileName = "";

            subshapes.Clear();
            subshapes.Add("1");
            xPosRefSubShapeList.Clear();
            xPosRefSubShapeList.Add("1");
            yPosRefSubShapeList.Clear();
            yPosRefSubShapeList.Add("1");
            stitcher.reset();
        }

        public List<string> getAvailableHorShapePositions()
        {
            return availableHorShapePositions;
        }

        public List<string> getAvailableVerShapePositions()
        {
            return availableVerShapePositions;
        }

        public List<string> getAvailableShapePositions()
        {
            return pGetAvailableShapePositions();
        }

        List<string> pGetAvailableShapePositions()
        {
            return availableSubShapePositions;
        }

        public List<string> getAvailableShapes()
        {
            return pGetAvailableShapes();
        }

        List<string> pGetAvailableShapes()
        {
            return availableShapes;
        }

        public enum gl_i { zoom }

        Int32 openGLZoomFactor;

        Int32 pGetGLInt(gl_i i)
        {
            int ret = 0;
            switch (i)
            {
                case gl_i.zoom:
                    ret = openGLZoomFactor;
                    break;
            }
            return ret;
        }

        public void setGLInt(gl_i i, Int32 val)
        {
            pSetGLInt(i, val);
        }

        void pSetGLInt(gl_i i, Int32 val)
        {
            switch (i)
            {
                case gl_i.zoom:
                    openGLZoomFactor = val;
                    break;
            }
        }

        public enum opacity_gl { fg, bg }
        double openGLFGOpacity;
        double openGLBGOpacity;

        public double getOpacity(opacity_gl o)
        {
            return pGetOpacity(o);
        }

        double pGetOpacity(opacity_gl o)
        {
            double ret = 0;
            switch (o)
            {
                case opacity_gl.fg:
                    ret = openGLFGOpacity;
                    break;
                case opacity_gl.bg:
                    ret = openGLBGOpacity;
                    break;
            }

            return ret;
        }

        public void setOpacity(opacity_gl o, double val)
        {
            pSetOpacity(o, val);
        }

        void pSetOpacity(opacity_gl o, double val)
        {
            switch (o)
            {
                case opacity_gl.fg:
                    openGLFGOpacity = val;
                    break;
                case opacity_gl.bg:
                    openGLBGOpacity = val;
                    break;
            }
        }

        public enum properties_gl { aa, fill, points }

        bool openGLAA;
        bool openGLFilledPolygons;
        bool openGLPoints;

        public void setOpenGLProp(properties_gl p, bool val)
        {
            pSetOpenGLProp(p, val);
        }

        void pSetOpenGLProp(properties_gl p, bool val)
        {
            switch (p)
            {
                case properties_gl.aa:
                    openGLAA = val;
                    break;
                case properties_gl.fill:
                    openGLFilledPolygons = val;
                    break;
                case properties_gl.points:
                    openGLPoints = val;
                    break;
            }
        }

        bool pGetOpenGLProp(properties_gl p)
        {
            bool ret = false;
            switch (p)
            {
                case properties_gl.aa:
                    ret = openGLAA;
                    break;
                case properties_gl.fill:
                    ret = openGLFilledPolygons;
                    break;
                case properties_gl.points:
                    ret = openGLPoints;
                    break;
            }

            return ret;
        }

        Colors colors;
        public Colors getColors()
        {
            return pGetColors();
        }

        Colors pGetColors()
        {
            return colors;
        }

        public void setColors(Colors source)
        {
            pSetColors(source);
        }

        void pSetColors(Colors source)
        {
            colors = source;
        }
    }
}
