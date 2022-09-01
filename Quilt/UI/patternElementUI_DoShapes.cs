using System;

namespace Quilt;

public partial class MainForm
{
    private void pDoUI_X(int pattern, int index)
    {
        // Validate our settings and clamp the inputs as needed.
        pClampSubShape(minHLength: 0.04, 
            maxHLength: 1000000, 
            minVLength: 0.04, 
            maxVLength: 1000000, 
            minHOffset: -1000000, 
            maxHOffset: 1000000, 
            minVOffset: -1000000, 
            maxVOffset: 1000000
        );

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

        num_layer_subshape3_minhl.Value = 0;
        num_layer_subshape3_minvl.Value = 0;
        num_layer_subshape3_minho.Value = 0;
        num_layer_subshape3_minvo.Value = 0;

        const decimal minSS2VOffset = 1;
        decimal maxSS2VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 1);

        decimal minSS2HOffset = -(commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 1) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0));
        const decimal maxSS2HOffset = -1;

        decimal minSS2HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0) + 2 * 0.01m;
        decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - 2 * 0.01m;
        if (maxSS2VLength < 0)
        {
            maxSS2VLength = 0.02m;
        }

        pClampSubShape2(minHLength: (double)minSS2HLength, 
            maxHLength: 1000000, 
            minVLength: 0.02, 
            maxVLength: (double)maxSS2VLength, 
            minHOffset: (double)minSS2HOffset, 
            maxHOffset: (double)maxSS2HOffset, 
            minVOffset: (double)minSS2VOffset, 
            maxVOffset: (double)maxSS2VOffset
        );

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value), 2);

        num_layer_subshape2_minho.Enabled = true;
        num_layer_subshape2_stepsHO.Enabled = true;
        num_layer_subshape2_incHO.Enabled = true;

        num_layer_subshape2_minvo.Enabled = true;
        num_layer_subshape2_stepsVO.Enabled = true;
        num_layer_subshape2_incVO.Enabled = true;
    }

    private void pDoUI_T(int pattern, int index)
    {
        // Validate our settings and clamp the inputs as needed.
        pClampSubShape(minHLength: 0.01, 
            maxHLength: 1000000, 
            minVLength: 0.04, 
            maxVLength: 1000000, 
            minHOffset: -1000000, 
            maxHOffset: 1000000, 
            minVOffset: -1000000, 
            maxVOffset: 1000000
        );

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

        num_layer_subshape3_minhl.Value = 0;
        num_layer_subshape3_minvl.Value = 0;
        num_layer_subshape3_minho.Value = 0;
        num_layer_subshape3_minvo.Value = 0;

        const decimal minSS2HLength = 0.01m;
        const decimal minSS2VLength = 0.02m;
        decimal ss2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0);
        decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - 2 * 0.01m;
        decimal maxSS2VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 1);

        pClampSubShape2(minHLength: (double)minSS2HLength,
            maxHLength: 1000000,
            minVLength: (double)minSS2VLength,
            maxVLength: (double)maxSS2VLength,
            minHOffset: (double)ss2HOffset,
            maxHOffset: (double)ss2HOffset,
            minVOffset: 1,
            maxVOffset: (double)maxSS2VOffset
        );

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value), 2);

        num_layer_subshape2_minho.Enabled = false;
        num_layer_subshape2_minvo.Enabled = true;
    }

    private void pDoUI_L(int pattern, int index)
    {
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

        num_layer_subshape3_minhl.Value = 0;
        num_layer_subshape3_minvl.Value = 0;
        num_layer_subshape3_minho.Value = 0;
        num_layer_subshape3_minvo.Value = 0;

        const decimal minSS2HLength = 0;
        const decimal minSS2VLength = 0;
        decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0);

        decimal minSS2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0);
        decimal maxSS2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0);
        const decimal minSS2VOffset = 0;
        const decimal maxSS2VOffset = 0;

        pClampSubShape2(minHLength: (double)minSS2HLength,
            maxHLength: 1000000, 
            minVLength: (double)minSS2VLength, 
            maxVLength: (double)maxSS2VLength,
            minHOffset: (double)minSS2HOffset, 
            maxHOffset: (double)maxSS2HOffset,
            minVOffset: (double)minSS2VOffset, 
            maxVOffset: (double)maxSS2VOffset
        );

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value), 2);

        num_layer_subshape2_minho.Enabled = false;
        num_layer_subshape2_stepsHO.Enabled = false;
        num_layer_subshape2_incHO.Enabled = false;

        num_layer_subshape2_minvo.Enabled = false;
        num_layer_subshape2_stepsVO.Enabled = false;
        num_layer_subshape2_incVO.Enabled = false;
    }

    private void pDoUI_U(int pattern, int index)
    {
        // Validate our settings and clamp the inputs as needed.
        pClampSubShape(minHLength: 0.04, 
            maxHLength: 1000000, 
            minVLength: 0.04, 
            maxVLength: 1000000, 
            minHOffset: -1000000, 
            maxHOffset: 1000000, 
            minVOffset: -1000000, 
            maxVOffset: 1000000
        );

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

        num_layer_subshape3_minhl.Value = 0;
        num_layer_subshape3_minvl.Value = 0;
        num_layer_subshape3_minho.Value = 0;
        num_layer_subshape3_minvo.Value = 0;

        const decimal minSS2HLength = 0.01m;
        const decimal minSS2VLength = 0.01m;
        decimal maxSS2HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0) - 0.02m;
        decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - 0.01m;

        decimal ss2HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0)  - (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 1) + 0.01m);
        decimal ss2VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 1);

        pClampSubShape2(minHLength: (double)minSS2HLength,
            maxHLength: (double)maxSS2HLength,
            minVLength: (double)minSS2VLength,
            maxVLength: (double)maxSS2VLength,
            minHOffset: 0.01f, 
            maxHOffset: (double)ss2HOffset,
            minVOffset: (double)ss2VOffset, 
            maxVOffset: (double)ss2VOffset
        );

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value), 2);

        num_layer_subshape2_minho.Enabled = true;
        num_layer_subshape2_stepsHO.Enabled = true;
        num_layer_subshape2_incHO.Enabled = true;

        num_layer_subshape2_minvo.Enabled = false;
        num_layer_subshape2_stepsVO.Enabled = false;
        num_layer_subshape2_incVO.Enabled = false;
    }

    private void pDoUI_S(int pattern, int index)
    {
        // Validate our settings and clamp the inputs as needed.
        pClampSubShape(minHLength: 0.04, 
            maxHLength: 1000000, 
            minVLength: 0.04, 
            maxVLength: 1000000, 
            minHOffset: -1000000, 
            maxHOffset: 1000000, 
            minVOffset: -1000000, 
            maxVOffset: 1000000
        );

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

        const decimal minSS2HLength = 0.01m;
        decimal maxSS2HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0) - 0.01m;
        const decimal minSS2VLength = 0.02m;
        decimal maxSS2VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - 0.01m;
        const decimal ss2HOffset = 0;
        const decimal minSS2VOffset = 0.01m;
        decimal maxSS2VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 1);
        pClampSubShape2(minHLength: (double)minSS2HLength, 
            maxHLength: (double)maxSS2HLength, 
            minVLength: (double)minSS2VLength, 
            maxVLength: (double)maxSS2VLength,
            minHOffset: (double)ss2HOffset,
            maxHOffset: (double)ss2HOffset,
            minVOffset: (double)minSS2VOffset,
            maxVOffset: (double)maxSS2VOffset
        );

        const decimal minSS3HLength = 0.01m;
        decimal maxSS3HLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0) - 0.01m;
        const decimal minSS3VLength = 0.02m;
        decimal maxSS3VLength = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - 0.01m;
        decimal ss3HOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 0) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minHorLength, 2);
        const decimal minSS3VOffset = 0.01m;
        decimal maxSS3VOffset = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 0) - commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getDecimal(PatternElement.properties_decimal.minVerLength, 2);
        pClampSubShape3(minHLength: (double)minSS3HLength,
            maxHLength: (double)maxSS3HLength, 
            minVLength: (double)minSS3VLength, 
            maxVLength: (double)maxSS3VLength,
            minHOffset: (double)ss3HOffset, 
            maxHOffset: (double)ss3HOffset, 
            minVOffset: (double)minSS3VOffset, 
            maxVOffset: (double)maxSS3VOffset
        );

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape2_minhl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape2_incHL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsHL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape2_minvl.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape2_incVL.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape2_stepsVL.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape2_minho.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape2_incHO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsHO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape2_minvo.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape2_incVO.Value), 1);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape2_stepsVO.Value), 1);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorLength, Convert.ToDecimal(num_layer_subshape3_minhl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horLengthInc, Convert.ToDecimal(num_layer_subshape3_incHL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsHL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerLength, Convert.ToDecimal(num_layer_subshape3_minvl.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verLengthInc, Convert.ToDecimal(num_layer_subshape3_incVL.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verLengthSteps, Convert.ToInt32(num_layer_subshape3_stepsVL.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minHorOffset, Convert.ToDecimal(num_layer_subshape3_minho.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.horOffsetInc, Convert.ToDecimal(num_layer_subshape3_incHO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.horOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsHO.Value), 2);

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minVerOffset, Convert.ToDecimal(num_layer_subshape3_minvo.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.verOffsetInc, Convert.ToDecimal(num_layer_subshape3_incVO.Value), 2);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.verOffsetSteps, Convert.ToInt32(num_layer_subshape3_stepsVO.Value), 2);

        // FIXME: Need some logic here to avoid bisection of the S.

        num_layer_subshape2_minho.Enabled = false;
        num_layer_subshape2_stepsHO.Enabled = false;
        num_layer_subshape2_incHO.Enabled = false;
        num_layer_subshape2_minvo.Enabled = true;
        num_layer_subshape2_stepsVO.Enabled = true;
        num_layer_subshape2_incVO.Enabled = true;

        num_layer_subshape3_minho.Enabled = false;
        num_layer_subshape3_stepsHO.Enabled = false;
        num_layer_subshape3_incHO.Enabled = false;
        num_layer_subshape3_minvo.Enabled = true;
        num_layer_subshape3_stepsVO.Enabled = true;
        num_layer_subshape3_incVO.Enabled = true;
    }

    private void pDoUI_bounding(int pattern, int index)
    {
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingLeftSteps, (int)num_layer_bblsteps.Value);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingLeft, Convert.ToDecimal(num_layer_minbbl.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingLeftInc, Convert.ToDecimal(num_layer_bblinc.Value));

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingRightSteps, (int)num_layer_bbrsteps.Value);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingRight, Convert.ToDecimal(num_layer_minbbr.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingRightInc, Convert.ToDecimal(num_layer_bbrinc.Value));

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingTopSteps, (int)num_layer_bbtsteps.Value);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingTop, Convert.ToDecimal(num_layer_minbbt.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingTopInc, Convert.ToDecimal(num_layer_bbtinc.Value));

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.boundingBottomSteps, (int)num_layer_bbbsteps.Value);
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingBottom, Convert.ToDecimal(num_layer_minbbb.Value));
        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.boundingBottomInc, Convert.ToDecimal(num_layer_bbbinc.Value));
    }

    private void pDoUI_complex(int pattern, int index)
    {
        for (int i = 0; i < commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.externalGeoVertexCount); i++)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.externalGeoCoordX, Convert.ToDecimal(num_externalGeoCoordsX[i].Value), listIndex:i);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.externalGeoCoordY, Convert.ToDecimal(num_externalGeoCoordsY[i].Value), listIndex:i);
        }
    }
}