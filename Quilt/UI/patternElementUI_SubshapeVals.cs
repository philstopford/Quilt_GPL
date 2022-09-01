using Eto.Forms;
using System;

namespace Quilt;

public partial class MainForm
{
    private void pSetTipVals(int index)
    {
        int htref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinHTRef);
        comboBox_minht_ref.SelectedIndex = htref;
        
        int vtref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinVTRef);
        comboBox_minvt_ref.SelectedIndex = vtref;
        
        int htiref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HTIncRef);
        comboBox_minhtinc_ref.SelectedIndex = htiref;
        
        int vtiref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VTIncRef);
        comboBox_minvtinc_ref.SelectedIndex = vtiref;
        
        int htsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HTStepsRef);
        comboBox_minhtsteps_ref.SelectedIndex = htsref;
        
        int vtsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VTStepsRef);
        comboBox_minvtsteps_ref.SelectedIndex = vtsref;

        cb_ht_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.HTRefFinal) == 1;
        cb_vt_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.VTRefFinal) == 1;

        num_layer_minht.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minHorTipLength));
        num_layer_incHT.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.horTipLengthInc));
        num_layer_stepsHT.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HorTipSteps));
        
        num_layer_minvt.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minVerTipLength));
        num_layer_incVT.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.verTipLengthInc));
        num_layer_stepsVT.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VerTipSteps));


        for (int ss = 0; ss < 3; ss++)
        {
            int comboBox_tipLocationsSelectedIndex = 0;
            int ss_tref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipRef, ss);
            int ss_stref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipSubShapeRef, ss);
            
            if (ss_tref > 0)
            {
                // Retrieve the tip from the reference layer to show in the menu.
                switch (ss_stref)
                {
                    case 0:
                        comboBox_tipLocationsSelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, ss_tref)
                            .getInt(PatternElement.properties_i.shape0Tip);
                        break;
                    case 1:
                        comboBox_tipLocationsSelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, ss_tref)
                            .getInt(PatternElement.properties_i.shape1Tip);
                        break;
                    case 2:
                        comboBox_tipLocationsSelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, ss_tref)
                            .getInt(PatternElement.properties_i.shape2Tip);
                        break;
                }
            }
            else
            {
                switch (ss)
                {
                    case 0:
                        comboBox_tipLocationsSelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                            .getInt(PatternElement.properties_i.shape0Tip);
                        break;
                    case 1:
                        comboBox_tipLocationsSelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                            .getInt(PatternElement.properties_i.shape1Tip);
                        break;
                    case 2:
                        comboBox_tipLocationsSelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index)
                            .getInt(PatternElement.properties_i.shape2Tip);
                        break;
                }
            }

            switch (ss)
            {
                case 0:
                    comboBox_tipLocations.SelectedIndex = comboBox_tipLocationsSelectedIndex;
                    comboBox_s0_tip_ref.SelectedIndex = ss_tref;
                    comboBox_s0_tip_subShapeRef.SelectedIndex = ss_stref;
                    break;
                case 1:
                    comboBox_tipLocations2.SelectedIndex = comboBox_tipLocationsSelectedIndex;
                    comboBox_s1_tip_ref.SelectedIndex = ss_tref;
                    comboBox_s1_tip_subShapeRef.SelectedIndex = ss_stref;
                    break;
                case 2:
                    comboBox_tipLocations3.SelectedIndex = comboBox_tipLocationsSelectedIndex;
                    comboBox_s2_tip_ref.SelectedIndex = ss_tref;
                    comboBox_s2_tip_subShapeRef.SelectedIndex = ss_stref;
                    break;
            }
        }
    }

    private void pSetSubShapeVals(int index, int subShapeIndex)
    {
        for (int ss = 0; ss < 3; ss++)
        {
            int hlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinHLRef, ss);
            int hlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinHLSubShapeRef, ss);
            int vlref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinVLRef, ss);
            int vlssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinVLSubShapeRef, ss);
            int horef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinHORef, ss);
            int hossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinHOSubShapeRef, ss);
            int voref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinVORef, ss);
            int vossref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.MinVOSubShapeRef, ss);
            int hlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HLIncRef, ss);
            int hlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HLIncSubShapeRef, ss);
            int vlincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VLIncRef, ss);
            int vlincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VLIncSubShapeRef, ss);
            int hoincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HOIncRef, ss);
            int hoincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HOIncSubShapeRef, ss);
            int voincref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VOIncRef, ss);
            int voincssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VOIncSubShapeRef, ss);
            int hlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HLStepsRef, ss);
            int hlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HLStepsSubShapeRef, ss);
            int vlstepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VLStepsRef, ss);
            int vlstepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VLStepsSubShapeRef, ss);
            int hostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HOStepsRef, ss);
            int hostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.HOStepsSubShapeRef, ss);
            int vostepsref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VOStepsRef, ss);
            int vostepsssref = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.VOStepsSubShapeRef, ss);

            switch (ss)
            {
                case 0:
                    comboBox_s0_minhl_ref.SelectedIndex = hlref;
                    comboBox_s0_minhl_subShapeRef.SelectedIndex = hlssref;
                    cb_s0_hl_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.HLRefFinal, ss) == 1;
                    comboBox_s0_minvl_ref.SelectedIndex = vlref;
                    comboBox_s0_minvl_subShapeRef.SelectedIndex = vlssref;
                    cb_s0_vl_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.VLRefFinal, ss) == 1;
                    comboBox_s0_minho_ref.SelectedIndex = horef;
                    comboBox_s0_minho_subShapeRef.SelectedIndex = hossref;
                    cb_s0_ho_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.HORefFinal, ss) == 1;
                    comboBox_s0_minvo_ref.SelectedIndex = voref;
                    comboBox_s0_minvo_subShapeRef.SelectedIndex = vossref;
                    cb_s0_vo_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.VORefFinal, ss) == 1;
                    comboBox_s0_minhlinc_ref.SelectedIndex = hlincref;
                    comboBox_s0_minhlinc_subShapeRef.SelectedIndex = hlincssref;
                    comboBox_s0_minvlinc_ref.SelectedIndex = vlincref;
                    comboBox_s0_minvlinc_subShapeRef.SelectedIndex = vlincssref;
                    comboBox_s0_minhoinc_ref.SelectedIndex = hoincref;
                    comboBox_s0_minhoinc_subShapeRef.SelectedIndex = hoincssref;
                    comboBox_s0_minvoinc_ref.SelectedIndex = voincref;
                    comboBox_s0_minvoinc_subShapeRef.SelectedIndex = voincssref;
                    comboBox_s0_minhlsteps_ref.SelectedIndex = hlstepsref;
                    comboBox_s0_minhlsteps_subShapeRef.SelectedIndex = hlstepsssref;
                    comboBox_s0_minvlsteps_ref.SelectedIndex = vlstepsref;
                    comboBox_s0_minvlsteps_subShapeRef.SelectedIndex = vlstepsssref;
                    comboBox_s0_minhosteps_ref.SelectedIndex = hostepsref;
                    comboBox_s0_minhosteps_subShapeRef.SelectedIndex = hostepsssref;
                    comboBox_s0_minvosteps_ref.SelectedIndex = vostepsref;
                    comboBox_s0_minvosteps_subShapeRef.SelectedIndex = vostepsssref;
                    break;
                case 1:
                    comboBox_s1_minhl_ref.SelectedIndex = hlref;
                    comboBox_s1_minhl_subShapeRef.SelectedIndex = hlssref;
                    cb_s1_hl_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.HLRefFinal, ss) == 1;
                    comboBox_s1_minvl_ref.SelectedIndex = vlref;
                    comboBox_s1_minvl_subShapeRef.SelectedIndex = vlssref;
                    cb_s1_vl_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.VLRefFinal, ss) == 1;
                    comboBox_s1_minho_ref.SelectedIndex = horef;
                    comboBox_s1_minho_subShapeRef.SelectedIndex = hossref;
                    cb_s1_ho_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.HORefFinal, ss) == 1;
                    comboBox_s1_minvo_ref.SelectedIndex = voref;
                    comboBox_s1_minvo_subShapeRef.SelectedIndex = vossref;
                    cb_s1_vo_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.VORefFinal, ss) == 1;
                    comboBox_s1_minhlinc_ref.SelectedIndex = hlincref;
                    comboBox_s1_minhlinc_subShapeRef.SelectedIndex = hlincssref;
                    comboBox_s1_minvlinc_ref.SelectedIndex = vlincref;
                    comboBox_s1_minvlinc_subShapeRef.SelectedIndex = vlincssref;
                    comboBox_s1_minhoinc_ref.SelectedIndex = hoincref;
                    comboBox_s1_minhoinc_subShapeRef.SelectedIndex = hoincssref;
                    comboBox_s1_minvoinc_ref.SelectedIndex = voincref;
                    comboBox_s1_minvoinc_subShapeRef.SelectedIndex = voincssref;
                    comboBox_s1_minhlsteps_ref.SelectedIndex = hlstepsref;
                    comboBox_s1_minhlsteps_subShapeRef.SelectedIndex = hlstepsssref;
                    comboBox_s1_minvlsteps_ref.SelectedIndex = vlstepsref;
                    comboBox_s1_minvlsteps_subShapeRef.SelectedIndex = vlstepsssref;
                    comboBox_s1_minhosteps_ref.SelectedIndex = hostepsref;
                    comboBox_s1_minhosteps_subShapeRef.SelectedIndex = hostepsssref;
                    comboBox_s1_minvosteps_ref.SelectedIndex = vostepsref;
                    comboBox_s1_minvosteps_subShapeRef.SelectedIndex = vostepsssref;
                    break;
                case 2:
                    comboBox_s2_minhl_ref.SelectedIndex = hlref;
                    comboBox_s2_minhl_subShapeRef.SelectedIndex = hlssref;
                    cb_s2_hl_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.HLRefFinal, ss) == 1;
                    comboBox_s2_minvl_ref.SelectedIndex = vlref;
                    comboBox_s2_minvl_subShapeRef.SelectedIndex = vlssref;
                    cb_s2_vl_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.VLRefFinal, ss) == 1;
                    comboBox_s2_minho_ref.SelectedIndex = horef;
                    comboBox_s2_minho_subShapeRef.SelectedIndex = hossref;
                    cb_s2_ho_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.HORefFinal, ss) == 1;
                    comboBox_s2_minvo_ref.SelectedIndex = voref;
                    comboBox_s2_minvo_subShapeRef.SelectedIndex = vossref;
                    cb_s2_vo_final.Checked = commonVars.stitcher.getPatternElement(0, index).getInt(PatternElement.properties_i.VORefFinal, ss) == 1;
                    comboBox_s2_minhlinc_ref.SelectedIndex = hlincref;
                    comboBox_s2_minhlinc_subShapeRef.SelectedIndex = hlincssref;
                    comboBox_s2_minvlinc_ref.SelectedIndex = vlincref;
                    comboBox_s2_minvlinc_subShapeRef.SelectedIndex = vlincssref;
                    comboBox_s2_minhoinc_ref.SelectedIndex = hoincref;
                    comboBox_s2_minhoinc_subShapeRef.SelectedIndex = hoincssref;
                    comboBox_s2_minvoinc_ref.SelectedIndex = voincref;
                    comboBox_s2_minvoinc_subShapeRef.SelectedIndex = voincssref;
                    comboBox_s2_minhlsteps_ref.SelectedIndex = hlstepsref;
                    comboBox_s2_minhlsteps_subShapeRef.SelectedIndex = hlstepsssref;
                    comboBox_s2_minvlsteps_ref.SelectedIndex = vlstepsref;
                    comboBox_s2_minvlsteps_subShapeRef.SelectedIndex = vlstepsssref;
                    comboBox_s2_minhosteps_ref.SelectedIndex = hostepsref;
                    comboBox_s2_minhosteps_subShapeRef.SelectedIndex = hostepsssref;
                    comboBox_s2_minvosteps_ref.SelectedIndex = vostepsref;
                    comboBox_s2_minvosteps_subShapeRef.SelectedIndex = vostepsssref;
                    break;
            }
            
        }
        
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

    private void pClearSubShapeVals(int subShapeIndex)
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

    private static void pClampNumeric(ref NumericStepper num, double min, double max)
    {
        num.Value = Math.Max(min, num.Value);
        num.Value = Math.Min(max, num.Value);
    }

    private void pClampSubShape(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
    {
        Application.Instance.Invoke(() =>
        {
            pClampNumeric(ref num_layer_subshape_minhl, minHLength, maxHLength);
            pClampNumeric(ref num_layer_subshape_minvl, minVLength, maxVLength);
            pClampNumeric(ref num_layer_subshape_minho, minHOffset, maxHOffset);
            pClampNumeric(ref num_layer_subshape_minvo, minVOffset, maxVOffset);
        });
    }

    private void pClampSubShape2(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
    {
        Application.Instance.Invoke(() =>
        {
            pClampNumeric(ref num_layer_subshape2_minhl, minHLength, maxHLength);
            pClampNumeric(ref num_layer_subshape2_minvl, minVLength, maxVLength);
            pClampNumeric(ref num_layer_subshape2_minho, minHOffset, maxHOffset);
            pClampNumeric(ref num_layer_subshape2_minvo, minVOffset, maxVOffset);
        });
    }

    private void pClampSubShape3(double minHLength, double maxHLength, double minVLength, double maxVLength, double minHOffset, double maxHOffset, double minVOffset, double maxVOffset)
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