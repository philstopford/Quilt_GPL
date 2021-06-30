using System;

namespace Quilt
{
    public partial class MainForm
    {
        void pDoPatternElementUI_position(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.linkedElementIndex, comboBox_merge.SelectedIndex - 1);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.subShapeIndex, comboBox_subShapeRef.SelectedIndex);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.posIndex, comboBox_posSubShape.SelectedIndex);

            int tmp = comboBox_xPosRef.SelectedIndex;
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosRef, tmp);
            tmp = comboBox_yPosRef.SelectedIndex;
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosRef, tmp);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosSubShapeRef, comboBox_xPos_subShapeRef.SelectedIndex);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosSubShapeRef, comboBox_yPos_subShapeRef.SelectedIndex);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosSubShapeRefPos, comboBox_xPos_subShapeRefPos.SelectedIndex);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosSubShapeRefPos, comboBox_yPos_subShapeRefPos.SelectedIndex);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minXPos, Convert.ToDecimal(num_minXPos.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minYPos, Convert.ToDecimal(num_minYPos.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.xPosInc, Convert.ToDecimal(num_incXPos.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.yPosInc, Convert.ToDecimal(num_incYPos.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.xPosSteps, Convert.ToInt32(num_stepsXPos.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.yPosSteps, Convert.ToInt32(num_stepsYPos.Value));
        }

        void pDoPatternElementUI_rotation(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minRotation, Convert.ToDecimal(num_minRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.rotationInc, Convert.ToDecimal(num_incRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.rotationSteps, Convert.ToInt32(num_stepsRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.rotationRef, comboBox_rotRef.SelectedIndex);

            int rRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.rotationRef) - 1;

            bool rRef_ = false;

            if (rRef >= 0)
            {
                if (rRef >= index)
                {
                    // Re-query index due to active layer screening.
                    rRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.rotationRef);
                }
                rRef_ = (commonVars.stitcher.getPatternElement(patternIndex: pattern, rRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: pattern, rRef).isYArray());
                // Disable if we have a relative array definition.
                rRef_ = rRef_ || (commonVars.stitcher.getPatternElement(patternIndex: 0, rRef).getInt(PatternElement.properties_i.arrayRef) == 1);

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refPivot, (bool)checkBox_refPivot.Checked ? 1 : 0);
            }
            else
            {
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refPivot, 0);

                checkBox_refPivot.Checked = false;
            }

            checkBox_rotRef.Enabled = rRef_;
            // Reset status if required.
            if (!rRef_)
            {
                checkBox_rotRef.Checked = false;
            }

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.rotRefUseArray, (bool)checkBox_rotRef.Checked ? 1 : 0);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refBoundsAfterRotation, (bool)checkBox_refBoundsAfterRotation.Checked ? 1 : 0);
        }

        void pDoPatternElementUI_transform(int pattern, int index)
        {
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.flipH, (bool)checkBox_flipH.Checked ? 1 : 0);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.flipV, (bool)checkBox_flipV.Checked ? 1 : 0);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.alignX, (bool)checkBox_alignX.Checked ? 1 : 0);
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.alignY, (bool)checkBox_alignY.Checked ? 1 : 0);
        }

        void pDoPatternElementUI_array(int pattern, int index)
        {
            bool isArray = false;
            bool isRelativeArray = false;

            bool bounding = (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.shapeIndex) == (int)CommonVars.shapeNames.bounding);

            // Prevent any array offerings for bounding elements.
            if (!bounding)
            {
                isArray = (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: pattern, index).isYArray());

                if (!isArray)
                {
                    isRelativeArray = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.arrayRef) > 0;
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
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.relativeArray, isRelativeArray ? 1 : 0);

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
            checkBox_refArrayPivot.Enabled = isArray || isRelativeArray;

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayMinXCount, Convert.ToInt32(num_arrayMinXCount.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayMinYCount, Convert.ToInt32(num_arrayMinYCount.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayXInc, Convert.ToInt32(num_arrayXInc.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayYInc, Convert.ToInt32(num_arrayYInc.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayXSteps, Convert.ToInt32(num_arrayXSteps.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayYSteps, Convert.ToInt32(num_arrayYSteps.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayMinXSpace, Convert.ToDecimal(num_arrayMinXSpace.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayMinYSpace, Convert.ToDecimal(num_arrayMinYSpace.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayXSpaceInc, Convert.ToDecimal(num_arrayXSpaceInc.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayYSpaceInc, Convert.ToDecimal(num_arrayYSpaceInc.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayXSpaceSteps, Convert.ToInt32(num_arrayXSpaceSteps.Value));
            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayYSpaceSteps, Convert.ToInt32(num_arrayYSpaceSteps.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.minArrayRotation, Convert.ToDecimal(num_minArrayRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setDecimal(PatternElement.properties_decimal.arrayRotationInc, Convert.ToDecimal(num_incArrayRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRotationSteps, Convert.ToInt32(num_stepsArrayRot.Value));

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRotationRef, comboBox_arrayRotRef.SelectedIndex);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRef, comboBox_arrayRef.SelectedIndex);

            int aRRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.arrayRotationRef) - 1;

            bool rRefArray = false;
            if (aRRef >= 0)
            {
                if (aRRef >= index)
                {
                    // Fix index based on omission of active layer.
                    aRRef = commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.arrayRotationRef);
                }
                rRefArray = (commonVars.stitcher.getPatternElement(patternIndex: pattern, aRRef).isXArray() || commonVars.stitcher.getPatternElement(patternIndex: pattern, aRRef).isYArray());
                // Disable if we have a relative array definition.
                rRefArray = rRefArray || (commonVars.stitcher.getPatternElement(patternIndex: 0, aRRef).getInt(PatternElement.properties_i.arrayRef) == 1);

                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refArrayPivot, (bool)checkBox_refArrayPivot.Checked ? 1 : 0);
            }
            else
            {
                commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refArrayPivot, 0);

                checkBox_refArrayPivot.Checked = false;
            }

            checkBox_arrayRotRef.Enabled = rRefArray;
            // Reset status if required.
            if (!rRefArray)
            {
                checkBox_arrayRotRef.Checked = false;
            }

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.arrayRotRefUseArray, (bool)checkBox_arrayRotRef.Checked ? 1 : 0);

            commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.refArrayBoundsAfterRotation, (bool)checkBox_refArrayBoundsAfterRotation.Checked ? 1 : 0);
        }
    }
}