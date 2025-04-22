using geoWrangler;
using System;
using System.Collections.Generic;
using System.Linq;
using Clipper2Lib;
using shapeEngine;

namespace Quilt;

public class PatternElement : ShapeSettings
{
    public enum Position { BL, TL, BR, TR, BS, TS, LS, RS, C }
    public override bool Equals(object obj)
    {
        return getDescription() == ((PatternElement)obj)?.getDescription();
    }

    public override int GetHashCode()
    {
        return pGetHashCode();
    }

    private int pGetHashCode()
    {
        return pGetDescription().GetHashCode();
    }
    
    private const int default_posXRefIndex = 0;
    private const int default_posYRefIndex = 0;
    private const int default_rotRefIndex = 0;
    private const int default_subShapeRef = 0;
    private const int defaultSteps = 1;
    private const int defaultArrayCount = 1;
    private const int defaultLayoutLDValue = -1;

    public PatternElement()
    {
        pNewPE();
    }

    private void reset()
    {
        pNewPE();
    }

    private void pNewPE()
    {
        name = "";
        subShapeHorLengthInc = 0;
        subShapeVerLengthInc = 0;
        subShapeHorOffsetInc = 0;
        subShapeVerOffsetInc = 0;
        subShape2HorLengthInc = 0;
        subShape2VerLengthInc = 0;
        subShape2HorOffsetInc = 0;
        subShape2VerOffsetInc = 0;
        subShape3HorLengthInc = 0;
        subShape3VerLengthInc = 0;
        subShape3HorOffsetInc = 0;
        subShape3VerOffsetInc = 0;
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
        xPosSubShapeRef = getDefaultInt(ShapeSettings.properties_i.subShapeRefIndex);
        yPosSubShapeRef = getDefaultInt(ShapeSettings.properties_i.subShapeRefIndex);
        xPosSubShapeRefPos = (int)subShapeHorLocs.L;
        yPosSubShapeRefPos = (int)subShapeVerLocs.B;
        HorTipSteps = defaultSteps;
        VerTipSteps = defaultSteps;

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

        arrayMinXCount = defaultArrayCount;
        arrayXSteps = defaultSteps;
        arrayXInc = 0;
        arrayXSpace = 0;
        arrayMinXSpace = 0;
        arrayXSpaceInc = 0;
        arrayMinYCount = defaultArrayCount;
        arrayYSteps = defaultSteps;
        arrayYInc = 0;
        arrayYSpace = 0;
        arrayMinYSpace = 0;
        arrayYSpaceInc = 0;

        arrayRotation = 0;
        arrayRotationSteps = defaultSteps;
        arrayRotRef = default_rotRefIndex;
        arrayRotRefUseArray = 0;

        refPivot = 0;
        refArrayPivot = 0;
        refBoundsAfterRotation = 0;
        refArrayBoundsAfterRotation = 0;

        relativeArray = 0;

        externalGeoCoordX = new List<decimal>();
        externalGeoCoordY = new List<decimal>();

        linkedElementIndex = -1;

        layoutLayer = defaultLayoutLDValue;
        layoutDataType = defaultLayoutLDValue;

        midpoint = new PointD(double.NaN, double.NaN);

        s0MinHLRef = 0;
        s0MinHLSubShapeRef = 0;
        s0HLIncRef = 0;
        s0HLIncSubShapeRef = 0;
        s0HLStepsRef = 0;
        s0HLStepsSubShapeRef = 0;
        s1MinHLRef = 0;
        s1MinHLSubShapeRef = 0;
        s1HLIncRef = 0;
        s1HLIncSubShapeRef = 0;
        s1HLStepsRef = 0;
        s1HLStepsSubShapeRef = 0;
        s2MinHLRef = 0;
        s2MinHLSubShapeRef = 0;
        s2HLIncRef = 0;
        s2HLIncSubShapeRef = 0;
        s2HLStepsRef = 0;
        s2HLStepsSubShapeRef = 0;
        s0MinVLRef = 0;
        s0MinVLSubShapeRef = 0;
        s0VLIncRef = 0;
        s0VLIncSubShapeRef = 0;
        s0VLStepsRef = 0;
        s0VLStepsSubShapeRef = 0;
        s1MinVLRef = 0;
        s1MinVLSubShapeRef = 0;
        s1VLIncRef = 0;
        s1VLIncSubShapeRef = 0;
        s1VLStepsRef = 0;
        s1VLStepsSubShapeRef = 0;
        s2MinVLRef = 0;
        s2MinVLSubShapeRef = 0;
        s2VLIncRef = 0;
        s2VLIncSubShapeRef = 0;
        s2VLStepsRef = 0;
        s2VLStepsSubShapeRef = 0;
        s0MinHORef = 0;
        s0MinHOSubShapeRef = 0;
        s0HOIncRef = 0;
        s0HOIncSubShapeRef = 0;
        s0HOStepsRef = 0;
        s0HOStepsSubShapeRef = 0;
        s1MinHORef = 0;
        s1MinHOSubShapeRef = 0;
        s1HOIncRef = 0;
        s1HOIncSubShapeRef = 0;
        s1HOStepsRef = 0;
        s1HOStepsSubShapeRef = 0;
        s2MinHORef = 0;
        s2MinHOSubShapeRef = 0;
        s2HOIncRef = 0;
        s2HOIncSubShapeRef = 0;
        s2HOStepsRef = 0;
        s2HOStepsSubShapeRef = 0;
        s0MinVORef = 0;
        s0MinVOSubShapeRef = 0;
        s0VOIncRef = 0;
        s0VOIncSubShapeRef = 0;
        s0VOStepsRef = 0;
        s0VOStepsSubShapeRef = 0;
        s1MinVORef = 0;
        s1MinVOSubShapeRef = 0;
        s1VOIncRef = 0;
        s1VOIncSubShapeRef = 0;
        s1VOStepsRef = 0;
        s1VOStepsSubShapeRef = 0;
        s2MinVORef = 0;
        s2MinVOSubShapeRef = 0;
        s2VOIncRef = 0;
        s2VOIncSubShapeRef = 0;
        s2VOStepsRef = 0;
        s2VOStepsSubShapeRef = 0;
        s0HLRefFinal = 0;
        s0VLRefFinal = 0;
        s0HORefFinal = 0;
        s0VORefFinal = 0;
            
        s1HLRefFinal = 0;
        s1VLRefFinal = 0;
        s1HORefFinal = 0;
        s1VORefFinal = 0;
            
        s2HLRefFinal = 0;
        s2VLRefFinal = 0;
        s2HORefFinal = 0;
        s2VORefFinal = 0;

        s0TipRef = 0;
        s0TipSubShapeRef = 0;
        s1TipRef = 0;
        s1TipSubShapeRef = 0;
        s2TipRef = 0;
        s2TipSubShapeRef = 0;

        minHorTipLength = 0;
        minVerTipLength = 0;
        horTipLengthInc = 0;
        verTipLengthInc = 0;
        HorTipSteps = 1;
        VerTipSteps = 1;
        
        MinHTRef = 0;
        MinVTRef = 0;
        HorTipRefFinal = 0;
        VerTipRefFinal = 0;
        hTIncRef = 0;
        vTIncRef = 0;
        hTStepsRef = 0;
        vTStepsRef = 0;
    }

    public PatternElement(PatternElement source)
    {
        pNewPE(source);
    }

    private void pNewPE(PatternElement source)
    {
        name = source.name;
        setInt(properties_i.shapeIndex, source.getInt(ShapeSettings.properties_i.shapeIndex));
        setInt(properties_i.subShapeIndex, source.getInt(ShapeSettings.properties_i.subShapeRefIndex));
        setInt(properties_i.posIndex, source.getInt(properties_i.posIndex));

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

        setInt(properties_i.shape0Tip, source.getInt(properties_i.shape0Tip));

        subShape2MinHorLength = source.subShape2MinHorLength;
        s1HorLengthSteps = source.s1HorLengthSteps;

        subShape2MinVerLength = source.subShape2MinVerLength;
        s1VerLengthSteps = source.s1VerLengthSteps;

        subShape2MinHorOffset = source.subShape2MinHorOffset;
        s1HorOffsetSteps = source.s1HorOffsetSteps;

        subShape2MinVerOffset = source.subShape2MinVerOffset;
        s1VerOffsetSteps = source.s1VerOffsetSteps;

        setInt(properties_i.shape1Tip, source.getInt(properties_i.shape1Tip));

        subShape3MinHorLength = source.subShape3MinHorLength;
        s2HorLengthSteps = source.s2HorLengthSteps;

        subShape3MinVerLength = source.subShape3MinVerLength;
        s2VerLengthSteps = source.s2VerLengthSteps;

        subShape3MinHorOffset = source.subShape3MinHorOffset;
        s2HorOffsetSteps = source.s2HorOffsetSteps;

        subShape3MinVerOffset = source.subShape3MinVerOffset;
        s2VerOffsetSteps = source.s2VerOffsetSteps;

        setInt(properties_i.shape2Tip, source.getInt(properties_i.shape2Tip));

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

        arrayMinXCount = source.arrayMinXCount;
        arrayXInc = source.arrayXInc;
        arrayXSteps = source.arrayXSteps;
        arrayXSpace = source.arrayXSpace;
        arrayXSpaceSteps = source.arrayXSpaceSteps;
        arrayMinXSpace = source.arrayMinXSpace;
        arrayXSpaceInc = source.arrayXSpaceInc;
        arrayMinYCount = source.arrayMinYCount;
        arrayYInc = source.arrayYInc;
        arrayYSteps = source.arrayYSteps;
        arrayYSpace = source.arrayYSpace;
        arrayYSpaceSteps = source.arrayYSpaceSteps;
        arrayMinYSpace = source.arrayMinYSpace;
        arrayYSpaceInc = source.arrayYSpaceInc;

        arrayXCount = source.arrayXCount;
        arrayYCount = source.arrayYCount;

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

        externalGeoCoordX = source.externalGeoCoordX.ToList();
        externalGeoCoordY = source.externalGeoCoordY.ToList();

        linkedElementIndex = source.linkedElementIndex;

        layoutLayer = source.layoutLayer;
        layoutDataType = source.layoutDataType;

        midpoint = source.midpoint;
            
        s0MinHLRef = source.s0MinHLRef;
        s0MinHLSubShapeRef = source.s0MinHLSubShapeRef;
        s0HLIncRef = source.s0HLIncRef;
        s0HLIncSubShapeRef = source.s0HLIncSubShapeRef;
        s0HLStepsRef = source.s0HLStepsRef;
        s0HLStepsSubShapeRef = source.s0HLStepsSubShapeRef;
        s1MinHLRef = source.s1MinHLRef;
        s1MinHLSubShapeRef = source.s1MinHLSubShapeRef;
        s1HLIncRef = source.s1HLIncRef;
        s1HLIncSubShapeRef = source.s1HLIncSubShapeRef;
        s1HLStepsRef = source.s1HLStepsRef;
        s1HLStepsSubShapeRef = source.s1HLStepsSubShapeRef;
        s2MinHLRef = source.s2MinHLRef;
        s2MinHLSubShapeRef = source.s2MinHLSubShapeRef;
        s2HLIncRef = source.s2HLIncRef;
        s2HLIncSubShapeRef = source.s2HLIncSubShapeRef;
        s2HLStepsRef = source.s2HLStepsRef;
        s2HLStepsSubShapeRef = source.s2HLStepsSubShapeRef;
        s0MinVLRef = source.s0MinVLRef;
        s0MinVLSubShapeRef = source.s0MinVLSubShapeRef;
        s0VLIncRef = source.s0VLIncRef;
        s0VLIncSubShapeRef = source.s0VLIncSubShapeRef;
        s0VLStepsRef = source.s0VLStepsRef;
        s0VLStepsSubShapeRef = source.s0VLStepsSubShapeRef;
        s1MinVLRef = source.s1MinVLRef;
        s1MinVLSubShapeRef = source.s1MinVLSubShapeRef;
        s1VLIncRef = source.s1VLIncRef;
        s1VLIncSubShapeRef = source.s1VLIncSubShapeRef;
        s1VLStepsRef = source.s1VLStepsRef;
        s1VLStepsSubShapeRef = source.s1VLStepsSubShapeRef;
        s2MinVLRef = source.s2MinVLRef;
        s2MinVLSubShapeRef = source.s2MinVLSubShapeRef;
        s2VLIncRef = source.s2VLIncRef;
        s2VLIncSubShapeRef = source.s2VLIncSubShapeRef;
        s2VLStepsRef = source.s2VLStepsRef;
        s2VLStepsSubShapeRef = source.s2VLStepsSubShapeRef;
        s0MinHORef = source.s0MinHORef;
        s0MinHOSubShapeRef = source.s0MinHOSubShapeRef;
        s0HOIncRef = source.s0HOIncRef;
        s0HOIncSubShapeRef = source.s0HOIncSubShapeRef;
        s0HOStepsRef = source.s0HOStepsRef;
        s0HOStepsSubShapeRef = source.s0HOStepsSubShapeRef;
        s1MinHORef = source.s1MinHORef;
        s1MinHOSubShapeRef = source.s1MinHOSubShapeRef;
        s1HOIncRef = source.s1HOIncRef;
        s1HOIncSubShapeRef = source.s1HOIncSubShapeRef;
        s1HOStepsRef = source.s1HOStepsRef;
        s1HOStepsSubShapeRef = source.s1HOStepsSubShapeRef;
        s2MinHORef = source.s2MinHORef;
        s2MinHOSubShapeRef = source.s2MinHOSubShapeRef;
        s2HOIncRef = source.s2HOIncRef;
        s2HOIncSubShapeRef = source.s2HOIncSubShapeRef;
        s2HOStepsRef = source.s2HOStepsRef;
        s2HOStepsSubShapeRef = source.s2HOStepsSubShapeRef;
        s0MinVORef = source.s2HOStepsSubShapeRef;
        s0MinVOSubShapeRef = source.s0MinVOSubShapeRef;
        s0VOIncRef = source.s0VOIncRef;
        s0VOIncSubShapeRef = source.s0VOIncSubShapeRef;
        s0VOStepsRef = source.s0VOStepsRef;
        s0VOStepsSubShapeRef = source.s0VOStepsSubShapeRef;
        s1MinVORef = source.s1MinVORef;
        s1MinVOSubShapeRef = source.s1MinVOSubShapeRef;
        s1VOIncRef = source.s1VOIncRef;
        s1VOIncSubShapeRef = source.s1VOIncSubShapeRef;
        s1VOStepsRef = source.s1VOStepsRef;
        s1VOStepsSubShapeRef = source.s1VOStepsSubShapeRef;
        s2MinVORef = source.s2MinVORef;
        s2MinVOSubShapeRef = source.s2MinVOSubShapeRef;
        s2VOIncRef = source.s2VOIncRef;
        s2VOIncSubShapeRef = source.s2VOIncSubShapeRef;
        s2VOStepsRef = source.s2VOStepsRef;
        s2VOStepsSubShapeRef = source.s2VOStepsSubShapeRef;

        s0HLRefFinal = source.s0HLRefFinal;
        s0VLRefFinal = source.s0VLRefFinal;
        s0HORefFinal = source.s0HORefFinal;
        s0VORefFinal = source.s0VORefFinal;
            
        s1HLRefFinal = source.s1HLRefFinal;
        s1VLRefFinal = source.s1VLRefFinal;
        s1HORefFinal = source.s1HORefFinal;
        s1VORefFinal = source.s1VORefFinal;
            
        s2HLRefFinal = source.s2HLRefFinal;
        s2VLRefFinal = source.s2VLRefFinal;
        s2HORefFinal = source.s2HORefFinal;
        s2VORefFinal = source.s2VORefFinal;

        s0TipRef = source.s0TipRef;
        s0TipSubShapeRef = source.s0TipSubShapeRef;
        s1TipRef = source.s1TipRef;
        s1TipSubShapeRef = source.s1TipSubShapeRef;
        s2TipRef = source.s2TipRef;
        s2TipSubShapeRef = source.s2TipSubShapeRef;

        minHorTipLength = source.minHorTipLength;
        minVerTipLength = source.minVerTipLength;
        horTipLengthInc = source.horTipLengthInc;
        verTipLengthInc = source.verTipLengthInc;
        HorTipSteps = source.HorTipSteps;
        VerTipSteps = source.VerTipSteps;
        horTipBias = source.horTipBias;
        verTipBias = source.verTipBias;
        v_hTSteps = source.v_hTSteps;
        v_vTSteps = source.v_vTSteps;
        
        MinHTRef = source.MinHTRef;
        MinVTRef = source.MinVTRef;
        HorTipRefFinal = source.HorTipRefFinal;
        VerTipRefFinal = source.VerTipRefFinal;
        hTIncRef = source.hTIncRef;
        vTIncRef = source.vTIncRef;
        hTStepsRef = source.hTStepsRef;
        vTStepsRef = source.vTStepsRef;

        horTipBias = source.horTipBias;
        verTipBias = source.verTipBias;

    }

