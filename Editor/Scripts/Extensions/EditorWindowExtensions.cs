using UnityEditor;
using UnityEngine;


namespace Unity.FilmInternalUtilities.Editor {

internal static class EditorWindowExtensions {
    private static Vector2 GetWindowSize(this EditorWindow editorWindow) {
        Rect pos = editorWindow.position;
        return new Vector2(pos.width, pos.height);
    }}

} //end namespace

