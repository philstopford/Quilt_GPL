using geoLib;
using System;

namespace Quilt
{
    public class PatternElement
    {
        public enum position { BL, TL, BR, TR, BS, TS, LS, RS, C }
        public override bool Equals(object obj)
        {
            return getDescription() == ((PatternElement)obj).getDescription();
        }

        public override int GetHashCode()
        {
            return pGetHashCode();
        }

        int pGetHashCode()
        {
            return pGetDescription().GetHashCode();
        }

        static Int32 default_shapeIndex = (Int32)CentralProperties.typeShapes.none;

        static decimal default_rotation = 0;
        static decimal default_subShapeHorLength = 0;
        static decimal default_subShapeHorOffset = 0;
        static decimal default_subShapeVerLength = 0;
        static decimal default_subShapeVerOffset = 0;
        static Int32 default_subShapeTipLocIndex = 0;
        static decimal default_subShape2HorLength = 0;
        static decimal default_subShape2HorOffset = 0;
        static decimal default_subShape2VerLength = 0;
        static decimal default_subShape2VerOffset = 0;
        static Int32 default_subShape2TipLocIndex = 0;
        static decimal default_subShape3HorLength = 0;
        static decimal default_subShape3HorOffset = 0;
        static decimal default_subShape3VerLength = 0;
        static decimal default_subShape3VerOffset = 0;
        static Int32 default_subShape3TipLocIndex = 0;
        static Int32 default_subShapeRefIndex = 0;
        static Int32 default_posInSubShapeIndex = (Int32)CommonVars.subShapeLocations.BL;
        static Int32 default_posXRefIndex = 0;
        static Int32 default_posYRefIndex = 0;
        static Int32 default_rotRefIndex = 0;
        static Int32 default_subShapeRef = 0;
        static Int32 defaultSteps = 1;
        static Int32 defaultArrayCount = 1;

        public PatternElement()
        {
            newPE();
        }

        void newPE()
        {
            name = "";
            subShapeMinHorLength = default_subShapeHorLength;
            subShapeMinVerLength = default_subShapeVerLength;
            subShapeMinHorOffset = default_subShapeHorOffset;
            subShapeMinVerOffset = default_subShapeVerOffset;
            subShapeHorLengthInc = 0;
            subShapeVerLengthInc = 0;
            subShapeHorOffsetInc = 0;
            subShapeVerOffsetInc = 0;
            subShapeTipLocIndex = default_subShapeTipLocIndex;
            subShape2MinHorLength = default_subShape2HorLength;
            subShape2MinVerLength = default_subShape2VerLength;
            subShape2MinHorOffset = default_subShape2HorOffset;
            subShape2MinVerOffset = default_subShape2VerOffset;
            subShape2HorLengthInc = 0;
            subShape2VerLengthInc = 0;
            subShape2HorOffsetInc = 0;
            subShape2VerOffsetInc = 0;
            subShape2TipLocIndex = default_subShape2TipLocIndex;
            subShape3MinHorLength = default_subShape3HorLength;
            subShape3MinVerLength = default_subShape3VerLength;
            subShape3MinHorOffset = default_subShape3HorOffset;
            subShape3MinVerOffset = default_subShape3VerOffset;
            subShape3HorLengthInc = 0;
            subShape3VerLengthInc = 0;
            subShape3HorOffsetInc = 0;
            subShape3VerOffsetInc = 0;
            subShape3TipLocIndex = default_subShape3TipLocIndex;
            subShapeRefIndex = default_subShapeRefIndex;
            posInSubShapeIndex = default_posInSubShapeIndex;
            minXPos = 0;
            minYPos = 0;
            xPosRef = default_posXRefIndex;
            yPosRef = default_posYRefIndex;
            xPosSteps = defaultSteps;
            yPosSteps = defaultSteps;
            xPosInc = 0;
            yPosInc = 0;
            s0HorLengthSteps = defaultSteps;
            s0HorOffsetSteps = defaultSteps;
            s0VerLengthSteps = defaultSteps;
            s0VerOffsetSteps = defaultSteps;
            s1HorLengthSteps = defaultSteps;
            s1HorOffsetSteps = defaultSteps;
            s1VerLengthSteps = defaultSteps;
            s1VerOffsetSteps = defaultSteps;
            s2HorLengthSteps = defaultSteps;
            s2HorOffsetSteps = defaultSteps;
            s2VerLengthSteps = defaultSteps;
            s2VerOffsetSteps = defaultSteps;
            xPosSubShapeRef = default_subShapeRefIndex;
            yPosSubShapeRef = default_subShapeRefIndex;
            xPosSubShapeRefPos = (int)CommonVars.subShapeHorLocs.L;
            yPosSubShapeRefPos = (int)CommonVars.subShapeVerLocs.B;

            boundingLeft = 0;
            boundingLeftInc = 0;
            boundingLeftSteps = defaultSteps;
            boundingRight = 0;
            boundingRightInc = 0;
            boundingRightSteps = defaultSteps;
            boundingBottom = 0;
            boundingBottomInc = 0;
            boundingBottomSteps = defaultSteps;
            boundingTop = 0;
            boundingTopInc = 0;
            boundingTopSteps = defaultSteps;

            minRotation = default_rotation;
            rotationSteps = defaultSteps;
            rotRef = default_rotRefIndex;
            rotRefUseArray = 0;

            flipH = 0;
            flipV = 0;
            alignX = 1;
            alignY = 1;

            variantCounter = 0;

            arrayXCount = defaultArrayCount;
            arrayXSpace = 0;
            arrayYCount = defaultArrayCount;
            arrayYSpace = 0;

            arrayRotation = 0;
            arrayRotationSteps = defaultSteps;
            arrayRotRef = default_rotRefIndex;
            arrayRotRefUseArray = 0;

            refPivot = 0;
            refArrayPivot = 0;
            refBoundsAfterRotation = 0;
            refArrayBoundsAfterRotation = 0;

            relativeArray = 0;
        }

        public PatternElement(PatternElement source)
        {
            newPE(source);
        }

        void newPE(PatternElement source)
        {
            name = source.name;
            shapeIndex = source.shapeIndex;
            subShapeRefIndex = source.subShapeRefIndex;
            posInSubShapeIndex = source.posInSubShapeIndex;

            xPosSteps = source.xPosSteps;
            yPosSteps = source.yPosSteps;

            xPosRef = source.xPosRef;
            yPosRef = source.yPosRef;

            xPosSubShapeRef = source.xPosSubShapeRef;
            yPosSubShapeRef = source.yPosSubShapeRef;

            xPosSubShapeRefPos = source.xPosSubShapeRefPos;
            yPosSubShapeRefPos = source.yPosSubShapeRefPos;

            minXPos = source.minXPos;
            minYPos = source.minYPos;

            xPosInc = source.xPosInc;
            yPosInc = source.yPosInc;

            subShapeMinHorLength = source.subShapeMinHorLength;
            s0HorLengthSteps = source.s0HorLengthSteps;

            subShapeMinVerLength = source.subShapeMinVerLength;
            s0VerLengthSteps = source.s0VerLengthSteps;

            subShapeMinHorOffset = source.subShapeMinHorOffset;
            s0HorOffsetSteps = source.s0HorOffsetSteps;

            subShapeMinVerOffset = source.subShapeMinVerOffset;
            s0VerOffsetSteps = source.s0VerOffsetSteps;

            subShapeTipLocIndex = source.subShapeTipLocIndex;

            subShape2MinHorLength = source.subShape2MinHorLength;
            s1HorLengthSteps = source.s1HorLengthSteps;

            subShape2MinVerLength = source.subShape2MinVerLength;
            s1VerLengthSteps = source.s1VerLengthSteps;

            subShape2MinHorOffset = source.subShape2MinHorOffset;
            s1HorOffsetSteps = source.s1HorOffsetSteps;

            subShape2MinVerOffset = source.subShape2MinVerOffset;
            s1VerOffsetSteps = source.s1VerOffsetSteps;

            subShape2TipLocIndex = source.subShape2TipLocIndex;

            subShape3MinHorLength = source.subShape3MinHorLength;
            s2HorLengthSteps = source.s2HorLengthSteps;

            subShape3MinVerLength = source.subShape3MinVerLength;
            s2VerLengthSteps = source.s2VerLengthSteps;

            subShape3MinHorOffset = source.subShape3MinHorOffset;
            s2HorOffsetSteps = source.s2HorOffsetSteps;

            subShape3MinVerOffset = source.subShape3MinVerOffset;
            s2VerOffsetSteps = source.s2VerOffsetSteps;

            subShape3TipLocIndex = source.subShape3TipLocIndex;

            variantCounter = 0;

            x = source.x;
            y = source.y;

            subShapeHorLengthInc = source.subShapeHorLengthInc;
            subShapeHorLength = source.subShapeHorLength;
            subShapeVerLengthInc = source.subShapeVerLengthInc;
            subShapeVerLength = source.subShapeVerLength;
            subShapeHorOffsetInc = source.subShapeHorOffsetInc;
            subShapeHorOffset = source.subShapeHorOffset;
            subShapeVerOffsetInc = source.subShapeVerOffsetInc;
            subShapeVerOffset = source.subShapeVerOffset;

            subShape2HorLengthInc = source.subShape2HorLengthInc;
            subShape2HorLength = source.subShape2HorLength;
            subShape2VerLengthInc = source.subShape2VerLengthInc;
            subShape2VerLength = source.subShape2VerLength;
            subShape2HorOffsetInc = source.subShape2HorOffsetInc;
            subShape2HorOffset = source.subShape2HorOffset;
            subShape2VerOffsetInc = source.subShape2VerOffsetInc;
            subShape2VerOffset = source.subShape2VerOffset;

            subShape3HorLengthInc = source.subShape3HorLengthInc;
            subShape3HorLength = source.subShape3HorLength;
            subShape3VerLengthInc = source.subShape3VerLengthInc;
            subShape3VerLength = source.subShape3VerLength;
            subShape3HorOffsetInc = source.subShape3HorOffsetInc;
            subShape3HorOffset = source.subShape3HorOffset;
            subShape3VerOffsetInc = source.subShape3VerOffsetInc;
            subShape3VerOffset = source.subShape3VerOffset;

            boundingLeft = source.boundingLeft;
            boundingLeftInc = source.boundingLeftInc;
            boundingLeftSteps = source.boundingLeftSteps;

            boundingRight = source.boundingRight;
            boundingRightInc = source.boundingRightInc;
            boundingRightSteps = source.boundingRightSteps;

            boundingBottom = source.boundingBottom;
            boundingBottomInc = source.boundingBottomInc;
            boundingBottomSteps = source.boundingBottomSteps;

            boundingTop = source.boundingTop;
            boundingTopInc = source.boundingTopInc;
            boundingTopSteps = source.boundingTopSteps;

            minRotation = source.minRotation;

            rotation = source.rotation;
            rotationInc = source.rotationInc;
            rotationSteps = source.rotationSteps;
            rotRef = source.rotRef;
            rotRefUseArray = source.rotRefUseArray;

            flipH = source.flipH;
            flipV = source.flipV;
            alignX = source.alignX;
            alignY = source.alignY;

            arrayXCount = source.arrayXCount;
            arrayXSpace = source.arrayXSpace;
            arrayYCount = source.arrayYCount;
            arrayYSpace = source.arrayYSpace;

            minArrayRotation = source.minArrayRotation;

            arrayRotation = source.arrayRotation;
            arrayRotationInc = source.arrayRotationInc;
            arrayRotationSteps = source.arrayRotationSteps;
            arrayRotRef = source.arrayRotRef;
            arrayRotRefUseArray = source.arrayRotRefUseArray;

            arrayRef = source.arrayRef;
            relativeArray = source.relativeArray;

            refPivot = source.refPivot;
            refArrayPivot = source.refArrayPivot;
            refBoundsAfterRotation = source.refBoundsAfterRotation;
            refArrayBoundsAfterRotation = source.refArrayBoundsAfterRotation;
        }

