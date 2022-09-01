using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
    private Panel pVerTipUI()
    {
        
        TableLayout tl = new();
        GroupBox gb1 = new() {Text = "Vertical Tips", Content = tl};
        Panel p = new() {Content = TableLayout.AutoSized(gb1)};
        tl.Rows.Add(new TableRow());

        tl.Rows[^1].Cells.Add(new TableCell { Control = pMinVerTipLengthUI() });
        tl.Rows[^1].Cells.Add(new TableCell { Control = pVerTipLengthIncrementUI() });
        tl.Rows[^1].Cells.Add(new TableCell { Control = pVerTipLengthStepsUI() });
        tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

        return p;
    }

    private Panel pMinVerTipLengthUI()
    {
        TableLayout tl = new();
        Panel p = new() {Content = TableLayout.AutoSized(tl)};
        tl.Rows.Add(new TableRow());

        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Minimum Tip Length", Content = g_tl};

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

        TableLayout vt0 = new();
        leftUpper.Cells.Add(new TableCell {Control = TableLayout.AutoSized(vt0)});

        TableRow vt0_r1 = new();
        vt0.Rows.Add(vt0_r1);

        comboBox_minvt_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_minvt_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        vt0_r1.Cells.Add(new TableCell { Control = comboBox_minvt_ref });
        
        cb_vt_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };
        
        vt0_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_vt_final)});

        TableLayout vt = new();
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(vt)});
        TableRow vt_r1 = new();
        vt.Rows.Add(vt_r1);

        btn_vt = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false,
        };
        btn_vt.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinVTRef);
        };

        vt_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_vt)});
        
        num_layer_minvt = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_minvt) });

        Panel pLeft = new() {Content = TableLayout.AutoSized(left)};

        g_tl.Rows[^1].Cells.Add(pLeft);
            
        TableLayout middle = new();

        Panel pMiddle = new() {Content = TableLayout.AutoSized(middle)};

        g_tl.Rows[^1].Cells.Add(pMiddle);

        TableLayout right = new();

        Panel pRight = new() {Content = TableLayout.AutoSized(right)};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pVerTipLengthIncrementUI()
    {
        TableLayout tl = new();
        tl.Rows.Add(new TableRow());
            
        Panel p = new() {Content = TableLayout.AutoSized(tl)};
            
        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Increment", Content = g_tl};

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

        comboBox_minvtinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_minvtinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_minvtinc_ref });

        TableLayout vt = new();
        TableRow vt_r = new();
        vt.Rows.Add(vt_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(vt)});
        
        btn_vtinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_vtinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VTIncRef);
        };
        vt_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_vtinc)});
        
        num_layer_incVT = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_incVT) });

        Panel pLeft = new() {Content = TableLayout.AutoSized(left)};

        g_tl.Rows[^1].Cells.Add(pLeft);
            
        TableLayout middle = new();
        Panel pMiddle = new() {Content = TableLayout.AutoSized(middle)};

        g_tl.Rows[^1].Cells.Add(pMiddle);

        TableLayout right = new();
        Panel pRight = new() {Content = TableLayout.AutoSized(right)};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

    private Panel pVerTipLengthStepsUI()
    {
        TableLayout tl = new();
        Panel p = new() {Content = tl};
        tl.Rows.Add(new TableRow());
            
        TableLayout g_tl = new();
        GroupBox gb1 = new() {Text = "Steps", Content = g_tl};

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

        comboBox_minvtsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_minvtsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = comboBox_minvtsteps_ref });

        TableLayout vt = new();
        TableRow vt_r = new();
        vt.Rows.Add(vt_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(vt)});
        
        btn_vtst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_vtst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.VTStepsRef);
        };
        vt_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_vtst)});

        num_layer_stepsVT = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_stepsVT) });

        Panel pLeft = new() {Content = left};

        g_tl.Rows[^1].Cells.Add(pLeft);
            
        TableLayout middle = new();
        Panel pMiddle = new() {Content = middle};

        g_tl.Rows[^1].Cells.Add(pMiddle);

        TableLayout right = new();
        Panel pRight = new() {Content = right};

        g_tl.Rows[^1].Cells.Add(pRight);

        return p;
    }

}