    private PointD midpoint;

    public bool midPointSet()
    {
        return (!double.IsNaN(midpoint.x) && !double.IsNaN(midpoint.y));
    }

    public PointD getMidPoint()
    {
        return new (midpoint.x, midpoint.y);
    }

    public void setMidPoint(PointD point)
    {
        midpoint = (double.IsNaN(point.x) || double.IsNaN(point.y))  ? new PointD(double.NaN, double.NaN) : new (point.x, point.y);
    }

    private string name;

    public new enum properties_s
    {
        name
    }

    public string getString(properties_s p)
    {
        return pGetString(p);
    }

    private string pGetString(properties_s p)
    {
        return p == properties_s.name ? name : "";
    }

    public void setString(properties_s p, string val)
    {
        pSetString(p, val);
    }

    private void pSetString(properties_s p, string val)
    {
        switch (p)
        {
            case properties_s.name:
                name = val;
                break;
        }
    }

    private int xPosSteps, yPosSteps;
    private int xPosRef, yPosRef;

    private int s0MinHLRef,
        s0MinHLSubShapeRef,
        s0HLRefFinal,
        s0HLIncRef,
        s0HLIncSubShapeRef,
        s0HLStepsRef,
        s0HLStepsSubShapeRef,
        s1MinHLRef,
        s1MinHLSubShapeRef,
        s1HLRefFinal,
        s1HLIncRef,
        s1HLIncSubShapeRef,
        s1HLStepsRef,
        s1HLStepsSubShapeRef,
        s2MinHLRef,
        s2MinHLSubShapeRef,
        s2HLRefFinal,
        s2HLIncRef,
        s2HLIncSubShapeRef,
        s2HLStepsRef,
        s2HLStepsSubShapeRef,
        s0MinVLRef,
        s0MinVLSubShapeRef,
        s0VLRefFinal,
        s0VLIncRef,
        s0VLIncSubShapeRef,
        s0VLStepsRef,
        s0VLStepsSubShapeRef,
        s1MinVLRef,
        s1MinVLSubShapeRef,
        s1VLRefFinal,
        s1VLIncRef,
        s1VLIncSubShapeRef,
        s1VLStepsRef,
        s1VLStepsSubShapeRef,
        s2MinVLRef,
        s2MinVLSubShapeRef,
        s2VLRefFinal,
        s2VLIncRef,
        s2VLIncSubShapeRef,
        s2VLStepsRef,
        s2VLStepsSubShapeRef,
        s0MinHORef,
        s0MinHOSubShapeRef,
        s0HORefFinal,
        s0HOIncRef,
        s0HOIncSubShapeRef,
        s0HOStepsRef,
        s0HOStepsSubShapeRef,
        s1MinHORef,
        s1MinHOSubShapeRef,
        s1HORefFinal,
        s1HOIncRef,
        s1HOIncSubShapeRef,
        s1HOStepsRef,
        s1HOStepsSubShapeRef,
        s2MinHORef,
        s2MinHOSubShapeRef,
        s2HORefFinal,
        s2HOIncRef,
        s2HOIncSubShapeRef,
        s2HOStepsRef,
        s2HOStepsSubShapeRef,
        s0MinVORef,
        s0MinVOSubShapeRef,
        s0VORefFinal,
        s0VOIncRef,
        s0VOIncSubShapeRef,
        s0VOStepsRef,
        s0VOStepsSubShapeRef,
        s1MinVORef,
        s1MinVOSubShapeRef,
        s1VORefFinal,
        s1VOIncRef,
        s1VOIncSubShapeRef,
        s1VOStepsRef,
        s1VOStepsSubShapeRef,
        s2MinVORef,
        s2MinVOSubShapeRef,
        s2VORefFinal,
        s2VOIncRef,
        s2VOIncSubShapeRef,
        s2VOStepsRef,
        s2VOStepsSubShapeRef;

    private int xPosSubShapeRef, yPosSubShapeRef, xPosSubShapeRefPos, yPosSubShapeRefPos;
    private int s0HorLengthSteps, s0VerLengthSteps, s0HorOffsetSteps, s0VerOffsetSteps;
    private int s1HorLengthSteps, s1VerLengthSteps, s1HorOffsetSteps, s1VerOffsetSteps;
    private int s2HorLengthSteps, s2VerLengthSteps, s2HorOffsetSteps, s2VerOffsetSteps;
    private int rotationSteps;
    private int boundingLeftSteps, boundingRightSteps, boundingTopSteps, boundingBottomSteps;
    private int rotRef, rotRefUseArray;
    private int flipH, flipV, alignX, alignY;
    private int arrayXCount, arrayYCount;
    private int arrayMinXCount, arrayMinYCount, arrayXSteps, arrayYSteps, arrayXInc, arrayYInc;
    private int arrayXSpaceSteps, arrayYSpaceSteps;
    private int arrayRotationSteps, arrayRotRef, arrayRotRefUseArray;
    private int arrayRef;
    private int refPivot, refArrayPivot;
    private int refBoundsAfterRotation, refArrayBoundsAfterRotation;
    private int relativeArray;
    private int linkedElementIndex;
    private int s0TipRef, s0TipSubShapeRef, s1TipRef, s1TipSubShapeRef, s2TipRef, s2TipSubShapeRef;
    private int HorTipSteps, VerTipSteps, HorTipRefFinal, VerTipRefFinal;
    private int MinHTRef, MinVTRef, hTIncRef, vTIncRef, hTStepsRef, vTStepsRef;

    // Used for layout-originated elements, to try and map back to original LD on export.
    private int layoutLayer, layoutDataType;

    public bool isXArray()
    {
        return pIsXArray();
    }

    private bool pIsXArray()
    {
        bool ret = arrayMinXCount > 1 || arrayXCount > 1;
        // Also need to consider whether this is an incremental array.
        ret = ret || arrayXInc > 0 && v_array_X_Steps > 0;
        return ret;
    }

    public bool isYArray()
    {
        return pIsYArray();
    }

    private bool pIsYArray()
    {
        bool ret = arrayMinYCount > 1 || arrayYCount > 1;
        return ret;
    }

    public new enum properties_i
    {
        shapeIndex, shape0Tip, shape1Tip, shape2Tip, subShapeIndex, posIndex, xPosRef, yPosRef, xPosSteps, yPosSteps,
        xPosSubShapeRef, yPosSubShapeRef, xPosSubShapeRefPos, yPosSubShapeRefPos,
        horLengthSteps, verLengthSteps, horOffsetSteps, verOffsetSteps,
        rotationSteps, rotationRef, rotRefUseArray,
        boundingLeftSteps, boundingRightSteps, boundingTopSteps, boundingBottomSteps,
        maxVariants,
        flipH, flipV, alignX, alignY,
        arrayXCount, arrayYCount,
        arrayMinXCount, arrayMinYCount,
        arrayXInc, arrayYInc,
        arrayXSteps, arrayYSteps,
        arrayXSpaceSteps, arrayYSpaceSteps,
        arrayRotationSteps, arrayRotationRef, arrayRotRefUseArray,
        arrayRef,
        refPivot, refArrayPivot,
        refBoundsAfterRotation, refArrayBoundsAfterRotation,
        relativeArray,
        externalGeoVertexCount,
        linkedElementIndex,
        layoutLayer, layoutDataType,
        MinHLRef, MinHLSubShapeRef, HLRefFinal,
        HLIncRef, HLIncSubShapeRef,
        HLStepsRef, HLStepsSubShapeRef,
        MinVLRef, MinVLSubShapeRef, VLRefFinal,
        VLIncRef, VLIncSubShapeRef,
        VLStepsRef, VLStepsSubShapeRef,
        MinHORef, MinHOSubShapeRef, HORefFinal,
        HOIncRef, HOIncSubShapeRef,
        HOStepsRef, HOStepsSubShapeRef,
        MinVORef, MinVOSubShapeRef, VORefFinal,
        VOIncRef, VOIncSubShapeRef,
        VOStepsRef, VOStepsSubShapeRef,
        HorTipSteps, VerTipSteps,
        MinHTRef, MinVTRef, HTIncRef, VTIncRef, HTStepsRef, VTStepsRef, HTRefFinal, VTRefFinal,
        tipRef, tipSubShapeRef
    }

    public int getSubShapeCount()
    {
        return pGetSubShapeCount();
    }

    private int pGetSubShapeCount()
    {
        return ShapeLibrary.getSubShapeCount(getInt(properties_i.shapeIndex));
    }
        
    public int getInt(properties_i p, int _subShapeRef = -1)
    {
        return pGetInt(p, _subShapeRef);
    }

