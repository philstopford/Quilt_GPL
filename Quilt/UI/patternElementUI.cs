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

            num_arrayXCount.LostFocus += doPatternElementUI;
            num_arrayXSpace.LostFocus += doPatternElementUI;
            num_arrayYCount.LostFocus += doPatternElementUI;
            num_arrayYSpace.LostFocus += doPatternElementUI;

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

        void updatePatternElementUI_position(int index)
        {
            // Set the X and Y position references.
            comboBox_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.subShapeIndex);
            comboBox_posSubShape.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.posIndex);

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

            num_arrayXCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSpace.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSpace.Enabled = bounding ? false : !isRelativeArray;

            // Register the relative array status with the pattern element.
            commonVars.stitcher.getPatternElement(patternIndex: 0, index).setInt(PatternElement.properties_i.relativeArray, isRelativeArray ? 1 : 0);

            num_minArrayRot.Enabled = isArray || isRelativeArray;
            num_incArrayRot.Enabled = isArray || isRelativeArray;
            num_stepsArrayRot.Enabled = isArray || isRelativeArray;
            comboBox_arrayRotRef.Enabled = isArray || isRelativeArray;
            checkBox_refArrayPivot.Enabled = isArray || isRelativeArray;

            num_arrayXCount.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayXCount);
            num_arrayYCount.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayYCount);
            num_arrayXSpace.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayXSpace));
            num_arrayYSpace.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayYSpace));
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

        void subShapesTableLayout(TableCell right_tr1_0)
        {
            Application.Instance.Invoke(() =>
            {
                // groupBox_subShapes.Size = new Size(310, 150);
                groupBox_subShapes_table = new TableLayout();
                groupBox_properties.Content = groupBox_subShapes_table;
                groupBox_properties.Text = "SubShapes";
                right_tr1_0.Control = groupBox_properties;

                int numWidth = 55;

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row0 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row0 });

                Scrollable s = new Scrollable();

                TableLayout row0_tl = new TableLayout();
                row0_tl.Rows.Add(new TableRow());
                s.Content = row0_tl;

                row0.Content = s;

                lbl_layer_subshape_hl = new Label();
                lbl_layer_subshape_hl.Text = "Min Hor. Length";
                row0_tl.Rows[row0_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_hl, ScaleWidth = true });

                num_layer_subshape_minhl = new NumericStepper();
                num_layer_subshape_minhl.Increment = 0.1;
                num_layer_subshape_minhl.DecimalPlaces = 2;
                num_layer_subshape_minhl.MinValue = 0;
                setSize(num_layer_subshape_minhl, numWidth, num_Height);
                row0_tl.Rows[row0_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minhl });

                num_layer_subshape2_minhl = new NumericStepper();
                num_layer_subshape2_minhl.Increment = 0.1;
                num_layer_subshape2_minhl.DecimalPlaces = 2;
                num_layer_subshape2_minhl.MinValue = 0;
                setSize(num_layer_subshape2_minhl, numWidth, num_Height);
                row0_tl.Rows[row0_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minhl });

                num_layer_subshape3_minhl = new NumericStepper();
                num_layer_subshape3_minhl.Increment = 0.1;
                num_layer_subshape3_minhl.DecimalPlaces = 2;
                num_layer_subshape3_minhl.MinValue = 0;
                setSize(num_layer_subshape3_minhl, numWidth, num_Height);
                row0_tl.Rows[row0_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minhl });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row1 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row1 });

                TableLayout row1_tl = new TableLayout();
                row1_tl.Rows.Add(new TableRow());
                row1.Content = row1_tl;

                lbl_layer_subshape_incHL = new Label();
                lbl_layer_subshape_incHL.Text = "Hor. Length Increment";
                row1_tl.Rows[row1_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incHL, ScaleWidth = true });

                num_layer_subshape_incHL = new NumericStepper();
                num_layer_subshape_incHL.Increment = 0.1;
                num_layer_subshape_incHL.DecimalPlaces = 2;
                setSize(num_layer_subshape_incHL, numWidth, num_Height);
                row1_tl.Rows[row1_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incHL });

                num_layer_subshape2_incHL = new NumericStepper();
                num_layer_subshape2_incHL.Increment = 0.1;
                num_layer_subshape2_incHL.DecimalPlaces = 2;
                setSize(num_layer_subshape2_incHL, numWidth, num_Height);
                row1_tl.Rows[row1_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incHL });

                num_layer_subshape3_incHL = new NumericStepper();
                num_layer_subshape3_incHL.Increment = 0.1;
                num_layer_subshape3_incHL.DecimalPlaces = 2;
                setSize(num_layer_subshape3_incHL, numWidth, num_Height);
                row1_tl.Rows[row1_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incHL });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row2 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row2 });

                TableLayout row2_tl = new TableLayout();
                row2_tl.Rows.Add(new TableRow());
                row2.Content = row2_tl;

                lbl_layer_subshape_stepsHL = new Label();
                lbl_layer_subshape_stepsHL.Text = "Hor. Length Steps";
                row2_tl.Rows[row2_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsHL, ScaleWidth = true });

                num_layer_subshape_stepsHL = new NumericStepper();
                num_layer_subshape_stepsHL.MinValue = 1;
                num_layer_subshape_stepsHL.Increment = 1;
                num_layer_subshape_stepsHL.DecimalPlaces = 0;
                setSize(num_layer_subshape_stepsHL, numWidth, num_Height);
                row2_tl.Rows[row2_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsHL });

                num_layer_subshape2_stepsHL = new NumericStepper();
                num_layer_subshape2_stepsHL.MinValue = 1;
                num_layer_subshape2_stepsHL.Increment = 1;
                num_layer_subshape2_stepsHL.DecimalPlaces = 0;
                setSize(num_layer_subshape2_stepsHL, numWidth, num_Height);
                row2_tl.Rows[row2_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsHL });

                num_layer_subshape3_stepsHL = new NumericStepper();
                num_layer_subshape3_stepsHL.MinValue = 1;
                num_layer_subshape3_stepsHL.Increment = 1;
                num_layer_subshape3_stepsHL.DecimalPlaces = 0;
                setSize(num_layer_subshape3_stepsHL, numWidth, num_Height);
                row2_tl.Rows[row2_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsHL });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row3 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row3 });

                TableLayout row3_tl = new TableLayout();
                row3_tl.Rows.Add(new TableRow());
                row3.Content = row3_tl;

                lbl_layer_subshape_ho = new Label();
                lbl_layer_subshape_ho.Text = "Min Hor. Offset";
                row3_tl.Rows[row3_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_ho, ScaleWidth = true });

                num_layer_subshape_minho = new NumericStepper();
                num_layer_subshape_minho.Increment = 0.1;
                num_layer_subshape_minho.DecimalPlaces = 2;
                setSize(num_layer_subshape_minho, numWidth, num_Height);
                row3_tl.Rows[row3_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minho });

                num_layer_subshape2_minho = new NumericStepper();
                num_layer_subshape2_minho.Increment = 0.1;
                num_layer_subshape2_minho.DecimalPlaces = 2;
                setSize(num_layer_subshape2_minho, numWidth, num_Height);
                row3_tl.Rows[row3_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minho });

                num_layer_subshape3_minho = new NumericStepper();
                num_layer_subshape3_minho.Increment = 0.1;
                num_layer_subshape3_minho.DecimalPlaces = 2;
                setSize(num_layer_subshape3_minho, numWidth, num_Height);
                row3_tl.Rows[row3_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minho });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row4 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row4 });

                TableLayout row4_tl = new TableLayout();
                row4_tl.Rows.Add(new TableRow());
                row4.Content = row4_tl;

                lbl_layer_subshape_incHO = new Label();
                lbl_layer_subshape_incHO.Text = "Hor. Offset Increment";
                row4_tl.Rows[row4_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incHO, ScaleWidth = true });

                num_layer_subshape_incHO = new NumericStepper();
                num_layer_subshape_incHO.Increment = 0.1;
                num_layer_subshape_incHO.DecimalPlaces = 2;
                setSize(num_layer_subshape_incHO, numWidth, num_Height);
                row4_tl.Rows[row4_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incHO });

                num_layer_subshape2_incHO = new NumericStepper();
                num_layer_subshape2_incHO.Increment = 0.1;
                num_layer_subshape2_incHO.DecimalPlaces = 2;
                setSize(num_layer_subshape2_incHO, numWidth, num_Height);
                row4_tl.Rows[row4_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incHO });

                num_layer_subshape3_incHO = new NumericStepper();
                num_layer_subshape3_incHO.Increment = 0.1;
                num_layer_subshape3_incHO.DecimalPlaces = 2;
                setSize(num_layer_subshape3_incHO, numWidth, num_Height);
                row4_tl.Rows[row4_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incHO });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row5 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row5 });

                TableLayout row5_tl = new TableLayout();
                row5_tl.Rows.Add(new TableRow());
                row5.Content = row5_tl;

                lbl_layer_subshape_stepsHO = new Label();
                lbl_layer_subshape_stepsHO.Text = "Hor. Offset Steps";
                row5_tl.Rows[row5_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsHO, ScaleWidth = true });

                num_layer_subshape_stepsHO = new NumericStepper();
                num_layer_subshape_stepsHO.MinValue = 1;
                num_layer_subshape_stepsHO.Increment = 1;
                num_layer_subshape_stepsHO.DecimalPlaces = 0;
                setSize(num_layer_subshape_stepsHO, numWidth, num_Height);
                row5_tl.Rows[row5_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsHO });

                num_layer_subshape2_stepsHO = new NumericStepper();
                num_layer_subshape2_stepsHO.MinValue = 1;
                num_layer_subshape2_stepsHO.Increment = 1;
                num_layer_subshape2_stepsHO.DecimalPlaces = 0;
                setSize(num_layer_subshape2_stepsHO, numWidth, num_Height);
                row5_tl.Rows[row5_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsHO });

                num_layer_subshape3_stepsHO = new NumericStepper();
                num_layer_subshape3_stepsHO.MinValue = 1;
                num_layer_subshape3_stepsHO.Increment = 1;
                num_layer_subshape3_stepsHO.DecimalPlaces = 0;
                setSize(num_layer_subshape3_stepsHO, numWidth, num_Height);
                row5_tl.Rows[row5_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsHO });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row6 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row6 });

                TableLayout row6_tl = new TableLayout();
                row6_tl.Rows.Add(new TableRow());
                row6.Content = row6_tl;

                lbl_layer_subshape_vl = new Label();
                lbl_layer_subshape_vl.Text = "Min Ver. Length";
                row6_tl.Rows[row6_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_vl, ScaleWidth = true });

                num_layer_subshape_minvl = new NumericStepper();
                num_layer_subshape_minvl.Increment = 0.1;
                num_layer_subshape_minvl.DecimalPlaces = 2;
                num_layer_subshape_minvl.MinValue = 0;
                setSize(num_layer_subshape_minvl, numWidth, num_Height);
                row6_tl.Rows[row6_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minvl });

                num_layer_subshape2_minvl = new NumericStepper();
                num_layer_subshape2_minvl.Increment = 0.1;
                num_layer_subshape2_minvl.DecimalPlaces = 2;
                num_layer_subshape2_minvl.MinValue = 0;
                setSize(num_layer_subshape2_minvl, numWidth, num_Height);
                row6_tl.Rows[row6_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minvl });

                num_layer_subshape3_minvl = new NumericStepper();
                num_layer_subshape3_minvl.Increment = 0.1;
                num_layer_subshape3_minvl.DecimalPlaces = 2;
                num_layer_subshape3_minvl.MinValue = 0;
                setSize(num_layer_subshape3_minvl, numWidth, num_Height);
                row6_tl.Rows[row6_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minvl });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row7 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row7 });

                TableLayout row7_tl = new TableLayout();
                row7_tl.Rows.Add(new TableRow());
                row7.Content = row7_tl;

                lbl_layer_subshape_incVL = new Label();
                lbl_layer_subshape_incVL.Text = "Ver. Length Increment";
                row7_tl.Rows[row7_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incVL, ScaleWidth = true });

                num_layer_subshape_incVL = new NumericStepper();
                num_layer_subshape_incVL.Increment = 0.1;
                num_layer_subshape_incVL.DecimalPlaces = 2;
                setSize(num_layer_subshape_incVL, numWidth, num_Height);
                row7_tl.Rows[row7_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incVL });

                num_layer_subshape2_incVL = new NumericStepper();
                num_layer_subshape2_incVL.Increment = 0.1;
                num_layer_subshape2_incVL.DecimalPlaces = 2;
                setSize(num_layer_subshape2_incVL, numWidth, num_Height);
                row7_tl.Rows[row7_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incVL });

                num_layer_subshape3_incVL = new NumericStepper();
                num_layer_subshape3_incVL.Increment = 0.1;
                num_layer_subshape3_incVL.DecimalPlaces = 2;
                setSize(num_layer_subshape3_incVL, numWidth, num_Height);
                row7_tl.Rows[row7_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incVL });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row8 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row8 });

                TableLayout row8_tl = new TableLayout();
                row8_tl.Rows.Add(new TableRow());
                row8.Content = row8_tl;

                lbl_layer_subshape_stepsVL = new Label();
                lbl_layer_subshape_stepsVL.Text = "Ver. Length Steps";
                row8_tl.Rows[row8_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsVL, ScaleWidth = true });

                num_layer_subshape_stepsVL = new NumericStepper();
                num_layer_subshape_stepsVL.MinValue = 1;
                num_layer_subshape_stepsVL.Increment = 1;
                num_layer_subshape_stepsVL.DecimalPlaces = 0;
                setSize(num_layer_subshape_stepsVL, numWidth, num_Height);
                row8_tl.Rows[row8_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsVL });

                num_layer_subshape2_stepsVL = new NumericStepper();
                num_layer_subshape2_stepsVL.MinValue = 1;
                num_layer_subshape2_stepsVL.Increment = 1;
                num_layer_subshape2_stepsVL.DecimalPlaces = 0;
                setSize(num_layer_subshape2_stepsVL, numWidth, num_Height);
                row8_tl.Rows[row8_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsVL });

                num_layer_subshape3_stepsVL = new NumericStepper();
                num_layer_subshape3_stepsVL.MinValue = 1;
                num_layer_subshape3_stepsVL.Increment = 1;
                num_layer_subshape3_stepsVL.DecimalPlaces = 0;
                setSize(num_layer_subshape3_stepsVL, numWidth, num_Height);
                row8_tl.Rows[row8_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsVL });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row9 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row9 });

                TableLayout row9_tl = new TableLayout();
                row9_tl.Rows.Add(new TableRow());
                row9.Content = row9_tl;

                lbl_layer_subshape_vo = new Label();
                lbl_layer_subshape_vo.Text = "Min Ver. Offset";
                row9_tl.Rows[row9_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_vo, ScaleWidth = true });

                num_layer_subshape_minvo = new NumericStepper();
                num_layer_subshape_minvo.Increment = 0.1;
                num_layer_subshape_minvo.DecimalPlaces = 2;
                setSize(num_layer_subshape_minvo, numWidth, num_Height);
                row9_tl.Rows[row9_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_minvo });

                num_layer_subshape2_minvo = new NumericStepper();
                num_layer_subshape2_minvo.Increment = 0.1;
                num_layer_subshape2_minvo.DecimalPlaces = 2;
                setSize(num_layer_subshape2_minvo, numWidth, num_Height);
                row9_tl.Rows[row9_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_minvo });

                num_layer_subshape3_minvo = new NumericStepper();
                num_layer_subshape3_minvo.Increment = 0.1;
                num_layer_subshape3_minvo.DecimalPlaces = 2;
                setSize(num_layer_subshape3_minvo, numWidth, num_Height);
                row9_tl.Rows[row9_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_minvo });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row10 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row10 });

                TableLayout row10_tl = new TableLayout();
                row10_tl.Rows.Add(new TableRow());
                row10.Content = row10_tl;

                lbl_layer_subshape_incVO = new Label();
                lbl_layer_subshape_incVO.Text = "Ver. Offset Increment";
                row10_tl.Rows[row10_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_incVO, ScaleWidth = true });

                num_layer_subshape_incVO = new NumericStepper();
                num_layer_subshape_incVO.Increment = 0.1;
                num_layer_subshape_incVO.DecimalPlaces = 2;
                setSize(num_layer_subshape_incVO, numWidth, num_Height);
                row10_tl.Rows[row10_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_incVO });

                num_layer_subshape2_incVO = new NumericStepper();
                num_layer_subshape2_incVO.Increment = 0.1;
                num_layer_subshape2_incVO.DecimalPlaces = 2;
                setSize(num_layer_subshape2_incVO, numWidth, num_Height);
                row10_tl.Rows[row10_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_incVO });

                num_layer_subshape3_incVO = new NumericStepper();
                num_layer_subshape3_incVO.Increment = 0.1;
                num_layer_subshape3_incVO.DecimalPlaces = 2;
                setSize(num_layer_subshape3_incVO, numWidth, num_Height);
                row10_tl.Rows[row10_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_incVO });

                groupBox_subShapes_table.Rows.Add(new TableRow());

                Panel row11 = new Panel();
                groupBox_subShapes_table.Rows[groupBox_subShapes_table.Rows.Count - 1].Cells.Add(new TableCell() { Control = row11 });

                TableLayout row11_tl = new TableLayout();
                row11_tl.Rows.Add(new TableRow());
                row11.Content = row11_tl;

                lbl_layer_subshape_stepsVO = new Label();
                lbl_layer_subshape_stepsVO.Text = "Ver. Offset Steps";
                row11_tl.Rows[row11_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_layer_subshape_stepsVO, ScaleWidth = true });

                num_layer_subshape_stepsVO = new NumericStepper();
                num_layer_subshape_stepsVO.MinValue = 1;
                num_layer_subshape_stepsVO.Increment = 1;
                num_layer_subshape_stepsVO.DecimalPlaces = 0;
                setSize(num_layer_subshape_stepsVO, numWidth, num_Height);
                row11_tl.Rows[row11_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape_stepsVO });

                num_layer_subshape2_stepsVO = new NumericStepper();
                num_layer_subshape2_stepsVO.MinValue = 1;
                num_layer_subshape2_stepsVO.Increment = 1;
                num_layer_subshape2_stepsVO.DecimalPlaces = 0;
                setSize(num_layer_subshape2_stepsVO, numWidth, num_Height);
                row11_tl.Rows[row11_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape2_stepsVO });

                num_layer_subshape3_stepsVO = new NumericStepper();
                num_layer_subshape3_stepsVO.MinValue = 1;
                num_layer_subshape3_stepsVO.Increment = 1;
                num_layer_subshape3_stepsVO.DecimalPlaces = 0;
                setSize(num_layer_subshape3_stepsVO, numWidth, num_Height);
                row11_tl.Rows[row11_tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = num_layer_subshape3_stepsVO });

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
            });
        }

        void sp3_subShapeRef(TableLayout groupBox_position_table)
        {
            TableRow tr0 = new TableRow();
            groupBox_position_table.Rows.Add(tr0);

            lbl_subShapeRef = new Label();
            lbl_subShapeRef.Text = "Subshape Reference";
            lbl_subShapeRef.ToolTip = "Which subshape to use for placement with respect to the world origin";

            TableCell tr0_0 = new TableCell();
            tr0_0.Control = lbl_subShapeRef;
            tr0.Cells.Add(tr0_0);

            comboBox_subShapeRef = new DropDown();
            comboBox_subShapeRef.DataContext = DataContext;
            comboBox_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeList);
            comboBox_subShapeRef.SelectedIndex = 0;
            comboBox_subShapeRef.ToolTip = "Which subshape to use for placement with respect to the world origin";

            TableCell tr0_1 = new TableCell();
            Panel tr0_1p = new Panel();
            PixelLayout tr0_1pl = new PixelLayout();
            tr0_1p.Content = tr0_1pl;
            tr0_1.Control = tr0_1p;
            tr0_1pl.Add(comboBox_subShapeRef, 0, 0);
            tr0.Cells.Add(tr0_1);
        }

        void sp3_subShapePos(TableLayout groupBox_position_table)
        {
            TableRow tr1 = new TableRow();
            groupBox_position_table.Rows.Add(tr1);

            lbl_posSubShape = new Label();
            lbl_posSubShape.Text = "Subshape Position";
            lbl_posSubShape.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            TableCell tr1_0 = new TableCell();
            Panel tr1_0p = new Panel();
            PixelLayout tr1_0pl = new PixelLayout();
            tr1_0p.Content = tr1_0pl;
            tr1_0.Control = tr1_0p;
            tr1_0pl.Add(lbl_posSubShape, 0, 0);
            tr1.Cells.Add(tr1_0);


            comboBox_posSubShape = new DropDown();
            comboBox_posSubShape.DataContext = DataContext;
            comboBox_posSubShape.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapePos);
            comboBox_posSubShape.SelectedIndex = 0;
            comboBox_posSubShape.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            TableCell tr1_1 = new TableCell();
            Panel tr1_1p = new Panel();
            PixelLayout tr1_1pl = new PixelLayout();
            tr1_1p.Content = tr1_1pl;
            tr1_1.Control = tr1_1p;
            tr1_1pl.Add(comboBox_posSubShape, 0, 0);
            tr1.Cells.Add(tr1_1);
        }

        void sp3_subShapeXPos(TableLayout groupBox_position_table)
        {
            TableRow tr2 = new TableRow();
            groupBox_position_table.Rows.Add(tr2);

            TableCell tr2_0 = new TableCell();
            Panel tr2_0p = new Panel();
            tr2_0.Control = tr2_0p;

            TableLayout tr2_0tl = new TableLayout();
            tr2_0p.Content = tr2_0tl;
            tr2.Cells.Add(tr2_0);

            tr2_0tl.Rows.Add(new TableRow());

            lbl_xPosRef = new Label();
            lbl_xPosRef.Text = "X Pos Ref";
            lbl_xPosRef.ToolTip = "Position this element in X relative to a different element, or world origin";
            tr2_0tl.Rows[tr2_0tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_xPosRef });

            TableCell tr2_1 = new TableCell();
            Panel tr2_1p = new Panel();
            TableLayout tr2_container = new TableLayout();
            tr2_1p.Content = tr2_container;
            tr2_1.Control = tr2_1p;
            TableRow tr2_c_0 = new TableRow();
            tr2_container.Rows.Add(tr2_c_0);
            tr2.Cells.Add(tr2_1);

            num_minXPos = new NumericStepper();
            num_minXPos.Increment = 0.1;
            num_minXPos.DecimalPlaces = 2;
            setSize(num_minXPos, numWidth, num_Height);

            TableCell tr2_1_0 = new TableCell();
            Panel tr2_1_0p = new Panel();
            PixelLayout tr2_1_0pl = new PixelLayout();
            tr2_1_0p.Content = tr2_1_0pl;
            tr2_1_0.Control = tr2_1_0p;
            tr2_1_0pl.Add(num_minXPos, 0, 0);
            tr2_c_0.Cells.Add(tr2_1_0);


            num_incXPos = new NumericStepper();
            num_incXPos.Increment = 0.1;
            num_incXPos.DecimalPlaces = 2;
            setSize(num_incXPos, numWidth, num_Height);

            TableCell tr2_1_1 = new TableCell();
            Panel tr2_1_1p = new Panel();
            PixelLayout tr2_1_1pl = new PixelLayout();
            tr2_1_1p.Content = tr2_1_1pl;
            tr2_1_1.Control = tr2_1_1p;
            tr2_1_1pl.Add(num_incXPos, 0, 0);
            tr2_c_0.Cells.Add(tr2_1_1);


            num_stepsXPos = new NumericStepper();
            num_stepsXPos.Increment = 1;
            num_stepsXPos.DecimalPlaces = 0;
            num_stepsXPos.MinValue = 1;
            setSize(num_stepsXPos, numWidth, num_Height);

            TableCell tr2_1_2 = new TableCell();
            Panel tr2_1_2p = new Panel();
            PixelLayout tr2_1_2pl = new PixelLayout();
            tr2_1_2p.Content = tr2_1_2pl;
            tr2_1_2.Control = tr2_1_2p;
            tr2_1_2pl.Add(num_stepsXPos, 0, 0);
            tr2_c_0.Cells.Add(tr2_1_2);
        }

        void sp3_subShapeXRelPos(TableLayout groupBox_position_table)
        {
            TableRow tr3 = new TableRow();
            groupBox_position_table.Rows.Add(tr3);

            Label lbl_relXPos = new Label();
            lbl_relXPos.Text = "Relative Position";
            lbl_relXPos.ToolTip = "Relative Positioning";

            TableCell tr3_0 = new TableCell();
            Panel tr3_0p = new Panel();
            PixelLayout tr3_0pl = new PixelLayout();
            tr3_0p.Content = tr3_0pl;
            tr3_0.Control = tr3_0p;
            tr3_0pl.Add(lbl_relXPos, 0, 0);
            tr3.Cells.Add(tr3_0);

            comboBox_xPosRef = new DropDown();
            comboBox_xPosRef.DataContext = DataContext;
            comboBox_xPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_xPosRef.ToolTip = "Position in X relative to this pattern element";

            TableCell tr3_1 = new TableCell();
            Panel tr3_1p = new Panel();
            PixelLayout tr3_1pl = new PixelLayout();
            tr3_1p.Content = tr3_1pl;
            tr3_1.Control = tr3_1p;
            tr3_1pl.Add(comboBox_xPosRef, 0, 0);
            tr3.Cells.Add(tr3_1);
        }

        void sp3_subShapeXRelPosSS(TableLayout groupBox_position_table)
        {
            TableRow tr4 = new TableRow();
            groupBox_position_table.Rows.Add(tr4);

            TableCell tr4_0 = new TableCell();
            Panel tr4_0p = new Panel();
            tr4_0.Control = tr4_0p;

            TableLayout tr4_0tl = new TableLayout();
            tr4_0p.Content = tr4_0tl;
            tr4.Cells.Add(tr4_0);

            tr4_0tl.Rows.Add(new TableRow());

            Label lbl_subShapeXPos = new Label();
            lbl_subShapeXPos.Text = "Subshape Ref";
            lbl_subShapeXPos.ToolTip = "Reference subshape";
            tr4_0tl.Rows[tr4_0tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_subShapeXPos });

            TableCell tr4_1 = new TableCell();
            Panel tr4_1p = new Panel();
            TableLayout tr4_container = new TableLayout();
            tr4_1p.Content = tr4_container;
            tr4_1.Control = tr4_1p;
            TableRow tr4_c_0 = new TableRow();
            tr4_container.Rows.Add(tr4_c_0);
            tr4.Cells.Add(tr4_1);

            comboBox_xPos_subShapeRef = new DropDown();
            comboBox_xPos_subShapeRef.DataContext = DataContext;
            comboBox_xPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.xPosRefSubShapeList);
            comboBox_xPos_subShapeRef.SelectedIndex = 0;
            comboBox_xPos_subShapeRef.ToolTip = "Subshape reference";

            TableCell tr4_1_0 = new TableCell();
            Panel tr4_1_0p = new Panel();
            PixelLayout tr4_1_0pl = new PixelLayout();
            tr4_1_0p.Content = tr4_1_0pl;
            tr4_1_0.Control = tr4_1_0p;
            tr4_1_0pl.Add(comboBox_xPos_subShapeRef, 0, 0);
            tr4_c_0.Cells.Add(tr4_1_0);


            comboBox_xPos_subShapeRefPos = new DropDown();
            comboBox_xPos_subShapeRefPos.DataContext = DataContext;
            comboBox_xPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeHorPos);
            comboBox_xPos_subShapeRefPos.SelectedIndex = (int)CommonVars.subShapeHorLocs.L;
            comboBox_xPos_subShapeRefPos.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            TableCell tr4_1_1 = new TableCell();
            Panel tr4_1_1p = new Panel();
            PixelLayout tr4_1_1pl = new PixelLayout();
            tr4_1_1p.Content = tr4_1_1pl;
            tr4_1_1.Control = tr4_1_1p;
            tr4_1_1pl.Add(comboBox_xPos_subShapeRefPos, 0, 0);
            tr4_c_0.Cells.Add(tr4_1_1);
        }

        void sp3_subShapeYPos(TableLayout groupBox_position_table)
        {
            TableRow tr5 = new TableRow();
            groupBox_position_table.Rows.Add(tr5);

            lbl_yPosRef = new Label();
            lbl_yPosRef.Text = "Y Pos Ref";
            lbl_yPosRef.ToolTip = "Position this element in Y relative to a different element, or world origin";

            TableCell tr5_0 = new TableCell();
            Panel tr5_0p = new Panel();
            PixelLayout tr5_0pl = new PixelLayout();
            tr5_0p.Content = tr5_0pl;
            tr5_0.Control = tr5_0p;
            tr5_0pl.Add(lbl_yPosRef, 0, 0);
            tr5.Cells.Add(tr5_0);


            TableCell tr5_1 = new TableCell();
            Panel tr5_1p = new Panel();
            TableLayout tr5_container = new TableLayout();
            tr5_1p.Content = tr5_container;
            tr5_1.Control = tr5_1p;
            TableRow tr5_c_0 = new TableRow();
            tr5_container.Rows.Add(tr5_c_0);
            tr5.Cells.Add(tr5_1);

            num_minYPos = new NumericStepper();
            num_minYPos.Increment = 0.1;
            num_minYPos.DecimalPlaces = 2;
            setSize(num_minYPos, numWidth, num_Height);

            TableCell tr5_1_0 = new TableCell();
            Panel tr5_1_0p = new Panel();
            PixelLayout tr5_1_0pl = new PixelLayout();
            tr5_1_0p.Content = tr5_1_0pl;
            tr5_1_0.Control = tr5_1_0p;
            tr5_1_0pl.Add(num_minYPos, 0, 0);
            tr5_c_0.Cells.Add(tr5_1_0);


            num_incYPos = new NumericStepper();
            num_incYPos.Increment = 0.1;
            num_incYPos.DecimalPlaces = 2;
            setSize(num_incYPos, numWidth, num_Height);

            TableCell tr5_1_1 = new TableCell();
            Panel tr5_1_1p = new Panel();
            PixelLayout tr5_1_1pl = new PixelLayout();
            tr5_1_1p.Content = tr5_1_1pl;
            tr5_1_1.Control = tr5_1_1p;
            tr5_1_1pl.Add(num_incYPos, 0, 0);
            tr5_c_0.Cells.Add(tr5_1_1);


            num_stepsYPos = new NumericStepper();
            num_stepsYPos.Increment = 1;
            num_stepsYPos.DecimalPlaces = 0;
            num_stepsYPos.MinValue = 1;
            setSize(num_stepsYPos, numWidth, num_Height);

            TableCell tr5_1_2 = new TableCell();
            Panel tr5_1_2p = new Panel();
            PixelLayout tr5_1_2pl = new PixelLayout();
            tr5_1_2p.Content = tr5_1_2pl;
            tr5_1_2.Control = tr5_1_2p;
            tr5_1_2pl.Add(num_stepsYPos, 0, 0);
            tr5_c_0.Cells.Add(tr5_1_2);
        }

        void sp3_subShapeYRelPos(TableLayout groupBox_position_table)
        {
            TableRow tr6 = new TableRow();
            groupBox_position_table.Rows.Add(tr6);

            Label lbl_relYPos = new Label();
            lbl_relYPos.Text = "Relative Position";
            lbl_relYPos.ToolTip = "Relative Positioning";

            TableCell tr6_0 = new TableCell();
            Panel tr6_0p = new Panel();
            PixelLayout tr6_0pl = new PixelLayout();
            tr6_0p.Content = tr6_0pl;
            tr6_0.Control = tr6_0p;
            tr6_0pl.Add(lbl_relYPos, 0, 0);
            tr6.Cells.Add(tr6_0);

            comboBox_yPosRef = new DropDown();
            comboBox_yPosRef.DataContext = DataContext;
            comboBox_yPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_yPosRef.SelectedIndex = 0;
            comboBox_yPosRef.ToolTip = "Position in Y relative to this pattern element";

            TableCell tr6_1 = new TableCell();
            Panel tr6_1p = new Panel();
            PixelLayout tr6_1pl = new PixelLayout();
            tr6_1p.Content = tr6_1pl;
            tr6_1.Control = tr6_1p;
            tr6_1pl.Add(comboBox_yPosRef, 0, 0);
            tr6.Cells.Add(tr6_1);
        }

        void sp3_subShapeYRelPosSS(TableLayout groupBox_position_table)
        {
            TableRow tr7 = new TableRow();
            groupBox_position_table.Rows.Add(tr7);

            TableCell tr7_0 = new TableCell();
            Panel tr7_0p = new Panel();
            tr7_0.Control = tr7_0p;

            TableLayout tr7_0tl = new TableLayout();
            tr7_0p.Content = tr7_0tl;
            tr7.Cells.Add(tr7_0);

            tr7_0tl.Rows.Add(new TableRow());

            Label lbl_subShapeYPos = new Label();
            lbl_subShapeYPos.Text = "Subshape Ref";
            lbl_subShapeYPos.ToolTip = "Reference subshape";
            tr7_0tl.Rows[tr7_0tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_subShapeYPos });

            TableCell tr7_1 = new TableCell();
            Panel tr7_1p = new Panel();
            TableLayout tr7_container = new TableLayout();
            tr7_1p.Content = tr7_container;
            tr7_1.Control = tr7_1p;
            TableRow tr7_c_0 = new TableRow();
            tr7_container.Rows.Add(tr7_c_0);
            tr7.Cells.Add(tr7_1);

            comboBox_yPos_subShapeRef = new DropDown();
            comboBox_yPos_subShapeRef.DataContext = DataContext;
            comboBox_yPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.yPosRefSubShapeList);
            comboBox_yPos_subShapeRef.SelectedIndex = 0;
            comboBox_yPos_subShapeRef.ToolTip = "Subshape reference";

            TableCell tr7_1_0 = new TableCell();
            Panel tr7_1_0p = new Panel();
            PixelLayout tr7_1_0pl = new PixelLayout();
            tr7_1_0p.Content = tr7_1_0pl;
            tr7_1_0.Control = tr7_1_0p;
            tr7_1_0pl.Add(comboBox_yPos_subShapeRef, 0, 0);
            tr7_c_0.Cells.Add(tr7_1_0);


            comboBox_yPos_subShapeRefPos = new DropDown();
            comboBox_yPos_subShapeRefPos.DataContext = DataContext;
            comboBox_yPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeVerPos);
            comboBox_yPos_subShapeRefPos.SelectedIndex = (int)CommonVars.subShapeVerLocs.B;
            comboBox_yPos_subShapeRefPos.ToolTip = "Which element of the subshape to use for placement with respect to the world origin";

            TableCell tr7_1_1 = new TableCell();
            Panel tr7_1_1p = new Panel();
            PixelLayout tr7_1_1pl = new PixelLayout();
            tr7_1_1p.Content = tr7_1_1pl;
            tr7_1_1.Control = tr7_1_1p;
            tr7_1_1pl.Add(comboBox_yPos_subShapeRefPos, 0, 0);
            tr7_c_0.Cells.Add(tr7_1_1);
        }

        void sp3_Rot(TableLayout groupBox_position_table)
        {
            TableRow tr8 = new TableRow();
            groupBox_position_table.Rows.Add(tr8);

            lbl_rotation = new Label();
            lbl_rotation.Text = "Rotation";
            lbl_rotation.ToolTip = "Rotation";

            TableCell tr8_0 = new TableCell();
            Panel tr8_0p = new Panel();
            PixelLayout tr8_0pl = new PixelLayout();
            tr8_0p.Content = tr8_0pl;
            tr8_0.Control = tr8_0p;
            tr8_0pl.Add(lbl_rotation, 0, 0);
            tr8.Cells.Add(tr8_0);


            TableCell tr8_1 = new TableCell();
            Panel tr8_1p = new Panel();
            TableLayout tr8_container = new TableLayout();
            tr8_1p.Content = tr8_container;
            tr8_1.Control = tr8_1p;
            TableRow tr8_c_0 = new TableRow();
            tr8_container.Rows.Add(tr8_c_0);
            tr8.Cells.Add(tr8_1);

            num_minRot = new NumericStepper();
            num_minRot.Increment = 0.1;
            num_minRot.DecimalPlaces = 2;
            setSize(num_minRot, numWidth, num_Height);

            TableCell tr8_1_0 = new TableCell();
            Panel tr8_1_0p = new Panel();
            PixelLayout tr8_1_0pl = new PixelLayout();
            tr8_1_0p.Content = tr8_1_0pl;
            tr8_1_0.Control = tr8_1_0p;
            tr8_1_0pl.Add(num_minRot, 0, 0);
            tr8_c_0.Cells.Add(tr8_1_0);


            num_incRot = new NumericStepper();
            num_incRot.Increment = 0.1;
            num_incRot.DecimalPlaces = 2;
            setSize(num_incRot, numWidth, num_Height);

            TableCell tr8_1_1 = new TableCell();
            Panel tr8_1_1p = new Panel();
            PixelLayout tr8_1_1pl = new PixelLayout();
            tr8_1_1p.Content = tr8_1_1pl;
            tr8_1_1.Control = tr8_1_1p;
            tr8_1_1pl.Add(num_incRot, 0, 0);
            tr8_c_0.Cells.Add(tr8_1_1);


            num_stepsRot = new NumericStepper();
            num_stepsRot.Increment = 1;
            num_stepsRot.DecimalPlaces = 0;
            num_stepsRot.MinValue = 1;
            setSize(num_stepsRot, numWidth, num_Height);

            TableCell tr8_1_2 = new TableCell();
            Panel tr8_1_2p = new Panel();
            PixelLayout tr8_1_2pl = new PixelLayout();
            tr8_1_2p.Content = tr8_1_2pl;
            tr8_1_2.Control = tr8_1_2p;
            tr8_1_2pl.Add(num_stepsRot, 0, 0);
            tr8_c_0.Cells.Add(tr8_1_2);
        }

        void sp3_RelRot(TableLayout groupBox_position_table)
        {
            TableRow tr9 = new TableRow();
            groupBox_position_table.Rows.Add(tr9);

            Label lbl_relRot = new Label();
            lbl_relRot.Text = "Relative Rotation";
            lbl_relRot.ToolTip = "Relative Rotation";

            TableCell tr9_0 = new TableCell();
            Panel tr9_0p = new Panel();
            PixelLayout tr9_0pl = new PixelLayout();
            tr9_0p.Content = tr9_0pl;
            tr9_0.Control = tr9_0p;
            tr9_0pl.Add(lbl_relRot, 0, 0);
            tr9.Cells.Add(tr9_0);

            comboBox_rotRef = new DropDown();
            comboBox_rotRef.DataContext = DataContext;
            comboBox_rotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_rotRef.SelectedIndex = 0;
            comboBox_rotRef.ToolTip = "Rotation relative to this pattern element";

            TableCell tr9_1 = new TableCell();
            Panel tr9_1p = new Panel();
            TableLayout tr9_1tl = new TableLayout();
            tr9_1tl.Rows.Add(new TableRow());
            tr9_1p.Content = tr9_1tl;
            tr9_1.Control = tr9_1p;
            tr9_1tl.Rows[tr9_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = comboBox_rotRef });
            tr9.Cells.Add(tr9_1);

            TableRow tr9b = new TableRow();
            tr9_1tl.Rows.Add(tr9b);

            TableLayout tr9b_1tl = new TableLayout();
            tr9b_1tl.Rows.Add(new TableRow());

            tr9b.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(tr9b_1tl) });

            lbl_use = new Label();
            lbl_use.Text = "Use";
            tr9b_1tl.Rows[tr9b_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_use });

            checkBox_refPivot = new CheckBox();
            checkBox_refPivot.Text = "Pivot";
            checkBox_refPivot.Enabled = false;
            checkBox_refPivot.ToolTip = "Use pivot point from reference.";

            tr9b_1tl.Rows[tr9b_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refPivot });

            checkBox_rotRef = new CheckBox();
            checkBox_rotRef.Text = "Array";
            checkBox_rotRef.Enabled = false;
            checkBox_rotRef.ToolTip = "Use array rotation rather than shape.";

            tr9b_1tl.Rows[tr9b_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_rotRef });

            tr9_1tl.Rows.Add(new TableRow());

            checkBox_refBoundsAfterRotation = new CheckBox();
            checkBox_refBoundsAfterRotation.Text = "Bounds after rotation";
            checkBox_refBoundsAfterRotation.Enabled = false;
            checkBox_refBoundsAfterRotation.ToolTip = "Perform rotation before bounding box. This affects the pivot.";

            tr9_1tl.Rows[tr9_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refBoundsAfterRotation });

            tr9_1tl.Rows[tr9_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });
        }

        void sp3_Flip(TableLayout groupBox_position_table)
        {
            TableRow tr11 = new TableRow();
            groupBox_position_table.Rows.Add(tr11);

            lbl_flip = new Label();
            lbl_flip.Text = "Flip";
            lbl_flip.ToolTip = "Flip";

            TableCell tr11_0 = new TableCell();
            Panel tr11_0p = new Panel();
            PixelLayout tr11_0pl = new PixelLayout();
            tr11_0p.Content = tr11_0pl;
            tr11_0.Control = tr11_0p;
            tr11_0pl.Add(lbl_flip, 0, 0);
            tr11.Cells.Add(tr11_0);


            TableCell tr11_1 = new TableCell();
            Panel tr11_1p = new Panel();
            TableLayout tr11_container = new TableLayout();
            tr11_1p.Content = tr11_container;
            tr11_1.Control = tr11_1p;
            TableRow tr11_c_0 = new TableRow();
            tr11_container.Rows.Add(tr11_c_0);
            tr11.Cells.Add(tr11_1);

            checkBox_flipH = new CheckBox();
            checkBox_flipH.Text = "H";
            checkBox_flipH.ToolTip = "Flip horizontally";

            TableCell tr11_1_0 = new TableCell();
            Panel tr11_1_0p = new Panel();
            PixelLayout tr11_1_0pl = new PixelLayout();
            tr11_1_0p.Content = tr11_1_0pl;
            tr11_1_0.Control = tr11_1_0p;
            tr11_1_0pl.Add(checkBox_flipH, 0, 0);
            tr11_c_0.Cells.Add(tr11_1_0);


            checkBox_flipV = new CheckBox();
            checkBox_flipV.Text = "V";
            checkBox_flipV.ToolTip = "Flip vertically";

            TableCell tr11_1_1 = new TableCell();
            Panel tr11_1_1p = new Panel();
            PixelLayout tr11_1_1pl = new PixelLayout();
            tr11_1_1p.Content = tr11_1_1pl;
            tr11_1_1.Control = tr11_1_1p;
            tr11_1_1pl.Add(checkBox_flipV, 0, 0);
            tr11_c_0.Cells.Add(tr11_1_1);


            checkBox_alignX = new CheckBox();
            checkBox_alignX.Text = "Align X";
            checkBox_alignX.ToolTip = "Align flipped shape with original in X";

            TableCell tr11_1_2 = new TableCell();
            Panel tr11_1_2p = new Panel();
            PixelLayout tr11_1_2pl = new PixelLayout();
            tr11_1_2p.Content = tr11_1_2pl;
            tr11_1_2.Control = tr11_1_2p;
            tr11_1_2pl.Add(checkBox_alignX, 0, 0);
            tr11_c_0.Cells.Add(tr11_1_2);

            checkBox_alignY = new CheckBox();
            checkBox_alignY.Text = "Align Y";
            checkBox_alignY.ToolTip = "Align flipped shape with original in Y";

            TableCell tr11_1_3 = new TableCell();
            Panel tr11_1_3p = new Panel();
            PixelLayout tr11_1_3pl = new PixelLayout();
            tr11_1_3p.Content = tr11_1_3pl;
            tr11_1_3.Control = tr11_1_3p;
            tr11_1_3pl.Add(checkBox_alignY, 0, 0);
            tr11_c_0.Cells.Add(tr11_1_3);
        }

        void sp3_Array(TableLayout groupBox_position_table)
        {
            TableRow tr10 = new TableRow();
            groupBox_position_table.Rows.Add(tr10);

            Label lbl_relArray = new Label();
            lbl_relArray.Text = "Relative Array Definition";
            lbl_relArray.ToolTip = "Relative Array Definition";

            TableCell tr10_0 = new TableCell();
            Panel tr10_0p = new Panel();
            PixelLayout tr10_0pl = new PixelLayout();
            tr10_0p.Content = tr10_0pl;
            tr10_0.Control = tr10_0p;
            tr10_0pl.Add(lbl_relArray, 0, 0);
            tr10.Cells.Add(tr10_0);

            comboBox_arrayRef = new DropDown();
            comboBox_arrayRef.DataContext = DataContext;
            comboBox_arrayRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered_array);
            comboBox_arrayRef.SelectedIndex = 0;
            comboBox_arrayRef.ToolTip = "Take array definition from this element";

            TableCell tr10_1 = new TableCell();
            Panel tr10_1p = new Panel();
            PixelLayout tr10_1pl = new PixelLayout();
            tr10_1p.Content = tr10_1pl;
            tr10_1.Control = tr10_1p;
            tr10_1pl.Add(comboBox_arrayRef, 0, 0);
            tr10.Cells.Add(tr10_1);

            sp3_ArrayX(groupBox_position_table);
            sp3_ArrayY(groupBox_position_table);
            sp3_ArrayRot(groupBox_position_table);
        }

        void sp3_ArrayX(TableLayout groupBox_position_table)
        {
            TableRow tr12 = new TableRow();
            groupBox_position_table.Rows.Add(tr12);

            lbl_arrayX = new Label();
            lbl_arrayX.Text = "Array X";
            lbl_arrayX.ToolTip = "Array X";

            TableCell tr12_0 = new TableCell();
            Panel tr12_0p = new Panel();
            PixelLayout tr12_0pl = new PixelLayout();
            tr12_0p.Content = tr12_0pl;
            tr12_0.Control = tr12_0p;
            tr12_0pl.Add(lbl_arrayX, 0, 0);
            tr12.Cells.Add(tr12_0);


            TableCell tr12_1 = new TableCell();
            Panel tr12_1p = new Panel();
            TableLayout tr12_container = new TableLayout();
            tr12_1p.Content = tr12_container;
            tr12_1.Control = tr12_1p;
            TableRow tr12_c_0 = new TableRow();
            tr12_container.Rows.Add(tr12_c_0);
            tr12.Cells.Add(tr12_1);

            num_arrayXCount = new NumericStepper();
            num_arrayXCount.Value = 1;
            num_arrayXCount.MinValue = 1;
            num_arrayXCount.Increment = 1;
            num_arrayXCount.DecimalPlaces = 0;
            num_arrayXCount.ToolTip = "Array X Count";
            setSize(num_arrayXCount, numWidth, num_Height);

            TableCell tr12_1_0 = new TableCell();
            Panel tr12_1_0p = new Panel();
            PixelLayout tr12_1_0pl = new PixelLayout();
            tr12_1_0p.Content = tr12_1_0pl;
            tr12_1_0.Control = tr12_1_0p;
            tr12_1_0pl.Add(num_arrayXCount, 0, 0);
            tr12_c_0.Cells.Add(tr12_1_0);


            num_arrayXSpace = new NumericStepper();
            num_arrayXSpace.Value = 0.00;
            num_arrayXSpace.Increment = 0.1;
            num_arrayXSpace.DecimalPlaces = 2;
            num_arrayXSpace.ToolTip = "Array X Space";
            setSize(num_arrayXSpace, numWidth, num_Height);

            TableCell tr12_1_1 = new TableCell();
            Panel tr12_1_1p = new Panel();
            PixelLayout tr12_1_1pl = new PixelLayout();
            tr12_1_1p.Content = tr12_1_1pl;
            tr12_1_1.Control = tr12_1_1p;
            tr12_1_1pl.Add(num_arrayXSpace, 0, 0);
            tr12_c_0.Cells.Add(tr12_1_1);
        }

        void sp3_ArrayY(TableLayout groupBox_position_table)
        {
            TableRow tr13 = new TableRow();
            groupBox_position_table.Rows.Add(tr13);

            lbl_arrayY = new Label();
            lbl_arrayY.Text = "Array Y";
            lbl_arrayY.ToolTip = "Array Y";

            TableCell tr13_0 = new TableCell();
            Panel tr13_0p = new Panel();
            PixelLayout tr13_0pl = new PixelLayout();
            tr13_0p.Content = tr13_0pl;
            tr13_0.Control = tr13_0p;
            tr13_0pl.Add(lbl_arrayY, 0, 0);
            tr13.Cells.Add(tr13_0);


            TableCell tr13_1 = new TableCell();
            Panel tr13_1p = new Panel();
            TableLayout tr13_container = new TableLayout();
            tr13_1p.Content = tr13_container;
            tr13_1.Control = tr13_1p;
            TableRow tr13_c_0 = new TableRow();
            tr13_container.Rows.Add(tr13_c_0);
            tr13.Cells.Add(tr13_1);

            num_arrayYCount = new NumericStepper();
            num_arrayYCount.Value = 1;
            num_arrayYCount.MinValue = 1;
            num_arrayYCount.Increment = 1;
            num_arrayYCount.DecimalPlaces = 0;
            num_arrayYCount.ToolTip = "Array Y Count";
            setSize(num_arrayYCount, numWidth, num_Height);

            TableCell tr13_1_0 = new TableCell();
            Panel tr13_1_0p = new Panel();
            PixelLayout tr13_1_0pl = new PixelLayout();
            tr13_1_0p.Content = tr13_1_0pl;
            tr13_1_0.Control = tr13_1_0p;
            tr13_1_0pl.Add(num_arrayYCount, 0, 0);
            tr13_c_0.Cells.Add(tr13_1_0);


            num_arrayYSpace = new NumericStepper();
            num_arrayYSpace.Value = 0.00;
            num_arrayYSpace.Increment = 0.1;
            num_arrayYSpace.DecimalPlaces = 2;
            num_arrayYSpace.ToolTip = "Array Y Space";
            setSize(num_arrayYSpace, numWidth, num_Height);

            TableCell tr13_1_1 = new TableCell();
            Panel tr13_1_1p = new Panel();
            PixelLayout tr13_1_1pl = new PixelLayout();
            tr13_1_1p.Content = tr13_1_1pl;
            tr13_1_1.Control = tr13_1_1p;
            tr13_1_1pl.Add(num_arrayYSpace, 0, 0);
            tr13_c_0.Cells.Add(tr13_1_1);
        }

        void sp3_ArrayRot(TableLayout groupBox_position_table)
        {
            TableRow tr10pre = new TableRow();
            groupBox_position_table.Rows.Add(tr10pre);

            lbl_arrayRotation = new Label();
            lbl_arrayRotation.Text = "Array Rotation";
            lbl_arrayRotation.ToolTip = "Array Rotation";

            TableCell tr10pre_0 = new TableCell();
            Panel tr10pre_0p = new Panel();
            PixelLayout tr10pre_0pl = new PixelLayout();
            tr10pre_0p.Content = tr10pre_0pl;
            tr10pre_0.Control = tr10pre_0p;
            tr10pre_0pl.Add(lbl_arrayRotation, 0, 0);
            tr10pre.Cells.Add(tr10pre_0);


            TableCell tr10pre_1 = new TableCell();
            Panel tr10pre_1p = new Panel();
            TableLayout tr10pre_container = new TableLayout();
            tr10pre_1p.Content = tr10pre_container;
            tr10pre_1.Control = tr10pre_1p;
            TableRow tr10pre_c_0 = new TableRow();
            tr10pre_container.Rows.Add(tr10pre_c_0);
            tr10pre.Cells.Add(tr10pre_1);

            num_minArrayRot = new NumericStepper();
            num_minArrayRot.Increment = 0.1;
            num_minArrayRot.DecimalPlaces = 2;
            setSize(num_minArrayRot, numWidth, num_Height);

            TableCell tr10pre_1_0 = new TableCell();
            Panel tr10pre_1_0p = new Panel();
            PixelLayout tr10pre_1_0pl = new PixelLayout();
            tr10pre_1_0p.Content = tr10pre_1_0pl;
            tr10pre_1_0.Control = tr10pre_1_0p;
            tr10pre_1_0pl.Add(num_minArrayRot, 0, 0);
            tr10pre_c_0.Cells.Add(tr10pre_1_0);


            num_incArrayRot = new NumericStepper();
            num_incArrayRot.Increment = 0.1;
            num_incArrayRot.DecimalPlaces = 2;
            setSize(num_incArrayRot, numWidth, num_Height);

            TableCell tr10pre_1_1 = new TableCell();
            Panel tr10pre_1_1p = new Panel();
            PixelLayout tr10pre_1_1pl = new PixelLayout();
            tr10pre_1_1p.Content = tr10pre_1_1pl;
            tr10pre_1_1.Control = tr10pre_1_1p;
            tr10pre_1_1pl.Add(num_incArrayRot, 0, 0);
            tr10pre_c_0.Cells.Add(tr10pre_1_1);


            num_stepsArrayRot = new NumericStepper();
            num_stepsArrayRot.Increment = 1;
            num_stepsArrayRot.DecimalPlaces = 0;
            num_stepsArrayRot.MinValue = 1;
            setSize(num_stepsArrayRot, numWidth, num_Height);

            TableCell tr10pre_1_2 = new TableCell();
            Panel tr10pre_1_2p = new Panel();
            PixelLayout tr10pre_1_2pl = new PixelLayout();
            tr10pre_1_2p.Content = tr10pre_1_2pl;
            tr10pre_1_2.Control = tr10pre_1_2p;
            tr10pre_1_2pl.Add(num_stepsArrayRot, 0, 0);
            tr10pre_c_0.Cells.Add(tr10pre_1_2);

            TableRow tr10 = new TableRow();
            groupBox_position_table.Rows.Add(tr10);

            Label lbl_relArrayRot = new Label();
            lbl_relArrayRot.Text = "Relative Array Rotation";
            lbl_relArrayRot.ToolTip = "Relative Array Rotation";

            TableCell tr10_0 = new TableCell();
            Panel tr10_0p = new Panel();
            PixelLayout tr10_0pl = new PixelLayout();
            tr10_0p.Content = tr10_0pl;
            tr10_0.Control = tr10_0p;
            tr10_0pl.Add(lbl_relArrayRot, 0, 0);
            tr10.Cells.Add(tr10_0);

            TableCell tr10_1 = new TableCell();
            Panel tr10_1p = new Panel();
            TableLayout tr10_1tl = new TableLayout();
            tr10_1tl.Rows.Add(new TableRow());
            tr10_1p.Content = tr10_1tl;
            tr10_1.Control = tr10_1p;
            tr10.Cells.Add(tr10_1);

            comboBox_arrayRotRef = new DropDown();
            comboBox_arrayRotRef.DataContext = DataContext;
            comboBox_arrayRotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);
            comboBox_arrayRotRef.SelectedIndex = 0;
            comboBox_arrayRotRef.ToolTip = "Rotation relative to this pattern element";

            tr10_1tl.Rows[tr10_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = comboBox_arrayRotRef });

            TableRow tr10b = new TableRow();
            tr10_1tl.Rows.Add(tr10b);

            TableLayout tr10b_1tl = new TableLayout();
            tr10b_1tl.Rows.Add(new TableRow());

            tr10b.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(tr10b_1tl) });

            lbl_arrayUse = new Label();
            lbl_arrayUse.Text = "Use";
            tr10b_1tl.Rows[tr10b_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = lbl_arrayUse });

            checkBox_refArrayPivot = new CheckBox();
            checkBox_refArrayPivot.Text = "Pivot";
            checkBox_refArrayPivot.Enabled = false;
            checkBox_refArrayPivot.ToolTip = "Use pivot point from reference.";

            tr10b_1tl.Rows[tr10b_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refArrayPivot });

            checkBox_arrayRotRef = new CheckBox();
            checkBox_arrayRotRef.Text = "Array";
            checkBox_arrayRotRef.Enabled = false;
            checkBox_arrayRotRef.ToolTip = "Use array rotation rather than shape.";

            tr10b_1tl.Rows[tr10b_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_arrayRotRef });

            tr10_1tl.Rows.Add(new TableRow());

            checkBox_refArrayBoundsAfterRotation = new CheckBox();
            checkBox_refArrayBoundsAfterRotation.Text = "Bounds after rotation";
            checkBox_refArrayBoundsAfterRotation.Enabled = false;
            checkBox_refArrayBoundsAfterRotation.ToolTip = "Perform rotation before bounding box. This affects the pivot.";

            tr10_1tl.Rows[tr10_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = checkBox_refArrayBoundsAfterRotation });

            tr10_1tl.Rows[tr10_1tl.Rows.Count - 1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            groupBox_position_table.Rows.Add(new TableRow() { ScaleHeight = true });
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

        void doPatternElementUI_subshape(int pattern, int index, bool updateUI, string shapeString)
        {
            int previousIndex = comboBox_subShapeRef.SelectedIndex;

            if (updateUI)
            {
                if ((shapeString != "bounding") && (shapeString != "complex"))
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
                        do_S(pattern, index);
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
            else
            {
                if (shapeString == "bounding")
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
                if (shapeString == "complex")
                {
                    for (int i = 0; i < commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.externalGeoVertexCount); i++)
                    {
                        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.externalGeoCoordX, Convert.ToDecimal(num_externalGeoCoordsX[i].Value), i);
                        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.externalGeoCoordY, Convert.ToDecimal(num_externalGeoCoordsY[i].Value), i);
                    }
                }
            }

        }

        void doPatternElementUI_position(int pattern, int index)
        {
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

            num_arrayXCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYCount.Enabled = bounding ? false : !isRelativeArray;
            num_arrayXSpace.Enabled = bounding ? false : !isRelativeArray;
            num_arrayYSpace.Enabled = bounding ? false : !isRelativeArray;

            // Register the relative array status with the pattern element.
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.relativeArray, isRelativeArray ? 1 : 0);

            num_minArrayRot.Enabled = isArray || isRelativeArray;
            num_incArrayRot.Enabled = isArray || isRelativeArray;
            num_stepsArrayRot.Enabled = isArray || isRelativeArray;
            comboBox_arrayRotRef.Enabled = isArray || isRelativeArray;
            checkBox_refArrayPivot.Enabled = isArray || isRelativeArray;

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayXCount, Convert.ToInt32(num_arrayXCount.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayYCount, Convert.ToInt32(num_arrayYCount.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayXSpace, Convert.ToDecimal(num_arrayXSpace.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayYSpace, Convert.ToDecimal(num_arrayYSpace.Value));

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

        void do_S(int pattern, int index)
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

        void clampSubShape(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                if (num_layer_subshape_minhl.Value < minHLength)
                {
                    num_layer_subshape_minhl.Value = minHLength;
                }

                if (num_layer_subshape_minhl.Value > maxHLength)
                {
                    num_layer_subshape_minhl.Value = maxHLength;
                }

                if (num_layer_subshape_minvl.Value < minVLength)
                {
                    num_layer_subshape_minvl.Value = minVLength;
                }

                if (num_layer_subshape_minvl.Value > maxVLength)
                {
                    num_layer_subshape_minvl.Value = maxVLength;
                }

                if (num_layer_subshape_minho.Value < minHOffset)
                {
                    num_layer_subshape_minho.Value = minHOffset;
                }

                if (num_layer_subshape_minho.Value > maxHOffset)
                {
                    num_layer_subshape_minho.Value = maxHOffset;
                }

                if (num_layer_subshape_minvo.Value < minVOffset)
                {
                    num_layer_subshape_minvo.Value = minVOffset;
                }

                if (num_layer_subshape_minvo.Value > maxVOffset)
                {
                    num_layer_subshape_minvo.Value = maxVOffset;
                }
            });
        }

        void clampSubShape2(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                if (num_layer_subshape2_minhl.Value < minHLength)
                {
                    num_layer_subshape2_minhl.Value = minHLength;
                }

                if (num_layer_subshape2_minhl.Value > maxHLength)
                {
                    num_layer_subshape2_minhl.Value = maxHLength;
                }

                if (num_layer_subshape2_minvl.Value < minVLength)
                {
                    num_layer_subshape2_minvl.Value = minVLength;
                }

                if (num_layer_subshape2_minvl.Value > maxVLength)
                {
                    num_layer_subshape2_minvl.Value = maxVLength;
                }

                if (num_layer_subshape2_minho.Value < minHOffset)
                {
                    num_layer_subshape2_minho.Value = minHOffset;
                }

                if (num_layer_subshape2_minho.Value > maxHOffset)
                {
                    num_layer_subshape2_minho.Value = maxHOffset;
                }

                if (num_layer_subshape2_minvo.Value < minVOffset)
                {
                    num_layer_subshape2_minvo.Value = minVOffset;
                }

                if (num_layer_subshape2_minvo.Value > maxVOffset)
                {
                    num_layer_subshape2_minvo.Value = maxVOffset;
                }
            });
        }

        void clampSubShape3(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                if (num_layer_subshape3_minhl.Value < minHLength)
                {
                    num_layer_subshape3_minhl.Value = minHLength;
                }

                if (num_layer_subshape3_minhl.Value > maxHLength)
                {
                    num_layer_subshape3_minhl.Value = maxHLength;
                }

                if (num_layer_subshape3_minvl.Value < minVLength)
                {
                    num_layer_subshape3_minvl.Value = minVLength;
                }

                if (num_layer_subshape3_minvl.Value > maxVLength)
                {
                    num_layer_subshape3_minvl.Value = maxVLength;
                }

                if (num_layer_subshape3_minho.Value < minHOffset)
                {
                    num_layer_subshape3_minho.Value = minHOffset;
                }

                if (num_layer_subshape3_minho.Value > maxHOffset)
                {
                    num_layer_subshape3_minho.Value = maxHOffset;
                }

                if (num_layer_subshape3_minvo.Value < minVOffset)
                {
                    num_layer_subshape3_minvo.Value = minVOffset;
                }

                if (num_layer_subshape3_minvo.Value > maxVOffset)
                {
                    num_layer_subshape3_minvo.Value = maxVOffset;
                }
            });
        }
    }
}