        string name;

        public enum properties_s
        {
            name
        }

        public string getString(properties_s p)
        {
            return pGetString(p);
        }

        string pGetString(properties_s p)
        {
            string ret = "";
            switch (p)
            {
                case properties_s.name:
                    ret = name;
                    break;
            }

            return ret;
        }

        public void setString(properties_s p, string val)
        {
            pSetString(p, val);
        }

        void pSetString(properties_s p, string val)
        {
            switch (p)
            {
                case properties_s.name:
                    name = val;
                    break;
            }
        }

        Int32 shapeIndex;
        Int32 subShapeRefIndex, posInSubShapeIndex;
        Int32 subShapeTipLocIndex, subShape2TipLocIndex, subShape3TipLocIndex; // unused.
        Int32 xPosSteps, yPosSteps;
        Int32 xPosRef, yPosRef;
        Int32 xPosSubShapeRef, yPosSubShapeRef, xPosSubShapeRefPos, yPosSubShapeRefPos;
        Int32 s0HorLengthSteps, s0VerLengthSteps, s0HorOffsetSteps, s0VerOffsetSteps;
        Int32 s1HorLengthSteps, s1VerLengthSteps, s1HorOffsetSteps, s1VerOffsetSteps;
        Int32 s2HorLengthSteps, s2VerLengthSteps, s2HorOffsetSteps, s2VerOffsetSteps;
        Int32 rotationSteps;
        Int32 boundingLeftSteps, boundingRightSteps, boundingTopSteps, boundingBottomSteps;
        Int32 rotRef, rotRefUseArray;
        Int32 flipH, flipV, alignX, alignY;
        Int32 arrayXCount, arrayYCount;
        Int32 arrayRotationSteps, arrayRotRef, arrayRotRefUseArray;
        Int32 arrayRef;
        Int32 refPivot, refArrayPivot;
        Int32 refBoundsAfterRotation, refArrayBoundsAfterRotation;
        Int32 relativeArray;

        public bool isXArray()
        {
            return pIsXArray();
        }

        bool pIsXArray()
        {
            bool ret = (arrayXCount > 1);
            return ret;
        }

        public bool isYArray()
        {
            return pIsYArray();
        }

        bool pIsYArray()
        {
            bool ret = (arrayYCount > 1);
            return ret;
        }

        public enum properties_i
        {
            shapeIndex, shape0Tip, shape1Tip, shape2Tip, subShapeIndex, posIndex, xPosRef, yPosRef, xPosSteps, yPosSteps,
            xPosSubShapeRef, yPosSubShapeRef, xPosSubShapeRefPos, yPosSubShapeRefPos,
            s0HorLengthSteps, s0VerLengthSteps, s0HorOffsetSteps, s0VerOffsetSteps,
            s1HorLengthSteps, s1VerLengthSteps, s1HorOffsetSteps, s1VerOffsetSteps,
            s2HorLengthSteps, s2VerLengthSteps, s2HorOffsetSteps, s2VerOffsetSteps,
            rotationSteps, rotationRef, rotRefUseArray,
            boundingLeftSteps, boundingRightSteps, boundingTopSteps, boundingBottomSteps,
            maxVariants,
            flipH, flipV, alignX, alignY,
            arrayXCount, arrayYCount,
            arrayRotationSteps, arrayRotationRef, arrayRotRefUseArray,
            arrayRef,
            refPivot, refArrayPivot,
            refBoundsAfterRotation, refArrayBoundsAfterRotation,
            relativeArray
        }

        public Int32 getSubShapeCount()
        {
            return pGetSubShapeCount();
        }

        Int32 pGetSubShapeCount()
        {
            return ShapeLibrary.getSubShapeCount(shapeIndex);
        }

        public static Int32 getSubShapeCount(int shapeIndex_)
        {
            return pGetSubShapeCount(shapeIndex_);
        }

        static Int32 pGetSubShapeCount(int shapeIndex_)
        {
            return ShapeLibrary.getSubShapeCount(shapeIndex_);
        }

        public Int32 getInt(properties_i p)
        {
            return pGetInt(p);
        }

        Int32 pGetInt(properties_i p)
        {
            int ret = 0;
            switch (p)
            {
                case properties_i.shapeIndex:
                    ret = shapeIndex;
                    break;
                case properties_i.shape0Tip:
                    ret = subShapeTipLocIndex;
                    break;
                case properties_i.shape1Tip:
                    ret = subShape2TipLocIndex;
                    break;
                case properties_i.shape2Tip:
                    ret = subShape3TipLocIndex;
                    break;
                case properties_i.subShapeIndex:
                    ret = subShapeRefIndex;
                    break;
                case properties_i.posIndex:
                    ret = posInSubShapeIndex;
                    break;
                case properties_i.xPosRef:
                    ret = xPosRef;
                    break;
                case properties_i.yPosRef:
                    ret = yPosRef;
                    break;
                case properties_i.xPosSteps:
                    ret = xPosSteps;
                    break;
                case properties_i.yPosSteps:
                    ret = yPosSteps;
                    break;
                case properties_i.s0HorLengthSteps:
                    ret = s0HorLengthSteps;
                    break;
                case properties_i.s0HorOffsetSteps:
                    ret = s0HorOffsetSteps;
                    break;
                case properties_i.s0VerLengthSteps:
                    ret = s0VerLengthSteps;
                    break;
                case properties_i.s0VerOffsetSteps:
                    ret = s0VerOffsetSteps;
                    break;
                case properties_i.s1HorLengthSteps:
                    ret = s1HorLengthSteps;
                    break;
                case properties_i.s1HorOffsetSteps:
                    ret = s1HorOffsetSteps;
                    break;
                case properties_i.s1VerLengthSteps:
                    ret = s1VerLengthSteps;
                    break;
                case properties_i.s1VerOffsetSteps:
                    ret = s1VerOffsetSteps;
                    break;
                case properties_i.s2HorLengthSteps:
                    ret = s2HorLengthSteps;
                    break;
                case properties_i.s2HorOffsetSteps:
                    ret = s2HorOffsetSteps;
                    break;
                case properties_i.s2VerLengthSteps:
                    ret = s2VerLengthSteps;
                    break;
                case properties_i.s2VerOffsetSteps:
                    ret = s2VerOffsetSteps;
                    break;
                case properties_i.xPosSubShapeRef:
                    ret = xPosSubShapeRef;
                    break;
                case properties_i.yPosSubShapeRef:
                    ret = yPosSubShapeRef;
                    break;
                case properties_i.xPosSubShapeRefPos:
                    ret = xPosSubShapeRefPos;
                    break;
                case properties_i.yPosSubShapeRefPos:
                    ret = yPosSubShapeRefPos;
                    break;
                case properties_i.rotationSteps:
                    ret = rotationSteps;
                    break;
                case properties_i.rotationRef:
                    ret = rotRef;
                    break;
                case properties_i.rotRefUseArray:
                    ret = rotRefUseArray;
                    break;
                case properties_i.maxVariants:
                    ret = calcMaxVariants();
                    break;
                case properties_i.flipH:
                    ret = flipH;
                    break;
                case properties_i.flipV:
                    ret = flipV;
                    break;
                case properties_i.alignX:
                    ret = alignX;
                    break;
                case properties_i.alignY:
                    ret = alignY;
                    break;
                case properties_i.arrayXCount:
                    ret = arrayXCount;
                    break;
                case properties_i.arrayYCount:
                    ret = arrayYCount;
                    break;
                case properties_i.boundingLeftSteps:
                    ret = boundingLeftSteps;
                    break;
                case properties_i.boundingRightSteps:
                    ret = boundingRightSteps;
                    break;
                case properties_i.boundingBottomSteps:
                    ret = boundingBottomSteps;
                    break;
                case properties_i.boundingTopSteps:
                    ret = boundingTopSteps;
                    break;
                case properties_i.arrayRotationSteps:
                    ret = arrayRotationSteps;
                    break;
                case properties_i.arrayRotationRef:
                    ret = arrayRotRef;
                    break;
                case properties_i.arrayRef:
                    ret = arrayRef;
                    break;
                case properties_i.arrayRotRefUseArray:
                    ret = arrayRotRefUseArray;
                    break;
                case properties_i.refPivot:
                    ret = refPivot;
                    break;
                case properties_i.refArrayPivot:
                    ret = refArrayPivot;
                    break;
                case properties_i.refBoundsAfterRotation:
                    ret = refBoundsAfterRotation;
                    break;
                case properties_i.refArrayBoundsAfterRotation:
                    ret = refArrayBoundsAfterRotation;
                    break;
                case properties_i.relativeArray:
                    ret = relativeArray;
                    break;
            }

            return ret;
        }

