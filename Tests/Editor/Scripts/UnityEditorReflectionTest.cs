using NUnit.Framework;
using Unity.FilmInternalUtilities.Editor;

namespace Unity.FilmInternalUtilities.EditorTests {
internal class UnityEditorReflectionTest {

    [Test]
    public void VerifyReflectedMethods() {
        
        Assert.IsNotNull(UnityEditorReflection.SCROLLABLE_TEXT_AREA_METHOD);

    }

}

    

} //end namespace