using System;

namespace Quilt
{
    public partial class MainForm
    {
        void pDoPatternElementUI_baseShape(int pattern, int index, string shapeString)
        {
            pDoPatternElementUI_baseShape1(pattern, index, shapeString);
            pDoPatternElementUI_baseShape_references(pattern, index);
            pDoPatternElementUI_baseShape2(pattern, index, shapeString);
        }

        void pDoPatternElementUI_baseShape_references(int pattern, int index)
        {
            pDoPatternElementUI_baseShape_references_lengths(pattern, index);
            pDoPatternElementUI_baseShape_references_length_incs(pattern, index);
            pDoPatternElementUI_baseShape_references_length_steps(pattern, index);

            pDoPatternElementUI_baseShape_references_offsets(pattern, index);
            pDoPatternElementUI_baseShape_references_offset_incs(pattern, index);
            pDoPatternElementUI_baseShape_references_offset_steps(pattern, index);
        }
        
        void pDoPatternElementUI_baseShape_referencesUI(int index)
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

        void pDoPatternElementUI_baseShape_references_length_steps(int pattern, int index)
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
        }
        
        void pDoPatternElementUI_baseShape_references_length_incs(int pattern, int index)
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
        }
        
        void pDoPatternElementUI_baseShape_references_lengths(int pattern, int index)
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

        }

        void pDoPatternElementUI_baseShape_references_offset_steps(int pattern, int index)
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
        }
        
        void pDoPatternElementUI_baseShape_references_offset_incs(int pattern, int index)
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
        }
        
        void pDoPatternElementUI_baseShape_references_offsets(int pattern, int index)
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

            if (m2VORefIndex > 0)
            {
                int useFinal = cb_s2_vo_final.Checked == true ? 1 : 0;
                commonVars.stitcher.getPatternElement(pattern, index).setInt(PatternElement.properties_i.VORefFinal, useFinal, 2);
            }
        }

        void pDoPatternElementUI_baseShape1(int pattern, int index, string shapeString)
        {
            if ((shapeString == "none") || (shapeString == "rectangle"))
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

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape1Tip, (int)CommonVars.tipLocations.none);

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

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape2Tip, (int)CommonVars.tipLocations.none);
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

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape0Tip, (int)CommonVars.tipLocations.none);

            // doPatternElementUI_baseShape_references(pattern, index);
        }

        void pDoPatternElementUI_baseShape2(int pattern, int index, string shapeString)
        {
            // Subshape 2 offsets contingent on shape selection choice
            if ((shapeString != "none") && (shapeString != "rectangle") && (shapeString != "GEOCORE") && (shapeString != "BOOLEAN"))
            {
                pClampSubShape(minHLength: 0.01,
                    maxHLength: 1000000,
                    minVLength: 0.01,
                    maxVLength: 1000000,
                    minHOffset: -1000000,
                    maxHOffset: 1000000,
                    minVOffset: -1000000,
                    maxVOffset: 1000000
                );

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shape1Tip, (int)CommonVars.tipLocations.none);
                if (shapeString == "X") // Limit offsets of subshape 2 for X-shape.
                {
                    pDoUI_X(pattern, index);
                }
                else if (shapeString == "T") // Disabled horizontal offset of subshape 2 for T-shape.
                {
                    pDoUI_T(pattern, index);
                }
                else if (shapeString == "L") // Disable horizontal and vertical offsets of subshape 2 for L-shape
                {
                    pDoUI_L(pattern, index);
                }
                else if (shapeString == "U") // U-shape
                {
                    pDoUI_U(pattern, index);
                }
                else if (shapeString == "S") // S-shape
                {
                    pDoUI_S(pattern, index);
                }
            }
        }
    }
}