using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Unity.FilmInternalUtilities {

internal static class ListExtensions {
    
    internal static void RemoveNullMembers<T>(this IList<T> list) {
        for (int i = list.Count-1; i >= 0 ; --i) {
            if (null != list[i])
                continue;
            
            list.RemoveAt(i);
        }
    }
    
    internal static void Move<T>(this List<T> list, int oldIndex, int newIndex) {
        if (oldIndex == newIndex)
            return;
            
        T item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }

    internal static bool AreElementsEqual<T>(this List<T> list, IList<T> otherList) {
        if (null == otherList || list.Count != otherList.Count)
            return false;

        int numElements = list.Count;
        for (int i = 0; i < numElements; ++i) {
            if (!list[i].Equals(otherList[i]))
                return false;
        }        

        return true;

    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    internal static void Loop<T>(this IList<T> list, Action<T> eachAction) {
        int numElements = list.Count;
        for (int i = 0; i < numElements; ++i) {
            eachAction(list[i]);
        }
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    internal static void SetCount<T>(this List<T> list, int reqCount) {
        Assert.IsTrue(reqCount >= 0);
        int curCount = list.Count;

        while (list.Count < reqCount) {
            list.Add(default(T));
        }

        if (curCount > reqCount) {
            list.RemoveRange(reqCount, curCount - reqCount);
        }
    }   
}

} //end namespace

