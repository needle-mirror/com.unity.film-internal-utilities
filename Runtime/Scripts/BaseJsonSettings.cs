using System;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;


namespace Unity.FilmInternalUtilities {

//[TODO-sin: 2022-02-04] Remove this later
[Serializable]
internal abstract class BaseJsonSettings  {

    internal BaseJsonSettings(string settingsPath) {
        m_settingsPath = settingsPath;
    }

    internal string GetSettingsPath() => m_settingsPath;
    
//----------------------------------------------------------------------------------------------------------------------
    
    //Works in Editor only. Not tested in Runtime
    internal bool Save() {
        Assert.IsNotNull(m_settingsPath);
        
        string path = m_settingsPath;
        string dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir)) {
            Directory.CreateDirectory(dir);

        }

        object objectLock = GetLockV();

        lock (objectLock) {
            FileUtility.SerializeToJson(this, path);
        }

        return true;
    }   

    protected static T DeserializeFromJson<T>(string path) where T : BaseJsonSettings {

        T instance = null;
        if (File.Exists(path)) {
            instance = FileUtility.DeserializeFromJson<T>(path);
        }

        if (null != instance) {
            instance.OnDeserializeV();
        }

        return instance;
       
    }
    
//----------------------------------------------------------------------------------------------------------------------
    protected abstract object GetLockV();

    protected abstract void OnDeserializeV();
//----------------------------------------------------------------------------------------------------------------------

    private readonly string m_settingsPath = null;


}

} //end namespace