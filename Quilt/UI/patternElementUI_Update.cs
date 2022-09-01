using Eto.Forms;
using System;
using System.Collections.ObjectModel;
using shapeEngine;

namespace Quilt;

public partial class MainForm
{
    private void pUpdatePatternElementUI_subshape(int index)
    {
        // Use 0 index to find the base configuration pattern to display.
        comboBox_patternElementShape.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.shapeIndex);
        if (comboBox_patternElementShape.SelectedIndex == 0)
        {
            pClearPatternElementUI();
            comboBox_patternElementShape.Visible = true;
        }

        groupBox_properties.Content = groupBox_subShapes_table;
        groupBox_position.Visible = true;

        for (int subshapeindex = 0; subshapeindex < 3; subshapeindex++)
        {
            pMinHLRefSubShapeList_update(index, subshapeindex);
            pMinVLRefSubShapeList_update(index, subshapeindex);

            pMinHORefSubShapeList_update(index, subshapeindex);
            pMinVORefSubShapeList_update(index, subshapeindex);

            pHLIncRefSubShapeList_update(index, subshapeindex);
            pVLIncRefSubShapeList_update(index, subshapeindex);

            pHOIncRefSubShapeList_update(index, subshapeindex);
            pVOIncRefSubShapeList_update(index, subshapeindex);

            pHLStepsRefSubShapeList_update(index, subshapeindex);
            pVLStepsRefSubShapeList_update(index, subshapeindex);

            pHOStepsRefSubShapeList_update(index, subshapeindex);
            pVOStepsRefSubShapeList_update(index, subshapeindex);
        }

