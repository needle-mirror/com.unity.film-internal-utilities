using System;
using System.Collections.Generic;
using JetBrains.Annotations;


namespace Unity.FilmInternalUtilities {

internal static class EnumerableExtensions {

    //Returns -1 if not found
    internal static int FindIndex<T>(this IEnumerable<T> collection, T elementToFind) {
        int i = 0;
        using var enumerator = collection.GetEnumerator();
        while (enumerator.MoveNext()) {
            T obj = enumerator.Current;
            if (null != obj && obj.Equals(elementToFind)) {
                return i;
            }
            ++i;
        }
        
        return -1;
    }
    
    //Returns false with ret set to default(T) if not found
    internal static bool FindElementAt<T>(this IEnumerable<T> collection, int index, out T ret) {

        int i = 0;
        using var enumerator = collection.GetEnumerator();
        while (enumerator.MoveNext()) {
            if (i == index) {
                ret = enumerator.Current;
                return true;
            }
            ++i;
        }

        ret = default(T);
        return false;
    }
    
    internal static void Loop<T>(this IEnumerable<T> collection, Action<T> eachAction) {
        
        using (var enumerator = collection.GetEnumerator()) {
            while (enumerator.MoveNext()) {
                eachAction(enumerator.Current);
            }
        }
    }
}
} //end namespace