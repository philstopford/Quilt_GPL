using Error;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using VeldridEto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using Eto.Veldrid;
using shapeEngine;
using Veldrid;

namespace Quilt;

/// <summary>
/// Your application's main form
/// </summary>
public partial class MainForm : Form
{
    public class UIStringLists
    {
        public ObservableCollection<string> patternElementNames { get; set; }
        public ObservableCollection<string> patternElementNames_filtered { get; set; }

        public ObservableCollection<string> patternElementNamesForMerge_filtered { get; set; }
            
        public ObservableCollection<string> patternElementNames_filtered_array { get; set; }
        public ObservableCollection<string> xPosRefSubShapeList { get; set; }
        public ObservableCollection<string> yPosRefSubShapeList { get; set; }

        public ObservableCollection<string> minHLRefSubShapeList { get; set; }
        public ObservableCollection<string> minHLRefSubShape2List { get; set; }
        public ObservableCollection<string> minHLRefSubShape3List { get; set; }
        public ObservableCollection<string> minVLRefSubShapeList { get; set; }
        public ObservableCollection<string> minVLRefSubShape2List { get; set; }
        public ObservableCollection<string> minVLRefSubShape3List { get; set; }
        public ObservableCollection<string> minHORefSubShapeList { get; set; }
        public ObservableCollection<string> minHORefSubShape2List { get; set; }
        public ObservableCollection<string> minHORefSubShape3List { get; set; }
        public ObservableCollection<string> minVORefSubShapeList { get; set; }
        public ObservableCollection<string> minVORefSubShape2List { get; set; }
        public ObservableCollection<string> minVORefSubShape3List { get; set; }

        public ObservableCollection<string> minHLStepsRefSubShapeList { get; set; }
        public ObservableCollection<string> minHLStepsRefSubShape2List { get; set; }
        public ObservableCollection<string> minHLStepsRefSubShape3List { get; set; }
        public ObservableCollection<string> minVLStepsRefSubShapeList { get; set; }
        public ObservableCollection<string> minVLStepsRefSubShape2List { get; set; }
        public ObservableCollection<string> minVLStepsRefSubShape3List { get; set; }
        public ObservableCollection<string> minHOStepsRefSubShapeList { get; set; }
        public ObservableCollection<string> minHOStepsRefSubShape2List { get; set; }
        public ObservableCollection<string> minHOStepsRefSubShape3List { get; set; }
        public ObservableCollection<string> minVOStepsRefSubShapeList { get; set; }
        public ObservableCollection<string> minVOStepsRefSubShape2List { get; set; }
        public ObservableCollection<string> minVOStepsRefSubShape3List { get; set; }
        public ObservableCollection<string> minHLIncRefSubShapeList { get; set; }
        public ObservableCollection<string> minHLIncRefSubShape2List { get; set; }
        public ObservableCollection<string> minHLIncRefSubShape3List { get; set; }
        public ObservableCollection<string> minVLIncRefSubShapeList { get; set; }
        public ObservableCollection<string> minVLIncRefSubShape2List { get; set; }
        public ObservableCollection<string> minVLIncRefSubShape3List { get; set; }
        public ObservableCollection<string> minHOIncRefSubShapeList { get; set; }
        public ObservableCollection<string> minHOIncRefSubShape2List { get; set; }
        public ObservableCollection<string> minHOIncRefSubShape3List { get; set; }
        public ObservableCollection<string> minVOIncRefSubShapeList { get; set; }
        public ObservableCollection<string> minVOIncRefSubShape2List { get; set; }
        public ObservableCollection<string> minVOIncRefSubShape3List { get; set; }

        public ObservableCollection<string> tipRefSubShapeList { get; set; }
        public ObservableCollection<string> tipRefSubShape2List { get; set; }
        public ObservableCollection<string> tipRefSubShape3List { get; set; }

        public List<string> shapes { get; set; }
        public List<string> subShapePos { get; set; }
        public List<string> subShapeHorPos { get; set; }
        public List<string> subShapeVerPos { get; set; }

        public ObservableCollection<string> geoCoreStructureList_exp { get; set; }

        public ObservableCollection<string> subShapeList { get; set; }
        
        public List<string> tipLocs { get; set; }
        public List<string> openGLMode { get; set; }
    }


    private string helpPath;
    private bool helpAvailable;

    private bool suspendBuild; // to allow user to suspend quilt build.

    private QuiltContext quiltContext;

    private Command quitCommand, helpCommand, aboutCommand, copyLayer, pasteLayer, newSim, openSim, fromLayout, revertSim, saveSim, saveAsSim;

    private OVPSettings ovpSettings;

    private CreditsScreen aboutBox;

    private object drawingLock;

    private CommonVars commonVars;

    private void pSetupUIDataContext()
    {
        DataContext = new UIStringLists
        {
            shapes = ShapeLibrary.getAvailableShapes(CentralProperties.shapeTable),
            tipLocs = ShapeSettings.getAvailableTipsLocations(),
            patternElementNames = commonVars.stitcher.patternElementNames,
            patternElementNames_filtered = commonVars.stitcher.patternElementNames_filtered,
            patternElementNamesForMerge_filtered = commonVars.stitcher.patternElementNamesForMerge_filtered,
            patternElementNames_filtered_array = commonVars.stitcher.patternElementNames_filtered_array,
            xPosRefSubShapeList = commonVars.xPosRefSubShapeList,
            yPosRefSubShapeList = commonVars.yPosRefSubShapeList,
            minHLRefSubShapeList = commonVars.minHLRefSubShapeList,
            minHLRefSubShape2List = commonVars.minHLRefSubShape2List,
            minHLRefSubShape3List = commonVars.minHLRefSubShape3List,
            minVLRefSubShapeList = commonVars.minVLRefSubShapeList,
            minVLRefSubShape2List = commonVars.minVLRefSubShape2List,
            minVLRefSubShape3List = commonVars.minVLRefSubShape3List,
            minHORefSubShapeList = commonVars.minHORefSubShapeList,
            minHORefSubShape2List = commonVars.minHORefSubShape2List,
            minHORefSubShape3List = commonVars.minHORefSubShape3List,
            minVORefSubShapeList = commonVars.minVORefSubShapeList,
            minVORefSubShape2List = commonVars.minVORefSubShape2List,
            minVORefSubShape3List = commonVars.minVORefSubShape3List,
            minHLIncRefSubShapeList = commonVars.minHLIncRefSubShapeList,
            minHLIncRefSubShape2List = commonVars.minHLIncRefSubShape2List,
            minHLIncRefSubShape3List = commonVars.minHLIncRefSubShape3List,
            minVLIncRefSubShapeList = commonVars.minVLIncRefSubShapeList,
            minVLIncRefSubShape2List = commonVars.minVLIncRefSubShape2List,
            minVLIncRefSubShape3List = commonVars.minVLIncRefSubShape3List,
            minHOIncRefSubShapeList = commonVars.minHOIncRefSubShapeList,
            minHOIncRefSubShape2List = commonVars.minHOIncRefSubShape2List,
            minHOIncRefSubShape3List = commonVars.minHOIncRefSubShape3List,
            minVOIncRefSubShapeList = commonVars.minVOIncRefSubShapeList,
            minVOIncRefSubShape2List = commonVars.minVOIncRefSubShape2List,
            minVOIncRefSubShape3List = commonVars.minVOIncRefSubShape3List,
            minHLStepsRefSubShapeList = commonVars.minHLStepsRefSubShapeList,
            minHLStepsRefSubShape2List = commonVars.minHLStepsRefSubShape2List,
            minHLStepsRefSubShape3List = commonVars.minHLStepsRefSubShape3List,
            minVLStepsRefSubShapeList = commonVars.minVLStepsRefSubShapeList,
            minVLStepsRefSubShape2List = commonVars.minVLStepsRefSubShape2List,
            minVLStepsRefSubShape3List = commonVars.minVLStepsRefSubShape3List,
            minHOStepsRefSubShapeList = commonVars.minHOStepsRefSubShapeList,
            minHOStepsRefSubShape2List = commonVars.minHOStepsRefSubShape2List,
            minHOStepsRefSubShape3List = commonVars.minHOStepsRefSubShape3List,
            minVOStepsRefSubShapeList = commonVars.minVOStepsRefSubShapeList,
            minVOStepsRefSubShape2List = commonVars.minVOStepsRefSubShape2List,
            minVOStepsRefSubShape3List = commonVars.minVOStepsRefSubShape3List,
            tipRefSubShapeList = commonVars.tipRefSubShapeList,
            tipRefSubShape2List = commonVars.tipRefSubShape2List,
            tipRefSubShape3List = commonVars.tipRefSubShape3List,
            subShapePos = ShapeSettings.getAvailableSubShapePositions(),
            subShapeHorPos = ShapeSettings.getAvailableHorShapePositions(),
            subShapeVerPos = ShapeSettings.getAvailableVerShapePositions(),
            subShapeList = commonVars.subshapes,
            geoCoreStructureList_exp = commonVars.structureList_exp,
            openGLMode = commonVars.openGLModeList
        };
    }

