using System.Collections.Generic;
using NUnit.Framework;


namespace Unity.FilmInternalUtilities.Tests {

internal class ListExtensionsTest {

    [Test]
    public void RemoveNullMembersInList() {
        RemoveNullMembersAndCheck(new List<string>() {"1", null});
        RemoveNullMembersAndCheck(new List<string>() {null});
        RemoveNullMembersAndCheck(new List<string>() {"1", "2", "3", null, null, "4" , "5"});
        RemoveNullMembersAndCheck(new List<string>() {"1", "2", "3", null});
        RemoveNullMembersAndCheck(new List<string>() {null, null, null, null, null});
    }

    
//----------------------------------------------------------------------------------------------------------------------       

    void RemoveNullMembersAndCheck<T>(IList<T> list) {
        list.RemoveNullMembers();
        foreach (T member in list) {
            Assert.IsNotNull(member);
        }
        
        
    }
}
 

        
        
} //end namespace
