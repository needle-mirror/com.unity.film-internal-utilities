using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unity.FilmInternalUtilities { 
/// <summary>
/// A track which requires its TimelineClip to store BaseClipData as an extension
/// </summary>
internal abstract class BaseExtendedClipTrack<D> : BaseTrack 
    where D: BaseClipData, new()
{
    
    void OnEnable() {
        InitClipData();
        OnEnableInternalV();
    }

    protected virtual void OnEnableInternalV() { }
    
    
//----------------------------------------------------------------------------------------------------------------------
    /// <inheritdoc/>
    protected override void OnBeforeTrackSerialize() {
        base.OnBeforeTrackSerialize();
        m_serializedDataCollection = new List<D>();
        
        foreach (TimelineClip clip in GetClips()) {

            if (null == m_dataCollection || !m_dataCollection.TryGetValue(clip, out D data)) {
                BaseExtendedClipPlayableAsset<D> playableAsset = clip.asset as BaseExtendedClipPlayableAsset<D>;
                Assert.IsNotNull(playableAsset);                 
                data = playableAsset.GetBoundClipData();
            }

            if (null == data) {
                data = new D();
                data.SetOwner(clip);
            }
            
                       
            m_serializedDataCollection.Add(data);
        }
    }
    
    /// <inheritdoc/>
    protected override  void OnAfterTrackDeserialize() {
        base.OnAfterTrackDeserialize();
        m_dataCollection = new Dictionary<TimelineClip, D>();

        if (null == m_serializedDataCollection) {
            m_serializedDataCollection = new List<D>();
        }         
        
        IEnumerator<TimelineClip> clipEnumerator = GetClips().GetEnumerator();
        List<D>.Enumerator sisEnumerator = m_serializedDataCollection.GetEnumerator();
        while (clipEnumerator.MoveNext() && sisEnumerator.MoveNext()) {
            TimelineClip clip = clipEnumerator.Current;
            Assert.IsNotNull(clip);

            D sceneCacheClipData = sisEnumerator.Current;
            Assert.IsNotNull(sceneCacheClipData);           
            
            m_dataCollection[clip] = sceneCacheClipData;
            
        }
        clipEnumerator.Dispose();
        sisEnumerator.Dispose();
    }
    
//----------------------------------------------------------------------------------------------------------------------
    
    /// <inheritdoc/>
    public sealed override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
               
        if (null == m_dataCollection) {
            m_dataCollection = new Dictionary<TimelineClip, D>();
        }
        InitClipData();
        Playable mixer = CreateTrackMixerInternal(graph, go, inputCount);
        
        return mixer;
    }

    protected abstract Playable CreateTrackMixerInternal(PlayableGraph graph, GameObject go, int inputCount);
    

//----------------------------------------------------------------------------------------------------------------------

    /// <inheritdoc/>
    public override string ToString() { return name; }   

        
//----------------------------------------------------------------------------------------------------------------------
    private void InitClipData() {
        //Initialize PlayableAssets and BaseClipData       
        foreach (TimelineClip clip in GetClips()) {
            
            BaseExtendedClipPlayableAsset<D> playableAsset = clip.asset as BaseExtendedClipPlayableAsset<D>;
            if (null == playableAsset)
                continue;

            //Try to get existing one, either from the collection, or the clip
            if (!m_dataCollection.TryGetValue(clip, out D clipData)) {
                clipData = playableAsset.GetBoundClipData();
            }

            if (null == clipData) {
                clipData = new D();                
            }
            
            //Fix the required data structure
            m_dataCollection[clip] = clipData;
            clipData.SetOwner(clip);
            playableAsset.BindClipData(clipData);                        
        }
        
    }


    
//----------------------------------------------------------------------------------------------------------------------

    [HideInInspector][SerializeField] List<D> m_serializedDataCollection = null;

    private Dictionary<TimelineClip, D> m_dataCollection = null;    
}

} //end namespace


