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

namespace Quilt
{
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

        bool evaluating;
        object exportLock;
        object evalLock;

        int mode = 0; // 0 is hashset, 1 is parallelFor, 2 is distinct, 3 is distinctparallelfor

        int cols, rows;

        double width, height, left, bottom;

        double padding; // will pad the bounding box by defined amount, e.g. 0.1 to pad by 10% of width and height.

        int showInput;

        int counter;
        int max;

        // Quilt will hold the list of patterns
        public ObservableCollection<string> patternElementNames { get; set; }
        public ObservableCollection<string> patternElementNames_filtered { get; set; } // screens out active layer and adds 'World Origin' at top of the list. Helper function is available to get index of layer.
        public ObservableCollection<string> patternElementNames_filtered_array { get; set; }

        List<Pattern> patterns;
        HashSet<int> hashes;
        List<PatternElement> patternElements; // our pattern elements from which patterns will be constructed.
        public List<PreviewShape>[] previewShapes { get; set; }
        public PreviewShape[] backgroundShapes { get; set; }
        QuiltContext quiltContext;
        PatternElement copyBuffer;

        System.Timers.Timer timer;

        void timerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            updateUIProgress?.Invoke((double)counter / max);
        }

        void statusUIWrapper(string text)
        {
            updateUIStatus?.Invoke(text);
        }

        void progressUIWrapper(double val)
        {
            updateUIProgress?.Invoke(val);
        }

        public int getPatternCount()
        {
            return pGetPatternCount();
        }

        int pGetPatternCount()
        {
            return patterns.Count;
        }

        public Stitcher(ref QuiltContext context)
        {
            init(ref context);
        }

        void init(ref QuiltContext context)
        {
            exportLock = new object();
            evalLock = new object();

            patternElementNames = new ObservableCollection<string>();
            patternElementNames_filtered = new ObservableCollection<string>();
            patternElementNames_filtered_array = new ObservableCollection<string>();
            quiltContext = context;

            copyBuffer = null;

            reset();
        }

        public bool isCopySet()
        {
            return pIsCopySet();
        }

        bool pIsCopySet()
        {
            return copyBuffer != null;
        }

        public void setCopy(int index)
        {
            pSetCopy(index);
        }

        void pSetCopy(int index)
        {
            copyBuffer = new PatternElement(patternElements[index]);
        }

        public void paste(int index)
        {
            pPaste(index);
        }

        void pPaste(int index)
        {
            if (pIsCopySet())
            {
                patternElements[index] = new PatternElement(copyBuffer);
                pUpdateQuilt();
            }
        }

        void pAddPattern(Pattern pattern)
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

        void pCleanup()
        {
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
        }

        void pAddPattern_(Pattern pattern)
        {
            patterns.Add(pattern);
        }

        void pAddPattern_hashset(Pattern pattern)
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
                bool add = true;
                ParallelOptions pco = new ParallelOptions();
                // Attempt at parallelism.
                CancellationTokenSource pcs = new CancellationTokenSource();
                CancellationToken pct = pcs.Token;                
                // Try to thread it for best possible performance.
                Parallel.For(0, patterns.Count(), pco, (p, loopState) =>
                {
                    // If we find an equivalent pattern, we need to abort.
                    if (patterns[p] == pattern)
                    {
                        Interlocked.Equals(add, false);
                        pcs.Cancel();
                    }
                });
                if (add)
                {
                    patterns.Add(pattern);
                }
            }
        }

        class PatternComparer : IEqualityComparer<Pattern>
        {
            // Products are equal if their names and product numbers are equal.
            public bool Equals(Pattern x, Pattern y)
            {
                return pEquals(x, y);
            }

            bool pEquals(Pattern x, Pattern y)
            {
                // Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                // Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

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

            int pGetHashCode(Pattern x)
            {
                // Check whether the object is null.
                if (Object.ReferenceEquals(x, null)) return 0;

                // Get the hash code for the instance if it is not null.
                return x.GetHashCode();
            }
        }

        void pCleanup_distinctParallel()
        {
            indeterminateQuiltUI?.Invoke("Clean-up", "Clean-up");
            ParallelOptions po = new ParallelOptions();
            int threads = po.MaxDegreeOfParallelism;
            var clean_ = patterns.Distinct(new PatternComparer()).AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).WithDegreeOfParallelism(threads);
            patterns = clean_.ToList();
        }

        void pCleanup_distinct()
        {
            indeterminateQuiltUI?.Invoke("Clean-up", "Clean-up");
            var clean_ = patterns.Distinct(new PatternComparer());
            patterns = clean_.ToList();
        }

        void pCleanup_parallelFor()
        {
            List<Pattern> clean = new List<Pattern>();
            List<int> cleanHash = new List<int>();

            // Make use of threading here to reduce overhead in large patterns.
            // Set our parallel task options based on user settings.
            ParallelOptions po = new ParallelOptions();
            // Attempt at parallelism.
            CancellationTokenSource cancelSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancelSource.Token;

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
                    add = Interlocked.Equals(add, !(pHC == cleanHash[p]));

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

        public Pattern getPattern(int index)
        {
            return patterns[index];
        }

        public void reset(bool empty = false)
        {
            pReset(empty);
        }

        void pReset(bool empty)
        {
            previewShapes = new List<PreviewShape>[0];
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
            patternElementNames_filtered_array.Clear();
            if (!empty)
            {
                update_filteredPatternedElementNames(0);
            }
            previewShapes = new List<PreviewShape>[0];

            padding = 0;

            showInput = 1;

            if (!empty)
            {
                pUpdateQuilt();
            }
        }

        public void setPadding(double value)
        {
            pSetPadding(value);
        }

        void pSetPadding(double value)
        {
            padding = value;
            // updateQuilt();
        }

        public double getPadding()
        {
            return pGetPadding();
        }

        double pGetPadding()
        {
            return padding;
        }

        public void setShowInput(int value)
        {
            pSetShowInput(value);
        }

        void pSetShowInput(int value)
        {
            showInput = value;
            // updateQuilt();
        }

        public int getShowInput()
        {
            return pGetShowInput();
        }

        int pGetShowInput()
        {
            return showInput;
        }

        public void update_filteredPatternedElementNames(int selectedEntry)
        {
            pUpdate_filteredPatternedElementNames(selectedEntry);
        }

        void pUpdate_filteredPatternedElementNames(int selectedEntry)
        {
            patternElementNames_filtered.Clear();
            patternElementNames_filtered.Add("World Origin");

            patternElementNames_filtered_array.Clear();
            patternElementNames_filtered_array.Add("Self");

            if (selectedEntry == -1)
            {
                return;
            }

            for (int i = 0; i < patternElementNames.Count; i++)
            {
                // Avoid offering current element, and any bounding elements.
                if ((i == selectedEntry) || (patternElements[i].getInt(PatternElement.properties_i.shapeIndex) == (int)CommonVars.shapeNames.bounding))
                {
                    continue;
                }

                patternElementNames_filtered.Add(patternElementNames[i]);
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

        void pAddPatternElements(List<PatternElement> p)
        {
            patternElements = p.ToList();
            patternElementNames.Clear();
            for (int i = 0; i < patternElements.Count; i++)
            {
                patternElementNames.Add(patternElements[i].getString(PatternElement.properties_s.name));
            }

            pUpdate_filteredPatternedElementNames(0);
        }

        public void addPatternElement(string name)
        {
            pAddPatternElement(name);
        }

        void pAddPatternElement(string name)
        {
            patternElementNames.Add(name);
            patternElements.Add(new PatternElement());
            patternElementNames_filtered.Add(name);
            pUpdateQuilt();
        }

        public void renamePatternElement(int index, string name)
        {
            pRenamePatternElement(index, name);
        }

        void pRenamePatternElement(int index, string name)
        {
            patternElements[index].setString(PatternElement.properties_s.name, name);
            patternElementNames[index] = name;
            pUpdate_filteredPatternedElementNames(index);
        }

        public void removePatternElement(int index)
        {
            pRemovePatternElement(index);
        }

        void pRemovePatternElement(int index)
        {
            /*
            try
            {
                // We need to do a look up of the index here because we are not sure where the string is in the filtered collection.
                patternElementNames_filtered.RemoveAt(patternElementNames_filtered.IndexOf(patternElementNames[index]));
            }
            catch (Exception)
            {
                // Ignore any exception in case the sought item isn't in the filtered list. No effect on the users.
            }
            */

            // We need to adjust any other elements in the pattern that have references to the element being removed.
#if QUILTTHREADED
            Parallel.For(0, patternElements.Count, (i) =>
#else
            for (int i = 0; i < patternElements.Count; i++)
#endif
            {
                if (i != index)
                {
                    int xRef = patternElements[i].getInt(PatternElement.properties_i.xPosRef);
                    if (xRef == index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.xPosRef, 0); // global reference as the reference layer will be removed.
                    }
                    else if (xRef > index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.xPosRef, xRef - 1); // decrement the reference as the reference layer below this point is being removed.
                    }

                    int yRef = patternElements[i].getInt(PatternElement.properties_i.yPosRef);
                    if (yRef == index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.yPosRef, 0); // global reference as the reference layer will be removed.
                    }
                    else if (yRef > index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.yPosRef, yRef - 1); // decrement the reference as the reference layer below this point is being removed.
                    }

                    int xSSRef = patternElements[i].getInt(PatternElement.properties_i.xPosSubShapeRef);
                    if (xSSRef == index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.xPosSubShapeRef, 0); // global reference as the reference layer will be removed.
                    }
                    else if (xSSRef > index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.xPosSubShapeRef, xSSRef - 1); // decrement the reference as the reference layer below this point is being removed.
                    }

                    int ySSRef = patternElements[i].getInt(PatternElement.properties_i.yPosSubShapeRef);
                    if (ySSRef == index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.yPosSubShapeRef, 0); // global reference as the reference layer will be removed.
                    }
                    else if (ySSRef > index)
                    {
                        patternElements[i].setInt(PatternElement.properties_i.yPosSubShapeRef, ySSRef - 1); // decrement the reference as the reference layer below this point is being removed.
                    }
                }
            }
