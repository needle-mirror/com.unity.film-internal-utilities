using NUnit.Framework;
using Unity.FilmInternalUtilities.Editor;

namespace Unity.FilmInternalUtilities.EditorTests {
internal class ReflectionTest {


//----------------------------------------------------------------------------------------------------------------------

    [Test]
    public void WindowLayoutReflectionTest() {
        Assert.IsNotNull(LayoutUtility.LOAD_WINDOW_LAYOUT_METHOD);
        Assert.IsNotNull(LayoutUtility.SAVE_WINDOW_LAYOUT_METHOD);
    }
}
}
