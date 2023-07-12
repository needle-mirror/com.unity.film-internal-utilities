using System.Collections.Generic;
using UnityEngine;

namespace Unity.FilmInternalUtilities {

internal static class TransformExtensions {

    internal static Transform FindOrCreateChild(this Transform t, string childName, bool worldPositionStays = true) {
        
        Transform childT = t.Find(childName);
        if (null != childT)
            return childT;
                
        GameObject go = new GameObject(childName);
        childT = go.transform;
        childT.SetParent(t, worldPositionStays);
        return childT;
    }
    

    internal static void SetParent(this ICollection<Transform> collection, Transform parent) {
        collection.Loop((Transform t) => {
            t.SetParent(parent);
        });
    }

    internal static IEnumerable<Transform> FindAllDescendants(this Transform t) {
        int numChildren = t.childCount;
        for (int i = 0; i < numChildren; ++i) {
            Transform child = t.GetChild(i);
            yield return child;

            using var childEnumerator = FindAllDescendants(child).GetEnumerator();
            while (childEnumerator.MoveNext()) {
                yield return childEnumerator.Current;
            }
        }
    }
    
}

} //end namespace