#if QUILTTHREADED
            );
#endif
            patternElementNames.RemoveAt(index);
            patternElements.RemoveAt(index);
            pUpdateQuilt();
        }

        public PatternElement getPatternElement(int patternIndex, int index)
        {
            return pGetPatternElement(patternIndex, index);
        }

        PatternElement pGetPatternElement(int patternIndex, int index)
        {
            if (patternIndex == 0) // reference pattern
            {
                return patternElements[index];
            }
            else // querying a specific pattern for its elememt.
            {
                return patterns[patternIndex].getPatternElement(index);
            }
        }

        public List<PatternElement> getPatternElements(int pattern)
        {
            return pGetPatternElements(pattern);
        }

        List<PatternElement> pGetPatternElements(int pattern)
        {
            return patterns[pattern].getPatternElements();
        }

        void clearPatterns()
        {
            patterns.Clear();
            if (mode == 0)
            {
                hashes.Clear();
            }
        }

        public void updateQuilt()
        {
            pUpdateQuilt();
        }

        void pUpdateQuilt()
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
                clearPatterns();

                if (patternElements.Count == 0)
                {
                    doneQuiltUI?.Invoke("No pattern elements defined. Nothing to do.");
                    return;
                }

                Stopwatch sw = new Stopwatch();
                indeterminateQuiltUI?.Invoke("Generating Quilt", "Generating");
                sw.Reset();
                sw.Start();

                timer = new System.Timers.Timer();
                // Set up timers for the UI refresh
                timer.AutoReset = true;
                timer.Interval = CentralProperties.timer_interval;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timerElapsed);

                // Get our total variant count for all elements
                long variantCount = 1;
                long variant = 1;

                variantCount = patternElements[0].getInt(PatternElement.properties_i.maxVariants);

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

                PatternElement pattEl = new PatternElement(patternElements[0]); // get our base pattern first element
                string pattName = pattEl.getString(PatternElement.properties_s.name);

                updateUIStatus?.Invoke(pattName);

                // Get the first variant from the first pattern element.
                PatternElement varEl = pattEl.getNextVariant();

                double progress = 0;

                generatingPatternUI?.Invoke();

                while (varEl != null)
                {
                    // For each variant, add a new pattern
                    Pattern newPattern = new Pattern(ref quiltContext, new List<PatternElement>() { new PatternElement(varEl) });

                    pAddPattern(newPattern);

                    varEl = pattEl.getNextVariant();

                    if ((variant % variantCount) == 0)
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
                while (level < patternElements.Count())
                {
                    List<Pattern> oldPatterns = patterns.ToList();
                    clearPatterns();

                    pattEl = new PatternElement(patternElements[level]);

                    pattName = pattEl.getString(PatternElement.properties_s.name);

                    updateUIStatus?.Invoke(pattName);

                    for (int pattern = 0; pattern < oldPatterns.Count; pattern++)
                    {
                        varEl = pattEl.getNextVariant();

                        while (varEl != null)
                        {
                            // Get our upper level element definitions for this pattern.
                            List<PatternElement> pattElements = oldPatterns[pattern].getPatternElements().ToList();

                            // Add our new pattern element to the list for the pattern.
                            pattElements.Add(varEl);

                            // Add the pattern variant to the quilt.
                            pAddPattern(new Pattern(ref quiltContext, pattElements.ToList()));

                            varEl = pattEl.getNextVariant();

                            if ((variant % variantCount) == 0)
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

                // Generate the preview shapes
                previewShapes = new List<PreviewShape>[0];
                backgroundShapes = new PreviewShape[0];
                if (patterns.Count == 0)
                {
                    doneQuiltUI?.Invoke("");
                    return;
                }

                int patternCount = patterns.Count;
                int progressChunk = patternCount / 100;

                updateUIProgress?.Invoke(0.0f);

                if (progressChunk < 1)
                {
                    progressChunk = 1;
                }

                // Get our bounding box information to inform grid placement.
                List<GeoLibPointF> bb = new List<GeoLibPointF>();
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
                GeoLibPointF bl = new GeoLibPointF(bb[0]);
                GeoLibPointF tr = new GeoLibPointF(bb[2]);

                double right = Math.Max(bl.X, tr.X);
                left = Math.Min(bl.X, tr.X);

                double top = Math.Max(bl.Y, tr.Y);
                bottom = Math.Min(bl.Y, tr.Y);

                updateUIStatus?.Invoke("Weaving");

                previewShapes = new List<PreviewShape>[patternCount];
                backgroundShapes = new PreviewShape[patternCount];

                ParallelOptions po = new ParallelOptions();

                counter = 0;
                max = patternCount;

                timer.Start();
                Parallel.For(0, patternCount, po, (pattern, loopState) =>
                {
                    previewShapes[pattern] = patterns[pattern].generate_shapes().ToList();

                    bb = patterns[pattern].boundingBox().getPoints();

                    GeoLibPointF bl_test = new GeoLibPointF(bb[0]);
                    GeoLibPointF tr_test = new GeoLibPointF(bb[2]);

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
                Parallel.For(0, patternCount, po, (entry, loopState) =>
                {
                    double x = width * (entry % cols);
                    int yCount = (int)Math.Floor((double)entry / cols);
                    double y = height * yCount;
                    Parallel.For(0, previewShapes[entry].Count, po, (ps, loopState2) =>
                    {
                        previewShapes[entry][ps].move(x, y);
                    });
                    // patterns[entry].setPos(x, y);
                    GeoLibPointF[] pBB = GeoWrangler.move(bb.ToArray(), x, y);
                    backgroundShapes[entry] = new PreviewShape();
                    backgroundShapes[entry].addPoints(pBB); Interlocked.Increment(ref counter);
                    /*
                    if ((c % progressChunk) == 0)
                    {
                        updateUIProgress?.Invoke(c / patternCount);
                    }
                    */
                }
                );
                timer.Stop();

                sw.Stop();
                doneQuiltUI?.Invoke(patternCount.ToString() + " patterns in " + sw.Elapsed.TotalSeconds.ToString("0.00") + " s.");
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

        PointF pFindPattern(int index)
        {
            double x = width * (index % cols);
            int tmp = (int)Math.Floor((double)index / cols);
            double y = height * tmp;

            return new PointF((float)x, (float)y);
        }

        public void toGeoCore(int type, string file)
        {
            pToGeoCore(type, file);
        }

        void pToGeoCore(int type, string file)
        {
            Monitor.Enter(exportLock);
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Reset();
                sw.Start();
                indeterminateQuiltUI?.Invoke("Saving", "Saving");
                int scale = 100; // for 0.01 nm resolution
                GeoCore g = new GeoCore();
                g.reset();
                GCDrawingfield drawing_ = new GCDrawingfield("");
                drawing_.accyear = (short)DateTime.Now.Year;
                drawing_.accmonth = (short)DateTime.Now.Month;
                drawing_.accday = (short)DateTime.Now.Day;
                drawing_.acchour = (short)DateTime.Now.Hour;
                drawing_.accmin = (short)DateTime.Now.Minute;
                drawing_.accsec = (short)DateTime.Now.Second;
                drawing_.modyear = (short)DateTime.Now.Year;
                drawing_.modmonth = (short)DateTime.Now.Month;
                drawing_.modday = (short)DateTime.Now.Day;
                drawing_.modhour = (short)DateTime.Now.Hour;
                drawing_.modmin = (short)DateTime.Now.Minute;
                drawing_.modsec = (short)DateTime.Now.Second;
                drawing_.databaseunits = 1000 * scale;
                drawing_.userunits = 0.001 / scale;
                drawing_.libname = "quilt";

                // Register layer names with geoCore.
                for (int i = 0; i < patternElementNames.Count; i++)
                {
                    g.addLayerName("L" + (i + 1).ToString() + "D0", patternElementNames[i]);
                }

                // The quilt is already consistent with the UI, so we can use it without a rebuild.
                // Set to 1 to avoid problems if there are fewer than 100 patterns.
                int updateInterval = Math.Max(1, patterns.Count / 100);

                updateUIStatus?.Invoke("Weaving");
                double progress = 0;
                updateUIProgress?.Invoke(progress);

                // Set our parallel task options based on user settings.
                ParallelOptions po = new ParallelOptions();

                drawing_.addCells(previewShapes.Length);

                progress = 0;

                Parallel.For(0, previewShapes.Length, po, (i, loopState) =>
                {
                    drawing_.cellList[i] = new GCCell();
                    drawing_.cellList[i].accyear = (short)DateTime.Now.Year;
                    drawing_.cellList[i].accmonth = (short)DateTime.Now.Month;
                    drawing_.cellList[i].accday = (short)DateTime.Now.Day;
                    drawing_.cellList[i].acchour = (short)DateTime.Now.Hour;
                    drawing_.cellList[i].accmin = (short)DateTime.Now.Minute;
                    drawing_.cellList[i].accsec = (short)DateTime.Now.Second;
                    drawing_.cellList[i].modyear = (short)DateTime.Now.Year;
                    drawing_.cellList[i].modmonth = (short)DateTime.Now.Month;
                    drawing_.cellList[i].modday = (short)DateTime.Now.Day;
                    drawing_.cellList[i].modhour = (short)DateTime.Now.Hour;
                    drawing_.cellList[i].modmin = (short)DateTime.Now.Minute;
                    drawing_.cellList[i].modsec = (short)DateTime.Now.Second;

                    drawing_.cellList[i].cellName = "pattern" + i.ToString();

                    GeoLibPointF loc = patterns[i].getPos();

                    //for (int element = 0; element < previewShapes[i].Count; element++)
                    Parallel.For(0, previewShapes[i].Count, po, (element, loopState2) =>
                    {
                        List<GeoLibPointF[]> polys = previewShapes[i][element].getPoints();
                        for (int poly = 0; poly < polys.Count; poly++)
                        {
                            // No drawn polygons desired.
                            if (!previewShapes[i][element].getDrawnPoly(poly))
                            {
                                GeoLibPoint[] ePoly = GeoWrangler.resize_to_int(polys[poly], scale);

                                ePoly = GeoWrangler.simplify(ePoly);

                                drawing_.cellList[i].addPolygon(ePoly.ToArray(), element + 1, 0); // layer is 1-index based for output, so need to offset element value accordingly.

                                if (previewShapes[i][element].isText(poly))
                                {
                                    // Get midpoint of geometry.
                                    GeoLibPointF bb = GeoWrangler.midPoint(polys[poly]);
                                    // We should only have one polygon here, so naively assume that.
                                    // Pin text coming from the element variable for now.
                                    string pinName = patternElementNames[element];
                                    drawing_.cellList[i].addText(element + 1, 0, new GeoLibPoint((int)((bb.X - loc.X) * scale), (int)((bb.Y - loc.Y) * scale)), pinName);
                                }
                            }
                        }
                    });


                    if (i % updateInterval == 0)
                    {
                        updateUIProgress?.Invoke(progress);
                        progress += 0.01;
                    }
                });

                // Now build the quilt.
                bool mirror_x = false;
                GCCell gcell_root = drawing_.addCell();

                updateUIStatus?.Invoke("Stitching");
                progress = 0;
                updateUIProgress?.Invoke(progress);

                gcell_root.addCellrefs(patterns.Count);

                // for (int i = 0; i < patterns.Count; i++)
                Parallel.For(0, patterns.Count, po, (i, loopState) =>
                {
                    GeoLibPointF loc = patterns[i].getPos();
                    gcell_root.elementList[i] = new GCCellref();
                    gcell_root.elementList[i].setPos(new GeoLibPoint(loc.X * scale, loc.Y * scale));
                    /*
                    gcell_root.elementList[i].setCellRef(drawing_.findCell("pattern" + i.ToString()));
                    */
                    gcell_root.elementList[i].setCellRef(drawing_.cellList[i]); // drawing_.findCell("pattern" + i.ToString()));
                    gcell_root.elementList[i].setName("pattern" + i.ToString());
                    gcell_root.elementList[i].rotate(0);
                    gcell_root.elementList[i].scale(1);
                    if (mirror_x)
                    {
                        gcell_root.elementList[i].setMirrorx();
                    }
                    if (i % updateInterval == 0)
                    {
                        updateUIProgress?.Invoke(progress);
                        progress += 0.01;
                    }
                });

                g.setDrawing(drawing_);
                g.setValid(true);

                indeterminateQuiltUI?.Invoke("Saving Quilt", "Saving");

                switch (type)
                {
                    case (int)GeoCore.fileType.gds:
                        gds.gdsWriter gw = new gds.gdsWriter(g, file);
                        gw.statusUpdateUI = statusUIWrapper;
                        gw.progressUpdateUI = progressUIWrapper;
                        gw.save();
                        break;
                    case (int)GeoCore.fileType.oasis:
                        oasis.oasWriter ow = new oasis.oasWriter(g, file);
                        ow.statusUpdateUI = statusUIWrapper;
                        ow.progressUpdateUI = progressUIWrapper;
                        ow.save();
                        break;
                }
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

        void pToCSV(string filename)
        {
            FileStream csvFile = File.OpenWrite(filename);
            StreamWriter sw = new StreamWriter(csvFile);

            sw.WriteLine(CentralProperties.productName + " " + CentralProperties.version.ToString());
            string out_ = left.ToString() + ",";
            out_ += bottom.ToString() + ",";
            out_ += width.ToString() + ",";
            out_ += height.ToString() + ",";
            out_ += rows.ToString() + ",";
            out_ += cols.ToString();

            sw.WriteLine(out_);

            out_ = "";
            List<string> descriptions = new List<string>();
            for (int p = 0; p < patterns.Count; p++)
            {
                out_ += patterns[p].getDescription();
                if (p != patterns.Count - 1)
                {
                    out_ += ",";
                }
                if ((p != 0) && (p % cols == 0))
                {
                    descriptions.Add(out_);
                    out_ = "";
                }
            }

            for (int line = 0; line < descriptions.Count; line++)
            {
                string ln = descriptions[line];
                if (line != descriptions.Count - 1)
                {
                    ln += ",";
                }
                sw.WriteLine(ln);
            }

            sw.Close();
            csvFile.Close();
            sw.Dispose();
            csvFile.Dispose();
        }
    }
}
