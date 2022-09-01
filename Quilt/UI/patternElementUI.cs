using Eto.Drawing;
using Eto.Forms;
using System;

namespace Quilt;

public partial class MainForm
{
    private bool UIFreeze;

    private void pSetSize(Button _control, int width, int height)
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

    private void pSetSize(CommonControl _control, int width, int height)
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

    private void pExternalGeoUI(int index)
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
                groupBox_layout_table.Rows[i].Cells.Add(new TableCell { Control = TableLayout.AutoSized(new Label { Text = "Edge " + i }) });

                double val0 = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.externalGeoCoordX, i));
                num_externalGeoCoordsX[i] = new NumericStepper { Value = val0, DecimalPlaces = 2 };
                groupBox_layout_table.Rows[i].Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_externalGeoCoordsX[i]) }); // length

                double val1 = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.externalGeoCoordY, i));
                num_externalGeoCoordsY[i] = new NumericStepper { Value = val1, DecimalPlaces = 2 };
                groupBox_layout_table.Rows[i].Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_externalGeoCoordsY[i]) }); // increment

                num_externalGeoCoordsX[i].LostFocus += pDoPatternElementUI;
                num_externalGeoCoordsY[i].LostFocus += pDoPatternElementUI;
            }
        }
        else
        {
            groupBox_layout_table.Rows.Add(new TableRow());
            TextArea ta = new()
            {
                ReadOnly = true,
                Text =
                    "This shape type is reserved for non-orthogonal geometry found when defining a pattern from layout.\r\nIt is not currently available for other uses.",
                Wrap = true
            };
            groupBox_layout_table.Rows[0].Cells.Add(new TableCell { Control = ta });
            pClearPositionBox();
        }
    }

    private void pClearPatternElementUI()
    {
        comboBox_patternElementShape.Visible = commonVars.stitcher.getPatternCount() > 0;

        pClearPositionBox();
        pClearPropertiesBox();
    }

    private void pClearPropertiesBox()
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

    private void pClearPositionBox()
    {
        groupBox_position.Visible = false;
    }

    private void pSetupPatternElementUI_2()
    {
        Application.Instance.Invoke(() =>
        {
            TableRow right_tr1 = new();
            right_tl.Rows.Add(right_tr1);
            TableCell right_tr1_0 = new();
            right_tr1.Cells.Add(right_tr1_0);

            groupBox_properties = new GroupBox();
            pSubShapesTableLayout(right_tr1_0);
        });
    }

    private void pSetupPatternElementUI_3()
    {
        Application.Instance.Invoke(() =>
        {
            TableRow right_tr2 = new();
            right_tl.Rows.Add(right_tr2);

            TableCell right_tr2_0 = new();
            right_tr2.Cells.Add(right_tr2_0);

            groupBox_position = new GroupBox();

            Expander s = new();
            TableLayout groupBox_position_table = new();
            s.Content = groupBox_position_table;
            s.Expanded = quiltContext.expandUI;
            groupBox_position.Content = s;
            groupBox_position.Text = "Position";

            right_tr2_0.Control = groupBox_position;

            pSubShapeRef_UI(groupBox_position_table);
            pSubShapePos_UI(groupBox_position_table);
            pSubShapeXPos_UI(groupBox_position_table);
            pSubShapeXRelPos_UI(groupBox_position_table);
            pSubShapeXRelPosSS_UI(groupBox_position_table);
            pSubShapeYPos_UI(groupBox_position_table);
            pSubShapeYRelPos_UI(groupBox_position_table);
            pSubShapeYRelPosSS_UI(groupBox_position_table);
            pRot_UI(groupBox_position_table);
            pRelRot_UI(groupBox_position_table);
            pFlip_UI(groupBox_position_table);
            pArray_UI(groupBox_position_table);
            pMerge_UI(groupBox_position_table);
        });
    }

    private void pSetupPatternElementUI_4()
    {
        Application.Instance.Invoke(() =>
        {
            groupBox_bounding_table = new TableLayout();

            TableLayout tl = new();
            Panel row0 = new() {Content = tl};
            groupBox_bounding_table.Rows.Add(new TableRow());
            groupBox_bounding_table.Rows[^1].Cells.Add(new TableCell { Control = row0 });

            tl.Rows.Add(new TableRow());

            Label bbLeft = new() {Text = "Left Padding"};
            tl.Rows[^1].Cells.Add(new TableCell { Control = bbLeft });

            num_layer_minbbl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_minbbl, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_minbbl });

            num_layer_bblinc = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_bblinc, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bblinc });

            num_layer_bblsteps = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_layer_bblsteps, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bblsteps });

            tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

            tl.Rows.Add(new TableRow());

            Label bbRight = new() {Text = "Right Padding"};
            tl.Rows[^1].Cells.Add(new TableCell { Control = bbRight });

            num_layer_minbbr = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_minbbr, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_minbbr });

            num_layer_bbrinc = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_bbrinc, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bbrinc });

            num_layer_bbrsteps = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_layer_bbrsteps, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bbrsteps });

            tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

            tl.Rows.Add(new TableRow());

            Label bbBottom = new() {Text = "Bottom Padding"};
            tl.Rows[^1].Cells.Add(new TableCell { Control = bbBottom });

            num_layer_minbbb = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_minbbb, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_minbbb });

            num_layer_bbbinc = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_bbbinc, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bbbinc });

            num_layer_bbbsteps = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_layer_bbbsteps, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bbbsteps });

            tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

            tl.Rows.Add(new TableRow());

            Label bbTop = new() {Text = "Top Padding"};
            tl.Rows[^1].Cells.Add(new TableCell { Control = bbTop });

            num_layer_minbbt = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_minbbt, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_minbbt });

            num_layer_bbtinc = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_layer_bbtinc, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bbtinc });

            num_layer_bbtsteps = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_layer_bbtsteps, numWidth, num_Height);
            tl.Rows[^1].Cells.Add(new TableCell { Control = num_layer_bbtsteps });


            tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

            groupBox_bounding_table.Rows.Add(new TableRow { ScaleHeight = true });

        });
    }

    private void pQuiltUISetup()
    {
        TableRow right_tr0 = new();
        right_tl.Rows.Add(right_tr0);

        comboBox_patternElementShape = new DropDown
        {
            Width = 120, DataContext = DataContext, SelectedIndex = 0, ToolTip = "Type of shape to generate"
        };
        comboBox_patternElementShape.BindDataContext(c => c.DataStore, (UIStringLists m) => m.shapes);

        TableCell right_tr0_0 = new();
        right_tr0.Cells.Add(right_tr0_0);
        right_tr0_0.Control = comboBox_patternElementShape;

        pSetupPatternElementUI_2();
        pSetupPatternElementUI_3();
        pSetupPatternElementUI_4();

        right_tl.Rows.Add(new TableRow { ScaleHeight = true });

        pAddHandlers();

        pDoColors();
    }
}