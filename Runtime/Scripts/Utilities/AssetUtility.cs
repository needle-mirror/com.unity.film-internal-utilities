using UnityEngine;

namespace Unity.FilmInternalUtilities {

/// <summary>
/// A utility class for executing operations related to Unity assets.
/// Can be executed by runtime code in the editor, but should not be executed in an executable.
/// </summary>
internal static class AssetUtility {

    /// <summary>
    /// Normalize an absolute path under Unity project to make it relative to the Unity project folder.
    /// Paths that are outside Unity project will be unchanged.
    /// Will always return with directory separators using slash ('/'), but can handle both slash/backslash
    /// as the directory separator for input. 
    /// Ex: C:/TempUnityProject/Assets/Foo.prefab => Assets/Foo.prefab
    ///     C:/NonUnityProject/Foo.prefab => C:/NonUnityProject/Foo.prefab
    /// </summary>
    /// <param name="path">The path to be normalized.</param>
    /// <returns>The normalized path.</returns>
    public static string NormalizeAssetPath(string path) {
        if (string.IsNullOrEmpty(path))
            return null;

        string slashedPath = path.Replace('\\', '/');        
        string projectRoot = GetApplicationRootPath();
        
        if (slashedPath.StartsWith(projectRoot)) {
            string normalizedPath = slashedPath.Substring(projectRoot.Length);
            if (normalizedPath.Length > 0) {
                normalizedPath = normalizedPath.Substring(1); //1 for additional '/'           
            }

            return normalizedPath;
        }
        return slashedPath;
    }

//----------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Returns whether the path points to a path under "Assets" folder
    /// </summary>
    public static bool IsAssetPath(string path) {
        string normalizedPath = NormalizeAssetPath(path);        
        string[] dirs = normalizedPath.Split('/');
        return (dirs.Length > 0 && dirs[0] == "Assets");
    }
    
//----------------------------------------------------------------------------------------------------------------------    

    static string GetApplicationRootPath() {
        if (null != m_appRootPath)
            return m_appRootPath;
        
        m_appRootPath = PathUtility.GetDirectoryName(Application.dataPath).Replace('\\','/');
        return m_appRootPath;
    }

//----------------------------------------------------------------------------------------------------------------------    

    private static string m_appRootPath = null;

}

} //end namespace