using System;
using shapeEngine;

namespace Quilt;

public partial class MainForm
{
    private void pDoPatternElementUI_baseShape(int pattern, int index, string shapeString)
    {
        pDoPatternElementUI_baseShape1(pattern, index, shapeString);
        pDoPatternElementUI_baseShape_references(pattern, index);
        pDoPatternElementUI_baseShape2(pattern, index, shapeString);
    }

    private void pDoPatternElementUI_baseShape_references(int pattern, int index)
    {
        pDoPatternElementUI_baseShape_references_lengths(pattern, index);
        pDoPatternElementUI_baseShape_references_length_incs(pattern, index);
        pDoPatternElementUI_baseShape_references_length_steps(pattern, index);

        pDoPatternElementUI_baseShape_references_offsets(pattern, index);
        pDoPatternElementUI_baseShape_references_offset_incs(pattern, index);
        pDoPatternElementUI_baseShape_references_offset_steps(pattern, index);
    }

    private void pDoPatternElementUI_baseShape_referencesUI(int index)
    {
        for (int i = 0; i < 3; i++)
        {
            pMinHLRefSubShapeList_update(index, i);
            pMinVLRefSubShapeList_update(index, i);
            pMinHORefSubShapeList_update(index, i);
            pMinVORefSubShapeList_update(index, i);
            pHLIncRefSubShapeList_update(index, i);
            pVLIncRefSubShapeList_update(index, i);
            pHOIncRefSubShapeList_update(index, i);
            pVOIncRefSubShapeList_update(index, i);
            pHLStepsRefSubShapeList_update(index, i);
            pVLStepsRefSubShapeList_update(index, i);
            pHOStepsRefSubShapeList_update(index, i);
            pVOStepsRefSubShapeList_update(index, i);
        }
    }

