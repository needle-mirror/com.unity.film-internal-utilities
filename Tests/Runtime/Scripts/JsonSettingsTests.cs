using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;


namespace Unity.FilmInternalUtilities.Tests {

internal class JsonSettingsTests {

    [UnityTearDown]
    public void TearDown() {
        CloseAndDeleteDummyJson();
    }

//----------------------------------------------------------------------------------------------------------------------    

    [Test]
    public void CreateAndSave() {
        DummyJsonSettings singletonSetting = DummyJsonSettings.GetOrCreateInstance();
        Assert.IsFalse(singletonSetting.IsDeserialized());
        string jsonPath = DummyJsonSettings.GetSettingPath();
        Assert.IsTrue(File.Exists(jsonPath));
        CloseAndDeleteDummyJson();
    }

//----------------------------------------------------------------------------------------------------------------------    
    
    [Test]
    public void CreateAndReload() {
        const int TEST_VALUE = 12345;
        
        DummyJsonSettings singletonSetting = DummyJsonSettings.GetOrCreateInstance();
        singletonSetting.SetValue(TEST_VALUE);

        DummyJsonSettings.Close();
        singletonSetting = DummyJsonSettings.GetOrCreateInstance();
        Assert.IsTrue(singletonSetting.IsDeserialized());
        Assert.AreEqual(TEST_VALUE, singletonSetting.GetValue());
        CloseAndDeleteDummyJson();
    }

//----------------------------------------------------------------------------------------------------------------------    
    [Test]
    public void DeserializeManually() {
        const int TEST_VALUE = 45678;
        
        DummyJsonSettings singletonSetting = DummyJsonSettings.GetOrCreateInstance();
        Assert.IsFalse(singletonSetting.IsDeserialized());
        singletonSetting.SetValue(TEST_VALUE);
        singletonSetting.Save();

        string jsonPath = DummyJsonSettings.GetSettingPath();
        DummyJsonSettings deserializedSetting = FileUtility.DeserializeFromJson<DummyJsonSettings>(jsonPath);
        Assert.NotNull(deserializedSetting);
        Assert.AreEqual(TEST_VALUE, deserializedSetting.GetValue());
        CloseAndDeleteDummyJson();

    }
    
//----------------------------------------------------------------------------------------------------------------------    

    static void CloseAndDeleteDummyJson() {
        DummyJsonSettings.Close();
        string path = DummyJsonSettings.GetSettingPath();
        if (File.Exists(path)) {
            AssetDatabase.DeleteAsset(path);
        }
        
    } 
    
}
        
} //end namespace
