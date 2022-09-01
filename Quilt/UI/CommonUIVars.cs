using Eto.Forms;
using Eto.Veldrid;
using VeldridEto;

namespace Quilt;

public partial class MainForm
{
    private const int listBox_entries_Width = 200;
    private const int listBox_entries_Height = 300;
    private const int viewportSize = 752;
    private const int label_Height = 8;
    private const int num_Height = 8;
    private const int numWidth = 55;

    private ListBox listBox_entries;
    private TextBox text_patternElement;
    private Panel settings;
    private TableLayout main_tl; // the non-viewport layout container.
    private TableLayout left_tl; // left-hand side table layout container within main_tl
    private TableLayout right_tl; // right-hand side table layout container within main_tl

    private Button btn_layout;

    private DropDown comboBox_patternElementShape, comboBox_layout_structures, comboBox_tipLocations, comboBox_tipLocations2, comboBox_tipLocations3;

    private DropDown comboBox_s0_minhl_ref, comboBox_s0_minhl_subShapeRef,
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
        comboBox_s2_minvo_ref, comboBox_s2_minvo_subShapeRef,
        comboBox_minht_ref, comboBox_minvt_ref;

    private CheckBox cb_s0_hl_final, cb_s1_hl_final, cb_s2_hl_final,
        cb_s0_vl_final, cb_s1_vl_final, cb_s2_vl_final,
        cb_s0_ho_final, cb_s1_ho_final, cb_s2_ho_final,
        cb_s0_vo_final, cb_s1_vo_final, cb_s2_vo_final,
        cb_ht_final, cb_vt_final;

    private DropDown comboBox_s0_minhlinc_ref, comboBox_s0_minhlinc_subShapeRef,
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
        comboBox_s2_minvoinc_ref, comboBox_s2_minvoinc_subShapeRef,
        comboBox_minhtinc_ref, comboBox_minvtinc_ref;

    private DropDown comboBox_s0_minhlsteps_ref, comboBox_s0_minhlsteps_subShapeRef,
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
        comboBox_s2_minvosteps_ref, comboBox_s2_minvosteps_subShapeRef,
        comboBox_minhtsteps_ref, comboBox_minvtsteps_ref;

    private NumericStepper num_layer_subshape_minhl, num_layer_subshape2_minhl, num_layer_subshape3_minhl,
        num_layer_subshape_minho, num_layer_subshape2_minho, num_layer_subshape3_minho,
        num_layer_subshape_minvl, num_layer_subshape2_minvl, num_layer_subshape3_minvl,
        num_layer_subshape_minvo, num_layer_subshape2_minvo, num_layer_subshape3_minvo,
        num_layer_minht, num_layer_minvt;

    private NumericStepper num_layer_subshape_stepsHL, num_layer_subshape2_stepsHL, num_layer_subshape3_stepsHL,
        num_layer_subshape_stepsHO, num_layer_subshape2_stepsHO, num_layer_subshape3_stepsHO,
        num_layer_subshape_stepsVL, num_layer_subshape2_stepsVL, num_layer_subshape3_stepsVL,
        num_layer_subshape_stepsVO, num_layer_subshape2_stepsVO, num_layer_subshape3_stepsVO,
        num_layer_stepsHT, num_layer_stepsVT;

    private NumericStepper num_layer_subshape_incHL, num_layer_subshape2_incHL, num_layer_subshape3_incHL,
        num_layer_subshape_incHO, num_layer_subshape2_incHO, num_layer_subshape3_incHO,
        num_layer_subshape_incVL, num_layer_subshape2_incVL, num_layer_subshape3_incVL,
        num_layer_subshape_incVO, num_layer_subshape2_incVO, num_layer_subshape3_incVO,
        num_layer_incHT, num_layer_incVT;

    private NumericStepper num_layer_minbbl, num_layer_bblinc, num_layer_bblsteps,
        num_layer_minbbr, num_layer_bbrinc, num_layer_bbrsteps,
        num_layer_minbbb, num_layer_bbbinc, num_layer_bbbsteps,
        num_layer_minbbt, num_layer_bbtinc, num_layer_bbtsteps;

    private DropDown comboBox_subShapeRef, comboBox_posSubShape, comboBox_xPos_subShapeRefPos, comboBox_yPos_subShapeRefPos, comboBox_rotRef, comboBox_arrayRotRef, comboBox_arrayRef, comboBox_merge;
    private RadioButton rB_rotPivot_self, rB_rotPivot_worldOrigin, rB_rotPivot_ref;
    private RadioButton rB_arrayRotPivot_self, rB_arrayRotPivot_worldOrigin, rB_arrayRotPivot_ref;
    private Label lbl_subShapeRef, lbl_posSubShape;

    private DropDown comboBox_xPosRef, comboBox_yPosRef, comboBox_xPos_subShapeRef, comboBox_yPos_subShapeRef;
    private Label lbl_xPosRef, lbl_yPosRef;
    private NumericStepper num_minXPos, num_incXPos, num_stepsXPos, num_minYPos, num_incYPos, num_stepsYPos;

    private Label lbl_rotation;
    private CheckBox checkBox_rotRef;
    private NumericStepper num_minRot, num_incRot, num_stepsRot;