    private int pGetInt(properties_i p, int _subShapeRef = -1)
    {
        bool showError = false;
        int ret = 0;
        switch (p)
        {
            case properties_i.shapeIndex:
                ret = getInt(ShapeSettings.properties_i.shapeIndex);
                break;
            case properties_i.shape0Tip:
                ret = getInt(ShapeSettings.properties_i.subShapeTipLocIndex);
                break;
            case properties_i.shape1Tip:
                ret = getInt(ShapeSettings.properties_i.subShape2TipLocIndex);
                break;
            case properties_i.shape2Tip:
                ret = getInt(ShapeSettings.properties_i.subShape3TipLocIndex);
                break;
            case properties_i.subShapeIndex:
                ret = getInt(ShapeSettings.properties_i.subShapeRefIndex);
                break;
            case properties_i.posIndex:
                ret = getInt(ShapeSettings.properties_i.posInSubShapeIndex);
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
            case properties_i.horLengthSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HorLengthSteps;
                        break;
                    case 2:
                        ret = s2HorLengthSteps;
                        break;
                    case 0:
                        ret = s0HorLengthSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.horOffsetSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HorOffsetSteps;
                        break;
                    case 2:
                        ret = s2HorOffsetSteps;
                        break;
                    case 0:
                        ret = s0HorOffsetSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.verLengthSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VerLengthSteps;
                        break;
                    case 2:
                        ret = s2VerLengthSteps;
                        break;
                    case 0:
                        ret = s0VerLengthSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.verOffsetSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VerOffsetSteps;
                        break;
                    case 2:
                        ret = s2VerOffsetSteps;
                        break;
                    case 0:
                        ret = s0VerOffsetSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
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
                ret = pCalcMaxVariants();
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
            case properties_i.arrayMinXCount:
                ret = arrayMinXCount;
                break;
            case properties_i.arrayMinYCount:
                ret = arrayMinYCount;
                break;
            case properties_i.arrayXInc:
                ret = arrayXInc;
                break;
            case properties_i.arrayYInc:
                ret = arrayYInc;
                break;
            case properties_i.arrayXSteps:
                ret = arrayXSteps;
                break;
            case properties_i.arrayYSteps:
                ret = arrayYSteps;
                break;
            case properties_i.arrayXSpaceSteps:
                ret = arrayXSpaceSteps;
                break;
            case properties_i.arrayYSpaceSteps:
                ret = arrayYSpaceSteps;
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
            case properties_i.externalGeoVertexCount:
                ret = externalGeoCoordX.Count;
                break;
            case properties_i.linkedElementIndex:
                ret = linkedElementIndex;
                break;
            case properties_i.layoutLayer:
                ret = layoutLayer;
                break;
            case properties_i.layoutDataType:
                ret = layoutDataType;
                break;
            case properties_i.HLRefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HLRefFinal;
                        break;
                    case 2:
                        ret = s2HLRefFinal;
                        break;
                    case 0:
                        ret = s0HLRefFinal;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHLRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinHLRef;
                        break;
                    case 2:
                        ret = s2MinHLRef;
                        break;
                    case 0:
                        ret = s0MinHLRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHLSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinHLSubShapeRef;
                        break;
                    case 2:
                        ret = s2MinHLSubShapeRef;
                        break;
                    case 0:
                        ret = s0MinHLSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HLIncRef;
                        break;
                    case 2:
                        ret = s2HLIncRef;
                        break;
                    case 0:
                        ret = s0HLIncRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HLIncSubShapeRef;
                        break;
                    case 2:
                        ret = s2HLIncSubShapeRef;
                        break;
                    case 0:
                        ret = s0HLIncSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HLStepsRef;
                        break;
                    case 2:
                        ret = s2HLStepsRef;
                        break;
                    case 0:
                        ret = s0HLStepsRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HLStepsSubShapeRef;
                        break;
                    case 2:
                        ret = s2HLStepsSubShapeRef;
                        break;
                    case 0:
                        ret = s0HLStepsSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLRefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VLRefFinal;
                        break;
                    case 2:
                        ret = s2VLRefFinal;
                        break;
                    case 0:
                        ret = s0VLRefFinal;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVLRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinVLRef;
                        break;
                    case 2:
                        ret = s2MinVLRef;
                        break;
                    case 0:
                        ret = s0MinVLRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVLSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinVLSubShapeRef;
                        break;
                    case 2:
                        ret = s2MinVLSubShapeRef;
                        break;
                    case 0:
                        ret = s0MinVLSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VLIncRef;
                        break;
                    case 2:
                        ret = s2VLIncRef;
                        break;
                    case 0:
                        ret = s0VLIncRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VLIncSubShapeRef;
                        break;
                    case 2:
                        ret = s2VLIncSubShapeRef;
                        break;
                    case 0:
                        ret = s0VLIncSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VLStepsRef;
                        break;
                    case 2:
                        ret = s2VLStepsRef;
                        break;
                    case 0:
                        ret = s0VLStepsRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VLStepsSubShapeRef;
                        break;
                    case 2:
                        ret = s2VLStepsSubShapeRef;
                        break;
                    case 0:
                        ret = s0VLStepsSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HORefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HORefFinal;
                        break;
                    case 2:
                        ret = s2HORefFinal;
                        break;
                    case 0:
                        ret = s0HORefFinal;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHORef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinHORef;
                        break;
                    case 2:
                        ret = s2MinHORef;
                        break;
                    case 0:
                        ret = s0MinHORef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHOSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinHOSubShapeRef;
                        break;
                    case 2:
                        ret = s2MinHOSubShapeRef;
                        break;
                    case 0:
                        ret = s0MinHOSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HOIncRef;
                        break;
                    case 2:
                        ret = s2HOIncRef;
                        break;
                    case 0:
                        ret = s0HOIncRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HOIncSubShapeRef;
                        break;
                    case 2:
                        ret = s2HOIncSubShapeRef;
                        break;
                    case 0:
                        ret = s0HOIncSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HOStepsRef;
                        break;
                    case 2:
                        ret = s2HOStepsRef;
                        break;
                    case 0:
                        ret = s0HOStepsRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1HOStepsSubShapeRef;
                        break;
                    case 2:
                        ret = s2HOStepsSubShapeRef;
                        break;
                    case 0:
                        ret = s0HOStepsSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VORefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VORefFinal;
                        break;
                    case 2:
                        ret = s2VORefFinal;
                        break;
                    case 0:
                        ret = s0VORefFinal;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVORef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinVORef;
                        break;
                    case 2:
                        ret = s2MinVORef;
                        break;
                    case 0:
                        ret = s0MinVORef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVOSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1MinVOSubShapeRef;
                        break;
                    case 2:
                        ret = s2MinVOSubShapeRef;
                        break;
                    case 0:
                        ret = s0MinVOSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VOIncRef;
                        break;
                    case 2:
                        ret = s2VOIncRef;
                        break;
                    case 0:
                        ret = s0VOIncRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VOIncSubShapeRef;
                        break;
                    case 2:
                        ret = s2VOIncSubShapeRef;
                        break;
                    case 0:
                        ret = s0VOIncSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VOStepsRef;
                        break;
                    case 2:
                        ret = s2VOStepsRef;
                        break;
                    case 0:
                        ret = s0VOStepsRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1VOStepsSubShapeRef;
                        break;
                    case 2:
                        ret = s2VOStepsSubShapeRef;
                        break;
                    case 0:
                        ret = s0VOStepsSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.tipRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1TipRef;
                        break;
                    case 2:
                        ret = s2TipRef;
                        break;
                    case 0:
                        ret = s0TipRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.tipSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = s1TipSubShapeRef;
                        break;
                    case 2:
                        ret = s2TipSubShapeRef;
                        break;
                    case 0:
                        ret = s0TipSubShapeRef;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HorTipSteps:
                ret = HorTipSteps;
                break;
            case properties_i.VerTipSteps:
                ret = VerTipSteps;
                break;
            case properties_i.MinHTRef:
                ret = MinHTRef;
                break;
            case properties_i.HTIncRef:
                ret = hTIncRef;
                break;
            case properties_i.HTStepsRef:
                ret = hTStepsRef;
                break;
            case properties_i.MinVTRef:
                ret = MinVTRef;
                break;
            case properties_i.VTIncRef:
                ret = vTIncRef;
                break;
            case properties_i.VTStepsRef:
                ret = vTStepsRef;
                break;
            case properties_i.HTRefFinal:
                ret = HorTipRefFinal;
                break;
            case properties_i.VTRefFinal:
                ret = VerTipRefFinal;
                break;
        }

        if (!showError)
        {
            return ret;
        }

        Error.ErrorReporter.showMessage_OK(p + ",subshapeRef:" + _subShapeRef, "Coder error - getInt");
        throw new Exception();
    }

    public void setInt(properties_i p, int val, int _subShapeRef = -1)
    {
        pSetInt(p, val, _subShapeRef);
    }

    private void pSetInt(properties_i p, int val, int _subShapeRef = -1)
    {
        bool showError = false;

        switch (p)
        {
            case properties_i.shapeIndex:
                setInt(ShapeSettings.properties_i.shapeIndex, val);
                break;
            case properties_i.shape0Tip:
                setInt(ShapeSettings.properties_i.subShapeTipLocIndex, val);
                break;
            case properties_i.shape1Tip:
                setInt(ShapeSettings.properties_i.subShape2TipLocIndex, val);
                break;
            case properties_i.shape2Tip:
                setInt(ShapeSettings.properties_i.subShape3TipLocIndex, val);
                break;
            case properties_i.subShapeIndex:
                setInt(ShapeSettings.properties_i.subShapeRefIndex, val);
                break;
            case properties_i.posIndex:
                setInt(ShapeSettings.properties_i.posInSubShapeIndex, val);
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
            case properties_i.horLengthSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HorLengthSteps = val;
                        break;
                    case 2:
                        s2HorLengthSteps = val;
                        break;
                    case 0:
                        s0HorLengthSteps = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.horOffsetSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HorOffsetSteps = val;
                        break;
                    case 2:
                        s2HorOffsetSteps = val;
                        break;
                    case 0:
                        s0HorOffsetSteps = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.verLengthSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VerLengthSteps = val;
                        break;
                    case 2:
                        s2VerLengthSteps = val;
                        break;
                    case 0:
                        s0VerLengthSteps = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.verOffsetSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VerOffsetSteps = val;
                        break;
                    case 2:
                        s2VerOffsetSteps = val;
                        break;
                    case 0:
                        s0VerOffsetSteps = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
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
            case properties_i.arrayMinXCount:
                arrayMinXCount = val;
                break;
            case properties_i.arrayMinYCount:
                arrayMinYCount = val;
                break;
            case properties_i.arrayXInc:
                arrayXInc = val;
                break;
            case properties_i.arrayYInc:
                arrayYInc = val;
                break;
            case properties_i.arrayXSteps:
                arrayXSteps = val;
                break;
            case properties_i.arrayYSteps:
                arrayYSteps = val;
                break;
            case properties_i.arrayXSpaceSteps:
                arrayXSpaceSteps = val;
                break;
            case properties_i.arrayYSpaceSteps:
                arrayYSpaceSteps = val;
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
            case properties_i.linkedElementIndex:
                linkedElementIndex = val;
                break;
            case properties_i.layoutLayer:
                layoutLayer = val;
                break;
            case properties_i.layoutDataType:
                layoutDataType = val;
                break;
            case properties_i.HLRefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HLRefFinal = val;
                        break;
                    case 2:
                        s2HLRefFinal = val;
                        break;
                    case 0:
                        s0HLRefFinal = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHLRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHLRef = val;
                        break;
                    case 2:
                        s2MinHLRef = val;
                        break;
                    case 0:
                        s0MinHLRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHLSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHLSubShapeRef = val;
                        break;
                    case 2:
                        s2MinHLSubShapeRef = val;
                        break;
                    case 0:
                        s0MinHLSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HLIncRef = val;
                        break;
                    case 2:
                        s2HLIncRef = val;
                        break;
                    case 0:
                        s0HLIncRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HLIncSubShapeRef = val;
                        break;
                    case 2:
                        s2HLIncSubShapeRef = val;
                        break;
                    case 0:
                        s0HLIncSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HLStepsRef = val;
                        break;
                    case 2:
                        s2HLStepsRef = val;
                        break;
                    case 0:
                        s0HLStepsRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HLStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HLStepsSubShapeRef = val;
                        break;
                    case 2:
                        s2HLStepsSubShapeRef = val;
                        break;
                    case 0:
                        s0HLStepsSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLRefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VLRefFinal = val;
                        break;
                    case 2:
                        s2VLRefFinal = val;
                        break;
                    case 0:
                        s0VLRefFinal = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVLRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVLRef = val;
                        break;
                    case 2:
                        s2MinVLRef = val;
                        break;
                    case 0:
                        s0MinVLRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVLSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVLSubShapeRef = val;
                        break;
                    case 2:
                        s2MinVLSubShapeRef = val;
                        break;
                    case 0:
                        s0MinVLSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VLIncRef = val;
                        break;
                    case 2:
                        s2VLIncRef = val;
                        break;
                    case 0:
                        s0VLIncRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VLIncSubShapeRef = val;
                        break;
                    case 2:
                        s2VLIncSubShapeRef = val;
                        break;
                    case 0:
                        s0VLIncSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VLStepsRef = val;
                        break;
                    case 2:
                        s2VLStepsRef = val;
                        break;
                    case 0:
                        s0VLStepsRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VLStepsSubShapeRef = val;
                        break;
                    case 2:
                        s2VLStepsSubShapeRef = val;
                        break;
                    case 0:
                        s0VLStepsSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HORefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HORefFinal = val;
                        break;
                    case 2:
                        s2HORefFinal = val;
                        break;
                    case 0:
                        s0HORefFinal = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHORef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHORef = val;
                        break;
                    case 2:
                        s2MinHORef = val;
                        break;
                    case 0:
                        s0MinHORef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHOSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHOSubShapeRef = val;
                        break;
                    case 2:
                        s2MinHOSubShapeRef = val;
                        break;
                    case 0:
                        s0MinHOSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HOIncRef = val;
                        break;
                    case 2:
                        s2HOIncRef = val;
                        break;
                    case 0:
                        s0HOIncRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HOIncSubShapeRef = val;
                        break;
                    case 2:
                        s2HOIncSubShapeRef = val;
                        break;
                    case 0:
                        s0HOIncSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HOStepsRef = val;
                        break;
                    case 2:
                        s2HOStepsRef = val;
                        break;
                    case 0:
                        s0HOStepsRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HOStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HOStepsSubShapeRef = val;
                        break;
                    case 2:
                        s2HOStepsSubShapeRef = val;
                        break;
                    case 0:
                        s0HOStepsSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VORefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VORefFinal = val;
                        break;
                    case 2:
                        s2VORefFinal = val;
                        break;
                    case 0:
                        s0VORefFinal = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVORef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVORef = val;
                        break;
                    case 2:
                        s2MinVORef = val;
                        break;
                    case 0:
                        s0MinVORef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVOSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVOSubShapeRef = val;
                        break;
                    case 2:
                        s2MinVOSubShapeRef = val;
                        break;
                    case 0:
                        s0MinVOSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOIncRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VOIncRef = val;
                        break;
                    case 2:
                        s2VOIncRef = val;
                        break;
                    case 0:
                        s0VOIncRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOIncSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VOIncSubShapeRef = val;
                        break;
                    case 2:
                        s2VOIncSubShapeRef = val;
                        break;
                    case 0:
                        s0VOIncSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOStepsRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VOStepsRef = val;
                        break;
                    case 2:
                        s2VOStepsRef = val;
                        break;
                    case 0:
                        s0VOStepsRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VOStepsSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VOStepsSubShapeRef = val;
                        break;
                    case 2:
                        s2VOStepsSubShapeRef = val;
                        break;
                    case 0:
                        s0VOStepsSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.tipRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1TipRef = val;
                        break;
                    case 2:
                        s2TipRef = val;
                        break;
                    case 0:
                        s0TipRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.tipSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1TipSubShapeRef = val;
                        break;
                    case 2:
                        s2TipSubShapeRef = val;
                        break;
                    case 0:
                        s0TipSubShapeRef = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HorTipSteps:
                HorTipSteps = val;
                break;
            case properties_i.VerTipSteps:
                VerTipSteps = val;
                break;
            case properties_i.MinHTRef:
                MinHTRef = val;
                break;
            case properties_i.HTIncRef:
                hTIncRef = val;
                break;
            case properties_i.HTStepsRef:
                hTStepsRef = val;
                break;
            case properties_i.MinVTRef:
                MinVTRef = val;
                break;
            case properties_i.VTIncRef:
                vTIncRef = val;
                break;
            case properties_i.VTStepsRef:
                vTStepsRef = val;
                break;
            case properties_i.HTRefFinal:
                HorTipRefFinal = val;
                break;
            case properties_i.VTRefFinal:
                VerTipRefFinal = val;
                break;
        }

        if (!showError)
        {
            return;
        }

        Error.ErrorReporter.showMessage_OK(p + ",subshapeRef:" + _subShapeRef, "Coder error - setInt");
        throw new Exception();
    }

    public void defaultInt(properties_i p, int _subShapeRef = -1, int listIndex = 0)
    {
        pDefaultInt(p, _subShapeRef);
    }

