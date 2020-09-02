using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.FilmInternalUtilities.Tests {

[TrackClipType(typeof(DummyTimelinePlayableAsset))]
[Serializable]
internal class DummyTimelineTrack : BaseExtendedClipTrack<DummyTimelineClipData> {
    
    protected override Playable CreateTrackMixerInternal(PlayableGraph graph, GameObject go, int inputCount) {
        return Playable.Create(graph, inputCount);
    }     
    
}

} //end namespace