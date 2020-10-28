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
        foreach (Transform t in collection) {
            t.SetParent(parent);
        }
    }
    
}

} //end namespace