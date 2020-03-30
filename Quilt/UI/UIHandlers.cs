using Error;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using System;
using System.Threading.Tasks;

namespace Quilt
{
    public partial class MainForm : Form
    {
        void newHandler(object sender, EventArgs e)
        {
            UIFreeze = true;
            commonVars.reset();
            Title = commonVars.titleText;
            listBox_entries.SelectedIndex = 0;
            num_padding.Value = 0;
            checkBox_showInput.Checked = (commonVars.stitcher.getShowInput() == 1);
            checkBox_suspendBuild.Checked = false;
            num_patNum.Value = 0;
            UIFreeze = false;
            doPatternElementUI(0, updateUI: true);
            ovpSettings.fullReset();
            viewPort.reset();
            updateViewport_2();
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
                    //menu_fileSave.Enabled = false;
                    Title = commonVars.titleText;
                }
                else
                {
                    Title = commonVars.titleText + " - " + commonVars.projectFileName;
                    //menu_fileSave.Enabled = true;
                }
            });
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
                //await Task.Run(() =>
                {
                    doLoad(ofd.FileName);
                }//);
            }
        }

        void copyHandler(object sender, EventArgs e)
        {
            commonVars.stitcher.setCopy(listBox_entries.SelectedIndex);
        }

        void pasteHandler(object sender, EventArgs e)
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
                    commonVars.storage.loadQuilt(xmlFile, ref commonVars.stitcher);
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
            string newString = text_patternElement.Text;
            if (newString == "")
            {
                return;
            }

            commonVars.stitcher.addPatternElement(newString);
            listBox_entries.SelectedIndex = commonVars.stitcher.patternElementNames.Count - 1;
            updatePatternElementUI();

            text_patternElement.Text = "";
        }

        void removePatternElement(object sender, EventArgs e)
        {
            int index = listBox_entries.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            commonVars.stitcher.removePatternElement(index);

            updatePatternElementUI();
        }

        void exportClicked(object sender, EventArgs e)
        {
            doExport();
        }

        async void doExport()
        {
            // Need to request output file location and name.
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "Enter file to save",
                Filters =
                        {
                            new FileFilter("GDS file", "*.gds"),
                            new FileFilter("OAS file", "*.oas")
                        }
            };
            if (sfd.ShowDialog(ParentWindow) == DialogResult.Ok)
            {
                string filename = sfd.FileName;
                string[] tokens = filename.Split(new char[] { '.' });
                string ext = tokens[tokens.Length - 1].ToUpper();

                int type = (int)geoCoreLib.GeoCore.fileType.gds;

                if (ext == "OAS")
                {
                    type = (int)geoCoreLib.GeoCore.fileType.oasis;
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
                creditText += "  Eto.OpenTK : Eto OpenGL viewport\r\n\thttps://github.com/picoe/Eto.OpenTK\r\n";
                creditText += "  clipperLib : geometry processing, area, SVG output reference\r\n\thttp://sourceforge.net/projects/polyclipping/\r\n";
                creditText += "  LibTessDotNet : for Delauney triangulation (viepwort related)\r\n\thttps://github.com/speps/LibTessDotNet\r\n";
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

                checkBox_OGLAA.CheckedChanged += preferencesChange;
                checkBox_OGLFill.CheckedChanged += preferencesChange;
                checkBox_OGLPoints.CheckedChanged += preferencesChange;
                num_fgOpacity.LostFocus += preferencesChange;
                num_bgOpacity.LostFocus += preferencesChange;
                num_zoomSpeed.LostFocus += preferencesChange;

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
                    Color sourceColor = Eto.Drawing.Colors.Black;

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

                    //colDialog.Dispose();
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

            quiltContext.openGLZoomFactor = Convert.ToInt32(num_zoomSpeed.Value);
            ovpSettings.setZoomFactor(quiltContext.openGLZoomFactor);
            commonVars.setGLInt(CommonVars.gl_i.zoom, (Convert.ToInt32(num_zoomSpeed.Value)));

            quiltContext.FGOpacity = num_fgOpacity.Value;
            commonVars.setOpacity(CommonVars.opacity_gl.fg, num_fgOpacity.Value);
            quiltContext.BGOpacity = num_bgOpacity.Value;
            commonVars.setOpacity(CommonVars.opacity_gl.bg, num_bgOpacity.Value);

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
