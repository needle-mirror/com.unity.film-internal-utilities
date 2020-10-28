using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.FilmInternalUtilities.Tests;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;
using UnityEditor;

namespace Unity.FilmInternalUtilities.EditorTests {

internal class SerializedDictionaryTest {

    [UnityTest]
    public IEnumerator VerifyNullDeserialization() {
        const string                testScenePath = "Assets/TestRunnerScene.unity";
        List<DummyScriptableObject> dummyObjects  = new List<DummyScriptableObject>();
        
        const int NUM_ELEMENTS_TO_CREATE = 10;
        const int NUM_ELEMENTS_TO_DELETE = 3;
        
        //Create elements, and delete some
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);        
        DummySerializedDictionaryComponent comp = new GameObject().AddComponent<DummySerializedDictionaryComponent>();
        for (int i = 0; i < NUM_ELEMENTS_TO_CREATE; ++i) {
            dummyObjects.Add(CreateDictionaryElement(comp,i));            
        }
        yield return EditorTestsUtility.WaitForFrames(1);

        for (int i = 0; i < NUM_ELEMENTS_TO_DELETE; ++i) {
            Object.DestroyImmediate(dummyObjects[i]);
        }
        yield return EditorTestsUtility.WaitForFrames(1);

        //Save and load
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), testScenePath);
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        Scene scene = EditorSceneManager.OpenScene(testScenePath);
        Assert.IsNotNull(scene);
        yield return EditorTestsUtility.WaitForFrames(1);
        
        comp = Object.FindObjectOfType<DummySerializedDictionaryComponent>();
        Assert.IsNotNull(comp);
        int expected    = NUM_ELEMENTS_TO_CREATE - NUM_ELEMENTS_TO_DELETE;
        int numElements = comp.GetNumElements();
        Assert.AreEqual(expected,numElements , $"Elements are not deserialized correctly. {expected} != {numElements}");
        yield return EditorTestsUtility.WaitForFrames(1);
        
        //Cleanup
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        EditorSceneManager.CloseScene(scene, true);
        AssetDatabase.DeleteAsset(testScenePath);
    }

//----------------------------------------------------------------------------------------------------------------------    

    private static DummyScriptableObject CreateDictionaryElement(DummySerializedDictionaryComponent comp, int key) {

        DummyScriptableObject newObject = ScriptableObject.CreateInstance<DummyScriptableObject>();
        comp.AddElement(newObject, Mathf.RoundToInt(key));
        return newObject;

    }
    
    
}


} //end namespace