        public void setInt(properties_i p, int val)
        {
            pSetInt(p, val);
        }

        void pSetInt(properties_i p, int val)
        {
            switch (p)
            {
                case properties_i.shapeIndex:
                    shapeIndex = val;
                    break;
                case properties_i.shape0Tip:
                    subShapeTipLocIndex = val;
                    break;
                case properties_i.shape1Tip:
                    subShape2TipLocIndex = val;
                    break;
                case properties_i.shape2Tip:
                    subShape3TipLocIndex = val;
                    break;
                case properties_i.subShapeIndex:
                    subShapeRefIndex = val;
                    break;
                case properties_i.posIndex:
                    posInSubShapeIndex = val;
                    break;
                case properties_i.xPosRef:
                    xPosRef = val;
                    break;
                case properties_i.yPosRef:
                    yPosRef = val;
                    break;
                case properties_i.xPosSteps:
                    xPosSteps = val;
                    break;
                case properties_i.yPosSteps:
                    yPosSteps = val;
                    break;
                case properties_i.s0HorLengthSteps:
                    s0HorLengthSteps = val;
                    break;
                case properties_i.s0HorOffsetSteps:
                    s0HorOffsetSteps = val;
                    break;
                case properties_i.s0VerLengthSteps:
                    s0VerLengthSteps = val;
                    break;
                case properties_i.s0VerOffsetSteps:
                    s0VerOffsetSteps = val;
                    break;
                case properties_i.s1HorLengthSteps:
                    s1HorLengthSteps = val;
                    break;
                case properties_i.s1HorOffsetSteps:
                    s1HorOffsetSteps = val;
                    break;
                case properties_i.s1VerLengthSteps:
                    s1VerLengthSteps = val;
                    break;
                case properties_i.s1VerOffsetSteps:
                    s1VerOffsetSteps = val;
                    break;
                case properties_i.s2HorLengthSteps:
                    s2HorLengthSteps = val;
                    break;
                case properties_i.s2HorOffsetSteps:
                    s2HorOffsetSteps = val;
                    break;
                case properties_i.s2VerLengthSteps:
                    s2VerLengthSteps = val;
                    break;
                case properties_i.s2VerOffsetSteps:
                    s2VerOffsetSteps = val;
                    break;
                case properties_i.xPosSubShapeRef:
                    xPosSubShapeRef = val;
                    break;
                case properties_i.yPosSubShapeRef:
                    yPosSubShapeRef = val;
                    break;
                case properties_i.xPosSubShapeRefPos:
                    xPosSubShapeRefPos = val;
                    break;
                case properties_i.yPosSubShapeRefPos:
                    yPosSubShapeRefPos = val;
                    break;
                case properties_i.rotationSteps:
                    rotationSteps = val;
                    break;
                case properties_i.rotationRef:
                    rotRef = val;
                    break;
                case properties_i.rotRefUseArray:
                    rotRefUseArray = val;
                    break;
                case properties_i.flipH:
                    flipH = val;
                    break;
                case properties_i.flipV:
                    flipV = val;
                    break;
                case properties_i.alignX:
                    alignX = val;
                    break;
                case properties_i.alignY:
                    alignY = val;
                    break;
                case properties_i.arrayXCount:
                    arrayXCount = val;
                    break;
                case properties_i.arrayYCount:
                    arrayYCount = val;
                    break;
                case properties_i.boundingLeftSteps:
                    boundingLeftSteps = val;
                    break;
                case properties_i.boundingRightSteps:
                    boundingRightSteps = val;
                    break;
                case properties_i.boundingBottomSteps:
                    boundingBottomSteps = val;
                    break;
                case properties_i.boundingTopSteps:
                    boundingTopSteps = val;
                    break;
                case properties_i.arrayRotationSteps:
                    arrayRotationSteps = val;
                    break;
                case properties_i.arrayRotationRef:
                    arrayRotRef = val;
                    break;
                case properties_i.arrayRef:
                    arrayRef = val;
                    break;
                case properties_i.arrayRotRefUseArray:
                    arrayRotRefUseArray = val;
                    break;
                case properties_i.refPivot:
                    refPivot = val;
                    break;
                case properties_i.refArrayPivot:
                    refArrayPivot = val;
                    break;
                case properties_i.refBoundsAfterRotation:
                    refBoundsAfterRotation = val;
                    break;
                case properties_i.refArrayBoundsAfterRotation:
                    refArrayBoundsAfterRotation = val;
                    break;
                case properties_i.relativeArray:
                    relativeArray = val;
                    break;
            }
        }

        public void defaultInt(properties_i p)
        {
            pDefaultInt(p);
        }

        void pDefaultInt(properties_i p)
        {
            switch (p)
            {
                case properties_i.posIndex:
                    posInSubShapeIndex = default_posInSubShapeIndex;
                    break;
                case properties_i.shape0Tip:
                    subShapeTipLocIndex = default_subShapeTipLocIndex;
                    break;
                case properties_i.shape1Tip:
                    subShape2TipLocIndex = default_subShape2TipLocIndex;
                    break;
                case properties_i.shape2Tip:
                    subShape3TipLocIndex = default_subShape3TipLocIndex;
                    break;
                case properties_i.shapeIndex:
                    shapeIndex = default_shapeIndex;
                    break;
                case properties_i.xPosRef:
                    xPosRef = default_posXRefIndex;
                    break;
                case properties_i.yPosRef:
                    yPosRef = default_posYRefIndex;
                    break;
                case properties_i.xPosSteps:
                    xPosRef = defaultSteps;
                    break;
                case properties_i.yPosSteps:
                    yPosSteps = defaultSteps;
                    break;
                case properties_i.s0HorLengthSteps:
                    s0HorLengthSteps = defaultSteps;
                    break;
                case properties_i.s0HorOffsetSteps:
                    s0HorOffsetSteps = defaultSteps;
                    break;
                case properties_i.s0VerLengthSteps:
                    s0VerLengthSteps = defaultSteps;
                    break;
                case properties_i.s0VerOffsetSteps:
                    s0VerOffsetSteps = defaultSteps;
                    break;
                case properties_i.s1HorLengthSteps:
                    s1HorLengthSteps = defaultSteps;
                    break;
                case properties_i.s1HorOffsetSteps:
                    s1HorOffsetSteps = defaultSteps;
                    break;
                case properties_i.s1VerLengthSteps:
                    s1VerLengthSteps = defaultSteps;
                    break;
                case properties_i.s1VerOffsetSteps:
                    s1VerOffsetSteps = defaultSteps;
                    break;
                case properties_i.s2HorLengthSteps:
                    s2HorLengthSteps = defaultSteps;
                    break;
                case properties_i.s2HorOffsetSteps:
                    s2HorOffsetSteps = defaultSteps;
                    break;
                case properties_i.s2VerLengthSteps:
                    s2VerLengthSteps = defaultSteps;
                    break;
                case properties_i.s2VerOffsetSteps:
                    s2VerOffsetSteps = defaultSteps;
                    break;
                case properties_i.xPosSubShapeRef:
                    xPosSubShapeRef = default_subShapeRef;
                    break;
                case properties_i.yPosSubShapeRef:
                    yPosSubShapeRef = default_subShapeRef;
                    break;
                case properties_i.xPosSubShapeRefPos:
                    xPosSubShapeRefPos = (int)CommonVars.subShapeHorLocs.L;
                    break;
                case properties_i.yPosSubShapeRefPos:
                    yPosSubShapeRefPos = (int)CommonVars.subShapeVerLocs.B;
                    break;
                case properties_i.rotationSteps:
                    rotationSteps = defaultSteps;
                    break;
                case properties_i.rotationRef:
                    rotRef = default_rotRefIndex;
                    break;
                case properties_i.flipH:
                    flipH = 0;
                    break;
                case properties_i.flipV:
                    flipV = 0;
                    break;
                case properties_i.alignX:
                    alignX = 1;
                    break;
                case properties_i.alignY:
                    alignY = 1;
                    break;
                case properties_i.arrayXCount:
                    arrayXCount = 0;
                    break;
                case properties_i.arrayYCount:
                    arrayYCount = 0;
                    break;
                case properties_i.arrayRotationSteps:
                    arrayRotationSteps = defaultSteps;
                    break;
                case properties_i.arrayRotationRef:
                    arrayRotRef = default_rotRefIndex;
                    break;
                case properties_i.boundingLeftSteps:
                    bbLSteps = defaultSteps;
                    break;
                case properties_i.boundingRightSteps:
                    bbRSteps = defaultSteps;
                    break;
                case properties_i.boundingBottomSteps:
                    bbBSteps = defaultSteps;
                    break;
                case properties_i.boundingTopSteps:
                    bbTSteps = defaultSteps;
                    break;
                case properties_i.arrayRef:
                    arrayRef = 0;
                    break;
                case properties_i.arrayRotRefUseArray:
                    arrayRotRefUseArray = 0;
                    break;
                case properties_i.rotRefUseArray:
                    rotRefUseArray = 0;
                    break;
                case properties_i.refPivot:
                    refPivot = 0;
                    break;
                case properties_i.refArrayPivot:
                    refArrayPivot = 0;
                    break;
                case properties_i.refBoundsAfterRotation:
                    refBoundsAfterRotation = 0;
                    break;
                case properties_i.refArrayBoundsAfterRotation:
                    refArrayBoundsAfterRotation = 0;
                    break;
                case properties_i.relativeArray:
                    relativeArray = 0;
                    break;
            }
        }

