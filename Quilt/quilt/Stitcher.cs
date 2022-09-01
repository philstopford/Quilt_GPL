using Eto.Drawing;
using geoCoreLib;
using geoLib;
using geoWrangler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quilt;

public class Stitcher
{
    public delegate void GeneratingPatternUI();
    public GeneratingPatternUI generatingPatternUI { get; set; }

    public delegate void IndeterminateQuiltUI(string tooltipText, string labelText);
    public IndeterminateQuiltUI indeterminateQuiltUI { get; set; }

    public delegate void StitchingQuiltUI();
    public StitchingQuiltUI stitchingQuiltUI { get; set; }

    public delegate void Viewport();
    public Viewport viewport { get; set; }

    public delegate void UpdateUIStatus(string text);
    public UpdateUIStatus updateUIStatus { get; set; }

    public delegate void UpdateUIProgress(double val);
    public UpdateUIProgress updateUIProgress { get; set; }

    public delegate void UpdateUIProgress2(int count, int max);
    public UpdateUIProgress2 updateUIProgress2 { get; set; }

    public delegate void DoneQuiltUI(string text);
    public DoneQuiltUI doneQuiltUI { get; set; }

    private bool evaluating;
    private object exportLock;
    private object evalLock;

    private const int mode = 0; // 0 is hashset, 1 is parallelFor, 2 is distinct, 3 is distinctparallelfor

    private int cols, rows;

    private double width, height, left, bottom;

    private double padding; // will pad the bounding box by defined amount, e.g. 0.1 to pad by 10% of width and height.

    private int showInput;

    private int counter;
    private int max;

    private GeoCore g;
    private GCDrawingfield drawing_;
    private ParallelOptions po;

    // Quilt will hold the list of patterns
    public ObservableCollection<string> patternElementNames { get; private set; }
    public ObservableCollection<string> patternElementNames_filtered { get; private set; } // screens out active layer and adds 'World Origin' at top of the list. Helper function is available to get index of layer.
    public ObservableCollection<string> patternElementNamesForMerge_filtered { get; private set; } // screens out active layer and adds 'Self' at top of the list. Helper function is available to get index of layer.
    public ObservableCollection<string> patternElementNames_filtered_array { get; private set; }

    private List<Pattern> patterns;
    private HashSet<int> hashes;
    private List<PatternElement> patternElements; // our pattern elements from which patterns will be constructed.
    public List<PreviewShape>[] previewShapes { get; private set; }
    public PreviewShape[] backgroundShapes { get; private set; }
    private QuiltContext quiltContext;

    private PatternElement copyBuffer;

    private System.Timers.Timer timer;

