using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;


namespace Unity.FilmInternalUtilities.Tests {

internal class EnumUtilityTests {

    [Test]
    public void ConvertEnumToValueList() {
        int numElements = m_enumValues.Count;
        NUnit.Framework.Assert.Greater(numElements,0);
        for (int i = 0; i < numElements; ++i) {
            Assert.AreEqual(m_enumValues[i], (TestEnum)(i));
        }
    }

    [Test]
    public void ConvertEnumToInspectorNames() {
        List<string> inspectorNames = EnumUtility.ToInspectorNames(typeof(TestEnum));
        string[]     names         = Enum.GetNames(typeof(TestEnum));
        Assert.AreEqual(names.Length, inspectorNames.Count);
                    
        Assert.AreEqual(INSPECTOR_NAME_0, inspectorNames[0]);
        Assert.AreEqual(INSPECTOR_NAME_1, inspectorNames[1]);
        Assert.AreEqual(INSPECTOR_NAME_2, inspectorNames[2]);
        Assert.AreEqual(INSPECTOR_NAME_3, inspectorNames[3]);
        Assert.AreEqual(names[4], inspectorNames[4]);
    }
    

//----------------------------------------------------------------------------------------------------------------------
    
    enum TestEnum {
        [InspectorName(INSPECTOR_NAME_0)] FOO = 0,
        [InspectorName(INSPECTOR_NAME_1)] BAR,
        [InspectorName(INSPECTOR_NAME_2)] FOO_BAR,
        [InspectorName(INSPECTOR_NAME_3)] GOO,
        HOGE,        
    };
    
    const string INSPECTOR_NAME_0 ="foo";
    const string INSPECTOR_NAME_1 ="bar";
    const string INSPECTOR_NAME_2 ="Foo_Bar";
    const string INSPECTOR_NAME_3 ="GoO";
    
    
    private readonly List<TestEnum> m_enumValues = EnumUtility.ToValueList<TestEnum>();
    
}
 

        
        
} //end namespace
