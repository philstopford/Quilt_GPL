using Eto.Forms;

namespace Quilt
{
    public partial class MainForm
    {
        void pSubShapesTableLayout(TableCell tc)
        {
            Application.Instance.Invoke(() =>
            {
                // groupBox_subShapes.Size = new Size(310, 150);
                groupBox_subShapes_table = new TableLayout();
                groupBox_properties.Content = groupBox_subShapes_table;
                groupBox_properties.Text = "SubShapes";
                tc.Control = groupBox_properties;

                TabControl subshapes_tabcontrol = new TabControl();
                TabPage subshapes_dimension = new TabPage() {Text =  "Dimensions"};
                TableLayout dimensions_tl = new TableLayout();
                subshapes_dimension.Content = dimensions_tl;
                TabPage subshapes_offset = new TabPage() {Text =  "Offsets"};
                TableLayout offsets_tl = new TableLayout();
                subshapes_offset.Content = offsets_tl;
                subshapes_tabcontrol.Pages.Add(subshapes_dimension);
                subshapes_tabcontrol.Pages.Add(subshapes_offset);

                groupBox_subShapes_table.Rows.Add(new TableRow());

                groupBox_subShapes_table.Rows[^1].Cells.Add(new TableCell() { Control = subshapes_tabcontrol });

                dimensions_tl.Rows.Add(new TableRow());
                
                dimensions_tl.Rows[^1].Cells.Add(new TableCell() { Control = pMinHorLengthUI() });

                dimensions_tl.Rows.Add(new TableRow());

                dimensions_tl.Rows[^1].Cells.Add(new TableCell() { Control = pHorLengthIncrementUI() });

                dimensions_tl.Rows.Add(new TableRow());

                dimensions_tl.Rows[^1].Cells.Add(new TableCell() { Control = pHorLengthStepsUI() });

                offsets_tl.Rows.Add(new TableRow());

                offsets_tl.Rows[^1].Cells.Add(new TableCell() { Control = pMinHorOffsetUI() });

                offsets_tl.Rows.Add(new TableRow());

                offsets_tl.Rows[^1].Cells.Add(new TableCell() { Control = pHorOffsetIncrementUI() });

                offsets_tl.Rows.Add(new TableRow());

                offsets_tl.Rows[^1].Cells.Add(new TableCell() { Control = pHorOffsetStepsUI() });

                dimensions_tl.Rows.Add(new TableRow());

                dimensions_tl.Rows[^1].Cells.Add(new TableCell() { Control = pMinVerLengthUI() });

                dimensions_tl.Rows.Add(new TableRow());

                dimensions_tl.Rows[^1].Cells.Add(new TableCell() { Control = pVerLengthIncrementUI() });

                dimensions_tl.Rows.Add(new TableRow());

                dimensions_tl.Rows[^1].Cells.Add(new TableCell() { Control = pVerLengthStepsUI() });

                offsets_tl.Rows.Add(new TableRow());

                offsets_tl.Rows[^1].Cells.Add(new TableCell() { Control = pMinVerOffsetUI() });

                offsets_tl.Rows.Add(new TableRow());

                offsets_tl.Rows[^1].Cells.Add(new TableCell() { Control = pVerOffsetIncrementUI() });

                offsets_tl.Rows.Add(new TableRow());

                offsets_tl.Rows[^1].Cells.Add(new TableCell() { Control = pVerOffsetStepsUI() });

                num_externalGeoCoordsX = new NumericStepper[1];
                num_externalGeoCoordsY = new NumericStepper[1];
            });
        }

        Panel pMinHorLengthUI()
        {
            TableLayout tl = new TableLayout();
            Panel p = new Panel {Content = TableLayout.AutoSized(tl)};
            tl.Rows.Add(new TableRow());

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Min Hor. Length", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftMiddle = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftMiddle);
            left.Rows.Add(leftLower);

            comboBox_s0_minhl_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhl_ref });

