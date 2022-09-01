using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
    private void pSubShapesTableLayout(TableCell tc)
    {
        Application.Instance.Invoke(() =>
        {
            // groupBox_subShapes.Size = new Size(310, 150);
            groupBox_subShapes_table = new TableLayout();
            groupBox_properties.Content = groupBox_subShapes_table;
            groupBox_properties.Text = "SubShapes";
            tc.Control = groupBox_properties;

            TabControl subshapes_tabcontrol = new();
            TabPage subshapes_dimension = new() {Text =  "Dimensions"};
            TableLayout dimensions_tl = new();
            subshapes_dimension.Content = dimensions_tl;
            TabPage subshapes_offset = new() {Text =  "Offsets"};
            TableLayout offsets_tl = new();
            subshapes_offset.Content = offsets_tl;
            TabPage subshapes_tips = new() {Text =  "Tips"};
            TableLayout tips_tl = new();
            subshapes_tips.Content = tips_tl;
            subshapes_tabcontrol.Pages.Add(subshapes_dimension);
            subshapes_tabcontrol.Pages.Add(subshapes_offset);
            subshapes_tabcontrol.Pages.Add(subshapes_tips);
            Expander exp = new Expander()
            {
                Content = subshapes_tabcontrol,
                Expanded = quiltContext.expandUI,
                Header = ""
            };

            groupBox_subShapes_table.Rows.Add(new TableRow());

            groupBox_subShapes_table.Rows[^1].Cells.Add(new TableCell { Control = exp });

            dimensions_tl.Rows.Add(new TableRow());
                
            dimensions_tl.Rows[^1].Cells.Add(new TableCell { Control = pMinHorLengthUI() });

            dimensions_tl.Rows.Add(new TableRow());

            dimensions_tl.Rows[^1].Cells.Add(new TableCell { Control = pHorLengthIncrementUI() });

            dimensions_tl.Rows.Add(new TableRow());

            dimensions_tl.Rows[^1].Cells.Add(new TableCell { Control = pHorLengthStepsUI() });

            offsets_tl.Rows.Add(new TableRow());

            offsets_tl.Rows[^1].Cells.Add(new TableCell { Control = pMinHorOffsetUI() });

            offsets_tl.Rows.Add(new TableRow());

            offsets_tl.Rows[^1].Cells.Add(new TableCell { Control = pHorOffsetIncrementUI() });

            offsets_tl.Rows.Add(new TableRow());

            offsets_tl.Rows[^1].Cells.Add(new TableCell { Control = pHorOffsetStepsUI() });

            dimensions_tl.Rows.Add(new TableRow());

            dimensions_tl.Rows[^1].Cells.Add(new TableCell { Control = pMinVerLengthUI() });

            dimensions_tl.Rows.Add(new TableRow());

            dimensions_tl.Rows[^1].Cells.Add(new TableCell { Control = pVerLengthIncrementUI() });

            dimensions_tl.Rows.Add(new TableRow());

            dimensions_tl.Rows[^1].Cells.Add(new TableCell { Control = pVerLengthStepsUI() });

            offsets_tl.Rows.Add(new TableRow());

            offsets_tl.Rows[^1].Cells.Add(new TableCell { Control = pMinVerOffsetUI() });

            offsets_tl.Rows.Add(new TableRow());

            offsets_tl.Rows[^1].Cells.Add(new TableCell { Control = pVerOffsetIncrementUI() });

            offsets_tl.Rows.Add(new TableRow());

            offsets_tl.Rows[^1].Cells.Add(new TableCell { Control = pVerOffsetStepsUI() });

            tips_tl.Rows.Add(new TableRow());

            tips_tl.Rows[^1].Cells.Add(new TableCell { Control = pTipLocationUI() });

            tips_tl.Rows.Add(new TableRow());

            tips_tl.Rows[^1].Cells.Add(new TableCell { Control = pHorTipUI() });

            tips_tl.Rows.Add(new TableRow());

            tips_tl.Rows[^1].Cells.Add(new TableCell { Control = pVerTipUI() });
            
            num_externalGeoCoordsX = new NumericStepper[1];
            num_externalGeoCoordsY = new NumericStepper[1];
        });
    }

    private Panel pTipLocationUI()
    {
        TableLayout tl = new();
        GroupBox gb1 = new() {Text = "Tip Locations", Content = tl};
        Panel p = new() {Content = TableLayout.AutoSized(gb1)};
        tl.Rows.Add(new TableRow()); // tip locations
        tl.Rows.Add(new TableRow()); // references
        tl.Rows.Add(new TableRow()); // find
        tl.Rows.Add(new TableRow()); // padding

        comboBox_tipLocations = new DropDown();

        tl.Rows[0].Cells.Add(new TableCell { Control = comboBox_tipLocations });

        comboBox_tipLocations.BindDataContext(c => c.DataStore, (UIStringLists m) => m.tipLocs);

        comboBox_tipLocations2 = new DropDown();

        tl.Rows[0].Cells.Add(new TableCell { Control = comboBox_tipLocations2 });

        comboBox_tipLocations2.BindDataContext(c => c.DataStore, (UIStringLists m) => m.tipLocs);

        comboBox_tipLocations3 = new DropDown();

        tl.Rows[0].Cells.Add(new TableCell { Control = comboBox_tipLocations3 });

        comboBox_tipLocations3.BindDataContext(c => c.DataStore, (UIStringLists m) => m.tipLocs);

        Panel s0tip = new();
        TableLayout tl_s0_tip = new();
        s0tip.Content = tl_s0_tip;
        tl_s0_tip.Rows.Add(new());
        
        comboBox_s0_tip_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use tip definition from this pattern element"
        };
        comboBox_s0_tip_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        tl_s0_tip.Rows[^1].Cells.Add(new TableCell { Control = comboBox_s0_tip_ref });

        comboBox_s0_tip_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use tip definition from this subshape"
        };
        comboBox_s0_tip_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.tipRefSubShapeList);

        tl_s0_tip.Rows[^1].Cells.Add(new() {Control = comboBox_s0_tip_subShapeRef});
        tl.Rows[1].Cells.Add(new TableCell { Control = s0tip });

        btn_s0_tip = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false,
        };
        btn_s0_tip.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.tipRef);
        };

        tl.Rows[2].Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s0_tip)});
        
        Panel s1tip = new();
        TableLayout tl_s1_tip = new();
        s1tip.Content = tl_s1_tip;
        tl_s1_tip.Rows.Add(new());
        
        comboBox_s1_tip_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use tip definition from this pattern element"
        };
        comboBox_s1_tip_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        tl_s1_tip.Rows[^1].Cells.Add(new TableCell { Control = comboBox_s1_tip_ref });

        comboBox_s1_tip_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use tip definition from this subshape"
        };
        comboBox_s1_tip_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.tipRefSubShape2List);

        tl_s1_tip.Rows[^1].Cells.Add(new() {Control = comboBox_s1_tip_subShapeRef});
        tl.Rows[1].Cells.Add(new TableCell { Control = s1tip });

        btn_s1_tip = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false,
        };
        btn_s1_tip.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.tipRef, 1);
        };

        tl.Rows[2].Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s1_tip)});
        
        Panel s2tip = new();
        TableLayout tl_s2_tip = new();
        s2tip.Content = tl_s2_tip;
        tl_s2_tip.Rows.Add(new());
        
        comboBox_s2_tip_ref = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use tip definition from this pattern element"
        };
        comboBox_s2_tip_ref.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        tl_s2_tip.Rows[^1].Cells.Add(new TableCell { Control = comboBox_s2_tip_ref });

        comboBox_s2_tip_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Use tip definition from this subshape"
        };
        comboBox_s2_tip_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.tipRefSubShape3List);

        tl_s2_tip.Rows[^1].Cells.Add(new() {Control = comboBox_s2_tip_subShapeRef});
        tl.Rows[1].Cells.Add(new TableCell { Control = s2tip });

        btn_s2_tip = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false,
        };
        btn_s2_tip.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.tipRef, 2);
        };

        tl.Rows[2].Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_s2_tip)});
        
        return p;
    }


}