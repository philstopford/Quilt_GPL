using Error;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using geoCoreLib;
using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quilt
{
    public partial class MainForm : Form
    {
        void newHandler(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "New", MessageBoxButtons.YesNo, type: MessageBoxType.Question);
            if (result == DialogResult.Yes)
            {
                pNew();
            }
        }

        void pNew(bool empty = false)
        {
            UIFreeze = true;
            commonVars.gCH.reset();
            commonVars.gCH.getGeo().updateCollections();
            commonVars.reset(empty);
            Title = commonVars.titleText;
            listBox_entries.SelectedIndex = 0;
            num_padding.Value = 0;
            checkBox_showInput.Checked = (commonVars.stitcher.getShowInput() == 1);
            checkBox_suspendBuild.Checked = false;
            num_patNum.Value = 0;
            UIFreeze = false;
            if (!empty)
            {
                doPatternElementUI(0, updateUI: true);
            }
            ovpSettings.fullReset();
            viewPort.reset();
            updateViewport_2();
            pasteLayer.Enabled = commonVars.stitcher.isCopySet();
            updateLBContextMenu();
        }

        void fromLayoutHandler(object sender, EventArgs e)
        {
            pNew(true);
            // Need to request input file location and name.
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Select GDSII or OASIS file to load",
                MultiSelect = false,
                Filters =
                            {
                                new FileFilter("Layout Files (*.gds; *.oas; *.oasis; *.gds.gz; *.oas.gz; *.oasis.gz)", ".gds", "*.oas", "*.oasis", ".gds.gz", "*.oas.gz", "*.oasis.gz")
                            }
            };
            if (ofd.ShowDialog(ParentWindow) == DialogResult.Ok)
            {
                indeterminateQuiltUI("Processing", "Processing");
                processLayout(ofd.FileName);
            }
            updateLBContextMenu();
        }

        void processLayout(object sender, EventArgs e)
        {
            int index = comboBox_layout_structures.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            processLayout(index);
        }

        void processLayout(string filename)
        {
            string[] tokens = filename.Split(new char[] { '.' });
            string ext = tokens[tokens.Length - 1].ToUpper();

            if ((ext == "GDS") || (ext == "OAS") || (ext == "OASIS") || (ext == "GZ"))
            {
                UIFreeze = true;
                commonVars.gCH.reset();
                commonVars.gCH.getGeo().baseScale = 1000.0;

                if ((ext == "GDS") || ((ext == "GZ") && (tokens[tokens.Length - 2].ToUpper() == "GDS")))
                {
                    commonVars.gCH.updateGeoCoreHandler(filename, GeoCore.fileType.gds);
                }

                if ((ext == "OAS") || (ext == "OASIS") || ((ext == "GZ") && ((tokens[tokens.Length - 2].ToUpper() == "OAS") || (tokens[tokens.Length - 2].ToUpper() == "OASIS"))))
                {
                    commonVars.gCH.updateGeoCoreHandler(filename, GeoCore.fileType.oasis);
                }

                if (commonVars.gCH.isValid())
                {
                    processLayout(0);
                }
            }
            updatePatternElementUI();
        }

        async void processLayout(int structureIndex)
        {
            UIFreeze = true;
            commonVars.stitcher.reset(true);

            commonVars.structureList_exp.Clear();
            if (!commonVars.gCH.isValid())
            {
                commonVars.gCH.reset();
                UIFreeze = false;
                return;
            }

            // Force first cell for now.
            commonVars.gCH.getGeo().activeStructure = structureIndex;
            commonVars.gCH.getGeo().updateCollections();

            // Set our UI list for the cells in the layout.
            comboBox_layout_structures.SelectedIndexChanged -= processLayout;
            comboBox_layout_structures.SelectedIndex = structureIndex;

            // Get list of layers and datatypes in the specified cell.
            List<string> structureLDs = commonVars.gCH.getGeo().getActiveStructureLDList();

            // Global list of layer names - we'll need to pick these out later.
            Dictionary<string, string> structureLDNames = commonVars.gCH.getGeo().getLayerNames();

            ProcessLayout pl = new ProcessLayout(structureLDs, structureLDNames);
            pl.getGeoCore = getGeoCore;

            CancellationTokenSource fileLoad_cancelTS = new CancellationTokenSource();

            bool done = false;

            Task bob = Task.Run(() =>
                {
                    try
                    {
                        using (fileLoad_cancelTS.Token.Register(Thread.CurrentThread.Abort))
                        {
                            done = pl.processLayout(quiltContext.angularTolerance, quiltContext.verticalRectDecomp);
                        }
                    }
                    catch (ThreadAbortException)
                    {
                    }
                    finally
                    {
                    }
                }
            , fileLoad_cancelTS.Token);

            try
            {
                await bob;// fileLoadTask;
            }
            catch (Exception)
            {
            }

            fileLoad_cancelTS.Dispose();
            bob.Dispose();

            commonVars.stitcher.addPatternElements(pl.pattElements);

            comboBox_layout_structures.SelectedIndexChanged += processLayout;
            UIFreeze = false;
            updatePatternElementUI(true);
        }

        GeoCore getGeoCore()
        {
            return commonVars.gCH.getGeo();
        }

        int getGeoActiveStructure()
        {
            return commonVars.gCH.getGeo().activeStructure;
        }

        void saveHandler(object sender, EventArgs e)
        {
            saveProject(commonVars.projectFileName);
        }

        void saveProject(string filename)
        {
            Application.Instance.Invoke(() =>
            {
                if (filename == "")
                {
                    // Need to request output file location and name.
                    SaveFileDialog sfd = new SaveFileDialog()
                    {
                        Title = "Enter file to save",
                        Filters =
                        {
                            new FileFilter("Quilt Files (*.quilt)", ".quilt")
                        }
                    };
                    if (sfd.ShowDialog(ParentWindow) == DialogResult.Ok)
                    {
                        filename = sfd.FileName;
                    }
                    else
                    {
                        saveEnabler();
                        return;
                    }
                }
                if (commonVars.storage.saveQuiltSettings(filename, ref commonVars.stitcher))
                {
                    commonVars.projectFileName = filename;
                }
                else
                {
                    MessageBox.Show("Error during save.", "Save failed", MessageBoxButtons.OK);
                }
                saveEnabler();
            });
        }

        void saveAsHandler(object sender, EventArgs e)
        {
            saveProject("");
        }

        void saveEnabler()
        {
            Application.Instance.Invoke(() =>
            {
                if (commonVars.projectFileName == "")
                {
                    Title = commonVars.titleText;
                    revertSim.Enabled = false;
                }
                else
                {
                    Title = commonVars.titleText + " - " + commonVars.projectFileName;
                    revertSim.Enabled = true;
                }
            });
        }

        void dragEvent(object sender, DragEventArgs e)
        {
            if (e.Data.ContainsUris)
            {
                // Check that we have a valid file somewhere in the dropped resources
                for (int i = 0; i < e.Data.Uris.Length; i++)
                {
                    string[] tokens = e.Data.Uris[i].LocalPath.Split(new char[] { '.' });
                    if ((tokens[tokens.Length - 1].ToUpper() == "QUILT") || (tokens[tokens.Length - 1].ToUpper() == "XML"))
                    {
                        e.Effects = DragEffects.Copy;
                        break;
                    }
                }
            }
        }

        void dragAndDrop(object sender, DragEventArgs e)
        {
            // Only allow a single selection; pick the first.
            DataObject d = e.Data;
            int length = d.Uris.Length;
            if (length < 1)
            {
                return;
            }

            // Find our first file object.
            int index = -1;
            for (int i = 0; i < length; i++)
            {
                if (d.Uris[i].IsFile)
                {
                    // Actually a supported file?
                    string[] tokens = d.Uris[i].LocalPath.Split(new char[] { '.' });
                    if ((tokens[tokens.Length - 1].ToUpper() == "QUILT") || (tokens[tokens.Length - 1].ToUpper() == "XML"))
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index != -1)
            {
                doLoad(d.Uris[index].LocalPath);
            }
        }

        void openHandler(object sender, EventArgs e)
        {
            // Need to request input file location and name.
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Choose file to load",
                MultiSelect = false,
                Filters =
                        {
                            new FileFilter("Quilt Files (*.quilt)", ".quilt")
                        }
            };
            if (ofd.ShowDialog(ParentWindow) == DialogResult.Ok)
            {
                doLoad(ofd.FileName);
            }
        }

        void copyHandler(object sender, EventArgs e)
        {
            copy();
        }

        void copy()
        {
            commonVars.stitcher.setCopy(listBox_entries.SelectedIndex);
            pasteLayer.Enabled = true;
            updateLBContextMenu();
        }

        void pasteHandler(object sender, EventArgs e)
        {
            paste();
        }

        void paste()
        {
            commonVars.stitcher.paste(listBox_entries.SelectedIndex);
            updatePatternElementUI();
        }

        async void doLoad(string xmlFile)
        {
            try
            {
                await Task.Run(() =>
                {
                    commonVars.storage.loadQuilt(xmlFile, ref commonVars.stitcher, quiltContext);
                });
            }
            catch (TaskCanceledException)
            {
                // No worries - just carry on.
            }
        }

        void revertHandler(object sender, EventArgs e)
        {
            doLoad(commonVars.projectFileName);
        }

        void quit(object sender, EventArgs e)
        {
            savePrefs();
            Application.Instance.Quit();
        }

        void quitHandler(object sender, EventArgs e)
        {
            savePrefs();
        }

        void addPatternElement(object sender, EventArgs e)
        {
            addPatternElement();
        }

        void addPatternElement(int index = -1)
        {
            if (index == -1)
            {
                string newString = text_patternElement.Text;
                if (newString == "")
                {
                    return;
                }

                commonVars.stitcher.addPatternElement(newString);
            }
            else
            {
                commonVars.stitcher.addPatternElement(commonVars.stitcher.getPatternElement(patternIndex:0, index));
            }
            listBox_entries.SelectedIndex = commonVars.stitcher.patternElementNames.Count - 1;
            updatePatternElementUI();

            text_patternElement.Text = "";
        }

        void renamePatternElement(object sender, EventArgs e)
        {
            renamePatternElement();
        }

        void renamePatternElement()
        {
            int index = listBox_entries.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            commonVars.stitcher.renamePatternElement(index, text_patternElement.Text);
            text_patternElement.Text = "";

            updatePatternElementUI();
        }

        void removePatternElement(object sender, EventArgs e)
        {
            removePatternElement();
        }

        void removePatternElement()
        {
            var result = MessageBox.Show("Are you sure?", "Remove element", MessageBoxButtons.YesNo, type: MessageBoxType.Question);
            if (result == DialogResult.Yes)
            {
                int index = listBox_entries.SelectedIndex;
                if (index == -1)
                {
                    return;
                }
                commonVars.stitcher.removePatternElement(index);

                updatePatternElementUI();
            }
        }

        void duplicatePatternElement()
        {
            int index = listBox_entries.SelectedIndex;
            addPatternElement(index);
        }

        void exportClicked(object sender, EventArgs e)
        {
            // Avoid users trying to export with no patterns
            if (commonVars.stitcher.getPatternCount() > 0)
            {
                doExport();
            }
        }

        async void doExport()
        {
            if (commonVars.stitcher.getPatternCount() <= 0)
            {
                return;
            }
            // Need to request output file location and name.
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "Enter file to save",
                Filters =
                        {
                            new FileFilter("GDS file", "*.gds", ".gdsii"),
                            new FileFilter("GDS file, GZIP compressed", "*.gds.gz", "*.gdsii.gz"),
                            new FileFilter("OAS file", "*.oas", "*.oasis"),
                            new FileFilter("OAS file. GZIP compressed", "*.oas.gz", "*.oasis.gz")
                        }
            };
            if (sfd.ShowDialog(ParentWindow) == DialogResult.Ok)
            {
                string filename = sfd.FileName;
                string[] tokens = filename.Split(new char[] { '.' });
                string ext = tokens[tokens.Length - 1].ToUpper();

                int type = -1;

                if (((ext == "GDS") || ((ext == "GZ") && (tokens[tokens.Length - 2].ToUpper() == "GDS"))) ||
                    ((ext == "GDSII") || ((ext == "GZ") && (tokens[tokens.Length - 2].ToUpper() == "GDSII"))))
                {
                    type = (int)GeoCore.fileType.gds;
                }
                else if (((ext == "OAS") || ((ext == "GZ") && (tokens[tokens.Length - 2].ToUpper() == "OAS"))) ||
                    ((ext == "OASIS") || ((ext == "GZ") && (tokens[tokens.Length - 2].ToUpper() == "OASIS"))))
                {
                    type = (int)GeoCore.fileType.oasis;
                }

                if (type == -1)
                {
                    return;
                }

                Application.Instance.Invoke(() =>
                {
                    progressBar.Indeterminate = true;
                });
                try
                {
                    await Task.Run(() =>
                    {
                        // If build is suspended, we need to udpate the quilt to ensure export makes sense.
                        if (suspendBuild)
                        {
                            commonVars.stitcher.updateQuilt();
                        }
                        commonVars.stitcher.toGeoCore(type, filename);
                        string csvFile = tokens[0];
                        for (int t = 1; t < tokens.Length - 1; t++)
                        {
                            csvFile += "." + tokens[t];
                        }
                        csvFile += "_" + tokens[tokens.Length - 1];

                        commonVars.stitcher.toCSV(csvFile + ".csv");
                    });
                }
                catch (TaskCanceledException)
                {
                    // No worries - silence the crash, that's all.
                }
                Application.Instance.Invoke(() =>
                {
                    progressBar.Indeterminate = false;
                });
            }
        }

        void aboutMe(object sender, EventArgs e)
        {
            if (aboutBox == null || !aboutBox.Visible)
            {
                string creditText = "Version " + CentralProperties.version + ", " +
                "© " + CentralProperties.author + " 2018-2020" + "\r\n\r\n";
                creditText += "Licence: GPLv3";
                creditText += "\r\n\r\n";
                creditText += "Libraries used:\r\n";
                creditText += "  Eto.Forms : UI framework\r\n\thttps://github.com/picoe/Eto/wiki\r\n";
                creditText += "  Eto.Veldrid : Eto Veldrid viewport\r\n\thttps://github.com/picoe/Eto.Veldrid\r\n";
                creditText += "  DesignLibs : Design libraries\r\n\thttps://github.com/philstopford/DesignLibs_GPL\r\n";
                creditText += "  clipperLib : geometry processing\r\n\thttp://sourceforge.net/projects/polyclipping/\r\n";
                creditText += "  KD-Sharp : for viewport selection\r\n\thttps://code.google.com/p/kd-sharp/\r\n";
                creditText += "  LibTessDotNet : for Delauney triangulation\r\n\thttps://github.com/speps/LibTessDotNet\r\n";
                creditText += "  MiscUtil : \r\n\thttp://yoda.arachsys.com/csharp/miscutil/\r\n";
                aboutBox = new CreditsScreen(this, creditText);
            }
            Point location = new Point((Int32)(Location.X + (Width - aboutBox.Width) / 2),
                                       (Int32)(Location.Y + (Height - aboutBox.Height) / 2));
            aboutBox.Location = location;
            aboutBox.Show();
        }

        void addPrefsHandlers()
        {
            Application.Instance.Invoke(() =>
            {
                lbl_ss1Color.MouseDoubleClick += layerColorChange;
                lbl_ss2Color.MouseDoubleClick += layerColorChange;
                lbl_ss3Color.MouseDoubleClick += layerColorChange;

                lbl_minorGridColor.MouseDoubleClick += layerColorChange;
                lbl_majorGridColor.MouseDoubleClick += layerColorChange;

                lbl_enabledColor.MouseDoubleClick += layerColorChange;
                lbl_backgroundColor.MouseDoubleClick += layerColorChange;
                lbl_extentsColor.MouseDoubleClick += layerColorChange;

                checkBox_OGLAA.CheckedChanged += preferencesChange;
                checkBox_OGLFill.CheckedChanged += preferencesChange;
                checkBox_OGLPoints.CheckedChanged += preferencesChange;
                checkBox_drawExtents.CheckedChanged += preferencesChange;
                checkBox_verticalRectDecomp.CheckedChanged += preferencesChange;
                num_fgOpacity.LostFocus += preferencesChange;
                num_bgOpacity.LostFocus += preferencesChange;
                num_zoomSpeed.LostFocus += preferencesChange;

                num_angularTolerance.LostFocus += preferencesChange;

                checkBox_suspendBuild.CheckedChanged += suspendQuiltBuild;
                checkBox_showInput.CheckedChanged += showInput;
                btn_resetColors.Click += resetColors;
            });
        }

        void layerColorChange(Label id, Color colDialogColor)
        {
            Application.Instance.Invoke(() =>
            {
                if (id == lbl_minorGridColor)
                {
                    quiltContext.colors.minor_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_majorGridColor)
                {
                    quiltContext.colors.major_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_enabledColor)
                {
                    quiltContext.colors.enabled_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_backgroundColor)
                {
                    quiltContext.colors.deselected_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_ss1Color)
                {
                    quiltContext.colors.subshape1_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_ss2Color)
                {
                    quiltContext.colors.subshape2_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_ss3Color)
                {
                    quiltContext.colors.subshape3_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_extentsColor)
                {
                    quiltContext.colors.extents_Color = UIHelper.colorToMyColor(colDialogColor);
                }

                quiltContext.colors.rebuildLists();
                commonVars.setColors(quiltContext.colors);

                updateUIColors();
            });
        }

        void layerColorChange(object sender, EventArgs e)
        {
            if (colUIFrozen)
            {
                return;
            }

            try
            {
                Label senderLabel = sender as Label;

                if (senderLabel != null)
                {
                    Color sourceColor = Colors.Black;

                    if (senderLabel == lbl_ss1Color)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.subshape1_Color);
                    }
                    if (senderLabel == lbl_ss2Color)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.subshape2_Color);
                    }
                    if (senderLabel == lbl_ss3Color)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.subshape3_Color);
                    }

                    if (senderLabel == lbl_majorGridColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.major_Color);
                    }
                    if (senderLabel == lbl_minorGridColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.minor_Color);
                    }

                    if (senderLabel == lbl_enabledColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.enabled_Color);
                    }

                    if (senderLabel == lbl_backgroundColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.deselected_Color);
                    }

                    if (senderLabel == lbl_extentsColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.extents_Color);
                    }

                    ColorDialog colDialog = new ColorDialog
                    {
                        Color = sourceColor
                    };

                    // Special OS X plumbing. The color picker on that platform is a floating dialog and has no OK/cancel.
                    if (Platform.IsMac)
                    {
                        colDialog.ColorChanged += delegate
                        {
                            layerColorChange(senderLabel, colDialog.Color);
                        };
                    }

                    DialogResult dR = colDialog.ShowDialog(this);

                    if (!Platform.IsMac)
                    {
                        if (dR == DialogResult.Ok)
                        {
                            layerColorChange(senderLabel, colDialog.Color);
                        }
                    }
                }
            }
            catch (Exception ec)
            {
                ErrorReporter.showMessage_OK(ec.ToString(), "Error");
            }
        }

        void preferencesChange(object sender, EventArgs e)
        {
            if (utilsUIFrozen)
            {
                return;
            }
            quiltContext.AA = (bool)checkBox_OGLAA.Checked;
            ovpSettings.aA(quiltContext.AA);
            commonVars.setOpenGLProp(CommonVars.properties_gl.aa, (bool)checkBox_OGLAA.Checked);

            quiltContext.filledPolygons = (bool)checkBox_OGLFill.Checked;
            ovpSettings.drawFilled(quiltContext.filledPolygons);
            commonVars.setOpenGLProp(CommonVars.properties_gl.fill, (bool)checkBox_OGLFill.Checked);

            quiltContext.drawPoints = (bool)checkBox_OGLPoints.Checked;
            ovpSettings.drawPoints(quiltContext.drawPoints);
            commonVars.setOpenGLProp(CommonVars.properties_gl.points, (bool)checkBox_OGLPoints.Checked);

            quiltContext.drawExtents = (bool)checkBox_drawExtents.Checked;

            quiltContext.verticalRectDecomp = (bool)checkBox_verticalRectDecomp.Checked;
            commonVars.verticalRectDecomp = quiltContext.verticalRectDecomp;

            quiltContext.openGLZoomFactor = Convert.ToInt32(num_zoomSpeed.Value);
            ovpSettings.setZoomFactor(quiltContext.openGLZoomFactor);
            commonVars.setGLInt(CommonVars.gl_i.zoom, (Convert.ToInt32(num_zoomSpeed.Value)));

            quiltContext.FGOpacity = num_fgOpacity.Value;
            commonVars.setOpacity(CommonVars.opacity_gl.fg, num_fgOpacity.Value);
            quiltContext.BGOpacity = num_bgOpacity.Value;
            commonVars.setOpacity(CommonVars.opacity_gl.bg, num_bgOpacity.Value);

            quiltContext.angularTolerance = num_angularTolerance.Value;

            viewPort.updateViewport();
            utilsUIFrozen = false;
        }

        void resetColors(object sender, EventArgs e)
        {
            quiltContext.colors.reset();
            commonVars.setColors(quiltContext.colors);
            updateUIColors();
        }

        void updateUIColors()
        {
            Application.Instance.Invoke(() =>
            {
                colUIFrozen = true;
                lbl_ss1Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape1_Color.toArgb());
                lbl_ss2Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape2_Color.toArgb());
                lbl_ss3Color.BackgroundColor = Color.FromArgb(quiltContext.colors.subshape3_Color.toArgb());
                lbl_enabledColor.BackgroundColor = Color.FromArgb(quiltContext.colors.enabled_Color.toArgb());
                lbl_backgroundColor.BackgroundColor = Color.FromArgb(quiltContext.colors.deselected_Color.toArgb());
                lbl_extentsColor.BackgroundColor = Color.FromArgb(quiltContext.colors.extents_Color.toArgb());
                lbl_majorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.major_Color.toArgb());
                lbl_minorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.minor_Color.toArgb());
                doColors();
                colUIFrozen = false;
            });

        }

        void setPadding(object sender, EventArgs e)
        {
            pSetPadding();
        }

        void pSetPadding()
        {
            commonVars.stitcher.setPadding(num_padding.Value);
            if (UIFreeze)
            {
                return;
            }
            drawPreviewPanelHandler();
        }

        void suspendQuiltBuild(object sender, EventArgs e)
        {
            pSuspendQuiltBuild();
        }

        void pSuspendQuiltBuild()
        {
            suspendBuild = (bool)checkBox_suspendBuild.Checked;
            if (!suspendBuild)
            {
                int index = listBox_entries.SelectedIndex;
                if (index == -1)
                {
                    if (commonVars.stitcher.patternElementNames.Count > 0)
                    {
                        listBox_entries.SelectedIndex = 0;
                        index = 0;
                    }
                }
                generatePreviewPanelContent(index);
            }
        }

        void showInput(object sender, EventArgs e)
        {
            pShowInput();
        }

        void pShowInput()
        {
            if ((bool)checkBox_showInput.Checked)
            {
                commonVars.stitcher.setShowInput(1);
            }
            else
            {
                commonVars.stitcher.setShowInput(0);
            }
            if (UIFreeze)
            {
                return;
            }
            if (!suspendBuild)
            {
                int index = listBox_entries.SelectedIndex;
                if (index == -1)
                {
                    if (commonVars.stitcher.patternElementNames.Count > 0)
                    {
                        listBox_entries.SelectedIndex = 0;
                        index = 0;
                    }
                }
                generatePreviewPanelContent(index);
            }
        }
    }
}
