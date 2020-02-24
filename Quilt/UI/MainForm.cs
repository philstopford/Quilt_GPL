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
            public ObservableCollection<string> patternElementNames_filtered_array { get; set; }
            public ObservableCollection<string> xPosRefSubShapeList { get; set; }
            public ObservableCollection<string> yPosRefSubShapeList { get; set; }
            public List<string> shapes { get; set; }
            public List<string> subShapePos { get; set; }
            public List<string> subShapeHorPos { get; set; }
            public List<string> subShapeVerPos { get; set; }

            public ObservableCollection<string> subShapeList { get; set; }
            public List<string> openGLMode { get; set; }
        }

        int label_Height = 8;
        int num_Height = 8;
        int numWidth = 55;

        string helpPath;
        bool helpAvailable;

        bool suspendBuild; // to allow user to suspend quilt build.

        QuiltContext quiltContext;

        Command quitCommand, helpCommand, aboutCommand, copyLayer, pasteLayer, newSim, openSim, revertSim, saveSim, saveAsSim;

        OVPSettings ovpSettings;

        CreditsScreen aboutBox;

        object drawingLock;

        CommonVars commonVars;

        void setupUIDataContext()
        {
            DataContext = new UIStringLists
            {
                shapes = commonVars.getAvailableShapes(),
                patternElementNames = commonVars.stitcher.patternElementNames,
                patternElementNames_filtered = commonVars.stitcher.patternElementNames_filtered,
                patternElementNames_filtered_array = commonVars.stitcher.patternElementNames_filtered_array,
                xPosRefSubShapeList = commonVars.xPosRefSubShapeList,
                yPosRefSubShapeList = commonVars.yPosRefSubShapeList,
                subShapePos = commonVars.getAvailableShapePositions(),
                subShapeHorPos = commonVars.getAvailableHorShapePositions(),
                subShapeVerPos = commonVars.getAvailableVerShapePositions(),
                subShapeList = commonVars.subshapes,
                openGLMode = commonVars.openGLModeList
            };
        }

        void loadPrefs()
        {
            // We have to do this by hand, reading and parsing an XML file. Yay.
            string filename = EtoEnvironment.GetFolderPath(EtoSpecialFolder.ApplicationSettings);
            filename += System.IO.Path.DirectorySeparatorChar + "quilt_prefs.xml";

            XElement prefs;
            try
            {
                prefs = XElement.Load(filename);
            }
            catch (Exception)
            {
                if (System.IO.File.Exists(filename))
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

            quiltContext.colors.rebuildLists();
        }

        void savePrefs()
        {
            string filename = EtoEnvironment.GetFolderPath(EtoSpecialFolder.ApplicationSettings);
            filename += System.IO.Path.DirectorySeparatorChar + "quilt_prefs.xml";

            try
            {
                XDocument prefsXML = new XDocument(
                    new XElement("root"));
                prefsXML.Root.Add(new XElement("version", CentralProperties.version));

                XElement openGLPrefs = new XElement("openGL",
                    new XElement("openGLAA", quiltContext.AA),
                    new XElement("openGLFilledPolygons", quiltContext.filledPolygons),
                    new XElement("openGLPoints", quiltContext.drawPoints),
                    new XElement("openGLZoomFactor", quiltContext.openGLZoomFactor),
                    new XElement("openGLFGOpacity", quiltContext.FGOpacity),
                    new XElement("openGLBGOpacity", quiltContext.BGOpacity));
                prefsXML.Root.Add(openGLPrefs);


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

                prefsXML.Root.Add(colorPrefs);

                prefsXML.Save(filename);
            }
            catch (Exception)
            {
                ErrorReporter.showMessage_OK("Failed to save preferences", "Error");
            }
        }

        void setUI(bool status)
        {
            fileMenu.Enabled = status;
            editMenu.Enabled = status;

            tabControl.Enabled = status;

            main_tl.Enabled = status;
            right_tl.Enabled = status;

            if (status)
            {
                updatePatternElementUI(false);
            }

            return;
        }

        public MainForm(ref bool doPrompts, QuiltContext _quiltContext)
        {
            pMainForm(ref doPrompts, _quiltContext);
        }

        void pMainForm(ref bool doPrompts, QuiltContext _quiltContext)
        {
#if (!LICENSEDISABLED)
            if (_quiltContext.licenceName == "")
            {
                MessageBox.Show("Please activate your license.", "License is missing", MessageBoxButtons.OK, MessageBoxType.Error);

                QLicense.Eto.Controls.ActivationForm a = new QLicense.Eto.Controls.ActivationForm(_quiltContext.licenseLocation, "Quilt", "2", Environment.UserName, typeof(Quilt2License.Quilt2Lic), _quiltContext._certPubicKeyData);
                a.quit = q;
                doPrompts = false;
                Content = a;
                Width = 400;
                Height = 400;
            }
            else
#endif
            {
                doPrompts = true;

                // string basePath = AppContext.BaseDirectory; // Disabled this as release builds do not seem to populate this field. Use the above complex approach instead.
                helpPath = Path.Combine(EtoEnvironment.GetFolderPath(EtoSpecialFolder.ApplicationResources), "Documentation", "index.html");
                helpAvailable = File.Exists(helpPath);

                UI(_quiltContext);
            }
        }

        void q()
        {
            Application.Instance.Quit();
        }

        private bool _veldridReady = false;
        public bool VeldridReady
        {
            get { return _veldridReady; }
            private set
            {
                _veldridReady = value;

                SetUpVeldrid();
            }
        }

        private bool _formReady = false;
        public bool FormReady
        {
            get { return _formReady; }
            set
            {
                _formReady = value;

                SetUpVeldrid();
            }
        }

        private void SetUpVeldrid()
        {
            if (!(FormReady && VeldridReady))
            {
                return;
            }

            viewPort.SetUpVeldrid();

            // Title = $"Veldrid backend: {vSurface.Backend.ToString()}";

            viewPort.Clock.Start();
            createVPContextMenu();
        }

        void UI(QuiltContext _quiltContext)
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

            loadPrefs();

            commonVars = new CommonVars(ref quiltContext);

            delegates();

            setupUIDataContext();

            drawingLock = new object();

            ovpSettings = new OVPSettings();
            ovpSettings.drawDrawn(true);
            ovpSettings.drawFilled(quiltContext.filledPolygons);
            ovpSettings.drawPoints(quiltContext.drawPoints);
            ovpSettings.setZoomStep(quiltContext.openGLZoomFactor);
            ovpSettings.aA(quiltContext.AA);

            Title = commonVars.titleText;

            commandsUI();

            // Compensate for menu.
            MinimumSize = new Size(750, 400 + (label_Height * 7));

            Panel vp = new Panel();

            TableLayout vp_tl = new TableLayout();

            vp.Content = vp_tl;

            vp_tl.Rows.Add(new TableRow());

            patternSettingsUI();
            Scrollable settings_sc = new Scrollable();
            settings_sc.Content = settings;

            quiltUISplitter = new Panel();
            quiltUISplitter.Content = new Splitter
            {
                Orientation = Orientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1,
                Panel1 = settings_sc,
                Panel2 = vp,
                // Panel1MinimumSize = settings.Size.Width,
            };

            tabControl = new TabControl();
            quiltPage = new TabPage();
            tabControl.Pages.Add(quiltPage);
            quiltPage.Text = "Quilt";
            quiltUISetup();
            quiltPage.Content = quiltUISplitter;

            prefsPage = new TabPage();
            tabControl.Pages.Add(prefsPage);
            prefsPage.Text = "Preferences";
            prefsUI();
            prefsPage.Content = prefsPanel;
            tabControl.SelectedIndexChanged += interfaceUpdate;

            Content = tabControl;

            doPatternElementUI(0, updateUI: true);
            drawPreviewPanelHandler();

            updateProgressLabel("Hello");
            updateUIColors();

            GraphicsDeviceOptions options = new GraphicsDeviceOptions(
                                    false,
                                    Veldrid.PixelFormat.R32_Float,
                                    false,
                                    ResourceBindingModel.Improved);

            vSurface = new VeldridSurface(quiltContext.backend, options);

            vSurface.VeldridInitialized += (sender, e) => VeldridReady = true;


            string exeDirectory = "";
            string shaders = "";
            if (Platform.IsMac)
            {
                // AppContext.BaseDirectory is too simple for the case of the Mac
                // projects. When building an app bundle that depends on the Mono
                // framework being installed, it properly returns the path of the
                // executable in Eto.Veldrid.app/Contents/MacOS. When building an
                // app bundle that instead bundles Mono by way of mkbundle, on the
                // other hand, it returns the directory containing the .app.

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
            viewPort.updateHostFunc = viewportUpdateHost;

            commonVars.titleText += " " + vSurface.Backend.ToString();

            Title = commonVars.titleText;

            vp_tl.Rows[0].Cells.Add(new TableCell() { Control = vSurface });

            //updateViewport();
        }

        void interfaceUpdate(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1) // utils
            {
                utilsUIFrozen = true;
                updatePrefsUI();
                utilsUIFrozen = false;
            }
            else
            {
                doPatternElementUI(sender, e);
            }
        }

        void prefsUI()
        {
            prefsPanel = new Panel();
            TableLayout prefsPanel_table = new TableLayout();
            prefsPanel.Content = prefsPanel_table;

            prefsPanel_table.Rows.Add(new TableRow());

            GroupBox groupBox_Prefs = new GroupBox();

            TableCell opengl_tc = new TableCell();

            prefsPanel_table.Rows[0].Cells.Add(opengl_tc);

            TableCell padding = new TableCell();
            prefsPanel_table.Rows[0].Cells.Add(padding);

            prefsPanel_table.Rows.Add(new TableRow()); // padding

            openGL_settings_utilsUISetup(opengl_tc);

            addPrefsHandlers();
        }

        void openGL_settings_utilsUISetup(TableCell tc)
        {
            GroupBox groupBox_openGL = new GroupBox();
            tc.Control = groupBox_openGL;
            TableLayout groupBox_openGL_table = new TableLayout();
            groupBox_openGL.Text = "OpenGL";
            groupBox_openGL.Content = groupBox_openGL_table;

            groupBox_openGL_table.Rows.Add(new TableRow());
            TableCell row0 = new TableCell();
            groupBox_openGL_table.Rows[groupBox_openGL_table.Rows.Count - 1].Cells.Add(row0);
            openGLRow0(row0);
            TableCell row0padding = new TableCell();
            groupBox_openGL_table.Rows[groupBox_openGL_table.Rows.Count - 1].Cells.Add(row0padding);

            groupBox_openGL_table.Rows.Add(new TableRow());
            TableCell row1 = new TableCell();
            groupBox_openGL_table.Rows[groupBox_openGL_table.Rows.Count - 1].Cells.Add(row1);
            openGLRow1(row1);
            TableCell row1padding = new TableCell();
            groupBox_openGL_table.Rows[groupBox_openGL_table.Rows.Count - 1].Cells.Add(row1padding);

            groupBox_openGL_table.Rows.Add(new TableRow());
            TableCell row2 = new TableCell();
            groupBox_openGL_table.Rows[groupBox_openGL_table.Rows.Count - 1].Cells.Add(row2);
            openGLRow2(row2);
            TableCell row2padding = new TableCell();
            groupBox_openGL_table.Rows[groupBox_openGL_table.Rows.Count - 1].Cells.Add(row2padding);
        }

        void openGLRow0(TableCell tc)
        {
            Panel p = new Panel();
            tc.Control = p;

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            Panel lRow0 = new Panel();
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lRow0 });

            Panel rRow0 = new Panel();
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = rRow0 });

            TableLayout lRow0tl = new TableLayout();
            lRow0.Content = lRow0tl;
            lRow0tl.Rows.Add(new TableRow());

            TableLayout rRow0tl = new TableLayout();
            rRow0.Content = rRow0tl;
            rRow0tl.Rows.Add(new TableRow());

            Panel zoomPnl = new Panel();
            TableLayout zoomTL = new TableLayout();
            zoomPnl.Content = zoomTL;
            lRow0tl.Rows[lRow0tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = zoomPnl });

            zoomTL.Rows.Add(new TableRow());

            lbl_zoomSpeed = new Label();
            lbl_zoomSpeed.Text = "Zoom Increment";
            zoomTL.Rows[zoomTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_zoomSpeed });

            num_zoomSpeed = new NumericStepper();
            num_zoomSpeed.MinValue = 1;
            num_zoomSpeed.Increment = 1;
            setSize(num_zoomSpeed, 50, num_Height);
            zoomTL.Rows[zoomTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_zoomSpeed });

            zoomTL.Rows[zoomTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = null }); // padding

            Panel fgOpPnl = new Panel();
            TableLayout fgOpTL = new TableLayout();
            fgOpPnl.Content = fgOpTL;
            rRow0tl.Rows[rRow0tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = fgOpPnl });

            fgOpTL.Rows.Add(new TableRow());

            lbl_fgOpacity = new Label();
            lbl_fgOpacity.Text = "FG Opacity";
            fgOpTL.Rows[fgOpTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_fgOpacity });

            num_fgOpacity = new NumericStepper();
            num_fgOpacity.MinValue = 0.01f;
            num_fgOpacity.Increment = 0.1f;
            num_fgOpacity.DecimalPlaces = 2;
            setSize(num_fgOpacity, 50, num_Height);
            fgOpTL.Rows[fgOpTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_fgOpacity });

            Panel bgOpPnl = new Panel();
            TableLayout bgOpTL = new TableLayout();
            bgOpPnl.Content = bgOpTL;
            rRow0tl.Rows[rRow0tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = bgOpPnl });

            bgOpTL.Rows.Add(new TableRow());

            lbl_bgOpacity = new Label();
            lbl_bgOpacity.Text = "BG Opacity";
            bgOpTL.Rows[bgOpTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_bgOpacity });

            num_bgOpacity = new NumericStepper();
            num_bgOpacity.MinValue = 0.01f;
            num_bgOpacity.Increment = 0.1f;
            num_bgOpacity.DecimalPlaces = 2;
            setSize(num_bgOpacity, 50, num_Height);
            bgOpTL.Rows[bgOpTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_bgOpacity });

            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null }); // padding

            tl.Rows.Add(new TableRow());

            Panel lRow1 = new Panel();
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lRow1 });

            Panel rRow1 = new Panel();
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = rRow1 });

            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null }); // padding

            TableLayout lRow1tl = new TableLayout();
            lRow1.Content = lRow1tl;
            lRow1tl.Rows.Add(new TableRow());

            TableLayout rRow1tl = new TableLayout();
            rRow1.Content = rRow1tl;
            rRow1tl.Rows.Add(new TableRow());

            Panel dispOptsPnl = new Panel();
            TableLayout dispOptsTL = new TableLayout();
            dispOptsPnl.Content = dispOptsTL;
            lRow1tl.Rows[lRow1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = dispOptsPnl });

            dispOptsTL.Rows.Add(new TableRow());

            checkBox_OGLAA = new CheckBox();
            checkBox_OGLAA.Text = "Antialiasing";
            dispOptsTL.Rows[dispOptsTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_OGLAA });

            checkBox_OGLFill = new CheckBox();
            checkBox_OGLFill.Text = "Fill";
            dispOptsTL.Rows[dispOptsTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_OGLFill });

            checkBox_OGLPoints = new CheckBox();
            checkBox_OGLPoints.Text = "Points";
            dispOptsTL.Rows[dispOptsTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_OGLPoints });
        }

        void openGLRow1(TableCell tc)
        {
            Panel p = new Panel();
            tc.Control = p;

            TableLayout swatchesTL = new TableLayout();
            p.Content = swatchesTL;

            swatchesTL.Rows.Add(new TableRow());

            TableRow row0 = new TableRow();
            swatchesTL.Rows.Add(row0);

            opengl_swatchrow0(row0);

            TableRow row1 = new TableRow();
            swatchesTL.Rows.Add(row1);

            opengl_swatchrow1(row1);

            TableRow padding = new TableRow();
            swatchesTL.Rows.Add(padding);
        }

        void opengl_swatchrow0(TableRow tr)
        {
            Panel c0 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c0 });

            TableLayout c0TL = new TableLayout();
            c0.Content = c0TL;
            c0TL.Rows.Add(new TableRow());

            lbl_minorGridColor = new Label();
            lbl_minorGridColor.BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().minor_Color);
            setSize(lbl_minorGridColor, label_Height, label_Height);
            c0TL.Rows[0].Cells.Add(lbl_minorGridColor);

            lbl_minorGridColor_name = new Label();
            lbl_minorGridColor_name.Text = "Minor Grid";
            c0TL.Rows[0].Cells.Add(lbl_minorGridColor_name);

            Panel c1 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c1 });

            TableLayout c1TL = new TableLayout();
            c1.Content = c1TL;
            c1TL.Rows.Add(new TableRow());

            lbl_majorGridColor = new Label();
            lbl_majorGridColor.BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().major_Color);
            setSize(lbl_majorGridColor, label_Height, label_Height);
            c1TL.Rows[0].Cells.Add(lbl_majorGridColor);

            lbl_majorGridColor_name = new Label();
            lbl_majorGridColor_name.Text = "Major Grid";
            c1TL.Rows[0].Cells.Add(lbl_majorGridColor_name);

            Panel c2 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c2 });

            Panel c3 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c3 });
        }

        void opengl_swatchrow1(TableRow tr)
        {
            Panel c0 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c0 });

            TableLayout c0TL = new TableLayout();
            c0.Content = c0TL;
            c0TL.Rows.Add(new TableRow());

            lbl_enabledColor = new Label();
            lbl_enabledColor.BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().enabled_Color);
            setSize(lbl_enabledColor, label_Height, label_Height);
            c0TL.Rows[0].Cells.Add(lbl_enabledColor);

            lbl_enabledColor_name = new Label();
            lbl_enabledColor_name.Text = "Enabled";
            c0TL.Rows[0].Cells.Add(lbl_enabledColor_name);

            Panel c1 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c1 });

            TableLayout c1TL = new TableLayout();
            c1.Content = c1TL;
            c1TL.Rows.Add(new TableRow());

            lbl_ss1Color = new Label();
            lbl_ss1Color.BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape1_Color);
            setSize(lbl_ss1Color, label_Height, label_Height);
            c1TL.Rows[0].Cells.Add(lbl_ss1Color);

            lbl_ss1Color_name = new Label();
            lbl_ss1Color_name.Text = "Subshape 1";
            c1TL.Rows[0].Cells.Add(lbl_ss1Color_name);

            Panel c2 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c2 });

            TableLayout c2TL = new TableLayout();
            c2.Content = c2TL;
            c2TL.Rows.Add(new TableRow());

            lbl_ss2Color = new Label();
            lbl_ss2Color.BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape2_Color);
            setSize(lbl_ss2Color, label_Height, label_Height);
            c2TL.Rows[0].Cells.Add(lbl_ss2Color);

            lbl_ss2Color_name = new Label();
            lbl_ss2Color_name.Text = "Subshape 2";
            c2TL.Rows[0].Cells.Add(lbl_ss2Color_name);

            Panel c3 = new Panel();
            tr.Cells.Add(new TableCell() { Control = c3 });

            TableLayout c3TL = new TableLayout();
            c3.Content = c3TL;
            c3TL.Rows.Add(new TableRow());

            lbl_ss3Color = new Label();
            lbl_ss3Color.BackgroundColor = UIHelper.myColorToColor(commonVars.getColors().subshape3_Color);
            setSize(lbl_ss3Color, label_Height, label_Height);
            c3TL.Rows[0].Cells.Add(lbl_ss3Color);

            lbl_ss3Color_name = new Label();
            lbl_ss3Color_name.Text = "Subshape 3";
            c3TL.Rows[0].Cells.Add(lbl_ss3Color_name);
        }

        void openGLRow2(TableCell tc)
        {
            Panel p = new Panel();
            tc.Control = p;

            btn_resetColors = new Button();
            btn_resetColors.Text = "Reset";
            setSize(btn_resetColors, 60, 21);
            tc.Control = TableLayout.AutoSized(btn_resetColors, centered: true);
        }

        void launchHelp(object sender, EventArgs e)
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

        void commandsUI()
        {
            Closed += quitHandler;

            quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += quit;
            Application.Instance.Terminating += quitHandler;

            aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += aboutMe;

            helpCommand = new Command { MenuText = "Help...", Shortcut = Keys.F1 };
            helpCommand.Executed += launchHelp;

            copyLayer = new Command { MenuText = "Copy", ToolBarText = "Copy", Shortcut = Application.Instance.CommonModifier | Keys.C };
            copyLayer.Executed += copyHandler;

            pasteLayer = new Command { MenuText = "Paste", ToolBarText = "Paste", Shortcut = Application.Instance.CommonModifier | Keys.V };
            pasteLayer.Executed += pasteHandler;

            newSim = new Command { MenuText = "New", ToolBarText = "New", Shortcut = Application.Instance.CommonModifier | Keys.N };
            newSim.Executed += newHandler;
            openSim = new Command { MenuText = "Open", ToolBarText = "Open", Shortcut = Application.Instance.CommonModifier | Keys.O };
            openSim.Executed += openHandler;

            revertSim = new Command { MenuText = "Revert", ToolBarText = "Revert", Shortcut = Application.Instance.CommonModifier | Keys.R };
            revertSim.Executed += revertHandler;
            revertSim.Enabled = false;

            saveSim = new Command { MenuText = "Save", ToolBarText = "Save", Shortcut = Application.Instance.CommonModifier | Keys.S };
            saveSim.Executed += saveHandler;

            saveAsSim = new Command { MenuText = "Save As", ToolBarText = "Save As", Shortcut = Application.Instance.CommonModifier | Keys.Shift | Keys.S };
            saveAsSim.Executed += saveAsHandler;

            fileMenu = new ButtonMenuItem { Text = "&File", Items = { newSim, openSim, revertSim, saveSim, saveAsSim } };
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

        void patternSettingsUI()
        {
            settings = new Panel();
            settings.Size = new Size(525, 270);
            Panel p = new Panel();
            TableLayout p_tl = new TableLayout();
            p.Content = p_tl;

            settings.Content = p;

            Panel pMain = new Panel();
            main_tl = new TableLayout();
            pMain.Content = main_tl;

            TableRow main_tr = new TableRow();
            main_tl.Rows.Add(main_tr);
            TableCell main_tc0 = new TableCell();
            main_tr.Cells.Add(main_tc0);
            TableCell main_tc1 = new TableCell();
            main_tr.Cells.Add(main_tc1);

            Panel left_s = new Panel();
            left_tl = new TableLayout();
            left_s.Content = left_tl;
            main_tc0.Control = left_s;

            Scrollable right_s = new Scrollable();
            right_tl = new TableLayout();
            right_s.Content = right_tl;
            main_tc1.Control = right_s;

            p_tl.Rows.Add(new TableRow() { ScaleHeight = true });
            p_tl.Rows[p_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = pMain });


            Panel pLower = new Panel();
            TableLayout progressTL = new TableLayout();
            pLower.Content = progressTL;// TableLayout.AutoSized(progressTL);
            TableRow progressTL_r0 = new TableRow();
            progressTL.Rows.Add(progressTL_r0);
            TableCell progressTL_r0_c0 = new TableCell();
            progressTL_r0.Cells.Add(progressTL_r0_c0);
            TableCell progressTL_r0_c1 = new TableCell();
            progressTL_r0.Cells.Add(progressTL_r0_c1);

            int progressLabelWidth = listBox_entries_Width; // forcing as the UI hasn't calculated at this point.
            progressLabel = new Label();
            progressLabel.Text = "";
            setSize(progressLabel, progressLabelWidth, label_Height * 2);
            progressTL_r0_c0.Control = progressLabel;

            progressBar = new ProgressBar();
            progressBar.Height = 10; // new Size(progressBarWidth, 10);
            progressTL_r0_c1.Control = progressBar;
            progressTL_r0_c1.ScaleWidth = true;

            p_tl.Rows.Add(new TableRow());
            p_tl.Rows[p_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = pLower });

            patternSettingsUI_2();
        }

        void patternSettingsUI_2()
        {
            TableRow left_tr0 = new TableRow();
            left_tr0.ScaleHeight = true;
            left_tl.Rows.Add(left_tr0);

            listBox_entries = new ListBox();
            int listBox_entries_Width = 200;
            int listBox_entries_Height = 300;
            setSize(listBox_entries, listBox_entries_Width, listBox_entries_Height);
            listBox_entries.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames);
            listBox_entries.SelectedIndexChanged += updatePatternElementUI;

            TableCell left_tr0_0 = new TableCell();
            left_tr0.Cells.Add(left_tr0_0);

            Panel listBox_pnl = new Panel();
            Scrollable listBox_s = new Scrollable();
            listBox_pnl.Content = listBox_s;
            listBox_s.Content = listBox_entries;
            left_tr0_0.Control = listBox_pnl;

            Panel lowerLeftContainer = new Panel();
            TableLayout settings_table = new TableLayout();
            lowerLeftContainer.Content = settings_table;

            TableRow left_tr1 = new TableRow();
            left_tl.Rows.Add(left_tr1);
            TableCell left_tr1_0 = new TableCell();
            left_tr1.Cells.Add(left_tr1_0);
            left_tr1_0.Control = lowerLeftContainer;

            settings_table.Rows.Add(new TableRow());

            text_patternElement = new TextBox();
            text_patternElement.ToolTip = "Name of pattern element.";
            settings_table.Rows[settings_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = text_patternElement });

            settings_table.Rows.Add(new TableRow());
            Panel bPanel = new Panel();
            settings_table.Rows[settings_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = bPanel });

            TableLayout bTable = new TableLayout();
            bPanel.Content = bTable;
            bTable.Rows.Add(new TableRow());

            entry_Add = new Button();

            int entry_btnWidth = listBox_entries_Width / 3;
            entry_Add.Text = "Add";
            entry_Add.ToolTip = "Add new pattern entry (duplicates will not be added).";
            setSize(entry_Add, entry_btnWidth, 21);
            bTable.Rows[bTable.Rows.Count - 1].Cells.Add(new TableCell() { Control = entry_Add });

            bTable.Rows[bTable.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            entry_Remove = new Button();
            entry_Remove.Text = "Remove";
            entry_Remove.ToolTip = "Remove selected pattern entry.";
            setSize(entry_Remove, entry_btnWidth, 21);
            bTable.Rows[bTable.Rows.Count - 1].Cells.Add(new TableCell() { Control = entry_Remove });

            settings_table.Rows.Add(new TableRow());
            Panel row2panel = new Panel();
            settings_table.Rows[settings_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row2panel });

            TableLayout row2tl = new TableLayout();
            row2panel.Content = row2tl;
            row2tl.Rows.Add(new TableRow());

            lbl_viewportZoom = new Label();
            lbl_viewportZoom.Text = "Zoom";

            int nX = 45;
            int nW = 55;
            num_viewportZoom = new NumericStepper();
            num_viewportZoom.Value = 1.0f;
            num_viewportZoom.DecimalPlaces = 2;
            num_viewportZoom.MinValue = 1E-2;
            setSize(num_viewportZoom, nW, num_Height);
            num_viewportZoom.LostFocus += zoomChanged;

            Panel row2left = new Panel();
            row2tl.Rows[row2tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = row2left });

            TableLayout row2leftTL = new TableLayout();
            row2left.Content = row2leftTL;
            row2leftTL.Rows.Add(new TableRow());
            row2leftTL.Rows[row2leftTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_viewportZoom });

            row2leftTL.Rows[row2leftTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_viewportZoom });

            row2leftTL.Rows[row2leftTL.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            checkBox_suspendBuild = new CheckBox();
            checkBox_suspendBuild.Text = "Suspend";
            checkBox_suspendBuild.ToolTip = "If checked, do not rebuild the quilt with changes.";

            row2tl.Rows[row2tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_suspendBuild });

            checkBox_showInput = new CheckBox();
            checkBox_showInput.Text = "Input";
            checkBox_showInput.ToolTip = "If checked, show subshapes in viewport, rather than the final shapes.";
            checkBox_showInput.Checked = true;

            row2tl.Rows[row2tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_showInput });

            row2tl.Rows[row2tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });

            settings_table.Rows.Add(new TableRow());
            Panel row3panel = new Panel();
            settings_table.Rows[settings_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row3panel });

            TableLayout row3tl = new TableLayout();
            row3panel.Content = row3tl;
            row3tl.Rows.Add(new TableRow());

            lbl_viewportPos = new Label();
            lbl_viewportPos.Text = "Position";

            row3tl.Rows[row3tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_viewportPos });

            num_viewportX = new NumericStepper();
            num_viewportX.Value = 0.0f;
            num_viewportX.DecimalPlaces = 2;
            num_viewportX.ToolTip = "X coordinate at center of viewport.";
            setSize(num_viewportX, nW, num_Height);
            num_viewportX.LostFocus += posChanged;

            row3tl.Rows[row3tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_viewportX });

            num_viewportY = new NumericStepper();
            num_viewportY.Value = 0.0f;
            num_viewportY.DecimalPlaces = 2;
            num_viewportY.ToolTip = "Y coordinate at center of viewport.";
            setSize(num_viewportY, nW, num_Height);
            num_viewportY.LostFocus += posChanged;

            row3tl.Rows[row3tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_viewportY });

            row3tl.Rows[row3tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });

            settings_table.Rows.Add(new TableRow());
            Panel row4panel = new Panel();
            settings_table.Rows[settings_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row4panel });

            TableLayout row4tl = new TableLayout();
            row4panel.Content = row4tl;
            row4tl.Rows.Add(new TableRow());

            lbl_padding = new Label();
            lbl_padding.Text = "Padding";

            row4tl.Rows[row4tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_padding });

            num_padding = new NumericStepper();
            num_padding.MinValue = 0;
            num_padding.DecimalPlaces = 2;
            num_padding.Increment = 0.1;
            num_padding.Value = 0;
            num_padding.ToolTip = "Padding to apply in both directions between patterns in quilt.";
            setSize(num_padding, 55, num_Height);

            row4tl.Rows[row4tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_padding });

            row4tl.Rows[row4tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });

            lbl_patNum = new Label();
            lbl_patNum.Text = "#";

            row4tl.Rows[row4tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_patNum });

            nW = 75;
            num_patNum = new NumericStepper();
            num_patNum.MinValue = 0;
            num_patNum.Increment = 1;
            num_patNum.Value = 0;
            num_patNum.MaxValue = 0;
            num_patNum.ToolTip = "Go to this pattern in the quilt.";
            num_patNum.LostFocus += goToPattern;
            setSize(num_patNum, nW, num_Height);

            patternIndex = 0;

            row4tl.Rows[row4tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_patNum });

            row4tl.Rows[row4tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });

            settings_table.Rows.Add(new TableRow());
            Panel row5panel = new Panel();
            settings_table.Rows[settings_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row5panel });

            TableLayout row5tl = new TableLayout();
            row5panel.Content = row5tl;
            row5tl.Rows.Add(new TableRow());

            btn_export = new Button();
            btn_export.Text = "Export";
            btn_export.ToolTip = "Export quilt to layout file, rebuilding if suspended.";
            setSize(btn_export, 200, 21);
            row5tl.Rows[row5tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(btn_export) });

            row5tl.Rows[row5tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });
        }

        void updatePrefsUI()
        {
            checkBox_OGLAA.Checked = quiltContext.AA;
            checkBox_OGLFill.Checked = quiltContext.filledPolygons;
            checkBox_OGLPoints.Checked = quiltContext.drawPoints;
            num_zoomSpeed.Value = quiltContext.openGLZoomFactor;
            num_fgOpacity.Value = quiltContext.FGOpacity;
            num_bgOpacity.Value = quiltContext.BGOpacity;
            lbl_majorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.major_Color.toArgb());
            lbl_minorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.minor_Color.toArgb());
            lbl_enabledColor.BackgroundColor = Color.FromArgb(quiltContext.colors.enabled_Color.toArgb());
            lbl_ss1Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape1_Color.toArgb());
            lbl_ss2Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape2_Color.toArgb());
            lbl_ss3Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape3_Color.toArgb());

            previewUpdate();
        }
    }
}
