using System;

namespace Unity.FilmInternalUtilities {

/// <summary>
/// A utility class for executing operations related to Unity assets.
/// Can be executed by runtime code in the editor, but should not be executed in an executable.
/// </summary>
internal static class AssetUtility {

    
    [Obsolete]
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
    [Obsolete] 
    public static bool IsAssetPath(string path) {
        string normalizedPath = NormalizeAssetPath(path);
        string[] dirs = normalizedPath.Split('/');
        return (dirs.Length > 0 && dirs[0] == "Assets");
    }
    
//----------------------------------------------------------------------------------------------------------------------    

    static string GetApplicationRootPath() {
        if (null != m_appRootPath)
            return m_appRootPath;

        //Not using Application.dataPath because it may not be called in certain times, e.g: during serialization
        
        m_appRootPath = System.IO.Directory.GetCurrentDirectory().Replace('\\','/');
        return m_appRootPath;
    }

//----------------------------------------------------------------------------------------------------------------------    

    private static string m_appRootPath = null;

}

} //end namespace