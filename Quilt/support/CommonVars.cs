using color;
using geoCoreLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Quilt
{
    public class CommonVars
    {
        public Stitcher stitcher;

        public Storage storage { get; private set; }

        QuiltContext quiltContext;

        public string projectFileName { get; set; }

        public string titleText = CentralProperties.productName + " " + CentralProperties.version;

        public enum shapeNames { none, rect, Lshape, Tshape, Xshape, Ushape, Sshape, text, bounding, complex };

        List<string> availableShapes;
        public ObservableCollection<string> subshapes { get; private set; }
        
        public ObservableCollection<string> minHLRefSubShapeList { get; private set; }
        public ObservableCollection<string> minHLRefSubShape2List { get; private set; }
        public ObservableCollection<string> minHLRefSubShape3List { get; private set; }
        public ObservableCollection<string> minVLRefSubShapeList { get; private set; }
        public ObservableCollection<string> minVLRefSubShape2List { get; private set; }
        public ObservableCollection<string> minVLRefSubShape3List { get; private set; }
        public ObservableCollection<string> minHORefSubShapeList { get; private set; }
        public ObservableCollection<string> minHORefSubShape2List { get; private set; }
        public ObservableCollection<string> minHORefSubShape3List { get; private set; }
        public ObservableCollection<string> minVORefSubShapeList { get; private set; }
        public ObservableCollection<string> minVORefSubShape2List { get; private set; }
        public ObservableCollection<string> minVORefSubShape3List { get; private set; }
        public ObservableCollection<string> minHLIncRefSubShapeList { get; private set; }
        public ObservableCollection<string> minHLIncRefSubShape2List { get; private set; }
        public ObservableCollection<string> minHLIncRefSubShape3List { get; private set; }
        
        public ObservableCollection<string> minVLIncRefSubShapeList { get; private set; }
        public ObservableCollection<string> minVLIncRefSubShape2List { get; private set; }
        public ObservableCollection<string> minVLIncRefSubShape3List { get; private set; }
        public ObservableCollection<string> minHOIncRefSubShapeList { get; private set; }
        public ObservableCollection<string> minHOIncRefSubShape2List { get; private set; }
        public ObservableCollection<string> minHOIncRefSubShape3List { get; private set; }
        public ObservableCollection<string> minVOIncRefSubShapeList { get; private set; }
        public ObservableCollection<string> minVOIncRefSubShape2List { get; private set; }
        public ObservableCollection<string> minVOIncRefSubShape3List { get; private set; }
        public ObservableCollection<string> minHLStepsRefSubShapeList { get; private set; }
        public ObservableCollection<string> minHLStepsRefSubShape2List { get; private set; }
        public ObservableCollection<string> minHLStepsRefSubShape3List { get; private set; }
        public ObservableCollection<string> minVLStepsRefSubShapeList { get; private set; }
        public ObservableCollection<string> minVLStepsRefSubShape2List { get; private set; }
        public ObservableCollection<string> minVLStepsRefSubShape3List { get; private set; }
        public ObservableCollection<string> minHOStepsRefSubShapeList { get; private set; }
        public ObservableCollection<string> minHOStepsRefSubShape2List { get; private set; }
        public ObservableCollection<string> minHOStepsRefSubShape3List { get; private set; }
        public ObservableCollection<string> minVOStepsRefSubShapeList { get; private set; }
        public ObservableCollection<string> minVOStepsRefSubShape2List { get; private set; }
        public ObservableCollection<string> minVOStepsRefSubShape3List { get; private set; }
        public ObservableCollection<string> xPosRefSubShapeList { get; private set; }
        public ObservableCollection<string> yPosRefSubShapeList { get; private set; }

        public ObservableCollection<string> structureList_exp { get; private set; }

        List<string> availableSubShapePositions;
        readonly List<string> availableHorShapePositions = new List<string>() { "Left", "Middle", "Right" };
        readonly List<string> availableVerShapePositions = new List<string>() { "Bottom", "Middle", "Top" };
        public enum subShapeLocations { TL, TR, BL, BR, TS, RS, BS, LS, C };
        public enum subShapeHorLocs { L, M, R };
        public enum subShapeVerLocs { B, M, T };

        public List<string> openGLModeList { get; private set; }

        public GeoCoreHandler gCH;

        // Retaining this, to allow use of Variance components, but the tips will not be used in Quilt.
        public enum tipLocations { none, L, R, LR, T, B, TB, TL, TR, TLR, BL, BR, BLR, TBL, TBR, all };

        public CommonVars(ref QuiltContext context)
        {
            pInit(ref context);
        }

        void pInit(ref QuiltContext context)
        {
            quiltContext = context;
            availableShapes = new List<string>() { "(None)", "Rectangle/Square", "L-shape", "T-shape", "X-shape", "U-shape", "S-shape", "Text", "Bounding", "Layout" };
            availableSubShapePositions = new List<string>() { "Corner: Top Left", "Corner: Top Right", "Corner: Bottom Left", "Corner: Bottom Right",
                                                             "Middle: Top Side", "Middle: Right Side", "Middle: Bottom Side", "Middle: Left Side",
                                                             "Center"};
            colors = new Colors();
            setColors(quiltContext.colors);
            openGLModeList = new List<string>() { "VBO", "Immediate" };

            subshapes = new ObservableCollection<string>() { "1" };
            minHLRefSubShapeList = new ObservableCollection<string>() { "1" };
            minHLRefSubShape2List = new ObservableCollection<string>() { "1" };
            minHLRefSubShape3List = new ObservableCollection<string>() { "1" };
            minVLRefSubShapeList = new ObservableCollection<string>() { "1" };
            minVLRefSubShape2List = new ObservableCollection<string>() { "1" };
            minVLRefSubShape3List = new ObservableCollection<string>() { "1" };
            minHORefSubShapeList = new ObservableCollection<string>() { "1" };
            minHORefSubShape2List = new ObservableCollection<string>() { "1" };
            minHORefSubShape3List = new ObservableCollection<string>() { "1" };
            minVORefSubShapeList = new ObservableCollection<string>() { "1" };
            minVORefSubShape2List = new ObservableCollection<string>() { "1" };
            minVORefSubShape3List = new ObservableCollection<string>() { "1" };
            minHLIncRefSubShapeList = new ObservableCollection<string>() { "1" };
            minHLIncRefSubShape2List = new ObservableCollection<string>() { "1" };
            minHLIncRefSubShape3List = new ObservableCollection<string>() { "1" };
            minVLIncRefSubShapeList = new ObservableCollection<string>() { "1" };
            minVLIncRefSubShape2List = new ObservableCollection<string>() { "1" };
            minVLIncRefSubShape3List = new ObservableCollection<string>() { "1" };
            minHOIncRefSubShapeList = new ObservableCollection<string>() { "1" };
            minHOIncRefSubShape2List = new ObservableCollection<string>() { "1" };
            minHOIncRefSubShape3List = new ObservableCollection<string>() { "1" };
            minVOIncRefSubShapeList = new ObservableCollection<string>() { "1" };
            minVOIncRefSubShape2List = new ObservableCollection<string>() { "1" };
            minVOIncRefSubShape3List = new ObservableCollection<string>() { "1" };
            minHLStepsRefSubShapeList = new ObservableCollection<string>() { "1" };
            minHLStepsRefSubShape2List = new ObservableCollection<string>() { "1" };
            minHLStepsRefSubShape3List = new ObservableCollection<string>() { "1" };
            minVLStepsRefSubShapeList = new ObservableCollection<string>() { "1" };
            minVLStepsRefSubShape2List = new ObservableCollection<string>() { "1" };
            minVLStepsRefSubShape3List = new ObservableCollection<string>() { "1" };
            minHOStepsRefSubShapeList = new ObservableCollection<string>() { "1" };
            minHOStepsRefSubShape2List = new ObservableCollection<string>() { "1" };
            minHOStepsRefSubShape3List = new ObservableCollection<string>() { "1" };
            minVOStepsRefSubShapeList = new ObservableCollection<string>() { "1" };
            minVOStepsRefSubShape2List = new ObservableCollection<string>() { "1" };
            minVOStepsRefSubShape3List = new ObservableCollection<string>() { "1" };
            xPosRefSubShapeList = new ObservableCollection<string>() { "1" };
            yPosRefSubShapeList = new ObservableCollection<string>() { "1" };
            stitcher = new Stitcher(ref quiltContext);

            titleText += " (" + quiltContext.licenceName + ")";
            storage = new Storage();

            gCH = new GeoCoreHandler();

            structureList_exp = gCH.getGeo().structureList_;

            reset();
        }

        public void reset(bool empty = false)
        {
            pReset(empty);
        }

        void pReset(bool empty)
        {
            projectFileName = "";

            subshapes.Clear();
            subshapes.Add("1");
            minHLRefSubShapeList.Clear();
            minHLRefSubShapeList.Add("1");
            xPosRefSubShapeList.Clear();
            xPosRefSubShapeList.Add("1");
            yPosRefSubShapeList.Clear();
            yPosRefSubShapeList.Add("1");
            stitcher.reset(empty);
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

        public void setGLInt(gl_i i)
        {
            pSetGLInt(i);
        }

        void pSetGLInt(gl_i i)
        {
            switch (i)
            {
                case gl_i.zoom:
                    break;
            }
        }

        public enum opacity_gl { fg, bg }

        public void setOpacity(opacity_gl o)
        {
            pSetOpacity(o);
        }

        void pSetOpacity(opacity_gl o)
        {
            switch (o)
            {
                case opacity_gl.fg:
                    break;
                case opacity_gl.bg:
                    break;
            }
        }

        public enum properties_gl { aa, fill, points }

        public void setOpenGLProp(properties_gl p)
        {
            pSetOpenGLProp(p);
        }

        void pSetOpenGLProp(properties_gl p)
        {
            switch (p)
            {
                case properties_gl.aa:
                    break;
                case properties_gl.fill:
                    break;
                case properties_gl.points:
                    break;
            }
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
