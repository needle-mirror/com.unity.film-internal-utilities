using System.IO;
using NUnit.Framework;
using Unity.FilmInternalUtilities.Editor;
using UnityEngine.Playables;
using Unity.FilmInternalUtilities.Tests;
using UnityEditor;
using UnityEngine.Timeline;

namespace Unity.FilmInternalUtilities.EditorTests {
internal class TimelineEditorUtilityTest {
                
    [Test]
    public void CreateAndDestroyTimelineAssets() {        
        string timelineAssetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/TimelineEditorUtilityTest.playable");
        
        PlayableDirector director = ObjectUtility.CreateGameObjectWithComponent<PlayableDirector>("Director");
        TimelineAsset timelineAsset = TimelineEditorUtility.CreateAsset(timelineAssetPath);

        director.playableAsset = timelineAsset; 
        Assert.IsTrue(File.Exists(timelineAssetPath));


        TimelineClip clip = TimelineEditorUtility.CreateTrackAndClip<DummyTimelineTrack, DummyTimelinePlayableAsset>(
            timelineAsset, "FirstTrack");        
        Assert.IsNotNull(clip);

        //Cleanup           
        TimelineEditorUtility.DestroyAssets(clip);
        Assert.IsFalse(File.Exists(timelineAssetPath));


    }

//----------------------------------------------------------------------------------------------------------------------

    
}
}