    private void pTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        updateUIProgress?.Invoke((double)counter / max);
    }

    private void pStatusUIWrapper(string text)
    {
        updateUIStatus?.Invoke(text);
    }

    private void pProgressUIWrapper(double val)
    {
        updateUIProgress?.Invoke(val);
    }

    public int getPatternCount()
    {
        return pGetPatternCount();
    }

    private int pGetPatternCount()
    {
        return patterns.Count;
    }

    public Stitcher(ref QuiltContext context)
    {
        pInit(ref context);
    }

    private void pInit(ref QuiltContext context)
    {
        exportLock = new object();
        evalLock = new object();

        patternElementNames = new ObservableCollection<string>();
        patternElementNames_filtered = new ObservableCollection<string>();
        patternElementNamesForMerge_filtered = new ObservableCollection<string>();
        patternElementNames_filtered_array = new ObservableCollection<string>();
        quiltContext = context;

        copyBuffer = null;

        reset();
    }

    public bool isCopySet()
    {
        return pIsCopySet();
    }

    private bool pIsCopySet()
    {
        return copyBuffer != null;
    }

    public void setCopy(int index)
    {
        pSetCopy(index);
    }

    private void pSetCopy(int index)
    {
        copyBuffer = new PatternElement(patternElements[index]);
    }

    public void paste(int index)
    {
        pPaste(index);
    }

    private void pPaste(int index)
    {
        if (!pIsCopySet())
        {
            return;
        }

        patternElements[index] = new PatternElement(copyBuffer);
        pUpdateQuilt();
    }

    private void pAddPattern(Pattern pattern)
    {
        // Screen approach by mode.
        switch (mode)
        {
            case 0:
                pAddPattern_hashset(pattern);
                break;
            default:
                pAddPattern_(pattern);
                break;
        }
    }

    private void pCleanup()
    {
        pCleanup_distinct();
        /*
        // Alternative methods to screen patterns; open to comment about which is best (performance / robustness)
        int m = 2;
        switch (m)
        {
            case 0:
                break;
            case 1:
                // This seems to be slow (2 seconds for the 10k test case
                pCleanup_parallelFor();
                break;
            case 2:
                // Faster than 1 - less than 1 second for the 10k test case
                pCleanup_distinct();
                break;
            case 3:
                // This gets stuck
                pCleanup_distinctParallel();
                break;
        }
        */
    }

    private void pAddPattern_(Pattern pattern)
    {
        patterns.Add(pattern);
    }

    private void pAddPattern_hashset(Pattern pattern)
    {
        int tHash = pattern.GetHashCode();
        // Try fast check first, but this is not robust due to limited int range
        if (!hashes.Contains(tHash))
        {
            patterns.Add(pattern);
            hashes.Add(tHash);
        }
        else
        {
            // Might actually not have a duplicate. Let's check. This will be slow.
            ParallelOptions pco = new();
            // Attempt at parallelism.
            CancellationTokenSource pcs = new();
            // Try to thread it for best possible performance.
            Parallel.For(0, patterns.Count, pco, (p) =>
            {
                // If we find an equivalent pattern, we need to abort.
                if (Equals(patterns[p], pattern))
                {
                    pcs.Cancel();
                }
            });
            patterns.Add(pattern);
        }
    }

    private class PatternComparer : IEqualityComparer<Pattern>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Pattern x, Pattern y)
        {
            return pEquals(x, y);
        }

        private static bool pEquals(Pattern x, Pattern y)
        {
            // Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            // Check whether any of the compared objects is null.
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            // Check whether the products' properties are equal.
            bool unique = x.equivalence(y);
            return unique;
        }

        // If Equals() returns true for a pair of objects,
        // GetHashCode must return the same value for these objects.

        public int GetHashCode(Pattern x)
        {
            return pGetHashCode(x);
        }

        private static int pGetHashCode(Pattern x)
        {
            // Check whether the object is null.
            return x?.GetHashCode() ?? 0;

            // Get the hash code for the instance if it is not null.
        }
    }

    /*
    void pCleanup_distinctParallel()
    {
        indeterminateQuiltUI?.Invoke("Clean-up", "Clean-up");
        po = new ParallelOptions();
        int threads = po.MaxDegreeOfParallelism;
        var clean_ = patterns.Distinct(new PatternComparer()).AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).WithDegreeOfParallelism(threads);
        patterns = clean_.ToList();
    }

    void pCleanup_parallelFor()
    {
        List<Pattern> clean = new List<Pattern>();
        List<int> cleanHash = new List<int>();

        // Make use of threading here to reduce overhead in large patterns.
        // Set our parallel task options based on user settings.
        po = new ParallelOptions();
        // Attempt at parallelism.
        CancellationTokenSource cancelSource = new CancellationTokenSource();

        int updateSample = patterns.Count / 100;

        if (updateSample < 1)
        {
            updateSample = 1;
        }

        // At first glance, this might be concerning. However, we are scanning for any equivalent pattern. If an equivalent pattern is found, we reject the attempt to add an identical pattern.
        // As such, since we are not looking for uniqueness, but equivalence, this check is safely parallelized. We lock patterns to prevent misfires.
        updateUIStatus?.Invoke("Clean-up");
        float progress = 0.0f;
        for (int i = 0; i < patterns.Count; i++)
        {
            Pattern pattern = patterns[i];
            int pHC = pattern.GetHashCode();
            bool add = true;

            Parallel.For(0, cleanHash.Count, po, (p, loopState) =>
            {
                // Fail the attempt to add if an equivalent pattern is found in the clean list.
                add = Equals(add, !(pHC == cleanHash[p]));

                if (!add)
                {
                    // We already failed the attempt, so cancel the evaluation and move on.
                    cancelSource.Cancel();
                }
            });

            if (add)
            {
                clean.Add(pattern);
                cleanHash.Add(pattern.GetHashCode());
            }
            if (i % updateSample == 0)
            {
                progress += 0.01f;
                updateUIProgress?.Invoke(progress);
            }

        }

        patterns = clean.ToList();
    }
    */

    private void pCleanup_distinct()
    {
        indeterminateQuiltUI?.Invoke("Clean-up", "Clean-up");
        var clean_ = patterns.Distinct(new PatternComparer());
        patterns = clean_.ToList();
    }

    public void reset(bool empty = false)
    {
        pReset(empty);
    }

    private void pReset(bool empty)
    {
        previewShapes = Array.Empty<List<PreviewShape>>();
        patternElementNames.Clear();
        if (!empty)
        {
            patternElementNames.Add("First");
        }

        patternElements = new List<PatternElement>();
        if (!empty)
        {
            patternElements.Add(new PatternElement());
        }

        patterns = new List<Pattern>();
        if (mode == 0)
        {
            hashes = new HashSet<int>();
        }
        if (!empty)
        {
            pAddPattern(new Pattern(ref quiltContext, patternElements.ToList()));
        }

        patternElementNames_filtered.Clear();
        patternElementNamesForMerge_filtered.Clear();
        patternElementNames_filtered_array.Clear();
        if (!empty)
        {
            update_filteredPatternedElementNames(0);
        }
        previewShapes = Array.Empty<List<PreviewShape>>();

        padding = 0;

        showInput = 1;

        copyBuffer = null;

        if (!empty)
        {
            pUpdateQuilt();
        }
    }

    public void setPadding(double value)
    {
        pSetPadding(value);
    }

    private void pSetPadding(double value)
    {
        padding = value;
    }

    public double getPadding()
    {
        return pGetPadding();
    }

    private double pGetPadding()
    {
        return padding;
    }

    public void setShowInput(int value)
    {
        pSetShowInput(value);
    }

    private void pSetShowInput(int value)
    {
        showInput = value;
    }

    public int getShowInput()
    {
        return pGetShowInput();
    }

    private int pGetShowInput()
    {
        return showInput;
    }

    public void update_filteredPatternedElementNames(int selectedEntry)
    {
        pUpdate_filteredPatternedElementNames(selectedEntry);
    }

    private void pUpdate_filteredPatternedElementNames(int selectedEntry)
    {
        patternElementNames_filtered.Clear();
        patternElementNames_filtered.Add("World Origin");

        patternElementNamesForMerge_filtered.Clear();
        patternElementNamesForMerge_filtered.Add("Self");

        patternElementNames_filtered_array.Clear();
        patternElementNames_filtered_array.Add("Self");

        if (selectedEntry == -1)
        {
            return;
        }

        for (int i = 0; i < patternElementNames.Count; i++)
        {
            // Avoid offering current element, and any bounding elements.
            if (i == selectedEntry || patternElements[i].getInt(PatternElement.properties_i.shapeIndex) == (int)CentralProperties.shapeNames.bounding)
            {
                continue;
            }

            patternElementNames_filtered.Add(patternElementNames[i]);
            patternElementNamesForMerge_filtered.Add(patternElementNames[i]);
            if (patternElements[i].isXArray() || patternElements[i].isYArray())
            {
                patternElementNames_filtered_array.Add(patternElementNames[i]);
            }
        }
    }

    public void addPatternElements(List<PatternElement> p)
    {
        pAddPatternElements(p);
    }

    private void pAddPatternElements(List<PatternElement> p)
    {
        patternElements = p.ToList();
        patternElementNames.Clear();
        foreach (PatternElement t in patternElements)
        {
            patternElementNames.Add(t.getString(PatternElement.properties_s.name));
        }

        pUpdate_filteredPatternedElementNames(0);
    }

    public void addPatternElement(string name)
    {
        pAddPatternElement(name);
    }

    private void pAddPatternElement(string name)
    {
        patternElements.Add(new PatternElement());
        pPostAdd(name);
    }

    public void addPatternElement(PatternElement source)
    {
        pAddPatternElement(source);
    }

    private void pAddPatternElement(PatternElement source)
    {
        patternElements.Add(new PatternElement(source));
        pPostAdd(source.getString(PatternElement.properties_s.name));
    }

    private void pPostAdd(string name)
    {
        patternElementNames.Add(name);
        patternElementNames_filtered.Add(name);
        patternElementNamesForMerge_filtered.Add(name);
        pUpdateQuilt();
    }

    public void renamePatternElement(int index, string name)
    {
        pRenamePatternElement(index, name);
    }

    private void pRenamePatternElement(int index, string name)
    {
        patternElements[index].setString(PatternElement.properties_s.name, name);
        patternElementNames[index] = name;
        pUpdate_filteredPatternedElementNames(index);
    }

    private bool pRemoveReferenceToElement(int currElIndex, int removedElIndex, PatternElement.properties_i shapeProp, PatternElement.properties_i subShapeProp, int currElSubShapeIndex = -1)
    {
        bool changed = false;
        int relElIndex = patternElements[currElIndex].getInt(shapeProp, currElSubShapeIndex);
        int relElSSIndex = 0;
        patternElements[currElIndex].getInt(subShapeProp, currElSubShapeIndex);

        int newref = 0;
        if (relElIndex == removedElIndex)
        {
            changed = true;
        }
        else if (relElIndex > removedElIndex)
        {
            newref = relElIndex - 1; // decrement the reference as the reference layer below this point is being removed.
            changed = true;
        }

        if (!changed)
        {
            return false;
        }

        // Make sure we don't have an invalid subshape reference index.
        int subshapeRefCount = patternElements[newref].getSubShapeCount();
        if (relElSSIndex >= subshapeRefCount)
        {
            relElSSIndex = 0;
        }
        patternElements[currElIndex].setInt(shapeProp, newref, currElSubShapeIndex); // global reference as the reference layer will be removed.
        patternElements[currElIndex].setInt(subShapeProp, relElSSIndex, currElSubShapeIndex); // change subshape index if needed.

        return true;
    }

    private bool pRemoveReferenceToElement(int currElIndex, int removedElIndex, PatternElement.properties_i shapeProp, int currElSubShapeIndex = -1)
    {
        bool changed = false;
        int relElIndex = patternElements[currElIndex].getInt(shapeProp, currElSubShapeIndex);

        int newref = 0;
        if (relElIndex == removedElIndex)
        {
            changed = true;
        }
        else if (relElIndex > removedElIndex)
        {
            newref = relElIndex - 1; // decrement the reference as the reference layer below this point is being removed.
            changed = true;
        }

        if (changed)
        {
            // Make sure we don't have an invalid subshape reference index.
            patternElements[currElIndex].setInt(shapeProp, newref, currElSubShapeIndex); // global reference as the reference layer will be removed.
        }
            
        return changed;
    }        

    public void removePatternElement(int index)
    {
        pRemovePatternElement(index);
    }

    private void pRemovePatternElement(int index)
    {
        patternElementNames.RemoveAt(index);
        patternElements.RemoveAt(index);

        // We need to adjust any other elements in the pattern that have references to the element being removed.
#if !QUILTSINGLETHREADED
        Parallel.For(0, patternElements.Count, i =>
#else
            for (int i = 0; i < patternElements.Count; i++)
#endif
            {
                bool changed = pRemoveReferenceToElement(i, index, PatternElement.properties_i.xPosRef);

                bool tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.yPosRef);

                changed = changed || tmp;
                
                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.xPosSubShapeRef);

                changed = changed || tmp;
                
                tmp =  pRemoveReferenceToElement(i, index, PatternElement.properties_i.yPosSubShapeRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.arrayRotationRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.rotationRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.arrayRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.linkedElementIndex);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.MinHTRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.MinVTRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.HTIncRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.VTIncRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.HTStepsRef);

                changed = changed || tmp;

                tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.VTStepsRef);

                changed = changed || tmp;

                for (int ss = 0; ss < 3; ss++)
                {
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.MinHLRef, PatternElement.properties_i.MinHLSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.HLIncRef, PatternElement.properties_i.HLIncSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.HLStepsRef, PatternElement.properties_i.HLStepsSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.MinVLRef, PatternElement.properties_i.MinVLSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.VLIncRef, PatternElement.properties_i.VLIncSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.VLStepsRef, PatternElement.properties_i.VLStepsSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.MinHORef, PatternElement.properties_i.MinHOSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.HOIncRef, PatternElement.properties_i.HOIncSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.HOStepsRef, PatternElement.properties_i.HOStepsSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.MinVORef, PatternElement.properties_i.MinVOSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.VOIncRef, PatternElement.properties_i.VOIncSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.VOStepsRef, PatternElement.properties_i.VOStepsSubShapeRef, ss);
                    changed = changed || tmp;
                    tmp = pRemoveReferenceToElement(i, index, PatternElement.properties_i.tipRef, PatternElement.properties_i.tipSubShapeRef, ss);
                    changed = changed || tmp;
                }

                if (changed)
                {
                    // Clear midpoint to force a recompute
                    patternElements[i].setMidPoint(null);
                }
            }
