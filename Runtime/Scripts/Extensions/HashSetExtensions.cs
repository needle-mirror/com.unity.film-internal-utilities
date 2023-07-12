using System;
using System.Collections.Generic;

namespace Unity.FilmInternalUtilities {

internal static class HashSetExtensions {
    
    internal static void Loop<T>(this HashSet<T> collection, Action<T> eachAction) {
        
        using (var enumerator = collection.GetEnumerator()) {
            while (enumerator.MoveNext()) {
                eachAction(enumerator.Current);
            }
        }
    }
}
} //end namespace