    private void pLoadPrefs()
    {
        // We have to do this by hand, reading and parsing an XML file. Yay.
        string filename = EtoEnvironment.GetFolderPath(EtoSpecialFolder.ApplicationSettings);
        filename += Path.DirectorySeparatorChar + "quilt_prefs.xml";

        XElement prefs;
        try
        {
            prefs = XElement.Load(filename);
        }
        catch (Exception)
        {
            if (File.Exists(filename))
            {
                ErrorReporter.showMessage_OK("Prefs file exists, but can't be read. Using defaults.", "Preferences Error");
            }
            return; // file may not exist (new user) or is inaccessible. We have defaults so can just return without trouble.
        }


        try
        {
            quiltContext.AA = Convert.ToBoolean(prefs.Descendants("openGL").Descendants("openGLAA").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            quiltContext.filledPolygons = Convert.ToBoolean(prefs.Descendants("openGL").Descendants("openGLFilledPolygons").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            quiltContext.drawExtents = Convert.ToBoolean(prefs.Descendants("openGL").Descendants("drawExtents").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            quiltContext.drawPoints = Convert.ToBoolean(prefs.Descendants("openGL").Descendants("openGLPoints").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            quiltContext.openGLZoomFactor = Convert.ToInt32(prefs.Descendants("openGL").Descendants("openGLZoomFactor").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            quiltContext.BGOpacity = Convert.ToDouble(prefs.Descendants("openGL").Descendants("openGLBGOpacity").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            quiltContext.FGOpacity = Convert.ToDouble(prefs.Descendants("openGL").Descendants("openGLFGOpacity").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            quiltContext.angularTolerance = Convert.ToDouble(prefs.Descendants("decomposition").Descendants("angularTolerance").First().Value);
        }
        catch (Exception)
        {
            try
            {
                quiltContext.angularTolerance = Convert.ToDouble(prefs.Descendants("misc").Descendants("angularTolerance").First().Value);
            }
            catch
            {
                // ignored
            }
        }

        try
        {
            quiltContext.verticalRectDecomp = Convert.ToBoolean(prefs.Descendants("decomposition").Descendants("verticalRectDecomp").First().Value);
        }
        catch (Exception)
        {
            try
            {
                quiltContext.verticalRectDecomp = Convert.ToBoolean(prefs.Descendants("misc").Descendants("verticalRectDecomp").First().Value);
            }
            catch
            {
                // ignored
            }
        }

        try
        {
            quiltContext.expandUI = Convert.ToBoolean(prefs.Descendants("interface").Descendants("expandUI").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "selectedColor";
            quiltContext.colors.selected_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").Single().Value);
            quiltContext.colors.selected_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").Single().Value);
            quiltContext.colors.selected_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").Single().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "deselectedColor";
            quiltContext.colors.deselected_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").Single().Value);
            quiltContext.colors.deselected_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").Single().Value);
            quiltContext.colors.deselected_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").Single().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "subshape1Color";
            quiltContext.colors.subshape1_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.subshape1_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.subshape1_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "subshape2Color";
            quiltContext.colors.subshape2_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.subshape2_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.subshape2_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "subshape3Color";
            quiltContext.colors.subshape3_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.subshape3_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.subshape3_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "axisColor";
            quiltContext.colors.axis_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.axis_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.axis_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "majorColor";
            quiltContext.colors.major_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.major_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.major_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "minorColor";
            quiltContext.colors.minor_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.minor_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.minor_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "backgroundColor";
            quiltContext.colors.background_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.background_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.background_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string layerCol = "extentsColor";
            quiltContext.colors.extents_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
            quiltContext.colors.extents_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
            quiltContext.colors.extents_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
        }
        catch
        {
            // ignored
        }

        quiltContext.colors.rebuildLists();
    }

    private void pSavePrefs()
    {
        string filename = EtoEnvironment.GetFolderPath(EtoSpecialFolder.ApplicationSettings);
        filename += Path.DirectorySeparatorChar + "quilt_prefs.xml";

        try
        {
            XDocument prefsXML = new(
                new XElement("root"));
            // ReSharper disable once PossibleNullReferenceException
            prefsXML.Root.Add(new XElement("version", CentralProperties.version));

            XElement openGLPrefs = new("openGL",
                new XElement("openGLAA", quiltContext.AA),
                new XElement("openGLFilledPolygons", quiltContext.filledPolygons),
                new XElement("openGLPoints", quiltContext.drawPoints),
                new XElement("openGLZoomFactor", quiltContext.openGLZoomFactor),
                new XElement("openGLFGOpacity", quiltContext.FGOpacity),
                new XElement("openGLBGOpacity", quiltContext.BGOpacity),
                new XElement("drawExtents", quiltContext.drawExtents));
            prefsXML.Root.Add(openGLPrefs);

            XElement decompPrefs = new("decomposition",
                new XElement("angularTolerance", quiltContext.angularTolerance),
                new XElement("verticalRectDecomp", quiltContext.verticalRectDecomp));
            prefsXML.Root.Add(decompPrefs);

            XElement uiPrefs = new("interface",
                new XElement("expandUI", quiltContext.expandUI));
            prefsXML.Root.Add(uiPrefs);

            XElement colorPrefs = new("colors");

            XElement subshape1Color = new("subshape1Color",
                new XElement("R", quiltContext.colors.subshape1_Color.R),
                new XElement("G", quiltContext.colors.subshape1_Color.G),
                new XElement("B", quiltContext.colors.subshape1_Color.B));
            colorPrefs.Add(subshape1Color);

            XElement subshape2Color = new("subshape2Color",
                new XElement("R", quiltContext.colors.subshape2_Color.R),
                new XElement("G", quiltContext.colors.subshape2_Color.G),
                new XElement("B", quiltContext.colors.subshape2_Color.B));
            colorPrefs.Add(subshape2Color);

            XElement subshape3Color = new("subshape3Color",
                new XElement("R", quiltContext.colors.subshape3_Color.R),
                new XElement("G", quiltContext.colors.subshape3_Color.G),
                new XElement("B", quiltContext.colors.subshape3_Color.B));
            colorPrefs.Add(subshape3Color);

            XElement enabledColor = new("selectedColor",
                new XElement("R", quiltContext.colors.selected_Color.R),
                new XElement("G", quiltContext.colors.selected_Color.G),
                new XElement("B", quiltContext.colors.selected_Color.B));
            colorPrefs.Add(enabledColor);

            XElement backgroundColor = new("deselectedColor",
                new XElement("R", quiltContext.colors.deselected_Color.R),
                new XElement("G", quiltContext.colors.deselected_Color.G),
                new XElement("B", quiltContext.colors.deselected_Color.B));
            colorPrefs.Add(backgroundColor);

            XElement axisColor = new("axisColor",
                new XElement("R", quiltContext.colors.axis_Color.R),
                new XElement("G", quiltContext.colors.axis_Color.G),
                new XElement("B", quiltContext.colors.axis_Color.B));
            colorPrefs.Add(axisColor);

            XElement majorColor = new("majorColor",
                new XElement("R", quiltContext.colors.major_Color.R),
                new XElement("G", quiltContext.colors.major_Color.G),
                new XElement("B", quiltContext.colors.major_Color.B));
            colorPrefs.Add(majorColor);

            XElement minorColor = new("minorColor",
                new XElement("R", quiltContext.colors.minor_Color.R),
                new XElement("G", quiltContext.colors.minor_Color.G),
                new XElement("B", quiltContext.colors.minor_Color.B));
            colorPrefs.Add(minorColor);

            XElement vpBackgroundColor = new("backgroundColor",
                new XElement("R", quiltContext.colors.background_Color.R),
                new XElement("G", quiltContext.colors.background_Color.G),
                new XElement("B", quiltContext.colors.background_Color.B));
            colorPrefs.Add(vpBackgroundColor);

            XElement extentsColor = new("extentsColor",
                new XElement("R", quiltContext.colors.extents_Color.R),
                new XElement("G", quiltContext.colors.extents_Color.G),
                new XElement("B", quiltContext.colors.extents_Color.B));
            colorPrefs.Add(extentsColor);
                
            prefsXML.Root.Add(colorPrefs);

            prefsXML.Save(filename);
        }
        catch (Exception)
        {
            ErrorReporter.showMessage_OK("Failed to save preferences", "Error");
        }
    }

    private void pSetUI(bool status)
    {
        fileMenu.Enabled = status;
        editMenu.Enabled = status;

        //settings_tl.Enabled = status;
        left_tl.Enabled = status;
        right_tl.Enabled = status;

        if (status)
        {
            pUpdatePatternElementUI(false);
        }
    }

    public MainForm(QuiltContext _quiltContext)
    {
        pMainForm(_quiltContext);
    }

    private void pMainForm(QuiltContext _quiltContext)
    {
        // string basePath = AppContext.BaseDirectory; // Disabled this as release builds do not seem to populate this field. Use the above complex approach instead.
        helpPath = Path.Combine(EtoEnvironment.GetFolderPath(EtoSpecialFolder.ApplicationResources), "Documentation", "index.html");
        helpAvailable = File.Exists(helpPath);

        pUI(_quiltContext);
    }

    private bool _veldridReady;

    private bool VeldridReady
    {
        get => _veldridReady;
        set
        {
            _veldridReady = value;

            pSetUpVeldrid();
        }
    }

    private bool _formReady;

    private bool FormReady
    {
        get => _formReady;
        set
        {
            _formReady = value;

            pSetUpVeldrid();
        }
    }

    private void pSetUpVeldrid()
    {
        if (!(FormReady && VeldridReady))
        {
            return;
        }

        pCreateVPContextMenu();

        viewPort.Clock.Start();
    }

    private void pCreateLBContextMenu()
    {
        listbox_menu = new ContextMenu();
        int contextMenuItemIndex = 0;
        lb_copy = new ButtonMenuItem { Text = "Copy" };
        listbox_menu.Items.Add(lb_copy);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pCopy();
        };
        contextMenuItemIndex++;
        lb_paste = new ButtonMenuItem { Text = "Paste" };
        listbox_menu.Items.Add(lb_paste);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pPaste();
        };
        contextMenuItemIndex++;
        listbox_menu.Items.Add(new ButtonMenuItem { Text = "Remove" });
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pRemovePatternElement();
        };
        contextMenuItemIndex++;
        listbox_menu.Items.Add(new ButtonMenuItem { Text = "Duplicate" });
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pDuplicatePatternElement();
        };
        contextMenuItemIndex++;
        lb_selLinkedElement = new ButtonMenuItem {Text = "Select Merge Element"};
        listbox_menu.Items.Add(lb_selLinkedElement);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.linkedElementIndex, offset:false);
        };
        contextMenuItemIndex++;
        lb_selXPosElement = new ButtonMenuItem {Text = "Select X Position Reference Element"};
        listbox_menu.Items.Add(lb_selXPosElement);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.xPosRef);
        };
        contextMenuItemIndex++;
        lb_selYPosElement = new ButtonMenuItem {Text = "Select Y Position Reference Element"};
        listbox_menu.Items.Add(lb_selYPosElement);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.yPosRef);
        };
        contextMenuItemIndex++;
        lb_selRotElement = new ButtonMenuItem {Text = "Select Rotation Reference Element"};
        listbox_menu.Items.Add(lb_selRotElement);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.rotationRef);
        };
        contextMenuItemIndex++;
        lb_selArrayElement = new ButtonMenuItem {Text = "Select Array Reference Element"};
        listbox_menu.Items.Add(lb_selArrayElement);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.arrayRef);
        };
        contextMenuItemIndex++;
        lb_selArrayRotElement = new ButtonMenuItem {Text = "Select Array Rotation Reference Element"};
        listbox_menu.Items.Add(lb_selArrayRotElement);
        listbox_menu.Items[contextMenuItemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.arrayRotationRef);
        };
    }

    private void pSelectReferenceElement(PatternElement.properties_i property, int subshape = 0, bool offset = true)
    {
        int patternElement = listBox_entries.SelectedIndex;
        if (patternElement > -1)
        {
            int tmp = commonVars.stitcher
                .getPatternElement(0, patternElement)
                .getInt(property, subshape);

            // Work around 'self' references that cause an offset in indexing that we need to compensate for.
            if (offset)
            {
                tmp--;
                if (tmp >= patternElement)
                {
                    tmp++;
                }
            }
            
            listBox_entries.SelectedIndex = tmp;

        }
        pDoPatternElementUI(0);
    }
    
    private void pUpdateLBContextMenu()
    {
        try
        {
            lb_paste.Enabled = commonVars.stitcher.isCopySet();
        }
        catch (Exception)
        {
            lb_paste.Enabled = false;
        }
        
        int patternElement = listBox_entries.SelectedIndex;
        try
        {
            lb_selLinkedElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.linkedElementIndex) != -1);
        }
        catch (Exception)
        {
            lb_selLinkedElement.Enabled = false;
        }

        try
        {
            lb_selXPosElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.xPosRef) != 0);
        }
        catch (Exception)
        {
            lb_selXPosElement.Enabled = false;
        }

        try
        {
            lb_selYPosElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.yPosRef) != 0);
        }
        catch (Exception)
        {
            lb_selYPosElement.Enabled = false;
        }

        try
        {
            // Self and 'World Origin'
            lb_selRotElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.rotationRef) > 1);
        }
        catch (Exception)
        {
            lb_selRotElement.Enabled = false;
        }

        try
        {
            lb_selArrayElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.arrayRef) != 0);
        }
        catch (Exception)
        {
            lb_selArrayElement.Enabled = false;
        }

        try
        {
            // Self and 'World Origin'
            lb_selArrayRotElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.arrayRotationRef) > 1);
        }
        catch (Exception)
        {
            lb_selArrayRotElement.Enabled = false;
        }

    }

    private void pUI(QuiltContext _quiltContext)
    {
        quiltContext = _quiltContext ?? new QuiltContext("", VeldridSurface.PreferredBackend);


        Shown += (_, _) => FormReady = true;

        pLoadPrefs();

        commonVars = new CommonVars(ref quiltContext);

        pCreateLBContextMenu();

        pDelegates();

        pSetupUIDataContext();

        drawingLock = new object();

        ovpSettings = new OVPSettings();
        ovpSettings.drawDrawn(true);
        ovpSettings.drawFilled(quiltContext.filledPolygons);
        ovpSettings.drawPoints(quiltContext.drawPoints);
        ovpSettings.setZoomStep(quiltContext.openGLZoomFactor);
        ovpSettings.aA(quiltContext.AA);

        Title = commonVars.titleText;

        pCommandsUI();

        // Compensate for menu.
        MinimumSize = new Size(750, 450);
        Size = new Size(1250,760);

        Panel vp = new();

        TableLayout vp_tl = new();

        vp.Content = vp_tl;

        vp_tl.Rows.Add(new TableRow());

        pPatternSettingsUI();
        Scrollable settings_sc = new() {Content = settings};

        quiltUISplitter = new Panel
        {
            Content = new Splitter
            {
                Orientation = Orientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1,
                Panel1 = settings_sc,
                Panel2 = vp
                // Panel1MinimumSize = settings.Size.Width,
            }
        };

        tabControl = new TabControl();
        quiltPage = new TabPage {Text = "Quilt"};
        tabControl.Pages.Add(quiltPage);
        pQuiltUISetup();
        quiltPage.Content = quiltUISplitter;

        prefsPage = new TabPage {Text = "Preferences"};
        tabControl.Pages.Add(prefsPage);
        pPrefsUI();
        prefsPage.Content = prefsPanel;
        tabControl.SelectedIndexChanged += pInterfaceUpdate;

        Content = tabControl;

        pDoPatternElementUI(0, updateUI: true);
        pDrawPreviewPanelHandler();

        pUpdateProgressLabel("Hello");
        pUpdateUIColors();

        GraphicsDeviceOptions options = new(
            false,
            Veldrid.PixelFormat.R32_Float,
            false,
            ResourceBindingModel.Improved);

        vSurface = new VeldridSurface(quiltContext.backend, options);
        
        viewPort = new VeldridDriver(ref ovpSettings, ref vSurface)
        {
            Surface = vSurface,
        };

        vSurface.VeldridInitialized += (_, _) =>
        {
            viewPort.SetUpVeldrid();
            
            VeldridReady = true;
        };

        vSurface.Size = new Size(viewportSize, viewportSize);
        viewPort.updateHostFunc = pViewportUpdateHost;
        viewPort.updateHostSelectionFunc = pViewportSelectionFunc;

        const string viewportToolTipText = "(w/a/s/d) to navigate\r\n(r) to reset\r\n(n/m) to zoom\r\n(f) to freeze/thaw\r\n(x) to zoom extents\r\n(z) to zoom selected";
        vp.ToolTip = viewportToolTipText;

        commonVars.titleText += " " + vSurface.Backend;

        Title = commonVars.titleText;

        vp_tl.Rows[0].Cells.Add(new TableCell { Control = vSurface });

        if (quiltContext.xmlFileArg != "")
        {
            pDoLoad(quiltContext.xmlFileArg);
        }

        tabControl.DragEnter += pDragEvent;
        tabControl.DragOver += pDragEvent;

        tabControl.DragDrop += pDragAndDrop;
    }

    private void pInterfaceUpdate(object sender, EventArgs e)
    {
        if (tabControl.SelectedIndex == 1) // utils
        {
            utilsUIFrozen = true;
            pUpdatePrefsUI();
            utilsUIFrozen = false;
        }
        else
        {
            pDoPatternElementUI(sender, e);
        }
    }

    private void pPrefsUI()
    {
        prefsPanel = new Panel();
        TableLayout prefsPanel_table = new();
        prefsPanel.Content = prefsPanel_table;

        prefsPanel_table.Rows.Add(new TableRow());

        TableCell opengl_tc = new();

        prefsPanel_table.Rows[^1].Cells.Add(opengl_tc);

        pOpenGL_settings_utilsUISetup(opengl_tc);

        TableCell padding = new();
        prefsPanel_table.Rows[^1].Cells.Add(padding);

        prefsPanel_table.Rows.Add(new TableRow());

        TableCell misc_tc = new();

        prefsPanel_table.Rows[^1].Cells.Add(misc_tc);

        TableCell padding2 = new();
        prefsPanel_table.Rows[^1].Cells.Add(padding2);

        prefsPanel_table.Rows.Add(new TableRow()); // padding

        pMisc_settings_utilsUISetup(misc_tc);

        TableCell ui_tc = new();

        prefsPanel_table.Rows[^1].Cells.Add(ui_tc);

        TableCell padding3 = new();
        prefsPanel_table.Rows[^1].Cells.Add(padding3);

        prefsPanel_table.Rows.Add(new TableRow()); // padding

        pUI_settings_setup(ui_tc);

        pAddPrefsHandlers();
    }

    private void pMisc_settings_utilsUISetup(TableCell tc)
    {
        GroupBox groupBox_misc = new();
        tc.Control = groupBox_misc;
        TableLayout groupBox_misc_table = new();
        groupBox_misc.Text = "Decomposition";
        groupBox_misc.Content = groupBox_misc_table;

        groupBox_misc_table.Rows.Add(new TableRow());
        TableCell row0 = new();
        groupBox_misc_table.Rows[^1].Cells.Add(row0);

        Panel angTol_pnl = new();
        TableLayout angTol_table = new();
        angTol_pnl.Content = TableLayout.AutoSized(angTol_table);
        row0.Control = angTol_pnl;

        angTol_table.Rows.Add(new TableRow());

        lbl_angularTolerance = new Label
        {
            Text = "Angular Tolerance",
            ToolTip =
                "This value is used as the 'error' accepted when removing co-linear vertices from imported layout."
        };
        angTol_table.Rows[^1].Cells.Add(new TableCell { Control = lbl_angularTolerance });

        num_angularTolerance = new NumericStepper
        {
            ToolTip =
                "This value is used as the 'error' accepted when removing co-linear vertices from imported layout.",
            MinValue = 0.01f,
            Increment = 0.01f,
            DecimalPlaces = 2
        };
        pSetSize(num_angularTolerance, 50, num_Height);
        angTol_table.Rows[^1].Cells.Add(new TableCell { Control = num_angularTolerance });

        checkBox_verticalRectDecomp = new CheckBox
        {
            ToolTip =
                "Decompose layout to vertically-oriented rectangles when set.\r\nUse horizontally-oriented rectangles, if cleared.",
            Text = "Vertical Decomp"
        };
        groupBox_misc_table.Rows[^1].Cells.Add(new TableCell { Control = checkBox_verticalRectDecomp });
        
        groupBox_misc_table.Rows.Add(new TableRow());
        TableCell row1 = new();
        groupBox_misc_table.Rows[^1].Cells.Add(row1);
    }

    private void pUI_settings_setup(TableCell tc)
    {
        GroupBox groupBox_misc = new();
        tc.Control = groupBox_misc;
        TableLayout groupBox_misc_table = new();
        groupBox_misc.Text = "Interface";
        groupBox_misc.Content = groupBox_misc_table;

        groupBox_misc_table.Rows.Add(new TableRow());
        TableCell row0 = new();
        groupBox_misc_table.Rows[^1].Cells.Add(row0);

        checkBox_expandUI = new CheckBox
        {
            ToolTip =
                "Start interface in expanded state.",
            Text = "Start interface expanded.",
            Checked = true
        };
        groupBox_misc_table.Rows[^1].Cells.Add(new TableCell { Control = checkBox_expandUI });
        
    }
    
    private void pOpenGL_settings_utilsUISetup(TableCell tc)
    {
        TableLayout groupBox_openGL_table = new();
        GroupBox groupBox_openGL = new() {Text = "OpenGL", Content = groupBox_openGL_table};
        tc.Control = groupBox_openGL;

        groupBox_openGL_table.Rows.Add(new TableRow());
        TableCell row0 = new();
        groupBox_openGL_table.Rows[^1].Cells.Add(row0);
        pOpenGLRow0(row0);
        TableCell row0padding = new();
        groupBox_openGL_table.Rows[^1].Cells.Add(row0padding);

        groupBox_openGL_table.Rows.Add(new TableRow());
        TableCell row1 = new();
        groupBox_openGL_table.Rows[^1].Cells.Add(row1);
        pOpenGLRow1(row1);
        TableCell row1padding = new();
        groupBox_openGL_table.Rows[^1].Cells.Add(row1padding);

        groupBox_openGL_table.Rows.Add(new TableRow());
        TableCell row2 = new();
        groupBox_openGL_table.Rows[^1].Cells.Add(row2);
        pOpenGLRow2(row2);
        TableCell row2padding = new();
        groupBox_openGL_table.Rows[^1].Cells.Add(row2padding);
    }

    private void pOpenGLRow0(TableCell tc)
    {
        Panel p = new();
        tc.Control = p;

        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        p.Content = tl;

        Panel lRow0 = new();
        tl.Rows[^1].Cells.Add(new TableCell { Control = lRow0 });

        Panel rRow0 = new();
        tl.Rows[^1].Cells.Add(new TableCell { Control = rRow0 });

        TableLayout lRow0tl = new();
        lRow0.Content = lRow0tl;
        lRow0tl.Rows.Add(new TableRow());

        TableLayout rRow0tl = new();
        rRow0.Content = rRow0tl;
        rRow0tl.Rows.Add(new TableRow());

        Panel zoomPnl = new();
        TableLayout zoomTL = new();
        zoomPnl.Content = zoomTL;
        lRow0tl.Rows[^1].Cells.Add(new TableCell { Control = zoomPnl });

        zoomTL.Rows.Add(new TableRow());

        lbl_zoomSpeed = new Label {Text = "Zoom Increment"};
        zoomTL.Rows[^1].Cells.Add(new TableCell { Control = lbl_zoomSpeed });

        num_zoomSpeed = new NumericStepper {MinValue = 1, Increment = 1};
        pSetSize(num_zoomSpeed, 50, num_Height);
        zoomTL.Rows[^1].Cells.Add(new TableCell { Control = num_zoomSpeed });

        zoomTL.Rows[^1].Cells.Add(new TableCell { Control = null }); // padding

        TableLayout fgOpTL = new();
        Panel fgOpPnl = new() {Content = fgOpTL};
        rRow0tl.Rows[^1].Cells.Add(new TableCell { Control = fgOpPnl });

        fgOpTL.Rows.Add(new TableRow());

        lbl_fgOpacity = new Label
        {
            Text = "FG Opacity", ToolTip = "Opacity of selected pattern elements in viewport."
        };
        fgOpTL.Rows[^1].Cells.Add(new TableCell { Control = lbl_fgOpacity });

        num_fgOpacity = new NumericStepper
        {
            ToolTip = "Opacity of selected pattern elements in viewport.",
            MinValue = 0.01f,
            Increment = 0.1f,
            DecimalPlaces = 2
        };
        pSetSize(num_fgOpacity, 50, num_Height);
        fgOpTL.Rows[^1].Cells.Add(new TableCell { Control = num_fgOpacity });

        TableLayout bgOpTL = new();
        Panel bgOpPnl = new() {Content = bgOpTL};
        rRow0tl.Rows[^1].Cells.Add(new TableCell { Control = bgOpPnl });

        bgOpTL.Rows.Add(new TableRow());

        lbl_bgOpacity = new Label
        {
            Text = "BG Opacity", ToolTip = "Opacity of not-selected pattern elements in viewport."
        };
        bgOpTL.Rows[^1].Cells.Add(new TableCell { Control = lbl_bgOpacity });

        num_bgOpacity = new NumericStepper
        {
            ToolTip = "Opacity of not-selected pattern elements in viewport.",
            MinValue = 0.01f,
            Increment = 0.1f,
            DecimalPlaces = 2
        };
        pSetSize(num_bgOpacity, 50, num_Height);
        bgOpTL.Rows[^1].Cells.Add(new TableCell { Control = num_bgOpacity });

        tl.Rows[^1].Cells.Add(new TableCell { Control = null }); // padding

        tl.Rows.Add(new TableRow());

        TableLayout lRow1tl = new();
        Panel lRow1 = new() {Content = lRow1tl};
        tl.Rows[^1].Cells.Add(new TableCell { Control = lRow1 });

        TableLayout rRow1tl = new();
        Panel rRow1 = new() {Content = rRow1tl};
        tl.Rows[^1].Cells.Add(new TableCell { Control = rRow1 });

        tl.Rows[^1].Cells.Add(new TableCell { Control = null }); // padding

        lRow1tl.Rows.Add(new TableRow());

        rRow1tl.Rows.Add(new TableRow());

        TableLayout dispOptsTL = new();
        Panel dispOptsPnl = new() {Content = dispOptsTL};
        lRow1tl.Rows[^1].Cells.Add(new TableCell { Control = dispOptsPnl });

        dispOptsTL.Rows.Add(new TableRow());

        checkBox_OGLAA = new CheckBox {Text = "Antialiasing"};
        dispOptsTL.Rows[^1].Cells.Add(new TableCell { Control = checkBox_OGLAA });

        checkBox_OGLFill = new CheckBox {Text = "Fill", ToolTip = "Draw filled polygons, if set."};
        dispOptsTL.Rows[^1].Cells.Add(new TableCell { Control = checkBox_OGLFill });

        checkBox_OGLPoints = new CheckBox {Text = "Points", ToolTip = "Draw points in viewport, if set."};
        dispOptsTL.Rows[^1].Cells.Add(new TableCell { Control = checkBox_OGLPoints });

        checkBox_drawExtents = new CheckBox
        {
            Text = "Extents", ToolTip = "Draw pattern extents in viewport, if set."
        };
        dispOptsTL.Rows[^1].Cells.Add(new TableCell { Control = checkBox_drawExtents });
    }

    private void pOpenGLRow1(TableCell tc)
    {
        TableLayout swatchesTL = new();
        Panel p = new() {Content = swatchesTL};
        tc.Control = p;

        swatchesTL.Rows.Add(new TableRow());

        TableRow row0 = new();
        swatchesTL.Rows.Add(row0);

        pOpengl_swatchrow0(row0);

        TableRow row1 = new();
        swatchesTL.Rows.Add(row1);

        pOpengl_swatchrow1(row1);

        TableRow padding = new();
        swatchesTL.Rows.Add(padding);
    }

    private void pOpengl_swatchrow0(TableRow tr)
    {
        TableLayout c0TL = new();
        Panel c0 = new() {Content = c0TL};
        tr.Cells.Add(new TableCell { Control = c0 });

        c0TL.Rows.Add(new TableRow());

        lbl_minorGridColor = new Label
        {
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().minor_Color)
        };
        pSetSize(lbl_minorGridColor, label_Height, label_Height);
        c0TL.Rows[0].Cells.Add(lbl_minorGridColor);

        lbl_minorGridColor_name = new Label {Text = "Minor Grid"};
        c0TL.Rows[0].Cells.Add(lbl_minorGridColor_name);

        TableLayout c1TL = new();
        Panel c1 = new() {Content = c1TL};
        tr.Cells.Add(new TableCell { Control = c1 });

        c1TL.Rows.Add(new TableRow());

        lbl_majorGridColor = new Label
        {
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().major_Color)
        };
        pSetSize(lbl_majorGridColor, label_Height, label_Height);
        c1TL.Rows[0].Cells.Add(lbl_majorGridColor);

        lbl_majorGridColor_name = new Label {Text = "Major Grid"};
        c1TL.Rows[0].Cells.Add(lbl_majorGridColor_name);

        TableLayout c2TL = new();
        Panel c2 = new() {Content = c2TL};
        tr.Cells.Add(new TableCell { Control = c2 });

        c2TL.Rows.Add(new TableRow());

        lbl_axisColor = new Label {BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().axis_Color)};
        pSetSize(lbl_axisColor, label_Height, label_Height);
        c2TL.Rows[0].Cells.Add(lbl_axisColor);

        lbl_axisColor_name = new Label {Text = "Axis"};
        c2TL.Rows[0].Cells.Add(lbl_axisColor_name);

        TableLayout c3TL = new();
        Panel c3 = new() {Content = c3TL};
        tr.Cells.Add(new TableCell { Control = c3 });

        c3TL.Rows.Add(new TableRow());

        lbl_vpbgColor = new Label
        {
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().background_Color)
        };
        pSetSize(lbl_vpbgColor, label_Height, label_Height);
        c3TL.Rows[0].Cells.Add(lbl_vpbgColor);

        lbl_vpbgColor_name = new Label {Text = "VP Background"};
        c3TL.Rows[0].Cells.Add(lbl_vpbgColor_name);
    }

    private void pOpengl_swatchrow1(TableRow tr)
    {
        TableLayout c0TL = new();
        Panel c0 = new() {Content = c0TL};
        tr.Cells.Add(new TableCell { Control = c0 });

        c0TL.Rows.Add(new TableRow());

        lbl_enabledColor = new Label
        {
            ToolTip = "Color of polygons from selected pattern elements in viewport",
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().enabled_Color)
        };
        pSetSize(lbl_enabledColor, label_Height, label_Height);
        c0TL.Rows[0].Cells.Add(lbl_enabledColor);

        lbl_enabledColor_name = new Label
        {
            ToolTip = "Color of polygons from selected pattern elements in viewport", Text = "Enabled"
        };
        c0TL.Rows[0].Cells.Add(lbl_enabledColor_name);

        TableLayout c0bTL = new();
        Panel c0b = new() {Content = c0bTL};
        tr.Cells.Add(new TableCell { Control = c0b });

        c0bTL.Rows.Add(new TableRow());

        lbl_backgroundColor = new Label
        {
            ToolTip = "Color of polygons from unselected pattern elements in viewport",
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().deselected_Color)
        };
        pSetSize(lbl_backgroundColor, label_Height, label_Height);
        c0bTL.Rows[0].Cells.Add(lbl_backgroundColor);

        lbl_backgroundColor_name = new Label
        {
            ToolTip = "Color of polygons from unselected pattern elements in viewport", Text = "Background"
        };
        c0bTL.Rows[0].Cells.Add(lbl_backgroundColor_name);


        TableLayout c1TL = new();
        Panel c1 = new() {Content = c1TL};
        tr.Cells.Add(new TableCell { Control = c1 });

        c1TL.Rows.Add(new TableRow());

        lbl_ss1Color = new Label
        {
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape1_Color)
        };
        pSetSize(lbl_ss1Color, label_Height, label_Height);
        c1TL.Rows[0].Cells.Add(lbl_ss1Color);

        lbl_ss1Color_name = new Label {Text = "Subshape 1"};
        c1TL.Rows[0].Cells.Add(lbl_ss1Color_name);

        TableLayout c2TL = new();
        Panel c2 = new() {Content = c2TL};
        tr.Cells.Add(new TableCell { Control = c2 });

        c2TL.Rows.Add(new TableRow());

        lbl_ss2Color = new Label
        {
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape2_Color)
        };
        pSetSize(lbl_ss2Color, label_Height, label_Height);
        c2TL.Rows[0].Cells.Add(lbl_ss2Color);

        lbl_ss2Color_name = new Label {Text = "Subshape 2"};
        c2TL.Rows[0].Cells.Add(lbl_ss2Color_name);

        TableLayout c3TL = new();
        Panel c3 = new() {Content = c3TL};
        tr.Cells.Add(new TableCell { Control = c3 });

        c3TL.Rows.Add(new TableRow());

        lbl_ss3Color = new Label
        {
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape3_Color)
        };
        pSetSize(lbl_ss3Color, label_Height, label_Height);
        c3TL.Rows[0].Cells.Add(lbl_ss3Color);

        lbl_ss3Color_name = new Label {Text = "Subshape 3"};
        c3TL.Rows[0].Cells.Add(lbl_ss3Color_name);

        TableLayout c4TL = new();
        Panel c4 = new() {Content = c4TL};
        tr.Cells.Add(new TableCell { Control = c4 });

        c4TL.Rows.Add(new TableRow());

        lbl_extentsColor = new Label
        {
            ToolTip = "Color of pattern extent box in viewport",
            BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().extents_Color)
        };
        pSetSize(lbl_extentsColor, label_Height, label_Height);
        c4TL.Rows[0].Cells.Add(lbl_extentsColor);

        lbl_extentsColor_name = new Label {ToolTip = "Color of pattern extent box in viewport", Text = "Extents"};
        c4TL.Rows[0].Cells.Add(lbl_extentsColor_name);
    }

    private void pOpenGLRow2(TableCell tc)
    {
        Panel p = new();
        tc.Control = p;

        btn_resetColors = new Button {Text = "Reset"};
        pSetSize(btn_resetColors, 60, 21);
        tc.Control = TableLayout.AutoSized(btn_resetColors, centered: true);
    }

    private void pLaunchHelp(object sender, EventArgs e)
    {
        if (helpAvailable)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(helpPath)
                {
                    UseShellExecute = true
                }
            }.Start();
        }
    }

    private void pCommandsUI()
    {
        Closed += pQuitHandler;

        quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
        quitCommand.Executed += pQuit;
        Application.Instance.Terminating += pQuitHandler;

        aboutCommand = new Command { MenuText = "About..." };
        aboutCommand.Executed += pAboutMe;

        helpCommand = new Command { MenuText = "Help...", Shortcut = Keys.F1 };
        helpCommand.Executed += pLaunchHelp;

        copyLayer = new Command { MenuText = "Copy", ToolBarText = "Copy", Shortcut = Application.Instance.CommonModifier | Keys.C };
        copyLayer.Executed += pCopyHandler;

        pasteLayer = new Command { MenuText = "Paste", ToolBarText = "Paste", Shortcut = Application.Instance.CommonModifier | Keys.V };
        pasteLayer.Executed += pPasteHandler;
        pasteLayer.Enabled = commonVars.stitcher.isCopySet();

        newSim = new Command { MenuText = "New", ToolBarText = "New", Shortcut = Application.Instance.CommonModifier | Keys.N };
        newSim.Executed += pNewHandler;
        openSim = new Command { MenuText = "Open", ToolBarText = "Open", Shortcut = Application.Instance.CommonModifier | Keys.O };
        openSim.Executed += pOpenHandler;

        fromLayout = new Command { MenuText = "Layout", ToolBarText = "Layout", Shortcut = Application.Instance.CommonModifier | Keys.I };
        fromLayout.Executed += pFromLayoutHandler;

        revertSim = new Command { MenuText = "Revert", ToolBarText = "Revert", Shortcut = Application.Instance.CommonModifier | Keys.R };
        revertSim.Executed += pRevertHandler;
        revertSim.Enabled = false;

        saveSim = new Command { MenuText = "Save", ToolBarText = "Save", Shortcut = Application.Instance.CommonModifier | Keys.S };
        saveSim.Executed += pSaveHandler;

        saveAsSim = new Command { MenuText = "Save As", ToolBarText = "Save As", Shortcut = Application.Instance.CommonModifier | Keys.Shift | Keys.S };
        saveAsSim.Executed += pSaveAsHandler;

        fileMenu = new ButtonMenuItem { Text = "&File", Items = { newSim, openSim, fromLayout, revertSim, saveSim, saveAsSim } };
        editMenu = new ButtonMenuItem { Text = "&Edit", Items = { copyLayer, pasteLayer } };

        // create menu
        Menu = new MenuBar
        {
            Items = {
                fileMenu,
                editMenu
            },
            /*ApplicationItems = {
                // application (OS X) or file menu (others)
                new ButtonMenuItem { Text = "&Preferences..." },
            },*/
            QuitItem = quitCommand,
            HelpItems = {
                helpCommand
            },
            AboutItem = aboutCommand
        };

        helpCommand.Enabled = helpAvailable;
    }

    private void pPatternSettingsUI()
    {
        settings_tl = new TableLayout();
        Panel p = new() {Content = settings_tl};

        settings = new Panel {Size = new Size(540, 270), Content = p};
            
        main_tl = new TableLayout();
        Panel pMain = new() {Content = main_tl};

        TableRow main_tr = new();
        main_tl.Rows.Add(main_tr);
        TableCell main_tc0 = new();
        main_tr.Cells.Add(main_tc0);

        left_tl = new TableLayout();
        main_tc0.Control = left_tl;

        right_tl = new TableLayout();
        Scrollable right_s = new() {Content = right_tl};

        TableCell main_tc1 = new() {Control = right_s};
        main_tr.Cells.Add(main_tc1);

        settings_tl.Rows.Add(new TableRow { ScaleHeight = true });
        settings_tl.Rows[^1].Cells.Add(new TableCell { Control = pMain });

        progress_tl = new TableLayout();
        Panel progressPanel = new() {Content = progress_tl};
        TableRow progress_tr = new();
        progress_tl.Rows.Add(progress_tr);

        progressLabel = new Label {Text = ""};
        pSetSize(progressLabel, listBox_entries_Width, label_Height * 2);
        TableCell progress_tc0 = new() {Control = progressLabel};
        progress_tr.Cells.Add(progress_tc0);

        progressBar = new ProgressBar {Height = 10};
        TableCell progress_tc1 = new() {Control = progressBar, ScaleWidth = true};
        progress_tr.Cells.Add(progress_tc1);

        btn_Cancel = new Button {Text = "Cancel", Enabled = false};
        btn_Cancel.Click += pCancelLayoutProcessing;
        TableCell progress_tc2 = new() {Control = TableLayout.AutoSized(btn_Cancel)};
        progress_tr.Cells.Add(progress_tc2);
            
        settings_tl.Rows.Add(new TableRow());
        settings_tl.Rows[^1].Cells.Add(new TableCell { Control = progressPanel });

        pPatternSettingsUI_2();
    }

    private void pPatternSettingsUI_2()
    {
        TableRow left_layout_tr = new();
        left_tl.Rows.Add(left_layout_tr);

        Panel layout_pnl = new();
        TableLayout layout_tl = new();
        layout_pnl.Content = TableLayout.AutoSized(layout_tl);
        left_layout_tr.Cells.Add(new TableCell { Control = layout_pnl });

        layout_tl.Rows.Add(new TableRow());

        btn_layout = new Button {Text = "From File", ToolTip = "Use layout to define pattern."};
        btn_layout.Click += pFromLayoutHandler;

        comboBox_layout_structures = new DropDown();
        comboBox_layout_structures.BindDataContext(c => c.DataStore, (UIStringLists m) => m.geoCoreStructureList_exp);
        comboBox_layout_structures.SelectedIndexChanged += pProcessLayout;

        layout_tl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(btn_layout) });

        layout_tl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_layout_structures), ScaleWidth = true });

        TableRow left_tr0 = new() {ScaleHeight = true};
        left_tl.Rows.Add(left_tr0);

        listBox_entries = new ListBox {ContextMenu = listbox_menu};
        pSetSize(listBox_entries, listBox_entries_Width, listBox_entries_Height);
        listBox_entries.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames);
        listBox_entries.SelectedIndexChanged += pUpdatePatternElementUI;

        TableCell left_tr0_0 = new();
        left_tr0.Cells.Add(left_tr0_0);

        Panel listBox_pnl = new() {Content = listBox_entries};
        left_tr0_0.Control = listBox_pnl;

        TableLayout settings_table = new();
        Panel lowerLeftContainer = new() {Content = settings_table};

        TableRow left_tr1 = new();
        left_tl.Rows.Add(left_tr1);
        TableCell left_tr1_0 = new();
        left_tr1.Cells.Add(left_tr1_0);
        left_tr1_0.Control = lowerLeftContainer;

        settings_table.Rows.Add(new TableRow());

        text_patternElement = new TextBox {ToolTip = "Name of pattern element."};
        settings_table.Rows[^1].Cells.Add(new TableCell { Control = text_patternElement });

        settings_table.Rows.Add(new TableRow());
        TableLayout bTable = new();
        Panel bPanel = new() {Content = bTable};
        settings_table.Rows[^1].Cells.Add(new TableCell { Control = bPanel });

        bTable.Rows.Add(new TableRow());

        entry_Add = new Button();

        const int entry_btnWidth = listBox_entries_Width / 3;
        entry_Add.Text = "Add";
        entry_Add.ToolTip = "Add new pattern entry (duplicates will not be added).";
        pSetSize(entry_Add, entry_btnWidth, 21);
        bTable.Rows[^1].Cells.Add(new TableCell { Control = entry_Add });

        entry_Rename = new Button {Text = "Rename", ToolTip = "Rename selected pattern entry."};
        pSetSize(entry_Rename, entry_btnWidth, 21);
        bTable.Rows[^1].Cells.Add(new TableCell { Control = entry_Rename });

        entry_Remove = new Button {Text = "Remove", ToolTip = "Remove selected pattern entry."};
        pSetSize(entry_Remove, entry_btnWidth, 21);
        bTable.Rows[^1].Cells.Add(new TableCell { Control = entry_Remove });

        settings_table.Rows.Add(new TableRow());
        TableLayout row2tl = new();
        Panel row2panel = new() {Content = row2tl};
        settings_table.Rows[^1].Cells.Add(new TableCell { Control = row2panel });

        row2tl.Rows.Add(new TableRow());

        lbl_viewportZoom = new Label {Text = "Zoom"};

        int nW = 55;
        num_viewportZoom = new NumericStepper {Value = 1.0f, DecimalPlaces = 2, MinValue = 1E-2};
        num_viewportZoom.LostFocus += pZoomChanged;
        pSetSize(num_viewportZoom, nW, num_Height);

        TableLayout row2leftTL = new();
        Panel row2left = new() {Content = row2leftTL};
        row2tl.Rows[^1].Cells.Add(new TableCell { Control = row2left });

        row2leftTL.Rows.Add(new TableRow());
        row2leftTL.Rows[^1].Cells.Add(new TableCell { Control = lbl_viewportZoom });

        row2leftTL.Rows[^1].Cells.Add(new TableCell { Control = num_viewportZoom });

        row2leftTL.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

        checkBox_suspendBuild = new CheckBox
        {
            Text = "Suspend", ToolTip = "If checked, do not rebuild the quilt with changes."
        };

        row2tl.Rows[^1].Cells.Add(new TableCell { Control = checkBox_suspendBuild });

        checkBox_showInput = new CheckBox
        {
            Text = "Input",
            ToolTip = "If checked, show subshapes in viewport, rather than the final shapes.",
            Checked = true
        };

        row2tl.Rows[^1].Cells.Add(new TableCell { Control = checkBox_showInput });

        row2tl.Rows[^1].Cells.Add(new TableCell { Control = null });

        settings_table.Rows.Add(new TableRow());
        TableLayout row3tl = new();
        Panel row3panel = new() {Content = row3tl};
        settings_table.Rows[^1].Cells.Add(new TableCell { Control = row3panel });

        row3tl.Rows.Add(new TableRow());

        lbl_viewportPos = new Label {Text = "Position"};

        row3tl.Rows[^1].Cells.Add(new TableCell { Control = lbl_viewportPos });

        num_viewportX = new NumericStepper
        {
            Value = 0.0f, DecimalPlaces = 2, ToolTip = "X coordinate at center of viewport."
        };
        pSetSize(num_viewportX, nW, num_Height);
        num_viewportX.LostFocus += pPosChanged;

        row3tl.Rows[^1].Cells.Add(new TableCell { Control = num_viewportX });

        num_viewportY = new NumericStepper
        {
            Value = 0.0f, DecimalPlaces = 2, ToolTip = "Y coordinate at center of viewport."
        };
        pSetSize(num_viewportY, nW, num_Height);
        num_viewportY.LostFocus += pPosChanged;

        row3tl.Rows[^1].Cells.Add(new TableCell { Control = num_viewportY });

        row3tl.Rows[^1].Cells.Add(new TableCell { Control = null });

        settings_table.Rows.Add(new TableRow());
        TableLayout row4tl = new();
        Panel row4panel = new() {Content = row4tl};
        settings_table.Rows[^1].Cells.Add(new TableCell { Control = row4panel });

        row4tl.Rows.Add(new TableRow());

        lbl_padding = new Label {Text = "Padding"};

        row4tl.Rows[^1].Cells.Add(new TableCell { Control = lbl_padding });

        num_padding = new NumericStepper
        {
            MinValue = 0,
            DecimalPlaces = 2,
            Increment = 0.1,
            Value = 0,
            ToolTip = "Padding to apply in both directions between patterns in quilt."
        };
        pSetSize(num_padding, 55, num_Height);

        row4tl.Rows[^1].Cells.Add(new TableCell { Control = num_padding });

        row4tl.Rows[^1].Cells.Add(new TableCell { Control = null });

        lbl_patNum = new Label {Text = "#"};

        row4tl.Rows[^1].Cells.Add(new TableCell { Control = lbl_patNum });

        nW = 75;
        num_patNum = new NumericStepper
        {
            MinValue = 0,
            Increment = 1,
            Value = 0,
            MaxValue = 0,
            ToolTip = "Go to this pattern in the quilt."
        };
        num_patNum.LostFocus += pGoToPattern;
        pSetSize(num_patNum, nW, num_Height);

        patternIndex = 0;

        row4tl.Rows[^1].Cells.Add(new TableCell { Control = num_patNum });

        row4tl.Rows[^1].Cells.Add(new TableCell { Control = null });

        settings_table.Rows.Add(new TableRow());
        TableLayout row5tl = new();
        Panel row5panel = new() {Content = row5tl};
        settings_table.Rows[^1].Cells.Add(new TableCell { Control = row5panel });

        row5tl.Rows.Add(new TableRow());

        btn_export = new Button
        {
            Text = "Export", ToolTip = "Export quilt to layout file, rebuilding if suspended."
        };
        pSetSize(btn_export, 200, 21);
        row5tl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(btn_export) });

        row5tl.Rows[^1].Cells.Add(new TableCell { Control = null });
    }

    private void pUpdatePrefsUI()
    {
        checkBox_OGLAA.Checked = quiltContext.AA;
        checkBox_OGLFill.Checked = quiltContext.filledPolygons;
        checkBox_OGLPoints.Checked = quiltContext.drawPoints;
        checkBox_drawExtents.Checked = quiltContext.drawExtents;
        checkBox_verticalRectDecomp.Checked = quiltContext.verticalRectDecomp;
        num_zoomSpeed.Value = quiltContext.openGLZoomFactor;
        num_fgOpacity.Value = quiltContext.FGOpacity;
        num_bgOpacity.Value = quiltContext.BGOpacity;
        num_angularTolerance.Value = quiltContext.angularTolerance;
        lbl_axisColor.BackgroundColor = Color.FromArgb(quiltContext.colors.axis_Color.toArgb());
        lbl_majorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.major_Color.toArgb());
        lbl_minorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.minor_Color.toArgb());
        lbl_vpbgColor.BackgroundColor = Color.FromArgb(quiltContext.colors.background_Color.toArgb());
        lbl_enabledColor.BackgroundColor = Color.FromArgb(quiltContext.colors.enabled_Color.toArgb());
        lbl_backgroundColor.BackgroundColor = Color.FromArgb(quiltContext.colors.deselected_Color.toArgb());
        lbl_extentsColor.BackgroundColor = Color.FromArgb(quiltContext.colors.extents_Color.toArgb());
        lbl_ss1Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape1_Color.toArgb());
        lbl_ss2Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape2_Color.toArgb());
        lbl_ss3Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape3_Color.toArgb());

        pPreviewUpdate();
    }
}