    private void pDefaultInt(properties_i p, int _subShapeRef = -1)
    {
        bool showError = false;
            
        switch (p)
        {
            case properties_i.posIndex:
                defaultInt(ShapeSettings.properties_i.posInSubShapeIndex);
                break;
            case properties_i.shape0Tip:
                defaultInt(ShapeSettings.properties_i.subShapeTipLocIndex);
                break;
            case properties_i.shape1Tip:
                defaultInt(ShapeSettings.properties_i.subShape2TipLocIndex);
                break;
            case properties_i.shape2Tip:
                defaultInt(ShapeSettings.properties_i.subShape3TipLocIndex);
                break;
            case properties_i.shapeIndex:
                defaultInt(ShapeSettings.properties_i.shapeIndex);
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
            case properties_i.horLengthSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HorLengthSteps = defaultSteps;
                        break;
                    case 2:
                        s2HorLengthSteps = defaultSteps;
                        break;
                    case 0:
                        s0HorLengthSteps = defaultSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.horOffsetSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HorOffsetSteps = defaultSteps;
                        break;
                    case 2:
                        s2HorOffsetSteps = defaultSteps;
                        break;
                    case 0:
                        s0HorOffsetSteps = defaultSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.verLengthSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VerLengthSteps = defaultSteps;
                        break;
                    case 2:
                        s2VerLengthSteps = defaultSteps;
                        break;
                    case 0:
                        s0VerLengthSteps = defaultSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.verOffsetSteps:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VerOffsetSteps = defaultSteps;
                        break;
                    case 2:
                        s2VerOffsetSteps = defaultSteps;
                        break;
                    case 0:
                        s0VerOffsetSteps = defaultSteps;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.xPosSubShapeRef:
                xPosSubShapeRef = default_subShapeRef;
                break;
            case properties_i.yPosSubShapeRef:
                yPosSubShapeRef = default_subShapeRef;
                break;
            case properties_i.xPosSubShapeRefPos:
                xPosSubShapeRefPos = (int)subShapeHorLocs.L;
                break;
            case properties_i.yPosSubShapeRefPos:
                yPosSubShapeRefPos = (int)subShapeVerLocs.B;
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
                arrayXCount = defaultArrayCount;
                break;
            case properties_i.arrayMinXCount:
                arrayMinXCount = defaultArrayCount;
                break;
            case properties_i.arrayXInc:
                arrayXInc = 0;
                break;
            case properties_i.arrayXSteps:
                arrayXSteps = defaultSteps;
                break;
            case properties_i.arrayXSpaceSteps:
                arrayXSpaceSteps = defaultSteps;
                break;
            case properties_i.arrayYCount:
                arrayYCount = defaultArrayCount;
                break;
            case properties_i.arrayMinYCount:
                arrayMinYCount = defaultArrayCount;
                break;
            case properties_i.arrayYInc:
                arrayYInc = 0;
                break;
            case properties_i.arrayYSteps:
                arrayYSteps = defaultSteps;
                break;
            case properties_i.arrayYSpaceSteps:
                arrayYSpaceSteps = defaultSteps;
                break;
            case properties_i.arrayRotationSteps:
                arrayRotationSteps = defaultSteps;
                break;
            case properties_i.arrayRotationRef:
                arrayRotRef = default_rotRefIndex;
                break;
            case properties_i.boundingLeftSteps:
                v_bbLSteps = defaultSteps;
                break;
            case properties_i.boundingRightSteps:
                v_bbRSteps = defaultSteps;
                break;
            case properties_i.boundingBottomSteps:
                v_bbBSteps = defaultSteps;
                break;
            case properties_i.boundingTopSteps:
                v_bbTSteps = defaultSteps;
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
            case properties_i.linkedElementIndex:
                linkedElementIndex = -1;
                break;
            case properties_i.layoutDataType:
                layoutLayer = defaultLayoutLDValue;
                break;
            case properties_i.layoutLayer:
                layoutDataType = defaultLayoutLDValue;
                break;
            case properties_i.HLRefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HLRefFinal = 0;
                        break;
                    case 2:
                        s2HLRefFinal = 0;
                        break;
                    case 0:
                        s0HLRefFinal = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHLRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHLRef = 0;
                        break;
                    case 2:
                        s2MinHLRef = 0;
                        break;
                    case 0:
                        s0MinHLRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHLSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHLSubShapeRef = 0;
                        break;
                    case 2:
                        s2MinHLSubShapeRef = 0;
                        break;
                    case 0:
                        s0MinHLSubShapeRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HORefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1HORefFinal = 0;
                        break;
                    case 2:
                        s2HORefFinal = 0;
                        break;
                    case 0:
                        s0HORefFinal = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHORef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHORef = 0;
                        break;
                    case 2:
                        s2MinHORef = 0;
                        break;
                    case 0:
                        s0MinHORef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinHOSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinHOSubShapeRef = 0;
                        break;
                    case 2:
                        s2MinHOSubShapeRef = 0;
                        break;
                    case 0:
                        s0MinHOSubShapeRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VLRefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VLRefFinal = 0;
                        break;
                    case 2:
                        s2VLRefFinal = 0;
                        break;
                    case 0:
                        s0VLRefFinal = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVLRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVLRef = 0;
                        break;
                    case 2:
                        s2MinVLRef = 0;
                        break;
                    case 0:
                        s0MinVLRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVLSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVLSubShapeRef = 0;
                        break;
                    case 2:
                        s2MinVLSubShapeRef = 0;
                        break;
                    case 0:
                        s0MinVLSubShapeRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.VORefFinal:
                switch (_subShapeRef)
                {
                    case 1:
                        s1VORefFinal = 0;
                        break;
                    case 2:
                        s2VORefFinal = 0;
                        break;
                    case 0:
                        s0VORefFinal = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVORef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVORef = 0;
                        break;
                    case 2:
                        s2MinVORef = 0;
                        break;
                    case 0:
                        s0MinVORef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.MinVOSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1MinVOSubShapeRef = 0;
                        break;
                    case 2:
                        s2MinVOSubShapeRef = 0;
                        break;
                    case 0:
                        s0MinVOSubShapeRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.tipRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1TipRef = 0;
                        break;
                    case 2:
                        s2TipRef = 0;
                        break;
                    case 0:
                        s0TipRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.tipSubShapeRef:
                switch (_subShapeRef)
                {
                    case 1:
                        s1TipSubShapeRef = 0;
                        break;
                    case 2:
                        s2TipSubShapeRef = 0;
                        break;
                    case 0:
                        s0TipSubShapeRef = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_i.HorTipSteps:
                HorTipSteps = 1;
                break;
            case properties_i.VerTipSteps:
                VerTipSteps = 1;
                break;
            case properties_i.MinHTRef:
                MinHTRef = 0;
                break;
            case properties_i.HTIncRef:
                hTIncRef = 0;
                break;
            case properties_i.HTStepsRef:
                hTStepsRef = 0;
                break;
            case properties_i.MinVTRef:
                MinVTRef = 0;
                break;
            case properties_i.VTIncRef:
                vTIncRef = 0;
                break;
            case properties_i.VTStepsRef:
                vTStepsRef = 0;
                break;
            case properties_i.HTRefFinal:
                HorTipRefFinal = 0;
                break;
            case properties_i.VTRefFinal:
                VerTipRefFinal = 0;
                break;
        }

        if (!showError)
        {
            return;
        }

        Error.ErrorReporter.showMessage_OK(p + ",subshapeRef:" + _subShapeRef, "Coder error - defaultInt");
        throw new Exception();
    }

    // Actual values for this element. Note that these are requested, but not guaranteed due to shape clamping.
    private decimal x;
    private decimal y;
    private decimal rotation;
    private decimal arrayRotation;

    private decimal boundingLeft;
    private decimal boundingRight;
    private decimal boundingTop;
    private decimal boundingBottom;

    // Min/max values, and step
    private decimal subShapeMinHorLength;
    private decimal subShapeHorLengthInc;
    private decimal subShapeMinHorOffset;
    private decimal subShapeHorOffsetInc;
    private decimal subShapeMinVerLength;
    private decimal subShapeVerLengthInc;
    private decimal subShapeMinVerOffset;
    private decimal subShapeVerOffsetInc;
    private decimal subShape2MinHorLength;
    private decimal subShape2HorLengthInc;
    private decimal subShape2MinHorOffset;
    private decimal subShape2HorOffsetInc;
    private decimal subShape2MinVerLength;
    private decimal subShape2VerLengthInc;
    private decimal subShape2MinVerOffset;
    private decimal subShape2VerOffsetInc;
    private decimal subShape3MinHorLength;
    private decimal subShape3HorLengthInc;
    private decimal subShape3MinHorOffset;
    private decimal subShape3HorOffsetInc;
    private decimal subShape3MinVerLength;
    private decimal subShape3VerLengthInc;
    private decimal subShape3MinVerOffset;
    private decimal subShape3VerOffsetInc;
    private decimal minXPos;
    private decimal xPosInc;
    private decimal minYPos;
    private decimal yPosInc;
    private decimal minRotation;
    private decimal rotationInc;
    private decimal minArrayRotation;
    private decimal arrayRotationInc;

    private decimal boundingLeftInc;
    private decimal boundingRightInc;
    private decimal boundingTopInc;
    private decimal boundingBottomInc;

    private decimal arrayXSpace;
    private decimal arrayYSpace;
    private decimal arrayMinXSpace;
    private decimal arrayMinYSpace;
    private decimal arrayXSpaceInc;
    private decimal arrayYSpaceInc;

    private decimal minHorTipLength;
    private decimal horTipLengthInc;
    private decimal minVerTipLength;
    private decimal verTipLengthInc;
    
    private List<decimal> externalGeoCoordX;
    private List<decimal> externalGeoCoordY;

    public PathsD decomposedPolys; // used to push back decomposed polygons to the stitcher to populate other elements.

    public PathsD nonOrthoGeometry;

    public new enum properties_decimal
    {
        minHorLength, minVerLength, minHorOffset, minVerOffset,
        horLengthInc, verLengthInc, horOffsetInc, verOffsetInc,
        gHorOffset, gVerOffset,
        minXPos, xPosInc, minYPos, yPosInc,
        xPos, yPos,
        horLength, verLength, horOffset, verOffset,
        minRotation, rotationInc, rotation,
        boundingLeft, boundingRight, boundingTop, boundingBottom,
        boundingLeftInc, boundingRightInc, boundingTopInc, boundingBottomInc,
        arrayXSpace, arrayYSpace, arrayMinXSpace, arrayXSpaceInc, arrayMinYSpace, arrayYSpaceInc,
        minArrayRotation, arrayRotationInc, arrayRotation,
        externalGeoCoordX, externalGeoCoordY,
        minHorTipLength, horTipLengthInc,
        minVerTipLength, verTipLengthInc,
    }

    public decimal getDecimal(properties_decimal p, int _subShapeRef = -1)
    {
        return pGetDecimal(p, _subShapeRef);
    }

    private decimal pGetDecimal(properties_decimal p, int _subShapeRef = -1)
    {
        bool showError = false;
        decimal ret = 0m;
        switch (p)
        {
            case properties_decimal.minHorLength:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2MinHorLength;
                        break;
                    case 2:
                        ret = subShape3MinHorLength;
                        break;
                    case 0:
                        ret = subShapeMinHorLength;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minVerLength:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2MinVerLength;
                        break;
                    case 2:
                        ret = subShape3MinVerLength;
                        break;
                    case 0:
                        ret = subShapeMinVerLength;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minHorOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2MinHorOffset;
                        break;
                    case 2:
                        ret = subShape3MinHorOffset;
                        break;
                    case 0:
                        ret = subShapeMinHorOffset;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minVerOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2MinVerOffset;
                        break;
                    case 2:
                        ret = subShape3MinVerOffset;
                        break;
                    case 0:
                        ret = subShapeMinVerOffset;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.xPos:
                ret = x;
                switch (_subShapeRef)
                {
                    case 1:
                        ret += pGetDecimal(properties_decimal.horOffset, 1);
                        break;
                    case 2:
                        ret += pGetDecimal(properties_decimal.horOffset, 2);
                        break;
                }

                break;
            case properties_decimal.yPos:
                ret = y;
                switch (_subShapeRef)
                {
                    case 1:
                        ret += pGetDecimal(properties_decimal.verOffset, 1);
                        break;
                    case 2:
                        ret += pGetDecimal(properties_decimal.verLength, 0);
                        ret -= pGetDecimal(properties_decimal.verOffset, 2);
                        ret -= pGetDecimal(properties_decimal.verLength, 2);
                        break;
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
            case properties_decimal.horLengthInc:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2HorLengthInc;
                        break;
                    case 2:
                        ret = subShape3HorLengthInc;
                        break;
                    case 0:
                        ret = subShapeHorLengthInc;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.horOffsetInc:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2HorOffsetInc;
                        break;
                    case 2:
                        ret = subShape3HorOffsetInc;
                        break;
                    case 0:
                        ret = subShapeHorOffsetInc;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verLengthInc:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2VerLengthInc;
                        break;
                    case 2:
                        ret = subShape3VerLengthInc;
                        break;
                    case 0:
                        ret = subShapeVerLengthInc;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verOffsetInc:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2VerOffsetInc;
                        break;
                    case 2:
                        ret = subShape3VerOffsetInc;
                        break;
                    case 0:
                        ret = subShapeVerOffsetInc;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;

            case properties_decimal.horLength:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2HorLength;
                        break;
                    case 2:
                        ret = subShape3HorLength;
                        break;
                    case 0:
                        ret = subShapeHorLength;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verLength:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2VerLength;
                        break;
                    case 2:
                        ret = subShape3VerLength;
                        break;
                    case 0:
                        ret = subShapeVerLength;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;

            case properties_decimal.horOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2HorOffset;
                        break;
                    case 2:
                        ret = subShape3HorOffset;
                        break;
                    case 0:
                        ret = subShapeHorOffset;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        ret = subShape2VerOffset;
                        break;
                    case 2:
                        ret = subShape3VerOffset;
                        break;
                    case 0:
                        ret = subShapeVerOffset;
                        break;
                    default:
                        showError = true;
                        break;
                }
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
            case properties_decimal.arrayMinXSpace:
                ret = arrayMinXSpace;
                break;
            case properties_decimal.arrayMinYSpace:
                ret = arrayMinYSpace;
                break;
            case properties_decimal.arrayXSpaceInc:
                ret = arrayXSpaceInc;
                break;
            case properties_decimal.arrayYSpaceInc:
                ret = arrayYSpaceInc;
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
            case properties_decimal.externalGeoCoordX:
                ret = externalGeoCoordX[_subShapeRef];
                break;
            case properties_decimal.externalGeoCoordY:
                ret = externalGeoCoordY[_subShapeRef];
                break;
            case properties_decimal.minHorTipLength:
                ret = minHorTipLength;
                break;
            case properties_decimal.minVerTipLength:
                ret = minVerTipLength;
                break;
            case properties_decimal.horTipLengthInc:
                ret = horTipLengthInc;
                break;
            case properties_decimal.verTipLengthInc:
                ret = verTipLengthInc;
                break;
        }

        if (!showError)
        {
            return ret;
        }

        Error.ErrorReporter.showMessage_OK(p + ",subshapeRef:" + _subShapeRef, "Coder error - getDecimal");
        throw new Exception();

    }

    public void setDecimal(properties_decimal p, decimal val, int _subShapeRef = -1, int listIndex = 0)
    {
        pSetDecimal(p, val, _subShapeRef, listIndex);
    }

    private void pSetDecimal(properties_decimal p, decimal val, int _subShapeRef = -1, int listIndex = 0)
    {
        bool showError = false;
        switch (p)
        {
            case properties_decimal.minHorLength:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinHorLength = val;
                        break;
                    case 2:
                        subShape3MinHorLength = val;
                        break;
                    case 0:
                        subShapeMinHorLength = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minVerLength:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinVerLength = val;
                        break;
                    case 2:
                        subShape3MinVerLength = val;
                        break;
                    case 0:
                        subShapeMinVerLength = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minHorOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinHorOffset = val;
                        break;
                    case 2:
                        subShape3MinHorOffset = val;
                        break;
                    case 0:
                        subShapeMinHorOffset = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minVerOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinVerOffset = val;
                        break;
                    case 2:
                        subShape3MinVerOffset = val;
                        break;
                    case 0:
                        subShapeMinVerOffset = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
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
            case properties_decimal.horLengthInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2HorLengthInc = val;
                        break;
                    case 2:
                        subShape3HorLengthInc = val;
                        break;
                    case 0:
                        subShapeHorLengthInc = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.horOffsetInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2HorOffsetInc = val;
                        break;
                    case 2:
                        subShape3HorOffsetInc = val;
                        break;
                    case 0:
                        subShapeHorOffsetInc = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verLengthInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2VerLengthInc = val;
                        break;
                    case 2:
                        subShape3VerLengthInc = val;
                        break;
                    case 0:
                        subShapeVerLengthInc = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verOffsetInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2VerOffsetInc = val;
                        break;
                    case 2:
                        subShape3VerOffsetInc = val;
                        break;
                    case 0:
                        subShapeVerOffsetInc = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;

            case properties_decimal.horLength:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2HorLength = val;
                        break;
                    case 2:
                        subShape3HorLength = val;
                        break;
                    case 0:
                        subShapeHorLength = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verLength:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2VerLength = val;
                        break;
                    case 2:
                        subShape3VerLength = val;
                        break;
                    case 0:
                        subShapeVerLength = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.horOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2HorOffset = val;
                        break;
                    case 2:
                        subShape3HorOffset = val;
                        break;
                    case 0:
                        subShapeHorOffset = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2VerOffset = val;
                        break;
                    case 2:
                        subShape3VerOffset = val;
                        break;
                    case 0:
                        subShapeVerOffset = val;
                        break;
                    default:
                        showError = true;
                        break;
                }
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
            case properties_decimal.arrayMinXSpace:
                arrayMinXSpace = val;
                break;
            case properties_decimal.arrayXSpaceInc:
                arrayXSpaceInc = val;
                break;
            case properties_decimal.arrayMinYSpace:
                arrayMinYSpace = val;
                break;
            case properties_decimal.arrayYSpaceInc:
                arrayYSpaceInc = val;
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

            case properties_decimal.externalGeoCoordX:
                externalGeoCoordX[listIndex] = val;
                break;

            case properties_decimal.externalGeoCoordY:
                externalGeoCoordY[listIndex] = val;
                break;

            case properties_decimal.minHorTipLength:
                minHorTipLength = val;
                break;
            case properties_decimal.minVerTipLength:
                minVerTipLength = val;
                break;
            case properties_decimal.horTipLengthInc:
                horTipLengthInc = val;
                break;
            case properties_decimal.verTipLengthInc:
                verTipLengthInc = val;
                break;

        }

        if (!showError)
        {
            return;
        }

        Error.ErrorReporter.showMessage_OK(p + ",subshapeRef:" + _subShapeRef, "Coder error - setDecimal");
        throw new Exception();
    }

    public void defaultDecimal(properties_decimal p, int _subShapeRef = -1, int listIndex = 0)
    {
        pDefaultDecimal(p, _subShapeRef, listIndex);
    }

    private void pDefaultDecimal(properties_decimal p, int _subShapeRef = -1, int listIndex = 0)
    {
        bool showError = false;
        switch (p)
        {
            case properties_decimal.minHorLength:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinHorLength = default_subShapeHorLength;
                        break;
                    case 2:
                        subShape3MinHorLength = default_subShapeHorLength;
                        break;
                    case 0:
                        subShapeMinHorLength = default_subShapeHorLength;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minHorOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinHorOffset = default_subShapeHorOffset;
                        break;
                    case 2:
                        subShape3MinHorOffset = default_subShapeHorOffset;
                        break;
                    case 0:
                        subShapeMinHorOffset = default_subShapeHorOffset;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minVerLength:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinVerLength = default_subShapeVerLength;
                        break;
                    case 2:
                        subShape3MinVerLength = default_subShapeVerLength;
                        break;
                    case 0:
                        subShapeMinVerLength = default_subShapeVerLength;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.minVerOffset:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2MinVerOffset = default_subShapeVerOffset;
                        break;
                    case 2:
                        subShape3MinVerOffset = default_subShapeVerOffset;
                        break;
                    case 0:
                        subShapeMinVerOffset = default_subShapeVerOffset;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.xPos:
                x = 0;
                break;
            case properties_decimal.yPos:
                y = 0;
                break;
            case properties_decimal.horLengthInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2HorLengthInc = 0;
                        break;
                    case 2:
                        subShape3HorLengthInc = 0;
                        break;
                    case 0:
                        subShapeHorLengthInc = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.horOffsetInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2HorOffsetInc = 0;
                        break;
                    case 2:
                        subShape3HorOffsetInc = 0;
                        break;
                    case 0:
                        subShapeHorOffsetInc = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verLengthInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2VerLengthInc = 0;
                        break;
                    case 2:
                        subShape3VerLengthInc = 0;
                        break;
                    case 0:
                        subShapeVerLengthInc = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
                break;
            case properties_decimal.verOffsetInc:
                switch (_subShapeRef)
                {
                    case 1:
                        subShape2VerOffsetInc = 0;
                        break;
                    case 2:
                        subShape3VerOffsetInc = 0;
                        break;
                    case 0:
                        subShapeVerOffsetInc = 0;
                        break;
                    default:
                        showError = true;
                        break;
                }
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
            case properties_decimal.arrayMinXSpace:
                arrayMinXSpace = 0;
                break;
            case properties_decimal.arrayXSpaceInc:
                arrayXSpaceInc = 0;
                break;
            case properties_decimal.arrayMinYSpace:
                arrayMinYSpace = 0;
                break;
            case properties_decimal.arrayYSpaceInc:
                arrayYSpaceInc = 0;
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

            case properties_decimal.externalGeoCoordX:
                externalGeoCoordX[listIndex] = 0;
                break;
            case properties_decimal.externalGeoCoordY:
                externalGeoCoordY[listIndex] = 0;
                break;
            
            case properties_decimal.minHorTipLength:
                minHorTipLength = 0;
                break;
            case properties_decimal.minVerTipLength:
                minVerTipLength = 0;
                break;
            case properties_decimal.horTipLengthInc:
                horTipLengthInc = 0;
                break;
            case properties_decimal.verTipLengthInc:
                verTipLengthInc = 0;
                break;
        }

        if (!showError)
        {
            return;
        }

        Error.ErrorReporter.showMessage_OK(p + ",subshapeRef:" + _subShapeRef, "Coder error - defaultDecimal");
        throw new Exception();
    }

    public bool equivalence(PatternElement pe)
    {
        return pEquivalence(pe);
    }

    private bool pEquivalence(PatternElement pe)
    {
        bool ret = getInt(properties_i.shapeIndex) == pe.getInt(properties_i.shapeIndex);

        ret = ret && subShapeHorLength == pe.subShapeHorLength;
        ret = ret && subShapeHorOffset == pe.subShapeHorOffset;
        ret = ret && subShapeVerLength == pe.subShapeVerLength;
        ret = ret && subShapeVerOffset == pe.subShapeVerOffset;

        ret = ret && subShape2HorLength == pe.subShape2HorLength;
        ret = ret && subShape2HorOffset == pe.subShape2HorOffset;
        ret = ret && subShape2VerLength == pe.subShape2VerLength;
        ret = ret && subShape2VerOffset == pe.subShape2VerOffset;

        ret = ret && subShape3HorLength == pe.subShape3HorLength;
        ret = ret && subShape3HorOffset == pe.subShape3HorOffset;
        ret = ret && subShape3VerLength == pe.subShape3VerLength;
        ret = ret && subShape3VerOffset == pe.subShape3VerOffset;

        ret = ret && horTipBias == pe.horTipBias;
        ret = ret && verTipBias == pe.verTipBias;

        ret = ret && x == pe.x;
        ret = ret && y == pe.y;

        decimal rotM360 = rotation % 360;
        decimal peRotM360 = pe.rotation % 360;

        if (Math.Abs(rotM360 - peRotM360) == 360)
        {
            peRotM360 = rotM360;
        }

        ret = ret && rotM360 == peRotM360;

        ret = ret && boundingBottom == pe.boundingBottom;
        ret = ret && boundingTop == pe.boundingTop;
        ret = ret && boundingLeft == pe.boundingLeft;
        ret = ret && boundingRight == pe.boundingRight;

        ret = ret && arrayRotation % 360 == pe.arrayRotation % 360;

        ret = ret && arrayXCount == pe.arrayXCount;
        ret = ret && arrayYCount == pe.arrayYCount;
        ret = ret && arrayXSpace == pe.arrayXSpace;
        ret = ret && arrayYSpace == pe.arrayYSpace;

        ret = ret && refBoundsAfterRotation == pe.refBoundsAfterRotation;

        ret = ret && refArrayBoundsAfterRotation == pe.refArrayBoundsAfterRotation;

        // Check our external geometry only if relevant
        if (getInt(properties_i.shapeIndex) != (int) typeShapes_mode1.complex)
        {
            return ret;
        }

        ret = ret && externalGeoCoordX.Count == pe.externalGeoCoordX.Count;
        if (!ret)
        {
            return false;
        }

        // Could probably use a hash here, but this should be cheap enough for now.
        if (externalGeoCoordX.Where((t, i) => t != pe.externalGeoCoordX[i] || externalGeoCoordY[i] != pe.externalGeoCoordY[i]).Any())
        {
            ret = false;
        }

        return ret;
    }

    private int variantCounter;

    private int v_xSteps, v_ySteps;
    private int v_s0HLSteps, v_s0VLSteps, v_s0HOSteps, v_s0VOSteps;
    private int v_s1HLSteps, v_s1VLSteps, v_s1HOSteps, v_s1VOSteps;
    private int v_s2HLSteps, v_s2VLSteps, v_s2HOSteps, v_s2VOSteps;
    private int v_rSteps, v_arrayRSteps, v_array_X_Steps, v_array_Y_Steps, v_array_XSpace_Steps, v_array_YSpace_Steps;
    private int v_bbLSteps, v_bbRSteps, v_bbBSteps, v_bbTSteps;

    private int v_s0hlIndex, v_s0hoIndex, v_s0vlIndex, v_s0voIndex;
    private int v_s1hlIndex, v_s1hoIndex, v_s1vlIndex, v_s1voIndex;
    private int v_s2hlIndex, v_s2hoIndex, v_s2vlIndex, v_s2voIndex;

    private int v_xIndex, v_yIndex, v_rotIndex;

    private int v_arrayXIndex, v_arrayYIndex, v_arrayXSpaceIndex, v_arrayYSpaceIndex, v_arrayRotIndex;

    private int v_bbLIndex, v_bbRIndex, v_bbBIndex, v_bbTIndex;

    private int v_hTSteps, v_vTSteps, v_hTIndex, v_vTIndex;

    private int pCalcMaxVariants()
    {
        // Override number of steps in case the step itself is zero
        v_xSteps = xPosSteps;
        if (xPosInc == 0)
        {
            v_xSteps = 1;
        }

        v_ySteps = yPosSteps;
        if (yPosInc == 0)
        {
            v_ySteps = 1;
        }

        v_rSteps = rotationSteps;
        if (rotationInc == 0)
        {
            v_rSteps = 1;
        }

        if (pIsXArray() || pIsYArray() || relativeArray == 1)
        {
            v_arrayRSteps = arrayRotationSteps;
            v_array_X_Steps = arrayXSteps;
            v_array_Y_Steps = arrayYSteps;
            v_array_XSpace_Steps = arrayXSpaceSteps;
            v_array_YSpace_Steps = arrayYSpaceSteps;
        }
        else
        {
            v_arrayRSteps = 1;
            v_array_X_Steps = 1;
            v_array_Y_Steps = 1;
            v_array_XSpace_Steps = 1;
            v_array_YSpace_Steps = 1;
        }

        int limit = v_xSteps * v_ySteps * v_rSteps * v_arrayRSteps * v_array_X_Steps * v_array_Y_Steps * v_array_XSpace_Steps * v_array_YSpace_Steps;

        switch (getInt(properties_i.shapeIndex))
        {
            case (int)CentralProperties.shapeNames.complex:
                return pCalcMaxVariants_external(limit);
            case (int)CentralProperties.shapeNames.bounding:
                return pCalcMaxVariants_bounding(limit);
            default:
                return pCalcMaxVariants_regular(limit);
        }
    }

    private static int pCalcMaxVariants_external(int limit)
    {
        // Need to calculate our variants based on the edge steps for the edges in the pattern.
        /*
        int extSteps = 1;
        for (int i = 0; i < externalGeoEdgeSteps.Count; i++)
        {
            extSteps *= externalGeoEdgeSteps[i];
        }
        */
        return limit; // * extSteps;
    }

    private int pCalcMaxVariants_bounding(int limit)
    {
        v_bbLSteps = boundingLeftSteps;
        if (v_bbLSteps == 0)
        {
            v_bbLSteps = 1;
        }

        v_bbRSteps = boundingRightSteps;
        if (v_bbRSteps == 0)
        {
            v_bbRSteps = 1;
        }

        v_bbTSteps = boundingTopSteps;
        if (v_bbTSteps == 0)
        {
            v_bbTSteps = 1;
        }

        v_bbBSteps = boundingBottomSteps;
        if (v_bbBSteps == 0)
        {
            v_bbBSteps = 1;
        }

        limit *= v_bbLSteps * v_bbRSteps * v_bbBSteps * v_bbTSteps;

        return limit;
    }

    private int pCalcMaxVariants_regular(int limit)
    {
        v_s0HLSteps = s0HorLengthSteps;
        if (subShapeHorLengthInc == 0)
        {
            v_s0HLSteps = 1;
        }

        v_s0HOSteps = s0HorOffsetSteps;
        if (subShapeHorOffsetInc == 0)
        {
            v_s0HOSteps = 1;
        }

        v_s0VLSteps = s0VerLengthSteps;
        if (subShapeVerLengthInc == 0)
        {
            v_s0VLSteps = 1;
        }

        v_s0VOSteps = s0VerOffsetSteps;
        if (subShapeVerOffsetInc == 0)
        {
            v_s0VOSteps = 1;
        }

        v_s1HLSteps = s1HorLengthSteps;
        if (subShape2HorLengthInc == 0)
        {
            v_s1HLSteps = 1;
        }

        v_s1HOSteps = s1HorOffsetSteps;
        if (subShape2HorOffsetInc == 0)
        {
            v_s1HOSteps = 1;
        }

        v_s1VLSteps = s1VerLengthSteps;
        if (subShape2VerLengthInc == 0)
        {
            v_s1VLSteps = 1;
        }

        v_s1VOSteps = s1VerOffsetSteps;
        if (subShape2VerOffsetInc == 0)
        {
            v_s1VOSteps = 1;
        }

        v_s2HLSteps = s2HorLengthSteps;
        if (subShape3HorLengthInc == 0)
        {
            v_s2HLSteps = 1;
        }

        v_s2HOSteps = s2HorOffsetSteps;
        if (subShape3HorOffsetInc == 0)
        {
            v_s2HOSteps = 1;
        }

        v_s2VLSteps = s2VerLengthSteps;
        if (subShape3VerLengthInc == 0)
        {
            v_s2VLSteps = 1;
        }

        v_s2VOSteps = s2VerOffsetSteps;
        if (subShape3VerOffsetInc == 0)
        {
            v_s2VOSteps = 1;
        }

        limit *= v_s0HLSteps * v_s0HOSteps * v_s0VLSteps * v_s0VOSteps;

        v_hTSteps = HorTipSteps;
        v_vTSteps = VerTipSteps;
        
        // So, we could have some steps, but the tip location is not set, making the setting irrelevant.....
        int numberOfSubShapes = getSubShapeCount();
        bool doHaveHTips = false;
        bool doHaveVTips = false;
        for (int i = 0; i < numberOfSubShapes; i++)
        {
            // However, we need to review to ensure that we have H and/or V tips from the location setting....
            int tipCheck = getInt(ShapeSettings.properties_i.subShapeTipLocIndex, i);
            switch (tipCheck)
            {
                case (int)tipLocations.L:
                case (int)tipLocations.R:
                case (int)tipLocations.LR:
                    doHaveHTips = true;
                    break;
                case (int)tipLocations.T:
                case (int)tipLocations.B:
                case (int)tipLocations.TB:
                    doHaveVTips = true;
                    break;
                case (int)tipLocations.TL:
                case (int)tipLocations.TR:
                case (int)tipLocations.TLR:
                case (int)tipLocations.BL:
                case (int)tipLocations.BR:
                case (int)tipLocations.BLR:
                case (int)tipLocations.all:
                    doHaveHTips = true;
                    doHaveVTips = true;
                    break;
            }
        }
        
        if ((horTipLengthInc == 0) || !doHaveHTips)
        {
            v_hTSteps = 1;
        }
        if ((verTipLengthInc == 0) || !doHaveVTips)
        {
            v_vTSteps = 1;
        }

        limit *= v_hTSteps * v_vTSteps;
        
        if (getInt(properties_i.shapeIndex) is (int) CentralProperties.shapeNames.none or (int) CentralProperties.shapeNames.rect or (int) CentralProperties.shapeNames.text)
        {
            return limit;
        }

        limit *= v_s1HLSteps * v_s1HOSteps * v_s1VLSteps * v_s1VOSteps;
        if (getInt(properties_i.shapeIndex) == (int)CentralProperties.shapeNames.Sshape)
        {
            limit *= v_s2HLSteps * v_s2HOSteps * v_s2VLSteps * v_s2VOSteps;
        }
        
        return limit;
    }

    public PatternElement getNextVariant()
    {
        return pGetNextVariant();
    }

    private PatternElement pGetNextVariant()
    {
        if (variantCounter >= pCalcMaxVariants())
        {
            variantCounter = 0;
            return null; // no available variant.
        }

        v_xIndex = variantCounter % v_xSteps;

        long fields = v_xSteps;

        v_yIndex = (int)(Math.Floor((float)variantCounter / fields) % v_ySteps);

        fields *= v_ySteps;

        PatternElement ret = new(this);

        if (getInt(properties_i.shapeIndex) != (int)typeShapes_mode1.complex)
        {
            if (getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.bounding)
            {
                v_s0hlIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s0HLSteps);

                fields *= v_s0HLSteps;

                v_s0hoIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s0HOSteps);

                fields *= v_s0HOSteps;

                v_s0vlIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s0VLSteps);

                fields *= v_s0VLSteps;

                v_s0voIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s0VOSteps);

