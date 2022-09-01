using Eto.Forms;
using shapeEngine;

namespace Quilt;

public partial class MainForm
{
    private void pSubShapeRef_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_subShapeRef = new Label
        {
            Text = "Subshape Reference",
            ToolTip = "Which subshape to use for placement with respect to the world origin"
        };

        tr.Cells.Add(new TableCell { Control = lbl_subShapeRef });

        comboBox_subShapeRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Which subshape to use for placement with respect to the world origin"
        };
        comboBox_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeList);

        tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_subShapeRef) });

        tr.Cells.Add(new TableCell { Control = null });
    }

    private void pSubShapePos_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_posSubShape = new Label
        {
            Text = "Subshape Position",
            ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
        };

        tr.Cells.Add(new TableCell { Control = lbl_posSubShape });

        comboBox_posSubShape = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
        };
        comboBox_posSubShape.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapePos);

        tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_posSubShape) });

        tr.Cells.Add(new TableCell { Control = null });
    }

    private void pSubShapeXPos_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_xPosRef = new Label
        {
            Text = "X Position",
            ToolTip = "Position this element in X relative to a different element, or world origin"
        };
        tr.Cells.Add(new TableCell { Control = lbl_xPosRef });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        ptl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p });

        num_minXPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_minXPos, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_minXPos });

        num_incXPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_incXPos, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_incXPos });

        num_stepsXPos = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
        pSetSize(num_stepsXPos, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_stepsXPos });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });

    }

    private void pSubShapeXRelPos_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        Label lbl_relXPos = new() {Text = "Relative Position", ToolTip = "Relative Positioning"};

        tr.Cells.Add(new TableCell { Control = lbl_relXPos });

        comboBox_xPosRef = new DropDown
        {
            DataContext = DataContext, ToolTip = "Position in X relative to this pattern element"
        };
        comboBox_xPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);

        tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_xPosRef) });

        TableRow tr1 = new();
        tl.Rows.Add(tr1);
        
        tr1.Cells.Add(new TableCell { Control = null });
        btn_posXRef = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_posXRef.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.xPosRef);
        };
        tr1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_posXRef)});
        
        tr.Cells.Add(new TableCell { Control = null });
        tr1.Cells.Add(new TableCell { Control = null });
    }

    private void pSubShapeXRelPosSS_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        Label lbl_subShapeXPos = new() {Text = "Subshape Reference", ToolTip = "Reference subshape"};
        tr.Cells.Add(new TableCell { Control = lbl_subShapeXPos });

        Panel p = new();
        TableLayout ptl = new();
        p.Content = ptl;
        ptl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p });

        comboBox_xPos_subShapeRef = new DropDown
        {
            DataContext = DataContext, SelectedIndex = 0, ToolTip = "Subshape reference"
        };
        comboBox_xPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.xPosRefSubShapeList);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_xPos_subShapeRef) });

        comboBox_xPos_subShapeRefPos = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = (int) ShapeSettings.subShapeHorLocs.L,
            ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
        };
        comboBox_xPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeHorPos);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_xPos_subShapeRefPos) });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });
    }

    private void pSubShapeYPos_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_yPosRef = new Label
        {
            Text = "Y Position",
            ToolTip = "Position this element in Y relative to a different element, or world origin"
        };

        tr.Cells.Add(new TableCell { Control = lbl_yPosRef });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        ptl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p });

        num_minYPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_minYPos, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_minYPos });

        num_incYPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_incYPos, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_incYPos });

        num_stepsYPos = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
        pSetSize(num_stepsYPos, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_stepsYPos });
            
        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });
    }

    private void pSubShapeYRelPos_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        Label lbl_relYPos = new() {Text = "Relative Position", ToolTip = "Relative Positioning"};

        tr.Cells.Add(new TableCell { Control = lbl_relYPos });

        comboBox_yPosRef = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = 0,
            ToolTip = "Position in Y relative to this pattern element"
        };
        comboBox_yPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);

        tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_yPosRef) });

        TableRow tr1 = new();
        tl.Rows.Add(tr1);
        
        tr1.Cells.Add(new TableCell { Control = null });
        btn_posYRef = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_posYRef.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.yPosRef);
        };
        tr1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_posYRef)});
        
        tr.Cells.Add(new TableCell { Control = null });
        tr1.Cells.Add(new TableCell { Control = null });
    }

    private void pSubShapeYRelPosSS_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        Label lbl_subShapeYPos = new() {Text = "Subshape Reference", ToolTip = "Reference subshape"};
        tr.Cells.Add(new TableCell { Control = lbl_subShapeYPos });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        TableRow ptl_tr = new();
        ptl.Rows.Add(ptl_tr);

        tr.Cells.Add(new TableCell { Control = p });

        comboBox_yPos_subShapeRef = new DropDown
        {
            DataContext = DataContext, SelectedIndex = 0, ToolTip = "Subshape reference"
        };
        comboBox_yPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.yPosRefSubShapeList);

        ptl_tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_yPos_subShapeRef) });

        comboBox_yPos_subShapeRefPos = new DropDown
        {
            DataContext = DataContext,
            SelectedIndex = (int) ShapeSettings.subShapeVerLocs.B,
            ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
        };
        comboBox_yPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeVerPos);

        ptl_tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_yPos_subShapeRefPos) });

        ptl_tr.Cells.Add(new TableCell { Control = null });
    }

    private void pRot_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_rotation = new Label {Text = "Rotation", ToolTip = "Rotation"};

        tr.Cells.Add(new TableCell { Control = lbl_rotation });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        ptl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p });

        num_minRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_minRot, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_minRot });

        num_incRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_incRot, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_incRot });

        num_stepsRot = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
        pSetSize(num_stepsRot, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_stepsRot });
            
        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });
    }

    private void pRelRot_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        Label lbl_relRot = new() {Text = "Relative Rotation", ToolTip = "Relative Rotation"};

        tr.Cells.Add(new TableCell { Control = lbl_relRot });

        comboBox_rotRef = new DropDown
        {
            DataContext = DataContext, SelectedIndex = 0, ToolTip = "Rotation relative to this pattern element"
        };
        comboBox_rotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        ptl.Rows.Add(new TableRow());
        ptl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_rotRef) });
        tr.Cells.Add(new TableCell { Control = p });

        TableRow tr1 = new();
        tl.Rows.Add(tr1);
        
        tr1.Cells.Add(new TableCell { Control = null });
        btn_rotRef = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_rotRef.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.rotationRef);
        };
        tr1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_rotRef)});
        
        // tr.Cells.Add(new TableCell { Control = null });
        tr1.Cells.Add(new TableCell { Control = null });

        ptl.Rows.Add(new TableRow());

        TableLayout tl0 = new();
        ptl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(tl0) });

        tl0.Rows.Add(new TableRow());

        lbl_use = new Label {Text = "Use"};
        tl0.Rows[^1].Cells.Add(new TableCell { Control = lbl_use });

        checkBox_rotRef = new CheckBox
        {
            Text = "Array", Enabled = false, ToolTip = "Use array rotation rather than shape."
        };

        tl0.Rows[^1].Cells.Add(new TableCell { Control = checkBox_rotRef });
        
        checkBox_refBoundsAfterRotation = new CheckBox
        {
            Text = "Bounds after rotation",
            Enabled = false,
            ToolTip = "Perform rotation before bounding box. This affects the pivot."
        };

        tl0.Rows[^1].Cells.Add(new TableCell { Control = checkBox_refBoundsAfterRotation });
        
        ptl.Rows.Add(new TableRow());

        Panel pnl_rotPivot = new();
        TableLayout tl_rotPivot = new();
        pnl_rotPivot.Content = tl_rotPivot;
        tl_rotPivot.Rows.Add(new());
        Label lbl_usePivot = new Label {Text = "Pivot"};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = lbl_usePivot });
        rB_rotPivot_self = new() {Text = "Self", Checked = true};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = rB_rotPivot_self });
        rB_rotPivot_ref = new(rB_rotPivot_self) {Text = "Reference", Checked = false};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = rB_rotPivot_ref });
        rB_rotPivot_worldOrigin = new(rB_rotPivot_self) {Text = "World Origin", Checked = false};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = rB_rotPivot_worldOrigin });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = pnl_rotPivot });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

    }

    private void pFlip_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_flip = new Label {Text = "Flip", ToolTip = "Flip"};

        tr.Cells.Add(new TableCell { Control = lbl_flip });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        TableRow ptl_tr = new();
        ptl.Rows.Add(ptl_tr);
        tr.Cells.Add(new TableCell { Control = p });

        checkBox_flipH = new CheckBox {Text = "H", ToolTip = "Flip horizontally"};

        ptl_tr.Cells.Add(new TableCell { Control = checkBox_flipH });

        checkBox_flipV = new CheckBox {Text = "V", ToolTip = "Flip vertically"};

        ptl_tr.Cells.Add(new TableCell { Control = checkBox_flipV });

        checkBox_alignX = new CheckBox {Text = "Align X", ToolTip = "Align flipped shape with original in X"};

        ptl_tr.Cells.Add(new TableCell { Control = checkBox_alignX });

        checkBox_alignY = new CheckBox {Text = "Align Y", ToolTip = "Align flipped shape with original in Y"};

        ptl_tr.Cells.Add(new TableCell { Control = checkBox_alignY });
    }

    private void pArray_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        Label lbl_relArray = new() {Text = "Relative Array Definition", ToolTip = "Relative Array Definition"};

        tr.Cells.Add(new TableCell { Control = lbl_relArray });

        comboBox_arrayRef = new DropDown
        {
            DataContext = DataContext, SelectedIndex = 0, ToolTip = "Take array definition from this element"
        };
        comboBox_arrayRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered_array);

        tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_arrayRef) });

        TableRow tr1 = new();
        tl.Rows.Add(tr1);
        
        tr1.Cells.Add(new TableCell { Control = null });
        btn_arrayRef = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_arrayRef.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.arrayRef);
        };
        tr1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_arrayRef)});
        
        tr.Cells.Add(new TableCell { Control = null });
        tr1.Cells.Add(new TableCell { Control = null });
        
        pArrayX_UI(tl);
        pArrayY_UI(tl);
        pArrayRot_UI(tl);
    }

    private void pArrayX_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_arrayX = new Label {Text = "Array X Count", ToolTip = "Array X"};

        tr.Cells.Add(new TableCell { Control = lbl_arrayX });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        ptl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p });

        num_arrayMinXCount = new NumericStepper
        {
            Value = 1,
            MinValue = 1,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array X Count"
        };
        pSetSize(num_arrayMinXCount, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayMinXCount });

        num_arrayXInc = new NumericStepper
        {
            Value = 0,
            MinValue = 0,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array X Increment"
        };
        pSetSize(num_arrayXInc, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayXInc });

        num_arrayXSteps = new NumericStepper
        {
            Value = 1,
            MinValue = 1,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array X Steps"
        };
        pSetSize(num_arrayXSteps, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayXSteps });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });

        pArrayXSpace_UI(tl);
    }

    private void pArrayXSpace_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_arrayX = new Label {Text = "Array X Space", ToolTip = "Array X Space"};
        tr.Cells.Add(new TableCell { Control = lbl_arrayX });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        ptl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p });

        num_arrayMinXSpace = new NumericStepper
        {
            Value = 0,
            MinValue = 0,
            Increment = 0.01f,
            DecimalPlaces = 2,
            ToolTip = "Array X Space"
        };
        pSetSize(num_arrayMinXSpace, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayMinXSpace });

        num_arrayXSpaceInc = new NumericStepper
        {
            Value = 0,
            MinValue = 0,
            Increment = 0.01f,
            DecimalPlaces = 2,
            ToolTip = "Array X Space Increment"
        };
        pSetSize(num_arrayXSpaceInc, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayXSpaceInc });

        num_arrayXSpaceSteps = new NumericStepper
        {
            Value = 1,
            MinValue = 1,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array X Space Steps"
        };
        pSetSize(num_arrayXSpaceSteps, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayXSpaceSteps });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });
    }

    private void pArrayY_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_arrayY = new Label {Text = "Array Y Count", ToolTip = "Array Y"};

        tr.Cells.Add(new TableCell { Control = lbl_arrayY });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        TableRow tr0 = new();
        ptl.Rows.Add(tr0);
        tr.Cells.Add(new TableCell { Control = p });

        num_arrayMinYCount = new NumericStepper
        {
            Value = 1,
            MinValue = 1,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array Y Count"
        };
        pSetSize(num_arrayMinYCount, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayMinYCount });

        num_arrayYInc = new NumericStepper
        {
            Value = 0,
            MinValue = 0,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array Y Increment"
        };
        pSetSize(num_arrayYInc, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayYInc });

        num_arrayYSteps = new NumericStepper
        {
            Value = 1,
            MinValue = 1,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array Y Steps"
        };
        pSetSize(num_arrayYSteps, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayYSteps });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });

        pArrayYSpace_UI(tl);
    }

    private void pArrayYSpace_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        lbl_arrayY = new Label {Text = "Array Y Space", ToolTip = "Array Y Space"};
        tr.Cells.Add(new TableCell { Control = lbl_arrayY });

        TableLayout ptl = new();
        Panel p = new() {Content = ptl};
        TableRow tr0 = new();
        ptl.Rows.Add(tr0);
        tr.Cells.Add(new TableCell { Control = p });

        num_arrayMinYSpace = new NumericStepper
        {
            Value = 0,
            MinValue = 0,
            Increment = 0.01f,
            DecimalPlaces = 2,
            ToolTip = "Array Y Space"
        };
        pSetSize(num_arrayMinYSpace, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayMinYSpace });

        num_arrayYSpaceInc = new NumericStepper
        {
            Value = 0,
            MinValue = 0,
            Increment = 0.01f,
            DecimalPlaces = 2,
            ToolTip = "Array Y Space Increment"
        };
        pSetSize(num_arrayYSpaceInc, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayYSpaceInc });

        num_arrayYSpaceSteps = new NumericStepper
        {
            Value = 1,
            MinValue = 1,
            Increment = 1,
            DecimalPlaces = 0,
            ToolTip = "Array Y Space Steps"
        };
        pSetSize(num_arrayYSpaceSteps, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_arrayYSpaceSteps });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });
    }

    private TableRow pArrayRotationUI_1_UI()
    {
        TableRow tr = new();
        lbl_arrayRotation = new Label {Text = "Array Rotation", ToolTip = "Array Rotation"};

        tr.Cells.Add(new TableCell { Control = lbl_arrayRotation });

        TableLayout ptl = new();
        Panel p1 = new() {Content = ptl};
        ptl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p1 });

        num_minArrayRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_minArrayRot, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_minArrayRot });

        num_incArrayRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
        pSetSize(num_incArrayRot, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_incArrayRot });

        num_stepsArrayRot = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
        pSetSize(num_stepsArrayRot, numWidth, num_Height);

        ptl.Rows[^1].Cells.Add(new TableCell { Control = num_stepsArrayRot });

        ptl.Rows[^1].Cells.Add(new TableCell { Control = null });
        return tr;
    }

    private TableRow pArrayRotationUI_2_UI()
    {
        TableRow tr = new();

        Label lbl_relArrayRot = new() {Text = "Relative Array Rotation", ToolTip = "Relative Array Rotation"};

        tr.Cells.Add(new TableCell { Control = lbl_relArrayRot });

        TableLayout tl = new();
        Panel p = new() {Content = tl};
        tl.Rows.Add(new TableRow());
        tr.Cells.Add(new TableCell { Control = p });

        comboBox_arrayRotRef = new DropDown
        {
            DataContext = DataContext, SelectedIndex = 0, ToolTip = "Rotation relative to this pattern element"
        };
        comboBox_arrayRotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        tl.Rows[^1].Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_arrayRotRef) });

        TableRow tr1 = new();
        tl.Rows.Add(tr1);

        TableLayout tr1_tl = new();
        tr1_tl.Rows.Add(new TableRow());

        tr1.Cells.Add(new TableCell { Control = TableLayout.AutoSized(tr1_tl) });

        lbl_arrayUse = new Label {Text = "Use"};
        tr1_tl.Rows[^1].Cells.Add(new TableCell { Control = lbl_arrayUse });
        
        checkBox_arrayRotRef = new CheckBox
        {
            Text = "Array", Enabled = false, ToolTip = "Use array rotation rather than shape."
        };

        tr1_tl.Rows[^1].Cells.Add(new TableCell { Control = checkBox_arrayRotRef });
        
        checkBox_refArrayBoundsAfterRotation = new CheckBox
        {
            Text = "Bounds after rotation",
            Enabled = false,
            ToolTip = "Perform rotation before bounding box. This affects the pivot."
        };

        tr1_tl.Rows[^1].Cells.Add(new TableCell { Control = checkBox_refArrayBoundsAfterRotation });

        tl.Rows.Add(new TableRow());

        Panel pnl_rotPivot = new();
        TableLayout tl_rotPivot = new();
        pnl_rotPivot.Content = tl_rotPivot;
        tl_rotPivot.Rows.Add(new());
        Label lbl_usePivot = new Label {Text = "Pivot"};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = lbl_usePivot });
        rB_arrayRotPivot_self = new() {Text = "Self", Checked = true};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = rB_arrayRotPivot_self });
        rB_arrayRotPivot_ref = new(rB_arrayRotPivot_self) {Text = "Reference", Checked = false};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = rB_arrayRotPivot_ref });
        rB_arrayRotPivot_worldOrigin = new(rB_arrayRotPivot_self) {Text = "World Origin", Checked = false};
        tl_rotPivot.Rows[^1].Cells.Add(new TableCell { Control = rB_arrayRotPivot_worldOrigin });

        tl.Rows[^1].Cells.Add(new TableCell { Control = pnl_rotPivot });

        tl.Rows[^1].Cells.Add(new TableCell { Control = null, ScaleWidth = true });

        return tr;
    }

    private void pArrayRot_UI(TableLayout tl)
    {
        tl.Rows.Add(pArrayRotationUI_1_UI());
        
        tl.Rows.Add(pArrayRotationUI_2_UI());

        TableRow tr1 = new();
        tl.Rows.Add(tr1);
        
        tr1.Cells.Add(new TableCell { Control = null });
        btn_arrayRotRef = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_arrayRotRef.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.arrayRotationRef);
        };
        tr1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_arrayRotRef)});
        
        tl.Rows[^1].Cells.Add(new TableCell { Control = null });
        tr1.Cells.Add(new TableCell { Control = null });

        tl.Rows.Add(new TableRow { ScaleHeight = true });
    }

    private void pMerge_UI(TableLayout tl)
    {
        TableRow tr = new();
        tl.Rows.Add(tr);

        Label lbl_merge = new() {Text = "Merge", ToolTip = "Merge"};

        tr.Cells.Add(new TableCell { Control = lbl_merge });

        comboBox_merge = new DropDown
        {
            DataContext = DataContext, SelectedIndex = 0, ToolTip = "Union with this element on export"
        };
        comboBox_merge.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

        tr.Cells.Add(new TableCell { Control = TableLayout.AutoSized(comboBox_merge) });

        TableRow tr1 = new();
        tl.Rows.Add(tr1);
        
        tr1.Cells.Add(new TableCell { Control = null });
        btn_mergeRef = new ()
        {
            Text = "Find", ToolTip = "Select reference for this property",
            Enabled = false
        };
        btn_mergeRef.Click += delegate
        {
            pSelectReferenceElement(PatternElement.properties_i.linkedElementIndex);
        };
        tr1.Cells.Add(new TableCell {Control = TableLayout.AutoSized(btn_mergeRef)});
        
        tl.Rows[^1].Cells.Add(new TableCell { Control = null });
        tr1.Cells.Add(new TableCell { Control = null });

        tr.Cells.Add(new TableCell { Control = null });
    }
}