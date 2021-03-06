﻿using Eto.Drawing;
using Eto.Forms;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quilt
{
    public partial class MainForm
    {
        void pSetViewportCamera(double[] parameters)
        {
            ovpSettings.setCameraPos((float)parameters[0], (float)parameters[1]);
            ovpSettings.setZoomFactor((float)parameters[2]);
        }

        double[] pGetViewportCamera()
        {
            double x = ovpSettings.getCameraX();
            double y = ovpSettings.getCameraY();
            double zoom = ovpSettings.getZoomFactor();
            return new[] { x, y, zoom };
        }

        async void pGeneratePreviewPanelContent()
        {
            if (openGLErrorReported)
            {
                return;
            }
            // Brute force setting, to ensure we're aligned with user preferences that might have changed.
            ovpSettings.minorGridColor = Color.FromArgb(commonVars.getColors().minor_Color.toArgb());
            ovpSettings.majorGridColor = Color.FromArgb(commonVars.getColors().major_Color.toArgb());
            ovpSettings.axisColor = Color.FromArgb(commonVars.getColors().axis_Color.toArgb());
            ovpSettings.backColor = Color.FromArgb(commonVars.getColors().background_Color.toArgb());
            ovpSettings.reset();

            if (suspendBuild)
            {
                return;
            }

            try
            {
                await Task.Run(() =>
                {
                    commonVars.stitcher.updateQuilt();
                });
            }
            catch (TaskCanceledException)
            {
                // No worries - carry on.
            }
            catch (Exception)
            {

            }
        }

        void pUpdateViewport_2()
        {
            Application.Instance.Invoke(() =>
            {
                progressBar.Indeterminate = false;

                float progress = 0.0f;
                pUpdateProgressBar(progress);
                int pShapesCount = commonVars.stitcher.previewShapes.Length;
                int progressChunk = pShapesCount / 100;

                if (progressChunk < 1)
                {
                    progressChunk = 1;
                }

                for (Int32 pattern = 0; pattern < pShapesCount; pattern++)
                {
                    int patternPShapesCount = commonVars.stitcher.previewShapes[pattern].Count;
                    for (Int32 i = 0; i < patternPShapesCount; i++)
                    {
                        float alpha = (float)quiltContext.BGOpacity;
                        // Boost opacity for the selected pattern element to make it obvious in the viewport.
                        if (i == listBox_entries.SelectedIndex)
                        {
                            alpha = (float)quiltContext.FGOpacity;
                        }

                        int polyCount = commonVars.stitcher.previewShapes[pattern][i].getPoints().Count;

                        int drawnIndex = 0;

                        for (Int32 poly = 0; poly < polyCount; poly++)
                        {
                            Color polyColor = Color.FromArgb(commonVars.getColors().deselected_Color.toArgb());
                            if (commonVars.stitcher.previewShapes[pattern][i].elementIndex == listBox_entries.SelectedIndex)
                            {
                                polyColor = Color.FromArgb(commonVars.getColors().enabled_Color.toArgb());
                            }
                            bool drawn = commonVars.stitcher.previewShapes[pattern][i].getDrawnPoly(poly);
                            // Skip this polygon in case drawn, when not showing drawn; not-drawn when showing drawn.
                            if (((commonVars.stitcher.getShowInput() == 1) && (!drawn)) || ((commonVars.stitcher.getShowInput() == 0) && (drawn)))
                            {
                                continue;
                            }
                            if (drawn)
                            {
                                drawnIndex = drawnIndex % 3; // max of 3 drawn shapes, loop around.
                                switch (drawnIndex)
                                {
                                    case 0:
                                        polyColor = Color.FromArgb(commonVars.getColors().subshape1_Color.toArgb());
                                        break;
                                    case 1:
                                        polyColor = Color.FromArgb(commonVars.getColors().subshape2_Color.toArgb());
                                        break;
                                    case 2:
                                        polyColor = Color.FromArgb(commonVars.getColors().subshape3_Color.toArgb());
                                        break;
                                }
                                drawnIndex++;
                            }
                            Monitor.Enter(drawingLock);
                            try
                            {
                                ovpSettings.addPolygon(
                                    poly: UIHelper.myPointFArrayToPointFArray(commonVars.stitcher.previewShapes[pattern][i].getPoints()[poly]),
                                    polyColor: polyColor,
                                    alpha: alpha,
                                    drawn: drawn,
                                    layerIndex: i,
                                    mask: pattern == (Int32)num_patNum.Value  // tag our chosen pattern polys for zoom-to-selected.    // (pattern % commonVars.getColors().simPreviewColors.Count)
                                );
                            }
                            finally
                            {
                                Monitor.Exit(drawingLock);
                            }
                        }

                    }

                    if (quiltContext.drawExtents)
                    {
                        Color extentColor = Color.FromArgb(commonVars.getColors().extents_Color.toArgb());
                        Monitor.Enter(drawingLock);
                        try
                        {
                            ovpSettings.addBGPolygon(
                                poly: UIHelper.myPointFArrayToPointFArray(commonVars.stitcher.backgroundShapes[pattern].getPoints()[0]),
                                polyColor: extentColor,
                                alpha: 1.0f,
                                layerIndex: 0
                            );
                        }
                        finally
                        {
                            Monitor.Exit(drawingLock);
                        }
                    }
                    if ((pattern % progressChunk) == 0)
                    {
                        pUpdateProgressBar(progress);
                        progress += 0.01f;
                    }
                }
            });

            UIFreeze = false;

            pUpdateViewport();
            Application.Instance.Invoke(() =>
            {
                progressBar.ToolTip = "";
                progressBar.Value = 0;
                pSetUI(true);
            });
        }

        void pUpdateViewport()
        {
            if (UIFreeze)
            {
                return;
            }
            Application.Instance.Invoke(() =>
            {
                Monitor.Enter(ovpSettings);
                try
                {
                    viewPort.updateViewport();
                }
                catch (Exception)
                {
                    openGLErrorReported = true;
                }
                finally
                {
                    Monitor.Exit(ovpSettings);
                }
            });
        }

        void pCreateVPContextMenu()
        {
            if (viewPort.dragging)
            {
                return;
            }
            // Single viewport now mandates regeneration of the context menu each time, to allow for entry screening.
            vp_menu = new ContextMenu();

            int itemIndex = 0;
            vp_menu.Items.Add(new ButtonMenuItem { Text = "Reset (r)" });
            vp_menu.Items[itemIndex].Click += delegate
            {
                viewPort.reset();
            };
            itemIndex++;

            var VPMenuDisplayOptionsMenu = vp_menu.Items.GetSubmenu("Display Options");
            itemIndex++;
            int displayOptionsSubItemIndex = 0;
            VPMenuDisplayOptionsMenu.Items.Add(new ButtonMenuItem { Text = "Toggle AA" });
            VPMenuDisplayOptionsMenu.Items[displayOptionsSubItemIndex].Click += delegate
            {
                viewPort.ovpSettings.aA(!viewPort.ovpSettings.aA());
                pUpdateViewport();
            };
            displayOptionsSubItemIndex++;
            VPMenuDisplayOptionsMenu.Items.Add(new ButtonMenuItem { Text = "Toggle Fill" });
            VPMenuDisplayOptionsMenu.Items[displayOptionsSubItemIndex].Click += delegate
            {
                viewPort.ovpSettings.drawFilled(!viewPort.ovpSettings.drawFilled());
                pUpdateViewport();
            };
            displayOptionsSubItemIndex++;
            VPMenuDisplayOptionsMenu.Items.Add(new ButtonMenuItem { Text = "Toggle Points" });
            VPMenuDisplayOptionsMenu.Items[displayOptionsSubItemIndex].Click += delegate
            {
                viewPort.ovpSettings.drawPoints(!viewPort.ovpSettings.drawPoints());
                pUpdateViewport();
            };
            // displayOptionsSubItemIndex++;

            {
                if (viewPort.ovpSettings.isLocked())
                {
                    vp_menu.Items.Add(new ButtonMenuItem { Text = "Thaw (f)" });
                }
                else
                {
                    vp_menu.Items.Add(new ButtonMenuItem { Text = "Freeze (f)" });
                }
                vp_menu.Items[itemIndex].Click += delegate
                {
                    viewPort.freeze_thaw();
                };
                itemIndex++;
                vp_menu.Items.AddSeparator();
                itemIndex++;
                vp_menu.Items.Add(new ButtonMenuItem { Text = "Save bookmark" });
                vp_menu.Items[itemIndex].Click += delegate
                {
                    viewPort.saveLocation();
                };
                itemIndex++;
                vp_menu.Items.Add(new ButtonMenuItem { Text = "Load bookmark" });
                vp_menu.Items[itemIndex].Click += delegate
                {
                    viewPort.loadLocation();
                };
                if (!viewPort.savedLocation_valid)
                {
                    vp_menu.Items[itemIndex].Enabled = false;
                }
                itemIndex++;
            }
            vp_menu.Items.AddSeparator();
            itemIndex++;
            vp_menu.Items.Add(new ButtonMenuItem { Text = "Zoom Extents (x)" });
            vp_menu.Items[itemIndex].Click += delegate
            {
                viewPort.zoomExtents(-1);
            };
            itemIndex++;
            vp_menu.Items.Add(new ButtonMenuItem { Text = "Zoom Selected (z)" });
            vp_menu.Items[itemIndex].Click += delegate
            {
                viewPort.zoomExtents(listBox_entries.SelectedIndex);
            };
            itemIndex++;
            vp_menu.Items.AddSeparator();
            itemIndex++;
            vp_menu.Items.Add(new ButtonMenuItem { Text = "Zoom In (m)" });
            vp_menu.Items[itemIndex].Click += delegate
            {
                viewPort.zoomIn(-1);
            };
            itemIndex++;

            var VPMenuZoomInMenu = vp_menu.Items.GetSubmenu("Fast Zoom In");
            itemIndex++;
            int zoomInSubItemIndex = 0;
            VPMenuZoomInMenu.Items.Add(new ButtonMenuItem { Text = "Zoom In (x5)" });
            VPMenuZoomInMenu.Items[zoomInSubItemIndex].Click += delegate
            {
                viewPort.fastZoomIn(-50);
            };
            zoomInSubItemIndex++;
            VPMenuZoomInMenu.Items.Add(new ButtonMenuItem { Text = "Zoom In (x10)" });
            VPMenuZoomInMenu.Items[zoomInSubItemIndex].Click += delegate
            {
                viewPort.fastZoomIn(-100);
            };
            zoomInSubItemIndex++;
            VPMenuZoomInMenu.Items.Add(new ButtonMenuItem { Text = "Zoom In (x50)" });
            VPMenuZoomInMenu.Items[zoomInSubItemIndex].Click += delegate
            {
                viewPort.fastZoomIn(-500);
            };
            zoomInSubItemIndex++;
            VPMenuZoomInMenu.Items.Add(new ButtonMenuItem { Text = "Zoom In (x100)" });
            VPMenuZoomInMenu.Items[zoomInSubItemIndex].Click += delegate
            {
                viewPort.fastZoomIn(-1000);
            };
            // zoomInSubItemIndex++;

            vp_menu.Items.AddSeparator();
            itemIndex++;

            vp_menu.Items.Add(new ButtonMenuItem { Text = "Zoom Out (n)" });
            vp_menu.Items[itemIndex].Click += delegate
            {
                viewPort.zoomOut(-1);
            };
            // itemIndex++;

            var VPMenuZoomOutMenu = vp_menu.Items.GetSubmenu("Fast Zoom Out");
            // itemIndex++;
            int zoomOutSubItemIndex = 0;
            VPMenuZoomOutMenu.Items.Add(new ButtonMenuItem { Text = "Zoom Out (x5)" });
            VPMenuZoomOutMenu.Items[zoomOutSubItemIndex].Click += delegate
            {
                viewPort.fastZoomOut(-50);
            };
            zoomOutSubItemIndex++;
            VPMenuZoomOutMenu.Items.Add(new ButtonMenuItem { Text = "Zoom Out (x10)" });
            VPMenuZoomOutMenu.Items[zoomOutSubItemIndex].Click += delegate
            {
                viewPort.fastZoomOut(-100);
            };
            zoomOutSubItemIndex++;
            VPMenuZoomOutMenu.Items.Add(new ButtonMenuItem { Text = "Zoom Out (x50)" });
            VPMenuZoomOutMenu.Items[zoomOutSubItemIndex].Click += delegate
            {
                viewPort.fastZoomOut(-500);
            };
            zoomOutSubItemIndex++;
            VPMenuZoomOutMenu.Items.Add(new ButtonMenuItem { Text = "Zoom Out (x100)" });
            VPMenuZoomOutMenu.Items[zoomOutSubItemIndex].Click += delegate
            {
                viewPort.fastZoomOut(-1000);
            };
            // zoomOutSubItemIndex++;

            viewPort.setContextMenu(ref vp_menu);
        }

        void pPreviewUpdate()
        {
            if (viewPort == null)
            {
                return;
            }
            viewPort.updateViewport();
        }

        void pPosChanged(object sender, EventArgs e)
        {
            if (openGLErrorReported)
            {
                return;
            }
            if (UIFreeze)
            {
                return;
            }

            pMoveCamera((float)num_viewportX.Value, (float)num_viewportY.Value);
        }

        void pMoveCamera(float x, float y)
        {
            viewPort.ovpSettings.setCameraPos(x, y);
        }

        void pZoomChanged(object sender, EventArgs e)
        {
            if (openGLErrorReported)
            {
                return;
            }
            if (UIFreeze)
            {
                return;
            }

            double zoomValue = Convert.ToDouble(num_viewportZoom.Value);
            if (zoomValue < num_viewportZoom.MinValue)
            {
                zoomValue = num_viewportZoom.MinValue;
            }

            viewPort.ovpSettings.setZoomFactor((float)(1.0f / zoomValue));
        }

        void pViewportUpdateHost()
        {
            UIFreeze = true;
            double value = 1.0f / viewPort.ovpSettings.getZoomFactor();
            if (value < num_viewportZoom.MinValue)
            {
                value = num_viewportZoom.MinValue;
            }
            num_viewportZoom.Value = value;
            num_viewportX.Value = viewPort.ovpSettings.getCameraX();
            num_viewportY.Value = viewPort.ovpSettings.getCameraY();
            viewPort.ovpSettings.selectedIndex = listBox_entries.SelectedIndex;
            UIFreeze = false;
            pCreateVPContextMenu();
        }

        void pViewportSelectionFunc(int index)
        {
            Application.Instance.Invoke(() =>
            {
                listBox_entries.SelectedIndex = index;
            });
        }

        void pGoToPattern(object sender, EventArgs e)
        {
            int index = (int)num_patNum.Value;
            if (index < 0)
            {
                index = 0;
            }
            if (index >= commonVars.stitcher.getPatternCount())
            {
                index = commonVars.stitcher.getPatternCount();
            }
            if (index == patternIndex)
            {
                return;
            }
            patternIndex = index;

            pDoPatternElementUI(patternIndex);

            PointF tmp = commonVars.stitcher.findPattern((int)num_patNum.Value);
            pMoveCamera(tmp.X, tmp.Y);
        }

        void pDrawPreviewPanelHandler()
        {
            if (UIFreeze)
            {
                return;
            }
            UIFreeze = true;
            pGeneratePreviewPanelContent();
            progressBar.Indeterminate = false;
        }
    }
}