        static Int32 pGetDefaultInt(properties_i p)
        {
            int ret = 0;
            switch (p)
            {
                case properties_i.shapeIndex:
                    ret = default_shapeIndex;
                    break;
                case properties_i.shape0Tip:
                    ret = default_subShapeTipLocIndex;
                    break;
                case properties_i.shape1Tip:
                    ret = default_subShape2TipLocIndex;
                    break;
                case properties_i.shape2Tip:
                    ret = default_subShape3TipLocIndex;
                    break;
                case properties_i.subShapeIndex:
                    ret = default_subShapeRefIndex;
                    break;
                case properties_i.posIndex:
                    ret = default_posInSubShapeIndex;
                    break;
                case properties_i.xPosRef:
                    ret = default_posXRefIndex;
                    break;
                case properties_i.yPosRef:
                    ret = default_posYRefIndex;
                    break;
                case properties_i.xPosSteps:
                case properties_i.yPosSteps:
                case properties_i.s0HorLengthSteps:
                case properties_i.s0HorOffsetSteps:
                case properties_i.s0VerLengthSteps:
                case properties_i.s0VerOffsetSteps:
                case properties_i.s1HorLengthSteps:
                case properties_i.s1HorOffsetSteps:
                case properties_i.s1VerLengthSteps:
                case properties_i.s1VerOffsetSteps:
                case properties_i.s2HorLengthSteps:
                case properties_i.s2HorOffsetSteps:
                case properties_i.s2VerLengthSteps:
                case properties_i.s2VerOffsetSteps:
                case properties_i.rotationSteps:
                case properties_i.boundingLeftSteps:
                case properties_i.boundingRightSteps:
                case properties_i.boundingBottomSteps:
                case properties_i.boundingTopSteps:
                case properties_i.arrayRotationSteps:
                    ret = defaultSteps;
                    break;
                case properties_i.rotationRef:
                case properties_i.arrayRotationRef:
                    ret = default_rotRefIndex;
                    break;
                default:
                    ret = 0;
                    break;
                case properties_i.flipH:
                case properties_i.flipV:
                case properties_i.arrayRotRefUseArray:
                case properties_i.rotRefUseArray:
                case properties_i.refPivot:
                case properties_i.refArrayPivot:
                case properties_i.refBoundsAfterRotation:
                case properties_i.refArrayBoundsAfterRotation:
                case properties_i.relativeArray:
                    ret = 0;
                    break;
                case properties_i.arrayXCount:
                case properties_i.arrayYCount:
                    ret = defaultArrayCount;
                    break;
                case properties_i.alignX:
                case properties_i.alignY:
                    ret = 1;
                    break;
            }

            return ret;
        }

        // Actual values for this element. Note that these are requested, but not guaranteed due to shape clamping.
        decimal x;
        decimal y;
        decimal subShapeHorLength;
        decimal subShapeVerLength;
        decimal subShapeHorOffset;
        decimal subShapeVerOffset;
        decimal subShape2HorLength;
        decimal subShape2VerLength;
        decimal subShape2HorOffset;
        decimal subShape2VerOffset;
        decimal subShape3HorLength;
        decimal subShape3VerLength;
        decimal subShape3HorOffset;
        decimal subShape3VerOffset;
        decimal rotation;
        decimal arrayRotation;

        decimal boundingLeft;
        decimal boundingRight;
        decimal boundingTop;
        decimal boundingBottom;

        // Min/max values, and step
        decimal subShapeMinHorLength;
        decimal subShapeHorLengthInc;
        decimal subShapeMinHorOffset;
        decimal subShapeHorOffsetInc;
        decimal subShapeMinVerLength;
        decimal subShapeVerLengthInc;
        decimal subShapeMinVerOffset;
        decimal subShapeVerOffsetInc;
        decimal subShape2MinHorLength;
        decimal subShape2HorLengthInc;
        decimal subShape2MinHorOffset;
        decimal subShape2HorOffsetInc;
        decimal subShape2MinVerLength;
        decimal subShape2VerLengthInc;
        decimal subShape2MinVerOffset;
        decimal subShape2VerOffsetInc;
        decimal subShape3MinHorLength;
        decimal subShape3HorLengthInc;
        decimal subShape3MinHorOffset;
        decimal subShape3HorOffsetInc;
        decimal subShape3MinVerLength;
        decimal subShape3VerLengthInc;
        decimal subShape3MinVerOffset;
        decimal subShape3VerOffsetInc;
        decimal minXPos;
        decimal xPosInc;
        decimal minYPos;
        decimal yPosInc;
        decimal minRotation;
        decimal rotationInc;
        decimal minArrayRotation;
        decimal arrayRotationInc;

        decimal boundingLeftInc;
        decimal boundingRightInc;
        decimal boundingTopInc;
        decimal boundingBottomInc;

        decimal arrayXSpace;
        decimal arrayYSpace;


        public enum properties_decimal
        {
            s0MinHorLength, s0MinVerLength, s0MinHorOffset, s0MinVerOffset,
            s0HorLengthInc, s0VerLengthInc, s0HorOffsetInc, s0VerOffsetInc,
            s1MinHorLength, s1MinVerLength, s1MinHorOffset, s1MinVerOffset,
            s1HorLengthInc, s1VerLengthInc, s1HorOffsetInc, s1VerOffsetInc,
            s2MinHorLength, s2MinVerLength, s2MinHorOffset, s2MinVerOffset,
            s2HorLengthInc, s2VerLengthInc, s2HorOffsetInc, s2VerOffsetInc,
            gHorOffset, gVerOffset,
            minXPos, xPosInc, minYPos, yPosInc,
            xPos, yPos,
            s0HorLength, s0VerLength, s0HorOffset, s0VerOffset,
            s1HorLength, s1VerLength, s1HorOffset, s1VerOffset,
            s2HorLength, s2VerLength, s2HorOffset, s2VerOffset,
            minRotation, rotationInc, rotation,
            boundingLeft, boundingRight, boundingTop, boundingBottom,
            boundingLeftInc, boundingRightInc, boundingTopInc, boundingBottomInc,
            arrayXSpace, arrayYSpace,
            minArrayRotation, arrayRotationInc, arrayRotation
        }

        public decimal getDecimal(properties_decimal p, int _subShapeRef = 0)
        {
            return pGetDecimal(p, _subShapeRef);
        }

