using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.FilmInternalUtilities.EditorTests {

internal class SceneComponentsTest {

[UnityTest]
public IEnumerator EnsureClearOnSceneClosed() {
    
    //Clear all lights first
    foreach (Light l in Object.FindObjectsOfType<Light>()) {
        Object.DestroyImmediate(l.gameObject);
    }
    
    const int NUM_LIGHTS = 3;
    for (int i = 0; i < NUM_LIGHTS; ++i) {
        new GameObject().AddComponent<Light>();
        SceneComponents<Light>.GetInstance().Update();
        yield return null;
        
        Assert.AreEqual(i + 1 , SceneComponents<Light>.GetInstance().GetCachedComponents().Count);
    }

    yield return null;

    EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
    yield return null;
    
    Assert.AreEqual(0 , SceneComponents<Light>.GetInstance().GetCachedComponents().Count);

}
}

} //end namespace
