namespace Quilt
{
    public partial class MainForm
    {
        void pSwitchUIGadgets_off()
        {
            comboBox_s1_minhl_ref.Enabled = false;
            comboBox_s1_minhlinc_ref.Enabled = false;
            comboBox_s1_minhlsteps_ref.Enabled = false;

            cb_s1_hl_final.Enabled = false;
            
            comboBox_s1_minhl_subShapeRef.Enabled = false;
            comboBox_s1_minhlinc_subShapeRef.Enabled = false;
            comboBox_s1_minhlsteps_subShapeRef.Enabled = false;

            num_layer_subshape2_minhl.Enabled = false;
            num_layer_subshape2_incHL.Enabled = false;
            num_layer_subshape2_stepsHL.Enabled = false;

            comboBox_s1_minvl_ref.Enabled = false;
            comboBox_s1_minvlinc_ref.Enabled = false;
            comboBox_s1_minvlsteps_ref.Enabled = false;
            
            cb_s1_vl_final.Enabled = false;

            comboBox_s1_minvl_subShapeRef.Enabled = false;
            comboBox_s1_minvlinc_subShapeRef.Enabled = false;
            comboBox_s1_minvlsteps_subShapeRef.Enabled = false;

            num_layer_subshape2_minvl.Enabled = false;
            num_layer_subshape2_incVL.Enabled = false;
            num_layer_subshape2_stepsVL.Enabled = false;

            comboBox_s1_minho_ref.Enabled = false;
            comboBox_s1_minhoinc_ref.Enabled = false;
            comboBox_s1_minhosteps_ref.Enabled = false;

            cb_s1_ho_final.Enabled = false;

            comboBox_s1_minho_subShapeRef.Enabled = false;
            comboBox_s1_minhoinc_subShapeRef.Enabled = false;
            comboBox_s1_minhosteps_subShapeRef.Enabled = false;
            
            num_layer_subshape2_minho.Enabled = false;
            num_layer_subshape2_incHO.Enabled = false;
            num_layer_subshape2_stepsHO.Enabled = false;

            comboBox_s1_minvo_ref.Enabled = false;
            comboBox_s1_minvoinc_ref.Enabled = false;
            comboBox_s1_minvosteps_ref.Enabled = false;

            cb_s1_vo_final.Enabled = false;

            comboBox_s1_minvo_subShapeRef.Enabled = false;
            comboBox_s1_minvoinc_subShapeRef.Enabled = false;
            comboBox_s1_minvosteps_subShapeRef.Enabled = false;

            num_layer_subshape2_minvo.Enabled = false;
            num_layer_subshape2_incVO.Enabled = false;
            num_layer_subshape2_stepsVO.Enabled = false;

            comboBox_s2_minhl_ref.Enabled = false;
            comboBox_s2_minhlinc_ref.Enabled = false;
            comboBox_s2_minhlsteps_ref.Enabled = false;

            cb_s2_hl_final.Enabled = false;

            comboBox_s2_minhl_subShapeRef.Enabled = false;
            comboBox_s2_minhlinc_subShapeRef.Enabled = false;
            comboBox_s2_minhlsteps_subShapeRef.Enabled = false;

            num_layer_subshape3_minhl.Enabled = false;
            num_layer_subshape3_incHL.Enabled = false;
            num_layer_subshape3_stepsHL.Enabled = false;

            comboBox_s2_minvl_ref.Enabled = false;
            comboBox_s2_minvlinc_ref.Enabled = false;
            comboBox_s2_minvlsteps_ref.Enabled = false;

            cb_s2_vl_final.Enabled = false;

            comboBox_s2_minvl_subShapeRef.Enabled = false;
            comboBox_s2_minvlinc_subShapeRef.Enabled = false;
            comboBox_s2_minvlsteps_subShapeRef.Enabled = false;

            num_layer_subshape3_minvl.Enabled = false;
            num_layer_subshape3_incVL.Enabled = false;
            num_layer_subshape3_stepsVL.Enabled = false;

            comboBox_s2_minho_ref.Enabled = false;
            comboBox_s2_minhoinc_ref.Enabled = false;
            comboBox_s2_minhosteps_ref.Enabled = false;

            cb_s2_ho_final.Enabled = false;

            comboBox_s2_minho_subShapeRef.Enabled = false;
            comboBox_s2_minhoinc_subShapeRef.Enabled = false;
            comboBox_s2_minhosteps_subShapeRef.Enabled = false;

            num_layer_subshape3_minho.Enabled = false;
            num_layer_subshape3_incHO.Enabled = false;
            num_layer_subshape3_stepsHO.Enabled = false;

            comboBox_s2_minvo_ref.Enabled = false;
            comboBox_s2_minvoinc_ref.Enabled = false;
            comboBox_s2_minvosteps_ref.Enabled = false;

            cb_s2_vo_final.Enabled = false;

            comboBox_s2_minvo_subShapeRef.Enabled = false;
            comboBox_s2_minvoinc_subShapeRef.Enabled = false;
            comboBox_s2_minvosteps_subShapeRef.Enabled = false;

            num_layer_subshape3_minvo.Enabled = false;
            num_layer_subshape3_incVO.Enabled = false;
            num_layer_subshape3_stepsVO.Enabled = false;
        }

