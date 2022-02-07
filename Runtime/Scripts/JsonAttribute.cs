using System;


namespace Unity.FilmInternalUtilities {
[AttributeUsage(AttributeTargets.Class)]
public class JsonAttribute : Attribute {
    public JsonAttribute(string path) {
        m_path = path;
    }

    internal string GetPath() => m_path;

//----------------------------------------------------------------------------------------------------------------------

    private readonly string m_path;
}
} //end namespace