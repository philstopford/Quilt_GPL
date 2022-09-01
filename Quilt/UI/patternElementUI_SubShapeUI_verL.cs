using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
    private Panel pMinVerLengthUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        Panel p = new() {Content = tl};

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Minimum Vertical Length", Content = g_tl};

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

        comboBox_s0_minvl_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s0_minvl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvl_ref });

        comboBox_s0_minvl_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s0_minvl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvl_subShapeRef });

        cb_s0_vl_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s0_vl = new();
        TableRow s0_vl_r = new();
        s0_vl.Rows.Add(s0_vl_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_vl)});
        
        s0_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s0_vl_final)});

        TableRow s0_vl_r1 = new();
        s0_vl.Rows.Add(s0_vl_r1);

        btn_s0_vl = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_vl.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinVLRef);            
        };
        s0_vl_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_vl)});
        
        num_layer_subshape_minvl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_minvl) });

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

        comboBox_s1_minvl_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s1_minvl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvl_ref });

        comboBox_s1_minvl_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s1_minvl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvl_subShapeRef });

        cb_s1_vl_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s1_vl = new();
        TableRow s1_vl_r = new();
        s1_vl.Rows.Add(s1_vl_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_vl)});
        
        s1_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s1_vl_final)});

        TableRow s1_vl_r1 = new();
        s1_vl.Rows.Add(s1_vl_r1);

        btn_s1_vl = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_vl.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinVLRef, 1);            
        };
        s1_vl_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_vl)});

        num_layer_subshape2_minvl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_minvl) });

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

        comboBox_s2_minvl_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s2_minvl_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvl_ref });

        comboBox_s2_minvl_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s2_minvl_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvl_subShapeRef });

        cb_s2_vl_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s2_vl = new();
        TableRow s2_vl_r = new();
        s2_vl.Rows.Add(s2_vl_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_vl)});
        
        s2_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s2_vl_final)});

        TableRow s2_vl_r1 = new();
        s2_vl.Rows.Add(s2_vl_r1);

        btn_s2_vl = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_vl.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinVLRef, 2);            
        };
        s2_vl_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_vl)});

        num_layer_subshape3_minvl = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_minvl) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pVerLengthIncrementUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        Panel p = new() {Content = tl};

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Vertical Length Increment", Content = g_tl};

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

        comboBox_s0_minvlinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s0_minvlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvlinc_ref });

        comboBox_s0_minvlinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s0_minvlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLIncRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvlinc_subShapeRef });

        TableLayout s0_vl = new();
        TableRow s0_vl_r = new();
        s0_vl.Rows.Add(s0_vl_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_vl)});
        
        btn_s0_vlinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_vlinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VLIncRef);            
        };
        s0_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_vlinc)});

        num_layer_subshape_incVL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_incVL) });

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

        comboBox_s1_minvlinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s1_minvlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvlinc_ref });

        comboBox_s1_minvlinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s1_minvlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLIncRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvlinc_subShapeRef });

        TableLayout s1_vl = new();
        TableRow s1_vl_r = new();
        s1_vl.Rows.Add(s1_vl_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_vl)});
        
        btn_s1_vlinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_vlinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VLIncRef, 1);
        };
        s1_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_vlinc)});

        num_layer_subshape2_incVL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_incVL) });

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

        comboBox_s2_minvlinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s2_minvlinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvlinc_ref });

        comboBox_s2_minvlinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s2_minvlinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLIncRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvlinc_subShapeRef });

        TableLayout s2_vl = new();
        TableRow s2_vl_r = new();
        s2_vl.Rows.Add(s2_vl_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_vl)});
        
        btn_s2_vlinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_vlinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VLIncRef, 2);
        };
        s2_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_vlinc)});

        num_layer_subshape3_incVL = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_incVL) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pVerLengthStepsUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        Panel p = new() {Content = tl};

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Vertical Length Steps", Content = g_tl};

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

        comboBox_s0_minvlsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s0_minvlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvlsteps_ref });

        comboBox_s0_minvlsteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s0_minvlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLStepsRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvlsteps_subShapeRef });

        TableLayout s0_vl = new();
        TableRow s0_vl_r = new();
        s0_vl.Rows.Add(s0_vl_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_vl)});
        
        btn_s0_vlst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_vlst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VLStepsRef);
        };
        s0_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_vlst)});

        num_layer_subshape_stepsVL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_stepsVL) });

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

        comboBox_s1_minvlsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s1_minvlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvlsteps_ref });

        comboBox_s1_minvlsteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s1_minvlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLStepsRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvlsteps_subShapeRef });

        TableLayout s1_vl = new();
        TableRow s1_vl_r = new();
        s1_vl.Rows.Add(s1_vl_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_vl)});
        
        btn_s1_vlst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_vlst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VLStepsRef, 1);
        };
        s1_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_vlst)});

        num_layer_subshape2_stepsVL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_stepsVL) });

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

        comboBox_s2_minvlsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s2_minvlsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvlsteps_ref });

        comboBox_s2_minvlsteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s2_minvlsteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVLStepsRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvlsteps_subShapeRef });

        TableLayout s2_vl = new();
        TableRow s2_vl_r = new();
        s2_vl.Rows.Add(s2_vl_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_vl)});
        
        btn_s2_vlst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_vlst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VLStepsRef, 2);
        };
        s2_vl_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_vlst)});

        num_layer_subshape3_stepsVL = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_stepsVL) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

}