        decimal pGetDecimal(properties_decimal p, int _subShapeRef)
        {
            decimal ret = 0m;
            switch (p)
            {
                case properties_decimal.s0MinHorLength:
                    ret = subShapeMinHorLength;
                    break;
                case properties_decimal.s0MinVerLength:
                    ret = subShapeMinVerLength;
                    break;
                case properties_decimal.s0MinHorOffset:
                    ret = subShapeMinHorOffset;
                    break;
                case properties_decimal.s0MinVerOffset:
                    ret = subShapeMinVerOffset;
                    break;
                case properties_decimal.s1MinHorLength:
                    ret = subShape2MinHorLength;
                    break;
                case properties_decimal.s1MinVerLength:
                    ret = subShape2MinVerLength;
                    break;
                case properties_decimal.s1MinHorOffset:
                    ret = subShape2MinHorOffset;
                    break;
                case properties_decimal.s1MinVerOffset:
                    ret = subShape2MinVerOffset;
                    break;
                case properties_decimal.s2MinHorLength:
                    ret = subShape3MinHorLength;
                    break;
                case properties_decimal.s2MinVerLength:
                    ret = subShape3MinVerLength;
                    break;
                case properties_decimal.s2MinHorOffset:
                    ret = subShape3MinHorOffset;
                    break;
                case properties_decimal.s2MinVerOffset:
                    ret = subShape3MinVerOffset;
                    break;
                case properties_decimal.xPos:
                    ret = x;
                    if (_subShapeRef == 1)
                    {
                        ret += pGetDecimal(properties_decimal.s1HorOffset, 0);
                    }
                    if (_subShapeRef == 2)
                    {
                        ret += pGetDecimal(properties_decimal.s2HorOffset, 0);
                    }
                    break;
                case properties_decimal.yPos:
                    ret = y;
                    if (_subShapeRef == 1)
                    {
                        ret += pGetDecimal(properties_decimal.s1VerOffset, 0);
                    }
                    if (_subShapeRef == 2)
                    {
                        ret += pGetDecimal(properties_decimal.s0VerLength, _subShapeRef);
                        ret -= pGetDecimal(properties_decimal.s2VerOffset, _subShapeRef);
                        ret -= pGetDecimal(properties_decimal.s2VerLength, _subShapeRef);
                    }
                    break;
                case properties_decimal.minXPos:
                    ret = minXPos;
                    break;
                case properties_decimal.xPosInc:
                    ret = xPosInc;
                    break;
                case properties_decimal.minYPos:
                    ret = minYPos;
                    break;
                case properties_decimal.yPosInc:
                    ret = yPosInc;
                    break;
                case properties_decimal.s0HorLengthInc:
                    ret = subShapeHorLengthInc;
                    break;
                case properties_decimal.s0HorOffsetInc:
                    ret = subShapeHorOffsetInc;
                    break;
                case properties_decimal.s0VerLengthInc:
                    ret = subShapeVerLengthInc;
                    break;
                case properties_decimal.s0VerOffsetInc:
                    ret = subShapeVerOffsetInc;
                    break;
                case properties_decimal.s1HorLengthInc:
                    ret = subShape2HorLengthInc;
                    break;
                case properties_decimal.s1HorOffsetInc:
                    ret = subShape2HorOffsetInc;
                    break;
                case properties_decimal.s1VerLengthInc:
                    ret = subShape2VerLengthInc;
                    break;
                case properties_decimal.s1VerOffsetInc:
                    ret = subShape2VerOffsetInc;
                    break;
                case properties_decimal.s2HorLengthInc:
                    ret = subShape3HorLengthInc;
                    break;
                case properties_decimal.s2HorOffsetInc:
                    ret = subShape3HorOffsetInc;
                    break;
                case properties_decimal.s2VerLengthInc:
                    ret = subShape3VerLengthInc;
                    break;
                case properties_decimal.s2VerOffsetInc:
                    ret = subShape3VerOffsetInc;
                    break;

                case properties_decimal.s0HorLength:
                    ret = subShapeHorLength;
                    break;
                case properties_decimal.s0VerLength:
                    ret = subShapeVerLength;
                    break;
                case properties_decimal.s1HorLength:
                    ret = subShape2HorLength;
                    break;
                case properties_decimal.s1VerLength:
                    ret = subShape2VerLength;
                    break;
                case properties_decimal.s2HorLength:
                    ret = subShape3HorLength;
                    break;
                case properties_decimal.s2VerLength:
                    ret = subShape3VerLength;
                    break;

                case properties_decimal.s0HorOffset:
                    ret = subShapeHorOffset;
                    break;
                case properties_decimal.s0VerOffset:
                    ret = subShapeVerOffset;
                    break;
                case properties_decimal.s1HorOffset:
                    ret = subShape2HorOffset;
                    break;
                case properties_decimal.s1VerOffset:
                    ret = subShape2VerOffset;
                    break;
                case properties_decimal.s2HorOffset:
                    ret = subShape3HorOffset;
                    break;
                case properties_decimal.s2VerOffset:
                    ret = subShape3VerOffset;
                    break;

                case properties_decimal.minRotation:
                    ret = minRotation;
                    break;
                case properties_decimal.rotationInc:
                    ret = rotationInc;
                    break;
                case properties_decimal.rotation:
                    ret = rotation;
                    break;
                case properties_decimal.boundingBottom:
                    ret = boundingBottom;
                    break;
                case properties_decimal.boundingBottomInc:
                    ret = boundingBottomInc;
                    break;
                case properties_decimal.boundingTop:
                    ret = boundingTop;
                    break;
                case properties_decimal.boundingTopInc:
                    ret = boundingTopInc;
                    break;
                case properties_decimal.boundingLeft:
                    ret = boundingLeft;
                    break;
                case properties_decimal.boundingLeftInc:
                    ret = boundingLeftInc;
                    break;
                case properties_decimal.boundingRight:
                    ret = boundingRight;
                    break;
                case properties_decimal.boundingRightInc:
                    ret = boundingRightInc;
                    break;

                case properties_decimal.arrayXSpace:
                    ret = arrayXSpace;
                    break;
                case properties_decimal.arrayYSpace:
                    ret = arrayYSpace;
                    break;
                case properties_decimal.minArrayRotation:
                    ret = minArrayRotation;
                    break;
                case properties_decimal.arrayRotationInc:
                    ret = arrayRotationInc;
                    break;
                case properties_decimal.arrayRotation:
                    ret = arrayRotation;
                    break;
            }

            return ret;
        }

        static decimal pGetDefaultDecimal(properties_decimal p)
        {
            decimal ret = 0m;
            switch (p)
            {
                case properties_decimal.s0MinHorLength:
                case properties_decimal.s0HorLength:
                    ret = default_subShapeHorLength;
                    break;
                case properties_decimal.s0MinVerLength:
                case properties_decimal.s0VerLength:
                    ret = default_subShapeVerLength;
                    break;
                case properties_decimal.s0MinHorOffset:
                case properties_decimal.s0HorOffset:
                    ret = default_subShapeHorOffset;
                    break;
                case properties_decimal.s0MinVerOffset:
                case properties_decimal.s0VerOffset:
                    ret = default_subShapeVerOffset;
                    break;
                case properties_decimal.s1MinHorLength:
                case properties_decimal.s1HorLength:
                    ret = default_subShape2HorLength;
                    break;
                case properties_decimal.s1MinVerLength:
                case properties_decimal.s1VerLength:
                    ret = default_subShape2VerLength;
                    break;
                case properties_decimal.s1MinHorOffset:
                case properties_decimal.s1HorOffset:
                    ret = default_subShape2HorOffset;
                    break;
                case properties_decimal.s1MinVerOffset:
                case properties_decimal.s1VerOffset:
                    ret = default_subShape2VerOffset;
                    break;
                case properties_decimal.s2MinHorLength:
                case properties_decimal.s2HorLength:
                    ret = default_subShape3HorLength;
                    break;
                case properties_decimal.s2MinVerLength:
                case properties_decimal.s2VerLength:
                    ret = default_subShape3VerLength;
                    break;
                case properties_decimal.s2MinHorOffset:
                case properties_decimal.s2HorOffset:
                    ret = default_subShape3HorOffset;
                    break;
                case properties_decimal.s2MinVerOffset:
                case properties_decimal.s2VerOffset:
                    ret = default_subShape3VerOffset;
                    break;
                case properties_decimal.xPos:
                case properties_decimal.yPos:
                case properties_decimal.minXPos:
                case properties_decimal.xPosInc:
                case properties_decimal.minYPos:
                case properties_decimal.yPosInc:
                case properties_decimal.s0HorLengthInc:
                case properties_decimal.s0HorOffsetInc:
                case properties_decimal.s0VerLengthInc:
                case properties_decimal.s0VerOffsetInc:
                case properties_decimal.s1HorLengthInc:
                case properties_decimal.s1HorOffsetInc:
                case properties_decimal.s1VerLengthInc:
                case properties_decimal.s1VerOffsetInc:
                case properties_decimal.s2HorLengthInc:
                case properties_decimal.s2HorOffsetInc:
                case properties_decimal.s2VerLengthInc:
                case properties_decimal.s2VerOffsetInc:
                case properties_decimal.rotation:
                case properties_decimal.minRotation:
                case properties_decimal.rotationInc:
                case properties_decimal.boundingLeftInc:
                case properties_decimal.boundingRightInc:
                case properties_decimal.boundingBottomInc:
                case properties_decimal.boundingTopInc:
                case properties_decimal.arrayXSpace:
                case properties_decimal.arrayYSpace:
                case properties_decimal.minArrayRotation:
                case properties_decimal.arrayRotation:
                case properties_decimal.arrayRotationInc:
                    ret = 0;
                    break;
            }

            return ret;
        }

        public void setDecimal(properties_decimal p, decimal val)
        {
            pSetDecimal(p, val);
        }

        void pSetDecimal(properties_decimal p, decimal val)
        {
            switch (p)
            {
                case properties_decimal.s0MinHorLength:
                    subShapeMinHorLength = val;
                    break;
                case properties_decimal.s0MinVerLength:
                    subShapeMinVerLength = val;
                    break;
                case properties_decimal.s0MinHorOffset:
                    subShapeMinHorOffset = val;
                    break;
                case properties_decimal.s0MinVerOffset:
                    subShapeMinVerOffset = val;
                    break;
                case properties_decimal.s1MinHorLength:
                    subShape2MinHorLength = val;
                    break;
                case properties_decimal.s1MinVerLength:
                    subShape2MinVerLength = val;
                    break;
                case properties_decimal.s1MinHorOffset:
                    subShape2MinHorOffset = val;
                    break;
                case properties_decimal.s1MinVerOffset:
                    subShape2MinVerOffset = val;
                    break;
                case properties_decimal.s2MinHorLength:
                    subShape3MinHorLength = val;
                    break;
                case properties_decimal.s2MinVerLength:
                    subShape3MinVerLength = val;
                    break;
                case properties_decimal.s2MinHorOffset:
                    subShape3MinHorOffset = val;
                    break;
                case properties_decimal.s2MinVerOffset:
                    subShape3MinVerOffset = val;
                    break;
                case properties_decimal.xPos:
                    x = val;
                    break;
                case properties_decimal.yPos:
                    y = val;
                    break;
                case properties_decimal.minXPos:
                    minXPos = val;
                    break;
                case properties_decimal.xPosInc:
                    xPosInc = val;
                    break;
                case properties_decimal.minYPos:
                    minYPos = val;
                    break;
                case properties_decimal.yPosInc:
                    yPosInc = val;
                    break;
                case properties_decimal.s0HorLengthInc:
                    subShapeHorLengthInc = val;
                    break;
                case properties_decimal.s0HorOffsetInc:
                    subShapeHorOffsetInc = val;
                    break;
                case properties_decimal.s0VerLengthInc:
                    subShapeVerLengthInc = val;
                    break;
                case properties_decimal.s0VerOffsetInc:
                    subShapeVerOffsetInc = val;
                    break;
                case properties_decimal.s1HorLengthInc:
                    subShape2HorLengthInc = val;
                    break;
                case properties_decimal.s1HorOffsetInc:
                    subShape2HorOffsetInc = val;
                    break;
                case properties_decimal.s1VerLengthInc:
                    subShape2VerLengthInc = val;
                    break;
                case properties_decimal.s1VerOffsetInc:
                    subShape2VerOffsetInc = val;
                    break;
                case properties_decimal.s2HorLengthInc:
                    subShape3HorLengthInc = val;
                    break;
                case properties_decimal.s2HorOffsetInc:
                    subShape3HorOffsetInc = val;
                    break;
                case properties_decimal.s2VerLengthInc:
                    subShape3VerLengthInc = val;
                    break;
                case properties_decimal.s2VerOffsetInc:
                    subShape3VerOffsetInc = val;
                    break;

                case properties_decimal.s0HorLength:
                    subShapeHorLength = val;
                    break;
                case properties_decimal.s0VerLength:
                    subShapeVerLength = val;
                    break;
                case properties_decimal.s0HorOffset:
                    subShapeHorOffset = val;
                    break;
                case properties_decimal.s0VerOffset:
                    subShapeVerOffset = val;
                    break;
                case properties_decimal.s1HorLength:
                    subShape2HorLength = val;
                    break;
                case properties_decimal.s1VerLength:
                    subShape2VerLength = val;
                    break;
                case properties_decimal.s1HorOffset:
                    subShape2HorOffset = val;
                    break;
                case properties_decimal.s1VerOffset:
                    subShape2VerOffset = val;
                    break;
                case properties_decimal.s2HorLength:
                    subShape3HorLength = val;
                    break;
                case properties_decimal.s2VerLength:
                    subShape3VerLength = val;
                    break;
                case properties_decimal.s2HorOffset:
                    subShape3HorOffset = val;
                    break;
                case properties_decimal.s2VerOffset:
                    subShape3VerOffset = val;
                    break;
                case properties_decimal.minRotation:
                    minRotation = val;
                    break;
                case properties_decimal.rotationInc:
                    rotationInc = val;
                    break;
                case properties_decimal.rotation:
                    rotation = val;
                    break;
                case properties_decimal.boundingBottom:
                    boundingBottom = val;
                    break;
                case properties_decimal.boundingBottomInc:
                    boundingBottomInc = val;
                    break;
                case properties_decimal.boundingTop:
                    boundingTop = val;
                    break;
                case properties_decimal.boundingTopInc:
                    boundingTopInc = val;
                    break;
                case properties_decimal.boundingLeft:
                    boundingLeft = val;
                    break;
                case properties_decimal.boundingLeftInc:
                    boundingLeftInc = val;
                    break;
                case properties_decimal.boundingRight:
                    boundingRight = val;
                    break;
                case properties_decimal.boundingRightInc:
                    boundingRightInc = val;
                    break;

                case properties_decimal.arrayXSpace:
                    arrayXSpace = val;
                    break;
                case properties_decimal.arrayYSpace:
                    arrayYSpace = val;
                    break;
                case properties_decimal.minArrayRotation:
                    minArrayRotation = val;
                    break;
                case properties_decimal.arrayRotationInc:
                    arrayRotationInc = val;
                    break;
                case properties_decimal.arrayRotation:
                    arrayRotation = val;
                    break;

            }
        }