    private Label lbl_arrayRotation;
    private NumericStepper num_minArrayRot, num_incArrayRot, num_stepsArrayRot;

    private Label lbl_flip, lbl_arrayX, lbl_arrayY, lbl_use, lbl_arrayUse;
    private CheckBox checkBox_flipH, checkBox_flipV, checkBox_alignX, checkBox_alignY, checkBox_arrayRotRef, checkBox_refBoundsAfterRotation, checkBox_refArrayBoundsAfterRotation;
    private NumericStepper num_arrayMinXCount, num_arrayMinYCount, num_arrayXInc, num_arrayYInc, num_arrayXSteps, num_arrayYSteps;
    private NumericStepper num_arrayMinXSpace, num_arrayMinYSpace, num_arrayXSpaceInc, num_arrayYSpaceInc, num_arrayXSpaceSteps, num_arrayYSpaceSteps;

    private GroupBox groupBox_properties;

    private GroupBox groupBox_position;

    private Label lbl_viewportZoom, lbl_viewportPos;
    private NumericStepper num_viewportZoom, num_viewportX, num_viewportY;
    private VeldridSurface vSurface;
    private VeldridDriver viewPort;
    private ContextMenu vp_menu;
    private ButtonMenuItem vp_selLinkedElement, vp_selXPosElement, vp_selYPosElement, vp_selRotElement, vp_selArrayElement, vp_selArrayRotElement;

    private Label lbl_padding, lbl_patNum;
    private NumericStepper num_padding, num_patNum;
    private Button entry_Add, entry_Rename, entry_Remove;
    private Button btn_export;

    private ButtonMenuItem fileMenu, editMenu;

    private CheckBox checkBox_suspendBuild, checkBox_showInput;

    private Label progressLabel;
    private ProgressBar progressBar;

    private TabControl tabControl;
    private TabPage quiltPage, prefsPage;

    private bool colUIFrozen, utilsUIFrozen;
    private Panel quiltUISplitter, prefsPanel;

    private NumericStepper[] num_externalGeoCoordsX, num_externalGeoCoordsY;

    private NumericStepper num_zoomSpeed, num_fgOpacity, num_bgOpacity, num_angularTolerance;
    private CheckBox checkBox_OGLAA, checkBox_OGLFill, checkBox_OGLPoints, checkBox_drawExtents, checkBox_verticalRectDecomp, checkBox_expandUI;

    private Label lbl_ss1Color, lbl_ss2Color, lbl_ss3Color,
        lbl_enabledColor, lbl_backgroundColor, lbl_axisColor, lbl_majorGridColor, lbl_minorGridColor, lbl_vpbgColor, lbl_extentsColor,
        lbl_ss1Color_name, lbl_ss2Color_name, lbl_ss3Color_name,
        lbl_enabledColor_name, lbl_backgroundColor_name, lbl_axisColor_name, lbl_majorGridColor_name, lbl_minorGridColor_name, lbl_vpbgColor_name, lbl_extentsColor_name,
        lbl_zoomSpeed, lbl_fgOpacity, lbl_bgOpacity, lbl_angularTolerance;

    private Button btn_s0_hl, btn_s1_hl, btn_s2_hl, btn_s0_vl, btn_s1_vl, btn_s2_vl;
    private Button btn_s0_ho, btn_s1_ho, btn_s2_ho, btn_s0_vo, btn_s1_vo, btn_s2_vo;
    private Button btn_s0_hlinc, btn_s1_hlinc, btn_s2_hlinc, btn_s0_vlinc, btn_s1_vlinc, btn_s2_vlinc;
    private Button btn_s0_hoinc, btn_s1_hoinc, btn_s2_hoinc, btn_s0_voinc, btn_s1_voinc, btn_s2_voinc;
    private Button btn_s0_hlst, btn_s1_hlst, btn_s2_hlst, btn_s0_vlst, btn_s1_vlst, btn_s2_vlst;
    private Button btn_s0_host, btn_s1_host, btn_s2_host, btn_s0_vost, btn_s1_vost, btn_s2_vost;
    private Button btn_ht, btn_vt, btn_htinc, btn_vtinc, btn_htst, btn_vtst, btn_s0_tip, btn_s1_tip, btn_s2_tip;

    private Button btn_posXRef, btn_posYRef, btn_rotRef, btn_arrayRef, btn_arrayRotRef, btn_mergeRef;

    private Button btn_resetColors;

    private TableLayout groupBox_subShapes_table, groupBox_bounding_table, groupBox_layout_table;

    private DropDown comboBox_s0_tip_ref, comboBox_s1_tip_ref, comboBox_s2_tip_ref,
        comboBox_s0_tip_subShapeRef, comboBox_s1_tip_subShapeRef, comboBox_s2_tip_subShapeRef;

    private ContextMenu listbox_menu;
    private ButtonMenuItem lb_copy, lb_paste, lb_selLinkedElement, lb_selXPosElement, lb_selYPosElement, lb_selRotElement, lb_selArrayElement, lb_selArrayRotElement;

    private Button btn_Cancel;

    private int patternIndex; // used to avoid moving the view if the pattern number loses focus, but the value hasn't changed.

    private bool openGLErrorReported;

    private TableLayout settings_tl, progress_tl;
}