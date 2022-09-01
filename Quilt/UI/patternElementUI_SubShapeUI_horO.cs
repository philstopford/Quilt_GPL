using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
        private Panel pMinHorOffsetUI()
    {
        TableLayout tl = new();
        Panel p = new() {Content = tl};
        tl.Rows.Add(new TableRow());
            
        TableLayout g_tl = new();

        GroupBox gb1 = new() {Text = "Minimum Horizontal Offset", Content = g_tl};

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

        comboBox_s0_minho_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s0_minho_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minho_ref });

        comboBox_s0_minho_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s0_minho_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHORefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minho_subShapeRef });

        cb_s0_ho_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s0_ho = new();
        TableRow s0_ho_r = new();
        s0_ho.Rows.Add(s0_ho_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_ho)});
        
        s0_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s0_ho_final)});

        TableRow s0_ho_r1 = new();
        s0_ho.Rows.Add(s0_ho_r1);

        btn_s0_ho = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_ho.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinHORef);            
        };
        s0_ho_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_ho)});
        
        num_layer_subshape_minho = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_minho) });

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

        comboBox_s1_minho_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s1_minho_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minho_ref });

        comboBox_s1_minho_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s1_minho_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHORefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minho_subShapeRef });

        cb_s1_ho_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s1_ho = new();
        TableRow s1_ho_r = new();
        s1_ho.Rows.Add(s1_ho_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_ho)});
        
        s1_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s1_ho_final)});

        TableRow s1_ho_r1 = new();
        s1_ho.Rows.Add(s1_ho_r1);

        btn_s1_ho = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_ho.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinHORef, 1);            
        };
        s1_ho_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_ho)});

        num_layer_subshape2_minho = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_minho) });

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

        comboBox_s2_minho_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s2_minho_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minho_ref });

        comboBox_s2_minho_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s2_minho_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHORefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minho_subShapeRef });

        cb_s2_ho_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s2_ho = new();
        TableRow s2_ho_r = new();
        s2_ho.Rows.Add(s2_ho_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_ho)});
        
        s2_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s2_ho_final)});

        TableRow s2_ho_r1 = new();
        s2_ho.Rows.Add(s2_ho_r1);

        btn_s2_ho = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_ho.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinHORef, 2);            
        };
        s2_ho_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_ho)});

        num_layer_subshape3_minho = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_minho) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pHorOffsetIncrementUI()
    {
        TableLayout tl = new();
        Panel p = new() {Content = tl};
        tl.Rows.Add(new TableRow());
            
        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Horizontal Offset Increment", Content = g_tl};

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

        comboBox_s0_minhoinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s0_minhoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhoinc_ref });

        comboBox_s0_minhoinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s0_minhoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOIncRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhoinc_subShapeRef });

        TableLayout s0_ho = new();
        TableRow s0_ho_r = new();
        s0_ho.Rows.Add(s0_ho_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_ho)});
        
        btn_s0_hoinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_hoinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HOIncRef);            
        };
        s0_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_hoinc)});
        
        num_layer_subshape_incHO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_incHO) });

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

        comboBox_s1_minhoinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s1_minhoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhoinc_ref });

        comboBox_s1_minhoinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s1_minhoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOIncRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhoinc_subShapeRef });

        TableLayout s1_ho = new();
        TableRow s1_ho_r = new();
        s1_ho.Rows.Add(s1_ho_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_ho)});
        
        btn_s1_hoinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_hoinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HOIncRef, 1);            
        };
        s1_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_hoinc)});

        num_layer_subshape2_incHO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_incHO) });

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

        comboBox_s2_minhoinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s2_minhoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhoinc_ref });

        comboBox_s2_minhoinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s2_minhoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOIncRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhoinc_subShapeRef });

        TableLayout s2_ho = new();
        TableRow s2_ho_r = new();
        s2_ho.Rows.Add(s2_ho_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_ho)});
        
        btn_s2_hoinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_hoinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HOIncRef, 2);            
        };
        s2_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_hoinc)});

        num_layer_subshape3_incHO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_incHO) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pHorOffsetStepsUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        Panel p = new() {Content = tl};

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Horizontal Offset Steps", Content = g_tl};

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

        comboBox_s0_minhosteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s0_minhosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhosteps_ref });

        comboBox_s0_minhosteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s0_minhosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOStepsRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minhosteps_subShapeRef });

        TableLayout s0_ho = new();
        TableRow s0_ho_r = new();
        s0_ho.Rows.Add(s0_ho_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_ho)});
        
        btn_s0_host = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_host.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HOStepsRef);            
        };
        s0_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_host)});

        num_layer_subshape_stepsHO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};
        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_stepsHO) });

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

        comboBox_s1_minhosteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s1_minhosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhosteps_ref });

        comboBox_s1_minhosteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s1_minhosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOStepsRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minhosteps_subShapeRef });

        TableLayout s1_ho = new();
        TableRow s1_ho_r = new();
        s1_ho.Rows.Add(s1_ho_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_ho)});
        
        btn_s1_host = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_host.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HOStepsRef, 1);            
        };
        s1_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_host)});

        num_layer_subshape2_stepsHO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_stepsHO) });

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

        comboBox_s2_minhosteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s2_minhosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhosteps_ref });

        comboBox_s2_minhosteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s2_minhosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minHOStepsRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minhosteps_subShapeRef });

        TableLayout s2_ho = new();
        TableRow s2_ho_r = new();
        s2_ho.Rows.Add(s2_ho_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_ho)});
        
        btn_s2_host = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_host.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HOStepsRef, 2);            
        };
        s2_ho_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_host)});

        num_layer_subshape3_stepsHO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_stepsHO) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

}