        public void defaultDecimal(properties_decimal p)
        {
            pDefaultDecimal(p);
        }

        void pDefaultDecimal(properties_decimal p)
        {
            switch (p)
            {
                case properties_decimal.s0MinHorLength:
                    subShapeMinHorLength = default_subShapeHorLength;
                    break;
                case properties_decimal.s0MinHorOffset:
                    subShapeMinHorOffset = default_subShapeHorOffset;
                    break;
                case properties_decimal.s0MinVerLength:
                    subShapeMinVerLength = default_subShapeVerLength;
                    break;
                case properties_decimal.s0MinVerOffset:
                    subShapeMinVerOffset = default_subShapeVerOffset;
                    break;
                case properties_decimal.s1MinHorLength:
                    subShape2MinHorLength = default_subShape2HorLength;
                    break;
                case properties_decimal.s1MinHorOffset:
                    subShape2MinHorOffset = default_subShape2HorOffset;
                    break;
                case properties_decimal.s1MinVerLength:
                    subShape2MinVerLength = default_subShape2VerLength;
                    break;
                case properties_decimal.s1MinVerOffset:
                    subShape2MinVerOffset = default_subShape2VerOffset;
                    break;
                case properties_decimal.s2MinHorLength:
                    subShape3MinHorLength = default_subShape3HorLength;
                    break;
                case properties_decimal.s2MinHorOffset:
                    subShape3MinHorOffset = default_subShape3HorOffset;
                    break;
                case properties_decimal.s2MinVerLength:
                    subShape3MinVerLength = default_subShape3VerLength;
                    break;
                case properties_decimal.s2MinVerOffset:
                    subShape3MinVerOffset = default_subShape3VerOffset;
                    break;
                case properties_decimal.xPos:
                    x = 0;
                    break;
                case properties_decimal.yPos:
                    y = 0;
                    break;
                case properties_decimal.s0HorLengthInc:
                    subShapeHorLengthInc = 0;
                    break;
                case properties_decimal.s0HorOffsetInc:
                    subShapeHorOffsetInc = 0;
                    break;
                case properties_decimal.s0VerLengthInc:
                    subShapeVerLengthInc = 0;
                    break;
                case properties_decimal.s0VerOffsetInc:
                    subShapeVerOffsetInc = 0;
                    break;
                case properties_decimal.s1HorLengthInc:
                    subShape2HorLengthInc = 0;
                    break;
                case properties_decimal.s1HorOffsetInc:
                    subShape2HorOffsetInc = 0;
                    break;
                case properties_decimal.s1VerLengthInc:
                    subShape2VerLengthInc = 0;
                    break;
                case properties_decimal.s1VerOffsetInc:
                    subShape2VerOffsetInc = 0;
                    break;
                case properties_decimal.s2HorLengthInc:
                    subShape3HorLengthInc = 0;
                    break;
                case properties_decimal.s2HorOffsetInc:
                    subShape3HorOffsetInc = 0;
                    break;
                case properties_decimal.s2VerLengthInc:
                    subShape3VerLengthInc = 0;
                    break;
                case properties_decimal.s2VerOffsetInc:
                    subShape3VerOffsetInc = 0;
                    break;

                case properties_decimal.s0HorLength:
                    subShapeHorLength = default_subShapeHorLength;
                    break;
                case properties_decimal.s0HorOffset:
                    subShapeHorOffset = default_subShapeHorOffset;
                    break;
                case properties_decimal.s0VerLength:
                    subShapeVerLength = default_subShapeVerLength;
                    break;
                case properties_decimal.s0VerOffset:
                    subShapeVerOffset = default_subShapeVerOffset;
                    break;
                case properties_decimal.s1HorLength:
                    subShape2HorLength = default_subShape2HorLength;
                    break;
                case properties_decimal.s1HorOffset:
                    subShape2HorOffset = default_subShape2HorOffset;
                    break;
                case properties_decimal.s1VerLength:
                    subShape2VerLength = default_subShape2VerLength;
                    break;
                case properties_decimal.s1VerOffset:
                    subShape2VerOffset = default_subShape2VerOffset;
                    break;
                case properties_decimal.s2HorLength:
                    subShape3HorLength = default_subShape3HorLength;
                    break;
                case properties_decimal.s2HorOffset:
                    subShape3HorOffset = default_subShape3HorOffset;
                    break;
                case properties_decimal.s2VerLength:
                    subShape3VerLength = default_subShape3VerLength;
                    break;
                case properties_decimal.s2VerOffset:
                    subShape3VerOffset = default_subShape3VerOffset;
                    break;

                case properties_decimal.minRotation:
                    minRotation = default_rotation;
                    break;

                case properties_decimal.rotation:
                    rotation = default_rotation;
                    break;

                case properties_decimal.rotationInc:
                    rotationInc = 0;
                    break;

                case properties_decimal.boundingBottom:
                    boundingBottom = 0;
                    break;
                case properties_decimal.boundingBottomInc:
                    boundingBottomInc = 0;
                    break;
                case properties_decimal.boundingTop:
                    boundingTop = 0;
                    break;
                case properties_decimal.boundingTopInc:
                    boundingTopInc = 0;
                    break;
                case properties_decimal.boundingLeft:
                    boundingLeft = 0;
                    break;
                case properties_decimal.boundingLeftInc:
                    boundingLeftInc = 0;
                    break;
                case properties_decimal.boundingRight:
                    boundingRight = 0;
                    break;
                case properties_decimal.boundingRightInc:
                    boundingRightInc = 0;
                    break;

                case properties_decimal.arrayXSpace:
                    arrayXSpace = 0;
                    break;
                case properties_decimal.arrayYSpace:
                    arrayYSpace = 0;
                    break;

                case properties_decimal.minArrayRotation:
                    minArrayRotation = 0;
                    break;
                case properties_decimal.arrayRotation:
                    arrayRotation = 0;
                    break;
                case properties_decimal.arrayRotationInc:
                    arrayRotationInc = 0;
                    break;
            }
        }

        public bool equivalence(PatternElement pe)
        {
            return pEquivalence(pe);
        }

        bool pEquivalence(PatternElement pe)
        {
            bool ret = true;

            ret = ret && (subShapeHorLength == pe.subShapeHorLength);
            ret = ret && (subShapeHorOffset == pe.subShapeHorOffset);
            ret = ret && (subShapeVerLength == pe.subShapeVerLength);
            ret = ret && (subShapeVerOffset == pe.subShapeVerOffset);

            ret = ret && (subShape2HorLength == pe.subShape2HorLength);
            ret = ret && (subShape2HorOffset == pe.subShape2HorOffset);
            ret = ret && (subShape2VerLength == pe.subShape2VerLength);
            ret = ret && (subShape2VerOffset == pe.subShape2VerOffset);

            ret = ret && (subShape3HorLength == pe.subShape3HorLength);
            ret = ret && (subShape3HorOffset == pe.subShape3HorOffset);
            ret = ret && (subShape3VerLength == pe.subShape3VerLength);
            ret = ret && (subShape3VerOffset == pe.subShape3VerOffset);

            ret = ret && (x == pe.x);
            ret = ret && (y == pe.y);

            ret = ret && (rotation % 360 == pe.rotation % 360);

            ret = ret && (boundingBottom == pe.boundingBottom);
            ret = ret && (boundingTop == pe.boundingTop);
            ret = ret && (boundingLeft == pe.boundingLeft);
            ret = ret && (boundingRight == pe.boundingRight);

            ret = ret && (arrayRotation % 360 == pe.arrayRotation % 360);

            ret = ret && (arrayXCount == pe.arrayXCount);
            ret = ret && (arrayYCount == pe.arrayYCount);
            ret = ret && (arrayXSpace == pe.arrayXSpace);
            ret = ret && (arrayYSpace == pe.arrayYSpace);

            ret = ret && (refBoundsAfterRotation == pe.refBoundsAfterRotation);

            ret = ret && (refArrayBoundsAfterRotation == pe.refArrayBoundsAfterRotation);

            return ret;
        }

