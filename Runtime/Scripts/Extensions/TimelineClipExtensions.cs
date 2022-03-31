
using JetBrains.Annotations;
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

    [CanBeNull]
    internal static T GetClipData<T>(this TimelineClip clip) where T: BaseClipData {
        
        BaseExtendedClipPlayableAsset<T> clipAsset = clip.asset as BaseExtendedClipPlayableAsset<T>;
        if (null == clipAsset)
            return null;
        
        T clipData = clipAsset.GetBoundClipData();
        return clipData;
    }
}

} //end namespace

