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
using Veldrid;

namespace Quilt
{
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

            public List<string> shapes { get; set; }
            public List<string> subShapePos { get; set; }
            public List<string> subShapeHorPos { get; set; }
            public List<string> subShapeVerPos { get; set; }

            public ObservableCollection<string> geoCoreStructureList_exp { get; set; }

            public ObservableCollection<string> subShapeList { get; set; }
            public List<string> openGLMode { get; set; }
        }



        string helpPath;
        bool helpAvailable;

        bool suspendBuild; // to allow user to suspend quilt build.

        QuiltContext quiltContext;

        Command quitCommand, helpCommand, aboutCommand, copyLayer, pasteLayer, newSim, openSim, fromLayout, revertSim, saveSim, saveAsSim;

        OVPSettings ovpSettings;

        CreditsScreen aboutBox;

        object drawingLock;

        CommonVars commonVars;

        void pSetupUIDataContext()
        {
            DataContext = new UIStringLists
            {
                shapes = commonVars.getAvailableShapes(),
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
                subShapePos = commonVars.getAvailableShapePositions(),
                subShapeHorPos = commonVars.getAvailableHorShapePositions(),
                subShapeVerPos = commonVars.getAvailableVerShapePositions(),
                subShapeList = commonVars.subshapes,
                geoCoreStructureList_exp = commonVars.structureList_exp,
                openGLMode = commonVars.openGLModeList
            };
        }

        void pLoadPrefs()
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
            catch (Exception)
            {
            }

            try
            {
                quiltContext.filledPolygons = Convert.ToBoolean(prefs.Descendants("openGL").Descendants("openGLFilledPolygons").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                quiltContext.drawExtents = Convert.ToBoolean(prefs.Descendants("openGL").Descendants("drawExtents").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                quiltContext.drawPoints = Convert.ToBoolean(prefs.Descendants("openGL").Descendants("openGLPoints").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                quiltContext.openGLZoomFactor = Convert.ToInt32(prefs.Descendants("openGL").Descendants("openGLZoomFactor").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                quiltContext.BGOpacity = Convert.ToDouble(prefs.Descendants("openGL").Descendants("openGLBGOpacity").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                quiltContext.FGOpacity = Convert.ToDouble(prefs.Descendants("openGL").Descendants("openGLFGOpacity").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                quiltContext.angularTolerance = Convert.ToDouble(prefs.Descendants("misc").Descendants("angularTolerance").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                quiltContext.verticalRectDecomp = Convert.ToBoolean(prefs.Descendants("misc").Descendants("verticalRectDecomp").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "selectedColor";
                quiltContext.colors.selected_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").Single().Value);
                quiltContext.colors.selected_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").Single().Value);
                quiltContext.colors.selected_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").Single().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "deselectedColor";
                quiltContext.colors.deselected_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").Single().Value);
                quiltContext.colors.deselected_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").Single().Value);
                quiltContext.colors.deselected_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").Single().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "subshape1Color";
                quiltContext.colors.subshape1_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.subshape1_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.subshape1_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "subshape2Color";
                quiltContext.colors.subshape2_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.subshape2_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.subshape2_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "subshape3Color";
                quiltContext.colors.subshape3_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.subshape3_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.subshape3_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "axisColor";
                quiltContext.colors.axis_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.axis_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.axis_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "majorColor";
                quiltContext.colors.major_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.major_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.major_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "minorColor";
                quiltContext.colors.minor_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.minor_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.minor_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "backgroundColor";
                quiltContext.colors.background_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.background_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.background_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            try
            {
                string layerCol = "extentsColor";
                quiltContext.colors.extents_Color.R = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("R").First().Value);
                quiltContext.colors.extents_Color.G = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("G").First().Value);
                quiltContext.colors.extents_Color.B = Convert.ToInt32(prefs.Descendants("colors").Descendants(layerCol).Descendants("B").First().Value);
            }
            catch (Exception)
            {
            }

            quiltContext.colors.rebuildLists();
        }

        void pSavePrefs()
        {
            string filename = EtoEnvironment.GetFolderPath(EtoSpecialFolder.ApplicationSettings);
            filename += Path.DirectorySeparatorChar + "quilt_prefs.xml";

            try
            {
                XDocument prefsXML = new XDocument(
                    new XElement("root"));
                // ReSharper disable once PossibleNullReferenceException
                prefsXML.Root.Add(new XElement("version", CentralProperties.version));

                XElement openGLPrefs = new XElement("openGL",
                    new XElement("openGLAA", quiltContext.AA),
                    new XElement("openGLFilledPolygons", quiltContext.filledPolygons),
                    new XElement("openGLPoints", quiltContext.drawPoints),
                    new XElement("openGLZoomFactor", quiltContext.openGLZoomFactor),
                    new XElement("openGLFGOpacity", quiltContext.FGOpacity),
                    new XElement("openGLBGOpacity", quiltContext.BGOpacity),
                    new XElement("drawExtents", quiltContext.drawExtents));
                prefsXML.Root.Add(openGLPrefs);

                XElement miscPrefs = new XElement("misc",
                    new XElement("angularTolerance", quiltContext.angularTolerance),
                    new XElement("verticalRectDecomp", quiltContext.verticalRectDecomp));
                prefsXML.Root.Add(miscPrefs);

                XElement colorPrefs = new XElement("colors");

                XElement subshape1Color = new XElement("subshape1Color",
                    new XElement("R", quiltContext.colors.subshape1_Color.R),
                    new XElement("G", quiltContext.colors.subshape1_Color.G),
                    new XElement("B", quiltContext.colors.subshape1_Color.B));
                colorPrefs.Add(subshape1Color);

                XElement subshape2Color = new XElement("subshape2Color",
                    new XElement("R", quiltContext.colors.subshape2_Color.R),
                    new XElement("G", quiltContext.colors.subshape2_Color.G),
                    new XElement("B", quiltContext.colors.subshape2_Color.B));
                colorPrefs.Add(subshape2Color);

                XElement subshape3Color = new XElement("subshape3Color",
                    new XElement("R", quiltContext.colors.subshape3_Color.R),
                    new XElement("G", quiltContext.colors.subshape3_Color.G),
                    new XElement("B", quiltContext.colors.subshape3_Color.B));
                colorPrefs.Add(subshape3Color);

                XElement enabledColor = new XElement("selectedColor",
                    new XElement("R", quiltContext.colors.selected_Color.R),
                    new XElement("G", quiltContext.colors.selected_Color.G),
                    new XElement("B", quiltContext.colors.selected_Color.B));
                colorPrefs.Add(enabledColor);

                XElement backgroundColor = new XElement("deselectedColor",
                    new XElement("R", quiltContext.colors.deselected_Color.R),
                    new XElement("G", quiltContext.colors.deselected_Color.G),
                    new XElement("B", quiltContext.colors.deselected_Color.B));
                colorPrefs.Add(backgroundColor);

                XElement axisColor = new XElement("axisColor",
                    new XElement("R", quiltContext.colors.axis_Color.R),
                    new XElement("G", quiltContext.colors.axis_Color.G),
                    new XElement("B", quiltContext.colors.axis_Color.B));
                colorPrefs.Add(axisColor);

                XElement majorColor = new XElement("majorColor",
                    new XElement("R", quiltContext.colors.major_Color.R),
                    new XElement("G", quiltContext.colors.major_Color.G),
                    new XElement("B", quiltContext.colors.major_Color.B));
                colorPrefs.Add(majorColor);

                XElement minorColor = new XElement("minorColor",
                    new XElement("R", quiltContext.colors.minor_Color.R),
                    new XElement("G", quiltContext.colors.minor_Color.G),
                    new XElement("B", quiltContext.colors.minor_Color.B));
                colorPrefs.Add(minorColor);

                XElement vpBackgroundColor = new XElement("backgroundColor",
                    new XElement("R", quiltContext.colors.background_Color.R),
                    new XElement("G", quiltContext.colors.background_Color.G),
                    new XElement("B", quiltContext.colors.background_Color.B));
                colorPrefs.Add(vpBackgroundColor);

                XElement extentsColor = new XElement("extentsColor",
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

        void pSetUI(bool status)
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

        void pMainForm(QuiltContext _quiltContext)
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

        void pSetUpVeldrid()
        {
            if (!(FormReady && VeldridReady))
            {
                return;
            }

            viewPort.SetUpVeldrid();

            // Title = $"Veldrid backend: {vSurface.Backend.ToString()}";

            viewPort.Clock.Start();
            pCreateVPContextMenu();
        }

        void pCreateLBContextMenu()
        {
            listbox_menu = new ContextMenu();
            int itemIndex = 0;
            lb_copy = new ButtonMenuItem() { Text = "Copy" };
            listbox_menu.Items.Add(lb_copy);
            listbox_menu.Items[itemIndex].Click += delegate
            {
                pCopy();
            };
            itemIndex++;
            lb_paste = new ButtonMenuItem() { Text = "Paste" };
            listbox_menu.Items.Add(lb_paste);
            listbox_menu.Items[itemIndex].Click += delegate
            {
                pPaste();
            };
            itemIndex++;
            listbox_menu.Items.Add(new ButtonMenuItem() { Text = "Remove" });
            listbox_menu.Items[itemIndex].Click += delegate
            {
                pRemovePatternElement();
            };
            itemIndex++;
            listbox_menu.Items.Add(new ButtonMenuItem() { Text = "Duplicate" });
            listbox_menu.Items[itemIndex].Click += delegate
            {
                pDuplicatePatternElement();
            };
        }

        void pUpdateLBContextMenu()
        {
            try
            {
                lb_paste.Enabled = commonVars.stitcher.isCopySet();
            }
            catch (Exception)
            {
                lb_paste.Enabled = false;
            }
        }

        void pUI(QuiltContext _quiltContext)
        {
            if (_quiltContext == null) // safety net.
            {
                quiltContext = new QuiltContext("", VeldridSurface.PreferredBackend);
            }
            else
            {
                quiltContext = _quiltContext;
            }


            Shown += (sender, e) => FormReady = true;

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

            Panel vp = new Panel();

            TableLayout vp_tl = new TableLayout();

            vp.Content = vp_tl;

            vp_tl.Rows.Add(new TableRow());

            pPatternSettingsUI();
            Scrollable settings_sc = new Scrollable {Content = settings};

            quiltUISplitter = new Panel
            {
                Content = new Splitter
                {
                    Orientation = Orientation.Horizontal,
                    FixedPanel = SplitterFixedPanel.Panel1,
                    Panel1 = settings_sc,
                    Panel2 = vp,
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

            GraphicsDeviceOptions options = new GraphicsDeviceOptions(
                                    false,
                                    Veldrid.PixelFormat.R32_Float,
                                    false,
                                    ResourceBindingModel.Improved);

            vSurface = new VeldridSurface(quiltContext.backend, options);

            vSurface.VeldridInitialized += (sender, e) => VeldridReady = true;


            string exeDirectory;
            string shaders;
            if (Platform.IsMac)
            {
                // AppContext.BaseDirectory is too simple for the case of the Mac
                // projects. When building an app bundle that depends on the Mono
                // framework being installed, it properly returns the path of the
                // executable in Eto.Veldrid.app/Contents/MacOS. When building an
                // app bundle that instead bundles Mono by way of mkbundle, on the
                // other hand, it returns the directory containing the .app.

                // ReSharper disable once PossibleNullReferenceException
                exeDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
				shaders = Path.Combine("..", "Resources", "shaders");
            }
            else
            {
                exeDirectory = AppContext.BaseDirectory;
                shaders = "shaders";
            }


            viewPort = new VeldridDriver(ref ovpSettings, ref vSurface)
            {
                Surface = vSurface,
                ExecutableDirectory = exeDirectory,
                ShaderSubdirectory = shaders
            };

            vSurface.Size = new Size(viewportSize, viewportSize);
            viewPort.updateHostFunc = pViewportUpdateHost;
            viewPort.updateHostSelectionFunc = pViewportSelectionFunc;

            string viewportToolTipText = "(w/a/s/d) to navigate\r\n(r) to reset\r\n(n/m) to zoom\r\n(f) to freeze/thaw\r\n(x) to zoom extents\r\n(z) to zoom selected";
            vp.ToolTip = viewportToolTipText;

            commonVars.titleText += " " + vSurface.Backend;

            Title = commonVars.titleText;

            vp_tl.Rows[0].Cells.Add(new TableCell() { Control = vSurface });

            if (quiltContext.xmlFileArg != "")
            {
                pDoLoad(quiltContext.xmlFileArg);
            }

            tabControl.DragEnter += pDragEvent;
            tabControl.DragOver += pDragEvent;

            tabControl.DragDrop += pDragAndDrop;
        }

        void pInterfaceUpdate(object sender, EventArgs e)
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

        void pPrefsUI()
        {
            prefsPanel = new Panel();
            TableLayout prefsPanel_table = new TableLayout();
            prefsPanel.Content = prefsPanel_table;

            prefsPanel_table.Rows.Add(new TableRow());

            TableCell opengl_tc = new TableCell();

            prefsPanel_table.Rows[^1].Cells.Add(opengl_tc);

            pOpenGL_settings_utilsUISetup(opengl_tc);

            TableCell padding = new TableCell();
            prefsPanel_table.Rows[^1].Cells.Add(padding);

            prefsPanel_table.Rows.Add(new TableRow());

            TableCell misc_tc = new TableCell();

            prefsPanel_table.Rows[^1].Cells.Add(misc_tc);

            TableCell padding2 = new TableCell();
            prefsPanel_table.Rows[^1].Cells.Add(padding2);

            prefsPanel_table.Rows.Add(new TableRow()); // padding

            pMisc_settings_utilsUISetup(misc_tc);



            pAddPrefsHandlers();
        }

        void pMisc_settings_utilsUISetup(TableCell tc)
        {
            GroupBox groupBox_misc = new GroupBox();
            tc.Control = groupBox_misc;
            TableLayout groupBox_misc_table = new TableLayout();
            groupBox_misc.Text = "Misc";
            groupBox_misc.Content = groupBox_misc_table;

            groupBox_misc_table.Rows.Add(new TableRow());
            TableCell row0 = new TableCell();
            groupBox_misc_table.Rows[^1].Cells.Add(row0);

            Panel angTol_pnl = new Panel();
            TableLayout angTol_table = new TableLayout();
            angTol_pnl.Content = TableLayout.AutoSized(angTol_table);
            row0.Control = angTol_pnl;

            angTol_table.Rows.Add(new TableRow());

            lbl_angularTolerance = new Label
            {
                Text = "Angular Tolerance",
                ToolTip =
                    "This value is used as the 'error' accepted when removing co-linear vertices from imported layout."
            };
            angTol_table.Rows[^1].Cells.Add(new TableCell() { Control = lbl_angularTolerance });

            num_angularTolerance = new NumericStepper
            {
                ToolTip =
                    "This value is used as the 'error' accepted when removing co-linear vertices from imported layout.",
                MinValue = 0.01f,
                Increment = 0.01f,
                DecimalPlaces = 2
            };
            pSetSize(num_angularTolerance, 50, num_Height);
            angTol_table.Rows[^1].Cells.Add(new TableCell() { Control = num_angularTolerance });

            checkBox_verticalRectDecomp = new CheckBox
            {
                ToolTip =
                    "Decompose layout to vertically-oriented rectangles when set.\r\nUse horizontally-oriented rectangles, if cleared.",
                Text = "Vertical Decomp"
            };
            groupBox_misc_table.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_verticalRectDecomp });
        }

        void pOpenGL_settings_utilsUISetup(TableCell tc)
        {
            TableLayout groupBox_openGL_table = new TableLayout();
            GroupBox groupBox_openGL = new GroupBox {Text = "OpenGL", Content = groupBox_openGL_table};
            tc.Control = groupBox_openGL;

            groupBox_openGL_table.Rows.Add(new TableRow());
            TableCell row0 = new TableCell();
            groupBox_openGL_table.Rows[^1].Cells.Add(row0);
            pOpenGLRow0(row0);
            TableCell row0padding = new TableCell();
            groupBox_openGL_table.Rows[^1].Cells.Add(row0padding);

            groupBox_openGL_table.Rows.Add(new TableRow());
            TableCell row1 = new TableCell();
            groupBox_openGL_table.Rows[^1].Cells.Add(row1);
            pOpenGLRow1(row1);
            TableCell row1padding = new TableCell();
            groupBox_openGL_table.Rows[^1].Cells.Add(row1padding);

            groupBox_openGL_table.Rows.Add(new TableRow());
            TableCell row2 = new TableCell();
            groupBox_openGL_table.Rows[^1].Cells.Add(row2);
            pOpenGLRow2(row2);
            TableCell row2padding = new TableCell();
            groupBox_openGL_table.Rows[^1].Cells.Add(row2padding);
        }

        void pOpenGLRow0(TableCell tc)
        {
            Panel p = new Panel();
            tc.Control = p;

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            Panel lRow0 = new Panel();
            tl.Rows[^1].Cells.Add(new TableCell() { Control = lRow0 });

            Panel rRow0 = new Panel();
            tl.Rows[^1].Cells.Add(new TableCell() { Control = rRow0 });

            TableLayout lRow0tl = new TableLayout();
            lRow0.Content = lRow0tl;
            lRow0tl.Rows.Add(new TableRow());

            TableLayout rRow0tl = new TableLayout();
            rRow0.Content = rRow0tl;
            rRow0tl.Rows.Add(new TableRow());

            Panel zoomPnl = new Panel();
            TableLayout zoomTL = new TableLayout();
            zoomPnl.Content = zoomTL;
            lRow0tl.Rows[^1].Cells.Add(new TableCell() { Control = zoomPnl });

            zoomTL.Rows.Add(new TableRow());

            lbl_zoomSpeed = new Label {Text = "Zoom Increment"};
            zoomTL.Rows[^1].Cells.Add(new TableCell() { Control = lbl_zoomSpeed });

            num_zoomSpeed = new NumericStepper {MinValue = 1, Increment = 1};
            pSetSize(num_zoomSpeed, 50, num_Height);
            zoomTL.Rows[^1].Cells.Add(new TableCell() { Control = num_zoomSpeed });

            zoomTL.Rows[^1].Cells.Add(new TableCell() { Control = null }); // padding

            TableLayout fgOpTL = new TableLayout();
            Panel fgOpPnl = new Panel {Content = fgOpTL};
            rRow0tl.Rows[^1].Cells.Add(new TableCell() { Control = fgOpPnl });

            fgOpTL.Rows.Add(new TableRow());

            lbl_fgOpacity = new Label
            {
                Text = "FG Opacity", ToolTip = "Opacity of selected pattern elements in viewport."
            };
            fgOpTL.Rows[^1].Cells.Add(new TableCell() { Control = lbl_fgOpacity });

            num_fgOpacity = new NumericStepper
            {
                ToolTip = "Opacity of selected pattern elements in viewport.",
                MinValue = 0.01f,
                Increment = 0.1f,
                DecimalPlaces = 2
            };
            pSetSize(num_fgOpacity, 50, num_Height);
            fgOpTL.Rows[^1].Cells.Add(new TableCell() { Control = num_fgOpacity });

            TableLayout bgOpTL = new TableLayout();
            Panel bgOpPnl = new Panel {Content = bgOpTL};
            rRow0tl.Rows[^1].Cells.Add(new TableCell() { Control = bgOpPnl });

            bgOpTL.Rows.Add(new TableRow());

            lbl_bgOpacity = new Label
            {
                Text = "BG Opacity", ToolTip = "Opacity of not-selected pattern elements in viewport."
            };
            bgOpTL.Rows[^1].Cells.Add(new TableCell() { Control = lbl_bgOpacity });

            num_bgOpacity = new NumericStepper
            {
                ToolTip = "Opacity of not-selected pattern elements in viewport.",
                MinValue = 0.01f,
                Increment = 0.1f,
                DecimalPlaces = 2
            };
            pSetSize(num_bgOpacity, 50, num_Height);
            bgOpTL.Rows[^1].Cells.Add(new TableCell() { Control = num_bgOpacity });

            tl.Rows[^1].Cells.Add(new TableCell() { Control = null }); // padding

            tl.Rows.Add(new TableRow());

            TableLayout lRow1tl = new TableLayout();
            Panel lRow1 = new Panel {Content = lRow1tl};
            tl.Rows[^1].Cells.Add(new TableCell() { Control = lRow1 });

            TableLayout rRow1tl = new TableLayout();
            Panel rRow1 = new Panel {Content = rRow1tl};
            tl.Rows[^1].Cells.Add(new TableCell() { Control = rRow1 });

            tl.Rows[^1].Cells.Add(new TableCell() { Control = null }); // padding

            lRow1tl.Rows.Add(new TableRow());

            rRow1tl.Rows.Add(new TableRow());

            TableLayout dispOptsTL = new TableLayout();
            Panel dispOptsPnl = new Panel {Content = dispOptsTL};
            lRow1tl.Rows[^1].Cells.Add(new TableCell() { Control = dispOptsPnl });

            dispOptsTL.Rows.Add(new TableRow());

            checkBox_OGLAA = new CheckBox {Text = "Antialiasing"};
            dispOptsTL.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_OGLAA });

            checkBox_OGLFill = new CheckBox {Text = "Fill", ToolTip = "Draw filled polygons, if set."};
            dispOptsTL.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_OGLFill });

            checkBox_OGLPoints = new CheckBox {Text = "Points", ToolTip = "Draw points in viewport, if set."};
            dispOptsTL.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_OGLPoints });

            checkBox_drawExtents = new CheckBox
            {
                Text = "Extents", ToolTip = "Draw pattern extents in viewport, if set."
            };
            dispOptsTL.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_drawExtents });
        }

        void pOpenGLRow1(TableCell tc)
        {
            TableLayout swatchesTL = new TableLayout();
            Panel p = new Panel {Content = swatchesTL};
            tc.Control = p;

            swatchesTL.Rows.Add(new TableRow());

            TableRow row0 = new TableRow();
            swatchesTL.Rows.Add(row0);

            pOpengl_swatchrow0(row0);

            TableRow row1 = new TableRow();
            swatchesTL.Rows.Add(row1);

            pOpengl_swatchrow1(row1);

            TableRow padding = new TableRow();
            swatchesTL.Rows.Add(padding);
        }

        void pOpengl_swatchrow0(TableRow tr)
        {
            TableLayout c0TL = new TableLayout();
            Panel c0 = new Panel {Content = c0TL};
            tr.Cells.Add(new TableCell() { Control = c0 });

            c0TL.Rows.Add(new TableRow());

            lbl_minorGridColor = new Label
            {
                BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().minor_Color)
            };
            pSetSize(lbl_minorGridColor, label_Height, label_Height);
            c0TL.Rows[0].Cells.Add(lbl_minorGridColor);

            lbl_minorGridColor_name = new Label {Text = "Minor Grid"};
            c0TL.Rows[0].Cells.Add(lbl_minorGridColor_name);

            TableLayout c1TL = new TableLayout();
            Panel c1 = new Panel {Content = c1TL};
            tr.Cells.Add(new TableCell() { Control = c1 });

            c1TL.Rows.Add(new TableRow());

            lbl_majorGridColor = new Label
            {
                BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().major_Color)
            };
            pSetSize(lbl_majorGridColor, label_Height, label_Height);
            c1TL.Rows[0].Cells.Add(lbl_majorGridColor);

            lbl_majorGridColor_name = new Label {Text = "Major Grid"};
            c1TL.Rows[0].Cells.Add(lbl_majorGridColor_name);

            TableLayout c2TL = new TableLayout();
            Panel c2 = new Panel {Content = c2TL};
            tr.Cells.Add(new TableCell() { Control = c2 });

            c2TL.Rows.Add(new TableRow());

            lbl_axisColor = new Label {BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().axis_Color)};
            pSetSize(lbl_axisColor, label_Height, label_Height);
            c2TL.Rows[0].Cells.Add(lbl_axisColor);

            lbl_axisColor_name = new Label {Text = "Axis"};
            c2TL.Rows[0].Cells.Add(lbl_axisColor_name);

            TableLayout c3TL = new TableLayout();
            Panel c3 = new Panel {Content = c3TL};
            tr.Cells.Add(new TableCell() { Control = c3 });

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

        void pOpengl_swatchrow1(TableRow tr)
        {
            TableLayout c0TL = new TableLayout();
            Panel c0 = new Panel {Content = c0TL};
            tr.Cells.Add(new TableCell() { Control = c0 });

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

            TableLayout c0bTL = new TableLayout();
            Panel c0b = new Panel {Content = c0bTL};
            tr.Cells.Add(new TableCell() { Control = c0b });

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


            TableLayout c1TL = new TableLayout();
            Panel c1 = new Panel {Content = c1TL};
            tr.Cells.Add(new TableCell() { Control = c1 });

            c1TL.Rows.Add(new TableRow());

            lbl_ss1Color = new Label
            {
                BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape1_Color)
            };
            pSetSize(lbl_ss1Color, label_Height, label_Height);
            c1TL.Rows[0].Cells.Add(lbl_ss1Color);

            lbl_ss1Color_name = new Label {Text = "Subshape 1"};
            c1TL.Rows[0].Cells.Add(lbl_ss1Color_name);

            TableLayout c2TL = new TableLayout();
            Panel c2 = new Panel {Content = c2TL};
            tr.Cells.Add(new TableCell() { Control = c2 });

            c2TL.Rows.Add(new TableRow());

            lbl_ss2Color = new Label
            {
                BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape2_Color)
            };
            pSetSize(lbl_ss2Color, label_Height, label_Height);
            c2TL.Rows[0].Cells.Add(lbl_ss2Color);

            lbl_ss2Color_name = new Label {Text = "Subshape 2"};
            c2TL.Rows[0].Cells.Add(lbl_ss2Color_name);

            TableLayout c3TL = new TableLayout();
            Panel c3 = new Panel {Content = c3TL};
            tr.Cells.Add(new TableCell() { Control = c3 });

            c3TL.Rows.Add(new TableRow());

            lbl_ss3Color = new Label
            {
                BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape3_Color)
            };
            pSetSize(lbl_ss3Color, label_Height, label_Height);
            c3TL.Rows[0].Cells.Add(lbl_ss3Color);

            lbl_ss3Color_name = new Label {Text = "Subshape 3"};
            c3TL.Rows[0].Cells.Add(lbl_ss3Color_name);

            TableLayout c4TL = new TableLayout();
            Panel c4 = new Panel {Content = c4TL};
            tr.Cells.Add(new TableCell() { Control = c4 });

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

        void pOpenGLRow2(TableCell tc)
        {
            Panel p = new Panel();
            tc.Control = p;

            btn_resetColors = new Button {Text = "Reset"};
            pSetSize(btn_resetColors, 60, 21);
            tc.Control = TableLayout.AutoSized(btn_resetColors, centered: true);
        }

        void pLaunchHelp(object sender, EventArgs e)
        {
            if (helpAvailable)
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo(@helpPath)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }

        void pCommandsUI()
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
                    editMenu,
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

        void pPatternSettingsUI()
        {
            settings_tl = new TableLayout();
            Panel p = new Panel {Content = settings_tl};

            settings = new Panel {Size = new Size(540, 270), Content = p};
            
            main_tl = new TableLayout();
            Panel pMain = new Panel {Content = main_tl};

            TableRow main_tr = new TableRow();
            main_tl.Rows.Add(main_tr);
            TableCell main_tc0 = new TableCell();
            main_tr.Cells.Add(main_tc0);

            left_tl = new TableLayout();
            main_tc0.Control = left_tl;

            right_tl = new TableLayout();
            Scrollable right_s = new Scrollable {Content = right_tl};

            TableCell main_tc1 = new TableCell {Control = right_s};
            main_tr.Cells.Add(main_tc1);

            settings_tl.Rows.Add(new TableRow() { ScaleHeight = true });
            settings_tl.Rows[^1].Cells.Add(new TableCell() { Control = pMain });

            progress_tl = new TableLayout();
            Panel progressPanel = new Panel {Content = progress_tl};
            TableRow progress_tr = new TableRow();
            progress_tl.Rows.Add(progress_tr);

            int progressLabelWidth = listBox_entries_Width; // forcing as the UI hasn't calculated at this point.
            progressLabel = new Label {Text = ""};
            pSetSize(progressLabel, progressLabelWidth, label_Height * 2);
            TableCell progress_tc0 = new TableCell {Control = progressLabel};
            progress_tr.Cells.Add(progress_tc0);

            progressBar = new ProgressBar {Height = 10};
            TableCell progress_tc1 = new TableCell {Control = progressBar, ScaleWidth = true};
            progress_tr.Cells.Add(progress_tc1);

            btn_Cancel = new Button {Text = "Cancel", Enabled = false};
            btn_Cancel.Click += pCancelLayoutProcessing;
            TableCell progress_tc2 = new TableCell {Control = TableLayout.AutoSized(btn_Cancel)};
            progress_tr.Cells.Add(progress_tc2);
            
            settings_tl.Rows.Add(new TableRow());
            settings_tl.Rows[^1].Cells.Add(new TableCell() { Control = progressPanel });

            pPatternSettingsUI_2();
        }

        void pPatternSettingsUI_2()
        {
            TableRow left_layout_tr = new TableRow();
            left_tl.Rows.Add(left_layout_tr);

            Panel layout_pnl = new Panel();
            TableLayout layout_tl = new TableLayout();
            layout_pnl.Content = TableLayout.AutoSized(layout_tl);
            left_layout_tr.Cells.Add(new TableCell() { Control = layout_pnl });

            layout_tl.Rows.Add(new TableRow());

            btn_layout = new Button {Text = "From File", ToolTip = "Use layout to define pattern."};
            btn_layout.Click += pFromLayoutHandler;

            comboBox_layout_structures = new DropDown();
            comboBox_layout_structures.BindDataContext(c => c.DataStore, (UIStringLists m) => m.geoCoreStructureList_exp);
            comboBox_layout_structures.SelectedIndexChanged += pProcessLayout;

            layout_tl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(btn_layout) });

            layout_tl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_layout_structures), ScaleWidth = true });

            TableRow left_tr0 = new TableRow {ScaleHeight = true};
            left_tl.Rows.Add(left_tr0);

            listBox_entries = new ListBox {ContextMenu = listbox_menu};
            pSetSize(listBox_entries, listBox_entries_Width, listBox_entries_Height);
            listBox_entries.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames);
            listBox_entries.SelectedIndexChanged += pUpdatePatternElementUI;

            TableCell left_tr0_0 = new TableCell();
            left_tr0.Cells.Add(left_tr0_0);

            Panel listBox_pnl = new Panel {Content = listBox_entries};
            left_tr0_0.Control = listBox_pnl;

            TableLayout settings_table = new TableLayout();
            Panel lowerLeftContainer = new Panel {Content = settings_table};

            TableRow left_tr1 = new TableRow();
            left_tl.Rows.Add(left_tr1);
            TableCell left_tr1_0 = new TableCell();
            left_tr1.Cells.Add(left_tr1_0);
            left_tr1_0.Control = lowerLeftContainer;

            settings_table.Rows.Add(new TableRow());

            text_patternElement = new TextBox {ToolTip = "Name of pattern element."};
            settings_table.Rows[^1].Cells.Add(new TableCell() { Control = text_patternElement });

            settings_table.Rows.Add(new TableRow());
            TableLayout bTable = new TableLayout();
            Panel bPanel = new Panel {Content = bTable};
            settings_table.Rows[^1].Cells.Add(new TableCell() { Control = bPanel });

            bTable.Rows.Add(new TableRow());

            entry_Add = new Button();

            int entry_btnWidth = listBox_entries_Width / 3;
            entry_Add.Text = "Add";
            entry_Add.ToolTip = "Add new pattern entry (duplicates will not be added).";
            pSetSize(entry_Add, entry_btnWidth, 21);
            bTable.Rows[^1].Cells.Add(new TableCell() { Control = entry_Add });

            entry_Rename = new Button {Text = "Rename", ToolTip = "Rename selected pattern entry."};
            pSetSize(entry_Rename, entry_btnWidth, 21);
            bTable.Rows[^1].Cells.Add(new TableCell() { Control = entry_Rename });

            entry_Remove = new Button {Text = "Remove", ToolTip = "Remove selected pattern entry."};
            pSetSize(entry_Remove, entry_btnWidth, 21);
            bTable.Rows[^1].Cells.Add(new TableCell() { Control = entry_Remove });

            settings_table.Rows.Add(new TableRow());
            TableLayout row2tl = new TableLayout();
            Panel row2panel = new Panel {Content = row2tl};
            settings_table.Rows[^1].Cells.Add(new TableCell() { Control = row2panel });

            row2tl.Rows.Add(new TableRow());

            lbl_viewportZoom = new Label {Text = "Zoom"};

            int nW = 55;
            num_viewportZoom = new NumericStepper {Value = 1.0f, DecimalPlaces = 2, MinValue = 1E-2};
            num_viewportZoom.LostFocus += pZoomChanged;
            pSetSize(num_viewportZoom, nW, num_Height);

            TableLayout row2leftTL = new TableLayout();
            Panel row2left = new Panel {Content = row2leftTL};
            row2tl.Rows[^1].Cells.Add(new TableCell() { Control = row2left });

            row2leftTL.Rows.Add(new TableRow());
            row2leftTL.Rows[^1].Cells.Add(new TableCell() { Control = lbl_viewportZoom });

            row2leftTL.Rows[^1].Cells.Add(new TableCell() { Control = num_viewportZoom });

            row2leftTL.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            checkBox_suspendBuild = new CheckBox
            {
                Text = "Suspend", ToolTip = "If checked, do not rebuild the quilt with changes."
            };

            row2tl.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_suspendBuild });

            checkBox_showInput = new CheckBox
            {
                Text = "Input",
                ToolTip = "If checked, show subshapes in viewport, rather than the final shapes.",
                Checked = true
            };

            row2tl.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_showInput });

            row2tl.Rows[^1].Cells.Add(new TableCell() { Control = null });

            settings_table.Rows.Add(new TableRow());
            TableLayout row3tl = new TableLayout();
            Panel row3panel = new Panel {Content = row3tl};
            settings_table.Rows[^1].Cells.Add(new TableCell() { Control = row3panel });

            row3tl.Rows.Add(new TableRow());

            lbl_viewportPos = new Label {Text = "Position"};

            row3tl.Rows[^1].Cells.Add(new TableCell() { Control = lbl_viewportPos });

            num_viewportX = new NumericStepper
            {
                Value = 0.0f, DecimalPlaces = 2, ToolTip = "X coordinate at center of viewport."
            };
            pSetSize(num_viewportX, nW, num_Height);
            num_viewportX.LostFocus += pPosChanged;

            row3tl.Rows[^1].Cells.Add(new TableCell() { Control = num_viewportX });

            num_viewportY = new NumericStepper
            {
                Value = 0.0f, DecimalPlaces = 2, ToolTip = "Y coordinate at center of viewport."
            };
            pSetSize(num_viewportY, nW, num_Height);
            num_viewportY.LostFocus += pPosChanged;

            row3tl.Rows[^1].Cells.Add(new TableCell() { Control = num_viewportY });

            row3tl.Rows[^1].Cells.Add(new TableCell() { Control = null });

            settings_table.Rows.Add(new TableRow());
            TableLayout row4tl = new TableLayout();
            Panel row4panel = new Panel {Content = row4tl};
            settings_table.Rows[^1].Cells.Add(new TableCell() { Control = row4panel });

            row4tl.Rows.Add(new TableRow());

            lbl_padding = new Label {Text = "Padding"};

            row4tl.Rows[^1].Cells.Add(new TableCell() { Control = lbl_padding });

            num_padding = new NumericStepper
            {
                MinValue = 0,
                DecimalPlaces = 2,
                Increment = 0.1,
                Value = 0,
                ToolTip = "Padding to apply in both directions between patterns in quilt."
            };
            pSetSize(num_padding, 55, num_Height);

            row4tl.Rows[^1].Cells.Add(new TableCell() { Control = num_padding });

            row4tl.Rows[^1].Cells.Add(new TableCell() { Control = null });

            lbl_patNum = new Label {Text = "#"};

            row4tl.Rows[^1].Cells.Add(new TableCell() { Control = lbl_patNum });

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

            row4tl.Rows[^1].Cells.Add(new TableCell() { Control = num_patNum });

            row4tl.Rows[^1].Cells.Add(new TableCell() { Control = null });

            settings_table.Rows.Add(new TableRow());
            TableLayout row5tl = new TableLayout();
            Panel row5panel = new Panel {Content = row5tl};
            settings_table.Rows[^1].Cells.Add(new TableCell() { Control = row5panel });

            row5tl.Rows.Add(new TableRow());

            btn_export = new Button
            {
                Text = "Export", ToolTip = "Export quilt to layout file, rebuilding if suspended."
            };
            pSetSize(btn_export, 200, 21);
            row5tl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(btn_export) });

            row5tl.Rows[^1].Cells.Add(new TableCell() { Control = null });
        }

        void pUpdatePrefsUI()
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
}
