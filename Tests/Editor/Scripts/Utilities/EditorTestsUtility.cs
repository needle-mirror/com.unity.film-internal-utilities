using System.Collections;
using UnityEditor;

namespace Unity.FilmInternalUtilities.EditorTests {

internal static class EditorTestsUtility {
                
    internal static IEnumerator WaitForFrames(int numFrames) {
        for (int i = 0; i < numFrames; ++i) {
            yield return null;
            
        }        
        Undo.IncrementCurrentGroup();
    }        
}

} //end namespace