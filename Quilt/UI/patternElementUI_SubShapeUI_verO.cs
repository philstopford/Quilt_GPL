using System;
using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
        private Panel pMinVerOffsetUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        Panel p = new() {Content = tl};

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Minimum Vertical Offset", Content = g_tl};

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

        comboBox_s0_minvo_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s0_minvo_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvo_ref });

        comboBox_s0_minvo_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s0_minvo_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVORefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvo_subShapeRef });

        cb_s0_vo_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };
        TableLayout s0_vo = new();
        TableRow s0_vo_r = new();
        s0_vo.Rows.Add(s0_vo_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_vo)});
        
        s0_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s0_vo_final)});

        TableRow s0_vo_r1 = new();
        s0_vo.Rows.Add(s0_vo_r1);

        btn_s0_vo = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_vo.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinVORef);
        };
        s0_vo_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_vo)});

        num_layer_subshape_minvo = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_minvo) });

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

        comboBox_s1_minvo_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s1_minvo_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvo_ref });

        comboBox_s1_minvo_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s1_minvo_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVORefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvo_subShapeRef });

        cb_s1_vo_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s1_vo = new();
        TableRow s1_vo_r = new();
        s1_vo.Rows.Add(s1_vo_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_vo)});
        
        s1_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s1_vo_final)});

        TableRow s1_vo_r1 = new();
        s1_vo.Rows.Add(s1_vo_r1);

        btn_s1_vo = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_vo.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinVORef, 1);
        };
        s1_vo_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_vo)});

        num_layer_subshape2_minvo = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_minvo) });

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

        comboBox_s2_minvo_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_s2_minvo_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvo_ref });

        comboBox_s2_minvo_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this subshape"
        };
        comboBox_s2_minvo_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVORefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvo_subShapeRef });

        cb_s2_vo_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };

        TableLayout s2_vo = new();
        TableRow s2_vo_r = new();
        s2_vo.Rows.Add(s2_vo_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_vo)});
        
        s2_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_s2_vo_final)});

        TableRow s2_vo_r1 = new();
        s2_vo.Rows.Add(s2_vo_r1);

        btn_s2_vo = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_vo.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinVORef, 2);
        };
        s2_vo_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_vo)});

        num_layer_subshape3_minvo = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_minvo) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pVerOffsetIncrementUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        Panel p = new() {Content = tl};

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Vertical Offset Increment", Content = g_tl};

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

        comboBox_s0_minvoinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s0_minvoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvoinc_ref });

        comboBox_s0_minvoinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s0_minvoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOIncRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvoinc_subShapeRef });

        TableLayout s0_vo = new();
        TableRow s0_vo_r = new();
        s0_vo.Rows.Add(s0_vo_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_vo)});
        
        btn_s0_voinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_voinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VOIncRef);
        };
        s0_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_voinc)});

        num_layer_subshape_incVO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_incVO) });

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

        comboBox_s1_minvoinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s1_minvoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvoinc_ref });

        comboBox_s1_minvoinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s1_minvoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOIncRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvoinc_subShapeRef });

        TableLayout s1_vo = new();
        TableRow s1_vo_r = new();
        s1_vo.Rows.Add(s1_vo_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_vo)});
        
        btn_s1_voinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_voinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VOIncRef, 1);
        };
        s1_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_voinc)});

        num_layer_subshape2_incVO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_incVO) });

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

        comboBox_s2_minvoinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_s2_minvoinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvoinc_ref });

        comboBox_s2_minvoinc_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this subshape"
        };
        comboBox_s2_minvoinc_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOIncRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvoinc_subShapeRef });

        TableLayout s2_vo = new();
        TableRow s2_vo_r = new();
        s2_vo.Rows.Add(s2_vo_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_vo)});
        
        btn_s2_voinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_voinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VOIncRef, 2);
        };
        s2_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_voinc)});

        num_layer_subshape3_incVO = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_incVO) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pVerOffsetStepsUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
        Panel p = new() {Content = tl};

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Vertical Offset Steps", Content = g_tl};

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

        comboBox_s0_minvosteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s0_minvosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvosteps_ref });

        comboBox_s0_minvosteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s0_minvosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOStepsRefSubShapeList);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_s0_minvosteps_subShapeRef });

        TableLayout s0_vo = new();
        TableRow s0_vo_r = new();
        s0_vo.Rows.Add(s0_vo_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s0_vo)});
        
        btn_s0_vost = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s0_vost.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.verOffsetSteps);
        };
        s0_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_vost)});

        num_layer_subshape_stepsVO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape_stepsVO) });

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

        comboBox_s1_minvosteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s1_minvosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvosteps_ref });

        comboBox_s1_minvosteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s1_minvosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOStepsRefSubShape2List);

        middleUpper.Cells.Add(new TableCell { Control = comboBox_s1_minvosteps_subShapeRef });

        TableLayout s1_vo = new();
        TableRow s1_vo_r = new();
        s1_vo.Rows.Add(s1_vo_r);
        
        middleMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s1_vo)});
        
        btn_s1_vost = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s1_vost.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.verOffsetSteps, 1);
        };
        s1_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_vost)});

        num_layer_subshape2_stepsVO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        middleLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape2_stepsVO) });

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

        comboBox_s2_minvosteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_s2_minvosteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvosteps_ref });

        comboBox_s2_minvosteps_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this subshape"
        };
        comboBox_s2_minvosteps_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.minVOStepsRefSubShape3List);

        rightUpper.Cells.Add(new TableCell { Control = comboBox_s2_minvosteps_subShapeRef });

        TableLayout s2_vo = new();
        TableRow s2_vo_r = new();
        s2_vo.Rows.Add(s2_vo_r);
        
        rightMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(s2_vo)});
        
        btn_s2_vost = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_s2_vost.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.verOffsetSteps, 2);
        };
        s2_vo_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_vost)});
        
        num_layer_subshape3_stepsVO = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        rightLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_subshape3_stepsVO) });

        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

}