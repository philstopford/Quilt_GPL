﻿using Eto.Forms;
using Eto.Veldrid;
using VeldridEto;

namespace Quilt
{
    public partial class MainForm
    {
        int listBox_entries_Width = 200;
        int listBox_entries_Height = 300;
        int viewportSize = 752;
        int label_Height = 8;
        int num_Height = 8;
        int numWidth = 55;
        
        ListBox listBox_entries;
        TextBox text_patternElement;
        Panel settings;
        TableLayout main_tl; // the non-viewport layout container.
        TableLayout left_tl; // left-hand side table layout container within main_tl
        TableLayout right_tl; // right-hand side table layout container within main_tl

        Button btn_layout;

        DropDown comboBox_patternElementShape, comboBox_layout_structures;

        DropDown comboBox_s0_minhl_ref, comboBox_s0_minhl_subShapeRef,
            comboBox_s1_minhl_ref, comboBox_s1_minhl_subShapeRef,
            comboBox_s2_minhl_ref, comboBox_s2_minhl_subShapeRef,
            comboBox_s0_minvl_ref, comboBox_s0_minvl_subShapeRef,
            comboBox_s1_minvl_ref, comboBox_s1_minvl_subShapeRef,
            comboBox_s2_minvl_ref, comboBox_s2_minvl_subShapeRef,
            comboBox_s0_minho_ref, comboBox_s0_minho_subShapeRef,
            comboBox_s1_minho_ref, comboBox_s1_minho_subShapeRef,
            comboBox_s2_minho_ref, comboBox_s2_minho_subShapeRef,
            comboBox_s0_minvo_ref, comboBox_s0_minvo_subShapeRef,
            comboBox_s1_minvo_ref, comboBox_s1_minvo_subShapeRef,
            comboBox_s2_minvo_ref, comboBox_s2_minvo_subShapeRef;
        
        CheckBox cb_s0_hl_final, cb_s1_hl_final, cb_s2_hl_final,
        cb_s0_vl_final, cb_s1_vl_final, cb_s2_vl_final,
        cb_s0_ho_final, cb_s1_ho_final, cb_s2_ho_final,
        cb_s0_vo_final, cb_s1_vo_final, cb_s2_vo_final;

        DropDown comboBox_s0_minhlinc_ref, comboBox_s0_minhlinc_subShapeRef,
            comboBox_s1_minhlinc_ref, comboBox_s1_minhlinc_subShapeRef,
            comboBox_s2_minhlinc_ref, comboBox_s2_minhlinc_subShapeRef,
            comboBox_s0_minvlinc_ref, comboBox_s0_minvlinc_subShapeRef,
            comboBox_s1_minvlinc_ref, comboBox_s1_minvlinc_subShapeRef,
            comboBox_s2_minvlinc_ref, comboBox_s2_minvlinc_subShapeRef,
            comboBox_s0_minhoinc_ref, comboBox_s0_minhoinc_subShapeRef,
            comboBox_s1_minhoinc_ref, comboBox_s1_minhoinc_subShapeRef,
            comboBox_s2_minhoinc_ref, comboBox_s2_minhoinc_subShapeRef,
            comboBox_s0_minvoinc_ref, comboBox_s0_minvoinc_subShapeRef,
            comboBox_s1_minvoinc_ref, comboBox_s1_minvoinc_subShapeRef,
            comboBox_s2_minvoinc_ref, comboBox_s2_minvoinc_subShapeRef;

        DropDown comboBox_s0_minhlsteps_ref, comboBox_s0_minhlsteps_subShapeRef,
            comboBox_s1_minhlsteps_ref, comboBox_s1_minhlsteps_subShapeRef,
            comboBox_s2_minhlsteps_ref, comboBox_s2_minhlsteps_subShapeRef,
            comboBox_s0_minvlsteps_ref, comboBox_s0_minvlsteps_subShapeRef,
            comboBox_s1_minvlsteps_ref, comboBox_s1_minvlsteps_subShapeRef,
            comboBox_s2_minvlsteps_ref, comboBox_s2_minvlsteps_subShapeRef,
            comboBox_s0_minhosteps_ref, comboBox_s0_minhosteps_subShapeRef,
            comboBox_s1_minhosteps_ref, comboBox_s1_minhosteps_subShapeRef,
            comboBox_s2_minhosteps_ref, comboBox_s2_minhosteps_subShapeRef,
            comboBox_s0_minvosteps_ref, comboBox_s0_minvosteps_subShapeRef,
            comboBox_s1_minvosteps_ref, comboBox_s1_minvosteps_subShapeRef,
            comboBox_s2_minvosteps_ref, comboBox_s2_minvosteps_subShapeRef;

        NumericStepper num_layer_subshape_minhl, num_layer_subshape2_minhl, num_layer_subshape3_minhl,
                        num_layer_subshape_minho, num_layer_subshape2_minho, num_layer_subshape3_minho,
                        num_layer_subshape_minvl, num_layer_subshape2_minvl, num_layer_subshape3_minvl,
                        num_layer_subshape_minvo, num_layer_subshape2_minvo, num_layer_subshape3_minvo;

