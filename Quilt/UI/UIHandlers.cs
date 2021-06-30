using Error;
using Eto.Drawing;
using Eto.Forms;
using geoCoreLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Quilt
{
    public partial class MainForm
    {
        private string layout_filename;
        private BackgroundWorker bw;
        private ProcessLayout pl;
        private bool abortLoad;
        void pNewHandler(object sender, EventArgs e)
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
                pDoPatternElementUI(0, updateUI: true);
            }
            ovpSettings.fullReset();
            viewPort.reset();
            pUpdateViewport_2();
            pasteLayer.Enabled = commonVars.stitcher.isCopySet();
            pUpdateLBContextMenu();
        }

        void pFromLayoutHandler(object sender, EventArgs e)
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
                pIndeterminateQuiltUI("Processing", "Processing");
                layout_filename = ofd.FileName;
                pLoadLayoutFromFile();
            }
            pUpdateLBContextMenu();
        }

        void pProcessLayout(object sender, EventArgs e)
        {
            int index = comboBox_layout_structures.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            pProcessLayoutWithWorker(index);
        }

        void pProcessLayoutWithWorker(int index)
        {
            pPreWorker(index);
            pPrepLayoutBW();
            bw.RunWorkerAsync();
        }
        
        void pLoadLayoutFromFile()
        {
            string[] tokens = layout_filename.Split(new[] { '.' });
            string ext = tokens[^1].ToUpper();
            string filename = String.Join(',', tokens.SkipLast(1));

            if ((ext == "GDS") || (ext == "OAS") || (ext == "OASIS") || (ext == "GZ"))
            {
                UIFreeze = true;
                commonVars.gCH.reset();
                commonVars.gCH.getGeo().baseScale = 1000.0;

                if ((ext == "GDS") || ((ext == "GZ") && (tokens[^2].ToUpper() == "GDS")))
                {
                    commonVars.gCH.updateGeoCoreHandler(layout_filename, GeoCore.fileType.gds);
                }

                if ((ext == "OAS") || (ext == "OASIS") || ((ext == "GZ") && ((tokens[^2].ToUpper() == "OAS") || (tokens[^2].ToUpper() == "OASIS"))))
                {
                    commonVars.gCH.updateGeoCoreHandler(layout_filename, GeoCore.fileType.oasis);
                }

                if (commonVars.gCH.isValid())
                {
                    pProcessLayoutWithWorker(0);
                }
                else
                {
                    string error = "";
                    int noOfErrors = commonVars.gCH.error_msgs.Count;
                    for (int s = 0; s < noOfErrors; s++)
                    {
                        error += commonVars.gCH.error_msgs[s];
                        if ((s > 0) && (s != noOfErrors - 1))
                        {
                            error += "\r\n";
                        }
                    }
                    ErrorReporter.showMessage_OK(error, "Error(s) trying to load layout file");
                    
                }
            }

            commonVars.projectFileName = filename + ".quilt";
            Title = commonVars.titleText + " - " + commonVars.projectFileName;
            pUpdatePatternElementUI();
            layout_filename = "";
        }

        void pCancelLayoutProcessing(object sender, EventArgs e)
        {
            bw.CancelAsync();
            abortLoad = true;
            pUpdateProgressLabel("Cancelling...");
            pNew(true);
            pUpdateProgressLabel("Cancelled.");
        }
        
        void pPrepLayoutBW()
        {
            abortLoad = false;
            bw = new BackgroundWorker {WorkerSupportsCancellation = true};
            bw.RunWorkerCompleted += pPostWorker;
            bw.DoWork += pProcessLayoutWithWorker;
        }

        void pPreWorker(int structureIndex)
        {
            btn_Cancel.Enabled = true;
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
            comboBox_layout_structures.SelectedIndexChanged -= pProcessLayout;
            comboBox_layout_structures.SelectedIndex = structureIndex;

            // Get list of layers and datatypes in the specified cell.
            List<string> structureLDs = commonVars.gCH.getGeo().getActiveStructureLDList();

            // Global list of layer names - we'll need to pick these out later.
            Dictionary<string, string> structureLDNames = commonVars.gCH.getGeo().getLayerNames();

            pl = new ProcessLayout(structureLDs, structureLDNames) {getGeoCore = pGetGeoCore};

        }

        void pPostWorker(object sender, EventArgs e)
        {
            if (pl.pattElements.Count > 0)
            {
                commonVars.stitcher.addPatternElements(pl.pattElements);
                comboBox_layout_structures.SelectedIndexChanged += pProcessLayout;
                UIFreeze = false;
                pUpdatePatternElementUI();
            }
            else
            {
                // Something went wrong or processing was cancelled. Reset the system.
                UIFreeze = false;
                pNew(true);
                comboBox_layout_structures.SelectedIndexChanged += pProcessLayout;
                progressLabel.Text = "Cancelled.";
            }
            btn_Cancel.Enabled = false;
        }
        
        void pProcessLayoutWithWorker(object sender, EventArgs e)
        {
            pl.processLayout(ref abortLoad, quiltContext.angularTolerance, quiltContext.verticalRectDecomp);
        }

        GeoCore pGetGeoCore()
        {
            return commonVars.gCH.getGeo();
        }

        void pSaveHandler(object sender, EventArgs e)
        {
            pSaveProject(commonVars.projectFileName);
        }

        void pSaveProject(string filename)
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
                        pSaveEnabler();
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
                pSaveEnabler();
            });
        }

        void pSaveAsHandler(object sender, EventArgs e)
        {
            pSaveProject("");
        }

        void pSaveEnabler()
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

        void pDragEvent(object sender, DragEventArgs e)
        {
            if (e.Data.ContainsUris)
            {
                // Check that we have a valid file somewhere in the dropped resources
                foreach (Uri t in e.Data.Uris)
                {
                    string[] tokens = t.LocalPath.Split(new[] { '.' });
                    if ((tokens[^1].ToUpper() == "QUILT") || (tokens[^1].ToUpper() == "XML"))
                    {
                        e.Effects = DragEffects.Copy;
                        break;
                    }
                }
            }
        }

        void pDragAndDrop(object sender, DragEventArgs e)
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
                    string[] tokens = d.Uris[i].LocalPath.Split(new[] { '.' });
                    if ((tokens[^1].ToUpper() == "QUILT") || (tokens[^1].ToUpper() == "XML"))
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index != -1)
            {
                pDoLoad(d.Uris[index].LocalPath);
            }
        }

        void pOpenHandler(object sender, EventArgs e)
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
                pDoLoad(ofd.FileName);
            }
        }

        void pCopyHandler(object sender, EventArgs e)
        {
            pCopy();
        }

        void pCopy()
        {
            commonVars.stitcher.setCopy(listBox_entries.SelectedIndex);
            pasteLayer.Enabled = true;
            pUpdateLBContextMenu();
        }

        void pPasteHandler(object sender, EventArgs e)
        {
            pPaste();
        }

        void pPaste()
        {
            commonVars.stitcher.paste(listBox_entries.SelectedIndex);
            pUpdatePatternElementUI();
        }

        async void pDoLoad(string xmlFile)
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

        void pRevertHandler(object sender, EventArgs e)
        {
            pDoLoad(commonVars.projectFileName);
        }

        void pQuit(object sender, EventArgs e)
        {
            pSavePrefs();
            Application.Instance.Quit();
        }

        void pQuitHandler(object sender, EventArgs e)
        {
            pSavePrefs();
        }

        void pAddPatternElement(object sender, EventArgs e)
        {
            pAddPatternElement();
        }

        void pAddPatternElement(int index = -1)
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
            pUpdatePatternElementUI();

            text_patternElement.Text = "";
        }

        void pRenamePatternElement(object sender, EventArgs e)
        {
            pRenamePatternElement();
        }

        void pRenamePatternElement()
        {
            int index = listBox_entries.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            commonVars.stitcher.renamePatternElement(index, text_patternElement.Text);
            text_patternElement.Text = "";

            pUpdatePatternElementUI();
        }

        void pRemovePatternElement(object sender, EventArgs e)
        {
            pRemovePatternElement();
        }

        void pRemovePatternElement()
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

                pUpdatePatternElementUI();
            }
        }

        void pDuplicatePatternElement()
        {
            int index = listBox_entries.SelectedIndex;
            pAddPatternElement(index);
        }

        void pExportClicked(object sender, EventArgs e)
        {
            // Avoid users trying to export with no patterns
            if (commonVars.stitcher.getPatternCount() > 0)
            {
                pDoExport();
            }
        }

        async void pDoExport()
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
                string[] tokens = filename.Split(new[] { '.' });
                string ext = tokens[^1].ToUpper();

                int type = -1;

                switch (ext)
                {
                    case "GDS":
                    case "GZ" when (tokens[^2].ToUpper() == "GDS"):
                    case "GDSII":
                    case "GZ" when (tokens[^2].ToUpper() == "GDSII"):
                        type = (int)GeoCore.fileType.gds;
                        break;
                    case "OAS":
                    case "GZ" when (tokens[^2].ToUpper() == "OAS"):
                    case "OASIS":
                    case "GZ" when (tokens[^2].ToUpper() == "OASIS"):
                        type = (int)GeoCore.fileType.oasis;
                        break;
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
                        // If build is suspended, we need to update the quilt to ensure export makes sense.
                        if (suspendBuild)
                        {
                            commonVars.stitcher.updateQuilt();
                        }
                        commonVars.stitcher.toGeoCore(type, filename);
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

        void pAboutMe(object sender, EventArgs e)
        {
            if (!(aboutBox is {Visible: true}))
            {
                string creditText = "Version " + CentralProperties.version + ", " +
                "© " + CentralProperties.author + " 2018-2021" + "\r\n\r\n";
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
                aboutBox = new CreditsScreen(creditText);
            }
            Point location = new Point(Location.X + (Width - aboutBox.Width) / 2,
                                       Location.Y + (Height - aboutBox.Height) / 2);
            aboutBox.Location = location;
            aboutBox.Show();
        }

        void pAddPrefsHandlers()
        {
            Application.Instance.Invoke(() =>
            {
                lbl_ss1Color.MouseDoubleClick += pLayerColorChange;
                lbl_ss2Color.MouseDoubleClick += pLayerColorChange;
                lbl_ss3Color.MouseDoubleClick += pLayerColorChange;

                lbl_minorGridColor.MouseDoubleClick += pLayerColorChange;
                lbl_majorGridColor.MouseDoubleClick += pLayerColorChange;
                lbl_axisColor.MouseDoubleClick += pLayerColorChange;
                lbl_vpbgColor.MouseDoubleClick += pLayerColorChange;

                lbl_enabledColor.MouseDoubleClick += pLayerColorChange;
                lbl_backgroundColor.MouseDoubleClick += pLayerColorChange;
                lbl_extentsColor.MouseDoubleClick += pLayerColorChange;

                checkBox_OGLAA.CheckedChanged += pPreferencesChange;
                checkBox_OGLFill.CheckedChanged += pPreferencesChange;
                checkBox_OGLPoints.CheckedChanged += pPreferencesChange;
                checkBox_drawExtents.CheckedChanged += pPreferencesChange;
                checkBox_verticalRectDecomp.CheckedChanged += pPreferencesChange;
                num_fgOpacity.LostFocus += pPreferencesChange;
                num_bgOpacity.LostFocus += pPreferencesChange;
                num_zoomSpeed.LostFocus += pPreferencesChange;

                num_angularTolerance.LostFocus += pPreferencesChange;

                checkBox_suspendBuild.CheckedChanged += pSuspendQuiltBuild;
                checkBox_showInput.CheckedChanged += pShowInput;
                btn_resetColors.Click += pResetColors;
            });
        }

        void pLayerColorChange(Label id, Color colDialogColor)
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
                if (id == lbl_axisColor)
                {
                    quiltContext.colors.axis_Color = UIHelper.colorToMyColor(colDialogColor);
                }
                if (id == lbl_vpbgColor)
                {
                    quiltContext.colors.background_Color = UIHelper.colorToMyColor(colDialogColor);
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

                pUpdateUIColors();
            });
        }

        void pLayerColorChange(object sender, EventArgs e)
        {
            if (colUIFrozen)
            {
                return;
            }

            try
            {
                if (sender is Label senderLabel)
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

                    if (senderLabel == lbl_axisColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.axis_Color);
                    }
                    if (senderLabel == lbl_majorGridColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.major_Color);
                    }
                    if (senderLabel == lbl_minorGridColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.minor_Color);
                    }
                    if (senderLabel == lbl_vpbgColor)
                    {
                        sourceColor = UIHelper.myColorToColor(quiltContext.colors.background_Color);
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
                            pLayerColorChange(senderLabel, colDialog.Color);
                        };
                    }

                    DialogResult dR = colDialog.ShowDialog(this);

                    if (!Platform.IsMac)
                    {
                        if (dR == DialogResult.Ok)
                        {
                            pLayerColorChange(senderLabel, colDialog.Color);
                        }
                    }
                }
            }
            catch (Exception ec)
            {
                ErrorReporter.showMessage_OK(ec.ToString(), "Error");
            }
        }

        void pPreferencesChange(object sender, EventArgs e)
        {
            if (utilsUIFrozen)
            {
                return;
            }
            quiltContext.AA = (bool)checkBox_OGLAA.Checked;
            ovpSettings.aA(quiltContext.AA);
            commonVars.setOpenGLProp(CommonVars.properties_gl.aa);

            quiltContext.filledPolygons = (bool)checkBox_OGLFill.Checked;
            ovpSettings.drawFilled(quiltContext.filledPolygons);
            commonVars.setOpenGLProp(CommonVars.properties_gl.fill);

            quiltContext.drawPoints = (bool)checkBox_OGLPoints.Checked;
            ovpSettings.drawPoints(quiltContext.drawPoints);
            commonVars.setOpenGLProp(CommonVars.properties_gl.points);

            quiltContext.drawExtents = (bool)checkBox_drawExtents.Checked;

            quiltContext.verticalRectDecomp = (bool)checkBox_verticalRectDecomp.Checked;

            quiltContext.openGLZoomFactor = Convert.ToInt32(num_zoomSpeed.Value);
            ovpSettings.setZoomFactor(quiltContext.openGLZoomFactor);
            commonVars.setGLInt(CommonVars.gl_i.zoom);

            quiltContext.FGOpacity = num_fgOpacity.Value;
            commonVars.setOpacity(CommonVars.opacity_gl.fg);
            quiltContext.BGOpacity = num_bgOpacity.Value;
            commonVars.setOpacity(CommonVars.opacity_gl.bg);

            quiltContext.angularTolerance = num_angularTolerance.Value;

            viewPort.updateViewport();
            utilsUIFrozen = false;
        }

        void pResetColors(object sender, EventArgs e)
        {
            quiltContext.colors.reset();
            commonVars.setColors(quiltContext.colors);
            pUpdateUIColors();
        }

        void pUpdateUIColors()
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
                lbl_axisColor.BackgroundColor = Color.FromArgb(quiltContext.colors.axis_Color.toArgb());
                lbl_majorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.major_Color.toArgb());
                lbl_minorGridColor.BackgroundColor = Color.FromArgb(quiltContext.colors.minor_Color.toArgb());
                lbl_vpbgColor.BackgroundColor = Color.FromArgb(quiltContext.colors.background_Color.toArgb());
                pDoColors();
                colUIFrozen = false;
            });

        }

        void pSetPadding(object sender, EventArgs e)
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
            pDrawPreviewPanelHandler();
        }

        void pSuspendQuiltBuild(object sender, EventArgs e)
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
                    }
                }
                pGeneratePreviewPanelContent();
            }
        }

        void pShowInput(object sender, EventArgs e)
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
                    }
                }
                pGeneratePreviewPanelContent();
            }
        }
    }
}
