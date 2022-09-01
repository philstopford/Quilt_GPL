using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using geoLib;
using shapeEngine;

namespace Quilt;

public partial class MainForm
{
    private void pSetViewportCamera(double[] parameters)
    {
        ovpSettings.setCameraPos((float)parameters[0], (float)parameters[1]);
        ovpSettings.setZoomFactor((float)parameters[2]);
    }

    private double[] pGetViewportCamera()
    {
        double x = ovpSettings.getCameraX();
        double y = ovpSettings.getCameraY();
        double zoom = ovpSettings.getZoomFactor();
        return new[] { x, y, zoom };
    }

    private async void pGeneratePreviewPanelContent()
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
        catch
        {
            // ignored
        }
    }

    class VPPatternElementData
    {
        public class fgdata
        {
            public PointF[] polys { get; set; }
            public Color color { get; set; }
            public float alpha { get; set; }
            public bool drawn { get; set; }
            public int index { get; set; }
            public bool mask { get; set; }
        }

        public class bgdata
        {
            public PointF[] bgpolys { get; set; }
            public Color bgcolor { get; set; }
            public float bgalpha { get; set; }
            public int bgindex { get; set; }
        }

        public fgdata[] fgdatalist { get; set; }
        public bgdata[] bgdatalist { get; set; }

        public void initfgdata(int cnt)
        {
            fgdatalist = new fgdata[cnt];
            for (int i = 0; i < cnt; i++)
            {
                fgdatalist[i] = new();
            }
        }

        public void initbgdata(int cnt)
        {
            bgdatalist = new bgdata[cnt];
            for (int i = 0; i < cnt; i++)
            {
                bgdatalist[i] = new();
            }
        }

    }

    class ViewportPatternData
    {
        public VPPatternElementData[] patternData;

        public ViewportPatternData(int count)
        {
            patternData = new VPPatternElementData[count];
        }
    }

    class ViewportData
    {
        public ViewportPatternData[] viewportData;

        public ViewportData(int count)
        {
            viewportData = new ViewportPatternData[count];
        }
    }
    
    private void pUpdateViewport_2()
    {
        int pShapesCount = commonVars.stitcher.previewShapes.Length;
        ViewportData vData = new ViewportData(pShapesCount);
        int selElementIndex = 0; 
        int maskCheck = 0;
        Application.Instance.Invoke(() =>
        {
            selElementIndex = listBox_entries.SelectedIndex;
            maskCheck = (int)num_patNum.Value;
        });

        Parallel.For(0, pShapesCount, pattern =>
        // for (int pattern = 0; pattern < pShapesCount; pattern++)
        {
            int patternPShapesCount = commonVars.stitcher.previewShapes[pattern].Count;
            vData.viewportData[pattern] = new(patternPShapesCount);
            
            Parallel.For(0, patternPShapesCount, i =>
            // for (int i = 0 ; i < patternPShapesCount; i++)
            {
                vData.viewportData[pattern].patternData[i] = new();
                float alpha = (float)quiltContext.BGOpacity;
                // Boost opacity for the selected pattern element to make it obvious in the viewport.
                if (i == selElementIndex)
                {
                    alpha = (float)quiltContext.FGOpacity;
                }

                List<GeoLibPointF[]> points = commonVars.stitcher.previewShapes[pattern][i].getPoints();
                int polyCount = points.Count;
                vData.viewportData[pattern].patternData[i].initfgdata(polyCount);
 
                int drawnIndex = 0;

                for (int poly = 0; poly < polyCount; poly++)
                {
                    Color polyColor = Color.FromArgb(commonVars.getColors().deselected_Color.toArgb());
                    if (commonVars.stitcher.previewShapes[pattern][i].elementIndex == selElementIndex)
                    {
                        polyColor = Color.FromArgb(commonVars.getColors().enabled_Color.toArgb());
                    }

                    bool drawn = commonVars.stitcher.previewShapes[pattern][i].getDrawnPoly(poly);
                    // Skip this polygon in case drawn, when not showing drawn; not-drawn when showing drawn.
                    if (commonVars.stitcher.getShowInput() == 1 && !drawn ||
                        commonVars.stitcher.getShowInput() == 0 && drawn)
                    {
                        continue;
                    }

                    int patternElement = commonVars.stitcher.previewShapes[pattern][i].elementIndex;
                    
                    if (drawn)
                    {
                        drawnIndex %= 3; // max of 3 drawn shapes, loop around.

                        // Avoid subshape coloring for complex cases; there are no subshapes in these shapes.
                        // Without this, arrayed complex cases would show subshape colors, for example.
                        if (commonVars.stitcher.getPatternElement(0, patternElement)
                                .getInt(ShapeSettings.properties_i.shapeIndex) ==
                            (int)CentralProperties.shapeNames.complex)
                        {
                            drawnIndex = 0;
                        }

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

                    vData.viewportData[pattern].patternData[i].fgdatalist[poly].alpha = alpha;
                    vData.viewportData[pattern].patternData[i].fgdatalist[poly].color = polyColor;
                    vData.viewportData[pattern].patternData[i].fgdatalist[poly].drawn = drawn;
                    vData.viewportData[pattern].patternData[i].fgdatalist[poly].index = i;
                    vData.viewportData[pattern].patternData[i].fgdatalist[poly].mask = pattern == maskCheck;
                    vData.viewportData[pattern].patternData[i].fgdatalist[poly].polys = UIHelper.myPointFArrayToPointFArray(
                        points[poly]);

                }

            }
            );

            
            if (quiltContext.drawExtents)
            {
                if (vData.viewportData[pattern].patternData == null)
                {
                    vData.viewportData[pattern] = new(1);
                }

                if (vData.viewportData[pattern].patternData.Length == 0)
                {
                    vData.viewportData[pattern].patternData = new VPPatternElementData[1];
                }
                if (vData.viewportData[pattern].patternData[0] == null)
                {
                    vData.viewportData[pattern].patternData[0] = new();
                }
                vData.viewportData[pattern].patternData[0].initbgdata(1);
                Color extentColor = Color.FromArgb(commonVars.getColors().extents_Color.toArgb());

                vData.viewportData[pattern].patternData[0].bgdatalist[0].bgalpha = 1.0f;
                vData.viewportData[pattern].patternData[0].bgdatalist[0].bgcolor = extentColor;
                vData.viewportData[pattern].patternData[0].bgdatalist[0].bgindex = 0;
                vData.viewportData[pattern].patternData[0].bgdatalist[0].bgpolys = UIHelper.myPointFArrayToPointFArray(commonVars.stitcher.backgroundShapes[pattern]
                    .getPoints()[0]);

            }
            
        }
        );
            
        Application.Instance.Invoke(() =>
        {
            try
            {
                Monitor.Enter(drawingLock);
                foreach (var vdata in vData.viewportData)
                {
                    foreach (var data in vdata.patternData)
                    {
                        if (data.fgdatalist != null)
                        {
                            foreach (var t in data.fgdatalist)
                            {
                                if (t.polys != null)
                                {
                                    ovpSettings.addPolygon(
                                        poly: t.polys,
                                        polyColor: t.color,
                                        alpha: t.alpha,
                                        drawn: t.drawn,
                                        layerIndex: t.index,
                                        mask: t
                                            .mask); // tag our chosen pattern polys for zoom-to-selected.    // (pattern % commonVars.getColors().simPreviewColors.Count)

                                }
                            }
                        }

                        if (data.bgdatalist != null)
                        {
                            foreach (var t in data.bgdatalist)
                            {
                                if (t.bgpolys != null)
                                {
                                    ovpSettings.addBGPolygon(
                                        poly: t.bgpolys,
                                        polyColor: t.bgcolor,
                                        alpha: t.bgalpha,
                                        layerIndex: t.bgindex
                                    );
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                Monitor.Exit(drawingLock);
            }

            UIFreeze = false;

            pUpdateViewport();
            progressBar.ToolTip = "";
            progressBar.Value = 0;
            pSetUI(true);
        });
    }

    private void pUpdateViewport()
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

    private void pCreateVPContextMenu()
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
            vp_menu.Items.Add(viewPort.ovpSettings.isLocked()
                ? new ButtonMenuItem {Text = "Thaw (f)"}
                : new ButtonMenuItem {Text = "Freeze (f)"});
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
        itemIndex++;

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

        itemIndex++;
        vp_selLinkedElement = new ButtonMenuItem {Text = "Select Merge Element"};
        vp_menu.Items.Add(vp_selLinkedElement);
        vp_menu.Items[itemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.linkedElementIndex, offset:false);
        };
        itemIndex++;
        vp_selXPosElement = new ButtonMenuItem {Text = "Select X Position Reference Element"};
        vp_menu.Items.Add(vp_selXPosElement);
        vp_menu.Items[itemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.xPosRef);
        };
        itemIndex++;
        vp_selYPosElement = new ButtonMenuItem {Text = "Select Y Position Reference Element"};
        vp_menu.Items.Add(vp_selYPosElement);
        vp_menu.Items[itemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.yPosRef);
        };
        itemIndex++;
        vp_selRotElement = new ButtonMenuItem {Text = "Select Rotation Reference Element"};
        vp_menu.Items.Add(vp_selRotElement);
        vp_menu.Items[itemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.rotationRef);
        };
        itemIndex++;
        vp_selArrayElement = new ButtonMenuItem {Text = "Select Array Reference Element"};
        vp_menu.Items.Add(vp_selArrayElement);
        vp_menu.Items[itemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.arrayRef);
        };
        itemIndex++;
        vp_selArrayRotElement = new ButtonMenuItem {Text = "Select Array Rotation Reference Element"};
        vp_menu.Items.Add(vp_selArrayRotElement);
        vp_menu.Items[itemIndex].Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.arrayRotationRef);
        };
        
        int patternElement = listBox_entries.SelectedIndex;
        try
        {
            vp_selLinkedElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.linkedElementIndex) != -1);
        }
        catch (Exception)
        {
            vp_selLinkedElement.Enabled = false;
        }

        try
        {
            vp_selXPosElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.xPosRef) != 0);
        }
        catch (Exception)
        {
            vp_selXPosElement.Enabled = false;
        }

        try
        {
            vp_selYPosElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.yPosRef) != 0);
        }
        catch (Exception)
        {
            vp_selYPosElement.Enabled = false;
        }

        try
        {
            // Self and 'World Origin'
            vp_selRotElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.rotationRef) > 1);
        }
        catch (Exception)
        {
            vp_selRotElement.Enabled = false;
        }

        try
        {
            vp_selArrayElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.arrayRef) != 0);
        }
        catch (Exception)
        {
            vp_selArrayElement.Enabled = false;
        }

        try
        {
            // Self and 'World Origin'
            vp_selArrayRotElement.Enabled = (commonVars.stitcher.getPatternElement(0, patternElement)
                .getInt(PatternElement.properties_i.arrayRotationRef) > 1);
        }
        catch (Exception)
        {
            vp_selArrayRotElement.Enabled = false;
        }

        viewPort.setContextMenu(ref vp_menu);
    }
    private void pPreviewUpdate()
    {
        viewPort?.updateViewport();
    }

    private void pPosChanged(object sender, EventArgs e)
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

    private void pMoveCamera(float x, float y)
    {
        viewPort.ovpSettings.setCameraPos(x, y);
    }

    private void pZoomChanged(object sender, EventArgs e)
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

    private void pViewportUpdateHost()
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

    private void pViewportSelectionFunc(int index)
    {
        Application.Instance.Invoke(() =>
        {
            listBox_entries.SelectedIndex = index;
        });
    }

    private void pGoToPattern(object sender, EventArgs e)
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

    private void pDrawPreviewPanelHandler()
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