        switch (commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.shapeIndex))
        {
            case (int)CentralProperties.shapeNames.none:
                pClearSubShapeVals(2);
                pClearSubShapeVals(1);
                pClearSubShapeVals(0);
                break;
            case (int)CentralProperties.shapeNames.rect:
            case (int)CentralProperties.shapeNames.text:
                pClearSubShapeVals(2);
                pClearSubShapeVals(1);
                pSetSubShapeVals(index, 0);
                break;
            case (int)CentralProperties.shapeNames.Lshape:
            case (int)CentralProperties.shapeNames.Tshape:
            case (int)CentralProperties.shapeNames.Ushape:
            case (int)CentralProperties.shapeNames.Xshape:
                pClearSubShapeVals(2);
                pSetSubShapeVals(index, 0);
                pSetSubShapeVals(index, 1);
                break;
            case (int)CentralProperties.shapeNames.Sshape:
                pSetSubShapeVals(index, 0);
                pSetSubShapeVals(index, 1);
                pSetSubShapeVals(index, 2);
                break;
            case (int)CentralProperties.shapeNames.bounding:
                pSetBoundingShapeVals(index);
                break;
            case (int)CentralProperties.shapeNames.complex:
                // Iterate our edges. For each edge, add a row with a label, and numeric fields
                pExternalGeoUI(index);

                // Replace properties groupbox content with external layout content.
                Expander s = new() {Content = groupBox_layout_table, Header = "Edges", Expanded = false};
                groupBox_properties.Content = s;
                break;
        }

        pSetTipVals(index);
        pUpdateLBContextMenu();
    }

    private void pMinHLRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLRefSubShapeList, PatternElement.properties_decimal.minHorLength, PatternElement.properties_i.MinHLRef, PatternElement.properties_i.MinHLSubShapeRef, comboBox_s0_minhl_subShapeRef, num_layer_subshape_minhl);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLRefSubShape2List, PatternElement.properties_decimal.minHorLength, PatternElement.properties_i.MinHLRef, PatternElement.properties_i.MinHLSubShapeRef, comboBox_s1_minhl_subShapeRef, num_layer_subshape2_minhl);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLRefSubShape3List, PatternElement.properties_decimal.minHorLength, PatternElement.properties_i.MinHLRef, PatternElement.properties_i.MinHLSubShapeRef, comboBox_s2_minhl_subShapeRef, num_layer_subshape3_minhl);
                break;
        }
    }

    private void pMinVLRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLRefSubShapeList, PatternElement.properties_decimal.minVerLength, PatternElement.properties_i.MinVLRef, PatternElement.properties_i.MinVLSubShapeRef, comboBox_s0_minvl_subShapeRef, num_layer_subshape_minvl);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLRefSubShape2List, PatternElement.properties_decimal.minVerLength, PatternElement.properties_i.MinVLRef, PatternElement.properties_i.MinVLSubShapeRef, comboBox_s1_minvl_subShapeRef, num_layer_subshape2_minvl);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLRefSubShape3List, PatternElement.properties_decimal.minVerLength, PatternElement.properties_i.MinVLRef, PatternElement.properties_i.MinVLSubShapeRef, comboBox_s2_minvl_subShapeRef, num_layer_subshape3_minvl);
                break;
        }
    }

    private void pMinHORefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minHORefSubShapeList, PatternElement.properties_decimal.minHorOffset, PatternElement.properties_i.MinHORef, PatternElement.properties_i.MinHOSubShapeRef, comboBox_s0_minho_subShapeRef, num_layer_subshape_minho);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minHORefSubShape2List, PatternElement.properties_decimal.minHorOffset, PatternElement.properties_i.MinHORef, PatternElement.properties_i.MinHOSubShapeRef, comboBox_s1_minho_subShapeRef, num_layer_subshape2_minho);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minHORefSubShape3List, PatternElement.properties_decimal.minHorOffset, PatternElement.properties_i.MinHORef, PatternElement.properties_i.MinHOSubShapeRef, comboBox_s2_minho_subShapeRef, num_layer_subshape3_minho);
                break;
        }
    }

    private void pMinVORefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minVORefSubShapeList, PatternElement.properties_decimal.minVerOffset, PatternElement.properties_i.MinVORef, PatternElement.properties_i.MinVOSubShapeRef, comboBox_s0_minvo_subShapeRef, num_layer_subshape_minvo);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minVORefSubShape2List, PatternElement.properties_decimal.minVerOffset, PatternElement.properties_i.MinVORef, PatternElement.properties_i.MinVOSubShapeRef, comboBox_s1_minvo_subShapeRef, num_layer_subshape2_minvo);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minVORefSubShape3List, PatternElement.properties_decimal.minVerOffset, PatternElement.properties_i.MinVORef, PatternElement.properties_i.MinVOSubShapeRef, comboBox_s2_minvo_subShapeRef, num_layer_subshape3_minvo);
                break;
        }
    }

    private void pHLStepsRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLStepsRefSubShapeList, PatternElement.properties_i.horLengthSteps, PatternElement.properties_i.HLStepsRef, PatternElement.properties_i.HLStepsSubShapeRef, comboBox_s0_minhlsteps_subShapeRef, num_layer_subshape_stepsHL);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLStepsRefSubShape2List, PatternElement.properties_i.horLengthSteps, PatternElement.properties_i.HLStepsRef, PatternElement.properties_i.HLStepsSubShapeRef, comboBox_s1_minhlsteps_subShapeRef, num_layer_subshape2_stepsHL);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLStepsRefSubShape3List, PatternElement.properties_i.horLengthSteps, PatternElement.properties_i.HLStepsRef, PatternElement.properties_i.HLStepsSubShapeRef, comboBox_s2_minhlsteps_subShapeRef, num_layer_subshape3_stepsHL);
                break;
        }
    }

    private void pVLStepsRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLStepsRefSubShapeList, PatternElement.properties_i.verLengthSteps, PatternElement.properties_i.VLStepsRef, PatternElement.properties_i.VLStepsSubShapeRef, comboBox_s0_minvlsteps_subShapeRef, num_layer_subshape_stepsVL);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLStepsRefSubShape2List, PatternElement.properties_i.verLengthSteps, PatternElement.properties_i.VLStepsRef, PatternElement.properties_i.VLStepsSubShapeRef, comboBox_s1_minvlsteps_subShapeRef, num_layer_subshape2_stepsVL);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLStepsRefSubShape3List, PatternElement.properties_i.verLengthSteps, PatternElement.properties_i.VLStepsRef, PatternElement.properties_i.VLStepsSubShapeRef, comboBox_s2_minvlsteps_subShapeRef, num_layer_subshape3_stepsVL);
                break;
        }
    }

    private void pHOStepsRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minHOStepsRefSubShapeList, PatternElement.properties_i.horOffsetSteps, PatternElement.properties_i.HOStepsRef, PatternElement.properties_i.HOStepsSubShapeRef, comboBox_s0_minhosteps_subShapeRef, num_layer_subshape_stepsHO);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minHOStepsRefSubShape2List, PatternElement.properties_i.horOffsetSteps, PatternElement.properties_i.HOStepsRef, PatternElement.properties_i.HOStepsSubShapeRef, comboBox_s1_minhosteps_subShapeRef, num_layer_subshape2_stepsHO);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minHOStepsRefSubShape3List, PatternElement.properties_i.horOffsetSteps, PatternElement.properties_i.HOStepsRef, PatternElement.properties_i.HOStepsSubShapeRef, comboBox_s2_minhosteps_subShapeRef, num_layer_subshape3_stepsHO);
                break;
        }
    }

    private void pVOStepsRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minVOStepsRefSubShapeList, PatternElement.properties_i.verOffsetSteps, PatternElement.properties_i.VOStepsRef, PatternElement.properties_i.VOStepsSubShapeRef, comboBox_s0_minvosteps_subShapeRef, num_layer_subshape_stepsVO);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minVOStepsRefSubShape2List, PatternElement.properties_i.verOffsetSteps, PatternElement.properties_i.VOStepsRef, PatternElement.properties_i.VOStepsSubShapeRef, comboBox_s1_minvosteps_subShapeRef, num_layer_subshape2_stepsVO);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minVOStepsRefSubShape3List, PatternElement.properties_i.verOffsetSteps, PatternElement.properties_i.VOStepsRef, PatternElement.properties_i.VOStepsSubShapeRef, comboBox_s2_minvosteps_subShapeRef, num_layer_subshape3_stepsVO);
                break;
        }
    }

    private void pHLIncRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLIncRefSubShapeList, PatternElement.properties_decimal.horLengthInc, PatternElement.properties_i.HLIncRef, PatternElement.properties_i.HLIncSubShapeRef, comboBox_s0_minhlinc_subShapeRef, num_layer_subshape_incHL);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLIncRefSubShape2List, PatternElement.properties_decimal.horLengthInc, PatternElement.properties_i.HLIncRef, PatternElement.properties_i.HLIncSubShapeRef, comboBox_s1_minhlinc_subShapeRef, num_layer_subshape2_incHL);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minHLIncRefSubShape3List, PatternElement.properties_decimal.horLengthInc, PatternElement.properties_i.HLIncRef, PatternElement.properties_i.HLIncSubShapeRef, comboBox_s2_minhlinc_subShapeRef, num_layer_subshape3_incHL);
                break;
        }
    }

    private void pVLIncRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLIncRefSubShapeList, PatternElement.properties_decimal.verLengthInc, PatternElement.properties_i.VLIncRef, PatternElement.properties_i.VLIncSubShapeRef, comboBox_s0_minvlinc_subShapeRef, num_layer_subshape_incVL);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLIncRefSubShape2List, PatternElement.properties_decimal.verLengthInc, PatternElement.properties_i.VLIncRef, PatternElement.properties_i.VLIncSubShapeRef, comboBox_s1_minvlinc_subShapeRef, num_layer_subshape2_incVL);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minVLIncRefSubShape3List, PatternElement.properties_decimal.verLengthInc, PatternElement.properties_i.VLIncRef, PatternElement.properties_i.VLIncSubShapeRef, comboBox_s2_minvlinc_subShapeRef, num_layer_subshape3_incVL);
                break;
        }
    }

    private void pHOIncRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minHOIncRefSubShapeList, PatternElement.properties_decimal.horOffsetInc, PatternElement.properties_i.HOIncRef, PatternElement.properties_i.HOIncSubShapeRef, comboBox_s0_minhoinc_subShapeRef, num_layer_subshape_incHO);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minHOIncRefSubShape2List, PatternElement.properties_decimal.horOffsetInc, PatternElement.properties_i.HOIncRef, PatternElement.properties_i.HOIncSubShapeRef, comboBox_s1_minhoinc_subShapeRef, num_layer_subshape2_incHO);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minHOIncRefSubShape3List, PatternElement.properties_decimal.horOffsetInc, PatternElement.properties_i.HOIncRef, PatternElement.properties_i.HOIncSubShapeRef, comboBox_s2_minhoinc_subShapeRef, num_layer_subshape3_incHO);
                break;
        }
    }

    private void pVOIncRefSubShapeList_update(int index, int subshapeindex)
    {
        switch (subshapeindex)
        {
            case 0:
                pSetReferenceValues(index, subshapeindex, commonVars.minVOIncRefSubShapeList, PatternElement.properties_decimal.verOffsetInc, PatternElement.properties_i.VOIncRef, PatternElement.properties_i.VOIncSubShapeRef, comboBox_s0_minvoinc_subShapeRef, num_layer_subshape_incVO);
                break;
            case 1:
                pSetReferenceValues(index, subshapeindex, commonVars.minVOIncRefSubShape2List, PatternElement.properties_decimal.verOffsetInc, PatternElement.properties_i.VOIncRef, PatternElement.properties_i.VOIncSubShapeRef, comboBox_s1_minvoinc_subShapeRef, num_layer_subshape2_incVO);
                break;
            case 2:
                pSetReferenceValues(index, subshapeindex, commonVars.minVOIncRefSubShape3List, PatternElement.properties_decimal.verOffsetInc, PatternElement.properties_i.VOIncRef, PatternElement.properties_i.VOIncSubShapeRef, comboBox_s2_minvoinc_subShapeRef, num_layer_subshape3_incVO);
                break;
        }
    }

    private void pSetReferenceValues(int index, int subshapeindex, ObservableCollection<string> uilist, PatternElement.properties_i targetValue, PatternElement.properties_i propertyIndex, PatternElement.properties_i subShapeIndex, DropDown cb, NumericStepper ns)
    {
        uilist.Clear();
        uilist.Add("1");
            
        int val = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(targetValue, subshapeindex);

        int refElement = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(propertyIndex, subshapeindex);

        bool refActive = refElement != 0;
            
        if (refActive)
        {
            if (refElement <= index)
            {
                refElement--;
            }
            int refSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, refElement).getSubShapeCount();

            for (int i = 1; i < refSubShapeCount; i++)
            {
                uilist.Add((i + 1).ToString());
            }

            int _subShapeRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(subShapeIndex, subshapeindex);

            cb.SelectedIndex = _subShapeRef;

            val = pGetReferenceValue(refElement, targetValue, _subShapeRef);
                
        }
        else
        {
            cb.SelectedIndex = 0;
        }
        ns.Value = Convert.ToInt32(val);
        commonVars.stitcher.getPatternElement(patternIndex: 0, index).setInt(targetValue, val, subshapeindex);
    }

    private void pSetReferenceValues(int index, int subshapeindex, ObservableCollection<string> uilist, PatternElement.properties_decimal targetValue, PatternElement.properties_i propertyIndex, PatternElement.properties_i subShapeIndex, DropDown cb, NumericStepper ns)
    {
        uilist.Clear();
        uilist.Add("1");
            
        decimal val = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(targetValue, subshapeindex);

        int refElement = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(propertyIndex, subshapeindex);

        bool refActive = refElement != 0;
            
        if (refActive)
        {
            if (refElement <= index)
            {
                refElement--;
            }
            int refSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, refElement).getSubShapeCount();

            for (int i = 1; i < refSubShapeCount; i++)
            {
                uilist.Add((i + 1).ToString());
            }

            int _subShapeRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(subShapeIndex, subshapeindex);

            // Safety check in case our reference element shifted under us.
            if (_subShapeRef >= refSubShapeCount)
            {
                _subShapeRef = 0;
                commonVars.stitcher.getPatternElement(patternIndex: 0, index).setInt(subShapeIndex, _subShapeRef, subshapeindex);
            }
                
            cb.SelectedIndex = _subShapeRef;

            val = pGetReferenceValue(refElement, targetValue, _subShapeRef);

        }
        else
        {
            cb.SelectedIndex = 0;
        }
        ns.Value = Convert.ToDouble(val);
        commonVars.stitcher.getPatternElement(patternIndex: 0, index).setDecimal(targetValue, val, subshapeindex);
    }

    private decimal pGetReferenceValue(int refElement, PatternElement.properties_decimal prop, int subshapeindex)
    {
        return commonVars.stitcher
            .getPatternElement(patternIndex: 0, refElement)
            .getDecimal(prop, subshapeindex);
    }

    private int pGetReferenceValue(int refElement, PatternElement.properties_i prop, int subshapeindex)
    {
        return commonVars.stitcher
            .getPatternElement(patternIndex: 0, refElement)
            .getInt(prop, subshapeindex);
    }

    private void pTipRefSubShapeList_update(int index)
    {
        commonVars.tipRefSubShapeList.Clear();
        commonVars.tipRefSubShapeList.Add("1");
        int tipRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipRef, 0);

        //comboBox_s0_tip_ref.Enabled = tipRef != 0;
        //comboBox_s0_tip_subShapeRef.Enabled = tipRef != 0;

        if (tipRef != 0)
        {
            if (tipRef <= index)
            {
                tipRef--;
            }
            int tipRefSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, tipRef).getSubShapeCount();

            for (int i = 1; i < tipRefSubShapeCount; i++)
            {
                commonVars.tipRefSubShapeList.Add((i + 1).ToString());
            }

            comboBox_s0_tip_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipSubShapeRef, 0);
        }
        else
        {
            comboBox_s0_tip_subShapeRef.SelectedIndex = 0;
        }
    }

    private void pTipRefSubShape2List_update(int index)
    {
        commonVars.tipRefSubShape2List.Clear();
        commonVars.tipRefSubShape2List.Add("1");
        int tipRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipRef, 1);

        // comboBox_s1_tip_ref.Enabled = tipRef != 0;
        // comboBox_s1_tip_subShapeRef.Enabled = tipRef != 0;

        if (tipRef != 0)
        {
            if (tipRef <= index)
            {
                tipRef--;
            }
            int tipRefSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, tipRef).getSubShapeCount();

            for (int i = 1; i < tipRefSubShapeCount; i++)
            {
                commonVars.tipRefSubShape2List.Add((i + 1).ToString());
            }

            comboBox_s1_tip_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipSubShapeRef, 1);
        }
        else
        {
            comboBox_s1_tip_subShapeRef.SelectedIndex = 0;
        }
    }

    private void pTipRefSubShape3List_update(int index)
    {
        commonVars.tipRefSubShape3List.Clear();
        commonVars.tipRefSubShape3List.Add("1");
        int tipRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipRef, 2);

        // comboBox_s2_tip_ref.Enabled = tipRef != 0;
        // comboBox_s2_tip_subShapeRef.Enabled = tipRef != 0;

        if (tipRef != 0)
        {
            if (tipRef <= index)
            {
                tipRef--;
            }
            int tipRefSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, tipRef).getSubShapeCount();

            for (int i = 1; i < tipRefSubShapeCount; i++)
            {
                commonVars.tipRefSubShape3List.Add((i + 1).ToString());
            }

            comboBox_s2_tip_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.tipSubShapeRef, 2);
        }
        else
        {
            comboBox_s2_tip_subShapeRef.SelectedIndex = 0;
        }
    }

    private void pXPosRefSubShapeList_update(int index)
    {
        commonVars.xPosRefSubShapeList.Clear();
        commonVars.xPosRefSubShapeList.Add("1");
        int xPosRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosRef);

        comboBox_xPos_subShapeRef.Enabled = xPosRef != 0;
        comboBox_xPos_subShapeRefPos.Enabled = xPosRef != 0;

        if (xPosRef != 0)
        {
            if (xPosRef <= index)
            {
                xPosRef--;
            }
            int xRefSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, xPosRef).getSubShapeCount();

            for (int i = 1; i < xRefSubShapeCount; i++)
            {
                commonVars.xPosRefSubShapeList.Add((i + 1).ToString());
            }

            bool array = commonVars.stitcher.getPatternElement(patternIndex: 0, xPosRef).isXArray();
            // Relative array definition?
            array = array || commonVars.stitcher.getPatternElement(patternIndex: 0, xPosRef).getInt(PatternElement.properties_i.arrayRef) != 0;

            if (array)
            {
                commonVars.xPosRefSubShapeList.Add("Array");
            }
            comboBox_xPos_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosSubShapeRef);
            comboBox_xPos_subShapeRefPos.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosSubShapeRefPos);
        }
        else
        {
            comboBox_xPos_subShapeRef.SelectedIndex = 0;
            comboBox_xPos_subShapeRefPos.SelectedIndex = (int)ShapeSettings.subShapeHorLocs.L;
        }
    }

    private void pYPosRefSubShapeList_update(int index)
    {
        commonVars.yPosRefSubShapeList.Clear();
        commonVars.yPosRefSubShapeList.Add("1");
        int yPosRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosRef);

        comboBox_yPos_subShapeRef.Enabled = yPosRef != 0;
        comboBox_yPos_subShapeRefPos.Enabled = yPosRef != 0;

        if (yPosRef != 0)
        {
            if (yPosRef <= index)
            {
                yPosRef--;
            }
            int yRefSubShapeCount = commonVars.stitcher.getPatternElement(patternIndex: 0, yPosRef).getSubShapeCount();

            for (int i = 1; i < yRefSubShapeCount; i++)
            {
                commonVars.yPosRefSubShapeList.Add((i + 1).ToString());
            }

            bool array = commonVars.stitcher.getPatternElement(patternIndex: 0, yPosRef).isYArray();
            // Relative array definition?
            array = array || commonVars.stitcher.getPatternElement(patternIndex: 0, yPosRef).getInt(PatternElement.properties_i.arrayRef) != 0;

            if (array)
            {
                commonVars.yPosRefSubShapeList.Add("Array");
            }
            comboBox_yPos_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosSubShapeRef);
            comboBox_yPos_subShapeRefPos.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosSubShapeRefPos);
        }
        else
        {
            comboBox_yPos_subShapeRef.SelectedIndex = 0;
            comboBox_yPos_subShapeRefPos.SelectedIndex = (int)ShapeSettings.subShapeVerLocs.B;
        }
    }

    private void pUpdatePatternElementUI_positionX(int index)
    {
        comboBox_xPosRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosRef);
        // Sort out the subshape combobox contents.
        pXPosRefSubShapeList_update(index);
    }

    private void pUpdatePatternElementUI_positionY(int index)
    {
        comboBox_yPosRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosRef);
        // Sort out the subshape combobox contents.
        pYPosRefSubShapeList_update(index);
    }

    private void pUpdatePatternElementUI_position(int index)
    {
        comboBox_merge.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.linkedElementIndex) + 1;

        comboBox_subShapeRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.subShapeIndex);
        comboBox_posSubShape.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.posIndex);

        // Set the X and Y position references.
        pUpdatePatternElementUI_positionX(index);
        pUpdatePatternElementUI_positionY(index);

        num_minXPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minXPos));
        num_minYPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minYPos));

        num_incXPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.xPosInc));
        num_incYPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.yPosInc));

        num_stepsXPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.xPosSteps));
        num_stepsYPos.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.yPosSteps));
        
        // Update find buttons.
        btn_mergeRef.Enabled = commonVars.stitcher.getPatternElement(0, index)
            .getInt(PatternElement.properties_i.linkedElementIndex) > -1;

    }

    private void pUpdatePatternElementUI_rotation(int index)
    {
        num_minRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minRotation));

        num_incRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.rotationInc));

        num_stepsRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotationSteps));

        comboBox_rotRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotationRef);

        int rotRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotationRef);
        bool rRef = false;

        rB_rotPivot_ref.Enabled = false;
        rB_rotPivot_ref.Checked = false;

        if (commonVars.stitcher.getPatternElement(patternIndex:0, index).getInt(PatternElement.properties_i.relativeArray) == 1 || commonVars.stitcher.getPatternElement(patternIndex: 0, index).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: 0, index).isYArray())
        {
            checkBox_refBoundsAfterRotation.Enabled = true;
            checkBox_refBoundsAfterRotation.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refBoundsAfterRotation) == 1;
        }
        else
        {
            checkBox_refBoundsAfterRotation.Enabled = false;
            checkBox_refBoundsAfterRotation.Checked = false;
        }
        if (rotRef > 0)
        {
            if (rotRef <= index)
            {
                rotRef--;
            }

            rB_rotPivot_ref.Enabled = true;
            rB_rotPivot_ref.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refPivot) == 1;

            checkBox_refBoundsAfterRotation.Enabled = true;
            checkBox_refBoundsAfterRotation.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refBoundsAfterRotation) == 1;
                
            rRef = commonVars.stitcher.getPatternElement(patternIndex: 0, rotRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: 0, rotRef).isYArray();
            // Relative array definition?
            rRef = rRef || commonVars.stitcher.getPatternElement(patternIndex: 0, rotRef).getInt(PatternElement.properties_i.arrayRef) != 0;
        }

        if (!rRef)
        {
            checkBox_rotRef.Checked = false;
        }
        else
        {
            checkBox_rotRef.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.rotRefUseArray) == 1;
        }
        checkBox_rotRef.Enabled = rRef;

        switch (commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refPivot))
        {
            default:
                rB_rotPivot_self.Checked = true;
                break;
            case 1:
                rB_rotPivot_ref.Checked = true;
                break;
            case 2:
                rB_rotPivot_worldOrigin.Checked = true;
                break;
        }

    }

    private void pUpdatePatternElementUI_transform(int index)
    {
        checkBox_flipH.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.flipH) == 1;
        checkBox_flipV.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.flipV) == 1;
        checkBox_alignX.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.alignX) == 1;
        checkBox_alignY.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.alignY) == 1;
    }

    private void pUpdatePatternElementUI_array(int index)
    {
        bool isArray = false;
        bool isRelativeArray = false;

        bool bounding = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.shapeIndex) == (int)CentralProperties.shapeNames.bounding;

        // Prevent any array offerings for bounding elements.
        if (!bounding)
        {
            isArray = commonVars.stitcher.getPatternElement(patternIndex: 0, index).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: 0, index).isYArray();

            if (!isArray)
            {
                isRelativeArray = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRef) > 0;
            }
        }

        num_arrayMinXCount.Enabled = !bounding && !isRelativeArray;
        num_arrayMinYCount.Enabled = !bounding && !isRelativeArray;
        num_arrayXInc.Enabled = !bounding && !isRelativeArray;
        num_arrayYInc.Enabled = !bounding && !isRelativeArray;
        num_arrayXSteps.Enabled = !bounding && !isRelativeArray;
        num_arrayYSteps.Enabled = !bounding && !isRelativeArray;
        num_arrayMinXSpace.Enabled = !bounding && !isRelativeArray;
        num_arrayMinYSpace.Enabled = !bounding && !isRelativeArray;
        num_arrayXSpaceInc.Enabled = !bounding && !isRelativeArray;
        num_arrayYSpaceInc.Enabled = !bounding && !isRelativeArray;
        num_arrayXSpaceSteps.Enabled = !bounding && !isRelativeArray;
        num_arrayYSpaceSteps.Enabled = !bounding && !isRelativeArray;

        // Register the relative array status with the pattern element.
        commonVars.stitcher.getPatternElement(patternIndex: 0, index).setInt(PatternElement.properties_i.relativeArray, isRelativeArray ? 1 : 0);

        num_minArrayRot.Enabled = isArray || isRelativeArray;
        num_incArrayRot.Enabled = isArray || isRelativeArray;
        num_stepsArrayRot.Enabled = isArray || isRelativeArray;

        num_arrayMinXSpace.Enabled = isArray || isRelativeArray;
        num_arrayXSpaceInc.Enabled = isArray || isRelativeArray;
        num_arrayXSpaceSteps.Enabled = isArray || isRelativeArray;

        num_arrayMinYSpace.Enabled = isArray || isRelativeArray;
        num_arrayYSpaceInc.Enabled = isArray || isRelativeArray;
        num_arrayYSpaceSteps.Enabled = isArray || isRelativeArray;

        comboBox_arrayRotRef.Enabled = isArray || isRelativeArray;
        rB_arrayRotPivot_ref.Enabled = isArray || isRelativeArray;

        num_arrayMinXCount.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayMinXCount);
        num_arrayMinYCount.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayMinYCount);
        num_arrayXInc.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayXInc);
        num_arrayYInc.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayYInc);
        num_arrayXSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayXSteps);
        num_arrayYSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayYSteps);
        num_arrayMinXSpace.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayMinXSpace));
        num_arrayMinYSpace.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayMinYSpace));
        num_arrayXSpaceInc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayXSpaceInc));
        num_arrayYSpaceInc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayYSpaceInc));
        num_arrayXSpaceSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayXSpaceSteps);
        num_arrayYSpaceSteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayYSpaceSteps);
        comboBox_arrayRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRef);
        num_minArrayRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.minArrayRotation));
        num_incArrayRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.arrayRotationInc));
        num_stepsArrayRot.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationSteps));
        comboBox_arrayRotRef.SelectedIndex = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationRef);

        // Self and 'World Origin'
        int arrayRotRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationRef);
        bool rArrayRef = false;

        rB_arrayRotPivot_ref.Enabled = false;
        rB_arrayRotPivot_ref.Checked = false;

        checkBox_refArrayBoundsAfterRotation.Enabled = false;
        checkBox_refArrayBoundsAfterRotation.Checked = false;

        if (arrayRotRef >= 0)
        {
            rB_arrayRotPivot_ref.Enabled = true;
            rB_arrayRotPivot_ref.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refArrayPivot) == 1;

            checkBox_refArrayBoundsAfterRotation.Enabled = true;
            checkBox_refArrayBoundsAfterRotation.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refArrayBoundsAfterRotation) == 1;

            if (arrayRotRef >= index)
            {
                // Index needs to be re-queried as the current layer is omitted from the list.
                arrayRotRef = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotationRef);
            }

            rArrayRef = commonVars.stitcher.getPatternElement(patternIndex: 0, arrayRotRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: 0, arrayRotRef).isYArray();
            // Relative array definition?
            rArrayRef = rArrayRef || commonVars.stitcher.getPatternElement(patternIndex: 0, arrayRotRef).getInt(PatternElement.properties_i.arrayRef) != 0;
        }

        if (!rArrayRef)
        {
            checkBox_arrayRotRef.Checked = false;
        }
        else
        {
            checkBox_arrayRotRef.Checked = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.arrayRotRefUseArray) == 1;
        }
        checkBox_arrayRotRef.Enabled = rArrayRef;
        
        switch (commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.refArrayPivot))
        {
            default:
                rB_arrayRotPivot_self.Checked = true;
                break;
            case 1:
                rB_arrayRotPivot_ref.Checked = true;
                break;
            case 2:
                rB_arrayRotPivot_worldOrigin.Checked = true;
                break;
        }

    }

    private void pUpdatePatternElementUI(bool doPreview = true)
    {
        if (UIFreeze)
        {
            return;
        }
        int index = listBox_entries.SelectedIndex;
        if (index == -1)
        {
            if (commonVars.stitcher.patternElementNames.Count >= 1)
            {
                listBox_entries.SelectedIndex = 0;
                return;
            }
            pClearPatternElementUI();
            return;
        }

        // Otherwise, we need to configure the UI based on the selected element and update the values.
        UIFreeze = true;
        // commonVars.stitcher.updateQuilt(); // ensure we're up to date. Commented out as it was breaking things and we have a later call somewhere that catches this. Leaving in case of problems, though.
        // Update our UI observable collections for relative positioning.
        commonVars.stitcher.update_filteredPatternedElementNames(index);

        pUpdatePatternElementUI_subshape(index);

        pTipRefSubShapeList_update(index);
        pTipRefSubShape2List_update(index);
        pTipRefSubShape3List_update(index);

        pUpdatePatternElementUI_position(index);

        pUpdatePatternElementUI_transform(index);

        pUpdatePatternElementUI_rotation(index);

        pUpdatePatternElementUI_array(index);

        UIFreeze = false;
        pDoPatternElementUI(0, updateUI: true, doPreview);

        btn_export.Enabled = commonVars.stitcher.getPatternCount() > 0;
    }

    private void pSetBoundingShapeVals(int index)
    {
        // This should always be called under UIFreeze!
        if (!UIFreeze)
        {
            Error.ErrorReporter.showMessage_OK("Coding error: setBoundingShapeVals called without freeze", "Oops");
        }
        num_layer_minbbl.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingLeft));
        num_layer_bblinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingLeftInc));
        num_layer_bblsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingLeftSteps);

        num_layer_minbbr.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingRight));
        num_layer_bbrinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingRightInc));
        num_layer_bbrsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingRightSteps);

        num_layer_minbbt.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingTop));
        num_layer_bbtinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingTopInc));
        num_layer_bbtsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingTopSteps);

        num_layer_minbbb.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingBottom));
        num_layer_bbbinc.Value = Convert.ToDouble(commonVars.stitcher.getPatternElement(patternIndex: 0, index).getDecimal(PatternElement.properties_decimal.boundingBottomInc));
        num_layer_bbbsteps.Value = commonVars.stitcher.getPatternElement(patternIndex: 0, index).getInt(PatternElement.properties_i.boundingBottomSteps);
    }
        
}