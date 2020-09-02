using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


namespace Unity.FilmInternalUtilities.Tests {

/// <summary>
/// A Dummy Timeline PlayableAsset 
/// </summary>
[System.Serializable]
internal class DummyTimelinePlayableAsset : BaseExtendedClipPlayableAsset<DummyTimelineClipData>, ITimelineClipAsset {

    public sealed override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        return Playable.Create(graph);
    }
    
    public ClipCaps clipCaps {
        get { return ClipCaps.None; }
    }
    
}

} //end namespace


