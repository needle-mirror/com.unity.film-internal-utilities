using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

namespace Unity.FilmInternalUtilities.Editor {

/// <summary>
/// A utility class for executing operations related to Timeline assets in the editor.
/// </summary>
internal static class TimelineEditorUtility {

    /// <summary>
    /// Create a TimelineAsset, which can be assigned to a PlayableDirector.
    /// </summary>
    /// <param name="timelineAssetPath"></param>
    /// <returns>The newly created TimelineAsset</returns>
    internal static TimelineAsset CreateAsset(string timelineAssetPath) 
    {
        TimelineAsset timelineAsset = ScriptableObject.CreateInstance<TimelineAsset>();
        AssetDatabase.CreateAsset(timelineAsset, timelineAssetPath);
        
        return timelineAsset;
    }
    
    /// <summary>
    /// Create a Track and TimelineClip in a TimelineAsset.
    /// </summary>
    /// <param name="timelineAsset">The TimelineAsset in which the track and clip will be created</param>
    /// <param name="trackName">The track Name</param>
    /// <typeparam name="T">The type of the Track</typeparam>
    /// <typeparam name="A">The type of the Clip</typeparam>
    /// <returns>The newly created TimelineClip</returns>
    internal static TimelineClip CreateTrackAndClip<T, A>(TimelineAsset timelineAsset, string trackName) 
        where T: TrackAsset, new() 
        where A : class, ITimelineClipAsset
    {
        T track = timelineAsset.CreateTrack<T>(null, trackName);
        TimelineClip clip = track.CreateDefaultClip();
        return clip;
    }
    
    
//----------------------------------------------------------------------------------------------------------------------
    
    
    /// <summary>
    /// Destroy Timeline assets related to the passed TimelineClip
    /// </summary>
    /// <param name="clip">The clip which assets will be destroyed</param>
    internal static void DestroyAssets(TimelineClip clip) {
        TrackAsset    movieTrack    = clip.GetParentTrack();
        TimelineAsset timelineAsset = movieTrack.timelineAsset;
            
        string tempTimelineAssetPath = AssetDatabase.GetAssetPath(timelineAsset);
        Assert.IsFalse(string.IsNullOrEmpty(tempTimelineAssetPath));

        timelineAsset.DeleteTrack(movieTrack);
        Object.DestroyImmediate(timelineAsset, true);
        AssetDatabase.DeleteAsset(tempTimelineAssetPath);            
    }
    
    
}

} //end namespace