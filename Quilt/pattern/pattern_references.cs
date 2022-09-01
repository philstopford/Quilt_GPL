using System.Collections.Generic;

namespace Quilt;

public partial class Pattern
{
    private bool pCyclicalCheck(int cRef, PatternElement.properties_i prop, int subshape = -1)
    {
        return pCyclicalCheck_int(cRef, prop, subshape) != -1;
    }

    private int pCyclicalCheck_int(int cRef, PatternElement.properties_i prop, int subshape = -1)
    {
        List<int> cRefs = new() {cRef};

        bool cyclical = false;
        int collisionIndex = -1;

        while (cRef >= 0 && !cyclical)
        {
            cRef = pGetRef(cRef, prop, subshape);
            if (cRefs.IndexOf(cRef) == -1)
            {
                cRefs.Add(cRef);
            }
            else
            {
                cyclical = true;
                collisionIndex = cRef;
            }
        }

        return collisionIndex;
    }
        
    public int getRef(int elementIndex, PatternElement.properties_i prop, int subshape = -1)
    {
        return pGetRef(elementIndex, prop, subshape);
    }

    private int pGetRef(int elementIndex, PatternElement.properties_i prop, int subshape = -1)
    {
        int tmp = pGetPatternElement(elementIndex).getInt(prop, subshape) - 1;
        
        /* Above, tmp == 0 means the world reference. We decrement the value by 1 to compensate.

         However, the active layer isn't in our list of reference layers. This causes trouble now because we need to detect and handle this.
         Consider the active layer as '1', the list is then (world, 0, 2, 3, 4) as (0th, 1st, 2nd, 3rd, 4th members).

         Decrementing the index means we have :
         -1 => world
          0 => 0
          1 => 2
          2 => 3
          3 => 4

         To compensate, if the reduced layer index is equal to, or more than the active index, we should increase the value by 1 to sort the look-up out.
         */
        if (tmp >= elementIndex)
        {
            tmp++;
        }
        return tmp;
    }
        
    public bool cyclicalCheck(int cRef, PatternElement.properties_i prop, int subshape = -1)
    {
        return pCyclicalCheck(cRef, prop, subshape);
    }
}