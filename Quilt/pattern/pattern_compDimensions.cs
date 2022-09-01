using System.Threading.Tasks;
using shapeEngine;

namespace Quilt;

public partial class Pattern
{
        
    public void computeDimensions(bool takeValues)
    {
#if !QUILTSINGLETHREADED
        Parallel.For(0, patternElements.Count, i =>
#else
            for (int i = 0; i < patternElements.Count; i++)
#endif
            {
                pComputeTips(i, takeValues);
                pComputeDimensions_HL(i, takeValues);
                pComputeDimensions_VL(i, takeValues);
                pComputeDimensions_HO(i, takeValues);
                pComputeDimensions_VO(i, takeValues);
                pComputeDimensions_HT(i, takeValues);
                pComputeDimensions_VT(i, takeValues);
            }
#if !QUILTSINGLETHREADED
        );
#endif
    }

    private int pGetDimensionSubshapeIndex(int sRef, PatternElement.properties_decimal value, int subshape)
    {
        int subshaperef;
            
        switch (value)
        {
            case PatternElement.properties_decimal.minHorLength:
            case PatternElement.properties_decimal.horLength:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.MinHLSubShapeRef, subshape);
                break;
            case PatternElement.properties_decimal.minVerLength:
            case PatternElement.properties_decimal.verLength:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.MinVLSubShapeRef, subshape);
                break;
            case PatternElement.properties_decimal.minHorOffset:
            case PatternElement.properties_decimal.horOffset:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.MinHOSubShapeRef, subshape);
                break;
            case PatternElement.properties_decimal.minVerOffset:
            case PatternElement.properties_decimal.verOffset:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.MinVOSubShapeRef, subshape);
                break;
            case PatternElement.properties_decimal.horLengthInc:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.HLIncSubShapeRef, subshape);
                break;
            case PatternElement.properties_decimal.verLengthInc:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.VLIncSubShapeRef, subshape);
                break;
            case PatternElement.properties_decimal.horOffsetInc:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.HOIncSubShapeRef, subshape);
                break;
            case PatternElement.properties_decimal.verOffsetInc:
                subshaperef = pGetPatternElement(sRef).getInt(PatternElement.properties_i.VOIncSubShapeRef, subshape);
                break;
            default:
                subshaperef = 0;
                break;
        }

        return subshaperef;
    }

    private decimal pGetDecimalValue(int sRef, int subshaperef, PatternElement.properties_decimal value)
    {
        decimal ret = pGetPatternElement(sRef).getDecimal(value, subshaperef);

        return ret;
    }

    private decimal pGetDecimalValue(int sRef, int subshaperef, ShapeSettings.properties_decimal value)
    {
        decimal ret = pGetPatternElement(sRef).getDecimal(value, subshaperef);

        return ret;
    }

    private void pSetDimensionValue(int i, PatternElement.properties_i refProp, PatternElement.properties_decimal value, int subshape)
    {
        int sRef = pGetRef(i, refProp, subshape);

        if (sRef < 0)
        {
            return;
        }

        int source = i;

        decimal val = pGetPatternElement(i).getDecimal(value, subshape);

        bool cyclical = pCyclicalCheck(sRef, refProp, subshape);

        if (!cyclical)
        {
            while (sRef >= 0)
            {
                // This is kind of ugly, but I don't see a better approach yet.
                int subshaperef = pGetDimensionSubshapeIndex(source, value, subshape);

                val = pGetDecimalValue(sRef, subshaperef, value);
                source = sRef;
                sRef = pGetRef(sRef, refProp, subshape);
            }
        }
                
        pGetPatternElement(i).setDecimal(value, val, subshape);
    }

    private void pSetDimensionValue(int i, PatternElement.properties_i refProp, ShapeSettings.properties_decimal value, int subshape)
    {
        int sRef = pGetRef(i, refProp, subshape);

        if (sRef < 0)
        {
            return;
        }

        decimal val = pGetPatternElement(i).getDecimal(value, subshape);

        bool cyclical = pCyclicalCheck(sRef, refProp, subshape);

        if (!cyclical)
        {
            while (sRef >= 0)
            {
                // This is kind of ugly, but I don't see a better approach yet.
                int subshaperef = 0;

                val = pGetDecimalValue(sRef, subshaperef, value);
                sRef = pGetRef(sRef, refProp, subshape);
            }
        }
                
        pGetPatternElement(i).setDecimal(value, val, subshape);
    }

    private void pComputeDimensions_HL(int i, bool takeValues = false)
    {
        pComputeDimensions(i, PatternElement.properties_i.HLRefFinal, PatternElement.properties_decimal.horLength,
            PatternElement.properties_decimal.minHorLength, PatternElement.properties_i.MinHLRef, PatternElement.properties_i.MinHLSubShapeRef, takeValues);
    }

    private void pComputeDimensions_VL(int i, bool takeValues = false)
    {
        pComputeDimensions(i, PatternElement.properties_i.VLRefFinal, PatternElement.properties_decimal.verLength,
            PatternElement.properties_decimal.minVerLength, PatternElement.properties_i.MinVLRef, PatternElement.properties_i.MinVLSubShapeRef, takeValues);
    }

    private void pComputeDimensions_HT(int i, bool takeValues = false)
    {
        pComputeTipDimensions(i, PatternElement.properties_i.HTRefFinal, ShapeSettings.properties_decimal.hTBias,
            PatternElement.properties_decimal.minHorTipLength, PatternElement.properties_i.MinHTRef, takeValues);
    }

    private void pComputeDimensions_VT(int i, bool takeValues = false)
    {
        pComputeTipDimensions(i, PatternElement.properties_i.VTRefFinal, ShapeSettings.properties_decimal.vTBias,
            PatternElement.properties_decimal.minVerTipLength, PatternElement.properties_i.MinVTRef, takeValues);
    }

    private void pComputeDimensions_HO(int i, bool takeValues = false)
    {
        pComputeDimensions(i, PatternElement.properties_i.HORefFinal, PatternElement.properties_decimal.horOffset,
            PatternElement.properties_decimal.minHorOffset, PatternElement.properties_i.MinHORef, PatternElement.properties_i.MinHOSubShapeRef, takeValues);
    }

    private void pComputeDimensions_VO(int i, bool takeValues = false)
    {
        pComputeDimensions(i, PatternElement.properties_i.VORefFinal, PatternElement.properties_decimal.verOffset,
            PatternElement.properties_decimal.minVerOffset, PatternElement.properties_i.MinVORef, PatternElement.properties_i.MinVOSubShapeRef, takeValues);
    }

    private void pComputeDimensions(int elementIndex, PatternElement.properties_i useFinalProp,  PatternElement.properties_decimal dimProp, PatternElement.properties_decimal minDimProp, PatternElement.properties_i refProp, PatternElement.properties_i refSS, bool takeValues)
    {
        for (int ss = 0; ss < 3; ss++)
        {
            pSetDimensionValue(elementIndex, refProp, minDimProp, ss);
        }

        if (!takeValues)
        {
            return;
        }

        {
            // Not terribly elegant, but it does the job. Subtract min dimension from actual dimension to get applied variation, then use this with the final dimension of the reference (element,subshape)
            for (int ss = 0; ss < 3; ss++)
            {
                int takeFinalDimension = pGetPatternElement(elementIndex).getInt(useFinalProp, ss);
                int ref_ = pGetPatternElement(elementIndex).getInt(refProp, ss);
                if (takeFinalDimension != 1 || ref_ <= 0)
                {
                    continue;
                }

                if (ref_ <= elementIndex)
                {
                    ref_--;
                }
                int ssref = pGetPatternElement(elementIndex).getInt(refSS, ss);
                        
                // Any cascading references.....
                int nestedRef = pGetPatternElement(ref_).getInt(refProp, ssref);
                while (nestedRef > 0)
                {
                    if (nestedRef <= ref_)
                    {
                        nestedRef--; // Offset due to missing index for current element.
                    }
                    ref_ = nestedRef;
                    ssref = pGetPatternElement(ref_).getInt(refSS, ssref);
                    nestedRef = pGetPatternElement(ref_).getInt(refProp, ssref);
                }
                        
                decimal variation = pGetPatternElement(elementIndex).getDecimal(dimProp, ss) -
                                    pGetPatternElement(elementIndex).getDecimal(minDimProp, ss);
                pSetDimensionValue(elementIndex, refProp, dimProp, ss);
                decimal newValue = pGetPatternElement(ref_).getDecimal(dimProp, ssref);
                newValue += variation;
                pGetPatternElement(elementIndex).setDecimal(dimProp, newValue, ss);
            }
        }
    }

    private void pComputeTips(int elementIndex, bool takeValues)
    {
        if (!takeValues)
        {
            return;
        }
        
        for (int ss = 0; ss < 3; ss++)
        {
            int ref_ = pGetPatternElement(elementIndex).getInt(PatternElement.properties_i.tipRef, ss);
            if (ref_ <= 0)
            {
                continue;
            }
            
            if (ref_ <= elementIndex)
            {
                ref_--;
            }

            int ssref_ = pGetPatternElement(elementIndex).getInt(PatternElement.properties_i.tipSubShapeRef, ss);

            // Any cascading references.....
            int nestedRef = pGetPatternElement(ref_).getInt(PatternElement.properties_i.tipRef, ssref_);
            int nestedRefSS = pGetPatternElement(ref_).getInt(PatternElement.properties_i.tipSubShapeRef, ssref_);
            while (nestedRef > 0)
            {
                if (nestedRef <= ref_)
                {
                    nestedRef--; // Offset due to missing index for current element.
                }

                ref_ = nestedRef;
                ssref_ = nestedRefSS;
                nestedRef = pGetPatternElement(ref_).getInt(PatternElement.properties_i.tipRef, ssref_);
            }

            int newValue = 0;
            switch (ssref_)
            {
                case 0:
                    newValue = pGetPatternElement(ref_).getInt(PatternElement.properties_i.shape0Tip);
                    break;
                case 1:
                    newValue = pGetPatternElement(ref_).getInt(PatternElement.properties_i.shape1Tip);
                    break;
                case 2:
                    newValue = pGetPatternElement(ref_).getInt(PatternElement.properties_i.shape2Tip);
                    break;
            }

            switch (ss)
            {
                case 0:
                    pGetPatternElement(elementIndex).setInt(PatternElement.properties_i.shape0Tip, newValue);
                    break;
                case 1:
                    pGetPatternElement(elementIndex).setInt(PatternElement.properties_i.shape1Tip, newValue);
                    break;
                case 2:
                    pGetPatternElement(elementIndex).setInt(PatternElement.properties_i.shape2Tip, newValue);
                    break;
            }
        }
    }

    private void pComputeTipDimensions(int elementIndex, PatternElement.properties_i useFinalProp,  ShapeSettings.properties_decimal dimProp, PatternElement.properties_decimal minDimProp, PatternElement.properties_i refProp, bool takeValues)
    {
        pSetDimensionValue(elementIndex, refProp, minDimProp, 0);
        pSetDimensionValue(elementIndex, refProp, dimProp, 0);

        if (!takeValues)
        {
            return;
        }

        // Not terribly elegant, but it does the job. Subtract min dimension from actual dimension to get applied variation, then use this with the final dimension of the reference (element,subshape)
        {
            int takeFinalDimension = pGetPatternElement(elementIndex).getInt(useFinalProp);
            int ref_ = pGetPatternElement(elementIndex).getInt(refProp);
            if (takeFinalDimension != 1 || ref_ <= 0)
            {
            }
            else
            {
                if (ref_ <= elementIndex)
                {
                    ref_--;
                }

                // Any cascading references.....
                int nestedRef = pGetPatternElement(ref_).getInt(refProp);
                while (nestedRef > 0)
                {
                    if (nestedRef <= ref_)
                    {
                        nestedRef--; // Offset due to missing index for current element.
                    }

                    ref_ = nestedRef;
                    nestedRef = pGetPatternElement(ref_).getInt(refProp);
                }

                decimal variation = pGetPatternElement(elementIndex).getDecimal(dimProp) -
                                    pGetPatternElement(elementIndex).getDecimal(minDimProp);
                pSetDimensionValue(elementIndex, refProp, dimProp, 0);
                decimal newValue = pGetPatternElement(ref_).getDecimal(dimProp);
                newValue += variation;
                pGetPatternElement(elementIndex).setDecimal(dimProp, newValue);
            }
        }
    }
}