using System;
using System.Collections.Generic;


namespace Unity.FilmInternalUtilities {

internal static class DictionaryExtensions {

    internal static void Loop<K,V>(this Dictionary<K,V> collection, Action<K,V> eachAction) {
        using (var enumerator = collection.GetEnumerator()) {
            while (enumerator.MoveNext()) {
                var kv = enumerator.Current;
                eachAction(kv.Key, kv.Value);
            }
        }
    }

}
} //end namespace