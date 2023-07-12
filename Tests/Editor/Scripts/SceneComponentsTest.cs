using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.FilmInternalUtilities.EditorTests {

internal class SceneComponentsTest {

    [UnityTest]
    public IEnumerator EnsureClearOnSceneClosed() {

        const int NUM_LIGHTS = 3;
        
        DestroyAllLights(); //Clear all lights first

        SceneComponents<Light> lightComponents = SceneComponents<Light>.GetInstance();
        for (int i = 0; i < NUM_LIGHTS; ++i) {
            new GameObject().AddComponent<Light>();
            lightComponents.Update();
            yield return null;
            
            Assert.AreEqual(i + 1 , lightComponents.GetCachedComponents().Count);
        }

        yield return null;

        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        yield return null;
        
        Assert.AreEqual(0 , lightComponents.GetCachedComponents().Count);

    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    [Test]
    public void CompareUpdateMethods() {
        const int NUM_LIGHTS = 3;
        DestroyAllLights(); //Clear all lights first
        
        SceneComponents<Light> lightComponents = SceneComponents<Light>.GetInstance();
        lightComponents.Update();
        Assert.AreEqual(0 , lightComponents.GetCachedComponents().Count);
        
        for (int i = 0; i < NUM_LIGHTS; ++i) {
            new GameObject().AddComponent<Light>();
            lightComponents.Update();
            Assert.AreEqual(i , lightComponents.GetCachedComponents().Count);
            
            lightComponents.ForceUpdate();
            Assert.AreEqual(i + 1 , lightComponents.GetCachedComponents().Count);
        }
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    [Test]
    public void IncludeDisabledObjects() {
        const int NUM_LIGHTS = 3;
        DestroyAllLights(); //Clear all lights first
        
        SceneComponents<Light> lightComponents = SceneComponents<Light>.GetInstance();
        lightComponents.SetIncludeInactive(true);
        CreateLightObjects(NUM_LIGHTS, active:false);
        
        lightComponents.ForceUpdate();
        Assert.AreEqual(NUM_LIGHTS , lightComponents.GetCachedComponents().Count);
    }

    [Test]
    public void ExcludeDisabledObjects() {
        DestroyAllLights(); //Clear all lights first
        
        SceneComponents<Light> lightComponents = SceneComponents<Light>.GetInstance();
        lightComponents.SetIncludeInactive(false);
        CreateLightObjects(numLights:3, active:false);
        
        lightComponents.ForceUpdate();
        Assert.AreEqual(0 , lightComponents.GetCachedComponents().Count);
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------

    static void CreateLightObjects(int numLights, bool active) {
        for (int i = 0; i < numLights; ++i) {
            GameObject go = new GameObject();
            go.AddComponent<Light>();
            go.SetActive(active);
        }
    }
    
    static void DestroyAllLights() {
        Object.FindObjectsByType<Light>(FindObjectsInactive.Include, FindObjectsSortMode.None).Loop((Light l) => {
            Object.DestroyImmediate(l.gameObject);
        });
    }
    
}

} //end namespace
