using System.Threading.Tasks;

namespace Quilt
{
    public partial class Pattern
    {
        
        public void computeDimensions(bool takeValues)
        {
#if !QUILTSINGLETHREADED
            Parallel.For(0, patternElements.Count, (i) =>
#else
            for (int i = 0; i < patternElements.Count; i++)
#endif
            {
                pComputeDimensions_HL(i, takeValues);
                pComputeDimensions_VL(i, takeValues);
                pComputeDimensions_HO(i, takeValues);
                pComputeDimensions_VO(i, takeValues);
            }
#if !QUILTSINGLETHREADED
            );
#endif
        }
        
        int pGetDimensionSubshapeIndex(int sRef, PatternElement.properties_decimal value, int subshape)
        {
            int subshaperef = 0;
            
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
                
            }

            return subshaperef;
        }

        decimal pGetDecimalValue(int sRef, int subshaperef, PatternElement.properties_decimal value)
        {
            decimal ret = pGetPatternElement(sRef).getDecimal(value, subshaperef);

            return ret;
        }
        
        void pSetDimensionValue(int i, PatternElement.properties_i refProp, PatternElement.properties_decimal value, int subshape)
        {
            int sRef = pGetRef(i, refProp, subshape);
            
            if (sRef >= 0)
            {
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
        }

        void pComputeDimensions_HL(int i, bool takeValues = false)
        {
            pComputeDimensions(i, PatternElement.properties_i.HLRefFinal, PatternElement.properties_decimal.horLength,
                PatternElement.properties_decimal.minHorLength, PatternElement.properties_i.MinHLRef, PatternElement.properties_i.MinHLSubShapeRef, takeValues);
        }

        void pComputeDimensions_VL(int i, bool takeValues = false)
        {
            pComputeDimensions(i, PatternElement.properties_i.VLRefFinal, PatternElement.properties_decimal.verLength,
                PatternElement.properties_decimal.minVerLength, PatternElement.properties_i.MinVLRef, PatternElement.properties_i.MinVLSubShapeRef, takeValues);
        }

        void pComputeDimensions_HO(int i, bool takeValues = false)
        {
            pComputeDimensions(i, PatternElement.properties_i.HORefFinal, PatternElement.properties_decimal.horOffset,
                PatternElement.properties_decimal.minHorOffset, PatternElement.properties_i.MinHORef, PatternElement.properties_i.MinHOSubShapeRef, takeValues);
        }

        void pComputeDimensions_VO(int i, bool takeValues = false)
        {
            pComputeDimensions(i, PatternElement.properties_i.VORefFinal, PatternElement.properties_decimal.verOffset,
                PatternElement.properties_decimal.minVerOffset, PatternElement.properties_i.MinVORef, PatternElement.properties_i.MinVOSubShapeRef, takeValues);
        }

        void pComputeDimensions(int elementIndex, PatternElement.properties_i useFinalProp,  PatternElement.properties_decimal dimProp, PatternElement.properties_decimal minDimProp, PatternElement.properties_i refProp, PatternElement.properties_i refSS, bool takeValues)
        {
            for (int ss = 0; ss < 3; ss++)
            {
                pSetDimensionValue(elementIndex, refProp, minDimProp, ss);
            }

            if (takeValues)
            {
                // Not terribly elegant, but it does the job. Subtract min dimension from actual dimension to get applied variation, then use this with the final dimension of the reference (element,subshape)
                for (int ss = 0; ss < 3; ss++)
                {
                    int takeFinalDimension = pGetPatternElement(elementIndex).getInt(useFinalProp, ss);
                    int ref_ = pGetPatternElement(elementIndex).getInt(refProp, ss);
                    if ((takeFinalDimension == 1) && (ref_ > 0))
                    {
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
        }
    }
}
