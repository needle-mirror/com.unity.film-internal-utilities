using System;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

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
        
#pragma warning disable 612
        m_obsoleteDataCollection     = null;
        m_obsoleteClipDataCollection = null;
#pragma warning restore 612
        
        foreach (TimelineClip clip in GetClips()) {
            int hashCode = clip.asset.GetHashCode();

            if (!m_hashClipDataCollection.TryGetValue(hashCode, out D data)) {
                BaseExtendedClipPlayableAsset<D> playableAsset = clip.asset as BaseExtendedClipPlayableAsset<D>;
                Assert.IsNotNull(playableAsset);
                data = playableAsset.GetBoundClipData();
            }

            if (null == data) {
                data = new D();
                data.SetOwner(clip);
            }

            m_hashClipDataCollection[hashCode] = data;
        }
    }
    
    /// <inheritdoc/>
    protected override  void OnAfterTrackDeserialize() {
        base.OnAfterTrackDeserialize();
        ConvertLegacyData();        
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void ConvertLegacyData() {
        
#pragma warning disable 612        

        //List<D> format
        if (null != m_obsoleteDataCollection && m_obsoleteDataCollection.Count > 0) {
            IEnumerator<TimelineClip> clipEnumerator = GetClips().GetEnumerator();
            List<D>.Enumerator        dataEnumerator = m_obsoleteDataCollection.GetEnumerator();
            while (clipEnumerator.MoveNext() && dataEnumerator.MoveNext()) {
                TimelineClip clip = clipEnumerator.Current;
                Assert.IsNotNull(clip);

                D sceneCacheClipData = dataEnumerator.Current;
                Assert.IsNotNull(sceneCacheClipData);

                m_hashClipDataCollection[clip.asset.GetHashCode()] = sceneCacheClipData;
            }

            clipEnumerator.Dispose();
            dataEnumerator.Dispose();
            m_obsoleteDataCollection.Clear();
            return;
        }
        
        // SerializedDictionary<TimelineClip, D> format
        if (null != m_obsoleteClipDataCollection && m_obsoleteClipDataCollection.Count > 0) {
            foreach (KeyValuePair<TimelineClip, D> kv in m_obsoleteClipDataCollection) {
                TimelineClip clip = kv.Key;
                Assert.IsNotNull(clip);

                D sceneCacheClipData = kv.Value;
                Assert.IsNotNull(sceneCacheClipData);
                m_hashClipDataCollection[clip.asset.GetHashCode()] = sceneCacheClipData;
            } 
            m_obsoleteClipDataCollection.Clear();
            return;
        }
#pragma warning restore 612
        
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    /// <inheritdoc/>
    public sealed override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
               
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

            int hashCode = playableAsset.GetHashCode();
            //Try to get existing one, either from the collection, or the clip
            if (!m_hashClipDataCollection.TryGetValue(hashCode, out D clipData)) {
                clipData = playableAsset.GetBoundClipData();
            }

            if (null == clipData) {
                clipData = new D();
            }

            //Fix the required data structure
            m_hashClipDataCollection[hashCode] = clipData;
            clipData.SetOwner(clip);
            playableAsset.BindClipData(clipData);
        }
        
    }


    
//----------------------------------------------------------------------------------------------------------------------

    [FormerlySerializedAs("m_serializedDataCollection")] [Obsolete][HideInInspector][SerializeField] 
    List<D> m_obsoleteDataCollection = null;

    [FormerlySerializedAs("m_clipDataCollection")] [Obsolete][HideInInspector][SerializeField] 
    private SerializedDictionary<TimelineClip, D> m_obsoleteClipDataCollection = null;
    
    [HideInInspector][SerializeField] 
    private SerializedDictionary<int, D> m_hashClipDataCollection = new SerializedDictionary<int, D>();

#pragma warning disable 414
    [HideInInspector][SerializeField] private int m_baseExtendedClipTrackVersion = (int) BaseExtendedClipTrackVersion.SerializedAssetHash_0_16_2;
#pragma warning restore 414

    enum BaseExtendedClipTrackVersion : int {
        SerializedAssetHash_0_16_2 = 1,
    }
    
}

} //end namespace


