using Eto.Forms;

namespace Quilt;

public partial class MainForm
{

    private Panel pHorTipUI()
    {
        
        TableLayout tl = new();
        GroupBox gb1 = new() {Text = "Horizontal Tips", Content = tl};
        Panel p = new() {Content = TableLayout.AutoSized(gb1)};
        tl.Rows.Add(new TableRow());

        tl.Rows[^1].Cells.Add(new TableCell { Control = pMinHorTipLengthUI() });
        tl.Rows[^1].Cells.Add(new TableCell { Control = pHorTipLengthIncrementUI() });
        tl.Rows[^1].Cells.Add(new TableCell { Control = pHorTipLengthStepsUI() });
        tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

        return p;
    }
    private Panel pMinHorTipLengthUI()
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

        TableLayout ht0 = new();
        TableRow ht0_r = new();
        ht0.Rows.Add(ht0_r);

        leftUpper.Cells.Add(new TableCell {Control = TableLayout.AutoSized(ht0)});

        comboBox_minht_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use minimum value from this pattern element"
        };
        comboBox_minht_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        ht0_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(comboBox_minht_ref)});
        
        cb_ht_final = new CheckBox
        {
            Text = "Final", ToolTip = "Use final dimension from reference, i.e. min and variation"
        };
        
        ht0_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(cb_ht_final)});

        TableLayout ht = new();
        TableRow ht_r = new();
        ht.Rows.Add(ht_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(ht)});
        
        TableRow ht_r1 = new();
        ht.Rows.Add(ht_r1);

        btn_ht = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false,
        };
        btn_ht.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.MinHTRef);
        };

        ht_r1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_ht)});
        
        num_layer_minht = new NumericStepper {Increment = 0.1, DecimalPlaces = 2, MinValue = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_minht) });

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

    private Panel pHorTipLengthIncrementUI()
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

        comboBox_minhtinc_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use increment value from this pattern element"
        };
        comboBox_minhtinc_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_minhtinc_ref) });

        TableLayout ht = new();
        TableRow ht_r = new();
        ht.Rows.Add(ht_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(ht)});
        
        btn_htinc = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_htinc.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HTIncRef);            
        };
        ht_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_htinc)});
        
        num_layer_incHT = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_incHT) });

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

    private Panel pHorTipLengthStepsUI()
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

        comboBox_minhtsteps_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use steps value from this pattern element"
        };
        comboBox_minhtsteps_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        leftUpper.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_minhtsteps_ref) });

        TableLayout ht = new();
        TableRow ht_r = new();
        ht.Rows.Add(ht_r);
        
        leftMiddle.Cells.Add(new TableCell {Control = TableLayout.AutoSized(ht)});
        
        btn_htst = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_htst.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.HTStepsRef);            
        };
        ht_r.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_htst)});

        num_layer_stepsHT = new NumericStepper {MinValue = 1, Increment = 1, DecimalPlaces = 0};

        leftLower.Cells.Add(new TableCell { Control = TableLayout.AutoSized(num_layer_stepsHT) });

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