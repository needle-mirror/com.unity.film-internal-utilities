using System;
using System.Collections;

namespace Unity.FilmInternalUtilities.Editor {

internal static class EditorTestsUtility {
                
    [Obsolete("Replaced by YieldEditorUtility.WaitForFramesAndIncrementUndo()")]
    internal static IEnumerator WaitForFrames(int numFrames) {
        yield return YieldEditorUtility.WaitForFramesAndIncrementUndo(numFrames);
    }
    
}

} //end namespace