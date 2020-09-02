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

//----------------------------------------------------------------------------------------------------------------------       

    internal static T[] ConvertArray<T>(Object[] objs) where T :  UnityEngine.Object{
        int numObjects = objs.Length;
        T[] ret = new T[numObjects];
        for (int i = 0; i < numObjects; i++) {
            ret[i] = objs[i] as T;
        }
        return ret;
    }

//----------------------------------------------------------------------------------------------------------------------       
    
    internal static void Destroy(Object obj, bool forceImmediate = false) {
        if (!Application.isPlaying || forceImmediate) {
            Object.DestroyImmediate(obj);                        
        } else {
            Object.Destroy(obj);            
        }
    }
    
//----------------------------------------------------------------------------------------------------------------------       
    
    /// <summary>
    /// Create a GameObject with a Component
    /// </summary>
    /// <param name="goName">The name of the GameObject</param>
    /// <typeparam name="T">The type of the Component</typeparam>
    /// <returns>The newly created GameObject</returns>
    internal static T CreateGameObjectWithComponent<T>(string goName) where T: Component {
        GameObject go        = new GameObject(goName);
        T          component = go.AddComponent<T>();
        return component;        
    }
    
}

} //end namespace
