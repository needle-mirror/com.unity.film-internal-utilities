#if AT_USE_TIMELINE

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.FilmInternalUtilities {


internal static class TimelineUtility {

//----------------------------------------------------------------------------------------------------------------------
    internal static int CalculateNumFrames(TimelineClip clip) {
        double fps       = clip.GetParentTrack().timelineAsset.editorSettings.GetFPS();
        int   numFrames = Mathf.RoundToInt((float)(clip.duration * fps));
        return numFrames;
            
    }
    
//----------------------------------------------------------------------------------------------------------------------
    
    internal static double CalculateTimePerFrame(TimelineClip clip) {
        return CalculateTimePerFrame(clip.GetParentTrack());
    }

//----------------------------------------------------------------------------------------------------------------------
    internal static double CalculateTimePerFrame(TrackAsset trackAsset) {
        double fps = trackAsset.timelineAsset.editorSettings.GetFPS();
        double timePerFrame = 1.0f / fps;
        return timePerFrame;
    }
    
    
//----------------------------------------------------------------------------------------------------------------------

    internal static Dictionary<TimelineClip, T> ConvertClipsToClipAssetsDictionary<T>(
        System.Collections.Generic.IEnumerable<TimelineClip> clips) 
        where T: class, IPlayableAsset
    {
        Dictionary<TimelineClip, T> clipAssets = new Dictionary<TimelineClip, T>();
        foreach (TimelineClip clip in clips) {
            T clipAsset = clip.asset as T;
            Assert.IsNotNull(clipAsset);
            clipAssets.Add(clip, clipAsset);
        }

        return clipAssets;
    }

}

} //ena namespace


#endif //AT_USE_TIMELINE
