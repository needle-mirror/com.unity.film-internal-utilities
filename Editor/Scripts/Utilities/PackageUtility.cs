using System;
using UnityEditor.PackageManager;

namespace Unity.FilmInternalUtilities.Editor {

internal static class PackageUtility {

    internal static void FindInstalledPackageVersion(string packageName, Action<string> onVersionFound, 
        Action onVersionNotFound) 
    {
        PackageRequestJobManager.CreateListRequest(offlineMode: false, includeIndirectIndependencies: false,
            onSuccess: (reqResult) => {
                PackageCollection result = reqResult.Result;
                foreach (PackageInfo packageInfo in result) {
                    if (packageInfo.name != packageName)
                        continue;

                    onVersionFound(packageInfo.version);
                    return;
                }
                
                onVersionNotFound();
            }, (request ) => {
                
                onVersionNotFound();
            });
        
    }
    
}

} //end namespace