#if !QUILTSINGLETHREADED
        );
#endif
        pUpdateQuilt();
    }

    public PatternElement getPatternElement(int patternIndex, int index)
    {
        return pGetPatternElement(patternIndex, index);
    }

    private PatternElement pGetPatternElement(int patternIndex, int index)
    {
        if (patternIndex == 0) // reference pattern
        {
            if (index >= patternElements.Count)
            {
                throw new Exception("Pattern element index exceeds pattern element count!");
            }
            return patternElements[index];
        }

        if (patternIndex >= patterns.Count)
        {
            throw new Exception("Pattern index exceeds pattern count!");
        }
        if (index >= patterns[patternIndex].getPatternElements().Count)
        {
            throw new Exception("Pattern element index exceeds pattern element count!");
        }
        return patterns[patternIndex].getPatternElement(index);
    }

    public List<PatternElement> getPatternElements(int pattern)
    {
        return pGetPatternElements(pattern);
    }

    private List<PatternElement> pGetPatternElements(int pattern)
    {
        return patterns[pattern].getPatternElements();
    }

    private void pClearPatterns()
    {
        patterns.Clear();
        if (mode == 0)
        {
            hashes.Clear();
        }
    }

    private decimal pGetDecimalValue(PatternElement pattEl, int pattElIndex, PatternElement.properties_i pattElProp, PatternElement.properties_i pattElSSProp, PatternElement.properties_decimal pattElDec, int subshapeindex)
    {
        decimal ret = pattEl.getDecimal(pattElDec, subshapeindex);

        int ref_ = pattEl.getInt(pattElProp, subshapeindex);
        if (ref_ <= 0)
        {
            return ret;
        }

        if (ref_ <= pattElIndex)
        {
            ref_--; // Offset due to missing index for current element.
        }

        int ssref = pattEl.getInt(pattElSSProp, subshapeindex);

        // Any cascading references.....
        int nestedRef = patternElements[ref_].getInt(pattElProp, ssref);

        if (nestedRef > 0)
        {
            if (nestedRef - 1 <= ref_)
            {
                nestedRef--; // Offset due to missing index for current element.
            }
            ssref = patternElements[ref_].getInt(pattElSSProp, subshapeindex);
        }
                
        ret = pGetDecimalValue(patternElements[nestedRef], nestedRef, pattElProp, pattElSSProp, pattElDec, ssref);

        return ret;
    }

    private int pGetIntValue(PatternElement pattEl, int pattElIndex, PatternElement.properties_i pattElProp, PatternElement.properties_i pattElSSProp, PatternElement.properties_i pattElInt, int subshapeindex)
    {
        int ret = pattEl.getInt(pattElInt, subshapeindex);

        int ref_ = pattEl.getInt(pattElProp, subshapeindex);
        if (ref_ <= 0)
        {
            return ret;
        }

        if (ref_ <= pattElIndex)
        {
            ref_--; // Offset due to missing index for current element.
        }

        int ssref = pattEl.getInt(pattElSSProp, subshapeindex);

        // Any cascading references.....
        int nestedRef = patternElements[ref_].getInt(pattElProp, ssref);
                
        if (nestedRef > 0)
        {
            if (nestedRef - 1 <= ref_)
            {
                nestedRef--; // Offset due to missing index for current element.
            }
            ssref = patternElements[ref_].getInt(pattElSSProp, subshapeindex);
        }
                
        ret = pGetIntValue(patternElements[nestedRef], nestedRef, pattElProp, pattElSSProp, pattElInt, ssref);

        return ret;
    }

    private PatternElement pSetDependentValues(PatternElement pattEl, int pattElIndex, PatternElement.properties_i pattElProp, PatternElement.properties_i pattElSSProp, PatternElement.properties_decimal pattElDec, int subshapeindex)
    {
        pattEl.setDecimal(pattElDec, pGetDecimalValue(pattEl, pattElIndex, pattElProp, pattElSSProp, pattElDec, subshapeindex), subshapeindex);

        return pattEl;
    }

    private PatternElement pSetDependentValues(PatternElement pattEl, int pattElIndex, PatternElement.properties_i pattElProp, PatternElement.properties_i pattElSSProp, PatternElement.properties_i pattElInt, int subshapeindex)
    {
        pattEl.setInt(pattElInt, pGetIntValue(pattEl, pattElIndex, pattElProp, pattElSSProp, pattElInt, subshapeindex), subshapeindex);

        return pattEl;
    }

    private PatternElement pSetDependentDimensions(PatternElement pattEl, int pattElIndex)
    {
        for (int subshapeindex = 0; subshapeindex < 3; subshapeindex++)
        {
            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.MinHLRef,
                PatternElement.properties_i.MinHLSubShapeRef, PatternElement.properties_decimal.minHorLength, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.MinHORef,
                PatternElement.properties_i.MinHOSubShapeRef, PatternElement.properties_decimal.minHorOffset, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.MinVLRef,
                PatternElement.properties_i.MinVLSubShapeRef, PatternElement.properties_decimal.minVerLength, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.MinVORef,
                PatternElement.properties_i.MinVOSubShapeRef, PatternElement.properties_decimal.minVerOffset, subshapeindex);
        }
            
        return pattEl;
    }

    private PatternElement pSetDependentIncrements(PatternElement pattEl, int pattElIndex)
    {
        for (int subshapeindex = 0; subshapeindex < 3; subshapeindex++)
        {
            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.HLIncRef,
                PatternElement.properties_i.HLIncSubShapeRef, PatternElement.properties_decimal.horLengthInc, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.HOIncRef,
                PatternElement.properties_i.HOIncSubShapeRef, PatternElement.properties_decimal.horOffsetInc, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.VLIncRef,
                PatternElement.properties_i.VLIncSubShapeRef, PatternElement.properties_decimal.verLengthInc, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.VOIncRef,
                PatternElement.properties_i.VOIncSubShapeRef, PatternElement.properties_decimal.verOffsetInc, subshapeindex);
        }

        return pattEl;
    }

    private PatternElement pSetDependentSteps(PatternElement pattEl, int pattElIndex)
    {
        for (int subshapeindex = 0; subshapeindex < 3; subshapeindex++)
        {
            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.HLStepsRef,
                PatternElement.properties_i.HLStepsSubShapeRef, PatternElement.properties_i.horLengthSteps, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.HOStepsRef,
                PatternElement.properties_i.HOStepsSubShapeRef, PatternElement.properties_i.horOffsetSteps, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.VLStepsRef,
                PatternElement.properties_i.VLStepsSubShapeRef, PatternElement.properties_i.verLengthSteps, subshapeindex);

            pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.VOStepsRef,
                PatternElement.properties_i.VOStepsSubShapeRef, PatternElement.properties_i.verOffsetSteps, subshapeindex);
        }

        return pattEl;
    }

    private PatternElement pSetDependentTips(PatternElement pattEl, int pattElIndex)
    {
        pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.tipRef,
            PatternElement.properties_i.tipSubShapeRef, PatternElement.properties_i.shape0Tip, 0);

        pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.tipRef,
            PatternElement.properties_i.tipSubShapeRef, PatternElement.properties_i.shape1Tip, 1);

        pattEl = pSetDependentValues(pattEl, pattElIndex, PatternElement.properties_i.tipRef,
            PatternElement.properties_i.tipSubShapeRef, PatternElement.properties_i.shape2Tip, 2);

        return pattEl;
    }

    private void pCheckDependentSubShapes()
    {
        for (int i = 0; i < patternElements.Count; i++)
        {
            pCheckDependentSubShapes_LO(i);
            pCheckDependentSubShapes_LOInc(i);
            pCheckDependentSubShapes_LOSteps(i);
            pCheckSubShapeIsValid(PatternElement.properties_i.tipRef,
                PatternElement.properties_i.tipSubShapeRef, i);
        }
    }

    private void pCheckDependentSubShapes_LO(int elementIndex)
    {
        pCheckSubShapeIsValid(PatternElement.properties_i.MinHLRef,
            PatternElement.properties_i.MinHLSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.MinVLRef,
            PatternElement.properties_i.MinVLSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.MinHORef,
            PatternElement.properties_i.MinHOSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.MinVORef,
            PatternElement.properties_i.MinVOSubShapeRef, elementIndex);
    }

    private void pCheckSubShapeIsValid(PatternElement.properties_i prop, PatternElement.properties_i ssprop, int elementIndex)
    {
        for (int ss = 0; ss < 3; ss++)
        {
            int refIndex = patternElements[elementIndex].getInt(prop, _subShapeRef: ss);
            if (refIndex <= 0)
            {
                continue;
            }

            if (refIndex <= elementIndex)
            {
                refIndex--;
            }

            int subShapeIndex = patternElements[elementIndex].getInt(ssprop, _subShapeRef: ss);
            // Does the reference element have enough subshapes to satisfy?
            int refSubShapeCount = patternElements[refIndex].getSubShapeCount();
            if (subShapeIndex >= refSubShapeCount)
            {
                // Fix references, also in main pattern.
                patternElements[elementIndex].setInt(ssprop, 0, ss);
            }
        }
    }

    private void pCheckDependentSubShapes_LOInc(int elementIndex)
    {
        pCheckSubShapeIsValid(PatternElement.properties_i.HLIncRef,
            PatternElement.properties_i.HLIncSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.VLIncRef,
            PatternElement.properties_i.VLIncSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.HOIncRef,
            PatternElement.properties_i.HOIncSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.VOIncRef,
            PatternElement.properties_i.VOIncSubShapeRef, elementIndex);
    }

    private void pCheckDependentSubShapes_LOSteps(int elementIndex)
    {
        pCheckSubShapeIsValid(PatternElement.properties_i.HLStepsRef,
            PatternElement.properties_i.HLStepsSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.VLStepsRef,
            PatternElement.properties_i.VLStepsSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.HOStepsRef,
            PatternElement.properties_i.HOStepsSubShapeRef, elementIndex);
        pCheckSubShapeIsValid(PatternElement.properties_i.VOStepsRef,
            PatternElement.properties_i.VOStepsSubShapeRef, elementIndex);
    }
        
    public void updateQuilt()
    {
        pUpdateQuilt();
    }

    private void pUpdateQuilt()
    {
        if (evaluating)
        {
            return;
        }
        evaluating = true; // bounce any other requests to update the quilt. Might not be necessary, but a safety measure.
        Monitor.Enter(evalLock); // try and prevent overlapping quilt builds, for whatever reason.
        try
        {
            // Here, we need to generate our quilt based on the variations in the reference pattern definition and create patterns.
            pClearPatterns();

            if (patternElements.Count == 0)
            {
                doneQuiltUI?.Invoke("No pattern elements!");
                return;
            }

            Stopwatch sw = new();
            indeterminateQuiltUI?.Invoke("Generating Quilt", "Generating");
            sw.Reset();
            sw.Start();

            timer = new System.Timers.Timer {AutoReset = true, Interval = CentralProperties.timer_interval};
            // Set up timers for the UI refresh
            timer.Elapsed += pTimerElapsed;

            // Get our total variant count for all elements
            long variant = 1;

            long variantCount = patternElements[0].getInt(PatternElement.properties_i.maxVariants);

            for (int i = 1; i < patternElements.Count; i++)
            {
                variantCount *= patternElements[i].getInt(PatternElement.properties_i.maxVariants);
            }

            if (variantCount > 0)
            {
                // Need to sample for progress feedback.
                variantCount /= 100;
            }

            if (variantCount < 1)
            {
                variantCount = 1;
            }

            // Do the subshape dependencies make sense? If not, fix them up.
            pCheckDependentSubShapes();

            PatternElement pattEl = new(pGetPatternElement(0, 0)); // get our base pattern first element
                
            string pattName = pattEl.getString(PatternElement.properties_s.name);

            updateUIStatus?.Invoke(pattName);

            // Get the first variant from the first pattern element.
            PatternElement varEl = pattEl.getNextVariant();

            double progress = 0;

            generatingPatternUI?.Invoke();

            while (varEl != null)
            {
                // For each variant, add a new pattern
                Pattern newPattern = new(ref quiltContext, new List<PatternElement> { new(varEl) });

                pAddPattern(newPattern);

                varEl = pattEl.getNextVariant();

                if (variant % variantCount == 0)
                {
                    updateUIProgress?.Invoke(progress);
                    progress += 0.01;
                }
                variant++;
            }

            // Start dealing with other elements and their variations.
            int level = 1;

            progress = 0;

            // Here, we want to iterate across our list of patterns. For each existing pattern, we need to create a list of derived patterns with the new set of variants.
            // This builds out the factorial for all variants of all elements.
            while (level < patternElements.Count)
            {
                List<Pattern> oldPatterns = patterns.ToList();
                pClearPatterns();

                pattEl = new PatternElement(patternElements[level]);
                    
                // We need to substitute dependent parameters here.
                pattEl = pSetDependentDimensions(pattEl, level);
                pattEl = pSetDependentIncrements(pattEl, level);
                pattEl = pSetDependentSteps(pattEl, level);
                pattEl = pSetDependentTips(pattEl, level);
                    
                pattName = pattEl.getString(PatternElement.properties_s.name);

                updateUIStatus?.Invoke(pattName);

                foreach (Pattern t in oldPatterns)
                {
                    varEl = pattEl.getNextVariant();

                    while (varEl != null)
                    {
                        // Get our upper level element definitions for this pattern.
                        List<PatternElement> pattElements = t.getPatternElements().ToList();

                        // Add our new pattern element to the list for the pattern.
                        pattElements.Add(varEl);

                        // Add the pattern variant to the quilt.
                        pAddPattern(new Pattern(ref quiltContext, pattElements.ToList()));

                        varEl = pattEl.getNextVariant();

                        if (variant % variantCount == 0)
                        {
                            updateUIProgress?.Invoke(progress);
                            progress += 0.01;
                        }
                        variant++;
                    }
                }

                level++;
            }

            pCleanup();

            int patternCount = patterns.Count;
            po = new ParallelOptions();

            // The below forces dimensions to be equivalent, overriding any individual variation. 
            // This code works, but UI control is missing right now and needs thought about how to implement.
            // Here, we walk the dependencies of the elements in each pattern.
            // The dependent dimensions are computed (e.g. linked width, height, etc.)
#if !QUILTSINGLETHREADED
            Parallel.For(0, patternCount, po, (pattern) =>
#else
                for (int pattern = 0; pattern < patternCount; pattern++)
#endif
                {
                    patterns[pattern].computeDimensions(true);
                }
#if !QUILTSINGLETHREADED
            );
#endif

            // Generate the preview shapes
            previewShapes = Array.Empty<List<PreviewShape>>();
            backgroundShapes = Array.Empty<PreviewShape>();
            if (patternCount == 0)
            {
                doneQuiltUI?.Invoke("");
                return;
            }
                
            updateUIProgress?.Invoke(0.0f);
                
            // Get our bounding box information to inform grid placement.
            List<GeoLibPointF> bb = new();
            try
            {
                bb = patterns[0].boundingBox().getPoints();
            }
            catch (Exception)
            {
                bb.Add(new GeoLibPointF(0, 0));
                bb.Add(new GeoLibPointF(0, 0));
                bb.Add(new GeoLibPointF(0, 0));
                bb.Add(new GeoLibPointF(0, 0));
            }
            GeoLibPointF bl = new(bb[0]);
            GeoLibPointF tr = new(bb[2]);

            double right = Math.Max(bl.X, tr.X);
            left = Math.Min(bl.X, tr.X);

            double top = Math.Max(bl.Y, tr.Y);
            bottom = Math.Min(bl.Y, tr.Y);

            updateUIStatus?.Invoke("Weaving");

            previewShapes = new List<PreviewShape>[patternCount];
            backgroundShapes = new PreviewShape[patternCount];
                
            counter = 0;
            max = patternCount;

            timer.Start();
            Parallel.For(0, patternCount, po, (pattern) =>
                {
                    previewShapes[pattern] = patterns[pattern].generate_shapes().ToList();

                    bb = patterns[pattern].boundingBox().getPoints();

                    GeoLibPointF bl_test = new(bb[0]);
                    GeoLibPointF tr_test = new(bb[2]);

                    bottom = Math.Min(bottom, Math.Min(bl_test.Y, tr_test.Y));
                    left = Math.Min(left, Math.Min(bl_test.X, tr_test.X));
                    top = Math.Max(top, Math.Max(bl_test.Y, tr_test.Y));
                    right = Math.Max(right, Math.Max(bl_test.X, tr_test.X));

                    Interlocked.Increment(ref counter);
                }
            );
            timer.Stop();

            bb[0] = new GeoLibPointF(left, bottom);
            bb[1] = new GeoLibPointF(left, top);
            bb[2] = new GeoLibPointF(right, top);
            bb[3] = new GeoLibPointF(right, bottom);
            GeoWrangler.close(bb);

            width = Math.Abs(right - left) + padding;
            height = Math.Abs(top - bottom) + padding;

            stitchingQuiltUI?.Invoke();
            // Move the non-0 patterns, try to create a reasonable grid.
            cols = (int)Math.Sqrt(patternCount);
            rows = (int)Math.Floor((double)patternCount / cols);

            updateUIProgress?.Invoke(0.0f);

            counter = 0;

            timer.Start();
            Parallel.For(0, patternCount, po, (entry) =>
                {
                    double x = width * (entry % cols);
                    int yCount = (int)Math.Floor((double)entry / cols);
                    double y = height * yCount;
                    Parallel.For(0, previewShapes[entry].Count, po, (ps) =>
                    {
                        previewShapes[entry][ps].move(x, y);
                    });
                    GeoLibPointF[] pBB = GeoWrangler.move(bb.ToArray(), x, y);
                    backgroundShapes[entry] = new PreviewShape();
                    backgroundShapes[entry].addPoints(pBB);
                    Interlocked.Increment(ref counter);

                }
            );
            timer.Stop();

            sw.Stop();
            doneQuiltUI?.Invoke(patternCount + " patterns in " + sw.Elapsed.TotalSeconds.ToString("0.00") + " s.");
        }
        catch (Exception)
        {
            // In case the evaluation is interrupted during an exit.
        }
        finally
        {
            timer.Stop();
            timer.Dispose();
            viewport?.Invoke();
            evaluating = false;
            Monitor.Exit(evalLock);
        }
    }

    public PointF findPattern(int index)
    {
        return pFindPattern(index);
    }

    private PointF pFindPattern(int index)
    {
        double x = width * (index % cols);
        int tmp = (int)Math.Floor((double)index / cols);
        double y = height * tmp;

        return new PointF((float)x, (float)y);
    }

    private List<List<PreviewShape>> pConsolidate()
    {
        List<List<PreviewShape>> consolidated = new();

        foreach (List<PreviewShape> t in previewShapes)
        {
            List<PreviewShape> previewShapesForPattern = new();
            previewShapesForPattern.AddRange(t);

            int previewShapesForPatternCount = previewShapesForPattern.Count;

            // Strip drawn elements to reduce confusion

            for (int element = previewShapesForPatternCount - 1; element >= 0; element--)
            {
                int polyCount = previewShapesForPattern[element].getPoints().Count;

                for (int poly = polyCount - 1; poly >= 0; poly--)
                {
                    try
                    {
                        if (previewShapesForPattern[element].getDrawnPoly(poly))
                        {
                            previewShapesForPattern[element].removePoly(poly);
                        }
                    }
                    catch (Exception)
                    {
                        // Had no drawn polygon, possibly due to not-configured layout case.
                    }
                }
            }

            for (int element = previewShapesForPatternCount - 1; element >= 0; element--)
            {
                int linkedElementIndex = previewShapesForPattern[element].linkedElementIndex;

                if (linkedElementIndex == -1)
                {
                    continue;
                }

                List<GeoLibPointF[]> geo = previewShapesForPattern[element].getPoints();
                for (int poly = 0; poly < geo.Count; poly++)
                {
                    previewShapesForPattern[linkedElementIndex].addPoints(geo[poly].ToArray(), false, text: previewShapesForPattern[element].isText(poly));
                    // Override the source index.
                    previewShapesForPattern[linkedElementIndex].sourceIndices[^1] = previewShapesForPattern[element].sourceIndices[poly];
                }

                previewShapesForPattern.RemoveAt(element);
            }
            consolidated.Add(previewShapesForPattern.ToList());
        }

        // We now need to crunch our unified polygons. Currently using a second state here for testing purposes.
        foreach (List<PreviewShape> t in consolidated)
        {
            foreach (PreviewShape t1 in t)
            {
                List<GeoLibPointF[]> geo = t1.getPoints().ToList();
                int geoCount = geo.Count;
                for (int poly = geoCount - 1; poly >= 0; poly--)
                {
                    // Avoid allowing text shapes to be considered for consolidation.
                    if (t1.isText(poly))
                    {
                        geo.RemoveAt(poly);
                    }
                    else
                    {
                        t1.removePoly(poly);
                    }
                }

                // Now consolidate our 'not drawn' elements. Drive some optional parameters to try and avoid losing keyholes.
                List<GeoLibPointF[]> c = GeoWrangler.clean_and_flatten(geo, CentralProperties.scaleFactorForOperation, customSizing: 2, extension: 1.0).ToList();

                foreach (GeoLibPointF[] t2 in c)
                {
                    t1.addPoints(t2, false);
                }
            }
        }

        return consolidated;
    }

    private List<string> pConsolidateNames(List<List<PreviewShape>> consolidated)
    {
        List<string> consolidated_elementNames = new();
        for (int i = 0; i < consolidated[0].Count; i++)
        {
            consolidated_elementNames.Add(patternElements[consolidated[0][i].elementIndex].getString(PatternElement.properties_s.name));
        }

        // Scan our element names to see if we have any duplicates.
        bool changed = true;
        const int startIndex = 0;
        while (changed)
        {
            changed = false;
            int elementsCount = consolidated_elementNames.Count;

            for (int index = startIndex; index < elementsCount; index++)
            {
                string name = consolidated_elementNames[index];

                for (int element = elementsCount - 1; element >= 0; element--)
                {
                    if (element == index)
                    {
                        continue;
                    }

                    if (consolidated_elementNames[element] != name)
                    {
                        continue;
                    }

                    // Duplicate name, need to merge the geometry.
                    foreach (List<PreviewShape> t in consolidated)
                    {
                        List<GeoLibPointF[]> geoFromSameNamedElement = t[element].getPoints().ToList();
                        for (int poly = 0; poly < geoFromSameNamedElement.Count; poly++)
                        {
                            if (!t[element].getDrawnPoly(poly))
                            {
                                t[index].addPoints(geoFromSameNamedElement[poly], false);
                            }
                        }

                        // Remove same-named pattern element.
                        t.RemoveAt(element);
                    }

                    // Remove our duplicate name.
                    consolidated_elementNames.RemoveAt(element);

                    changed = true;
                }
                if (changed)
                {
                    break;
                }
            }
        }

        return consolidated_elementNames;
    }

    private void pRegisterLayerNames(List<string> consolidated_elementNames)
    {
        for (int i = 0; i < consolidated_elementNames.Count; i++)
        {
            int targetLayer = i + 1;
            int targetDataType = 0;

            try
            {
                // Do we have a LxxxDxxx type layer name that we should use instead?
                string[] tokens = consolidated_elementNames[i].Split(new [] { 'D' });
                int dt = Math.Abs(Convert.ToInt32(tokens[1]));
                string token = tokens[0].Split(new [] { 'L' })[1];
                int lt = Math.Abs(Convert.ToInt32(token));

                targetLayer = lt;
                targetDataType = dt;
            }
            catch (Exception)
            {
                // Rely on a general exception to trap for invalid layer names mapping to LD target.
            }
            g.addLayerName("L" + targetLayer + "D" + targetDataType, consolidated_elementNames[i]);
        }
    }

    private void pUpdateDrawing(List<string> consolidated_elementNames, List<List<PreviewShape>> consolidated, int scale, int updateInterval)
    {
        double progress = 0;
        int consolidatedCount = consolidated.Count;
        drawing_.addCells(consolidatedCount);

        Parallel.For(0, consolidatedCount, po, (i) =>
        {
            drawing_.cellList[i] = new GCCell
            {
                accyear = (short) DateTime.Now.Year,
                accmonth = (short) DateTime.Now.Month,
                accday = (short) DateTime.Now.Day,
                acchour = (short) DateTime.Now.Hour,
                accmin = (short) DateTime.Now.Minute,
                accsec = (short) DateTime.Now.Second,
                modyear = (short) DateTime.Now.Year,
                modmonth = (short) DateTime.Now.Month,
                modday = (short) DateTime.Now.Day,
                modhour = (short) DateTime.Now.Hour,
                modmin = (short) DateTime.Now.Minute,
                modsec = (short) DateTime.Now.Second,
                cellName = "pattern" + i
            };


            GeoLibPointF loc = patterns[i].getPos();

            Parallel.For(0, consolidated[i].Count, po, (element) =>
            {
                // layer is 1-index based for output, so need to offset element value accordingly.
                int targetLayer = element + 1;
                int targetDataType = 0;

                int refElement = consolidated[i][element].elementIndex;
                if (consolidated[i][element].linkedElementIndex != -1)
                {
                    refElement = consolidated[i][element].linkedElementIndex;
                }

                if (patternElements[refElement].getInt(PatternElement.properties_i.layoutLayer) != -1)
                {
                    targetLayer = patternElements[refElement].getInt(PatternElement.properties_i.layoutLayer);
                }

                if (patternElements[refElement].getInt(PatternElement.properties_i.layoutDataType) != -1)
                {
                    targetDataType = patternElements[refElement].getInt(PatternElement.properties_i.layoutDataType);
                }

                try
                {
                    // Do we have a LxxxDxxx type layer name that we should use instead?
                    string[] tokens = consolidated_elementNames[element].Split(new [] { 'D' });
                    int dt = Math.Abs(Convert.ToInt32(tokens[1]));
                    string token = tokens[0].Split(new [] { 'L' })[1];
                    int lt = Math.Abs(Convert.ToInt32(token));

                    targetLayer = lt;
                    targetDataType = dt;
                }
                catch (Exception)
                {
                    // Rely on a general exception to trap for invalid layer names mapping to LD target.
                }

                // Might not have the LD registered. Add it, just in case.
                try
                {
                    string unused = g.getLayerNames()["L" + targetLayer + "D" + targetDataType];
                }
                catch (Exception)
                {
                    try
                    {
                        g.addLayerName("L" + targetLayer + "D" + targetDataType, consolidated_elementNames[element]);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                List<GeoLibPointF[]> polys = consolidated[i][element].getPoints().ToList();
                for (int poly = 0; poly < polys.Count; poly++)
                {
                    GeoLibPoint[] ePoly = GeoWrangler.resize_to_int(polys[poly], scale);

                    ePoly = GeoWrangler.simplify(ePoly);
                    ePoly = GeoWrangler.stripColinear(ePoly);

                    drawing_.cellList[i].addPolygon(ePoly.ToArray(), targetLayer, targetDataType);

                    if (!consolidated[i][element].isText(poly))
                    {
                        continue;
                    }

                    // Get midpoint of geometry.
                    GeoLibPointF bb = GeoWrangler.midPoint(polys[poly]);
                    // We should only have one polygon here, so naively assume that.
                    // Pin text coming from the element variable for now.
                    string pinName = patternElementNames[consolidated[i][element].sourceIndices[poly]];
                    drawing_.cellList[i].addText(targetLayer, targetDataType, new GeoLibPoint((int)((bb.X - loc.X) * scale), (int)((bb.Y - loc.Y) * scale)), pinName);
                }
            });


            if (i % updateInterval != 0)
            {
                return;
            }

            updateUIProgress?.Invoke(progress);
            progress += 0.01;
        });
    }

    private void pBuildQuilt(int scale, int updateInterval)
    {
        double progress = 0;
        //bool mirror_x = false;
        GCCell gcell_root = drawing_.addCell();

        gcell_root.addCellrefs(patterns.Count);

        Parallel.For(0, patterns.Count, po, (i) =>
        {
            GeoLibPointF loc = patterns[i].getPos();
            gcell_root.elementList[i] = new GCCellref();
            gcell_root.elementList[i].setPos(new GeoLibPoint(loc.X * scale, loc.Y * scale));

            gcell_root.elementList[i].setCellRef(drawing_.cellList[i]);
            gcell_root.elementList[i].setName("pattern" + i);
            gcell_root.elementList[i].rotate(0);
            gcell_root.elementList[i].scale(1);
            /*
            if (mirror_x)
            {
                gcell_root.elementList[i].setMirrorx();
            }
            */
            if (i % updateInterval != 0)
            {
                return;
            }

            updateUIProgress?.Invoke(progress);
            progress += 0.01;
        });

        g.setDrawing(drawing_);
        g.setValid(true);
    }

    private void pInitQuilt(int scale)
    {
        g = new GeoCore();
        g.reset();
        drawing_ = new GCDrawingfield("")
        {
            accyear = (short) DateTime.Now.Year,
            accmonth = (short) DateTime.Now.Month,
            accday = (short) DateTime.Now.Day,
            acchour = (short) DateTime.Now.Hour,
            accmin = (short) DateTime.Now.Minute,
            accsec = (short) DateTime.Now.Second,
            modyear = (short) DateTime.Now.Year,
            modmonth = (short) DateTime.Now.Month,
            modday = (short) DateTime.Now.Day,
            modhour = (short) DateTime.Now.Hour,
            modmin = (short) DateTime.Now.Minute,
            modsec = (short) DateTime.Now.Second,
            databaseunits = 1000 * scale,
            userunits = 0.001 / scale,
            libname = "quilt"
        };

        // Set our parallel task options based on user settings.
        po = new ParallelOptions();
    }

    public void toGeoCore(int type, string file)
    {
        pToGeoCore(type, file);
    }

    private void pToGeoCore(int type, string file)
    {
        Monitor.Enter(exportLock);
        Stopwatch sw = new();
        try
        {
            sw.Reset();
            sw.Start();
            indeterminateQuiltUI?.Invoke("Saving", "Saving");

            const int scale = 100; // for 0.01 nm resolution
            pInitQuilt(scale);

            // Decomposition means that we need to unify output.
            List<List<PreviewShape>> consolidated = pConsolidate();

            // Get a list of our current element names
            List<string> consolidated_elementNames = pConsolidateNames(consolidated);

            // Register layer names with geoCore.
            pRegisterLayerNames(consolidated_elementNames);

            // The quilt is already consistent with the UI, so we can use it without a rebuild.
            updateUIStatus?.Invoke("Weaving");
            updateUIProgress?.Invoke(0);

            // Set to 1 to avoid problems if there are fewer than 100 patterns.
            int updateInterval = Math.Max(1, patterns.Count / 100);
            pUpdateDrawing(consolidated_elementNames, consolidated, scale, updateInterval);

            // Now build the quilt.
            updateUIStatus?.Invoke("Stitching");
            updateUIProgress?.Invoke(0);
            pBuildQuilt(scale, updateInterval);

            indeterminateQuiltUI?.Invoke("Saving Quilt", "Saving");

            switch (type)
            {
                case (int)GeoCore.fileType.gds:
                    gds.gdsWriter gw = new(g, file)
                    {
                        statusUpdateUI = pStatusUIWrapper,
                        progressUpdateUI = pProgressUIWrapper
                    };
                    gw.save();
                    break;
                case (int)GeoCore.fileType.oasis:
                    oasis.oasWriter ow = new(g, file)
                    {
                        statusUpdateUI = pStatusUIWrapper,
                        progressUpdateUI = pProgressUIWrapper
                    };
                    ow.save();
                    break;
            }

            pToCSV(file + ".csv");
        }
        finally
        {
            sw.Stop();
            doneQuiltUI?.Invoke("Done in " + sw.Elapsed.TotalSeconds.ToString("0.00") + " s.");
            Monitor.Exit(exportLock);
        }
    }

    public void toCSV(string filename)
    {
        pToCSV(filename);
    }

    private void pToCSV(string filename)
    {
        FileStream csvFile = File.OpenWrite(filename);
        StreamWriter sw = new(csvFile);

        sw.WriteLine(CentralProperties.productName + " " + CentralProperties.version);
        string out_ = left + ",";
        out_ += bottom + ",";
        out_ += width + ",";
        out_ += height + ",";
        out_ += rows + ",";
        out_ += cols + ",";
        out_ += padding;

        sw.WriteLine(out_);

        string[] descriptions = new string[patterns.Count];
#if !QUILTSINGLETHREADED
        Parallel.For(0, descriptions.Length, po, (p) =>
#else
            for (int p = 0; p < patterns.Count; p++)
#endif
            {
                descriptions[p] = patterns[p].getDescription();
            }
#if !QUILTSINGLETHREADED
        );
#endif
        foreach (string t in descriptions)
        {
            sw.WriteLine(t);
        }

        sw.Close();
        csvFile.Close();
        sw.Dispose();
        csvFile.Dispose();
    }
}