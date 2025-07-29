using color;
using geoCoreLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Quilt;

public class CommonVars
{
    public Stitcher stitcher;

    public Storage storage { get; private set; }

    private QuiltContext quiltContext;

    public string projectFileName { get; set; }

    public string titleText = CentralProperties.productName + " " + CentralProperties.version;

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
    public ObservableCollection<string> tipRefSubShapeList { get; private set; }
    public ObservableCollection<string> tipRefSubShape2List { get; private set; }
    public ObservableCollection<string> tipRefSubShape3List { get; private set; }
    public ObservableCollection<string> xPosRefSubShapeList { get; private set; }
    public ObservableCollection<string> yPosRefSubShapeList { get; private set; }

    public ObservableCollection<string> structureList_exp { get; private set; }
    
    public List<string> openGLModeList { get; private set; }

    public GeoCoreHandler gCH;
    
    public CommonVars(ref QuiltContext context)
    {
        pInit(ref context);
    }

    private void pInit(ref QuiltContext context)
    {
        quiltContext = context;
        colors = new Colors();
        setColors(quiltContext.colors);
        openGLModeList = ["VBO", "Immediate"];

        subshapes = ["1"];
        minHLRefSubShapeList = ["1"];
        minHLRefSubShape2List = ["1"];
        minHLRefSubShape3List = ["1"];
        minVLRefSubShapeList = ["1"];
        minVLRefSubShape2List = ["1"];
        minVLRefSubShape3List = ["1"];
        minHORefSubShapeList = ["1"];
        minHORefSubShape2List = ["1"];
        minHORefSubShape3List = ["1"];
        minVORefSubShapeList = ["1"];
        minVORefSubShape2List = ["1"];
        minVORefSubShape3List = ["1"];
        minHLIncRefSubShapeList = ["1"];
        minHLIncRefSubShape2List = ["1"];
        minHLIncRefSubShape3List = ["1"];
        minVLIncRefSubShapeList = ["1"];
        minVLIncRefSubShape2List = ["1"];
        minVLIncRefSubShape3List = ["1"];
        minHOIncRefSubShapeList = ["1"];
        minHOIncRefSubShape2List = ["1"];
        minHOIncRefSubShape3List = ["1"];
        minVOIncRefSubShapeList = ["1"];
        minVOIncRefSubShape2List = ["1"];
        minVOIncRefSubShape3List = ["1"];
        minHLStepsRefSubShapeList = ["1"];
        minHLStepsRefSubShape2List = ["1"];
        minHLStepsRefSubShape3List = ["1"];
        minVLStepsRefSubShapeList = ["1"];
        minVLStepsRefSubShape2List = ["1"];
        minVLStepsRefSubShape3List = ["1"];
        minHOStepsRefSubShapeList = ["1"];
        minHOStepsRefSubShape2List = ["1"];
        minHOStepsRefSubShape3List = ["1"];
        minVOStepsRefSubShapeList = ["1"];
        minVOStepsRefSubShape2List = ["1"];
        minVOStepsRefSubShape3List = ["1"];
        tipRefSubShapeList = ["1"];
        tipRefSubShape2List = ["1"];
        tipRefSubShape3List = ["1"];
        xPosRefSubShapeList = ["1"];
        yPosRefSubShapeList = ["1"];
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

    private void pReset(bool empty)
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
    
    public enum gl_i { zoom }

    public static void setGLInt(gl_i i)
    {
        pSetGLInt(i);
    }

    private static void pSetGLInt(gl_i i)
    {
        switch (i)
        {
            case gl_i.zoom:
                break;
        }
    }

    public enum opacity_gl { fg, bg }

    public static void setOpacity(opacity_gl o)
    {
        pSetOpacity(o);
    }

    private static void pSetOpacity(opacity_gl o)
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

    public static void setOpenGLProp(properties_gl p)
    {
        pSetOpenGLProp(p);
    }

    private static void pSetOpenGLProp(properties_gl p)
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

    private Colors colors;
    public Colors getColors()
    {
        return pGetColors();
    }

    private Colors pGetColors()
    {
        return colors;
    }

    public void setColors(Colors source)
    {
        pSetColors(source);
    }

    private void pSetColors(Colors source)
    {
        colors = source;
    }
}