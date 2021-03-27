using Eto;
using Eto.Drawing;
using Eto.Forms;
using System;

namespace Quilt
{
    public partial class MainForm : Form
    {
        bool UIFreeze;

        void setSize(Button _control, int width, int height)
        {
            if (Platform.IsWinForms)
            {
                _control.Size = new Size(width, height);
            }
            else
            {
                _control.Width = width;
                _control.Height = height;
            }
        }

        void setSize(CommonControl _control, int width, int height)
        {
            if (Platform.IsWinForms)
            {
                _control.Size = new Size(width, height);
            }
            else
            {
                _control.Width = width;
            }
        }

        void addHandlers()
        {
            comboBox_patternElementShape.SelectedIndexChanged += doPatternElementUI;

            num_layer_subshape_minhl.LostFocus += doPatternElementUI;
            num_layer_subshape2_minhl.LostFocus += doPatternElementUI;
            num_layer_subshape3_minhl.LostFocus += doPatternElementUI;

            num_layer_subshape_minvl.LostFocus += doPatternElementUI;
            num_layer_subshape2_minvl.LostFocus += doPatternElementUI;
            num_layer_subshape3_minvl.LostFocus += doPatternElementUI;

            num_layer_subshape_minho.LostFocus += doPatternElementUI;
            num_layer_subshape2_minho.LostFocus += doPatternElementUI;
            num_layer_subshape3_minho.LostFocus += doPatternElementUI;

            num_layer_subshape_minvo.LostFocus += doPatternElementUI;
            num_layer_subshape2_minvo.LostFocus += doPatternElementUI;
            num_layer_subshape3_minvo.LostFocus += doPatternElementUI;

            num_layer_subshape_incHL.LostFocus += doPatternElementUI;
            num_layer_subshape2_incHL.LostFocus += doPatternElementUI;
            num_layer_subshape3_incHL.LostFocus += doPatternElementUI;

            num_layer_subshape_incVL.LostFocus += doPatternElementUI;
            num_layer_subshape2_incVL.LostFocus += doPatternElementUI;
            num_layer_subshape3_incVL.LostFocus += doPatternElementUI;

            num_layer_subshape_incHO.LostFocus += doPatternElementUI;
            num_layer_subshape2_incHO.LostFocus += doPatternElementUI;
            num_layer_subshape3_incHO.LostFocus += doPatternElementUI;

            num_layer_subshape_incVO.LostFocus += doPatternElementUI;
            num_layer_subshape2_incVO.LostFocus += doPatternElementUI;
            num_layer_subshape3_incVO.LostFocus += doPatternElementUI;

            num_layer_subshape_stepsHL.LostFocus += doPatternElementUI;
            num_layer_subshape2_stepsHL.LostFocus += doPatternElementUI;
            num_layer_subshape3_stepsHL.LostFocus += doPatternElementUI;

            num_layer_subshape_stepsVL.LostFocus += doPatternElementUI;
            num_layer_subshape2_stepsVL.LostFocus += doPatternElementUI;
            num_layer_subshape3_stepsVL.LostFocus += doPatternElementUI;

            num_layer_subshape_stepsHO.LostFocus += doPatternElementUI;
            num_layer_subshape2_stepsHO.LostFocus += doPatternElementUI;
            num_layer_subshape3_stepsHO.LostFocus += doPatternElementUI;

            num_layer_subshape_stepsVO.LostFocus += doPatternElementUI;
            num_layer_subshape2_stepsVO.LostFocus += doPatternElementUI;
            num_layer_subshape3_stepsVO.LostFocus += doPatternElementUI;

            comboBox_subShapeRef.SelectedIndexChanged += doPatternElementUI;
            comboBox_posSubShape.SelectedIndexChanged += doPatternElementUI;

            num_layer_minbbl.LostFocus += doPatternElementUI;
            num_layer_bblinc.LostFocus += doPatternElementUI;
            num_layer_bblsteps.LostFocus += doPatternElementUI;

            num_layer_minbbr.LostFocus += doPatternElementUI;
            num_layer_bbrinc.LostFocus += doPatternElementUI;
            num_layer_bbrsteps.LostFocus += doPatternElementUI;

            num_layer_minbbb.LostFocus += doPatternElementUI;
            num_layer_bbbinc.LostFocus += doPatternElementUI;
            num_layer_bbbsteps.LostFocus += doPatternElementUI;

            num_layer_minbbt.LostFocus += doPatternElementUI;
            num_layer_bbtinc.LostFocus += doPatternElementUI;
            num_layer_bbtsteps.LostFocus += doPatternElementUI;

            comboBox_xPosRef.SelectedIndexChanged += doPatternElementUI;
            comboBox_yPosRef.SelectedIndexChanged += doPatternElementUI;

            comboBox_xPos_subShapeRef.SelectedIndexChanged += doPatternElementUI;
            comboBox_yPos_subShapeRef.SelectedIndexChanged += doPatternElementUI;

            comboBox_xPos_subShapeRefPos.SelectedIndexChanged += doPatternElementUI;
            comboBox_yPos_subShapeRefPos.SelectedIndexChanged += doPatternElementUI;

            num_minXPos.LostFocus += doPatternElementUI;
            num_incXPos.LostFocus += doPatternElementUI;
            num_stepsXPos.LostFocus += doPatternElementUI;

            num_minYPos.LostFocus += doPatternElementUI;
            num_incYPos.LostFocus += doPatternElementUI;
            num_stepsYPos.LostFocus += doPatternElementUI;

            num_minRot.LostFocus += doPatternElementUI;
            num_incRot.LostFocus += doPatternElementUI;
            num_stepsRot.LostFocus += doPatternElementUI;
            checkBox_rotRef.CheckedChanged += doPatternElementUI;
            checkBox_refPivot.CheckedChanged += doPatternElementUI;
            checkBox_refBoundsAfterRotation.CheckedChanged += doPatternElementUI;

            comboBox_rotRef.SelectedIndexChanged += doPatternElementUI;

            num_minArrayRot.LostFocus += doPatternElementUI;
            num_incArrayRot.LostFocus += doPatternElementUI;
            num_stepsArrayRot.LostFocus += doPatternElementUI;

            comboBox_arrayRotRef.SelectedIndexChanged += doPatternElementUI;
            comboBox_arrayRef.SelectedIndexChanged += doPatternElementUI;
            checkBox_arrayRotRef.CheckedChanged += doPatternElementUI;
            checkBox_refArrayPivot.CheckedChanged += doPatternElementUI;
            checkBox_refArrayBoundsAfterRotation.CheckedChanged += doPatternElementUI;

            checkBox_flipH.CheckedChanged += doPatternElementUI;
            checkBox_flipV.CheckedChanged += doPatternElementUI;
            checkBox_alignX.CheckedChanged += doPatternElementUI;
            checkBox_alignY.CheckedChanged += doPatternElementUI;

            num_arrayMinXCount.LostFocus += doPatternElementUI;
            num_arrayXInc.LostFocus += doPatternElementUI;
            num_arrayXSteps.LostFocus += doPatternElementUI;
            num_arrayMinXSpace.LostFocus += doPatternElementUI;
            num_arrayXSpaceInc.LostFocus += doPatternElementUI;
            num_arrayXSpaceSteps.LostFocus += doPatternElementUI;
            num_arrayMinYCount.LostFocus += doPatternElementUI;
            num_arrayYInc.LostFocus += doPatternElementUI;
            num_arrayYSteps.LostFocus += doPatternElementUI;
            num_arrayMinYSpace.LostFocus += doPatternElementUI;
            num_arrayYSpaceInc.LostFocus += doPatternElementUI;
            num_arrayYSpaceSteps.LostFocus += doPatternElementUI;

            comboBox_merge.SelectedIndexChanged += doPatternElementUI;

            num_padding.LostFocus += setPadding;

            entry_Add.Click += addPatternElement;
            entry_Rename.Click += renamePatternElement;
            entry_Remove.Click += removePatternElement;

            btn_export.Click += exportClicked;

            if (num_externalGeoCoordsX != null)
            {
                try
                {
                    for (int i = 0; i < num_externalGeoCoordsX.Length; i++)
                    {
                        if (num_externalGeoCoordsX[i] != null)
                        {
                            num_externalGeoCoordsX[i].LostFocus += doPatternElementUI;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }

            if (num_externalGeoCoordsY != null)
            {
                try
                {
                    for (int i = 0; i < num_externalGeoCoordsY.Length; i++)
                    {
                        if (num_externalGeoCoordsY[i] != null)
                        {
                            num_externalGeoCoordsY[i].LostFocus += doPatternElementUI;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        void updatePatternElementUI(object sender, EventArgs e)
        {
            if (UIFreeze)
            {
                return;
            }
            updatePatternElementUI();
        }

        void updatePatternElementUI_subshape(int index)
        {
            // Use 0 index to find the base configuration pattern to display.
            comboBox_patternElementShape.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.shapeIndex);
            if (comboBox_patternElementShape.SelectedIndex == 0)
            {
                clearPatternElementUI();
                comboBox_patternElementShape.Visible = true;
            }

            groupBox_properties.Content = groupBox_subShapes_table;
            groupBox_position.Visible = true;

            switch (commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.shapeIndex))
            {
                case (int)CommonVars.shapeNames.none:
                    clearSubShapeVals(index, 2);
                    clearSubShapeVals(index, 1);
                    clearSubShapeVals(index, 0);
                    break;
                case (int)CommonVars.shapeNames.rect:
                case (int)CommonVars.shapeNames.text:
                    clearSubShapeVals(index, 2);
                    clearSubShapeVals(index, 1);
                    setSubShapeVals(index, 0);
                    break;
                case (int)CommonVars.shapeNames.Lshape:
                case (int)CommonVars.shapeNames.Tshape:
                case (int)CommonVars.shapeNames.Ushape:
                case (int)CommonVars.shapeNames.Xshape:
                    clearSubShapeVals(index, 2);
                    setSubShapeVals(index, 1);
                    setSubShapeVals(index, 0);
                    break;
                case (int)CommonVars.shapeNames.Sshape:
                    setSubShapeVals(index, 2);
                    setSubShapeVals(index, 1);
                    setSubShapeVals(index, 0);
                    break;
                case (int)CommonVars.shapeNames.bounding:
                    setBoundingShapeVals(index);
                    break;
                case (int)CommonVars.shapeNames.complex:
 
                    Scrollable s = new Scrollable();

                    // Iterate our edges. For each edge, add a row with a label, and numeric fields
                    externalGeoUI(index);

                    // Replace properties groupbox content with external layout content.
                    s.Content = groupBox_layout_table;
                    groupBox_properties.Content = s;
                    break;
            }

            updateLBContextMenu();
        }

        void externalGeoUI(int index)
        {
            // We need to replace the UI here with sufficient gadgets for our edges. Tricky.
            int numberOfRowsNeeded = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.externalGeoVertexCount);
            groupBox_layout_table = new TableLayout();
            if (numberOfRowsNeeded > 0)
            {
                num_externalGeoCoordsX = new NumericStepper[numberOfRowsNeeded];
                num_externalGeoCoordsY = new NumericStepper[numberOfRowsNeeded];

                for (int i = 0; i < numberOfRowsNeeded; i++)
                {
                    groupBox_layout_table.Rows.Add(new TableRow());
                    groupBox_layout_table.Rows[i].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(new Label() { Text = "Edge " + i }) });

                    double val0 = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.externalGeoCoordX, i));
                    num_externalGeoCoordsX[i] = new NumericStepper() { Value = val0, DecimalPlaces = 2 };
                    groupBox_layout_table.Rows[i].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_externalGeoCoordsX[i]) }); // length

                    double val1 = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.externalGeoCoordY, i));
                    num_externalGeoCoordsY[i] = new NumericStepper() { Value = val1, DecimalPlaces = 2 };
                    groupBox_layout_table.Rows[i].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_externalGeoCoordsY[i]) }); // increment

                    num_externalGeoCoordsX[i].LostFocus += doPatternElementUI;
                    num_externalGeoCoordsY[i].LostFocus += doPatternElementUI;
                }
            }
            else
            {
                groupBox_layout_table.Rows.Add(new TableRow());
                TextArea ta = new TextArea();
                ta.ReadOnly = true;
                ta.Text = "This shape type is reserved for non-orthogonal geometry found when defining a pattern from layout.\r\nIt is not currently available for other uses.";
                ta.Wrap = true;
                groupBox_layout_table.Rows[0].Cells.Add(new TableCell() { Control = ta });
                clearPositionBox();
            }
        }

        void updatePatternElementUI_positionX(int index)
        {
            comboBox_xPosRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosRef);
            // Sort out the subshape combobox contents.
            commonVars.xPosRefSubShapeList.Clear();
            commonVars.xPosRefSubShapeList.Add("1");
            int xPosRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosRef) - 1;

            comboBox_xPos_subShapeRef.Enabled = (xPosRef != -1);
            comboBox_xPos_subShapeRefPos.Enabled = (xPosRef != -1);

            if (xPosRef != -1)
            {
                int xRefSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, xPosRef).getSubShapeCount();

                for (int i = 1; i < xRefSubShapeCount; i++)
                {
                    commonVars.xPosRefSubShapeList.Add((i + 1).ToString());
                }

                bool array = commonVars.stitcher.getPatternElement(patternIndex: 0, xPosRef).isXArray();
                // Relative array definition?
                array = array || (commonVars.stitcher.getPatternElement(patternIndex: 0, xPosRef).getInt(PatternElement.properties_i.arrayRef) != 0);

                if (array)
                {
                    commonVars.xPosRefSubShapeList.Add("Array");
                }
                comboBox_xPos_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosSubShapeRef);
                comboBox_xPos_subShapeRefPos.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosSubShapeRefPos);
            }
            else
            {
                comboBox_xPos_subShapeRef.SelectedIndex = 0;
                comboBox_xPos_subShapeRefPos.SelectedIndex = (int)CommonVars.subShapeHorLocs.L;
            }
        }

        void updatePatternElementUI_positionY(int index)
        {
            comboBox_yPosRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosRef);
            // Sort out the subshape combobox contents.
            commonVars.yPosRefSubShapeList.Clear();
            commonVars.yPosRefSubShapeList.Add("1");
            int yPosRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosRef) - 1;

            comboBox_yPos_subShapeRef.Enabled = (yPosRef != -1);
            comboBox_yPos_subShapeRefPos.Enabled = (yPosRef != -1);

            if (yPosRef != -1)
            {
                int yRefSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, yPosRef).getSubShapeCount();

                for (int i = 1; i < yRefSubShapeCount; i++)
                {
                    commonVars.yPosRefSubShapeList.Add((i + 1).ToString());
                }

                bool array = commonVars.stitcher.getPatternElement(patternIndex: 0, yPosRef).isYArray();
                // Relative array definition?
                array = array || (commonVars.stitcher.getPatternElement(patternIndex: 0, yPosRef).getInt(PatternElement.properties_i.arrayRef) != 0);

                if (array)
                {
                    commonVars.yPosRefSubShapeList.Add("Array");
                }
                comboBox_yPos_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosSubShapeRef);
                comboBox_yPos_subShapeRefPos.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosSubShapeRefPos);
            }
            else
            {
                comboBox_yPos_subShapeRef.SelectedIndex = 0;
                comboBox_yPos_subShapeRefPos.SelectedIndex = (int)CommonVars.subShapeVerLocs.B;
            }
        }

        void updatePatternElementUI_position(int index)
        {
            comboBox_merge.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.linkedElementIndex) + 1;

            // Set the X and Y position references.
            comboBox_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.subShapeIndex);
            comboBox_posSubShape.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.posIndex);

            updatePatternElementUI_positionX(index);
            updatePatternElementUI_positionY(index);

            num_minXPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minXPos));
            num_minYPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minYPos));

            num_incXPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.xPosInc));
            num_incYPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.yPosInc));

            num_stepsXPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosSteps));
            num_stepsYPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosSteps));
        }

        void updatePatternElementUI_rotation(int index)
        {
            num_minRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minRotation));

            num_incRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.rotationInc));

            num_stepsRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotationSteps));

            comboBox_rotRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotationRef);

            int rotRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotationRef) - 1;
            bool rRef = false;

            checkBox_refPivot.Enabled = rRef;
            checkBox_refPivot.Checked = rRef;

            if ((commonVars.stitcher.getPatternElement(patternIndex:0, index).getInt(PatternElement.properties_i.relativeArray) == 1) || (commonVars.stitcher.getPatternElement(patternIndex: 0, index).isXArray()) || (commonVars.stitcher.getPatternElement(patternIndex: 0, index).isYArray()))
            {
                checkBox_refBoundsAfterRotation.Enabled = true;
                checkBox_refBoundsAfterRotation.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refBoundsAfterRotation) == 1;
            }
            else
            {
                checkBox_refBoundsAfterRotation.Enabled = rRef;
                checkBox_refBoundsAfterRotation.Checked = rRef;
            }
            if (rotRef >= 0)
            {
                checkBox_refPivot.Enabled = true;
                checkBox_refPivot.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refPivot) == 1;

                checkBox_refBoundsAfterRotation.Enabled = true;
                checkBox_refBoundsAfterRotation.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refBoundsAfterRotation) == 1;

                if (rotRef >= index)
                {
                    // Re-query index due to screening of active layer.
                    rotRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotationRef);
                }

                rRef = commonVars.stitcher.getPatternElement(patternIndex: 0, rotRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: 0, rotRef).isYArray();
                // Relative array definition?
                rRef = rRef || (commonVars.stitcher.getPatternElement(patternIndex: 0, rotRef).getInt(PatternElement.properties_i.arrayRef) != 0);
            }

            if (!rRef)
            {
                checkBox_rotRef.Checked = false;
            }
            else
            {
                checkBox_rotRef.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotRefUseArray) == 1;
            }
            checkBox_rotRef.Enabled = rRef;

        }

        void updatePatternElementUI_transform(int index)
        {
            checkBox_flipH.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.flipH) == 1;
            checkBox_flipV.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.flipV) == 1;
            checkBox_alignX.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.alignX) == 1;
            checkBox_alignY.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.alignY) == 1;
        }

        void updatePatternElementUI_array(int index)
        {
            bool isArray = false;
            bool isRelativeArray = false;

            bool bounding = (commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.shapeIndex) == (int)CommonVars.shapeNames.bounding);

            // Prevent any array offerings for bounding elements.
            if (!bounding)
            {
                isArray = (commonVars.stitcher.getPatternElement(patternIndex: 0, index).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: 0, index).isYArray());

                if (!isArray)
                {
                    isRelativeArray = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRef) > 0;
                }
            }

            num_arrayMinXCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayMinYCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSteps.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSteps.Enabled = bounding ? false : !isRelativeArray;
            num_arrayMinXSpace.Enabled = bounding ? false : !isRelativeArray;
            num_arrayMinYSpace.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSpaceInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSpaceInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSpaceSteps.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSpaceSteps.Enabled = bounding ? false : !isRelativeArray;

            // Register the relative array status with the pattern element.
            commonVars.stitcher.getPatternElement(patternIndex: 0, index).setInt(PatternElement.properties_i.relativeArray, isRelativeArray ? 1 : 0);

            num_minArrayRot.Enabled = isArray || isRelativeArray;
            num_incArrayRot.Enabled = isArray || isRelativeArray;
            num_stepsArrayRot.Enabled = isArray || isRelativeArray;

            num_arrayMinXSpace.Enabled = isArray || isRelativeArray;
            num_arrayXSpaceInc.Enabled = isArray || isRelativeArray;
            num_arrayXSpaceSteps.Enabled = isArray || isRelativeArray;

            num_arrayMinYSpace.Enabled = isArray || isRelativeArray;
            num_arrayYSpaceInc.Enabled = isArray || isRelativeArray;
            num_arrayYSpaceSteps.Enabled = isArray || isRelativeArray;

            comboBox_arrayRotRef.Enabled = isArray || isRelativeArray;
            checkBox_refArrayPivot.Enabled = isArray || isRelativeArray;

            num_arrayMinXCount.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayMinXCount);
            num_arrayMinYCount.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayMinYCount);
            num_arrayXInc.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayXInc);
            num_arrayYInc.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayYInc);
            num_arrayXSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayXSteps);
            num_arrayYSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayYSteps);
            num_arrayMinXSpace.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayMinXSpace));
            num_arrayMinYSpace.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayMinYSpace));
            num_arrayXSpaceInc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayXSpaceInc));
            num_arrayYSpaceInc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayYSpaceInc));
            num_arrayXSpaceSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayXSpaceSteps);
            num_arrayYSpaceSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayYSpaceSteps);
            comboBox_arrayRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRef);
            num_minArrayRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minArrayRotation));
            num_incArrayRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayRotationInc));
            num_stepsArrayRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationSteps));
            comboBox_arrayRotRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationRef);

            int arrayRotRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationRef) - 1;
            bool rArrayRef = false;

            checkBox_refArrayPivot.Enabled = rArrayRef;
            checkBox_refArrayPivot.Checked = rArrayRef;

            checkBox_refArrayBoundsAfterRotation.Enabled = rArrayRef;
            checkBox_refArrayBoundsAfterRotation.Checked = rArrayRef;

            if (arrayRotRef >= 0)
            {
                checkBox_refArrayPivot.Enabled = true;
                checkBox_refArrayPivot.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refArrayPivot) == 1;

                checkBox_refArrayBoundsAfterRotation.Enabled = true;
                checkBox_refArrayBoundsAfterRotation.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refArrayBoundsAfterRotation) == 1;

                if (arrayRotRef >= index)
                {
                    // Index needs to be re-queried as the current layer is omitted from the list.
                    arrayRotRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationRef);
                }

                rArrayRef = commonVars.stitcher.getPatternElement(patternIndex: 0, arrayRotRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: 0, arrayRotRef).isYArray();
                // Relative array definition?
                rArrayRef = rArrayRef || (commonVars.stitcher.getPatternElement(patternIndex: 0, arrayRotRef).getInt(PatternElement.properties_i.arrayRef) != 0);
            }

            if (!rArrayRef)
            {
                checkBox_arrayRotRef.Checked = false;
            }
            else
            {
                checkBox_arrayRotRef.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotRefUseArray) == 1;
            }
            checkBox_arrayRotRef.Enabled = rArrayRef;

        }

        void updatePatternElementUI(bool doPreview = true)
        {
            if (UIFreeze)
            {
                return;
            }
            int index = listBox_entries.SelectedIndex;
            if (index == -1)
            {
                if (commonVars.stitcher.patternElementNames.Count >= 1)
                {
                    listBox_entries.SelectedIndex = 0;
                    return;
                }
                else
                {
                    clearPatternElementUI();
                    return;
                }
            }

            // Otherwise, we need to configure the UI based on the selected element and update the values.
            UIFreeze = true;
            // commonVars.stitcher.updateQuilt(); // ensure we're up to date. Commented out as it was breaking things and we have a later call somewhere that catches this. Leaving in case of problems, though.
            // Update our UI observable collections for relative positioning.
            commonVars.stitcher.update_filteredPatternedElementNames(index);

            updatePatternElementUI_subshape(index);

            updatePatternElementUI_position(index);

            updatePatternElementUI_transform(index);

            updatePatternElementUI_rotation(index);

            updatePatternElementUI_array(index);

            UIFreeze = false;
            doPatternElementUI(0, updateUI: true, doPreview);

            btn_export.Enabled = commonVars.stitcher.getPatternCount() > 0;
        }

        void setBoundingShapeVals(int index)
        {
            // This should always be called under UIFreeze!
            if (!UIFreeze)
            {
                Error.ErrorReporter.showMessage_OK("Coding error: setBoundingShapeVals called without freeze", "Oops");
            }
            num_layer_minbbl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingLeft));
            num_layer_bblinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingLeftInc));
            num_layer_bblsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingLeftSteps);

            num_layer_minbbr.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingRight));
            num_layer_bbrinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingRightInc));
            num_layer_bbrsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingRightSteps);

            num_layer_minbbt.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingTop));
            num_layer_bbtinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingTopInc));
            num_layer_bbtsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingTopSteps);

            num_layer_minbbb.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingBottom));
            num_layer_bbbinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingBottomInc));
            num_layer_bbbsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingBottomSteps);
        }

        void setSubShapeVals(int index, int subShapeIndex)
        {
            switch (subShapeIndex)
            {
                case 0:
                    num_layer_subshape_minhl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength));
                    num_layer_subshape_minho.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0MinHorOffset));
                    num_layer_subshape_minvl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength));
                    num_layer_subshape_minvo.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0MinVerOffset));

                    num_layer_subshape_incHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0HorLengthInc));
                    num_layer_subshape_incHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0HorOffsetInc));
                    num_layer_subshape_incVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0VerLengthInc));
                    num_layer_subshape_incVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s0VerOffsetInc));

                    num_layer_subshape_stepsHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s0HorLengthSteps));
                    num_layer_subshape_stepsHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s0HorOffsetSteps));
                    num_layer_subshape_stepsVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s0VerLengthSteps));
                    num_layer_subshape_stepsVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s0VerOffsetSteps));
                    break;
                case 1:
                    num_layer_subshape2_minhl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1MinHorLength));
                    num_layer_subshape2_minho.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1MinHorOffset));
                    num_layer_subshape2_minvl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1MinVerLength));
                    num_layer_subshape2_minvo.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1MinVerOffset));

                    num_layer_subshape2_incHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1HorLengthInc));
                    num_layer_subshape2_incHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1HorOffsetInc));
                    num_layer_subshape2_incVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1VerLengthInc));
                    num_layer_subshape2_incVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s1VerOffsetInc));

                    num_layer_subshape2_stepsHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s1HorLengthSteps));
                    num_layer_subshape2_stepsHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s1HorOffsetSteps));
                    num_layer_subshape2_stepsVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s1VerLengthSteps));
                    num_layer_subshape2_stepsVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s1VerOffsetSteps));
                    break;
                case 2:
                    num_layer_subshape3_minhl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2MinHorLength));
                    num_layer_subshape3_minho.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2MinHorOffset));
                    num_layer_subshape3_minvl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2MinVerLength));
                    num_layer_subshape3_minvo.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2MinVerOffset));

                    num_layer_subshape3_incHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2HorLengthInc));
                    num_layer_subshape3_incHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2HorOffsetInc));
                    num_layer_subshape3_incVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2VerLengthInc));
                    num_layer_subshape3_incVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.s2VerOffsetInc));

                    num_layer_subshape3_stepsHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s2HorLengthSteps));
                    num_layer_subshape3_stepsHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s2HorOffsetSteps));
                    num_layer_subshape3_stepsVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s2VerLengthSteps));
                    num_layer_subshape3_stepsVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.s2VerOffsetSteps));
                    break;
            }
        }

        void clearSubShapeVals(int index, int subShapeIndex)
        {
            switch (subShapeIndex)
            {
                case 0:
                    num_layer_subshape_minhl.Value = 0;
                    num_layer_subshape_minho.Value = 0;
                    num_layer_subshape_minvl.Value = 0;
                    num_layer_subshape_minvo.Value = 0;

                    num_layer_subshape_incHL.Value = 0;
                    num_layer_subshape_incHO.Value = 0;
                    num_layer_subshape_incVL.Value = 0;
                    num_layer_subshape_incVO.Value = 0;

                    num_layer_subshape_stepsHL.Value = 1;
                    num_layer_subshape_stepsHO.Value = 1;
                    num_layer_subshape_stepsVL.Value = 1;
                    num_layer_subshape_stepsVO.Value = 1;
                    break;
                case 1:
                    num_layer_subshape2_minhl.Value = 0;
                    num_layer_subshape2_minho.Value = 0;
                    num_layer_subshape2_minvl.Value = 0;
                    num_layer_subshape2_minvo.Value = 0;

                    num_layer_subshape2_incHL.Value = 0;
                    num_layer_subshape2_incHO.Value = 0;
                    num_layer_subshape2_incVL.Value = 0;
                    num_layer_subshape2_incVO.Value = 0;

                    num_layer_subshape2_stepsHL.Value = 1;
                    num_layer_subshape2_stepsHO.Value = 1;
                    num_layer_subshape2_stepsVL.Value = 1;
                    num_layer_subshape2_stepsVO.Value = 1;
                    break;
                case 2:
                    num_layer_subshape3_minhl.Value = 0;
                    num_layer_subshape3_minho.Value = 0;
                    num_layer_subshape3_minvl.Value = 0;
                    num_layer_subshape3_minvo.Value = 0;

                    num_layer_subshape3_incHL.Value = 0;
                    num_layer_subshape3_incHO.Value = 0;
                    num_layer_subshape3_incVL.Value = 0;
                    num_layer_subshape3_incVO.Value = 0;

                    num_layer_subshape3_stepsHL.Value = 1;
                    num_layer_subshape3_stepsHO.Value = 1;
                    num_layer_subshape3_stepsVL.Value = 1;
                    num_layer_subshape3_stepsVO.Value = 1;
                    break;
            }
        }

        void clearPatternElementUI()
        {
            comboBox_patternElementShape.Visible = (commonVars.stitcher.getPatternCount() > 0);

            clearPositionBox();
            clearPropertiesBox();
        }

        void clearPropertiesBox()
        {
            groupBox_properties.Visible = false;
            num_layer_subshape_minhl.Enabled = false;
            num_layer_subshape_minvl.Enabled = false;
            num_layer_subshape_minho.Enabled = false;
            num_layer_subshape_minvo.Enabled = false;

            num_layer_subshape2_minhl.Enabled = false;
            num_layer_subshape2_minvl.Enabled = false;
            num_layer_subshape2_minho.Enabled = false;
            num_layer_subshape2_minvo.Enabled = false;

            num_layer_subshape3_minhl.Enabled = false;
            num_layer_subshape3_minvl.Enabled = false;
            num_layer_subshape3_minho.Enabled = false;
            num_layer_subshape3_minvo.Enabled = false;
        }

        void clearPositionBox()
        {
            groupBox_position.Visible = false;
        }

        void setupPatternElementUI_2()
        {
            Application.Instance.Invoke(() =>
            {
                TableRow right_tr1 = new TableRow();
                right_tl.Rows.Add(right_tr1);
                TableCell right_tr1_0 = new TableCell();
                right_tr1.Cells.Add(right_tr1_0);

                groupBox_properties = new GroupBox();
                subShapesTableLayout(right_tr1_0);
            });
        }

        Panel minHorLengthUI()
        {
            Panel p = new Panel();
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_hl = new Label();
            lbl_layer_subshape_hl.Text = "Min Hor. Length";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_hl, ScaleWidth = true });

            num_layer_subshape_minhl = new NumericStepper();
            num_layer_subshape_minhl.Increment = 0.1;
            num_layer_subshape_minhl.DecimalPlaces = 2;
            num_layer_subshape_minhl.MinValue = 0;
            setSize(num_layer_subshape_minhl, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minhl });

            num_layer_subshape2_minhl = new NumericStepper();
            num_layer_subshape2_minhl.Increment = 0.1;
            num_layer_subshape2_minhl.DecimalPlaces = 2;
            num_layer_subshape2_minhl.MinValue = 0;
            setSize(num_layer_subshape2_minhl, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minhl });

            num_layer_subshape3_minhl = new NumericStepper();
            num_layer_subshape3_minhl.Increment = 0.1;
            num_layer_subshape3_minhl.DecimalPlaces = 2;
            num_layer_subshape3_minhl.MinValue = 0;
            setSize(num_layer_subshape3_minhl, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minhl });

            return p;
        }

        Panel horLengthIncrementUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_incHL = new Label();
            lbl_layer_subshape_incHL.Text = "Hor. Length Increment";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incHL, ScaleWidth = true });

            num_layer_subshape_incHL = new NumericStepper();
            num_layer_subshape_incHL.Increment = 0.1;
            num_layer_subshape_incHL.DecimalPlaces = 2;
            setSize(num_layer_subshape_incHL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incHL });

            num_layer_subshape2_incHL = new NumericStepper();
            num_layer_subshape2_incHL.Increment = 0.1;
            num_layer_subshape2_incHL.DecimalPlaces = 2;
            setSize(num_layer_subshape2_incHL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incHL });

            num_layer_subshape3_incHL = new NumericStepper();
            num_layer_subshape3_incHL.Increment = 0.1;
            num_layer_subshape3_incHL.DecimalPlaces = 2;
            setSize(num_layer_subshape3_incHL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incHL });

            return p;
        }

        Panel horLengthStepsUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_stepsHL = new Label();
            lbl_layer_subshape_stepsHL.Text = "Hor. Length Steps";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsHL, ScaleWidth = true });

            num_layer_subshape_stepsHL = new NumericStepper();
            num_layer_subshape_stepsHL.MinValue = 1;
            num_layer_subshape_stepsHL.Increment = 1;
            num_layer_subshape_stepsHL.DecimalPlaces = 0;
            setSize(num_layer_subshape_stepsHL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsHL });

            num_layer_subshape2_stepsHL = new NumericStepper();
            num_layer_subshape2_stepsHL.MinValue = 1;
            num_layer_subshape2_stepsHL.Increment = 1;
            num_layer_subshape2_stepsHL.DecimalPlaces = 0;
            setSize(num_layer_subshape2_stepsHL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsHL });

            num_layer_subshape3_stepsHL = new NumericStepper();
            num_layer_subshape3_stepsHL.MinValue = 1;
            num_layer_subshape3_stepsHL.Increment = 1;
            num_layer_subshape3_stepsHL.DecimalPlaces = 0;
            setSize(num_layer_subshape3_stepsHL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsHL });

            return p;
        }

        Panel minHorOffsetUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_ho = new Label();
            lbl_layer_subshape_ho.Text = "Min Hor. Offset";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_ho, ScaleWidth = true });

            num_layer_subshape_minho = new NumericStepper();
            num_layer_subshape_minho.Increment = 0.1;
            num_layer_subshape_minho.DecimalPlaces = 2;
            setSize(num_layer_subshape_minho, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minho });

            num_layer_subshape2_minho = new NumericStepper();
            num_layer_subshape2_minho.Increment = 0.1;
            num_layer_subshape2_minho.DecimalPlaces = 2;
            setSize(num_layer_subshape2_minho, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minho });

            num_layer_subshape3_minho = new NumericStepper();
            num_layer_subshape3_minho.Increment = 0.1;
            num_layer_subshape3_minho.DecimalPlaces = 2;
            setSize(num_layer_subshape3_minho, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minho });

            return p;
        }

        Panel horOffsetIncrementUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_incHO = new Label();
            lbl_layer_subshape_incHO.Text = "Hor. Offset Increment";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incHO, ScaleWidth = true });

            num_layer_subshape_incHO = new NumericStepper();
            num_layer_subshape_incHO.Increment = 0.1;
            num_layer_subshape_incHO.DecimalPlaces = 2;
            setSize(num_layer_subshape_incHO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incHO });

            num_layer_subshape2_incHO = new NumericStepper();
            num_layer_subshape2_incHO.Increment = 0.1;
            num_layer_subshape2_incHO.DecimalPlaces = 2;
            setSize(num_layer_subshape2_incHO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incHO });

            num_layer_subshape3_incHO = new NumericStepper();
            num_layer_subshape3_incHO.Increment = 0.1;
            num_layer_subshape3_incHO.DecimalPlaces = 2;
            setSize(num_layer_subshape3_incHO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incHO });

            return p;
        }

        Panel horOffsetStepsUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_stepsHO = new Label();
            lbl_layer_subshape_stepsHO.Text = "Hor. Offset Steps";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsHO, ScaleWidth = true });

            num_layer_subshape_stepsHO = new NumericStepper();
            num_layer_subshape_stepsHO.MinValue = 1;
            num_layer_subshape_stepsHO.Increment = 1;
            num_layer_subshape_stepsHO.DecimalPlaces = 0;
            setSize(num_layer_subshape_stepsHO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsHO });

            num_layer_subshape2_stepsHO = new NumericStepper();
            num_layer_subshape2_stepsHO.MinValue = 1;
            num_layer_subshape2_stepsHO.Increment = 1;
            num_layer_subshape2_stepsHO.DecimalPlaces = 0;
            setSize(num_layer_subshape2_stepsHO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsHO });

            num_layer_subshape3_stepsHO = new NumericStepper();
            num_layer_subshape3_stepsHO.MinValue = 1;
            num_layer_subshape3_stepsHO.Increment = 1;
            num_layer_subshape3_stepsHO.DecimalPlaces = 0;
            setSize(num_layer_subshape3_stepsHO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsHO });

            return p;
        }

        Panel minVerLengthUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_vl = new Label();
            lbl_layer_subshape_vl.Text = "Min Ver. Length";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_vl, ScaleWidth = true });

            num_layer_subshape_minvl = new NumericStepper();
            num_layer_subshape_minvl.Increment = 0.1;
            num_layer_subshape_minvl.DecimalPlaces = 2;
            num_layer_subshape_minvl.MinValue = 0;
            setSize(num_layer_subshape_minvl, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minvl });

            num_layer_subshape2_minvl = new NumericStepper();
            num_layer_subshape2_minvl.Increment = 0.1;
            num_layer_subshape2_minvl.DecimalPlaces = 2;
            num_layer_subshape2_minvl.MinValue = 0;
            setSize(num_layer_subshape2_minvl, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minvl });

            num_layer_subshape3_minvl = new NumericStepper();
            num_layer_subshape3_minvl.Increment = 0.1;
            num_layer_subshape3_minvl.DecimalPlaces = 2;
            num_layer_subshape3_minvl.MinValue = 0;
            setSize(num_layer_subshape3_minvl, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minvl });

            return p;
        }

        Panel verLengthIncrementUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_incVL = new Label();
            lbl_layer_subshape_incVL.Text = "Ver. Length Increment";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incVL, ScaleWidth = true });

            num_layer_subshape_incVL = new NumericStepper();
            num_layer_subshape_incVL.Increment = 0.1;
            num_layer_subshape_incVL.DecimalPlaces = 2;
            setSize(num_layer_subshape_incVL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incVL });

            num_layer_subshape2_incVL = new NumericStepper();
            num_layer_subshape2_incVL.Increment = 0.1;
            num_layer_subshape2_incVL.DecimalPlaces = 2;
            setSize(num_layer_subshape2_incVL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incVL });

            num_layer_subshape3_incVL = new NumericStepper();
            num_layer_subshape3_incVL.Increment = 0.1;
            num_layer_subshape3_incVL.DecimalPlaces = 2;
            setSize(num_layer_subshape3_incVL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incVL });

            return p;
        }

        Panel verLengthStepsUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_stepsVL = new Label();
            lbl_layer_subshape_stepsVL.Text = "Ver. Length Steps";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsVL, ScaleWidth = true });

            num_layer_subshape_stepsVL = new NumericStepper();
            num_layer_subshape_stepsVL.MinValue = 1;
            num_layer_subshape_stepsVL.Increment = 1;
            num_layer_subshape_stepsVL.DecimalPlaces = 0;
            setSize(num_layer_subshape_stepsVL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsVL });

            num_layer_subshape2_stepsVL = new NumericStepper();
            num_layer_subshape2_stepsVL.MinValue = 1;
            num_layer_subshape2_stepsVL.Increment = 1;
            num_layer_subshape2_stepsVL.DecimalPlaces = 0;
            setSize(num_layer_subshape2_stepsVL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsVL });

            num_layer_subshape3_stepsVL = new NumericStepper();
            num_layer_subshape3_stepsVL.MinValue = 1;
            num_layer_subshape3_stepsVL.Increment = 1;
            num_layer_subshape3_stepsVL.DecimalPlaces = 0;
            setSize(num_layer_subshape3_stepsVL, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsVL });

            return p;
        }

        Panel minVerOffsetUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_vo = new Label();
            lbl_layer_subshape_vo.Text = "Min Ver. Offset";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_vo, ScaleWidth = true });

            num_layer_subshape_minvo = new NumericStepper();
            num_layer_subshape_minvo.Increment = 0.1;
            num_layer_subshape_minvo.DecimalPlaces = 2;
            setSize(num_layer_subshape_minvo, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minvo });

            num_layer_subshape2_minvo = new NumericStepper();
            num_layer_subshape2_minvo.Increment = 0.1;
            num_layer_subshape2_minvo.DecimalPlaces = 2;
            setSize(num_layer_subshape2_minvo, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minvo });

            num_layer_subshape3_minvo = new NumericStepper();
            num_layer_subshape3_minvo.Increment = 0.1;
            num_layer_subshape3_minvo.DecimalPlaces = 2;
            setSize(num_layer_subshape3_minvo, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minvo });

            return p;
        }

        Panel verOffsetIncrementUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_incVO = new Label();
            lbl_layer_subshape_incVO.Text = "Ver. Offset Increment";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incVO, ScaleWidth = true });

            num_layer_subshape_incVO = new NumericStepper();
            num_layer_subshape_incVO.Increment = 0.1;
            num_layer_subshape_incVO.DecimalPlaces = 2;
            setSize(num_layer_subshape_incVO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incVO });

            num_layer_subshape2_incVO = new NumericStepper();
            num_layer_subshape2_incVO.Increment = 0.1;
            num_layer_subshape2_incVO.DecimalPlaces = 2;
            setSize(num_layer_subshape2_incVO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incVO });

            num_layer_subshape3_incVO = new NumericStepper();
            num_layer_subshape3_incVO.Increment = 0.1;
            num_layer_subshape3_incVO.DecimalPlaces = 2;
            setSize(num_layer_subshape3_incVO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incVO });

            return p;
        }

        Panel verOffsetStepsUI()
        {
            Panel p = new Panel();

            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;

            lbl_layer_subshape_stepsVO = new Label();
            lbl_layer_subshape_stepsVO.Text = "Ver. Offset Steps";
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsVO, ScaleWidth = true });

            num_layer_subshape_stepsVO = new NumericStepper();
            num_layer_subshape_stepsVO.MinValue = 1;
            num_layer_subshape_stepsVO.Increment = 1;
            num_layer_subshape_stepsVO.DecimalPlaces = 0;
            setSize(num_layer_subshape_stepsVO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsVO });

            num_layer_subshape2_stepsVO = new NumericStepper();
            num_layer_subshape2_stepsVO.MinValue = 1;
            num_layer_subshape2_stepsVO.Increment = 1;
            num_layer_subshape2_stepsVO.DecimalPlaces = 0;
            setSize(num_layer_subshape2_stepsVO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsVO });

            num_layer_subshape3_stepsVO = new NumericStepper();
            num_layer_subshape3_stepsVO.MinValue = 1;
            num_layer_subshape3_stepsVO.Increment = 1;
            num_layer_subshape3_stepsVO.DecimalPlaces = 0;
            setSize(num_layer_subshape3_stepsVO, numWidth, num_Height);
            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsVO });

            return p;
        }

        void subShapesTableLayout(TableCell tc)
        {
            Application.Instance.Invoke(() =>
            {
                // groupBox_subShapes.Size = new Size(310, 150);
                groupBox_subShapes_table = new TableLayout();
                groupBox_properties.Content = groupBox_subShapes_table;
                groupBox_properties.Text = "SubShapes";
                tc.Control = groupBox_properties;

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = minHorLengthUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = horLengthIncrementUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = horLengthStepsUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = minHorOffsetUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = horOffsetIncrementUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = horOffsetStepsUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = minVerLengthUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = verLengthIncrementUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = verLengthStepsUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = minVerOffsetUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = verOffsetIncrementUI() });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = verOffsetStepsUI() });

                num_externalGeoCoordsX = new NumericStepper[1];
                num_externalGeoCoordsY = new NumericStepper[1];
            });
        }

        void setupPatternElementUI_3()
        {
            Application.Instance.Invoke(() =>
            {
                TableRow right_tr2 = new TableRow();
                right_tl.Rows.Add(right_tr2);

                TableCell right_tr2_0 = new TableCell();
                right_tr2.Cells.Add(right_tr2_0);

                groupBox_position = new GroupBox();

                Scrollable s = new Scrollable();
                TableLayout groupBox_position_table = new TableLayout();
                s.Content = groupBox_position_table;
                groupBox_position.Content = s;
                groupBox_position.Text = "Position";

                right_tr2_0.Control = groupBox_position;

                sp3_subShapeRef(groupBox_position_table);
                sp3_subShapePos(groupBox_position_table);
                sp3_subShapeXPos(groupBox_position_table);
                sp3_subShapeXRelPos(groupBox_position_table);
                sp3_subShapeXRelPosSS(groupBox_position_table);
                sp3_subShapeYPos(groupBox_position_table);
                sp3_subShapeYRelPos(groupBox_position_table);
                sp3_subShapeYRelPosSS(groupBox_position_table);
                sp3_Rot(groupBox_position_table);
                sp3_RelRot(groupBox_position_table);
                sp3_Flip(groupBox_position_table);
                sp3_Array(groupBox_position_table);
                sp3_Merge(groupBox_position_table);
            });
        }

        void sp3_subShapeRef(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_subShapeRef = new Label();
            lbl_subShapeRef.Text = "Subshape Reference";
            lbl_subShapeRef.ToolTip = "Which subshape to use for placement with respect to the world origin";

            tr.Cells.Add(new TableCell() { Control = lbl_subShapeRef });

            comboBox_subShapeRef = new DropDown();
            comboBox_subShapeRef.DataContext = DataContext;
            comboBox_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeList);
            comboBox_subShapeRef.SelectedIndex = 0;
            comboBox_subShapeRef.ToolTip = "Which subshape to use for placement with respect to the world origin";

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_subShapeRef) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void sp3_subShapePos(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_posSubShape = new Label();
            lbl_posSubShape.Text = "Subshape Position";
            lbl_posSubShape.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            tr.Cells.Add(new TableCell() { Control = lbl_posSubShape });

            comboBox_posSubShape = new DropDown();
            comboBox_posSubShape.DataContext = DataContext;
            comboBox_posSubShape.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapePos);
            comboBox_posSubShape.SelectedIndex = 0;
            comboBox_posSubShape.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_posSubShape) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void sp3_subShapeXPos(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_xPosRef = new Label();
            lbl_xPosRef.Text = "X Pos Ref";
            lbl_xPosRef.ToolTip = "Position this element in X relative to a different element, or world origin";
            tr.Cells.Add(new TableCell() { Control = lbl_xPosRef });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_minXPos = new NumericStepper();
            num_minXPos.Increment = 0.1;
            num_minXPos.DecimalPlaces = 2;
            setSize(num_minXPos, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_minXPos });

            num_incXPos = new NumericStepper();
            num_incXPos.Increment = 0.1;
            num_incXPos.DecimalPlaces = 2;
            setSize(num_incXPos, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_incXPos });

            num_stepsXPos = new NumericStepper();
            num_stepsXPos.Increment = 1;
            num_stepsXPos.DecimalPlaces = 0;
            num_stepsXPos.MinValue = 1;
            setSize(num_stepsXPos, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_stepsXPos });

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });

        }

        void sp3_subShapeXRelPos(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relXPos = new Label();
            lbl_relXPos.Text = "Relative Position";
            lbl_relXPos.ToolTip = "Relative Positioning";

            tr.Cells.Add(new TableCell() { Control = lbl_relXPos });

            comboBox_xPosRef = new DropDown();
            comboBox_xPosRef.DataContext = DataContext;
            comboBox_xPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_xPosRef.ToolTip = "Position in X relative to this pattern element";

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_xPosRef) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void sp3_subShapeXRelPosSS(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_subShapeXPos = new Label();
            lbl_subShapeXPos.Text = "Subshape Ref";
            lbl_subShapeXPos.ToolTip = "Reference subshape";
            tr.Cells.Add(new TableCell() { Control = lbl_subShapeXPos });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            comboBox_xPos_subShapeRef = new DropDown();
            comboBox_xPos_subShapeRef.DataContext = DataContext;
            comboBox_xPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.xPosRefSubShapeList);
            comboBox_xPos_subShapeRef.SelectedIndex = 0;
            comboBox_xPos_subShapeRef.ToolTip = "Subshape reference";

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_xPos_subShapeRef) });

            comboBox_xPos_subShapeRefPos = new DropDown();
            comboBox_xPos_subShapeRefPos.DataContext = DataContext;
            comboBox_xPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeHorPos);
            comboBox_xPos_subShapeRefPos.SelectedIndex = (int)CommonVars.subShapeHorLocs.L;
            comboBox_xPos_subShapeRefPos.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_xPos_subShapeRefPos) });

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });
        }

        void sp3_subShapeYPos(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_yPosRef = new Label();
            lbl_yPosRef.Text = "Y Pos Ref";
            lbl_yPosRef.ToolTip = "Position this element in Y relative to a different element, or world origin";

            tr.Cells.Add(new TableCell() { Control = lbl_yPosRef });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_minYPos = new NumericStepper();
            num_minYPos.Increment = 0.1;
            num_minYPos.DecimalPlaces = 2;
            setSize(num_minYPos, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_minYPos });

            num_incYPos = new NumericStepper();
            num_incYPos.Increment = 0.1;
            num_incYPos.DecimalPlaces = 2;
            setSize(num_incYPos, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_incYPos });

            num_stepsYPos = new NumericStepper();
            num_stepsYPos.Increment = 1;
            num_stepsYPos.DecimalPlaces = 0;
            num_stepsYPos.MinValue = 1;
            setSize(num_stepsYPos, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_stepsYPos });
        }

        void sp3_subShapeYRelPos(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relYPos = new Label();
            lbl_relYPos.Text = "Relative Position";
            lbl_relYPos.ToolTip = "Relative Positioning";

            tr.Cells.Add(new TableCell() { Control = lbl_relYPos });

            comboBox_yPosRef = new DropDown();
            comboBox_yPosRef.DataContext = DataContext;
            comboBox_yPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_yPosRef.SelectedIndex = 0;
            comboBox_yPosRef.ToolTip = "Position in Y relative to this pattern element";

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_yPosRef) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void sp3_subShapeYRelPosSS(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_subShapeYPos = new Label();
            lbl_subShapeYPos.Text = "Subshape Ref";
            lbl_subShapeYPos.ToolTip = "Reference subshape";
            tr.Cells.Add(new TableCell() { Control = lbl_subShapeYPos });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            TableRow ptl_tr = new TableRow();
            ptl.Rows.Add(ptl_tr);

            tr.Cells.Add(new TableCell() { Control = p });

            comboBox_yPos_subShapeRef = new DropDown();
            comboBox_yPos_subShapeRef.DataContext = DataContext;
            comboBox_yPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.yPosRefSubShapeList);
            comboBox_yPos_subShapeRef.SelectedIndex = 0;
            comboBox_yPos_subShapeRef.ToolTip = "Subshape reference";

            ptl_tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_yPos_subShapeRef) });

            comboBox_yPos_subShapeRefPos = new DropDown();
            comboBox_yPos_subShapeRefPos.DataContext = DataContext;
            comboBox_yPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeVerPos);
            comboBox_yPos_subShapeRefPos.SelectedIndex = (int)CommonVars.subShapeVerLocs.B;
            comboBox_yPos_subShapeRefPos.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            ptl_tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_yPos_subShapeRefPos) });

            ptl_tr.Cells.Add(new TableCell() { Control = null });
        }

        void sp3_Rot(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_rotation = new Label();
            lbl_rotation.Text = "Rotation";
            lbl_rotation.ToolTip = "Rotation";

            tr.Cells.Add(new TableCell() { Control = lbl_rotation });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_minRot = new NumericStepper();
            num_minRot.Increment = 0.1;
            num_minRot.DecimalPlaces = 2;
            setSize(num_minRot, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count -1].Cells.Add(new TableCell() { Control = num_minRot });

            num_incRot = new NumericStepper();
            num_incRot.Increment = 0.1;
            num_incRot.DecimalPlaces = 2;
            setSize(num_incRot, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_incRot });

            num_stepsRot = new NumericStepper();
            num_stepsRot.Increment = 1;
            num_stepsRot.DecimalPlaces = 0;
            num_stepsRot.MinValue = 1;
            setSize(num_stepsRot, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_stepsRot });
        }

        void sp3_RelRot(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relRot = new Label();
            lbl_relRot.Text = "Relative Rotation";
            lbl_relRot.ToolTip = "Relative Rotation";

            tr.Cells.Add(new TableCell() { Control = lbl_relRot });

            comboBox_rotRef = new DropDown();
            comboBox_rotRef.DataContext = DataContext;
            comboBox_rotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_rotRef.SelectedIndex = 0;
            comboBox_rotRef.ToolTip = "Rotation relative to this pattern element";

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            ptl.Rows.Add(new TableRow());
            p.Content = ptl;
            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_rotRef) });
            tr.Cells.Add(new TableCell() { Control = p });

            ptl.Rows.Add(new TableRow());

            TableLayout tl0 = new TableLayout();
            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(tl0) });

            tl0.Rows.Add(new TableRow());

            lbl_use = new Label();
            lbl_use.Text = "Use";
            tl0.Rows[tl0.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_use });

            checkBox_refPivot = new CheckBox();
            checkBox_refPivot.Text = "Pivot";
            checkBox_refPivot.Enabled = false;
            checkBox_refPivot.ToolTip = "Use pivot point from reference.";

            tl0.Rows[tl0.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refPivot });

            checkBox_rotRef = new CheckBox();
            checkBox_rotRef.Text = "Array";
            checkBox_rotRef.Enabled = false;
            checkBox_rotRef.ToolTip = "Use array rotation rather than shape.";

            tl0.Rows[tl0.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_rotRef });

            ptl.Rows.Add(new TableRow());

            checkBox_refBoundsAfterRotation = new CheckBox();
            checkBox_refBoundsAfterRotation.Text = "Bounds after rotation";
            checkBox_refBoundsAfterRotation.Enabled = false;
            checkBox_refBoundsAfterRotation.ToolTip = "Perform rotation before bounding box. This affects the pivot.";

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refBoundsAfterRotation });

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });
        }

        void sp3_Flip(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_flip = new Label();
            lbl_flip.Text = "Flip";
            lbl_flip.ToolTip = "Flip";

            tr.Cells.Add(new TableCell() { Control = lbl_flip });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            TableRow ptl_tr = new TableRow();
            ptl.Rows.Add(ptl_tr);
            tr.Cells.Add(new TableCell() { Control = p });

            checkBox_flipH = new CheckBox();
            checkBox_flipH.Text = "H";
            checkBox_flipH.ToolTip = "Flip horizontally";

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_flipH });

            checkBox_flipV = new CheckBox();
            checkBox_flipV.Text = "V";
            checkBox_flipV.ToolTip = "Flip vertically";

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_flipV });

            checkBox_alignX = new CheckBox();
            checkBox_alignX.Text = "Align X";
            checkBox_alignX.ToolTip = "Align flipped shape with original in X";

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_alignX });

            checkBox_alignY = new CheckBox();
            checkBox_alignY.Text = "Align Y";
            checkBox_alignY.ToolTip = "Align flipped shape with original in Y";

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_alignY });
        }

        void sp3_Array(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relArray = new Label();
            lbl_relArray.Text = "Relative Array Definition";
            lbl_relArray.ToolTip = "Relative Array Definition";

            tr.Cells.Add(new TableCell() { Control = lbl_relArray });

            comboBox_arrayRef = new DropDown();
            comboBox_arrayRef.DataContext = DataContext;
            comboBox_arrayRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered_array);
            comboBox_arrayRef.SelectedIndex = 0;
            comboBox_arrayRef.ToolTip = "Take array definition from this element";

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_arrayRef) });

            tr.Cells.Add(new TableCell() { Control = null });
            sp3_ArrayX(tl);
            sp3_ArrayY(tl);
            sp3_ArrayRot(tl);
        }

        void sp3_ArrayX(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayX = new Label();
            lbl_arrayX.Text = "Array X";
            lbl_arrayX.ToolTip = "Array X";

            tr.Cells.Add(new TableCell() { Control = lbl_arrayX });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinXCount = new NumericStepper();
            num_arrayMinXCount.Value = 1;
            num_arrayMinXCount.MinValue = 1;
            num_arrayMinXCount.Increment = 1;
            num_arrayMinXCount.DecimalPlaces = 0;
            num_arrayMinXCount.ToolTip = "Array X Count";
            setSize(num_arrayMinXCount, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayMinXCount });

            num_arrayXInc = new NumericStepper();
            num_arrayXInc.Value = 0;
            num_arrayXInc.MinValue = 0;
            num_arrayXInc.Increment = 1;
            num_arrayXInc.DecimalPlaces = 0;
            num_arrayXInc.ToolTip = "Array X Increment";
            setSize(num_arrayXInc, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayXInc });

            num_arrayXSteps = new NumericStepper();
            num_arrayXSteps.Value = 1;
            num_arrayXSteps.MinValue = 1;
            num_arrayXSteps.Increment = 1;
            num_arrayXSteps.DecimalPlaces = 0;
            num_arrayXSteps.ToolTip = "Array X Steps";
            setSize(num_arrayXSteps, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayXSteps });

            sp3_ArrayXSpace(tl);
        }

        void sp3_ArrayXSpace(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayX = new Label();
            lbl_arrayX.Text = "Array X Space";
            lbl_arrayX.ToolTip = "Array X Space";
            tr.Cells.Add(new TableCell() { Control = lbl_arrayX });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinXSpace = new NumericStepper();
            num_arrayMinXSpace.Value = 0;
            num_arrayMinXSpace.MinValue = 0;
            num_arrayMinXSpace.Increment = 0.01f; ;
            num_arrayMinXSpace.DecimalPlaces = 2;
            num_arrayMinXSpace.ToolTip = "Array X Space";
            setSize(num_arrayMinXSpace, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayMinXSpace });

            num_arrayXSpaceInc = new NumericStepper();
            num_arrayXSpaceInc.Value = 0;
            num_arrayXSpaceInc.MinValue = 0;
            num_arrayXSpaceInc.Increment = 0.01f;
            num_arrayXSpaceInc.DecimalPlaces = 2;
            num_arrayXSpaceInc.ToolTip = "Array X Space Increment";
            setSize(num_arrayXSpaceInc, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayXSpaceInc });

            num_arrayXSpaceSteps = new NumericStepper();
            num_arrayXSpaceSteps.Value = 1;
            num_arrayXSpaceSteps.MinValue = 1;
            num_arrayXSpaceSteps.Increment = 1;
            num_arrayXSpaceSteps.DecimalPlaces = 0;
            num_arrayXSpaceSteps.ToolTip = "Array X Space Steps";
            setSize(num_arrayXSpaceSteps, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayXSpaceSteps });
        }

        void sp3_ArrayY(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayY = new Label();
            lbl_arrayY.Text = "Array Y";
            lbl_arrayY.ToolTip = "Array Y";

            tr.Cells.Add(new TableCell() { Control = lbl_arrayY });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            TableRow tr0 = new TableRow();
            ptl.Rows.Add(tr0);
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinYCount = new NumericStepper();
            num_arrayMinYCount.Value = 1;
            num_arrayMinYCount.MinValue = 1;
            num_arrayMinYCount.Increment = 1;
            num_arrayMinYCount.DecimalPlaces = 0;
            num_arrayMinYCount.ToolTip = "Array Y Count";
            setSize(num_arrayMinYCount, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayMinYCount });

            num_arrayYInc = new NumericStepper();
            num_arrayYInc.Value = 0;
            num_arrayYInc.MinValue = 0;
            num_arrayYInc.Increment = 1;
            num_arrayYInc.DecimalPlaces = 0;
            num_arrayYInc.ToolTip = "Array Y Increment";
            setSize(num_arrayYInc, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayYInc });

            num_arrayYSteps = new NumericStepper();
            num_arrayYSteps.Value = 1;
            num_arrayYSteps.MinValue = 1;
            num_arrayYSteps.Increment = 1;
            num_arrayYSteps.DecimalPlaces = 0;
            num_arrayYSteps.ToolTip = "Array Y Steps";
            setSize(num_arrayYSteps, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayYSteps });

            sp3_ArrayYSpace(tl);
        }

        void sp3_ArrayYSpace(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayY = new Label();
            lbl_arrayY.Text = "Array Y Space";
            lbl_arrayY.ToolTip = "Array Y Space";
            tr.Cells.Add(new TableCell() { Control = lbl_arrayY });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            TableRow tr0 = new TableRow();
            ptl.Rows.Add(tr0);
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinYSpace = new NumericStepper();
            num_arrayMinYSpace.Value = 0;
            num_arrayMinYSpace.MinValue = 0;
            num_arrayMinYSpace.Increment = 0.01f;
            num_arrayMinYSpace.DecimalPlaces = 2;
            num_arrayMinYSpace.ToolTip = "Array Y Space";
            setSize(num_arrayMinYSpace, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayMinYSpace });

            num_arrayYSpaceInc = new NumericStepper();
            num_arrayYSpaceInc.Value = 0;
            num_arrayYSpaceInc.MinValue = 0;
            num_arrayYSpaceInc.Increment = 0.01f;
            num_arrayYSpaceInc.DecimalPlaces = 2;
            num_arrayYSpaceInc.ToolTip = "Array Y Space Increment";
            setSize(num_arrayYSpaceInc, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayYSpaceInc });

            num_arrayYSpaceSteps = new NumericStepper();
            num_arrayYSpaceSteps.Value = 1;
            num_arrayYSpaceSteps.MinValue = 1;
            num_arrayYSpaceSteps.Increment = 1;
            num_arrayYSpaceSteps.DecimalPlaces = 0;
            num_arrayYSpaceSteps.ToolTip = "Array Y Space Steps";
            setSize(num_arrayYSpaceSteps, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_arrayYSpaceSteps });
        }

        TableRow arrayRotationUI_1()
        {
            TableRow tr = new TableRow();
            lbl_arrayRotation = new Label();
            lbl_arrayRotation.Text = "Array Rotation";
            lbl_arrayRotation.ToolTip = "Array Rotation";

            tr.Cells.Add(new TableCell() { Control = lbl_arrayRotation });

            Panel p1 = new Panel();
            TableLayout ptl = new TableLayout();
            p1.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p1 });

            num_minArrayRot = new NumericStepper();
            num_minArrayRot.Increment = 0.1;
            num_minArrayRot.DecimalPlaces = 2;
            setSize(num_minArrayRot, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_minArrayRot });

            num_incArrayRot = new NumericStepper();
            num_incArrayRot.Increment = 0.1;
            num_incArrayRot.DecimalPlaces = 2;
            setSize(num_incArrayRot, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_incArrayRot });

            num_stepsArrayRot = new NumericStepper();
            num_stepsArrayRot.Increment = 1;
            num_stepsArrayRot.DecimalPlaces = 0;
            num_stepsArrayRot.MinValue = 1;
            setSize(num_stepsArrayRot, numWidth, num_Height);

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_stepsArrayRot });

            ptl.Rows[ptl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null });
            return tr;
        }

        TableRow arrayRotationUI_2()
        {
            TableRow tr = new TableRow();

            Label lbl_relArrayRot = new Label();
            lbl_relArrayRot.Text = "Relative Array Rotation";
            lbl_relArrayRot.ToolTip = "Relative Array Rotation";

            tr.Cells.Add(new TableCell() { Control = lbl_relArrayRot });

            Panel p = new Panel();
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            p.Content = tl;
            tr.Cells.Add(new TableCell() { Control = p });

            comboBox_arrayRotRef = new DropDown();
            comboBox_arrayRotRef.DataContext = DataContext;
            comboBox_arrayRotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_arrayRotRef.SelectedIndex = 0;
            comboBox_arrayRotRef.ToolTip = "Rotation relative to this pattern element";

            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_arrayRotRef) });

            TableRow tr1 = new TableRow();
            tl.Rows.Add(tr1);

            TableLayout tr1_tl = new TableLayout();
            tr1_tl.Rows.Add(new TableRow());

            tr1.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(tr1_tl) });

            lbl_arrayUse = new Label();
            lbl_arrayUse.Text = "Use";
            tr1_tl.Rows[tr1_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_arrayUse });

            checkBox_refArrayPivot = new CheckBox();
            checkBox_refArrayPivot.Text = "Pivot";
            checkBox_refArrayPivot.Enabled = false;
            checkBox_refArrayPivot.ToolTip = "Use pivot point from reference.";

            tr1_tl.Rows[tr1_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refArrayPivot });

            checkBox_arrayRotRef = new CheckBox();
            checkBox_arrayRotRef.Text = "Array";
            checkBox_arrayRotRef.Enabled = false;
            checkBox_arrayRotRef.ToolTip = "Use array rotation rather than shape.";

            tr1_tl.Rows[tr1_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_arrayRotRef });

            tl.Rows.Add(new TableRow());

            checkBox_refArrayBoundsAfterRotation = new CheckBox();
            checkBox_refArrayBoundsAfterRotation.Text = "Bounds after rotation";
            checkBox_refArrayBoundsAfterRotation.Enabled = false;
            checkBox_refArrayBoundsAfterRotation.ToolTip = "Perform rotation before bounding box. This affects the pivot.";

            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refArrayBoundsAfterRotation });

            tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            return tr;
        }

        void sp3_ArrayRot(TableLayout tl)
        {
            tl.Rows.Add(arrayRotationUI_1());

            tl.Rows.Add(arrayRotationUI_2());

            tl.Rows.Add(new TableRow() { ScaleHeight = true });
        }

        void sp3_Merge(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_merge = new Label();
            lbl_merge.Text = "Merge";
            lbl_merge.ToolTip = "Merge";

            tr.Cells.Add(new TableCell() { Control = lbl_merge });

            comboBox_merge = new DropDown();
            comboBox_merge.DataContext = DataContext;
            comboBox_merge.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);
            comboBox_merge.SelectedIndex = 0;
            comboBox_merge.ToolTip = "Union with this element on export";

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_merge) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void setupPatternElementUI_4()
        {
            Application.Instance.Invoke(() =>
            {
                int numWidth = 55;

                groupBox_bounding_table = new TableLayout();

                Panel row0 = new Panel();
                groupBox_bounding_table.Rows.Add(new TableRow());
                groupBox_bounding_table.Rows[groupBox_bounding_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row0 });

                TableLayout tl = new TableLayout();
                tl.Rows.Add(new TableRow());
                row0.Content = tl;

                Label bbLeft = new Label();
                bbLeft.Text = "Left Padding";
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = bbLeft });

                num_layer_minbbl = new NumericStepper();
                num_layer_minbbl.Increment = 0.1;
                num_layer_minbbl.DecimalPlaces = 2;
                setSize(num_layer_minbbl, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_minbbl });

                num_layer_bblinc = new NumericStepper();
                num_layer_bblinc.Increment = 0.1;
                num_layer_bblinc.DecimalPlaces = 2;
                setSize(num_layer_bblinc, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bblinc });

                num_layer_bblsteps = new NumericStepper();
                num_layer_bblsteps.Increment = 1;
                num_layer_bblsteps.DecimalPlaces = 0;
                num_layer_bblsteps.MinValue = 1;
                setSize(num_layer_bblsteps, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bblsteps });

                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

                tl.Rows.Add(new TableRow());

                Label bbRight = new Label();
                bbRight.Text = "Right Padding";
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = bbRight });

                num_layer_minbbr = new NumericStepper();
                num_layer_minbbr.Increment = 0.1;
                num_layer_minbbr.DecimalPlaces = 2;
                setSize(num_layer_minbbr, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_minbbr });

                num_layer_bbrinc = new NumericStepper();
                num_layer_bbrinc.Increment = 0.1;
                num_layer_bbrinc.DecimalPlaces = 2;
                setSize(num_layer_bbrinc, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bbrinc });

                num_layer_bbrsteps = new NumericStepper();
                num_layer_bbrsteps.Increment = 1;
                num_layer_bbrsteps.DecimalPlaces = 0;
                num_layer_bbrsteps.MinValue = 1;
                setSize(num_layer_bbrsteps, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bbrsteps });

                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

                tl.Rows.Add(new TableRow());

                Label bbBottom = new Label();
                bbBottom.Text = "Bottom Padding";
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = bbBottom });

                num_layer_minbbb = new NumericStepper();
                num_layer_minbbb.Increment = 0.1;
                num_layer_minbbb.DecimalPlaces = 2;
                setSize(num_layer_minbbb, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_minbbb });

                num_layer_bbbinc = new NumericStepper();
                num_layer_bbbinc.Increment = 0.1;
                num_layer_bbbinc.DecimalPlaces = 2;
                setSize(num_layer_bbbinc, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bbbinc });

                num_layer_bbbsteps = new NumericStepper();
                num_layer_bbbsteps.Increment = 1;
                num_layer_bbbsteps.DecimalPlaces = 0;
                num_layer_bbbsteps.MinValue = 1;
                setSize(num_layer_bbbsteps, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bbbsteps });

                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

                tl.Rows.Add(new TableRow());

                Label bbTop = new Label();
                bbTop.Text = "Top Padding";
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = bbTop });

                num_layer_minbbt = new NumericStepper();
                num_layer_minbbt.Increment = 0.1;
                num_layer_minbbt.DecimalPlaces = 2;
                setSize(num_layer_minbbt, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_minbbt });

                num_layer_bbtinc = new NumericStepper();
                num_layer_bbtinc.Increment = 0.1;
                num_layer_bbtinc.DecimalPlaces = 2;
                setSize(num_layer_bbtinc, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bbtinc });

                num_layer_bbtsteps = new NumericStepper();
                num_layer_bbtsteps.Increment = 1;
                num_layer_bbtsteps.DecimalPlaces = 0;
                num_layer_bbtsteps.MinValue = 1;
                setSize(num_layer_bbtsteps, numWidth, num_Height);
                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_bbtsteps });


                tl.Rows[tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

                groupBox_bounding_table.Rows.Add(new TableRow() { ScaleHeight = true });

            });
        }

        void quiltUISetup()
        {
            TableRow right_tr0 = new TableRow();
            right_tl.Rows.Add(right_tr0);

            comboBox_patternElementShape = new DropDown();
            comboBox_patternElementShape.Width = 120;
            comboBox_patternElementShape.DataContext = DataContext;
            comboBox_patternElementShape.BindDataContext(c => c.DataStore, (UIStringLists m) => m.shapes);
            comboBox_patternElementShape.SelectedIndex = 0;
            comboBox_patternElementShape.ToolTip = "Type of shape to generate";

            TableCell right_tr0_0 = new TableCell();
            right_tr0.Cells.Add(right_tr0_0);
            right_tr0_0.Control = comboBox_patternElementShape;

            setupPatternElementUI_2();
            setupPatternElementUI_3();
            setupPatternElementUI_4();

            right_tl.Rows.Add(new TableRow() { ScaleHeight = true });

            addHandlers();

            doColors();
        }

        void doColors()
        {
            Color lyr1Color = Color.FromArgb(quiltContext.colors.subshape1_Color.R,
                                            quiltContext.colors.subshape1_Color.G,
                                            quiltContext.colors.subshape1_Color.B);
            num_layer_subshape_minhl.TextColor = lyr1Color;
            num_layer_subshape_minvl.TextColor = lyr1Color;
            num_layer_subshape_minho.TextColor = lyr1Color;
            num_layer_subshape_minvo.TextColor = lyr1Color;

            num_layer_subshape_incHL.TextColor = lyr1Color;
            num_layer_subshape_incVL.TextColor = lyr1Color;
            num_layer_subshape_incHO.TextColor = lyr1Color;
            num_layer_subshape_incVO.TextColor = lyr1Color;

            num_layer_subshape_stepsHL.TextColor = lyr1Color;
            num_layer_subshape_stepsVL.TextColor = lyr1Color;
            num_layer_subshape_stepsHO.TextColor = lyr1Color;
            num_layer_subshape_stepsVO.TextColor = lyr1Color;

            Color lyr2Color = Color.FromArgb(quiltContext.colors.subshape2_Color.R,
                                            quiltContext.colors.subshape2_Color.G,
                                            quiltContext.colors.subshape2_Color.B);
            num_layer_subshape2_minhl.TextColor = lyr2Color;
            num_layer_subshape2_minvl.TextColor = lyr2Color;
            num_layer_subshape2_minho.TextColor = lyr2Color;
            num_layer_subshape2_minvo.TextColor = lyr2Color;

            num_layer_subshape2_incHL.TextColor = lyr2Color;
            num_layer_subshape2_incVL.TextColor = lyr2Color;
            num_layer_subshape2_incHO.TextColor = lyr2Color;
            num_layer_subshape2_incVO.TextColor = lyr2Color;

            num_layer_subshape2_stepsHL.TextColor = lyr2Color;
            num_layer_subshape2_stepsVL.TextColor = lyr2Color;
            num_layer_subshape2_stepsHO.TextColor = lyr2Color;
            num_layer_subshape2_stepsVO.TextColor = lyr2Color;

            Color lyr3Color = Color.FromArgb(quiltContext.colors.subshape3_Color.R,
                                            quiltContext.colors.subshape3_Color.G,
                                            quiltContext.colors.subshape3_Color.B);
            num_layer_subshape3_minhl.TextColor = lyr3Color;
            num_layer_subshape3_minvl.TextColor = lyr3Color;
            num_layer_subshape3_minho.TextColor = lyr3Color;
            num_layer_subshape3_minvo.TextColor = lyr3Color;

            num_layer_subshape3_incHL.TextColor = lyr3Color;
            num_layer_subshape3_incVL.TextColor = lyr3Color;
            num_layer_subshape3_incHO.TextColor = lyr3Color;
            num_layer_subshape3_incVO.TextColor = lyr3Color;

            num_layer_subshape3_stepsHL.TextColor = lyr3Color;
            num_layer_subshape3_stepsVL.TextColor = lyr3Color;
            num_layer_subshape3_stepsHO.TextColor = lyr3Color;
            num_layer_subshape3_stepsVO.TextColor = lyr3Color;

            ovpSettings.minorGridColor = Color.FromArgb(quiltContext.colors.minor_Color.toArgb());
            ovpSettings.majorGridColor = Color.FromArgb(quiltContext.colors.major_Color.toArgb());
            ovpSettings.axisColor = Color.FromArgb(quiltContext.colors.axis_Color.toArgb());
            ovpSettings.backColor = Color.FromArgb(quiltContext.colors.background_Color.toArgb());
            ovpSettings.reset(false);

        }

        void doPatternElementUI(object sender, EventArgs e)
        {
            if (UIFreeze)
            {
                return;
            }
            bool updateUI = false;
            try
            {
                if ((DropDown)sender == comboBox_patternElementShape)
                {
                    updateUI = true;
                }
            }
            catch (Exception)
            {

            }
            doPatternElementUI(0, updateUI);
            pasteLayer.Enabled = commonVars.stitcher.isCopySet();
            revertSim.Enabled = commonVars.projectFileName != "";
            updateLBContextMenu();
        }

        void doPatternElementUI_num(string shapeString)
        {
            groupBox_properties.Content = groupBox_subShapes_table;
            comboBox_patternElementShape.Visible = true;

            num_layer_subshape_minhl.Enabled = true;
            num_layer_subshape_incHL.Enabled = true;
            num_layer_subshape_stepsHL.Enabled = true;

            num_layer_subshape_minvl.Enabled = true;
            num_layer_subshape_incVL.Enabled = true;
            num_layer_subshape_stepsVL.Enabled = true;

            num_layer_subshape_minho.Enabled = true;
            num_layer_subshape_incHO.Enabled = true;
            num_layer_subshape_stepsHO.Enabled = true;

            num_layer_subshape_minvo.Enabled = true;
            num_layer_subshape_incVO.Enabled = true;
            num_layer_subshape_stepsVO.Enabled = true;

            // Any configuration beyond the first couple requires a second shape to be defined so we need to display that part of the interface.
            if ((shapeString != "none") && (shapeString != "rectangle"))
            {
                // Let's display the subshape 2 section if a shape configuration is chosen that requires it.
                num_layer_subshape2_minhl.Enabled = true;
                num_layer_subshape2_incHL.Enabled = true;
                num_layer_subshape2_stepsHL.Enabled = true;

                num_layer_subshape2_minvl.Enabled = true;
                num_layer_subshape2_incVL.Enabled = true;
                num_layer_subshape2_stepsVL.Enabled = true;

                num_layer_subshape2_minho.Enabled = true;
                num_layer_subshape2_incHO.Enabled = true;
                num_layer_subshape2_stepsHO.Enabled = true;

                num_layer_subshape2_minvo.Enabled = true;
                num_layer_subshape2_incVO.Enabled = true;
                num_layer_subshape2_stepsVO.Enabled = true;

                if (shapeString == "S")
                {
                    num_layer_subshape3_minhl.Enabled = true;
                    num_layer_subshape3_incHL.Enabled = true;
                    num_layer_subshape3_stepsHL.Enabled = true;

                    num_layer_subshape3_minvl.Enabled = true;
                    num_layer_subshape3_incVL.Enabled = true;
                    num_layer_subshape3_stepsVL.Enabled = true;

                    num_layer_subshape3_minho.Enabled = true;
                    num_layer_subshape3_incHO.Enabled = true;
                    num_layer_subshape3_stepsHO.Enabled = true;

                    num_layer_subshape3_minvo.Enabled = true;
                    num_layer_subshape3_incVO.Enabled = true;
                    num_layer_subshape3_stepsVO.Enabled = true;

                    commonVars.subshapes.Clear();
                    commonVars.subshapes.Add("1");
                    commonVars.subshapes.Add("2");
                    commonVars.subshapes.Add("3");
                }
                else
                {
                    num_layer_subshape3_minhl.Enabled = false;
                    num_layer_subshape3_incHL.Enabled = false;
                    num_layer_subshape3_stepsHL.Enabled = false;

                    num_layer_subshape3_minvl.Enabled = false;
                    num_layer_subshape3_incVL.Enabled = false;
                    num_layer_subshape3_stepsVL.Enabled = false;

                    num_layer_subshape3_minho.Enabled = false;
                    num_layer_subshape3_incHO.Enabled = false;
                    num_layer_subshape3_stepsHO.Enabled = false;

                    num_layer_subshape3_minvo.Enabled = false;
                    num_layer_subshape3_incVO.Enabled = false;
                    num_layer_subshape3_stepsVO.Enabled = false;

                    commonVars.subshapes.Clear();
                    commonVars.subshapes.Add("1");
                    commonVars.subshapes.Add("2");
                }
            }
            else
            {
                num_layer_subshape2_minhl.Enabled = false;
                num_layer_subshape2_incHL.Enabled = false;
                num_layer_subshape2_stepsHL.Enabled = false;

                num_layer_subshape2_minvl.Enabled = false;
                num_layer_subshape2_incVL.Enabled = false;
                num_layer_subshape2_stepsVL.Enabled = false;

                num_layer_subshape2_minho.Enabled = false;
                num_layer_subshape2_incHO.Enabled = false;
                num_layer_subshape2_stepsHO.Enabled = false;

                num_layer_subshape2_minvo.Enabled = false;
                num_layer_subshape2_incVO.Enabled = false;
                num_layer_subshape2_stepsVO.Enabled = false;

                num_layer_subshape3_minhl.Enabled = false;
                num_layer_subshape3_incHL.Enabled = false;
                num_layer_subshape3_stepsHL.Enabled = false;

                num_layer_subshape3_minvl.Enabled = false;
                num_layer_subshape3_incVL.Enabled = false;
                num_layer_subshape3_stepsVL.Enabled = false;

                num_layer_subshape3_minho.Enabled = false;
                num_layer_subshape3_incHO.Enabled = false;
                num_layer_subshape3_stepsHO.Enabled = false;

                num_layer_subshape3_minvo.Enabled = false;
                num_layer_subshape3_incVO.Enabled = false;
                num_layer_subshape3_stepsVO.Enabled = false;

                commonVars.subshapes.Clear();
                commonVars.subshapes.Add("1");
            }
        }

        void doPatternElementUI_baseShape(int pattern, int index, bool updateUI, string shapeString)
        {
            doPatternElementUI_baseShape1(pattern, index, updateUI, shapeString);
            doPatternElementUI_baseShape2(pattern, index, updateUI, shapeString);
        }

        void doPatternElementUI_baseShape1(int pattern, int index, bool updateUI, string shapeString)
        {
            if ((shapeString == "none") || (shapeString == "rectangle"))
            {
                clampSubShape2(minHLength: 0,
                    maxHLength: 1000000,
                    minVLength: 0,
                    maxVLength: 1000000,
                    minHOffset: -1000000,
                    maxHOffset: 1000000,
                    minVOffset: -1000000,
                    maxVOffset: 1000000
                );
                clampSubShape3(minHLength: 0,
                    maxHLength: 1000000,
                    minVLength: 0,
                    maxVLength: 1000000,
                    minHOffset: -1000000,
                    maxHOffset: 1000000,
                    minVOffset: -1000000,
                    maxVOffset: 1000000
                );

                if (shapeString == "none")
                {
                    num_layer_subshape_minhl.Value = 0;
                    num_layer_subshape_minvl.Value = 0;
                    num_layer_subshape_minho.Value = 0;
                    num_layer_subshape_minvo.Value = 0;
                }

                num_layer_subshape2_minhl.Value = 0;
                num_layer_subshape2_minvl.Value = 0;
                num_layer_subshape2_minho.Value = 0;
                num_layer_subshape2_minvo.Value = 0;

                num_layer_subshape3_minhl.Value = 0;
                num_layer_subshape3_minvl.Value = 0;
                num_layer_subshape3_minho.Value = 0;
                num_layer_subshape3_minvo.Value = 0;

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape1Tip, (int)CommonVars.tipLocations.none);

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value));
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value));

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape2Tip, (int)CommonVars.tipLocations.none);
            }

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorLength, Convert.ToDecimal(num_layer_subshape_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorLengthInc, Convert.ToDecimal(num_layer_subshape_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorLengthSteps, Convert.ToInt32(num_layer_subshape_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorOffset, Convert.ToDecimal(num_layer_subshape_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorOffsetInc, Convert.ToDecimal(num_layer_subshape_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerLength, Convert.ToDecimal(num_layer_subshape_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerLengthInc, Convert.ToDecimal(num_layer_subshape_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerLengthSteps, Convert.ToInt32(num_layer_subshape_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerOffset, Convert.ToDecimal(num_layer_subshape_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerOffsetInc, Convert.ToDecimal(num_layer_subshape_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsVO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape0Tip, (int)CommonVars.tipLocations.none);
        }

        void doPatternElementUI_baseShape2(int pattern, int index, bool updateUI, string shapeString)
        {
            // Subshape 2 offsets contingent on shape selection choice
            if ((shapeString != "none") && (shapeString != "rectangle") && (shapeString != "GEOCORE") && (shapeString != "BOOLEAN"))
            {
                clampSubShape(minHLength: 0.01,
                    maxHLength: 1000000,
                    minVLength: 0.01,
                    maxVLength: 1000000,
                    minHOffset: -1000000,
                    maxHOffset: 1000000,
                    minVOffset: -1000000,
                    maxVOffset: 1000000
                );

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape1Tip, (int)CommonVars.tipLocations.none);
                if (shapeString == "X") // Limit offsets of subshape 2 for X-shape.
                {
                    doUI_X(pattern, index);
                }
                else if (shapeString == "T") // Disabled horizontal offset of subshape 2 for T-shape.
                {
                    doUI_T(pattern, index);
                }
                else if (shapeString == "L") // Disable horizontal and vertical offsets of subshape 2 for L-shape
                {
                    doUI_L(pattern, index);
                }
                else if (shapeString == "U") // U-shape
                {
                    doUI_U(pattern, index);
                }
                else if (shapeString == "S") // S-shape
                {
                    doUI_S(pattern, index);
                }
                else
                {
                    if (updateUI)
                    {
                        num_layer_subshape2_minho.Enabled = true;
                        num_layer_subshape2_minvo.Enabled = true;
                    }
                    commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value));
                    commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value));
                    commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value));

                    commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value));
                    commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value));
                    commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value));
                }
            }
        }

        void doPatternElementUI_subshape(int pattern, int index, bool updateUI, string shapeString)
        {
            int previousIndex = comboBox_subShapeRef.SelectedIndex;

            if (updateUI)
            {
                if ((shapeString != "bounding") && (shapeString != "complex"))
                {
                    doPatternElementUI_num(shapeString);
                }
                else
                {
                    if (shapeString == "bounding")
                    {
                        groupBox_properties.Content = groupBox_bounding_table;
                    }
                    if (shapeString == "complex")
                    {
                        groupBox_properties.Content = groupBox_layout_table;
                    }

                    commonVars.subshapes.Clear();
                    commonVars.subshapes.Add("1");
                }
            }
            if (previousIndex >= commonVars.subshapes.Count)
            {
                previousIndex = commonVars.subshapes.Count - 1;
            }

            comboBox_subShapeRef.SelectedIndex = previousIndex;

            if ((shapeString != "bounding") && (shapeString != "complex"))
            {
                doPatternElementUI_baseShape(pattern, index, updateUI, shapeString);
            }
            else
            {
                if (shapeString == "bounding")
                {
                    doUI_bounding(pattern, index);
                }
                if (shapeString == "complex")
                {
                    doUI_complex(pattern, index);
                }
            }
        }

        void doPatternElementUI_position(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.linkedElementIndex, comboBox_merge.SelectedIndex - 1);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.subShapeIndex, comboBox_subShapeRef.SelectedIndex);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.posIndex, comboBox_posSubShape.SelectedIndex);

            int tmp = comboBox_xPosRef.SelectedIndex;
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosRef, tmp);
            tmp = comboBox_yPosRef.SelectedIndex;
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosRef, tmp);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosSubShapeRef, comboBox_xPos_subShapeRef.SelectedIndex);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosSubShapeRef, comboBox_yPos_subShapeRef.SelectedIndex);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosSubShapeRefPos, comboBox_xPos_subShapeRefPos.SelectedIndex);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosSubShapeRefPos, comboBox_yPos_subShapeRefPos.SelectedIndex);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minXPos, Convert.ToDecimal(num_minXPos.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minYPos, Convert.ToDecimal(num_minYPos.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.xPosInc, Convert.ToDecimal(num_incXPos.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.yPosInc, Convert.ToDecimal(num_incYPos.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosSteps, Convert.ToInt32(num_stepsXPos.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosSteps, Convert.ToInt32(num_stepsYPos.Value));
        }

        void doPatternElementUI_rotation(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minRotation, Convert.ToDecimal(num_minRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.rotationInc, Convert.ToDecimal(num_incRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.rotationSteps, Convert.ToInt32(num_stepsRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.rotationRef, comboBox_rotRef.SelectedIndex);

            int rRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.rotationRef) - 1;

            bool rRef_ = false;

            if (rRef >= 0)
            {
                if (rRef >= index)
                {
                    // Re-query index due to active layer screening.
                    rRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.rotationRef);
                }
                rRef_ = (commonVars.stitcher.getPatternElement(patternIndex: pattern, rRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: pattern, rRef).isYArray());
                // Disable if we have a relative array definition.
                rRef_ = rRef_ || (commonVars.stitcher.getPatternElement(patternIndex: 0, rRef).getInt(PatternElement.properties_i.arrayRef) == 1);

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refPivot, (bool)checkBox_refPivot.Checked ? 1 : 0);
            }
            else
            {
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refPivot, 0);

                checkBox_refPivot.Checked = false;
            }

            checkBox_rotRef.Enabled = rRef_;
            // Reset status if required.
            if (!rRef_)
            {
                checkBox_rotRef.Checked = false;
            }

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.rotRefUseArray, (bool)checkBox_rotRef.Checked ? 1 : 0);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refBoundsAfterRotation, (bool)checkBox_refBoundsAfterRotation.Checked ? 1 : 0);
        }

        void doPatternElementUI_transform(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.flipH, (bool)checkBox_flipH.Checked ? 1 : 0);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.flipV, (bool)checkBox_flipV.Checked ? 1 : 0);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.alignX, (bool)checkBox_alignX.Checked ? 1 : 0);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.alignY, (bool)checkBox_alignY.Checked ? 1 : 0);
        }

        void doPatternElementUI_array(int pattern, int index, string shapeString)
        {
            bool isArray = false;
            bool isRelativeArray = false;

            bool bounding = (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.shapeIndex) == (int)CommonVars.shapeNames.bounding);

            // Prevent any array offerings for bounding elements.
            if (!bounding)
            {
                isArray = (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: pattern, index).isYArray());

                if (!isArray)
                {
                    isRelativeArray = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.arrayRef) > 0;
                }
            }

            num_arrayMinXCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayMinYCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSteps.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSteps.Enabled = bounding ? false : !isRelativeArray;
            num_arrayMinXSpace.Enabled = bounding ? false : !isRelativeArray;
            num_arrayMinYSpace.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSpaceInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSpaceInc.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSpaceSteps.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSpaceSteps.Enabled = bounding ? false : !isRelativeArray;

            // Register the relative array status with the pattern element.
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.relativeArray, isRelativeArray ? 1 : 0);

            num_minArrayRot.Enabled = isArray || isRelativeArray;
            num_incArrayRot.Enabled = isArray || isRelativeArray;
            num_stepsArrayRot.Enabled = isArray || isRelativeArray;

            num_arrayMinXSpace.Enabled = isArray || isRelativeArray;
            num_arrayXSpaceInc.Enabled = isArray || isRelativeArray;
            num_arrayXSpaceSteps.Enabled = isArray || isRelativeArray;

            num_arrayMinYSpace.Enabled = isArray || isRelativeArray;
            num_arrayYSpaceInc.Enabled = isArray || isRelativeArray;
            num_arrayYSpaceSteps.Enabled = isArray || isRelativeArray;

            comboBox_arrayRotRef.Enabled = isArray || isRelativeArray;
            checkBox_refArrayPivot.Enabled = isArray || isRelativeArray;

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayMinXCount, Convert.ToInt32(num_arrayMinXCount.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayMinYCount, Convert.ToInt32(num_arrayMinYCount.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayXInc, Convert.ToInt32(num_arrayXInc.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayYInc, Convert.ToInt32(num_arrayYInc.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayXSteps, Convert.ToInt32(num_arrayXSteps.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayYSteps, Convert.ToInt32(num_arrayYSteps.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayMinXSpace, Convert.ToDecimal(num_arrayMinXSpace.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayMinYSpace, Convert.ToDecimal(num_arrayMinYSpace.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayXSpaceInc, Convert.ToDecimal(num_arrayXSpaceInc.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayYSpaceInc, Convert.ToDecimal(num_arrayYSpaceInc.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayXSpaceSteps, Convert.ToInt32(num_arrayXSpaceSteps.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayYSpaceSteps, Convert.ToInt32(num_arrayYSpaceSteps.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minArrayRotation, Convert.ToDecimal(num_minArrayRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayRotationInc, Convert.ToDecimal(num_incArrayRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRotationSteps, Convert.ToInt32(num_stepsArrayRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRotationRef, comboBox_arrayRotRef.SelectedIndex);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRef, comboBox_arrayRef.SelectedIndex);

            int aRRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.arrayRotationRef) - 1;

            bool rRefArray = false;
            if (aRRef >= 0)
            {
                if (aRRef >= index)
                {
                    // Fix index based on omission of active layer.
                    aRRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.arrayRotationRef);
                }
                rRefArray = (commonVars.stitcher.getPatternElement(patternIndex: pattern, aRRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: pattern, aRRef).isYArray());
                // Disable if we have a relative array definition.
                rRefArray = rRefArray || (commonVars.stitcher.getPatternElement(patternIndex: 0, aRRef).getInt(PatternElement.properties_i.arrayRef) == 1);

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refArrayPivot, (bool)checkBox_refArrayPivot.Checked ? 1 : 0);
            }
            else
            {
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refArrayPivot, 0);

                checkBox_refArrayPivot.Checked = false;
            }

            checkBox_arrayRotRef.Enabled = rRefArray;
            // Reset status if required.
            if (!rRefArray)
            {
                checkBox_arrayRotRef.Checked = false;
            }

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRotRefUseArray, (bool)checkBox_arrayRotRef.Checked ? 1 : 0);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refArrayBoundsAfterRotation, (bool)checkBox_refArrayBoundsAfterRotation.Checked ? 1 : 0);
        }

        void doPatternElementUI(int pattern, bool updateUI = false, bool doPreview = true)
        {
            if (UIFreeze)
            {
                return;
            }

            if (suspendBuild)
            {
                updateProgressLabel("Build suspended.");
            }

            UIFreeze = true;

            int index = listBox_entries.SelectedIndex;

            if (index == -1)
            {
                UIFreeze = false;
                clearPatternElementUI();
                comboBox_patternElementShape.Visible = true;
                return;
            }

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setString(PatternElement.properties_s.name, commonVars.stitcher.patternElementNames[index]);

            comboBox_patternElementShape.Visible = true;

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shapeIndex, comboBox_patternElementShape.SelectedIndex);

            if (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.shapeIndex) == (int)CommonVars.shapeNames.none)
            {

                if (doPreview)
                {
                    drawPreviewPanelHandler();
                }
                UIFreeze = false;
                clearPatternElementUI();
                return;
            }

            groupBox_properties.Visible = true;

            string shapeString = ((CentralProperties.typeShapes)commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.shapeIndex)).ToString();

            if (shapeString == "bounding")
            {
                groupBox_properties.Content = groupBox_bounding_table;
            }
            if (shapeString == "complex")
            {
                groupBox_properties.Content = groupBox_layout_table;
            }

            doPatternElementUI_subshape(pattern, index, updateUI, shapeString);
            doPatternElementUI_position(pattern, index);
            doPatternElementUI_rotation(pattern, index);
            doPatternElementUI_transform(pattern, index);
            doPatternElementUI_array(pattern, index, shapeString);


            UIFreeze = false;

            if (doPreview)
            {
                drawPreviewPanelHandler();
            }
        }

        void doUI_X(int pattern, int index)
        {
            // Validate our settings and clamp the inputs as needed.
            clampSubShape(minHLength: 0.04, 
                maxHLength: 1000000, 
                minVLength: 0.04, 
                maxVLength: 1000000, 
                minHOffset: -1000000, 
                maxHOffset: 1000000, 
                minVOffset: -1000000, 
                maxVOffset: 1000000
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorLength, Convert.ToDecimal(num_layer_subshape_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorLengthInc, Convert.ToDecimal(num_layer_subshape_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorLengthSteps, Convert.ToInt32(num_layer_subshape_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorOffset, Convert.ToDecimal(num_layer_subshape_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorOffsetInc, Convert.ToDecimal(num_layer_subshape_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerLength, Convert.ToDecimal(num_layer_subshape_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerLengthInc, Convert.ToDecimal(num_layer_subshape_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerLengthSteps, Convert.ToInt32(num_layer_subshape_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerOffset, Convert.ToDecimal(num_layer_subshape_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerOffsetInc, Convert.ToDecimal(num_layer_subshape_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsVO.Value));

            num_layer_subshape3_minhl.Value = 0;
            num_layer_subshape3_minvl.Value = 0;
            num_layer_subshape3_minho.Value = 0;
            num_layer_subshape3_minvo.Value = 0;

            decimal minSS2VOffset = 1;
            decimal maxSS2VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s1MinVerLength);

            decimal minSS2HOffset = -(commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s1MinHorLength) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength));
            decimal maxSS2HOffset = -1;

            decimal minSS2HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength) + (2 * 0.01m);
            decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - (2 * 0.01m);
            if (maxSS2VLength < 0)
            {
                maxSS2VLength = 0.02m;
            }

            clampSubShape2(minHLength: (double)minSS2HLength, 
                maxHLength: 1000000, 
                minVLength: 0.02, 
                maxVLength: (double)maxSS2VLength, 
                minHOffset: (double)minSS2HOffset, 
                maxHOffset: (double)maxSS2HOffset, 
                minVOffset: (double)minSS2VOffset, 
                maxVOffset: (double)maxSS2VOffset
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value));

            num_layer_subshape2_minho.Enabled = true;
            num_layer_subshape2_stepsHO.Enabled = true;
            num_layer_subshape2_incHO.Enabled = true;

            num_layer_subshape2_minvo.Enabled = true;
            num_layer_subshape2_stepsVO.Enabled = true;
            num_layer_subshape2_incVO.Enabled = true;
        }

        void doUI_T(int pattern, int index)
        {
            // Validate our settings and clamp the inputs as needed.
            clampSubShape(minHLength: 0.01, 
                maxHLength: 1000000, 
                minVLength: 0.04, 
                maxVLength: 1000000, 
                minHOffset: -1000000, 
                maxHOffset: 1000000, 
                minVOffset: -1000000, 
                maxVOffset: 1000000
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorLength, Convert.ToDecimal(num_layer_subshape_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorLengthInc, Convert.ToDecimal(num_layer_subshape_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorLengthSteps, Convert.ToInt32(num_layer_subshape_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorOffset, Convert.ToDecimal(num_layer_subshape_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorOffsetInc, Convert.ToDecimal(num_layer_subshape_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerLength, Convert.ToDecimal(num_layer_subshape_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerLengthInc, Convert.ToDecimal(num_layer_subshape_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerLengthSteps, Convert.ToInt32(num_layer_subshape_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerOffset, Convert.ToDecimal(num_layer_subshape_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerOffsetInc, Convert.ToDecimal(num_layer_subshape_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsVO.Value));

            num_layer_subshape3_minhl.Value = 0;
            num_layer_subshape3_minvl.Value = 0;
            num_layer_subshape3_minho.Value = 0;
            num_layer_subshape3_minvo.Value = 0;

            decimal minSS2HLength = 0.01m;
            decimal minSS2VLength = 0.02m;
            decimal ss2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength);
            decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - (2 * 0.01m);
            decimal maxSS2VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s1MinVerLength);

            clampSubShape2(minHLength: (double)minSS2HLength,
                maxHLength: 1000000,
                minVLength: (double)minSS2VLength,
                maxVLength: (double)maxSS2VLength,
                minHOffset: (double)ss2HOffset,
                maxHOffset: (double)ss2HOffset,
                minVOffset: 1,
                maxVOffset: (double)maxSS2VOffset
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value));

            num_layer_subshape2_minho.Enabled = false;
            num_layer_subshape2_minvo.Enabled = true;
        }

        void doUI_L(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorLength, Convert.ToDecimal(num_layer_subshape_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorLengthInc, Convert.ToDecimal(num_layer_subshape_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorLengthSteps, Convert.ToInt32(num_layer_subshape_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorOffset, Convert.ToDecimal(num_layer_subshape_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorOffsetInc, Convert.ToDecimal(num_layer_subshape_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerLength, Convert.ToDecimal(num_layer_subshape_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerLengthInc, Convert.ToDecimal(num_layer_subshape_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerLengthSteps, Convert.ToInt32(num_layer_subshape_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerOffset, Convert.ToDecimal(num_layer_subshape_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerOffsetInc, Convert.ToDecimal(num_layer_subshape_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsVO.Value));

            num_layer_subshape3_minhl.Value = 0;
            num_layer_subshape3_minvl.Value = 0;
            num_layer_subshape3_minho.Value = 0;
            num_layer_subshape3_minvo.Value = 0;

            decimal minSS2HLength = 0;
            decimal minSS2VLength = 0;
            decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength);

            decimal minSS2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength);
            decimal maxSS2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength);
            decimal minSS2VOffset = 0;
            decimal maxSS2VOffset = 0;

            clampSubShape2(minHLength: (double)minSS2HLength,
                maxHLength: 1000000, 
                minVLength: (double)minSS2VLength, 
                maxVLength: (double)maxSS2VLength,
                minHOffset: (double)minSS2HOffset, 
                maxHOffset: (double)maxSS2HOffset,
                minVOffset: (double)minSS2VOffset, 
                maxVOffset: (double)maxSS2VOffset
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value));

            num_layer_subshape2_minho.Enabled = false;
            num_layer_subshape2_stepsHO.Enabled = false;
            num_layer_subshape2_incHO.Enabled = false;

            num_layer_subshape2_minvo.Enabled = false;
            num_layer_subshape2_stepsVO.Enabled = false;
            num_layer_subshape2_incVO.Enabled = false;
        }

        void doUI_U(int pattern, int index)
        {
            // Validate our settings and clamp the inputs as needed.
            clampSubShape(minHLength: 0.04, 
                maxHLength: 1000000, 
                minVLength: 0.04, 
                maxVLength: 1000000, 
                minHOffset: -1000000, 
                maxHOffset: 1000000, 
                minVOffset: -1000000, 
                maxVOffset: 1000000
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorLength, Convert.ToDecimal(num_layer_subshape_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorLengthInc, Convert.ToDecimal(num_layer_subshape_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorLengthSteps, Convert.ToInt32(num_layer_subshape_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorOffset, Convert.ToDecimal(num_layer_subshape_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorOffsetInc, Convert.ToDecimal(num_layer_subshape_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerLength, Convert.ToDecimal(num_layer_subshape_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerLengthInc, Convert.ToDecimal(num_layer_subshape_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerLengthSteps, Convert.ToInt32(num_layer_subshape_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerOffset, Convert.ToDecimal(num_layer_subshape_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerOffsetInc, Convert.ToDecimal(num_layer_subshape_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsVO.Value));

            num_layer_subshape3_minhl.Value = 0;
            num_layer_subshape3_minvl.Value = 0;
            num_layer_subshape3_minho.Value = 0;
            num_layer_subshape3_minvo.Value = 0;

            decimal minSS2HLength = 0.01m;
            decimal minSS2VLength = 0.01m;
            decimal maxSS2HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength) - 0.02m;
            decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - 0.01m;

            decimal ss2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength)  - (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s1MinHorLength) + 0.01m);
            decimal ss2VOffset = (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s1MinVerLength));

            clampSubShape2(minHLength: (double)minSS2HLength,
                maxHLength: (double)maxSS2HLength,
                minVLength: (double)minSS2VLength,
                maxVLength: (double)maxSS2VLength,
                minHOffset: 0.01f, 
                maxHOffset: (double)ss2HOffset,
                minVOffset: (double)ss2VOffset, 
                maxVOffset: (double)ss2VOffset
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value));

            num_layer_subshape2_minho.Enabled = true;
            num_layer_subshape2_stepsHO.Enabled = true;
            num_layer_subshape2_incHO.Enabled = true;

            num_layer_subshape2_minvo.Enabled = false;
            num_layer_subshape2_stepsVO.Enabled = false;
            num_layer_subshape2_incVO.Enabled = false;
        }

        void doUI_S(int pattern, int index)
        {
            // Validate our settings and clamp the inputs as needed.
            clampSubShape(minHLength: 0.04, 
                maxHLength: 1000000, 
                minVLength: 0.04, 
                maxVLength: 1000000, 
                minHOffset: -1000000, 
                maxHOffset: 1000000, 
                minVOffset: -1000000, 
                maxVOffset: 1000000
            );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorLength, Convert.ToDecimal(num_layer_subshape_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorLengthInc, Convert.ToDecimal(num_layer_subshape_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorLengthSteps, Convert.ToInt32(num_layer_subshape_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinHorOffset, Convert.ToDecimal(num_layer_subshape_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0HorOffsetInc, Convert.ToDecimal(num_layer_subshape_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0HorOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerLength, Convert.ToDecimal(num_layer_subshape_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerLengthInc, Convert.ToDecimal(num_layer_subshape_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerLengthSteps, Convert.ToInt32(num_layer_subshape_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0MinVerOffset, Convert.ToDecimal(num_layer_subshape_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s0VerOffsetInc, Convert.ToDecimal(num_layer_subshape_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s0VerOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsVO.Value));

            decimal minSS2HLength = 0.01m;
            decimal maxSS2HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength) - 0.01m;
            decimal minSS2VLength = 0.02m;
            decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - 0.01m;
            decimal ss2HOffset = 0;
            decimal minSS2VOffset = 0.01m;
            decimal maxSS2VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s1MinVerLength);
            clampSubShape2(minHLength: (double)minSS2HLength, 
                maxHLength: (double)maxSS2HLength, 
                minVLength: (double)minSS2VLength, 
                maxVLength: (double)maxSS2VLength,
                minHOffset: (double)ss2HOffset,
                maxHOffset: (double)ss2HOffset,
                minVOffset: (double)minSS2VOffset,
                maxVOffset: (double)maxSS2VOffset
            );

            decimal minSS3HLength = 0.01m;
            decimal maxSS3HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength) - 0.01m;
            decimal minSS3VLength = 0.02m;
            decimal maxSS3VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - 0.01m;
            decimal ss3HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinHorLength) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s2MinHorLength);
            decimal minSS3VOffset = 0.01m;
            decimal maxSS3VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s0MinVerLength) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.s2MinVerLength);
            clampSubShape3(minHLength: (double)minSS3HLength,
                maxHLength: (double)maxSS3HLength, 
                minVLength: (double)minSS3VLength, 
                maxVLength: (double)maxSS3VLength,
                minHOffset: (double)ss3HOffset, 
                maxHOffset: (double)ss3HOffset, 
                minVOffset: (double)minSS3VOffset, 
                maxVOffset: (double)maxSS3VOffset
                );

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1HorOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1HorOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1MinVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s1VerOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s1VerOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2HorOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2HorOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2MinVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.s2VerOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.s2VerOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value));

            // FIXME: Need some logic here to avoid bisection of the S.

            num_layer_subshape2_minho.Enabled = false;
            num_layer_subshape2_stepsHO.Enabled = false;
            num_layer_subshape2_incHO.Enabled = false;
            num_layer_subshape2_minvo.Enabled = true;
            num_layer_subshape2_stepsVO.Enabled = true;
            num_layer_subshape2_incVO.Enabled = true;

            num_layer_subshape3_minho.Enabled = false;
            num_layer_subshape3_stepsHO.Enabled = false;
            num_layer_subshape3_incHO.Enabled = false;
            num_layer_subshape3_minvo.Enabled = true;
            num_layer_subshape3_stepsVO.Enabled = true;
            num_layer_subshape3_incVO.Enabled = true;
        }

        void doUI_bounding(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingLeftSteps, (int)num_layer_bblsteps.Value);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingLeft, Convert.ToDecimal(num_layer_minbbl.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingLeftInc, Convert.ToDecimal(num_layer_bblinc.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingRightSteps, (int)num_layer_bbrsteps.Value);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingRight, Convert.ToDecimal(num_layer_minbbr.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingRightInc, Convert.ToDecimal(num_layer_bbrinc.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingTopSteps, (int)num_layer_bbtsteps.Value);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingTop, Convert.ToDecimal(num_layer_minbbt.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingTopInc, Convert.ToDecimal(num_layer_bbtinc.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingBottomSteps, (int)num_layer_bbbsteps.Value);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingBottom, Convert.ToDecimal(num_layer_minbbb.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingBottomInc, Convert.ToDecimal(num_layer_bbbinc.Value));
        }

        void doUI_complex(int pattern, int index)
        {
            for (int i = 0; i < commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.externalGeoVertexCount); i++)
            {
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.externalGeoCoordX, Convert.ToDecimal(num_externalGeoCoordsX[i].Value), i);
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.externalGeoCoordY, Convert.ToDecimal(num_externalGeoCoordsY[i].Value), i);
            }
        }

        void clampNumeric(ref NumericStepper num, double min, double max)
        {
            num.Value = Math.Max(min, num.Value);
            num.Value = Math.Min(max, num.Value);
        }

        void clampSubShape(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                clampNumeric(ref num_layer_subshape_minhl, minHLength, maxHLength);
                clampNumeric(ref num_layer_subshape_minvl, minVLength, maxVLength);
                clampNumeric(ref num_layer_subshape_minho, minHOffset, maxHOffset);
                clampNumeric(ref num_layer_subshape_minvo, minVOffset, maxVOffset);
            });
        }

        void clampSubShape2(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {

                clampNumeric(ref num_layer_subshape2_minhl, minHLength, maxHLength);
                clampNumeric(ref num_layer_subshape2_minvl, minVLength, maxVLength);
                clampNumeric(ref num_layer_subshape2_minho, minHOffset, maxHOffset);
                clampNumeric(ref num_layer_subshape2_minvo, minVOffset, maxVOffset);
            });
        }

        void clampSubShape3(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                clampNumeric(ref num_layer_subshape3_minhl, minHLength, maxHLength);
                clampNumeric(ref num_layer_subshape3_minvl, minVLength, maxVLength);
                clampNumeric(ref num_layer_subshape3_minho, minHOffset, maxHOffset);
                clampNumeric(ref num_layer_subshape3_minvo, minVOffset, maxVOffset);
            });
        }
    }
}
