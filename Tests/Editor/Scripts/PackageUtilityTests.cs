using System.Collections;
using NUnit.Framework;
using Unity.FilmInternalUtilities.Editor;
using UnityEngine.TestTools;

namespace Unity.FilmInternalUtilities.EditorTests {
internal class PackageUtilityTests {

//----------------------------------------------------------------------------------------------------------------------

    [UnityTest]
    public IEnumerator VerifyExistingPackageVersion() {
        string packageVersion = null;
        yield return FindPackageVersionAndWait(
            FilmInternalUtilitiesEditorConstants.PACKAGE_NAME, v => packageVersion = v
        );
        Assert.IsFalse(string.IsNullOrEmpty(packageVersion));
    }

//----------------------------------------------------------------------------------------------------------------------
    [UnityTest]
    public IEnumerator VerifyMissingPackageVersion() {
        string packageVersion = null;
        yield return FindPackageVersionAndWait(
            "com.package.does-not-exist", v => packageVersion = v
        );
        Assert.IsTrue(string.IsNullOrEmpty(packageVersion));
    }


//----------------------------------------------------------------------------------------------------------------------
    
    private IEnumerator FindPackageVersionAndWait(string packageName, System.Action<string> onComplete) {
        bool done = false;
        string packageVersion = null;
        PackageUtility.FindInstalledPackageVersion(
            packageName,
            onVersionFound: (version) => {
                packageVersion = version;
                done = true;
            },
            onVersionNotFound: () => {
                done = true;
            }
        );
        while (!done) {
            yield return null;
        }
        onComplete?.Invoke(packageVersion);
    }
    
}
} //end namespace