
#if AT_USE_TIMELINE

using UnityEngine.Timeline;

namespace Unity.FilmInternalUtilities {

internal static class TimelineClipExtensions {
    
#if !AT_USE_TIMELINE_GE_1_5_0            
    internal static TrackAsset GetParentTrack(this TimelineClip clip) {
        return clip.parentTrack;
    }

    internal static void TryMoveToTrack(this TimelineClip clip, TrackAsset track) {
        clip.parentTrack = track;
    }
    
#endif
}

} //end namespace

#endif //AT_USE_TIMELINE