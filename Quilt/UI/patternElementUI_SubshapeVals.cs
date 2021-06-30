using Eto.Forms;
using System;

namespace Quilt
{
    public partial class MainForm
    {
        void pSetSubShapeVals(int index, int subShapeIndex)
        {

            int s0hlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHLRef, 0);
            int s0hlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHLSubShapeRef, 0);
            comboBox_s0_minhl_ref.SelectedIndex = s0hlref;
            comboBox_s0_minhl_subShapeRef.SelectedIndex = s0hlssref;
            cb_s0_hl_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.HLRefFinal, 0) == 1;

            int s1hlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHLRef, 1);
            int s1hlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHLSubShapeRef, 1);
            comboBox_s1_minhl_ref.SelectedIndex = s1hlref;
            comboBox_s1_minhl_subShapeRef.SelectedIndex = s1hlssref;
            cb_s1_hl_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.HLRefFinal, 1) == 1;

            int s2hlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHLRef, 2);
            int s2hlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHLSubShapeRef, 2);
            comboBox_s2_minhl_ref.SelectedIndex = s2hlref;
            comboBox_s2_minhl_subShapeRef.SelectedIndex = s2hlssref;
            cb_s2_hl_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.HLRefFinal, 0) == 2;

            int s0vlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVLRef, 0);
            int s0vlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVLSubShapeRef, 0);
            comboBox_s0_minvl_ref.SelectedIndex = s0vlref;
            comboBox_s0_minvl_subShapeRef.SelectedIndex = s0vlssref;
            cb_s0_vl_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.VLRefFinal, 0) == 1;

            int s1vlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVLRef, 1);
            int s1vlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVLSubShapeRef, 1);
            comboBox_s1_minvl_ref.SelectedIndex = s1vlref;
            comboBox_s1_minvl_subShapeRef.SelectedIndex = s1vlssref;
            cb_s1_vl_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.VLRefFinal, 1) == 1;

            int s2vlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVLRef, 2);
            int s2vlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVLSubShapeRef, 2);
            comboBox_s2_minvl_ref.SelectedIndex = s2vlref;
            comboBox_s2_minvl_subShapeRef.SelectedIndex = s2vlssref;
            cb_s2_vl_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.VLRefFinal, 2) == 1;

            int s0horef = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHORef, 0);
            int s0hossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHOSubShapeRef, 0);
            comboBox_s0_minho_ref.SelectedIndex = s0horef;
            comboBox_s0_minho_subShapeRef.SelectedIndex = s0hossref;
            cb_s0_ho_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.HORefFinal, 0) == 1;

            int s1horef = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHORef, 1);
            int s1hossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHOSubShapeRef, 1);
            comboBox_s1_minho_ref.SelectedIndex = s1horef;
            comboBox_s1_minho_subShapeRef.SelectedIndex = s1hossref;
            cb_s1_ho_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.HORefFinal, 1) == 1;

            int s2horef = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHORef, 2);
            int s2hossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinHOSubShapeRef, 2);
            comboBox_s2_minho_ref.SelectedIndex = s2horef;
            comboBox_s2_minho_subShapeRef.SelectedIndex = s2hossref;
            cb_s2_ho_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.HORefFinal, 2) == 1;

            int s0voref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVORef, 0);
            int s0vossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVOSubShapeRef, 0);
            comboBox_s0_minvo_ref.SelectedIndex = s0voref;
            comboBox_s0_minvo_subShapeRef.SelectedIndex = s0vossref;
            cb_s0_vo_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.VORefFinal, 0) == 1;

            int s1voref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVORef, 1);
            int s1vossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVOSubShapeRef, 1);
            comboBox_s1_minvo_ref.SelectedIndex = s1voref;
            comboBox_s1_minvo_subShapeRef.SelectedIndex = s1vossref;
            cb_s1_vo_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.VORefFinal, 1) == 1;

            int s2voref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVORef, 2);
            int s2vossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.MinVOSubShapeRef, 2);
            comboBox_s2_minvo_ref.SelectedIndex = s2voref;
            comboBox_s2_minvo_subShapeRef.SelectedIndex = s2vossref;
            cb_s2_vo_final.Checked = commonVars.stitcher.getPatternElement(0, index)
                .getInt(PatternElement.properties_i.VORefFinal, 2) == 1;

            int s0hlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLIncRef, 0);
            int s0hlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLIncSubShapeRef, 0);
            comboBox_s0_minhlinc_ref.SelectedIndex = s0hlincref;
            comboBox_s0_minhlinc_subShapeRef.SelectedIndex = s0hlincssref;

            int s1hlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLIncRef, 1);
            int s1hlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLIncSubShapeRef, 1);
            comboBox_s1_minhlinc_ref.SelectedIndex = s1hlincref;
            comboBox_s1_minhlinc_subShapeRef.SelectedIndex = s1hlincssref;

            int s2hlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLIncRef, 2);
            int s2hlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLIncSubShapeRef, 2);
            comboBox_s2_minhlinc_ref.SelectedIndex = s2hlincref;
            comboBox_s2_minhlinc_subShapeRef.SelectedIndex = s2hlincssref;

            int s0vlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLIncRef, 0);
            int s0vlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLIncSubShapeRef, 0);
            comboBox_s0_minvlinc_ref.SelectedIndex = s0vlincref;
            comboBox_s0_minvlinc_subShapeRef.SelectedIndex = s0vlincssref;

            int s1vlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLIncRef, 1);
            int s1vlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLIncSubShapeRef, 1);
            comboBox_s1_minvlinc_ref.SelectedIndex = s1vlincref;
            comboBox_s1_minvlinc_subShapeRef.SelectedIndex = s1vlincssref;

            int s2vlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLIncRef, 2);
            int s2vlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLIncSubShapeRef, 2);
            comboBox_s2_minvlinc_ref.SelectedIndex = s2vlincref;
            comboBox_s2_minvlinc_subShapeRef.SelectedIndex = s2vlincssref;

            int s0hoincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOIncRef, 0);
            int s0hoincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOIncSubShapeRef, 0);
            comboBox_s0_minhoinc_ref.SelectedIndex = s0hoincref;
            comboBox_s0_minhoinc_subShapeRef.SelectedIndex = s0hoincssref;

            int s1hoincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOIncRef, 1);
            int s1hoincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOIncSubShapeRef, 1);
            comboBox_s1_minhoinc_ref.SelectedIndex = s1hoincref;
            comboBox_s1_minhoinc_subShapeRef.SelectedIndex = s1hoincssref;

            int s2hoincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOIncRef, 2);
            int s2hoincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOIncSubShapeRef, 2);
            comboBox_s2_minhoinc_ref.SelectedIndex = s2hoincref;
            comboBox_s2_minhoinc_subShapeRef.SelectedIndex = s2hoincssref;

            int s0voincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOIncRef, 0);
            int s0voincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOIncSubShapeRef, 0);
            comboBox_s0_minvoinc_ref.SelectedIndex = s0voincref;
            comboBox_s0_minvoinc_subShapeRef.SelectedIndex = s0voincssref;

            int s1voincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOIncRef, 1);
            int s1voincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOIncSubShapeRef, 1);
            comboBox_s1_minvoinc_ref.SelectedIndex = s1voincref;
            comboBox_s1_minvoinc_subShapeRef.SelectedIndex = s1voincssref;

            int s2voincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOIncRef, 2);
            int s2voincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOIncSubShapeRef, 2);
            comboBox_s2_minvoinc_ref.SelectedIndex = s2voincref;
            comboBox_s2_minvoinc_subShapeRef.SelectedIndex = s2voincssref;

            int s0hlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLStepsRef, 0);
            int s0hlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLStepsSubShapeRef, 0);
            comboBox_s0_minhlsteps_ref.SelectedIndex = s0hlstepsref;
            comboBox_s0_minhlsteps_subShapeRef.SelectedIndex = s0hlstepsssref;

            int s1hlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLStepsRef, 1);
            int s1hlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLStepsSubShapeRef, 1);
            comboBox_s1_minhlsteps_ref.SelectedIndex = s1hlstepsref;
            comboBox_s1_minhlsteps_subShapeRef.SelectedIndex = s1hlstepsssref;

            int s2hlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLStepsRef, 2);
            int s2hlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HLStepsSubShapeRef, 2);
            comboBox_s2_minhlsteps_ref.SelectedIndex = s2hlstepsref;
            comboBox_s2_minhlsteps_subShapeRef.SelectedIndex = s2hlstepsssref;

            int s0vlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLStepsRef, 0);
            int s0vlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLStepsSubShapeRef, 0);
            comboBox_s0_minvlsteps_ref.SelectedIndex = s0vlstepsref;
            comboBox_s0_minvlsteps_subShapeRef.SelectedIndex = s0vlstepsssref;

            int s1vlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLStepsRef, 1);
            int s1vlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLStepsSubShapeRef, 1);
            comboBox_s1_minvlsteps_ref.SelectedIndex = s1vlstepsref;
            comboBox_s1_minvlsteps_subShapeRef.SelectedIndex = s1vlstepsssref;

            int s2vlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLStepsRef, 2);
            int s2vlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VLStepsSubShapeRef, 2);
            comboBox_s2_minvlsteps_ref.SelectedIndex = s2vlstepsref;
            comboBox_s2_minvlsteps_subShapeRef.SelectedIndex = s2vlstepsssref;

            int s0hostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOStepsRef, 0);
            int s0hostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOStepsSubShapeRef, 0);
            comboBox_s0_minhosteps_ref.SelectedIndex = s0hostepsref;
            comboBox_s0_minhosteps_subShapeRef.SelectedIndex = s0hostepsssref;

            int s1hostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOStepsRef, 1);
            int s1hostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOStepsSubShapeRef, 1);
            comboBox_s1_minhosteps_ref.SelectedIndex = s1hostepsref;
            comboBox_s1_minhosteps_subShapeRef.SelectedIndex = s1hostepsssref;

            int s2hostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOStepsRef, 2);
            int s2hostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.HOStepsSubShapeRef, 2);
            comboBox_s2_minhosteps_ref.SelectedIndex = s2hostepsref;
            comboBox_s2_minhosteps_subShapeRef.SelectedIndex = s2hostepsssref;

            int s0vostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOStepsRef, 0);
            int s0vostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOStepsSubShapeRef, 0);
            comboBox_s0_minvosteps_ref.SelectedIndex = s0vostepsref;
            comboBox_s0_minvosteps_subShapeRef.SelectedIndex = s0vostepsssref;

            int s1vostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOStepsRef, 1);
            int s1vostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOStepsSubShapeRef, 1);
            comboBox_s1_minvosteps_ref.SelectedIndex = s1vostepsref;
            comboBox_s1_minvosteps_subShapeRef.SelectedIndex = s1vostepsssref;

            int s2vostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOStepsRef, 2);
            int s2vostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                .getInt(PatternElement.properties_i.VOStepsSubShapeRef, 2);
            comboBox_s2_minvosteps_ref.SelectedIndex = s2vostepsref;
            comboBox_s2_minvosteps_subShapeRef.SelectedIndex = s2vostepsssref;

            switch (subShapeIndex)
            {
                case 0:
                    num_layer_subshape_minhl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0));
                    num_layer_subshape_minho.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minHorOffset, 0));
                    num_layer_subshape_minvl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0));
                    num_layer_subshape_minvo.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minVerOffset, 0));

                    num_layer_subshape_incHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.horLengthInc, 0));
                    num_layer_subshape_incHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.horOffsetInc, 0));
                    num_layer_subshape_incVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.verLengthInc, 0));
                    num_layer_subshape_incVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.verOffsetInc, 0));

                    num_layer_subshape_stepsHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.horLengthSteps, 0));
                    num_layer_subshape_stepsHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.horOffsetSteps, 0));
                    num_layer_subshape_stepsVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.verLengthSteps, 0));
                    num_layer_subshape_stepsVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.verOffsetSteps, 0));
                    break;
                case 1:
                    num_layer_subshape2_minhl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minHorLength, 1));
                    num_layer_subshape2_minho.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minHorOffset, 1));
                    num_layer_subshape2_minvl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minVerLength, 1));
                    num_layer_subshape2_minvo.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minVerOffset, 1));

                    num_layer_subshape2_incHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.horLengthInc, 1));
                    num_layer_subshape2_incHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.horOffsetInc, 1));
                    num_layer_subshape2_incVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.verLengthInc, 1));
                    num_layer_subshape2_incVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.verOffsetInc, 1));

                    num_layer_subshape2_stepsHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.horLengthSteps, 1));
                    num_layer_subshape2_stepsHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.horOffsetSteps, 1));
                    num_layer_subshape2_stepsVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.verLengthSteps, 1));
                    num_layer_subshape2_stepsVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.verOffsetSteps, 1));
                    break;
                case 2:
                    num_layer_subshape3_minhl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minHorLength, 2));
                    num_layer_subshape3_minho.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minHorOffset, 2));
                    num_layer_subshape3_minvl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minVerLength, 2));
                    num_layer_subshape3_minvo.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minVerOffset, 2));

                    num_layer_subshape3_incHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.horLengthInc, 2));
                    num_layer_subshape3_incHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.horOffsetInc, 2));
                    num_layer_subshape3_incVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.verLengthInc, 2));
                    num_layer_subshape3_incVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.verOffsetInc, 2));

                    num_layer_subshape3_stepsHL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.horLengthSteps, 2));
                    num_layer_subshape3_stepsHO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.horOffsetSteps, 2));
                    num_layer_subshape3_stepsVL.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.verLengthSteps, 2));
                    num_layer_subshape3_stepsVO.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.verOffsetSteps, 2));
                    break;
            }
        }

        void pClearSubShapeVals(int subShapeIndex)
        {
            switch (subShapeIndex)
            {
                case 0:
                    num_layer_subshape_minhl.Value = 0;
                    num_layer_subshape_minho.Value = 0;
                    num_layer_subshape_minvl.Value = 0;
                    num_layer_subshape_minvo.Value = 0;

                    num_layer_subshape_incHL.Value = 0;
                    num_layer_subshape_incHO.Value = 0;
                    num_layer_subshape_incVL.Value = 0;
                    num_layer_subshape_incVO.Value = 0;

                    num_layer_subshape_stepsHL.Value = 1;
                    num_layer_subshape_stepsHO.Value = 1;
                    num_layer_subshape_stepsVL.Value = 1;
                    num_layer_subshape_stepsVO.Value = 1;
                    break;
                case 1:
                    num_layer_subshape2_minhl.Value = 0;
                    num_layer_subshape2_minho.Value = 0;
                    num_layer_subshape2_minvl.Value = 0;
                    num_layer_subshape2_minvo.Value = 0;

                    num_layer_subshape2_incHL.Value = 0;
                    num_layer_subshape2_incHO.Value = 0;
                    num_layer_subshape2_incVL.Value = 0;
                    num_layer_subshape2_incVO.Value = 0;

                    num_layer_subshape2_stepsHL.Value = 1;
                    num_layer_subshape2_stepsHO.Value = 1;
                    num_layer_subshape2_stepsVL.Value = 1;
                    num_layer_subshape2_stepsVO.Value = 1;
                    break;
                case 2:
                    num_layer_subshape3_minhl.Value = 0;
                    num_layer_subshape3_minho.Value = 0;
                    num_layer_subshape3_minvl.Value = 0;
                    num_layer_subshape3_minvo.Value = 0;

                    num_layer_subshape3_incHL.Value = 0;
                    num_layer_subshape3_incHO.Value = 0;
                    num_layer_subshape3_incVL.Value = 0;
                    num_layer_subshape3_incVO.Value = 0;

                    num_layer_subshape3_stepsHL.Value = 1;
                    num_layer_subshape3_stepsHO.Value = 1;
                    num_layer_subshape3_stepsVL.Value = 1;
                    num_layer_subshape3_stepsVO.Value = 1;
                    break;
            }
        }

        void pClampNumeric(ref NumericStepper num, double min, double max)
        {
            num.Value = Math.Max(min, num.Value);
            num.Value = Math.Min(max, num.Value);
        }

        void pClampSubShape(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                pClampNumeric(ref num_layer_subshape_minhl, minHLength, maxHLength);
                pClampNumeric(ref num_layer_subshape_minvl, minVLength, maxVLength);
                pClampNumeric(ref num_layer_subshape_minho, minHOffset, maxHOffset);
                pClampNumeric(ref num_layer_subshape_minvo, minVOffset, maxVOffset);
            });
        }

        void pClampSubShape2(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                pClampNumeric(ref num_layer_subshape2_minhl, minHLength, maxHLength);
                pClampNumeric(ref num_layer_subshape2_minvl, minVLength, maxVLength);
                pClampNumeric(ref num_layer_subshape2_minho, minHOffset, maxHOffset);
                pClampNumeric(ref num_layer_subshape2_minvo, minVOffset, maxVOffset);
            });
        }

        void pClampSubShape3(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
        {
            Application.Instance.Invoke(() =>
            {
                pClampNumeric(ref num_layer_subshape3_minhl, minHLength, maxHLength);
                pClampNumeric(ref num_layer_subshape3_minvl, minVLength, maxVLength);
                pClampNumeric(ref num_layer_subshape3_minho, minHOffset, maxHOffset);
                pClampNumeric(ref num_layer_subshape3_minvo, minVOffset, maxVOffset);
            });
        }
    }
}