        int variantCounter;

        int xSteps, ySteps;
        int s0HLSteps, s0VLSteps, s0HOSteps, s0VOSteps;
        int s1HLSteps, s1VLSteps, s1HOSteps, s1VOSteps;
        int s2HLSteps, s2VLSteps, s2HOSteps, s2VOSteps;
        int rSteps, arrayRSteps;
        int bbLSteps, bbRSteps, bbBSteps, bbTSteps;

        int calcMaxVariants()
        {
            // Override number of steps in case the step itself is zero
            xSteps = xPosSteps;
            if (xPosInc == 0)
            {
                xSteps = 1;
            }

            ySteps = yPosSteps;
            if (yPosInc == 0)
            {
                ySteps = 1;
            }

            rSteps = rotationSteps;
            if (rotationInc == 0)
            {
                rSteps = 1;
            }

            if (pIsXArray() || pIsYArray() || relativeArray == 1)
            {
                arrayRSteps = arrayRotationSteps;
            }
            else
            {
                arrayRSteps = 1;
            }

            int limit = xSteps * ySteps * rSteps * arrayRSteps;

            if (shapeIndex != (int)CommonVars.shapeNames.bounding)
            {
                s0HLSteps = s0HorLengthSteps;
                if (subShapeHorLengthInc == 0)
                {
                    s0HLSteps = 1;
                }

                s0HOSteps = s0HorOffsetSteps;
                if (subShapeHorOffsetInc == 0)
                {
                    s0HOSteps = 1;
                }

                s0VLSteps = s0VerLengthSteps;
                if (subShapeVerLengthInc == 0)
                {
                    s0VLSteps = 1;
                }

                s0VOSteps = s0VerOffsetSteps;
                if (subShapeVerOffsetInc == 0)
                {
                    s0VOSteps = 1;
                }

                s1HLSteps = s1HorLengthSteps;
                if (subShape2HorLengthInc == 0)
                {
                    s1HLSteps = 1;
                }

                s1HOSteps = s1HorOffsetSteps;
                if (subShape2HorOffsetInc == 0)
                {
                    s1HOSteps = 1;
                }

                s1VLSteps = s1VerLengthSteps;
                if (subShape2VerLengthInc == 0)
                {
                    s1VLSteps = 1;
                }

                s1VOSteps = s1VerOffsetSteps;
                if (subShape2VerOffsetInc == 0)
                {
                    s1VOSteps = 1;
                }

                s2HLSteps = s2HorLengthSteps;
                if (subShape3HorLengthInc == 0)
                {
                    s2HLSteps = 1;
                }

                s2HOSteps = s2HorOffsetSteps;
                if (subShape3HorOffsetInc == 0)
                {
                    s2HOSteps = 1;
                }

                s2VLSteps = s2VerLengthSteps;
                if (subShape3VerLengthInc == 0)
                {
                    s2VLSteps = 1;
                }

                s2VOSteps = s2VerOffsetSteps;
                if (subShape3VerOffsetInc == 0)
                {
                    s2VOSteps = 1;
                }

                limit *= s0HLSteps * s0HOSteps * s0VLSteps * s0VOSteps;
                if ((shapeIndex != (int)CommonVars.shapeNames.none) && (shapeIndex != (int)CommonVars.shapeNames.rect) && (shapeIndex != (int)CommonVars.shapeNames.text))
                {
                    limit *= s1HLSteps * s1HOSteps * s1VLSteps * s1VOSteps;
                    if (shapeIndex == (int)CommonVars.shapeNames.Sshape)
                    {
                        limit *= s2HLSteps * s2HOSteps * s2VLSteps * s2VOSteps;
                    }
                }
            }
            else
            {
                bbLSteps = boundingLeftSteps;
                if (bbLSteps == 0)
                {
                    bbLSteps = 1;
                }

                bbRSteps = boundingRightSteps;
                if (bbRSteps == 0)
                {
                    bbRSteps = 1;
                }

                bbTSteps = boundingTopSteps;
                if (bbTSteps == 0)
                {
                    bbTSteps = 1;
                }

                bbBSteps = boundingBottomSteps;
                if (bbBSteps == 0)
                {
                    bbBSteps = 1;
                }

                limit *= bbLSteps * bbRSteps * bbBSteps * bbTSteps;
            }

            return limit;
        }

        public PatternElement getNextVariant()
        {
            return pGetNextVariant();
        }

        PatternElement pGetNextVariant()
        {
            if (variantCounter >= calcMaxVariants())
            {
                variantCounter = 0;
                return null; // no available variant.
            }
            int xIndex = variantCounter % xSteps;

            long fields = xSteps;

            int yIndex = (int)(Math.Floor((float)variantCounter / fields) % ySteps);

            fields *= ySteps;

            PatternElement ret = new PatternElement(this);

            if (shapeIndex != (int)CommonVars.shapeNames.bounding)
            {
                int s0hlIndex = 0;
                int s0hoIndex = 0;
                int s0vlIndex = 0;
                int s0voIndex = 0;

                int s1hlIndex = 0;
                int s1hoIndex = 0;
                int s1vlIndex = 0;
                int s1voIndex = 0;

                int s2hlIndex = 0;
                int s2hoIndex = 0;
                int s2vlIndex = 0;
                int s2voIndex = 0;

                s0hlIndex = (int)(Math.Floor((float)variantCounter / fields) % s0HLSteps);

                fields *= s0HLSteps;

                s0hoIndex = (int)(Math.Floor((float)variantCounter / fields) % s0HOSteps);

                fields *= s0HOSteps;

                s0vlIndex = (int)(Math.Floor((float)variantCounter / fields) % s0VLSteps);

                fields *= s0VLSteps;

                s0voIndex = (int)(Math.Floor((float)variantCounter / fields) % s0VOSteps);

                fields *= s0VOSteps;

                if ((shapeIndex != (int)CommonVars.shapeNames.none) && (shapeIndex != (int)CommonVars.shapeNames.rect) && (shapeIndex != (int)CommonVars.shapeNames.text))
                {
                    s1hlIndex = (int)(Math.Floor((float)variantCounter / fields) % s1HLSteps);

                    fields *= s1HLSteps;

                    s1hoIndex = (int)(Math.Floor((float)variantCounter / fields) % s1HOSteps);

                    fields *= s1HOSteps;

                    s1vlIndex = (int)(Math.Floor((float)variantCounter / fields) % s1VLSteps);

                    fields *= s1VLSteps;

                    s1voIndex = (int)(Math.Floor((float)variantCounter / fields) % s1VOSteps);

                    fields *= s1VOSteps;

                    if (shapeIndex == (int)CommonVars.shapeNames.Sshape)
                    {
                        s2hlIndex = (int)(Math.Floor((float)variantCounter / fields) % s2HLSteps);

                        fields *= s2HLSteps;

                        s2hoIndex = (int)(Math.Floor((float)variantCounter / fields) % s2HOSteps);

                        fields *= s2HOSteps;

                        s2vlIndex = (int)(Math.Floor((float)variantCounter / fields) % s2VLSteps);

                        fields *= s2VLSteps;

                        s2voIndex = (int)(Math.Floor((float)variantCounter / fields) % s2VOSteps);

                        fields *= s2VOSteps;
                    }
                }

                ret.subShapeHorLength = subShapeMinHorLength + (s0hlIndex * subShapeHorLengthInc);
                ret.subShapeVerLength = subShapeMinVerLength + (s0vlIndex * subShapeVerLengthInc);
                ret.subShapeHorOffset = subShapeMinHorOffset + (s0hoIndex * subShapeHorOffsetInc);
                ret.subShapeVerOffset = subShapeMinVerOffset + (s0voIndex * subShapeVerOffsetInc);

                ret.subShape2HorLength = subShape2MinHorLength + (s1hlIndex * subShape2HorLengthInc);
                ret.subShape2VerLength = subShape2MinVerLength + (s1vlIndex * subShape2VerLengthInc);
                ret.subShape2HorOffset = subShape2MinHorOffset + (s1hoIndex * subShape2HorOffsetInc);
                ret.subShape2VerOffset = subShape2MinVerOffset + (s1voIndex * subShape2VerOffsetInc);

                ret.subShape3HorLength = subShape3MinHorLength + (s2hlIndex * subShape3HorLengthInc);
                ret.subShape3VerLength = subShape3MinVerLength + (s2vlIndex * subShape3VerLengthInc);
                ret.subShape3HorOffset = subShape3MinHorOffset + (s2hoIndex * subShape3HorOffsetInc);
                ret.subShape3VerOffset = subShape3MinVerOffset + (s2voIndex * subShape3VerOffsetInc);
            }
            else
            {
                int bbLIndex = (int)(Math.Floor((float)variantCounter / fields) % bbLSteps);

                fields *= bbLSteps;

                int bbRIndex = (int)(Math.Floor((float)variantCounter / fields) % bbRSteps);

                fields *= bbRSteps;

                int bbBIndex = (int)(Math.Floor((float)variantCounter / fields) % bbBSteps);

                fields *= bbBSteps;

                int bbTIndex = (int)(Math.Floor((float)variantCounter / fields) % bbTSteps);

                fields *= bbTSteps;

                ret.boundingLeft = boundingLeft + (bbLIndex * boundingLeftInc);
                ret.boundingRight = boundingRight + (bbRIndex * boundingRightInc);
                ret.boundingBottom = boundingBottom + (bbBIndex * boundingBottomInc);
                ret.boundingTop = boundingTop + (bbTIndex * boundingTopInc);
            }

            ret.x = minXPos + (xIndex * xPosInc);
            ret.y = minYPos + (yIndex * yPosInc);

            int rotIndex = (int)(Math.Floor((float)variantCounter / fields) % rSteps);
            ret.rotation = minRotation + (rotIndex * rotationInc);

            fields *= rSteps;

            ret.arrayRotation = 0;
            if (pIsXArray() || pIsYArray() || relativeArray == 1) // Would like to be able to directly identify a relative array reference, but there's no way to check this from the element level. We have to set this from elsewhere.
            {
                int arrayRotIndex = (int)(Math.Floor((float)variantCounter / fields) % arrayRSteps);
                ret.arrayRotation = minArrayRotation + (arrayRotIndex * arrayRotationInc);
                fields *= arrayRSteps;
            }

            // We need to worry about clamping here....

            if ((shapeIndex != (int)CommonVars.shapeNames.none) && (shapeIndex != (int)CommonVars.shapeNames.rect) && (shapeIndex != (int)CommonVars.shapeNames.text) && (shapeIndex != (int)CommonVars.shapeNames.bounding))
            {
                switch (shapeIndex)
                {
                    case (int)CommonVars.shapeNames.Lshape:
                        Lshape_limits(ref ret);
                        break;
                    case (int)CommonVars.shapeNames.Tshape:
                        Tshape_limits(ref ret);
                        break;
                    case (int)CommonVars.shapeNames.Ushape:
                        Ushape_limits(ref ret);
                        break;
                    case (int)CommonVars.shapeNames.Sshape:
                        Sshape_limits(ref ret);
                        break;
                    case (int)CommonVars.shapeNames.Xshape:
                        Xshape_limits(ref ret);
                        break;
                }
            }
            variantCounter++;

            return ret;
        }

