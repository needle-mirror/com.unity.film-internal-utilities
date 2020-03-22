using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Unity.FilmInternalUtilities {
internal static class ObjectUtility {

    internal static IEnumerable<T> FindSceneComponents<T>() where T: UnityEngine.Component {
        foreach (Object o in Resources.FindObjectsOfTypeAll(typeof(T))) {
            T comp = (T) o;
            Assert.IsNotNull(comp);
            GameObject go   = comp.gameObject;

            if (!(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
#if UNITY_EDITOR                    
                && !EditorUtility.IsPersistent(comp.transform.root.gameObject)
#endif                    
            ) 
            {
                yield return comp;
                    
            }
        }
    }
    
}

} //end namespace