            comboBox_s0_minhl_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhl_subShapeRef });

            cb_s0_hl_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            leftMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s0_hl_final)});

            num_layer_subshape_minhl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_minhl) });

            Panel pLeft = new Panel {Content = TableLayout.AutoSized(left)};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleMiddle = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleMiddle);
            middle.Rows.Add(middleLower);

            comboBox_s1_minhl_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhl_ref });

            comboBox_s1_minhl_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhl_subShapeRef });

            cb_s1_hl_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            middleMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s1_hl_final)});

            num_layer_subshape2_minhl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_minhl) });

            Panel pMiddle = new Panel {Content = TableLayout.AutoSized(middle)};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightMiddle = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightMiddle);
            right.Rows.Add(rightLower);

            comboBox_s2_minhl_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhl_ref });

            comboBox_s2_minhl_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhl_subShapeRef });

            cb_s2_hl_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            rightMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s2_hl_final)});

            num_layer_subshape3_minhl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_minhl) });

            Panel pRight = new Panel {Content = TableLayout.AutoSized(right)};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pHorLengthIncrementUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            
            Panel p = new Panel {Content = TableLayout.AutoSized(tl)};
            
            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Hor. Length Increment", Content = g_tl};

            g_tl.Rows.Add(new TableRow());

            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minhlinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhlinc_ref });

            comboBox_s0_minhlinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLIncRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhlinc_subShapeRef });

            num_layer_subshape_incHL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_incHL) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minhlinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhlinc_ref });

            comboBox_s1_minhlinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLIncRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhlinc_subShapeRef });

            num_layer_subshape2_incHL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_incHL) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minhlinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhlinc_ref });

            comboBox_s2_minhlinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLIncRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhlinc_subShapeRef });

            num_layer_subshape3_incHL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_incHL) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pHorLengthStepsUI()
        {
            TableLayout tl = new TableLayout();
            Panel p = new Panel {Content = tl};
            tl.Rows.Add(new TableRow());
            
            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Hor. Length Steps", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minhlsteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhlsteps_ref });

            comboBox_s0_minhlsteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLStepsRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhlsteps_subShapeRef });

            num_layer_subshape_stepsHL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_stepsHL) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minhlsteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhlsteps_ref });

            comboBox_s1_minhlsteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLStepsRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhlsteps_subShapeRef });

            num_layer_subshape2_stepsHL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_stepsHL) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minhlsteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhlsteps_ref });

            comboBox_s2_minhlsteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLStepsRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhlsteps_subShapeRef });

            num_layer_subshape3_stepsHL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_stepsHL) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pMinHorOffsetUI()
        {
            TableLayout tl = new TableLayout();
            Panel p = new Panel {Content = tl};
            tl.Rows.Add(new TableRow());
            
            TableLayout g_tl = new TableLayout();

            GroupBox gb1 = new GroupBox {Text = "Min Hor. Offset", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftMiddle = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftMiddle);
            left.Rows.Add(leftLower);

            comboBox_s0_minho_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minho_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minho_ref });

            comboBox_s0_minho_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minho_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHORefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minho_subShapeRef });

            cb_s0_ho_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            leftMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s0_ho_final)});

            num_layer_subshape_minho = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_minho) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleMiddle = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleMiddle);
            middle.Rows.Add(middleLower);

            comboBox_s1_minho_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minho_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minho_ref });

            comboBox_s1_minho_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minho_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHORefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minho_subShapeRef });

            cb_s1_ho_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            middleMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s1_ho_final)});

            num_layer_subshape2_minho = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_minho) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightMiddle = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightMiddle);
            right.Rows.Add(rightLower);

            comboBox_s2_minho_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minho_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minho_ref });

            comboBox_s2_minho_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minho_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHORefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minho_subShapeRef });

            cb_s2_ho_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            rightMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s2_ho_final)});

            num_layer_subshape3_minho = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_minho) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pHorOffsetIncrementUI()
        {
            TableLayout tl = new TableLayout();
            Panel p = new Panel {Content = tl};
            tl.Rows.Add(new TableRow());
            
            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Hor. Offset Increment", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minhoinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhoinc_ref });

            comboBox_s0_minhoinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOIncRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhoinc_subShapeRef });

            num_layer_subshape_incHO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_incHO) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minhoinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhoinc_ref });

            comboBox_s1_minhoinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOIncRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhoinc_subShapeRef });

            num_layer_subshape2_incHO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_incHO) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minhoinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhoinc_ref });

            comboBox_s2_minhoinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOIncRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhoinc_subShapeRef });

            num_layer_subshape3_incHO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_incHO) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pHorOffsetStepsUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            Panel p = new Panel {Content = tl};

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Hor. Offset Steps", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minhosteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhosteps_ref });

            comboBox_s0_minhosteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minhosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOStepsRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minhosteps_subShapeRef });

            num_layer_subshape_stepsHO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};
            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_stepsHO) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minhosteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhosteps_ref });

            comboBox_s1_minhosteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minhosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOStepsRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minhosteps_subShapeRef });

            num_layer_subshape2_stepsHO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_stepsHO) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minhosteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhosteps_ref });

            comboBox_s2_minhosteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minhosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOStepsRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minhosteps_subShapeRef });

            num_layer_subshape3_stepsHO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_stepsHO) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pMinVerLengthUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            Panel p = new Panel {Content = tl};

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Min Ver. Length", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftMiddle = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftMiddle);
            left.Rows.Add(leftLower);

            comboBox_s0_minvl_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvl_ref });

            comboBox_s0_minvl_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvl_subShapeRef });

            cb_s0_vl_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            leftMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s0_vl_final)});

            num_layer_subshape_minvl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_minvl) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleMiddle = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleMiddle);
            middle.Rows.Add(middleLower);

            comboBox_s1_minvl_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvl_ref });

            comboBox_s1_minvl_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvl_subShapeRef });

            cb_s1_vl_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            middleMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s1_vl_final)});

            num_layer_subshape2_minvl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_minvl) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightMiddle = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightMiddle);
            right.Rows.Add(rightLower);

            comboBox_s2_minvl_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvl_ref });

            comboBox_s2_minvl_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvl_subShapeRef });

            cb_s2_vl_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            rightMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s2_vl_final)});

            num_layer_subshape3_minvl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_minvl) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pVerLengthIncrementUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            Panel p = new Panel {Content = tl};

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Ver. Length Increment", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minvlinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvlinc_ref });

            comboBox_s0_minvlinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLIncRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvlinc_subShapeRef });

            num_layer_subshape_incVL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_incVL) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minvlinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvlinc_ref });

            comboBox_s1_minvlinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLIncRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvlinc_subShapeRef });

            num_layer_subshape2_incVL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_incVL) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minvlinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvlinc_ref });

            comboBox_s2_minvlinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLIncRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvlinc_subShapeRef });

            num_layer_subshape3_incVL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_incVL) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pVerLengthStepsUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            Panel p = new Panel {Content = tl};

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Ver. Length Steps", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minvlsteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvlsteps_ref });

            comboBox_s0_minvlsteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLStepsRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvlsteps_subShapeRef });

            num_layer_subshape_stepsVL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_stepsVL) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minvlsteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvlsteps_ref });

            comboBox_s1_minvlsteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLStepsRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvlsteps_subShapeRef });

            num_layer_subshape2_stepsVL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_stepsVL) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minvlsteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvlsteps_ref });

            comboBox_s2_minvlsteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLStepsRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvlsteps_subShapeRef });

            num_layer_subshape3_stepsVL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_stepsVL) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pMinVerOffsetUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            Panel p = new Panel {Content = tl};

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Min Ver. Offset", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftMiddle = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftMiddle);
            left.Rows.Add(leftLower);

            comboBox_s0_minvo_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvo_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvo_ref });

            comboBox_s0_minvo_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvo_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVORefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvo_subShapeRef });

            cb_s0_vo_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            leftMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s0_vo_final)});

            num_layer_subshape_minvo = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_minvo) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleMiddle = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleMiddle);
            middle.Rows.Add(middleLower);

            comboBox_s1_minvo_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvo_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvo_ref });

            comboBox_s1_minvo_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvo_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVORefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvo_subShapeRef });

            cb_s1_vo_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            middleMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s1_vo_final)});

            num_layer_subshape2_minvo = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_minvo) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightMiddle = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightMiddle);
            right.Rows.Add(rightLower);

            comboBox_s2_minvo_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvo_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvo_ref });

            comboBox_s2_minvo_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvo_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVORefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvo_subShapeRef });

            cb_s2_vo_final = new CheckBox
            {
                Text = "Ref Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
            };
            rightMiddle.Cells.Add(new TableCell() {Control = TableLayout.AutoSized(cb_s2_vo_final)});

            num_layer_subshape3_minvo = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_minvo) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pVerOffsetIncrementUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            Panel p = new Panel {Content = tl};

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Ver. Offset Increment", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minvoinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvoinc_ref });

            comboBox_s0_minvoinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOIncRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvoinc_subShapeRef });

            num_layer_subshape_incVO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_incVO) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minvoinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvoinc_ref });

            comboBox_s1_minvoinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOIncRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvoinc_subShapeRef });

            num_layer_subshape2_incVO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_incVO) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minvoinc_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvoinc_ref });

            comboBox_s2_minvoinc_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOIncRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvoinc_subShapeRef });

            num_layer_subshape3_incVO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_incVO) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }

        Panel pVerOffsetStepsUI()
        {
            TableLayout tl = new TableLayout();
            tl.Rows.Add(new TableRow());
            Panel p = new Panel {Content = tl};

            TableLayout g_tl = new TableLayout();
            GroupBox gb1 = new GroupBox {Text = "Ver. Offset Steps", Content = g_tl};

            g_tl.Rows.Add(new TableRow());
            
            tl.Rows[^1].Cells.Add(new TableCell() { Control = gb1 });
            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            TableLayout left = new TableLayout();
            TableRow leftUpper = new TableRow();
            TableRow leftLower = new TableRow();

            TableLayout utl = new TableLayout();
            Panel pU1 = new Panel {Content = TableLayout.AutoSized(utl)};
            utl.Rows.Add(leftUpper);
            
            left.Rows.Add(new TableRow());
            left.Rows[^1].Cells.Add(new TableCell() {Control =  pU1});
            left.Rows.Add(leftLower);

            comboBox_s0_minvosteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvosteps_ref });

            comboBox_s0_minvosteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s0_minvosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOStepsRefSubShapeList);

            leftUpper.Cells.Add(new TableCell() { Control = comboBox_s0_minvosteps_subShapeRef });

            num_layer_subshape_stepsVO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            leftLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape_stepsVO) });

            Panel pLeft = new Panel {Content = left};

            g_tl.Rows[^1].Cells.Add(pLeft);
            
            TableLayout middle = new TableLayout();
            TableRow middleUpper = new TableRow();
            TableRow middleLower = new TableRow();

            TableLayout utl2 = new TableLayout();
            Panel pU2 = new Panel {Content = TableLayout.AutoSized(utl2)};
            utl2.Rows.Add(middleUpper);
            
            middle.Rows.Add(new TableRow());
            middle.Rows[^1].Cells.Add(new TableCell() {Control =  pU2});
            middle.Rows.Add(middleLower);

            comboBox_s1_minvosteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvosteps_ref });

            comboBox_s1_minvosteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s1_minvosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOStepsRefSubShape2List);

            middleUpper.Cells.Add(new TableCell() { Control = comboBox_s1_minvosteps_subShapeRef });

            num_layer_subshape2_stepsVO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            middleLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape2_stepsVO) });

            Panel pMiddle = new Panel {Content = middle};

            g_tl.Rows[^1].Cells.Add(pMiddle);

            TableLayout right = new TableLayout();
            TableRow rightUpper = new TableRow();
            TableRow rightLower = new TableRow();

            TableLayout utl3 = new TableLayout();
            Panel pU3 = new Panel {Content = TableLayout.AutoSized(utl3)};
            utl3.Rows.Add(rightUpper);
            
            right.Rows.Add(new TableRow());
            right.Rows[^1].Cells.Add(new TableCell() {Control =  pU3});
            right.Rows.Add(rightLower);

            comboBox_s2_minvosteps_ref = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvosteps_ref });

            comboBox_s2_minvosteps_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Use minimum value from this pattern element"
            };
            comboBox_s2_minvosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOStepsRefSubShape3List);

            rightUpper.Cells.Add(new TableCell() { Control = comboBox_s2_minvosteps_subShapeRef });

            num_layer_subshape3_stepsVO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

            rightLower.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(num_layer_subshape3_stepsVO) });

            Panel pRight = new Panel {Content = right};

            g_tl.Rows[^1].Cells.Add(pRight);

            return p;
        }
    }
}