        void Sshape_limits(ref PatternElement ret)
        {
            // Clamp dimensions
            ret.subShape2HorLength = Math.Min(ret.subShape2HorLength, ret.subShapeHorLength - 0.02m);
            ret.subShape2VerLength = Math.Min(ret.subShape2VerLength, ret.subShapeVerLength - 0.01m);

            // Pin subshape in place.
            ret.subShape2HorOffset = 0;
            ret.subShape2VerOffset = Math.Min(ret.subShape2VerOffset, ret.subShapeVerLength - ret.subShape2VerLength);

            // Clamp dimensions
            ret.subShape3HorLength = Math.Min(ret.subShape3HorLength, ret.subShapeHorLength - 0.02m);
            ret.subShape3VerLength = Math.Min(ret.subShape3VerLength, ret.subShapeVerLength - 0.01m);

            // Pin subshape in place.
            ret.subShape3HorOffset = ret.subShapeHorLength - ret.subShape3HorLength;
            ret.subShape3VerOffset = Math.Min(ret.subShape3VerOffset, ret.subShapeVerLength - ret.subShape3VerLength);
        }

        void Ushape_limits(ref PatternElement ret)
        {
            // Clamp dimensions
            ret.subShape2HorLength = Math.Min(ret.subShape2HorLength, ret.subShapeHorLength - 0.02m);
            ret.subShape2VerLength = Math.Min(ret.subShape2VerLength, ret.subShapeVerLength - 0.01m);

            // Pin subshape in place.
            ret.subShape2HorOffset = (ret.subShapeHorLength - ret.subShape2HorLength) / 2;
            ret.subShape2VerOffset = ret.subShapeVerLength - ret.subShape2VerLength;

            ret.subShape3HorLength = 0;
            ret.subShape3VerLength = 0;
            ret.subShape3HorOffset = 0;
            ret.subShape3VerOffset = 0;
        }

        void Lshape_limits(ref PatternElement ret)
        {
            // Clamp vertical dimension
            ret.subShape2VerLength = Math.Min(ret.subShape2VerLength, ret.subShapeVerLength);

            // Pin subshape in place.
            ret.subShape2HorOffset = ret.subShapeHorLength;
            ret.subShape2VerOffset = 0;

            ret.subShape3HorLength = 0;
            ret.subShape3VerLength = 0;
            ret.subShape3HorOffset = 0;
            ret.subShape3VerOffset = 0;
        }

        void Tshape_limits(ref PatternElement ret)
        {
            ret.subShape2HorOffset = ret.subShapeHorLength;
            ret.subShape2VerLength = Math.Min(ret.subShape2VerLength, ret.subShapeVerLength);
            ret.subShape2VerOffset = Math.Min(ret.subShape2VerOffset, ret.subShapeVerLength - ret.subShape2VerLength);

            ret.subShape3HorLength = 0;
            ret.subShape3VerLength = 0;
            ret.subShape3HorOffset = 0;
            ret.subShape3VerOffset = 0;
        }

        void Xshape_limits(ref PatternElement ret)
        {
            ret.subShape2HorLength = Math.Max(ret.subShape2HorLength, ret.subShapeHorLength + 0.02m);
            ret.subShape2HorOffset = Math.Min(ret.subShape2HorOffset, -(ret.subShapeHorLength - ret.subShape2HorLength) / 2);
            ret.subShape2VerLength = Math.Min(ret.subShape2VerLength, ret.subShapeVerLength);
            ret.subShape2VerOffset = Math.Min(ret.subShape2VerOffset, ret.subShapeVerLength - ret.subShape2VerLength);

            ret.subShape3HorLength = 0;
            ret.subShape3VerLength = 0;
            ret.subShape3HorOffset = 0;
            ret.subShape3VerOffset = 0;
        }

        public string getDescription()
        {
            return pGetDescription();
        }

        string pGetDescription()
        {
            string description = name + "|";

            bool bounding = false;

            switch (shapeIndex)
            {
                case (int)CommonVars.shapeNames.none:
                    description = "none";
                    break;
                case (int)CommonVars.shapeNames.rect:
                    description = "rect";
                    break;
                case (int)CommonVars.shapeNames.Lshape:
                    description = "L";
                    break;
                case (int)CommonVars.shapeNames.Tshape:
                    description = "T";
                    break;
                case (int)CommonVars.shapeNames.Ushape:
                    description = "U";
                    break;
                case (int)CommonVars.shapeNames.Xshape:
                    description = "X";
                    break;
                case (int)CommonVars.shapeNames.Sshape:
                    description = "S";
                    break;
                case (int)CommonVars.shapeNames.text:
                    description = "text";
                    break;
                case (int)CommonVars.shapeNames.bounding:
                    description = "bounding";
                    bounding = true;
                    break;
            }

            description += "|";

            if (!bounding)
            {
                description += "ss0HL:" + subShapeHorLength.ToString() + "|";
                description += "ss0VL:" + subShapeVerLength.ToString() + "|";
                description += "ss0OL:" + subShapeHorOffset.ToString() + "|";
                description += "ss0VO:" + subShapeVerOffset.ToString() + "|";

                if ((shapeIndex != (int)CommonVars.shapeNames.none) && (shapeIndex != (int)CommonVars.shapeNames.none))
                {
                    description += "ss1HL:" + subShape2HorLength.ToString() + "|";
                    description += "ss1VL:" + subShape2VerLength.ToString() + "|";
                    description += "ss1HO:" + subShape2HorOffset.ToString() + "|";
                    description += "ss1VO:" + subShape2VerOffset.ToString() + "|";
                }
                else
                {
                    description += "ss1HL:N/A|";
                    description += "ss1VL:N/A|";
                    description += "ss1HO:N/A|";
                    description += "ss1VO:N/A|";
                }

                if (shapeIndex == (int)CommonVars.shapeNames.Sshape)
                {
                    description += "ss2HL:" + subShape3HorLength.ToString() + "|";
                    description += "ss2VL:" + subShape3VerLength.ToString() + "|";
                    description += "ss2HO:" + subShape3HorOffset.ToString() + "|";
                    description += "ss2VO:" + subShape3VerOffset.ToString() + "|";
                }
                else
                {
                    description += "ss2HL:N/A|";
                    description += "ss2VL:N/A|";
                    description += "ss2HO:N/A|";
                    description += "ss2VO:N/A|";
                }

                if (arrayRef != 0)
                {
                    description += "arrayRef:" + arrayRef.ToString() + "|";
                }
 
                if (pIsXArray() || relativeArray == 1)
                {
                    description += "arrayXCount:" + arrayXCount.ToString() + "|";
                    description += "arrayXSpace:" + arrayXSpace.ToString() + "|";
                }
                if (pIsYArray() || relativeArray == 1)
                {
                    description += "arrayYCount:" + arrayYCount.ToString() + "|";
                    description += "arrayYSpace:" + arrayYSpace.ToString() + "|";
                }
            }
            else
            {
                description += "boundingLeft:" + boundingLeft.ToString() + "|";
                description += "boundingRight:" + boundingRight.ToString() + "|";
                description += "boundingTop:" + boundingTop.ToString() + "|";
                description += "boundingBottom:" + boundingBottom.ToString() + "|";
            }

            description += "X:" + x.ToString() + "|";
            description += "Y:" + y.ToString() + "|";

            description += "refBoundsAfterRotation:" + refBoundsAfterRotation.ToString() + "|";
            description += "refArrayBoundsAfterRotation:" + refArrayBoundsAfterRotation.ToString() + "|";

            // Normalize rotation value 0-359 range
            decimal r = rotation % 360;
            if (rotation < 0)
            {
                r = (r + 360) % 360;
            }
            description += "R:" + r.ToString();

            if (pIsXArray() || pIsYArray() || relativeArray == 1)
            {
                description += "|";
                // Normalize rotation value 0-359 range
                decimal aR = arrayRotation % 360;
                if (arrayRotation < 0)
                {
                    aR = (aR + 360) % 360;
                }
                description += "arrayR:" + aR.ToString();
            }

            return description;
        }
    }
}