        void pRefState_subShape1(int pattern, int index)
        {
            int ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinHLRef, 0);
            bool refActive = (ref_ != 0);
            num_layer_subshape_minhl.Enabled = !refActive;
            comboBox_s0_minhl_subShapeRef.Enabled = refActive;
            cb_s0_hl_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s0_hl_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinVLRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_minvl.Enabled = !refActive;
            comboBox_s0_minvl_subShapeRef.Enabled = refActive;
            cb_s0_vl_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s0_vl_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HLIncRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_incHL.Enabled = !refActive;
            comboBox_s0_minhlinc_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VLIncRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_incVL.Enabled = !refActive;
            comboBox_s0_minvlinc_subShapeRef.Enabled = refActive;

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HLStepsRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_stepsHL.Enabled = !refActive;
            comboBox_s0_minhlsteps_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VLStepsRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_stepsVL.Enabled = !refActive;
            comboBox_s0_minvlsteps_subShapeRef.Enabled = refActive;

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinHORef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_minho.Enabled = !refActive;
            comboBox_s0_minho_subShapeRef.Enabled = refActive;
            cb_s0_ho_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s0_ho_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinVORef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_minvo.Enabled = !refActive;
            comboBox_s0_minvo_subShapeRef.Enabled = refActive;
            cb_s0_vo_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s0_vo_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HOIncRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_incHO.Enabled = !refActive;
            comboBox_s0_minhoinc_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VOIncRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_incVO.Enabled = !refActive;
            comboBox_s0_minvoinc_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HOStepsRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_stepsHO.Enabled = !refActive;
            comboBox_s0_minhosteps_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VOStepsRef, 0);
            refActive = (ref_ != 0);
            num_layer_subshape_stepsVO.Enabled = !refActive;
            comboBox_s0_minvosteps_subShapeRef.Enabled = refActive;
        }

        void pRefState_subShape2(int pattern, int index)
        {
            comboBox_s1_minhl_ref.Enabled = true;
            comboBox_s1_minhlinc_ref.Enabled = true;
            comboBox_s1_minhlsteps_ref.Enabled = true;

            comboBox_s1_minvl_ref.Enabled = true;
            comboBox_s1_minvlinc_ref.Enabled = true;
            comboBox_s1_minvlsteps_ref.Enabled = true;
            
            comboBox_s1_minho_ref.Enabled = true;
            comboBox_s1_minhoinc_ref.Enabled = true;
            comboBox_s1_minhosteps_ref.Enabled = true;

            comboBox_s1_minvo_ref.Enabled = true;
            comboBox_s1_minvoinc_ref.Enabled = true;
            comboBox_s1_minvosteps_ref.Enabled = true;
            
            int ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinHLRef, 1);
            bool refActive = (ref_ != 0);
            num_layer_subshape2_minhl.Enabled = !refActive;
            comboBox_s1_minhl_subShapeRef.Enabled = refActive;
            cb_s1_hl_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s1_hl_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinVLRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_minvl.Enabled = !refActive;
            comboBox_s1_minvl_subShapeRef.Enabled = refActive;
            cb_s1_vl_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s1_vl_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HLIncRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_incHL.Enabled = !refActive;
            comboBox_s1_minhlinc_subShapeRef.Enabled = refActive;
        
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VLIncRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_incVL.Enabled = !refActive;
            comboBox_s1_minvlinc_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HLStepsRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_stepsHL.Enabled = !refActive;
            comboBox_s1_minhlsteps_subShapeRef.Enabled = refActive;
        
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VLStepsRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_stepsVL.Enabled = !refActive;
            comboBox_s1_minvlsteps_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinHORef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_minho.Enabled = !refActive;
            comboBox_s1_minho_subShapeRef.Enabled = refActive;
            cb_s1_ho_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s1_ho_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinVORef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_minvo.Enabled = !refActive;
            comboBox_s1_minvo_subShapeRef.Enabled = refActive;
            cb_s1_vo_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s1_vo_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HOIncRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_incHO.Enabled = !refActive;
            comboBox_s1_minhoinc_subShapeRef.Enabled = refActive;
        
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VOIncRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_incVO.Enabled = !refActive;
            comboBox_s1_minvoinc_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HOStepsRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_stepsHO.Enabled = !refActive;
            comboBox_s1_minhosteps_subShapeRef.Enabled = refActive;
        
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VOStepsRef, 1);
            refActive = (ref_ != 0);
            num_layer_subshape2_stepsVO.Enabled = !refActive;
            comboBox_s1_minvosteps_subShapeRef.Enabled = refActive;
        }

        void pRefState_subShape3(int pattern, int index)
        {
            comboBox_s2_minhl_ref.Enabled = true;
            comboBox_s2_minhlinc_ref.Enabled = true;
            comboBox_s2_minhlsteps_ref.Enabled = true;

            comboBox_s2_minvl_ref.Enabled = true;
            comboBox_s2_minvlinc_ref.Enabled = true;
            comboBox_s2_minvlsteps_ref.Enabled = true;
            
            comboBox_s2_minho_ref.Enabled = true;
            comboBox_s2_minhoinc_ref.Enabled = true;
            comboBox_s2_minhosteps_ref.Enabled = true;

            comboBox_s2_minvo_ref.Enabled = true;
            comboBox_s2_minvoinc_ref.Enabled = true;
            comboBox_s2_minvosteps_ref.Enabled = true;
            
            int ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinHLRef, 2);
            bool refActive = (ref_ != 0);
            num_layer_subshape3_minhl.Enabled = !refActive;
            comboBox_s2_minhl_subShapeRef.Enabled = refActive;
            cb_s2_hl_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s2_hl_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinVLRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_minvl.Enabled = !refActive;
            comboBox_s2_minvl_subShapeRef.Enabled = refActive;
            cb_s2_vl_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s2_vl_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HLIncRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_incHL.Enabled = !refActive;
            comboBox_s2_minhlinc_subShapeRef.Enabled = refActive;
    
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VLIncRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_incVL.Enabled = !refActive;
            comboBox_s2_minvlinc_subShapeRef.Enabled = refActive;

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HLStepsRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_stepsHL.Enabled = !refActive;
            comboBox_s2_minhlsteps_subShapeRef.Enabled = refActive;
    
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VLStepsRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_stepsVL.Enabled = !refActive;
            comboBox_s2_minvlsteps_subShapeRef.Enabled = refActive;
            
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinHORef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_minho.Enabled = !refActive;
            comboBox_s2_minho_subShapeRef.Enabled = refActive;
            cb_s2_ho_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s2_ho_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.MinVORef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_minvo.Enabled = !refActive;
            comboBox_s2_minvo_subShapeRef.Enabled = refActive;
            cb_s2_vo_final.Enabled = refActive;
            if (!refActive)
            {
                cb_s2_vo_final.Checked = false;
            }

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HOIncRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_incHO.Enabled = !refActive;
            comboBox_s2_minhoinc_subShapeRef.Enabled = refActive;
    
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VOIncRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_incVO.Enabled = !refActive;
            comboBox_s2_minvoinc_subShapeRef.Enabled = refActive;

            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.HOStepsRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_stepsHO.Enabled = !refActive;
            comboBox_s2_minhosteps_subShapeRef.Enabled = refActive;
    
            ref_ = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.VOStepsRef, 2);
            refActive = (ref_ != 0);
            num_layer_subshape3_stepsVO.Enabled = !refActive;
            comboBox_s2_minvosteps_subShapeRef.Enabled = refActive;
        }
        
        void pDoPatternElementUI_subshape(int pattern, int index, bool updateUI, string shapeString)
        {
            int previousIndex = comboBox_subShapeRef.SelectedIndex;

            if (updateUI)
            {
                if ((shapeString != "bounding") && (shapeString != "complex"))
                {
                    pDoPatternElementUI_num(pattern, index, shapeString);
                }
                else
                {
                    if (shapeString == "bounding")
                    {
                        groupBox_properties.Content = groupBox_bounding_table;
                    }
                    if (shapeString == "complex")
                    {
                        groupBox_properties.Content = groupBox_layout_table;
                    }

                    commonVars.subshapes.Clear();
                    commonVars.subshapes.Add("1");
                }
            }
            if (previousIndex >= commonVars.subshapes.Count)
            {
                previousIndex = commonVars.subshapes.Count - 1;
            }

            comboBox_subShapeRef.SelectedIndex = previousIndex;

            if ((shapeString != "bounding") && (shapeString != "complex"))
            {
                pDoPatternElementUI_baseShape(pattern, index, shapeString);
            }
            else
            {
                if (shapeString == "bounding")
                {
                    pDoUI_bounding(pattern, index);
                }
                if (shapeString == "complex")
                {
                    pDoUI_complex(pattern, index);
                }
            }

        }
    }
}