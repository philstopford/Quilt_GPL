using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
    private Panel pMinHorLengthUI()
    {
        TableLayout tl = new();
        Panel p = new() {Content = TableLayout.AutoSized(tl)};
        tl.Rows.Add(new TableRow());

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Minimum Horizontal Length", Content = g_tl};

        g_tl.Rows.Add(new TableRow());
            
        tl.Rows[^1].Cells.Add(new TableCell { Control = gb1 });
        tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

        TableLayout left = new();
        TableRow leftUpper = new();
        TableRow leftMiddle = new();
        TableRow leftLower = new();

        TableLayout utl = new();
        Panel pU1 = new() {Content = TableLayout.AutoSized(utl)};
        utl.Rows.Add(leftUpper);
            
        left.Rows.Add(new TableRow());
        left.Rows[^1].Cells.Add(new TableCell {Control =  pU1});
        left.Rows.Add(leftMiddle);
        left.Rows.Add(leftLower);

        comboBox_s0_minhl_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s0_minhl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhl_ref });

        comboBox_s0_minhl_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s0_minhl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhl_subShapeRef });

        cb_s0_hl_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };
        
        TableLayout s0_hl = new();
        TableRow s0_hl_r = new();
        s0_hl.Rows.Add(s0_hl_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_hl)});
        
        s0_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s0_hl_final)});

        TableRow s0_hl_r1 = new();
        s0_hl.Rows.Add(s0_hl_r1);

        btn_s0_hl = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false,
        };
        btn_s0_hl.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinHLRef);            
        };

        s0_hl_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_hl)});
        
        num_layer_subshape_minhl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_minhl) });

        Panel pLeft = new() {Content = TableLayout.AutoSized(left)};

        g_tl.Rows[^1].Cells.Add(pLeft);
            
        TableLayout middle = new();
        TableRow middleUpper = new();
        TableRow middleMiddle = new();
        TableRow middleLower = new();

        TableLayout utl2 = new();
        Panel pU2 = new() {Content = TableLayout.AutoSized(utl2)};
        utl2.Rows.Add(middleUpper);
            
        middle.Rows.Add(new TableRow());
        middle.Rows[^1].Cells.Add(new TableCell {Control =  pU2});
        middle.Rows.Add(middleMiddle);
        middle.Rows.Add(middleLower);

        comboBox_s1_minhl_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s1_minhl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhl_ref });

        comboBox_s1_minhl_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s1_minhl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhl_subShapeRef });

        cb_s1_hl_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s1_hl = new();
        TableRow s1_hl_r = new();
        s1_hl.Rows.Add(s1_hl_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_hl)});
        
        s1_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s1_hl_final)});

        TableRow s1_hl_r1 = new();
        s1_hl.Rows.Add(s1_hl_r1);

        btn_s1_hl = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_hl.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinHLRef, 1);            
        };

        s1_hl_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_hl)});
        
        num_layer_subshape2_minhl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_minhl) });

        Panel pMiddle = new() {Content = TableLayout.AutoSized(middle)};

        g_tl.Rows[^1].Cells.Add(pMiddle);

        TableLayout right = new();
        TableRow rightUpper = new();
        TableRow rightMiddle = new();
        TableRow rightLower = new();

        TableLayout utl3 = new();
        Panel pU3 = new() {Content = TableLayout.AutoSized(utl3)};
        utl3.Rows.Add(rightUpper);
            
        right.Rows.Add(new TableRow());
        right.Rows[^1].Cells.Add(new TableCell {Control =  pU3});
        right.Rows.Add(rightMiddle);
        right.Rows.Add(rightLower);

        comboBox_s2_minhl_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s2_minhl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhl_ref });

        comboBox_s2_minhl_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s2_minhl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhl_subShapeRef });

        cb_s2_hl_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s2_hl = new();
        TableRow s2_hl_r = new();
        s2_hl.Rows.Add(s2_hl_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_hl)});
        
        s2_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s2_hl_final)});
        
        TableRow s2_hl_r1 = new();
        s2_hl.Rows.Add(s2_hl_r1);

        btn_s2_hl = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_hl.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinHLRef, 2);            
        };

        s2_hl_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_hl)});
        
        num_layer_subshape3_minhl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_minhl) });

        Panel pRight = new() {Content = TableLayout.AutoSized(right)};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pHorLengthIncrementUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
            
        Panel p = new() {Content = TableLayout.AutoSized(tl)};
            
        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Horizontal Length Increment", Content = g_tl};

        g_tl.Rows.Add(new TableRow());
            
        tl.Rows[^1].Cells.Add(new TableCell { Control = gb1 });
        tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

        TableLayout left = new();
        TableRow leftUpper = new();
        TableRow leftMiddle = new();
        TableRow leftLower = new();

        TableLayout utl = new();
        Panel pU1 = new() {Content = TableLayout.AutoSized(utl)};
        utl.Rows.Add(leftUpper);
            
        left.Rows.Add(new TableRow());
        left.Rows[^1].Cells.Add(new TableCell {Control =  pU1});
        left.Rows.Add(leftMiddle);
        left.Rows.Add(leftLower);

        comboBox_s0_minhlinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s0_minhlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhlinc_ref });

        comboBox_s0_minhlinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s0_minhlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLIncRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhlinc_subShapeRef });
        
        TableLayout s0_hl = new();
        TableRow s0_hl_r = new();
        s0_hl.Rows.Add(s0_hl_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_hl)});
        
        btn_s0_hlinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_hlinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HLIncRef);            
        };
        s0_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_hlinc)});
        
        num_layer_subshape_incHL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_incHL) });

        Panel pLeft = new() {Content = TableLayout.AutoSized(left)};

        g_tl.Rows[^1].Cells.Add(pLeft);
            
        TableLayout middle = new();
        TableRow middleUpper = new();
        TableRow middleMiddle = new();
        TableRow middleLower = new();

        TableLayout utl2 = new();
        Panel pU2 = new() {Content = TableLayout.AutoSized(utl2)};
        utl2.Rows.Add(middleUpper);
            
        middle.Rows.Add(new TableRow());
        middle.Rows[^1].Cells.Add(new TableCell {Control =  pU2});
        middle.Rows.Add(middleMiddle);
        middle.Rows.Add(middleLower);

        comboBox_s1_minhlinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s1_minhlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhlinc_ref });

        comboBox_s1_minhlinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s1_minhlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLIncRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhlinc_subShapeRef });

        TableLayout s1_hl = new();
        TableRow s1_hl_r = new();
        s1_hl.Rows.Add(s1_hl_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_hl)});
        
        btn_s1_hlinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_hlinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HLIncRef, 1);            
        };
        s1_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_hlinc)});
        
        num_layer_subshape2_incHL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_incHL) });

        Panel pMiddle = new() {Content = TableLayout.AutoSized(middle)};

        g_tl.Rows[^1].Cells.Add(pMiddle);

        TableLayout right = new();
        TableRow rightUpper = new();
        TableRow rightMiddle = new();
        TableRow rightLower = new();

        TableLayout utl3 = new();
        Panel pU3 = new() {Content = TableLayout.AutoSized(utl3)};
        utl3.Rows.Add(rightUpper);
            
        right.Rows.Add(new TableRow());
        right.Rows[^1].Cells.Add(new TableCell {Control =  pU3});
        right.Rows.Add(rightMiddle);
        right.Rows.Add(rightLower);

        comboBox_s2_minhlinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s2_minhlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhlinc_ref });

        comboBox_s2_minhlinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s2_minhlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLIncRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhlinc_subShapeRef });
        
        TableLayout s2_hl = new();
        TableRow s2_hl_r = new();
        s2_hl.Rows.Add(s2_hl_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_hl)});
        
        btn_s2_hlinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_hlinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HLIncRef, 2);            
        };
        s2_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_hlinc)});
        
        num_layer_subshape3_incHL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_incHL) });

        Panel pRight = new() {Content = TableLayout.AutoSized(right)};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pHorLengthStepsUI()
    {
        TableLayout tl = new();
        Panel p = new() {Content = tl};
        tl.Rows.Add(new TableRow());
            
        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Horizontal Length Steps", Content = g_tl};

        g_tl.Rows.Add(new TableRow());
            
        tl.Rows[^1].Cells.Add(new TableCell { Control = gb1 });
        tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

        TableLayout left = new();
        TableRow leftUpper = new();
        TableRow leftMiddle = new();
        TableRow leftLower = new();

        TableLayout utl = new();
        Panel pU1 = new() {Content = TableLayout.AutoSized(utl)};
        utl.Rows.Add(leftUpper);
            
        left.Rows.Add(new TableRow());
        left.Rows[^1].Cells.Add(new TableCell {Control =  pU1});
        left.Rows.Add(leftMiddle);
        left.Rows.Add(leftLower);

        comboBox_s0_minhlsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s0_minhlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhlsteps_ref });

        comboBox_s0_minhlsteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s0_minhlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLStepsRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhlsteps_subShapeRef });

        TableLayout s0_hl = new();
        TableRow s0_hl_r = new();
        s0_hl.Rows.Add(s0_hl_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_hl)});
        
        btn_s0_hlst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_hlst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HLStepsRef);            
        };
        s0_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_hlst)});

        num_layer_subshape_stepsHL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_stepsHL) });

        Panel pLeft = new() {Content = left};

        g_tl.Rows[^1].Cells.Add(pLeft);
            
        TableLayout middle = new();
        TableRow middleUpper = new();
        TableRow middleMiddle = new();
        TableRow middleLower = new();

        TableLayout utl2 = new();
        Panel pU2 = new() {Content = TableLayout.AutoSized(utl2)};
        utl2.Rows.Add(middleUpper);
            
        middle.Rows.Add(new TableRow());
        middle.Rows[^1].Cells.Add(new TableCell {Control =  pU2});
        middle.Rows.Add(middleMiddle);
        middle.Rows.Add(middleLower);

        comboBox_s1_minhlsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s1_minhlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhlsteps_ref });

        comboBox_s1_minhlsteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s1_minhlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLStepsRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhlsteps_subShapeRef });

        TableLayout s1_hl = new();
        TableRow s1_hl_r = new();
        s1_hl.Rows.Add(s1_hl_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_hl)});
        
        btn_s1_hlst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_hlst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HLStepsRef, 1);            
        };
        s1_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_hlst)});

        num_layer_subshape2_stepsHL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_stepsHL) });

        Panel pMiddle = new() {Content = middle};

        g_tl.Rows[^1].Cells.Add(pMiddle);

        TableLayout right = new();
        TableRow rightUpper = new();
        TableRow rightMiddle = new();
        TableRow rightLower = new();

        TableLayout utl3 = new();
        Panel pU3 = new() {Content = TableLayout.AutoSized(utl3)};
        utl3.Rows.Add(rightUpper);
            
        right.Rows.Add(new TableRow());
        right.Rows[^1].Cells.Add(new TableCell {Control =  pU3});
        right.Rows.Add(rightMiddle);
        right.Rows.Add(rightLower);

        comboBox_s2_minhlsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s2_minhlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhlsteps_ref });

        comboBox_s2_minhlsteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s2_minhlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHLStepsRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhlsteps_subShapeRef });

        TableLayout s2_hl = new();
        TableRow s2_hl_r = new();
        s2_hl.Rows.Add(s2_hl_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_hl)});
        
        btn_s2_hlst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_hlst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HLStepsRef, 2);            
        };
        s2_hl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_hlst)});

        num_layer_subshape3_stepsHL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_stepsHL) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

}