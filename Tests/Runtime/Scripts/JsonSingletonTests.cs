using System.IO;
using NUnit.Framework;
using UnityEditor;


namespace Unity.FilmInternalUtilities.Tests {

internal class JsonSingletonTests {

    [TearDown]
    public void TearDown() {
        CloseAndDeleteDummyJson();
    }

//----------------------------------------------------------------------------------------------------------------------    

    [Test]
    public void CreateAndSave() {
        DummyJsonSingleton jsonSingleton = DummyJsonSingleton.GetOrCreateInstance();
        Assert.IsFalse(jsonSingleton.IsDeserialized());
        string jsonPath = jsonSingleton.GetJsonPath();
        Assert.IsTrue(File.Exists(jsonPath));
    }

//----------------------------------------------------------------------------------------------------------------------    
    
    [Test]
    public void CreateAndReload() {
        const int TEST_VALUE = 12345;

        DummyJsonSingleton jsonSingleton = DummyJsonSingleton.GetOrCreateInstance();
        jsonSingleton.SetValue(TEST_VALUE);
        jsonSingleton.SaveInEditor();
        DummyJsonSingleton.Close();
        
        jsonSingleton = DummyJsonSingleton.GetOrCreateInstance();
        Assert.IsTrue(jsonSingleton.IsDeserialized());
        Assert.AreEqual(TEST_VALUE, jsonSingleton.GetValue());
    }

//----------------------------------------------------------------------------------------------------------------------    
    [Test]
    public void DeserializeManually() {
        const int TEST_VALUE = 45678;
        
        DummyJsonSingleton jsonSingleton = DummyJsonSingleton.GetOrCreateInstance();
        Assert.IsFalse(jsonSingleton.IsDeserialized());
        jsonSingleton.SetValue(TEST_VALUE);
        jsonSingleton.SaveInEditor();

        string             jsonPath              = jsonSingleton.GetJsonPath();
        DummyJsonSingleton deserializedSingleton = FileUtility.DeserializeFromJson<DummyJsonSingleton>(jsonPath);
        Assert.NotNull(deserializedSingleton);
        Assert.AreEqual(TEST_VALUE, deserializedSingleton.GetValue());
    }
    
//----------------------------------------------------------------------------------------------------------------------
    
    static void CloseAndDeleteDummyJson() {
        DummyJsonSingleton jsonSingleton = DummyJsonSingleton.GetOrCreateInstance();
        string             path          = jsonSingleton.GetJsonPath();
        DummyJsonSingleton.Close();
        if (!File.Exists(path)) 
            return;
        
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.Refresh();
    }
    
}
        
} //end namespace
