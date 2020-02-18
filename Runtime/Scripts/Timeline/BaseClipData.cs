#if AT_USE_TIMELINE
    
using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace Unity.FilmInternalUtilities {

[Serializable]
internal abstract class BaseClipData : ISerializationCallbackReceiver {

//----------------------------------------------------------------------------------------------------------------------
    #region ISerializationCallbackReceiver
    public abstract void OnBeforeSerialize();
    public abstract void OnAfterDeserialize();
    #endregion
    
//----------------------------------------------------------------------------------------------------------------------
    internal abstract void Destroy();
    

//----------------------------------------------------------------------------------------------------------------------
    internal void SetOwner(TimelineClip clip) { m_clipOwner = clip;}
    
    internal TimelineClip GetOwner() { return m_clipOwner; }

//----------------------------------------------------------------------------------------------------------------------    
    
    //The owner of this ClipData
    [NonSerialized] private TimelineClip  m_clipOwner = null;

#pragma warning disable 414    
    [HideInInspector][SerializeField] private int m_baseClipDataVersion = CUR_CLIP_DATA_VERSION;        
#pragma warning restore 414    

    private const int    CUR_CLIP_DATA_VERSION = 1;
    
}


} //end namespace

#endif //AT_USE_TIMELINE