        NumericStepper num_layer_subshape_stepsHL, num_layer_subshape2_stepsHL, num_layer_subshape3_stepsHL,
                        num_layer_subshape_stepsHO, num_layer_subshape2_stepsHO, num_layer_subshape3_stepsHO,
                        num_layer_subshape_stepsVL, num_layer_subshape2_stepsVL, num_layer_subshape3_stepsVL,
                        num_layer_subshape_stepsVO, num_layer_subshape2_stepsVO, num_layer_subshape3_stepsVO;

        NumericStepper num_layer_subshape_incHL, num_layer_subshape2_incHL, num_layer_subshape3_incHL,
                        num_layer_subshape_incHO, num_layer_subshape2_incHO, num_layer_subshape3_incHO,
                        num_layer_subshape_incVL, num_layer_subshape2_incVL, num_layer_subshape3_incVL,
                        num_layer_subshape_incVO, num_layer_subshape2_incVO, num_layer_subshape3_incVO;

        NumericStepper num_layer_minbbl, num_layer_bblinc, num_layer_bblsteps,
                        num_layer_minbbr, num_layer_bbrinc, num_layer_bbrsteps,
                        num_layer_minbbb, num_layer_bbbinc, num_layer_bbbsteps,
                        num_layer_minbbt, num_layer_bbtinc, num_layer_bbtsteps;

        DropDown comboBox_subShapeRef, comboBox_posSubShape, comboBox_xPos_subShapeRefPos, comboBox_yPos_subShapeRefPos, comboBox_rotRef, comboBox_arrayRotRef, comboBox_arrayRef, comboBox_merge;
        Label lbl_subShapeRef, lbl_posSubShape;

        DropDown comboBox_xPosRef, comboBox_yPosRef, comboBox_xPos_subShapeRef, comboBox_yPos_subShapeRef;
        Label lbl_xPosRef, lbl_yPosRef;
        NumericStepper num_minXPos, num_incXPos, num_stepsXPos, num_minYPos, num_incYPos, num_stepsYPos;

        Label lbl_rotation;
        CheckBox checkBox_rotRef;
        NumericStepper num_minRot, num_incRot, num_stepsRot;

        Label lbl_arrayRotation;
        NumericStepper num_minArrayRot, num_incArrayRot, num_stepsArrayRot;

        Label lbl_flip, lbl_arrayX, lbl_arrayY, lbl_use, lbl_arrayUse;
        CheckBox checkBox_flipH, checkBox_flipV, checkBox_alignX, checkBox_alignY, checkBox_arrayRotRef, checkBox_refPivot, checkBox_refArrayPivot, checkBox_refBoundsAfterRotation, checkBox_refArrayBoundsAfterRotation;
        NumericStepper num_arrayMinXCount, num_arrayMinYCount, num_arrayXInc, num_arrayYInc, num_arrayXSteps, num_arrayYSteps;
        NumericStepper num_arrayMinXSpace, num_arrayMinYSpace, num_arrayXSpaceInc, num_arrayYSpaceInc, num_arrayXSpaceSteps, num_arrayYSpaceSteps;

        GroupBox groupBox_properties;

        GroupBox groupBox_position;

        Label lbl_viewportZoom, lbl_viewportPos;
        NumericStepper num_viewportZoom, num_viewportX, num_viewportY;
        VeldridSurface vSurface;
        VeldridDriver viewPort;
        ContextMenu vp_menu;

        Label lbl_padding, lbl_patNum;
        NumericStepper num_padding, num_patNum;
        Button entry_Add, entry_Rename, entry_Remove;
        Button btn_export;

        ButtonMenuItem fileMenu, editMenu;

        CheckBox checkBox_suspendBuild, checkBox_showInput;

        Label progressLabel;
        ProgressBar progressBar;

        TabControl tabControl;
        TabPage quiltPage, prefsPage;

        bool colUIFrozen, utilsUIFrozen;
        Panel quiltUISplitter, prefsPanel;

        NumericStepper[] num_externalGeoCoordsX, num_externalGeoCoordsY;

        NumericStepper num_zoomSpeed, num_fgOpacity, num_bgOpacity, num_angularTolerance;
        CheckBox checkBox_OGLAA, checkBox_OGLFill, checkBox_OGLPoints, checkBox_drawExtents, checkBox_verticalRectDecomp;
        Label lbl_ss1Color, lbl_ss2Color, lbl_ss3Color,
                lbl_enabledColor, lbl_backgroundColor, lbl_axisColor, lbl_majorGridColor, lbl_minorGridColor, lbl_vpbgColor, lbl_extentsColor,
                lbl_ss1Color_name, lbl_ss2Color_name, lbl_ss3Color_name,
                lbl_enabledColor_name, lbl_backgroundColor_name, lbl_axisColor_name, lbl_majorGridColor_name, lbl_minorGridColor_name, lbl_vpbgColor_name, lbl_extentsColor_name,
                lbl_zoomSpeed, lbl_fgOpacity, lbl_bgOpacity, lbl_angularTolerance;
        Button btn_resetColors;

        TableLayout groupBox_subShapes_table, groupBox_bounding_table, groupBox_layout_table;

        ContextMenu listbox_menu;
        ButtonMenuItem lb_copy, lb_paste;

        Button btn_Cancel;
        
        int patternIndex; // used to avoid moving the view if the pattern number loses focus, but the value hasn't changed.

        bool openGLErrorReported;

        TableLayout settings_tl, progress_tl;
    }
}