    private void pDoPatternElementUI_baseShape_references_length_steps(int pattern, int index)
    {
        int m0HLSRefIndex = Math.Max(0, comboBox_s0_minhlsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1HLSRefIndex = Math.Max(0, comboBox_s1_minhlsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2HLSRefIndex = Math.Max(0, comboBox_s2_minhlsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLStepsRef, m0HLSRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLStepsSubShapeRef, comboBox_s0_minhlsteps_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLStepsRef, m1HLSRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLStepsSubShapeRef, comboBox_s1_minhlsteps_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLStepsRef, m2HLSRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLStepsSubShapeRef, comboBox_s2_minhlsteps_subShapeRef.SelectedIndex, 2);

        int m0VLSRefIndex = Math.Max(0, comboBox_s0_minvlsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1VLSRefIndex = Math.Max(0, comboBox_s1_minvlsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2VLSRefIndex = Math.Max(0, comboBox_s2_minvlsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLStepsRef, m0VLSRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLStepsSubShapeRef, comboBox_s0_minvlsteps_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLStepsRef, m1VLSRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLStepsSubShapeRef, comboBox_s1_minvlsteps_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLStepsRef, m2VLSRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLStepsSubShapeRef, comboBox_s2_minvlsteps_subShapeRef.SelectedIndex, 2);

        // Update find buttons.
        btn_s0_hlst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HLStepsRef, 0) > 0;
        btn_s1_hlst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HLStepsRef, 1) > 0;
        btn_s2_hlst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HLStepsRef, 2) > 0;
        btn_s0_vlst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VLStepsRef, 0) > 0;
        btn_s1_vlst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VLStepsRef, 1) > 0;
        btn_s2_vlst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VLStepsRef, 2) > 0;

        int HTSRefIndex = Math.Max(0, comboBox_minhtsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.
        int VTSRefIndex = Math.Max(0, comboBox_minvtsteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HTStepsRef, HTSRefIndex);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VTStepsRef, VTSRefIndex);

        btn_htst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HTStepsRef) > 0;
        btn_vtst.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VTStepsRef) > 0;
    }

    private void pDoPatternElementUI_baseShape_references_length_incs(int pattern, int index)
    {
        int m0HLIRefIndex = Math.Max(0, comboBox_s0_minhlinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1HLIRefIndex = Math.Max(0, comboBox_s1_minhlinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2HLIRefIndex = Math.Max(0, comboBox_s2_minhlinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLIncRef, m0HLIRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLIncSubShapeRef, comboBox_s0_minhlinc_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLIncRef, m1HLIRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLIncSubShapeRef, comboBox_s1_minhlinc_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLIncRef, m2HLIRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLIncSubShapeRef, comboBox_s2_minhlinc_subShapeRef.SelectedIndex, 2);

        int m0VLIRefIndex = Math.Max(0, comboBox_s0_minvlinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1VLIRefIndex = Math.Max(0, comboBox_s1_minvlinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2VLIRefIndex = Math.Max(0, comboBox_s2_minvlinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLIncRef, m0VLIRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLIncSubShapeRef, comboBox_s0_minvlinc_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLIncRef, m1VLIRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLIncSubShapeRef, comboBox_s1_minvlinc_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLIncRef, m2VLIRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VLIncSubShapeRef, comboBox_s2_minvlinc_subShapeRef.SelectedIndex, 2);
        
        // Update find buttons.
        btn_s0_hlinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HLIncRef, 0) > 0;
        btn_s1_hlinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HLIncRef, 1) > 0;
        btn_s2_hlinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HLIncRef, 2) > 0;
        btn_s0_vlinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VLIncRef, 0) > 0;
        btn_s1_vlinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VLIncRef, 1) > 0;
        btn_s2_vlinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VLIncRef, 2) > 0;
        
        int HTSRefIndex = Math.Max(0, comboBox_minhtinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.
        int VTSRefIndex = Math.Max(0, comboBox_minvtinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HTIncRef, HTSRefIndex);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VTIncRef, VTSRefIndex);

        btn_htinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HTIncRef) > 0;
        btn_vtinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VTIncRef) > 0;
    }

    private void pDoPatternElementUI_baseShape_references_lengths(int pattern, int index)
    {
        int m0HLRefIndex = Math.Max(0, comboBox_s0_minhl_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1HLRefIndex = Math.Max(0, comboBox_s1_minhl_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2HLRefIndex = Math.Max(0, comboBox_s2_minhl_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHLRef, m0HLRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHLSubShapeRef, comboBox_s0_minhl_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHLRef, m1HLRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHLSubShapeRef, comboBox_s1_minhl_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHLRef, m2HLRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHLSubShapeRef, comboBox_s2_minhl_subShapeRef.SelectedIndex, 2);

        // Set default
        for (int ss = 0; ss < 3; ss++)
        {
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HLRefFinal, 0, ss);
        }

        if (m0HLRefIndex > 0)
        {
            int useFinal = cb_s0_hl_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HLRefFinal, useFinal, 0);
        }

        if (m1HLRefIndex > 0)
        {
            int useFinal = cb_s1_hl_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HLRefFinal, useFinal, 1);
        }

        if (m2HLRefIndex > 0)
        {
            int useFinal = cb_s2_hl_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HLRefFinal, useFinal, 2);
        }

        int m0VLRefIndex = Math.Max(0, comboBox_s0_minvl_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1VLRefIndex = Math.Max(0, comboBox_s1_minvl_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2VLRefIndex = Math.Max(0, comboBox_s2_minvl_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVLRef, m0VLRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVLSubShapeRef, comboBox_s0_minvl_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVLRef, m1VLRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVLSubShapeRef, comboBox_s1_minvl_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVLRef, m2VLRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVLSubShapeRef, comboBox_s2_minvl_subShapeRef.SelectedIndex, 2);
            
        // Set default
        for (int ss = 0; ss < 3; ss++)
        {
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VLRefFinal, 0, ss);
        }
            
        if (m0VLRefIndex > 0)
        {
            int useFinal = cb_s0_vl_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VLRefFinal, useFinal, 0);
        }

        if (m1VLRefIndex > 0)
        {
            int useFinal = cb_s1_vl_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VLRefFinal, useFinal, 1);
        }
        if (m2VLRefIndex > 0)
        {
            int useFinal = cb_s2_vl_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VLRefFinal, useFinal, 2);
        }

        // Update find buttons.
        btn_s0_hl.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinHLRef, 0) > 0;
        btn_s1_hl.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinHLRef, 1) > 0;
        btn_s2_hl.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinHLRef, 2) > 0;
        btn_s0_vl.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinVLRef, 0) > 0;
        btn_s1_vl.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinVLRef, 1) > 0;
        btn_s2_vl.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinVLRef, 2) > 0;

        int s0TipRefIndex = Math.Max(0, comboBox_s0_tip_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.
        int s0TipSRefIndex = Math.Max(0, comboBox_s0_tip_subShapeRef.SelectedIndex);
        int s1TipRefIndex = Math.Max(0, comboBox_s1_tip_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.
        int s1TipSRefIndex = Math.Max(0, comboBox_s1_tip_subShapeRef.SelectedIndex);
        int s2TipRefIndex = Math.Max(0, comboBox_s2_tip_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.
        int s2TipSRefIndex = Math.Max(0, comboBox_s2_tip_subShapeRef.SelectedIndex);
        
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.tipRef, s0TipRefIndex, 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.tipSubShapeRef, s0TipSRefIndex, 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.tipRef, s1TipRefIndex, 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.tipSubShapeRef, s1TipSRefIndex, 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.tipRef, s2TipRefIndex, 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.tipSubShapeRef, s2TipSRefIndex, 2);
        
        // Update find buttons.
        btn_s0_tip.Enabled = s0TipRefIndex > 0;
        btn_s1_tip.Enabled = s1TipRefIndex > 0;
        btn_s2_tip.Enabled = s2TipRefIndex > 0;
        
        int HTSRefIndex = Math.Max(0, comboBox_minht_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.
        int VTSRefIndex = Math.Max(0, comboBox_minvt_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHTRef, HTSRefIndex);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVTRef, VTSRefIndex);

        if (HTSRefIndex > 0)
        {
            int useFinal = cb_ht_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HTRefFinal, useFinal, 0);
        }

        if (VTSRefIndex > 0)
        {
            int useFinal = cb_vt_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VTRefFinal, useFinal, 0);
        }

        btn_ht.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinHTRef) > 0;
        btn_vt.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinVTRef) > 0;
        
    }

    private void pDoPatternElementUI_baseShape_references_offset_steps(int pattern, int index)
    {
        int m0HOSRefIndex = Math.Max(0, comboBox_s0_minhosteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1HOSRefIndex = Math.Max(0, comboBox_s1_minhosteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2HOSRefIndex = Math.Max(0, comboBox_s2_minhosteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOStepsRef, m0HOSRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOStepsSubShapeRef, comboBox_s0_minhosteps_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOStepsRef, m1HOSRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOStepsSubShapeRef, comboBox_s1_minhosteps_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOStepsRef, m2HOSRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HLStepsSubShapeRef, comboBox_s2_minhosteps_subShapeRef.SelectedIndex, 2);

        int m0VOSRefIndex = Math.Max(0, comboBox_s0_minvosteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1VOSRefIndex = Math.Max(0, comboBox_s1_minvosteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2VOSRefIndex = Math.Max(0, comboBox_s2_minvosteps_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOStepsRef, m0VOSRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOStepsSubShapeRef, comboBox_s0_minvosteps_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOStepsRef, m1VOSRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOStepsSubShapeRef, comboBox_s1_minvosteps_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOStepsRef, m2VOSRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOStepsSubShapeRef, comboBox_s2_minvosteps_subShapeRef.SelectedIndex, 2);
        
        
        // Update find buttons.
        btn_s0_host.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HOStepsRef, 0) > 0;
        btn_s1_host.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HOStepsRef, 1) > 0;
        btn_s2_host.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HOStepsRef, 2) > 0;
        btn_s0_vost.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VOStepsRef, 0) > 0;
        btn_s1_vost.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VOStepsRef, 1) > 0;
        btn_s2_vost.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VOStepsRef, 2) > 0;
    }

    private void pDoPatternElementUI_baseShape_references_offset_incs(int pattern, int index)
    {
        int m0HOIRefIndex = Math.Max(0, comboBox_s0_minhoinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1HOIRefIndex = Math.Max(0, comboBox_s1_minhoinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2HOIRefIndex = Math.Max(0, comboBox_s2_minhoinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOIncRef, m0HOIRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOIncSubShapeRef, comboBox_s0_minhoinc_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOIncRef, m1HOIRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOIncSubShapeRef, comboBox_s1_minhoinc_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOIncRef, m2HOIRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HOIncSubShapeRef, comboBox_s2_minhoinc_subShapeRef.SelectedIndex, 2);

        int m0VOIRefIndex = Math.Max(0, comboBox_s0_minvoinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1VOIRefIndex = Math.Max(0, comboBox_s1_minvoinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2VOIRefIndex = Math.Max(0, comboBox_s2_minvoinc_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOIncRef, m0VOIRefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOIncSubShapeRef, comboBox_s0_minvoinc_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOIncRef, m1VOIRefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOIncSubShapeRef, comboBox_s1_minvoinc_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOIncRef, m2VOIRefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VOIncSubShapeRef, comboBox_s2_minvoinc_subShapeRef.SelectedIndex, 2);
        
        // Update find buttons.
        btn_s0_hoinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HOIncRef, 0) > 0;
        btn_s1_hoinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HOIncRef, 1) > 0;
        btn_s2_hoinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.HOIncRef, 2) > 0;
        btn_s0_voinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VOIncRef, 0) > 0;
        btn_s1_voinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VOIncRef, 1) > 0;
        btn_s2_voinc.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.VOIncRef, 2) > 0;
    }

    private void pDoPatternElementUI_baseShape_references_offsets(int pattern, int index)
    {
        int m0HORefIndex = Math.Max(0, comboBox_s0_minho_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1HORefIndex = Math.Max(0, comboBox_s1_minho_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2HORefIndex = Math.Max(0, comboBox_s2_minho_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHORef, m0HORefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHOSubShapeRef, comboBox_s0_minho_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHORef, m1HORefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHOSubShapeRef, comboBox_s1_minho_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHORef, m2HORefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinHOSubShapeRef, comboBox_s2_minho_subShapeRef.SelectedIndex, 2);

        // Set default
        for (int ss = 0; ss < 3; ss++)
        {
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HORefFinal, 0, ss);
        }
            
        if (m0HORefIndex > 0)
        {
            int useFinal = cb_s0_ho_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HORefFinal, useFinal, 0);
        }

        if (m1HORefIndex > 0)
        {
            int useFinal = cb_s1_ho_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HORefFinal, useFinal, 1);
        }

        if (m2HORefIndex > 0)
        {
            int useFinal = cb_s2_ho_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.HORefFinal, useFinal, 2);
        }
            
        int m0VORefIndex = Math.Max(0, comboBox_s0_minvo_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m1VORefIndex = Math.Max(0, comboBox_s1_minvo_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        int m2VORefIndex = Math.Max(0, comboBox_s2_minvo_ref.SelectedIndex); // due to lazy draw, we can -1 here as an index, which is problematic.

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVORef, m0VORefIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVOSubShapeRef, comboBox_s0_minvo_subShapeRef.SelectedIndex, 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVORef, m1VORefIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVOSubShapeRef, comboBox_s1_minvo_subShapeRef.SelectedIndex, 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVORef, m2VORefIndex, 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.MinVOSubShapeRef, comboBox_s2_minvo_subShapeRef.SelectedIndex, 2);
            
        // Set default
        for (int ss = 0; ss < 3; ss++)
        {
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VORefFinal, 0, ss);
        }
            
        if (m0VORefIndex > 0)
        {
            int useFinal = cb_s0_vo_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VORefFinal, useFinal, 0);
        }

        if (m1VORefIndex > 0)
        {
            int useFinal = cb_s1_vo_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VORefFinal, useFinal, 1);
        }

        if (m2VORefIndex <= 0)
        {
            return;
        }

        {
            int useFinal = cb_s2_vo_final.Checked == true ? 1 : 0;
            commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VORefFinal, useFinal, 2);
        }
        
        // Update find buttons.
        btn_s0_ho.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinHORef, 0) > 0;
        btn_s1_ho.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinHORef, 1) > 0;
        btn_s2_ho.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinHORef, 2) > 0;
        btn_s0_vo.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinVORef, 0) > 0;
        btn_s1_vo.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinVORef, 1) > 0;
        btn_s2_vo.Enabled = commonVars.stitcher.getPatternElement(pattern, index)
            .getInt(PatternElement.properties_i.MinVORef, 2) > 0;
    }

    private void pDoPatternElementUI_baseShape1(int pattern, int index, string shapeString)
    {
        if (shapeString is "none" or "rectangle")
        {
            pClampSubShape2(minHLength: 0,
                maxHLength: 1000000,
                minVLength: 0,
                maxVLength: 1000000,
                minHOffset: -1000000,
                maxHOffset: 1000000,
                minVOffset: -1000000,
                maxVOffset: 1000000
            );
            pClampSubShape3(minHLength: 0,
                maxHLength: 1000000,
                minVLength: 0,
                maxVLength: 1000000,
                minHOffset: -1000000,
                maxHOffset: 1000000,
                minVOffset: -1000000,
                maxVOffset: 1000000
            );

            if (shapeString == "none")
            {
                num_layer_subshape_minhl.Value = 0;
                num_layer_subshape_minvl.Value = 0;
                num_layer_subshape_minho.Value = 0;
                num_layer_subshape_minvo.Value = 0;
            }

            num_layer_subshape2_minhl.Value = 0;
            num_layer_subshape2_minvl.Value = 0;
            num_layer_subshape2_minho.Value = 0;
            num_layer_subshape2_minvo.Value = 0;

            num_layer_subshape3_minhl.Value = 0;
            num_layer_subshape3_minvl.Value = 0;
            num_layer_subshape3_minho.Value = 0;
            num_layer_subshape3_minvo.Value = 0;

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value), 1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value), 1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value), 1);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value), 1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value), 1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value), 1);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value), 1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value),1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value), 1);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value), 1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value), 1);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value), 1);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape1Tip, (int)ShapeSettings.tipLocations.none);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value), 2);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value), 2);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value), 2);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value), 2);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value), 2);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape2Tip, (int)ShapeSettings.tipLocations.none);
        }

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape_minhl.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape_incHL.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape_stepsHL.Value), 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape_minho.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape_incHO.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsHO.Value), 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape_minvl.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape_incVL.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape_stepsVL.Value), 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape_minvo.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape_incVO.Value), 0);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape_stepsVO.Value), 0);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape0Tip, Convert.ToInt32(comboBox_tipLocations.SelectedIndex));

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorTipLength, Convert.ToDecimal(num_layer_minht.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horTipLengthInc, Convert.ToDecimal(num_layer_incHT.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.HorTipSteps, Convert.ToInt32(num_layer_stepsHT.Value));

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerTipLength, Convert.ToDecimal(num_layer_minvt.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verTipLengthInc, Convert.ToDecimal(num_layer_incVT.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.VerTipSteps, Convert.ToInt32(num_layer_stepsVT.Value));

        // doPatternElementUI_baseShape_references(pattern, index);
    }

    private void pDoPatternElementUI_baseShape2(int pattern, int index, string shapeString)
    {
        // Subshape 2 offsets contingent on shape selection choice
        if (shapeString is "none" or "rectangle" or "GEOCORE" or "BOOLEAN")
        {
            return;
        }

        pClampSubShape(minHLength: 0.01,
            maxHLength: 1000000,
            minVLength: 0.01,
            maxVLength: 1000000,
            minHOffset: -1000000,
            maxHOffset: 1000000,
            minVOffset: -1000000,
            maxVOffset: 1000000
        );

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape1Tip, Convert.ToInt32(comboBox_tipLocations2.SelectedIndex));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape2Tip, Convert.ToInt32(comboBox_tipLocations3.SelectedIndex));
        switch (shapeString)
        {
            // Limit offsets of subshape 2 for X-shape.
            case "X":
                pDoUI_X(pattern, index);
                break;
            // Disabled horizontal offset of subshape 2 for T-shape.
            case "T":
                pDoUI_T(pattern, index);
                break;
            // Disable horizontal and vertical offsets of subshape 2 for L-shape
            case "L":
                pDoUI_L(pattern, index);
                break;
            // U-shape
            case "U":
                pDoUI_U(pattern, index);
                break;
            // S-shape
            case "S":
                pDoUI_S(pattern, index);
                break;
        }
    }
}