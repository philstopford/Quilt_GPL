using Eto.Forms;

namespace Quilt
{
    public partial class MainForm
    {
        void pSubShapeRef_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_subShapeRef = new Label
            {
                Text = "Subshape Reference",
                ToolTip = "Which subshape to use for placement with respect to the world origin"
            };

            tr.Cells.Add(new TableCell() { Control = lbl_subShapeRef });

            comboBox_subShapeRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Which subshape to use for placement with respect to the world origin"
            };
            comboBox_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeList);

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_subShapeRef) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void pSubShapePos_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_posSubShape = new Label
            {
                Text = "Subshape Position",
                ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
            };

            tr.Cells.Add(new TableCell() { Control = lbl_posSubShape });

            comboBox_posSubShape = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
            };
            comboBox_posSubShape.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapePos);

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_posSubShape) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void pSubShapeXPos_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_xPosRef = new Label
            {
                Text = "X Pos",
                ToolTip = "Position this element in X relative to a different element, or world origin"
            };
            tr.Cells.Add(new TableCell() { Control = lbl_xPosRef });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_minXPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_minXPos, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_minXPos });

            num_incXPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_incXPos, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_incXPos });

            num_stepsXPos = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_stepsXPos, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_stepsXPos });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });

        }

        void pSubShapeXRelPos_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relXPos = new Label {Text = "Relative Position", ToolTip = "Relative Positioning"};

            tr.Cells.Add(new TableCell() { Control = lbl_relXPos });

            comboBox_xPosRef = new DropDown
            {
                DataContext = DataContext, ToolTip = "Position in X relative to this pattern element"
            };
            comboBox_xPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_xPosRef) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void pSubShapeXRelPosSS_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_subShapeXPos = new Label {Text = "Subshape Ref", ToolTip = "Reference subshape"};
            tr.Cells.Add(new TableCell() { Control = lbl_subShapeXPos });

            Panel p = new Panel();
            TableLayout ptl = new TableLayout();
            p.Content = ptl;
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            comboBox_xPos_subShapeRef = new DropDown
            {
                DataContext = DataContext, SelectedIndex = 0, ToolTip = "Subshape reference"
            };
            comboBox_xPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.xPosRefSubShapeList);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_xPos_subShapeRef) });

            comboBox_xPos_subShapeRefPos = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = (int) CommonVars.subShapeHorLocs.L,
                ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
            };
            comboBox_xPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeHorPos);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_xPos_subShapeRefPos) });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });
        }

        void pSubShapeYPos_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_yPosRef = new Label
            {
                Text = "Y Pos",
                ToolTip = "Position this element in Y relative to a different element, or world origin"
            };

            tr.Cells.Add(new TableCell() { Control = lbl_yPosRef });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_minYPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_minYPos, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_minYPos });

            num_incYPos = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_incYPos, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_incYPos });

            num_stepsYPos = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_stepsYPos, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_stepsYPos });
            
            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });
        }

        void pSubShapeYRelPos_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relYPos = new Label {Text = "Relative Position", ToolTip = "Relative Positioning"};

            tr.Cells.Add(new TableCell() { Control = lbl_relYPos });

            comboBox_yPosRef = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = 0,
                ToolTip = "Position in Y relative to this pattern element"
            };
            comboBox_yPosRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_yPosRef) });

            tr.Cells.Add(new TableCell() { Control = null });
        }

        void pSubShapeYRelPosSS_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_subShapeYPos = new Label {Text = "Subshape Ref", ToolTip = "Reference subshape"};
            tr.Cells.Add(new TableCell() { Control = lbl_subShapeYPos });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            TableRow ptl_tr = new TableRow();
            ptl.Rows.Add(ptl_tr);

            tr.Cells.Add(new TableCell() { Control = p });

            comboBox_yPos_subShapeRef = new DropDown
            {
                DataContext = DataContext, SelectedIndex = 0, ToolTip = "Subshape reference"
            };
            comboBox_yPos_subShapeRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.yPosRefSubShapeList);

            ptl_tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_yPos_subShapeRef) });

            comboBox_yPos_subShapeRefPos = new DropDown
            {
                DataContext = DataContext,
                SelectedIndex = (int) CommonVars.subShapeVerLocs.B,
                ToolTip = "Which element of the subshape to use for placement with respect to the world origin"
            };
            comboBox_yPos_subShapeRefPos.BindDataContext(c => c.DataStore, (UIStringLists m) => m.subShapeVerPos);

            ptl_tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_yPos_subShapeRefPos) });

            ptl_tr.Cells.Add(new TableCell() { Control = null });
        }

        void pRot_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_rotation = new Label {Text = "Rotation", ToolTip = "Rotation"};

            tr.Cells.Add(new TableCell() { Control = lbl_rotation });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_minRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_minRot, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_minRot });

            num_incRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_incRot, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_incRot });

            num_stepsRot = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_stepsRot, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_stepsRot });
            
            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });
        }

        void pRelRot_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relRot = new Label {Text = "Relative Rotation", ToolTip = "Relative Rotation"};

            tr.Cells.Add(new TableCell() { Control = lbl_relRot });

            comboBox_rotRef = new DropDown
            {
                DataContext = DataContext, SelectedIndex = 0, ToolTip = "Rotation relative to this pattern element"
            };
            comboBox_rotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            ptl.Rows.Add(new TableRow());
            ptl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_rotRef) });
            tr.Cells.Add(new TableCell() { Control = p });

            ptl.Rows.Add(new TableRow());

            TableLayout tl0 = new TableLayout();
            ptl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(tl0) });

            tl0.Rows.Add(new TableRow());

            lbl_use = new Label {Text = "Use"};
            tl0.Rows[^1].Cells.Add(new TableCell() { Control = lbl_use });

            checkBox_refPivot = new CheckBox
            {
                Text = "Pivot", Enabled = false, ToolTip = "Use pivot point from reference."
            };

            tl0.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_refPivot });

            checkBox_rotRef = new CheckBox
            {
                Text = "Array", Enabled = false, ToolTip = "Use array rotation rather than shape."
            };

            tl0.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_rotRef });

            ptl.Rows.Add(new TableRow());

            checkBox_refBoundsAfterRotation = new CheckBox
            {
                Text = "Bounds after rotation",
                Enabled = false,
                ToolTip = "Perform rotation before bounding box. This affects the pivot."
            };

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_refBoundsAfterRotation });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });
        }

        void pFlip_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_flip = new Label {Text = "Flip", ToolTip = "Flip"};

            tr.Cells.Add(new TableCell() { Control = lbl_flip });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            TableRow ptl_tr = new TableRow();
            ptl.Rows.Add(ptl_tr);
            tr.Cells.Add(new TableCell() { Control = p });

            checkBox_flipH = new CheckBox {Text = "H", ToolTip = "Flip horizontally"};

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_flipH });

            checkBox_flipV = new CheckBox {Text = "V", ToolTip = "Flip vertically"};

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_flipV });

            checkBox_alignX = new CheckBox {Text = "Align X", ToolTip = "Align flipped shape with original in X"};

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_alignX });

            checkBox_alignY = new CheckBox {Text = "Align Y", ToolTip = "Align flipped shape with original in Y"};

            ptl_tr.Cells.Add(new TableCell() { Control = checkBox_alignY });
        }

        void pArray_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_relArray = new Label {Text = "Relative Array Definition", ToolTip = "Relative Array Definition"};

            tr.Cells.Add(new TableCell() { Control = lbl_relArray });

            comboBox_arrayRef = new DropDown
            {
                DataContext = DataContext, SelectedIndex = 0, ToolTip = "Take array definition from this element"
            };
            comboBox_arrayRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered_array);

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_arrayRef) });

            tr.Cells.Add(new TableCell() { Control = null });
            pArrayX_UI(tl);
            pArrayY_UI(tl);
            pArrayRot_UI(tl);
        }

        void pArrayX_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayX = new Label {Text = "Array X", ToolTip = "Array X"};

            tr.Cells.Add(new TableCell() { Control = lbl_arrayX });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinXCount = new NumericStepper
            {
                Value = 1,
                MinValue = 1,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array X Count"
            };
            pSetSize(num_arrayMinXCount, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayMinXCount });

            num_arrayXInc = new NumericStepper
            {
                Value = 0,
                MinValue = 0,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array X Increment"
            };
            pSetSize(num_arrayXInc, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayXInc });

            num_arrayXSteps = new NumericStepper
            {
                Value = 1,
                MinValue = 1,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array X Steps"
            };
            pSetSize(num_arrayXSteps, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayXSteps });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });

            pArrayXSpace_UI(tl);
        }

        void pArrayXSpace_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayX = new Label {Text = "Array X Space", ToolTip = "Array X Space"};
            tr.Cells.Add(new TableCell() { Control = lbl_arrayX });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinXSpace = new NumericStepper
            {
                Value = 0,
                MinValue = 0,
                Increment = 0.01f,
                DecimalPlaces = 2,
                ToolTip = "Array X Space"
            };
            pSetSize(num_arrayMinXSpace, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayMinXSpace });

            num_arrayXSpaceInc = new NumericStepper
            {
                Value = 0,
                MinValue = 0,
                Increment = 0.01f,
                DecimalPlaces = 2,
                ToolTip = "Array X Space Increment"
            };
            pSetSize(num_arrayXSpaceInc, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayXSpaceInc });

            num_arrayXSpaceSteps = new NumericStepper
            {
                Value = 1,
                MinValue = 1,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array X Space Steps"
            };
            pSetSize(num_arrayXSpaceSteps, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayXSpaceSteps });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });
        }

        void pArrayY_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayY = new Label {Text = "Array Y", ToolTip = "Array Y"};

            tr.Cells.Add(new TableCell() { Control = lbl_arrayY });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            TableRow tr0 = new TableRow();
            ptl.Rows.Add(tr0);
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinYCount = new NumericStepper
            {
                Value = 1,
                MinValue = 1,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array Y Count"
            };
            pSetSize(num_arrayMinYCount, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayMinYCount });

            num_arrayYInc = new NumericStepper
            {
                Value = 0,
                MinValue = 0,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array Y Increment"
            };
            pSetSize(num_arrayYInc, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayYInc });

            num_arrayYSteps = new NumericStepper
            {
                Value = 1,
                MinValue = 1,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array Y Steps"
            };
            pSetSize(num_arrayYSteps, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayYSteps });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });

            pArrayYSpace_UI(tl);
        }

        void pArrayYSpace_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            lbl_arrayY = new Label {Text = "Array Y Space", ToolTip = "Array Y Space"};
            tr.Cells.Add(new TableCell() { Control = lbl_arrayY });

            TableLayout ptl = new TableLayout();
            Panel p = new Panel {Content = ptl};
            TableRow tr0 = new TableRow();
            ptl.Rows.Add(tr0);
            tr.Cells.Add(new TableCell() { Control = p });

            num_arrayMinYSpace = new NumericStepper
            {
                Value = 0,
                MinValue = 0,
                Increment = 0.01f,
                DecimalPlaces = 2,
                ToolTip = "Array Y Space"
            };
            pSetSize(num_arrayMinYSpace, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayMinYSpace });

            num_arrayYSpaceInc = new NumericStepper
            {
                Value = 0,
                MinValue = 0,
                Increment = 0.01f,
                DecimalPlaces = 2,
                ToolTip = "Array Y Space Increment"
            };
            pSetSize(num_arrayYSpaceInc, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayYSpaceInc });

            num_arrayYSpaceSteps = new NumericStepper
            {
                Value = 1,
                MinValue = 1,
                Increment = 1,
                DecimalPlaces = 0,
                ToolTip = "Array Y Space Steps"
            };
            pSetSize(num_arrayYSpaceSteps, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_arrayYSpaceSteps });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });
        }

        TableRow pArrayRotationUI_1_UI()
        {
            TableRow tr = new TableRow();
            lbl_arrayRotation = new Label {Text = "Array Rotation", ToolTip = "Array Rotation"};

            tr.Cells.Add(new TableCell() { Control = lbl_arrayRotation });

            TableLayout ptl = new TableLayout();
            Panel p1 = new Panel {Content = ptl};
            ptl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p1 });

            num_minArrayRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_minArrayRot, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_minArrayRot });

            num_incArrayRot = new NumericStepper {Increment = 0.1, DecimalPlaces = 2};
            pSetSize(num_incArrayRot, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_incArrayRot });

            num_stepsArrayRot = new NumericStepper {Increment = 1, DecimalPlaces = 0, MinValue = 1};
            pSetSize(num_stepsArrayRot, numWidth, num_Height);

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = num_stepsArrayRot });

            ptl.Rows[^1].Cells.Add(new TableCell() { Control = null });
            return tr;
        }

        TableRow pArrayRotationUI_2_UI()
        {
            TableRow tr = new TableRow();

            Label lbl_relArrayRot = new Label {Text = "Relative Array Rotation", ToolTip = "Relative Array Rotation"};

            tr.Cells.Add(new TableCell() { Control = lbl_relArrayRot });

            TableLayout tl = new TableLayout();
            Panel p = new Panel {Content = tl};
            tl.Rows.Add(new TableRow());
            tr.Cells.Add(new TableCell() { Control = p });

            comboBox_arrayRotRef = new DropDown
            {
                DataContext = DataContext, SelectedIndex = 0, ToolTip = "Rotation relative to this pattern element"
            };
            comboBox_arrayRotRef.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNames_filtered);

            tl.Rows[^1].Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_arrayRotRef) });

            TableRow tr1 = new TableRow();
            tl.Rows.Add(tr1);

            TableLayout tr1_tl = new TableLayout();
            tr1_tl.Rows.Add(new TableRow());

            tr1.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(tr1_tl) });

            lbl_arrayUse = new Label {Text = "Use"};
            tr1_tl.Rows[^1].Cells.Add(new TableCell() { Control = lbl_arrayUse });

            checkBox_refArrayPivot = new CheckBox
            {
                Text = "Pivot", Enabled = false, ToolTip = "Use pivot point from reference."
            };

            tr1_tl.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_refArrayPivot });

            checkBox_arrayRotRef = new CheckBox
            {
                Text = "Array", Enabled = false, ToolTip = "Use array rotation rather than shape."
            };

            tr1_tl.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_arrayRotRef });

            tl.Rows.Add(new TableRow());

            checkBox_refArrayBoundsAfterRotation = new CheckBox
            {
                Text = "Bounds after rotation",
                Enabled = false,
                ToolTip = "Perform rotation before bounding box. This affects the pivot."
            };

            tl.Rows[^1].Cells.Add(new TableCell() { Control = checkBox_refArrayBoundsAfterRotation });

            tl.Rows[^1].Cells.Add(new TableCell() { Control = null, ScaleWidth = true });

            return tr;
        }

        void pArrayRot_UI(TableLayout tl)
        {
            tl.Rows.Add(pArrayRotationUI_1_UI());

            tl.Rows.Add(pArrayRotationUI_2_UI());

            tl.Rows.Add(new TableRow() { ScaleHeight = true });
        }

        void pMerge_UI(TableLayout tl)
        {
            TableRow tr = new TableRow();
            tl.Rows.Add(tr);

            Label lbl_merge = new Label {Text = "Merge", ToolTip = "Merge"};

            tr.Cells.Add(new TableCell() { Control = lbl_merge });

            comboBox_merge = new DropDown
            {
                DataContext = DataContext, SelectedIndex = 0, ToolTip = "Union with this element on export"
            };
            comboBox_merge.BindDataContext(c => c.DataStore, (UIStringLists m) => m.patternElementNamesForMerge_filtered);

            tr.Cells.Add(new TableCell() { Control = TableLayout.AutoSized(comboBox_merge) });

            tr.Cells.Add(new TableCell() { Control = null });
        }
    }
}