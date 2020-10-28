using System;
using UnityEngine;

namespace Unity.FilmInternalUtilities.Tests {

[Serializable]
internal class DummyJsonSettings : BaseJsonSettings {

//----------------------------------------------------------------------------------------------------------------------


    internal static DummyJsonSettings GetOrCreateInstance() {
        
        if (null != m_instance) {
            return m_instance;
        }

        lock (m_lock) {           
        
#if UNITY_EDITOR
            m_instance = DeserializeFromJson<DummyJsonSettings>(DUMMY_SETTINGS_PATH);
            if (null != m_instance) {
                return m_instance;
            }            
#endif
            
            m_instance = new DummyJsonSettings();
#if UNITY_EDITOR
            m_instance.Save();
#endif
        }        

        return m_instance;
    }

    internal static void Close() {
        if (null != m_instance) {
            m_instance.Save();
        }
        m_instance = null;
    }
    
    internal static string GetSettingPath() => DUMMY_SETTINGS_PATH;
    
//----------------------------------------------------------------------------------------------------------------------    
    internal DummyJsonSettings() : base(DUMMY_SETTINGS_PATH) {
        
    } 
    protected override object GetLockV() { return m_lock; }

    protected override void OnDeserializeV() {
        m_isDeserialized = true;       
    }

//----------------------------------------------------------------------------------------------------------------------

    internal void SetValue(int v) {
        m_basicValue = v;
    }

    internal int GetValue() => m_basicValue;

    internal bool IsDeserialized() => m_isDeserialized;


//----------------------------------------------------------------------------------------------------------------------
    private bool   m_isDeserialized = false;

    private const string DUMMY_SETTINGS_PATH = "Assets/TestDummyJsonSettings.asset";


    [SerializeField] private int m_basicValue = 1;

//----------------------------------------------------------------------------------------------------------------------

    private static          DummyJsonSettings m_instance;
    private static readonly object            m_lock = new object();

}

} //end namespace
