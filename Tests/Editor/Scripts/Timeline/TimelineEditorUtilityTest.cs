using System.Collections;
using System.IO;
using NUnit.Framework;
using Unity.FilmInternalUtilities.Editor;
using UnityEngine.Playables;
using Unity.FilmInternalUtilities.Tests;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Timeline;
using Assert = UnityEngine.Assertions.Assert;

namespace Unity.FilmInternalUtilities.EditorTests {
internal class TimelineEditorUtilityTest {

    [UnityTearDown]
    public IEnumerator TearDown() {
        if (File.Exists(TIMELINE_ASSET_PATH)) {
            AssetDatabase.DeleteAsset(TIMELINE_ASSET_PATH);
        }
        yield return null;
    }
    
//----------------------------------------------------------------------------------------------------------------------
                
    [Test]
    public void CreateAndDestroyTimelineAssets() {
        PlayableDirector director          = CreateDirectorWithTimelineAsset(TIMELINE_ASSET_PATH);
        string           timelineAssetPath = AssetDatabase.GetAssetPath(director.playableAsset);
        TimelineEditorUtility.DestroyAssets(director.playableAsset);
        Assert.IsFalse(File.Exists(timelineAssetPath));
    }

//----------------------------------------------------------------------------------------------------------------------


    [UnityTest]
    public IEnumerator CreateClip() {
        PlayableDirector director      = CreateDirectorWithTimelineAsset(TIMELINE_ASSET_PATH);
        TimelineAsset    timelineAsset = director.playableAsset as TimelineAsset;
        Assert.IsNotNull(timelineAsset);
        yield return null;
        
        TimelineClip clip = TimelineEditorUtility.CreateTrackAndClip<DummyTimelineTrack, DummyTimelinePlayableAsset>(
            timelineAsset, "FirstTrack");
        VerifyClip(clip);

        TimelineEditorUtility.DestroyAssets(clip); //Cleanup
    }

//----------------------------------------------------------------------------------------------------------------------
    
    [UnityTest]
    public IEnumerator ShowClipInInspector() {
        PlayableDirector director      = CreateDirectorWithTimelineAsset(TIMELINE_ASSET_PATH);
        TimelineAsset    timelineAsset = director.playableAsset as TimelineAsset;
        Assert.IsNotNull(timelineAsset);
        yield return null;
        
        TrackAsset track = timelineAsset.CreateTrack<DummyTimelineTrack>(null, "FooTrack");        
        TimelineClip clip = TimelineEditorReflection.CreateClipOnTrack(typeof(DummyTimelinePlayableAsset), track, 0);
        VerifyClip(clip);
        
        ScriptableObject editorClip = TimelineEditorUtility.SelectTimelineClipInInspector(clip);
        Assert.IsNotNull(editorClip);
        yield return null;
        
        Object.DestroyImmediate(editorClip);
        yield return null;
                   
        TimelineEditorUtility.DestroyAssets(clip); //Cleanup

    }
    
//----------------------------------------------------------------------------------------------------------------------


    private static PlayableDirector CreateDirectorWithTimelineAsset(string candidatePath) {
        string timelineAssetPath = AssetDatabase.GenerateUniqueAssetPath(candidatePath);
        
        PlayableDirector director   = new GameObject("Director").AddComponent<PlayableDirector>();
        TimelineAsset timelineAsset = TimelineEditorUtility.CreateAsset(timelineAssetPath);

        director.playableAsset = timelineAsset; 
        Assert.IsTrue(File.Exists(timelineAssetPath));
        return director;

    }

    private static void VerifyClip(TimelineClip clip) {
        Assert.IsNotNull(clip);
        DummyTimelinePlayableAsset playableAsset = clip.asset as DummyTimelinePlayableAsset;
        Assert.IsNotNull(playableAsset);
        Assert.IsTrue(playableAsset.IsInitialized());
        
    }

//----------------------------------------------------------------------------------------------------------------------

    private const string TIMELINE_ASSET_PATH = "Assets/TimelineEditorUtilityTest.playable";

}
}
