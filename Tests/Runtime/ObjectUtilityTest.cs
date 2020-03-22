using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


namespace Unity.FilmInternalUtilities.Tests {

internal class ObjectUtilityTest {

    [Test]   
    public void CreatePrimitiveAndFindComponents() {
            
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        int instanceID = go.GetInstanceID();

        Assert.IsNotNull(FindComponentWithGameObjectID(ObjectUtility.FindSceneComponents<MeshFilter>(), instanceID));        
        Assert.IsNotNull(FindComponentWithGameObjectID(ObjectUtility.FindSceneComponents<MeshRenderer>(), instanceID));        
        Assert.IsNotNull(FindComponentWithGameObjectID(ObjectUtility.FindSceneComponents<SphereCollider>(), instanceID));        
        
        Object.DestroyImmediate(go);
    }

//----------------------------------------------------------------------------------------------------------------------
    
    private static T FindComponentWithGameObjectID<T>(IEnumerable<T> components, int goID) where T : UnityEngine.Component {
        T ret = null;
        Assert.IsNotNull(components);
        var enumerator = components.GetEnumerator();
        while (enumerator.MoveNext() && null == ret) {
            T curComponent = enumerator.Current;
            Assert.IsNotNull(curComponent);
            if (curComponent.gameObject.GetInstanceID() == goID) {
                ret = curComponent;
            }            
        }
        enumerator.Dispose();
        return ret;
        
    }
    
}
 
        
} //end namespace

