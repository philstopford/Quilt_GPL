using System;
using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
    private void pAddHandlers_RefLengths()
    {
        comboBox_s0_minhl_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minhl_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minhl_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minhl_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minhl_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minhl_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minvl_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minvl_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minvl_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minvl_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minvl_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minvl_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
            
        comboBox_s0_minhlinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minhlinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minhlinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minhlinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minhlinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minhlinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minvlinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minvlinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minvlinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minvlinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minvlinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minvlinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minhlsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minhlsteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minhlsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minhlsteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minhlsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minhlsteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minvlsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minvlsteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minvlsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minvlsteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minvlsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minvlsteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_tipLocations.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_tipLocations2.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_tipLocations3.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_tip_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_tip_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_tip_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_tip_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_tip_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_tip_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_minht_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_minvt_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_minhtinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_minvtinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_minhtsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_minvtsteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        
        cb_ht_final.CheckedChanged += pDoPatternElementUI;
        cb_vt_final.CheckedChanged += pDoPatternElementUI;
    }

    private void pAddHandlers_RefOffsets()
    {
        comboBox_s0_minho_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minho_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minho_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minho_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minho_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minho_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minvo_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minvo_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minvo_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minvo_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minvo_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minvo_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minhoinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minhoinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minhoinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minhoinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minhoinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minhoinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minvoinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minvoinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minvoinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minvoinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minvoinc_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minvoinc_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minhosteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minhosteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minhosteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minhosteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minhosteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minhosteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_minvosteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s0_minvosteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s1_minvosteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_minvosteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s2_minvosteps_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_minvosteps_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        cb_s0_hl_final.CheckedChanged += pDoPatternElementUI;
        cb_s0_vl_final.CheckedChanged += pDoPatternElementUI;
        cb_s0_ho_final.CheckedChanged += pDoPatternElementUI;
        cb_s0_vo_final.CheckedChanged += pDoPatternElementUI;
            
        cb_s1_hl_final.CheckedChanged += pDoPatternElementUI;
        cb_s1_vl_final.CheckedChanged += pDoPatternElementUI;
        cb_s1_ho_final.CheckedChanged += pDoPatternElementUI;
        cb_s1_vo_final.CheckedChanged += pDoPatternElementUI;
            
        cb_s2_hl_final.CheckedChanged += pDoPatternElementUI;
        cb_s2_vl_final.CheckedChanged += pDoPatternElementUI;
        cb_s2_ho_final.CheckedChanged += pDoPatternElementUI;
        cb_s2_vo_final.CheckedChanged += pDoPatternElementUI;
    }

    private void pAddHanders_baseShape()
    {
        comboBox_patternElementShape.SelectedIndexChanged += pDoPatternElementUI;
        
        num_layer_subshape_minhl.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_minhl.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_minhl.LostFocus += pDoPatternElementUI;

        num_layer_subshape_minvl.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_minvl.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_minvl.LostFocus += pDoPatternElementUI;

        num_layer_subshape_minho.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_minho.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_minho.LostFocus += pDoPatternElementUI;

        num_layer_subshape_minvo.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_minvo.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_minvo.LostFocus += pDoPatternElementUI;

        num_layer_subshape_incHL.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_incHL.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_incHL.LostFocus += pDoPatternElementUI;

        num_layer_subshape_incVL.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_incVL.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_incVL.LostFocus += pDoPatternElementUI;

        num_layer_subshape_incHO.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_incHO.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_incHO.LostFocus += pDoPatternElementUI;

        num_layer_subshape_incVO.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_incVO.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_incVO.LostFocus += pDoPatternElementUI;

        num_layer_subshape_stepsHL.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_stepsHL.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_stepsHL.LostFocus += pDoPatternElementUI;

        num_layer_subshape_stepsVL.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_stepsVL.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_stepsVL.LostFocus += pDoPatternElementUI;

        num_layer_subshape_stepsHO.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_stepsHO.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_stepsHO.LostFocus += pDoPatternElementUI;

        num_layer_subshape_stepsVO.LostFocus += pDoPatternElementUI;
        num_layer_subshape2_stepsVO.LostFocus += pDoPatternElementUI;
        num_layer_subshape3_stepsVO.LostFocus += pDoPatternElementUI;

        comboBox_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_posSubShape.SelectedIndexChanged += pDoPatternElementUI;
    }

    private void pAddHandlers_BBox()
    {
        num_layer_minbbl.LostFocus += pDoPatternElementUI;
        num_layer_bblinc.LostFocus += pDoPatternElementUI;
        num_layer_bblsteps.LostFocus += pDoPatternElementUI;

        num_layer_minbbr.LostFocus += pDoPatternElementUI;
        num_layer_bbrinc.LostFocus += pDoPatternElementUI;
        num_layer_bbrsteps.LostFocus += pDoPatternElementUI;

        num_layer_minbbb.LostFocus += pDoPatternElementUI;
        num_layer_bbbinc.LostFocus += pDoPatternElementUI;
        num_layer_bbbsteps.LostFocus += pDoPatternElementUI;

        num_layer_minbbt.LostFocus += pDoPatternElementUI;
        num_layer_bbtinc.LostFocus += pDoPatternElementUI;
        num_layer_bbtsteps.LostFocus += pDoPatternElementUI;
    }

    private void pAddHandlers_Pos()
    {
        comboBox_xPosRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_yPosRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_xPos_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_yPos_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_xPos_subShapeRefPos.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_yPos_subShapeRefPos.SelectedIndexChanged += pDoPatternElementUI;

        num_minXPos.LostFocus += pDoPatternElementUI;
        num_incXPos.LostFocus += pDoPatternElementUI;
        num_stepsXPos.LostFocus += pDoPatternElementUI;

        num_minYPos.LostFocus += pDoPatternElementUI;
        num_incYPos.LostFocus += pDoPatternElementUI;
        num_stepsYPos.LostFocus += pDoPatternElementUI;

        num_minRot.LostFocus += pDoPatternElementUI;
        num_incRot.LostFocus += pDoPatternElementUI;
        num_stepsRot.LostFocus += pDoPatternElementUI;
        checkBox_rotRef.CheckedChanged += pDoPatternElementUI;
        rB_rotPivot_self.CheckedChanged += pDoPatternElementUI;
        rB_rotPivot_ref.CheckedChanged += pDoPatternElementUI;
        rB_rotPivot_worldOrigin.CheckedChanged += pDoPatternElementUI;
        checkBox_refBoundsAfterRotation.CheckedChanged += pDoPatternElementUI;

        comboBox_rotRef.SelectedIndexChanged += pDoPatternElementUI;
        
        checkBox_flipH.CheckedChanged += pDoPatternElementUI;
        checkBox_flipV.CheckedChanged += pDoPatternElementUI;
        checkBox_alignX.CheckedChanged += pDoPatternElementUI;
        checkBox_alignY.CheckedChanged += pDoPatternElementUI;
    }

    private void pAddHandlers_array()
    {
        num_minArrayRot.LostFocus += pDoPatternElementUI;
        num_incArrayRot.LostFocus += pDoPatternElementUI;
        num_stepsArrayRot.LostFocus += pDoPatternElementUI;

        comboBox_arrayRotRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_arrayRef.SelectedIndexChanged += pDoPatternElementUI;
        checkBox_arrayRotRef.CheckedChanged += pDoPatternElementUI;
        rB_arrayRotPivot_self.CheckedChanged += pDoPatternElementUI;
        rB_arrayRotPivot_ref.CheckedChanged += pDoPatternElementUI;
        rB_arrayRotPivot_worldOrigin.CheckedChanged += pDoPatternElementUI;
        checkBox_refArrayBoundsAfterRotation.CheckedChanged += pDoPatternElementUI;

        num_arrayMinXCount.LostFocus += pDoPatternElementUI;
        num_arrayXInc.LostFocus += pDoPatternElementUI;
        num_arrayXSteps.LostFocus += pDoPatternElementUI;
        num_arrayMinXSpace.LostFocus += pDoPatternElementUI;
        num_arrayXSpaceInc.LostFocus += pDoPatternElementUI;
        num_arrayXSpaceSteps.LostFocus += pDoPatternElementUI;
        num_arrayMinYCount.LostFocus += pDoPatternElementUI;
        num_arrayYInc.LostFocus += pDoPatternElementUI;
        num_arrayYSteps.LostFocus += pDoPatternElementUI;
        num_arrayMinYSpace.LostFocus += pDoPatternElementUI;
        num_arrayYSpaceInc.LostFocus += pDoPatternElementUI;
        num_arrayYSpaceSteps.LostFocus += pDoPatternElementUI;
    }

    private void pAddHandlers_tips()
    {
        comboBox_tipLocations.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_tipLocations2.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_tipLocations3.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_tip_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_tip_ref.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_tip_ref.SelectedIndexChanged += pDoPatternElementUI;

        comboBox_s0_tip_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s1_tip_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;
        comboBox_s2_tip_subShapeRef.SelectedIndexChanged += pDoPatternElementUI;

        cb_ht_final.CheckedChanged += pDoPatternElementUI;
        cb_vt_final.CheckedChanged += pDoPatternElementUI;
        
        num_layer_minht.LostFocus += pDoPatternElementUI;
        num_layer_minvt.LostFocus += pDoPatternElementUI;
        num_layer_incHT.LostFocus += pDoPatternElementUI;
        num_layer_incVT.LostFocus += pDoPatternElementUI;
        num_layer_stepsHT.LostFocus += pDoPatternElementUI;
        num_layer_stepsVT.LostFocus += pDoPatternElementUI;
    }
    
    private void pAddHandlers()
    {
        pAddHanders_baseShape();
        pAddHandlers_BBox();
        pAddHandlers_RefLengths();
        pAddHandlers_RefOffsets();
        pAddHandlers_Pos();
        pAddHandlers_array();
        pAddHandlers_tips();
        
        comboBox_merge.SelectedIndexChanged += pDoPatternElementUI;

        num_padding.LostFocus += pSetPadding;

        entry_Add.Click += pAddPatternElement;
        entry_Rename.Click += pRenamePatternElement;
        entry_Remove.Click += pRemovePatternElement;

        btn_export.Click += pExportClicked;

        if (num_externalGeoCoordsX != null)
        {
            try
            {
                foreach (var t in num_externalGeoCoordsX)
                {
                    if (t != null)
                    {
                        t.LostFocus += pDoPatternElementUI;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        if (num_externalGeoCoordsY == null)
        {
            return;
        }

        {
            try
            {
                foreach (NumericStepper t in num_externalGeoCoordsY)
                {
                    if (t != null)
                    {
                        t.LostFocus += pDoPatternElementUI;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }

    private void pUpdatePatternElementUI(object sender, EventArgs e)
    {
        if (UIFreeze)
        {
            return;
        }
        pUpdatePatternElementUI();
    }
}