                fields *= v_s0VOSteps;
                
                v_hTIndex = (int)(Math.Floor((float)variantCounter / fields) % HorTipSteps);

                fields *= v_hTSteps;

                v_vTIndex = (int)(Math.Floor((float)variantCounter / fields) % VerTipSteps);

                fields *= v_vTSteps;

                if (getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.none && getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.rect && getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.text)
                {
                    v_s1hlIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s1HLSteps);

                    fields *= v_s1HLSteps;

                    v_s1hoIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s1HOSteps);

                    fields *= v_s1HOSteps;

                    v_s1vlIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s1VLSteps);

                    fields *= v_s1VLSteps;

                    v_s1voIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s1VOSteps);

                    fields *= v_s1VOSteps;

                    if (getInt(properties_i.shapeIndex) == (int)CentralProperties.shapeNames.Sshape)
                    {
                        v_s2hlIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s2HLSteps);

                        fields *= v_s2HLSteps;

                        v_s2hoIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s2HOSteps);

                        fields *= v_s2HOSteps;

                        v_s2vlIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s2VLSteps);

                        fields *= v_s2VLSteps;

                        v_s2voIndex = (int)(Math.Floor((float)variantCounter / fields) % v_s2VOSteps);

                        fields *= v_s2VOSteps;
                    }
                }

                ret.subShapeHorLength = subShapeMinHorLength + v_s0hlIndex * subShapeHorLengthInc;
                ret.subShapeVerLength = subShapeMinVerLength + v_s0vlIndex * subShapeVerLengthInc;
                ret.subShapeHorOffset = subShapeMinHorOffset + v_s0hoIndex * subShapeHorOffsetInc;
                ret.subShapeVerOffset = subShapeMinVerOffset + v_s0voIndex * subShapeVerOffsetInc;

                ret.subShape2HorLength = subShape2MinHorLength + v_s1hlIndex * subShape2HorLengthInc;
                ret.subShape2VerLength = subShape2MinVerLength + v_s1vlIndex * subShape2VerLengthInc;
                ret.subShape2HorOffset = subShape2MinHorOffset + v_s1hoIndex * subShape2HorOffsetInc;
                ret.subShape2VerOffset = subShape2MinVerOffset + v_s1voIndex * subShape2VerOffsetInc;

                ret.subShape3HorLength = subShape3MinHorLength + v_s2hlIndex * subShape3HorLengthInc;
                ret.subShape3VerLength = subShape3MinVerLength + v_s2vlIndex * subShape3VerLengthInc;
                ret.subShape3HorOffset = subShape3MinHorOffset + v_s2hoIndex * subShape3HorOffsetInc;
                ret.subShape3VerOffset = subShape3MinVerOffset + v_s2voIndex * subShape3VerOffsetInc;
                
                ret.horTipBias = minHorTipLength + v_hTIndex * horTipLengthInc;
                ret.verTipBias = minVerTipLength + v_vTIndex * verTipLengthInc;
            }
            else
            {
                v_bbLIndex = (int)(Math.Floor((float)variantCounter / fields) % v_bbLSteps);

                fields *= v_bbLSteps;

                v_bbRIndex = (int)(Math.Floor((float)variantCounter / fields) % v_bbRSteps);

                fields *= v_bbRSteps;

                v_bbBIndex = (int)(Math.Floor((float)variantCounter / fields) % v_bbBSteps);

                fields *= v_bbBSteps;

                v_bbTIndex = (int)(Math.Floor((float)variantCounter / fields) % v_bbTSteps);

                fields *= v_bbTSteps;

                ret.boundingLeft = boundingLeft + v_bbLIndex * boundingLeftInc;
                ret.boundingRight = boundingRight + v_bbRIndex * boundingRightInc;
                ret.boundingBottom = boundingBottom + v_bbBIndex * boundingBottomInc;
                ret.boundingTop = boundingTop + v_bbTIndex * boundingTopInc;
            }
        }

        ret.x = minXPos + v_xIndex * xPosInc;
        ret.y = minYPos + v_yIndex * yPosInc;

        v_rotIndex = (int)(Math.Floor((float)variantCounter / fields) % v_rSteps);
        ret.rotation = minRotation + v_rotIndex * rotationInc;

        fields *= v_rSteps;

        ret.arrayRotation = 0;
        ret.arrayXCount = arrayMinXCount;
        ret.arrayYCount = arrayMinYCount;
        ret.arrayXSpace = ret.arrayMinXSpace;
        ret.arrayYSpace = ret.arrayMinYSpace;
        if (pIsXArray() || pIsYArray() || relativeArray == 1) // Would like to be able to directly identify a relative array reference, but there's no way to check this from the element level. We have to set this from elsewhere.
        {
            v_arrayRotIndex = (int)(Math.Floor((float)variantCounter / fields) % v_arrayRSteps);
            ret.arrayRotation = minArrayRotation + v_arrayRotIndex * arrayRotationInc;
            fields *= v_arrayRSteps;
            v_arrayXIndex = (int)(Math.Floor((float)variantCounter / fields) % v_array_X_Steps);
            ret.arrayXCount += v_arrayXIndex * arrayXInc;
            fields *= v_array_X_Steps;
            v_arrayYIndex = (int)(Math.Floor((float)variantCounter / fields) % v_array_Y_Steps);
            ret.arrayYCount += v_arrayYIndex * arrayYInc;
            fields *= v_array_Y_Steps;

            v_arrayXSpaceIndex = (int)(Math.Floor((float)variantCounter / fields) % v_array_XSpace_Steps);
            ret.arrayXSpace += v_arrayXSpaceIndex * arrayXSpaceInc;
            fields *= v_array_XSpace_Steps;
            v_arrayYSpaceIndex = (int)(Math.Floor((float)variantCounter / fields) % v_array_YSpace_Steps);
            ret.arrayYSpace += v_arrayYSpaceIndex * arrayYSpaceInc;
            // fields *= array_YSpace_Steps;
        }


        if (getInt(properties_i.shapeIndex) != (int)typeShapes_mode1.complex)
        {
            // We need to worry about clamping here....

            if (getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.none && getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.rect && getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.text && getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.bounding && getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.complex)
            {
                switch (getInt(properties_i.shapeIndex))
                {
                    case (int)CentralProperties.shapeNames.Lshape:
                        pLshape_limits(ref ret);
                        break;
                    case (int)CentralProperties.shapeNames.Tshape:
                        pTshape_limits(ref ret);
                        break;
                    case (int)CentralProperties.shapeNames.Ushape:
                        pUshape_limits(ref ret);
                        break;
                    case (int)CentralProperties.shapeNames.Sshape:
                        pSshape_limits(ref ret);
                        break;
                    case (int)CentralProperties.shapeNames.Xshape:
                        pXshape_limits(ref ret);
                        break;
                }
            }
        }
        variantCounter++;
            
        return ret;
    }

    private static void pSshape_limits(ref PatternElement ret)
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

    private static void pUshape_limits(ref PatternElement ret)
    {
        // Clamp dimensions
        ret.subShape2HorLength = Math.Min(ret.subShape2HorLength, ret.subShapeHorLength - 0.02m);
        ret.subShape2VerLength = Math.Min(ret.subShape2VerLength, ret.subShapeVerLength - 0.01m);

        // Pin subshape in place.
        if (ret.subShape2HorOffset < 0.01m)
        {
            ret.subShape2HorOffset = 0.01m;
        }
        else
        {
            if (ret.subShapeHorLength - (ret.subShape2HorOffset + ret.subShape2HorLength) < 0.01m)
            {
                ret.subShape2HorOffset = ret.subShapeHorLength - (ret.subShape2HorLength + 0.01m);
            }
        }

        ret.subShape3HorLength = 0;
        ret.subShape3VerLength = 0;
        ret.subShape3HorOffset = 0;
        ret.subShape3VerOffset = 0;
    }

    private static void pLshape_limits(ref PatternElement ret)
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

    private static void pTshape_limits(ref PatternElement ret)
    {
        ret.subShape2HorOffset = ret.subShapeHorLength;
        ret.subShape2VerLength = Math.Min(ret.subShape2VerLength, ret.subShapeVerLength);
        ret.subShape2VerOffset = Math.Min(ret.subShape2VerOffset, ret.subShapeVerLength - ret.subShape2VerLength);

        ret.subShape3HorLength = 0;
        ret.subShape3VerLength = 0;
        ret.subShape3HorOffset = 0;
        ret.subShape3VerOffset = 0;
    }

    private static void pXshape_limits(ref PatternElement ret)
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

    private string pGetDescription()
    {
        string description = name + "|";

        bool bounding = false;

        switch (getInt(properties_i.shapeIndex))
        {
            case (int)CentralProperties.shapeNames.none:
                description = "none";
                break;
            case (int)CentralProperties.shapeNames.rect:
                description = "rect";
                break;
            case (int)CentralProperties.shapeNames.Lshape:
                description = "L";
                break;
            case (int)CentralProperties.shapeNames.Tshape:
                description = "T";
                break;
            case (int)CentralProperties.shapeNames.Ushape:
                description = "U";
                break;
            case (int)CentralProperties.shapeNames.Xshape:
                description = "X";
                break;
            case (int)CentralProperties.shapeNames.Sshape:
                description = "S";
                break;
            case (int)CentralProperties.shapeNames.text:
                description = "text";
                break;
            case (int)CentralProperties.shapeNames.bounding:
                description = "bounding";
                bounding = true;
                break;
            case (int)CentralProperties.shapeNames.complex:
                description = "complex";
                break;
        }

        description += "|";

        if (!bounding)
        {
            description += "ss0HL:" + subShapeHorLength + "|";
            if (s0MinHLRef > 0)
            {
                description += "ss0HLRef:" + (s0MinHLRef - 1) + "|";
                description += "ss0HLSubShapeRef:" + s0MinHLSubShapeRef + "|";
                description += "ss0HLRefFinal:" + s0HLRefFinal + "|";
            }
            description += "ss0VL:" + subShapeVerLength + "|";
            if (s0MinVLRef > 0)
            {
                description += "ss0VLRef:" + (s0MinVLRef - 1) + "|";
                description += "ss0VLSubShapeRef:" + s0MinVLSubShapeRef + "|";
                description += "ss0VLRefFinal:" + s0VLRefFinal + "|";
            }
            description += "ss0HO:" + subShapeHorOffset + "|";
            if (s0MinHORef > 0)
            {
                description += "ss0HORef:" + (s0MinHORef - 1) + "|";
                description += "ss0HOSubShapeRef:" + s0MinHOSubShapeRef + "|";
                description += "ss0HORefFinal:" + s0HORefFinal + "|";
            }
            description += "ss0VO:" + subShapeVerOffset + "|";
            if (s0MinVORef > 0)
            {
                description += "ss0VORef:" + (s0MinVORef - 1) + "|";
                description += "ss0VOSubShapeRef:" + s0MinVOSubShapeRef + "|";
                description += "ss0VORefFinal:" + s0VORefFinal + "|";
            }
            description += "ss0Tip:" + getInt(properties_i.shape0Tip) + "|";
            if (s0TipRef > 0)
            {
                description += "ss0TipRef:" + (s0TipRef - 1) + "|";
                description += "ss0TipSubShapeRef:" + s0TipSubShapeRef + "|";
            }

            if (getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.none && getInt(properties_i.shapeIndex) != (int)CentralProperties.shapeNames.none)
            {
                description += "ss1HL:" + subShape2HorLength + "|";
                if (s1MinHLRef > 0)
                {
                    description += "ss1HLRef:" + (s1MinHLRef - 1) + "|";
                    description += "ss1HLSubShapeRef:" + s1MinHLSubShapeRef + "|";
                    description += "ss1HLRefFinal:" + s1HLRefFinal + "|";
                }
                description += "ss1VL:" + subShape2VerLength + "|";
                if (s1MinVLRef > 0)
                {
                    description += "ss1VLRef:" + (s1MinVLRef - 1) + "|";
                    description += "ss1VLSubShapeRef:" + s1MinVLSubShapeRef + "|";
                    description += "ss1VLRefFinal:" + s1VLRefFinal + "|";
                }
                description += "ss1HO:" + subShape2HorOffset + "|";
                if (s1MinHORef > 0)
                {
                    description += "ss1HORef:" + (s1MinHORef - 1) + "|";
                    description += "ss1HOSubShapeRef:" + s1MinHOSubShapeRef + "|";
                    description += "ss1HORefFinal:" + s1HORefFinal + "|";
                }
                description += "ss1VO:" + subShape2VerOffset + "|";
                if (s1MinVORef > 0)
                {
                    description += "ss1VORef:" + (s1MinVORef - 1) + "|";
                    description += "ss1VOSubShapeRef:" + s1MinVOSubShapeRef + "|";
                    description += "ss1VORefFinal:" + s1VORefFinal + "|";
                }
                description += "ss1Tip:" + getInt(properties_i.shape1Tip) + "|";
                if (s1TipRef > 0)
                {
                    description += "ss1TipRef:" + (s1TipRef - 1) + "|";
                    description += "ss1TipSubShapeRef:" + s1TipSubShapeRef + "|";
                }
            }
            else
            {
                description += "ss1HL:N/A|";
                description += "ss1VL:N/A|";
                description += "ss1HO:N/A|";
                description += "ss1VO:N/A|";
            }

            if (getInt(properties_i.shapeIndex) == (int)CentralProperties.shapeNames.Sshape)
            {
                description += "ss2HL:" + subShape3HorLength + "|";
                if (s2MinHLRef > 0)
                {
                    description += "ss2HLRef:" + (s2MinHLRef - 1) + "|";
                    description += "ss2HLSubShapeRef:" + s2MinHLSubShapeRef + "|";
                    description += "ss2HLRefFinal:" + s0HLRefFinal + "|";
                }
                description += "ss2VL:" + subShape3VerLength + "|";
                if (s2MinVLRef > 0)
                {
                    description += "ss2VLRef:" + (s2MinVLRef - 1) + "|";
                    description += "ss2VLSubShapeRef:" + s2MinVLSubShapeRef + "|";
                    description += "ss2VLRefFinal:" + s0VLRefFinal + "|";
                }
                description += "ss2HO:" + subShape3HorOffset + "|";
                if (s2MinHORef > 0)
                {
                    description += "ss2HORef:" + (s2MinHORef - 1) + "|";
                    description += "ss2HOSubShapeRef:" + s2MinHOSubShapeRef + "|";
                    description += "ss2HORefFinal:" + s0HORefFinal + "|";
                }
                description += "ss2VO:" + subShape3VerOffset + "|";
                if (s2MinVORef > 0)
                {
                    description += "ss2VORef:" + (s2MinVORef - 1) + "|";
                    description += "ss2VOSubShapeRef:" + s2MinVOSubShapeRef + "|";
                    description += "ss2VORefFinal:" + s0VORefFinal + "|";
                }
                description += "ss2Tip:" + getInt(properties_i.shape2Tip) + "|";
                if (s2TipRef > 0)
                {
                    description += "ss2TipRef:" + (s2TipRef - 1) + "|";
                    description += "ss2TipSubShapeRef:" + s2TipSubShapeRef + "|";
                }
            }
            else
            {
                description += "ss2HL:N/A|";
                description += "ss2VL:N/A|";
                description += "ss2HO:N/A|";
                description += "ss2VO:N/A|";
            }

            description += "HT:" + horTipBias + "|";
            if (MinHTRef > 0)
            {
                description += "HTRef:" + (MinHTRef - 1) + "|";
                description += "HTRefFinal:" + HorTipRefFinal + "|";
            }
            description += "VT:" + verTipBias + "|";
            if (MinVTRef > 0)
            {
                description += "VTRef:" + (MinVTRef - 1) + "|";
                description += "VTRefFinal:" + VerTipRefFinal + "|";
            }

            if (arrayRef != 0)
            {
                description += "arrayRef:" + arrayRef + "|";
            }
 
            if (pIsXArray() || relativeArray == 1)
            {
                description += "arrayXCount:" + arrayXCount + "|";
                description += "arrayXSpace:" + arrayXSpace + "|";
            }
            if (pIsYArray() || relativeArray == 1)
            {
                description += "arrayYCount:" + arrayYCount + "|";
                description += "arrayYSpace:" + arrayYSpace + "|";
            }
        }
        else
        {
            description += "boundingLeft:" + boundingLeft + "|";
            description += "boundingRight:" + boundingRight + "|";
            description += "boundingTop:" + boundingTop + "|";
            description += "boundingBottom:" + boundingBottom + "|";
        }

        description += "X:" + x + "|";
        if (xPosRef > 0)
        {
            description += "xPosRef:" + (xPosRef - 1) + "|";
        }
        description += "xPosSubShapeRef:" + xPosSubShapeRef + "|";
        description += "xPosSubShapeRefPos:" + xPosSubShapeRefPos + "|";

        description += "Y:" + y + "|";
        if (yPosRef > 0)
        {
            description += "yPosRef:" + (yPosRef - 1) + "|";
        }
        description += "yPosSubShapeRef:" + yPosSubShapeRef + "|";
        description += "yPosSubShapeRefPos:" + yPosSubShapeRefPos + "|";

        description += "refBoundsAfterRotation:" + refBoundsAfterRotation + "|";
        description += "refArrayBoundsAfterRotation:" + refArrayBoundsAfterRotation + "|";

        // Normalize rotation value 0-359 range
        decimal r = rotation % 360;
        if (rotation < 0)
        {
            r = (r + 360) % 360;
        }
        description += "R:" + r;

        if (!pIsXArray() && !pIsYArray() && relativeArray != 1)
        {
            return description;
        }

        description += "|";
        // Normalize rotation value 0-359 range
        decimal aR = arrayRotation % 360;
        if (arrayRotation < 0)
        {
            aR = (aR + 360) % 360;
        }
        description += "arrayR:" + aR;

        return description;
    }

    public void parsePoints(ref bool abortParse, PathD points, int layer, int datatype, bool isText, bool vertical)
    {
        pParsePoints(ref abortParse, points, layer, datatype, isText, vertical:vertical);
    }

    private void pParsePoints(ref bool abortParse, PathD points, int layer, int datatype, bool isText, bool vertical)
    {
        if (abortParse)
        {
            reset();
            return;
        }
        // Track our original layer and datatype for use on export.
        layoutLayer = layer;
        layoutDataType = datatype;

        // So this is where it gets tricky. We need to analyze our point data. We may be able to map it into a primitive type already known to us. Defer to the pattern element to sort it out.
        // This should already be the case, but let's be careful in case the caller was not kind.
        points = GeoWrangler.removeDuplicates(points);
        points = GeoWrangler.stripCollinear(points);
        points = GeoWrangler.clockwiseAndReorderXY(points);
        bool ortho = GeoWrangler.orthogonal(points, angularTolerance: 0);

        minXPos = Convert.ToDecimal(points[0].x);
        minYPos = Convert.ToDecimal(points[0].y);

        // Remove the closing point from the geometry and any duplicate terminators.
        points = GeoWrangler.stripTerminators(points, false);

        int pointsLength = points.Count;
        // This can be our first hint of what kind of geometry we're dealing with.
        bool ok = false;

        // If orthogonal, we can try and set up a primitive. Failsafe to complex if no match found.
        if (ortho)
        {
            switch (pointsLength)
            {
                case 4: // rectangle.
                    ok = isText ? pText(points) : pRectangle(points);
                    break;
                case 6: // L
                    ok = pLShape(points);
                    break;
                case 8: // T or U.
                    ok = pMightBeTorU(points);
                    break;
                case 12: // X or S.
                    ok = pMightBeXorS(points);
                    break;
            }
        }

        if (ok)
        {
            return;
        }

        if (abortParse)
        {
            reset();
            return;
        }
        points = GeoWrangler.close(points);
        
        // Run decomposition
        decomposedPolys = new ();
        nonOrthoGeometry = new ();
        if (abortParse)
        {
            reset();
            return;
        }
        if (abortParse)
        {
            reset();
            return;
        }
        
        PathsD decompOut;
        if (ortho)
        {
            // Give the keyholder a whirl: override the internal keyhole width with a value based on experimentation.
            PathD toDecomp = GeoWrangler.makeKeyHole(GeoWrangler.sliverGapRemoval(points), reverseEval:true, biDirectionalEval:false, customSizing:CentralProperties.keyhole_width)[0];
            if (abortParse)
            {
                reset();
                return;
            }
        
            PathD bounds = GeoWrangler.getBounds(toDecomp);
            if (abortParse)
            {
                reset();
                return;
            }

            PointD dist = GeoWrangler.distanceBetweenPoints_point(bounds[0], bounds[1]);
            if (abortParse)
            {
                reset();
                return;
            }
            decompOut = GeoWrangler.rectangular_decomposition(ref abortParse, toDecomp,
                maxRayLength: (long) Math.Max(Math.Abs(dist.x), Math.Abs(dist.y)), vertical: vertical);
        }
        else
        {
            decompOut = new()
            {
                points
            };
        }

        if (abortParse)
        {
            reset();
            return;
        }
        decomposedPolys = new(decompOut);
        if (abortParse)
        {
            reset();
            return;
        }
        decomposedPolys = GeoWrangler.xySequence(decomposedPolys);
        if (abortParse)
        {
            reset();
            return;
        }

        if (decomposedPolys.Count == 1)
        {
            // Using absolute positioning for non-orthogonal, so set to 0
            minXPos = 0;
            minYPos = 0;
            // No decomposition, so treat as complex.
            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.complex);
            PathD tmpNO = Helper.initedPathD(pointsLength);
            for (int p = 0; p < pointsLength; p++)
            {
                if (abortParse)
                {
                    reset();
                    return;
                }
                externalGeoCoordX.Add(Convert.ToDecimal(points[p].x));
                externalGeoCoordY.Add(Convert.ToDecimal(points[p].y));
                tmpNO[p] = new (points[p].x, points[p].y);
            }
            tmpNO = GeoWrangler.close(tmpNO);
            nonOrthoGeometry.Add(tmpNO);
            decomposedPolys.RemoveAt(0);
        }
        else
        {
            decomposedPolys = GeoWrangler.close(decomposedPolys);
            pParsePoints(ref abortParse, decomposedPolys[0], layer, datatype, isText:false, vertical: vertical);
            if (decomposedPolys.Count <= 0)
            {
                return;
            }

            if (abortParse)
            {
                reset();
                return;
            }
            decomposedPolys.RemoveAt(0);
        }
    }

    private bool pText(PathD points)
    {
        bool ret = pRectangle(points);
        if (ret)
        {
            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.text);
        }
        return ret;
    }

    private bool pRectangle(PathD points)
    {
        try
        {
            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.rectangle);
            PointD dist = GeoWrangler.distanceBetweenPoints_point(points[0], points[2]);
            subShapeMinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
            subShapeMinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool pLShape(PathD points)
    {
        try
        {
            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.L);
            // Start naively, taking geometry as-is.
            // Get bounds.
            PathD bounds = GeoWrangler.getBounds(points);
            PointD extents = GeoWrangler.distanceBetweenPoints_point(bounds[0], bounds[1]);
            double extents_x = Math.Abs(extents.x);
            double extents_y = Math.Abs(extents.y);

            PointD dist = GeoWrangler.distanceBetweenPoints_point(points[0], points[2]);
            decimal poss_subShapeMinHorLength = Convert.ToDecimal(dist.x);
            decimal poss_subShapeMinVerLength = Convert.ToDecimal(dist.y);
            dist = GeoWrangler.distanceBetweenPoints_point(points[3], points[5]);
            decimal poss_subShape2MinHorLength = Convert.ToDecimal(dist.x);
            decimal poss_subShape2MinVerLength = Convert.ToDecimal(dist.y);

            bool handled = true;

            // Transforms may now make this awkward. Let's see what we have. using our extents to figure things out.
            // Left-hand edge is correctly located. Dig deeper.
            if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[5]).x) - extents_x) < Constants.tolerance)
            {
                if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[1]).y) - extents_y) < Constants.tolerance)
                {
                    // Bottom edge is correctly located for a non-transformed L.
                }
                else
                {
                    if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[4], points[5]).y) - extents_y) < Constants.tolerance)
                    {
                        // 90 degree counter-clockwise rotation
                        dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[5]);
                        poss_subShapeMinHorLength = Convert.ToDecimal(dist.y);
                        poss_subShapeMinVerLength = Convert.ToDecimal(dist.x);
                        dist = GeoWrangler.distanceBetweenPoints_point(points[2], points[4]);
                        poss_subShape2MinHorLength = Convert.ToDecimal(dist.y);
                        poss_subShape2MinVerLength = Convert.ToDecimal(dist.x);
                        minRotation = 90.0m;
                    }
                    else
                    {
                        handled = false;
                    }
                }
            }
            else
            {
                if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[1], points[2]).x) - extents_x) < Constants.tolerance)
                {
                    // Two scenarios lead to tbe same result here numerically - vertical flip or rotated 90 degrees clockwise.

                    if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[1]).y) - extents_y) < Constants.tolerance)
                    {
                        dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[3]);
                        poss_subShapeMinVerLength = Math.Abs(Convert.ToDecimal(dist.x));
                        poss_subShapeMinHorLength = Math.Abs(Convert.ToDecimal(dist.y));
                        dist = GeoWrangler.distanceBetweenPoints_point(points[0], points[4]);
                        poss_subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.x));
                        poss_subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.y));
                        minRotation = -90.0m;
                    }
                    else
                    {
                        if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[2], points[3]).y) - extents_y) < Constants.tolerance)
                        {
                            // 180 degree rotation.
                            dist = GeoWrangler.distanceBetweenPoints_point(points[2], points[4]);
                            poss_subShapeMinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                            poss_subShapeMinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                            dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[5]);
                            poss_subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                            poss_subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                            minRotation = 180.0m;
                        }
                        else
                        {
                            handled = false;
                        }
                    }
                    if (handled)
                    { 
                    }
                }
                else
                {
                    handled = false;
                }
            }

            if (!handled)
            {
                throw new Exception("L: unhandled geometry state!");
            }

            subShapeMinHorLength = Math.Abs(poss_subShapeMinHorLength);
            subShapeMinVerLength = Math.Abs(poss_subShapeMinVerLength);
            subShape2MinHorLength = Math.Abs(poss_subShape2MinHorLength);
            subShape2MinVerLength = Math.Abs(poss_subShape2MinVerLength);
            subShape2MinHorOffset = subShapeMinHorLength;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool pMightBeTorU(PathD points)
    {
        // Abuse tone inversion to see whether we have two islands afterwards (the gaps for the T) or 1 (for the U).
        int polyCount = GeoWrangler.invertTone(points, preserveCollinear: false, useBounds: true).Count;
        // int polyCount = workAroundInvertTone(points, CentralProperties.scaleFactorForOperation).Count;
        switch (polyCount)
        {
            case 1:
                return pUShape(points);
            case 2:
                return pTShape(points);
            default:
                return false;
        }
    }

    private bool pTShape(PathD points)
    {
        try
        {
            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.T);

            PointD dist;

            // Start naively, taking geometry as-is.
            // Get bounds.
            PathD bounds = GeoWrangler.getBounds(points);
            PointD extents = GeoWrangler.distanceBetweenPoints_point(bounds[0], bounds[1]);
            double extents_x = Math.Abs(extents.x);
            double extents_y = Math.Abs(extents.y);

            decimal poss_subShapeMinHorLength = 0;
            decimal poss_subShapeMinVerLength = 0;
            decimal poss_subShape2MinHorLength = 0;
            decimal poss_subShape2MinVerLength = 0;

            bool handled = true;

            // Figure out the transforms.
            if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[1]).y) - extents_y) < Constants.tolerance)
            {
                // T-shape appears to be correctly located.
                dist = GeoWrangler.distanceBetweenPoints_point(points[0], points[2]);
                poss_subShapeMinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                poss_subShapeMinHorLength = Math.Abs(Convert.ToDecimal(dist.x));

                dist = GeoWrangler.distanceBetweenPoints_point(points[4], points[6]);
                poss_subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                poss_subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                subShape2MinVerOffset = Convert.ToDecimal(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[6], points[7]).y));
            }
            else
            {
                if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[7]).x) - extents_x) < Constants.tolerance)
                {
                    // T-shape appears to be rotated 90 degrees CCW.
                    minRotation = -90.0m;
                    dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[7]);
                    poss_subShapeMinVerLength = Math.Abs(Convert.ToDecimal(dist.x));
                    poss_subShapeMinHorLength = Math.Abs(Convert.ToDecimal(dist.y));

                    dist = GeoWrangler.distanceBetweenPoints_point(points[2], points[4]);
                    poss_subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.x));
                    poss_subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.y));
                    subShape2MinVerOffset = Convert.ToDecimal(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[5], points[6]).x));
                }
                else
                {
                    if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[1], points[2]).x) - extents_x) < Constants.tolerance)
                    {
                        // T-shape appears to be rotated 90 degrees.
                        minRotation = 90.0m;
                        dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[3]);
                        poss_subShapeMinVerLength = Math.Abs(Convert.ToDecimal(dist.x));
                        poss_subShapeMinHorLength = Math.Abs(Convert.ToDecimal(dist.x));

                        dist = GeoWrangler.distanceBetweenPoints_point(points[4], points[6]);
                        poss_subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.x));
                        poss_subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.y));
                        subShape2MinVerOffset = Convert.ToDecimal(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[7]).x));
                    }
                    else
                    {
                        if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[4], points[5]).y) - extents_y) < Constants.tolerance)
                        {
                            // T-shape appears to be rotated 180 degrees.
                            minRotation = 180.0m;
                            dist = GeoWrangler.distanceBetweenPoints_point(points[4], points[6]);
                            poss_subShapeMinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                            poss_subShapeMinHorLength = Math.Abs(Convert.ToDecimal(dist.x));

                            dist = GeoWrangler.distanceBetweenPoints_point(points[0], points[2]);
                            poss_subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                            poss_subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                            subShape2MinVerOffset = Convert.ToDecimal(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[2], points[3]).y));
                        }
                        else
                        {
                            handled = false;
                        }
                    }
                }
            }

            if (!handled)
            {
                throw new Exception("T illegal geometry");
            }

            subShapeMinHorLength = poss_subShapeMinHorLength;
            subShapeMinVerLength = poss_subShapeMinVerLength;
            subShape2MinHorLength = poss_subShape2MinHorLength;
            subShape2MinVerLength = poss_subShape2MinVerLength;
            subShape2MinHorOffset = subShapeMinHorLength;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool pUShape(PathD points)
    {
        try
        {
            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.U);

            PointD dist;

            // Start naively, taking geometry as-is.
            // Get bounds.
            PathD bounds = GeoWrangler.getBounds(points);
            PointD extents = GeoWrangler.distanceBetweenPoints_point(bounds[0], bounds[1]);
            double extents_x = Math.Abs(extents.x);
            double extents_y = Math.Abs(extents.y);

            bool handled = true;

            // Figure out the transforms.
            subShapeMinHorLength = Convert.ToDecimal(extents_x);
            subShapeMinVerLength = Convert.ToDecimal(extents_y);

            // Figure out the transforms.
            if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[1]).y) - extents_y) < Constants.tolerance)
            {
                if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[1], points[2]).x) - extents_x) < Constants.tolerance)
                {
                    if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[2], points[3]).y) - extents_y) < Constants.tolerance)
                    {
                        // U notch facing downwards.
                        minRotation = 180.0m;
                        dist = GeoWrangler.distanceBetweenPoints_point(points[5], points[7]);
                        subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                        subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                        dist = GeoWrangler.distanceBetweenPoints_point(points[3], points[5]);
                        subShape2MinHorOffset = Math.Abs(Convert.ToDecimal(dist.x));
                        subShape2MinVerOffset = -Math.Abs(Convert.ToDecimal(dist.y));
                    }
                    else
                    {
                        if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[7]).x) - extents_x) < Constants.tolerance)
                        {
                            // U notch right
                            minRotation = -90.0m;
                            (subShapeMinHorLength, subShapeMinVerLength) = (subShapeMinVerLength, subShapeMinHorLength);
                            dist = GeoWrangler.distanceBetweenPoints_point(points[2], new(0,0));
                            minXPos = -Convert.ToDecimal(dist.y);
                            minYPos = -Convert.ToDecimal(dist.x);
                            dist = GeoWrangler.distanceBetweenPoints_point(points[3], points[5]);
                            subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                            subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                            (subShape2MinHorLength, subShape2MinVerLength) = (subShape2MinVerLength, subShape2MinHorLength);
                            dist = GeoWrangler.distanceBetweenPoints_point(points[2], points[4]);
                            subShape2MinHorOffset = Math.Abs(Convert.ToDecimal(dist.y));
                            subShape2MinVerOffset = 0;
                        }
                        else
                        {
                            handled = false;
                        }
                    }

                }
                else
                {
                    if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[7]).x) - extents_x) < Constants.tolerance)
                    {
                        // U notch up
                        dist = GeoWrangler.distanceBetweenPoints_point(points[2], points[4]);
                        subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                        subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));
                        dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[3]);
                        subShape2MinHorOffset = Math.Abs(Convert.ToDecimal(dist.x));
                        subShape2MinVerOffset = -Math.Abs(Convert.ToDecimal(dist.y));
                    }
                    else
                    {
                        handled = false;
                    }
                }
            }
            else
            {
                if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[6], points[7]).y) - extents_y) < Constants.tolerance)
                {
                    // U notch left
                    minRotation = 90.0m;
                    (subShapeMinHorLength, subShapeMinVerLength) = (subShapeMinVerLength, subShapeMinHorLength);
                    dist = GeoWrangler.distanceBetweenPoints_point(points[0], new(0,0));
                    minXPos = Convert.ToDecimal(dist.y);
                    minYPos = Convert.ToDecimal(dist.x);
                    dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[3]);
                    subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.y));
                    subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.x));
                    dist = GeoWrangler.distanceBetweenPoints_point(points[0], points[3]);
                    subShape2MinHorOffset = Math.Abs(Convert.ToDecimal(dist.x));
                    subShape2MinVerOffset = 0;
                }
                else
                {
                    handled = false;
                }
            }

            if (!handled)
            {
                throw new Exception("U illegal geometry");
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool pMightBeXorS(PathD points)
    {
        // Abuse tone inversion to see whether we have 4 islands afterwards (the gaps for the X) or 2 (for the S).
        int polyCount = GeoWrangler.invertTone(points, preserveCollinear:false, useBounds: true).Count;
        switch (polyCount)
        {
            case 2:
                return pSShape(points);
            case 4:
                return pXShape(points);
            default:
                return false;
        }
    }
    private bool pXShape(PathD points)
    {
        try
        {
            // This one is quite simple.

            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.X);

            // X-shape is unusual as the X, Y point is not the leftmost X, lowest Y.
            minXPos = Convert.ToDecimal(points[10].x);
            minYPos = Convert.ToDecimal(points[10].y);

            // Extract the subshape 1 values.
            PointD ss1 = GeoWrangler.distanceBetweenPoints_point(points[10], points[4]);

            subShapeMinHorLength = Math.Abs(Convert.ToDecimal(ss1.x));
            subShapeMinVerLength = Math.Abs(Convert.ToDecimal(ss1.y));

            // Extract the subshape 2 values.
            PointD ss2 = GeoWrangler.distanceBetweenPoints_point(points[0], points[6]);

            subShape2MinHorLength = Math.Abs(Convert.ToDecimal(ss2.x));
            subShape2MinVerLength = Math.Abs(Convert.ToDecimal(ss2.y));

            PointD ss2offsets = GeoWrangler.distanceBetweenPoints_point(points[0], points[10]);
            subShape2MinHorOffset = -Math.Abs(Convert.ToDecimal(ss2offsets.x));
            subShape2MinVerOffset = Math.Abs(Convert.ToDecimal(ss2offsets.y));

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool pSShape(PathD points)
    {
        try
        {
            setInt(properties_i.shapeIndex, (int)typeShapes_mode1.S);

            PointD dist;

            // Start naively, taking geometry as-is.
            // Get bounds.
            PathD bounds = GeoWrangler.getBounds(points);
            PointD extents = GeoWrangler.distanceBetweenPoints_point(bounds[0], bounds[1]);
            double extents_x = Math.Abs(extents.x);
            double extents_y = Math.Abs(extents.y);

            // Figure out the transforms.
            subShapeMinHorLength = Convert.ToDecimal(extents_x);
            subShapeMinVerLength = Convert.ToDecimal(extents_y);

            // Figure out the transforms.
            if (Math.Abs(Math.Abs(GeoWrangler.distanceBetweenPoints_point(points[0], points[1]).y) - extents_y) > Constants.tolerance)
            {
                dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[3]);
                subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));

                dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[0]);
                subShape2MinHorOffset = Math.Abs(Convert.ToDecimal(dist.x));
                subShape2MinVerOffset = Math.Abs(Convert.ToDecimal(dist.y));

                dist = GeoWrangler.distanceBetweenPoints_point(points[7], points[9]);
                subShape3MinHorLength = Math.Abs(Convert.ToDecimal(dist.x));
                subShape3MinVerLength = Math.Abs(Convert.ToDecimal(dist.y));

                dist = GeoWrangler.distanceBetweenPoints_point(points[6], points[8]);
                subShape3MinHorOffset = Math.Abs(Convert.ToDecimal(dist.x));
                subShape3MinVerOffset = Math.Abs(Convert.ToDecimal(dist.y));
            }
            else
            {
                minRotation = -90.0m;

                dist = GeoWrangler.distanceBetweenPoints_point(points[2], points[4]);
                subShape2MinHorLength = Math.Abs(Convert.ToDecimal(dist.y));
                subShape2MinVerLength = Math.Abs(Convert.ToDecimal(dist.x));

                dist = GeoWrangler.distanceBetweenPoints_point(points[1], points[2]);
                subShape2MinHorOffset = Math.Abs(Convert.ToDecimal(dist.y));
                subShape2MinVerOffset = Math.Abs(Convert.ToDecimal(dist.x));

                dist = GeoWrangler.distanceBetweenPoints_point(points[9], points[11]);
                subShape3MinHorLength = Math.Abs(Convert.ToDecimal(dist.y));
                subShape3MinVerLength = Math.Abs(Convert.ToDecimal(dist.x));

                dist = GeoWrangler.distanceBetweenPoints_point(points[7], points[9]);
                subShape3MinHorOffset = Math.Abs(Convert.ToDecimal(dist.y));
                subShape3MinVerOffset = Math.Abs(Convert.ToDecimal(dist.x));
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}