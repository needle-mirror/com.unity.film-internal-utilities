using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;


namespace Unity.FilmInternalUtilities.Tests {

internal class TransformExtensionsTest {

    [Test]
    public void FindOrCreateChildren() {
        GameObject parent  =  new GameObject("Parent");
        Transform  parentT = parent.transform;
        Transform  child0  = FindOrCreateChildAndVerify(parentT,"Child0");
        
        Transform sameChild = FindOrCreateChildAndVerify(parentT,"Child0");
        Assert.AreEqual(child0, sameChild);

        Transform child1 = FindOrCreateChildAndVerify(parentT,"Child1");
        Assert.AreNotEqual(child0, child1);
    }
//----------------------------------------------------------------------------------------------------------------------    

    [Test]
    public void SetParentOfTransforms() {
        GameObject parent  =  new GameObject("Parent");
        Transform  parentT = parent.transform;

        List<Transform> children = new List<Transform>();
        for (int i = 0; i < 10; ++i) {
            GameObject child =  new GameObject($"Child-{i}");
            children.Add(child.transform);
        }
        
        children.SetParent(parentT);

        foreach (Transform t in children) {
            Assert.IsNotNull(t);
            Assert.AreEqual(parentT, t.parent);
        }
    }
    
//----------------------------------------------------------------------------------------------------------------------    

    static Transform FindOrCreateChildAndVerify(Transform parent, string childName) {
        Transform child = parent.FindOrCreateChild(childName);
        Assert.IsNotNull(child);
        Assert.AreEqual(parent, child.parent);
        return child;
    }

}
 

        
        
} //end namespace
