using System.Collections.Generic;
using NUnit.Framework;


namespace Unity.FilmInternalUtilities.Tests {

internal class ListExtensionsTests {

    [Test]
    public void RemoveNullMembersInList() {
        RemoveNullMembersAndCheck(new List<string>() {"1", null});
        RemoveNullMembersAndCheck(new List<string>() {null});
        RemoveNullMembersAndCheck(new List<string>() {"1", "2", "3", null, null, "4" , "5"});
        RemoveNullMembersAndCheck(new List<string>() {"1", "2", "3", null});
        RemoveNullMembersAndCheck(new List<string>() {null, null, null, null, null});
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    [Test]
    public void MoveListElements() {

        List<int> l = new List<int>() { 1, 2, 3, 4, 5 };

        l.Move(2, 0);       
        Assert.IsTrue(l.AreElementsEqual(new List<int>() {3, 1, 2, 4, 5}));
        
        l.Move(3, 3);
        Assert.IsTrue(l.AreElementsEqual(new List<int>() {3, 1, 2, 4, 5}));

        l.Move(4, 3);
        Assert.IsTrue(l.AreElementsEqual(new List<int>() {3, 1, 2, 5, 4}));
        
        l.Move(0, 4);
        Assert.IsTrue(l.AreElementsEqual(new List<int>() {1, 2, 5, 4, 3}));

        l.Move(1, 3);
        Assert.IsTrue(l.AreElementsEqual(new List<int>() {1, 5, 4, 2, 3}));

        l.Move(0, 2);
        Assert.IsTrue(l.AreElementsEqual(new List<int>() {5, 4, 1, 2, 3}));
    }
    
    
    [Test]
    public void SetListCount() {
        
        List<int> l = new List<int>();
        SetListCountAndCheck(l, 10);
        SetListCountAndCheck(l, 100);
        SetListCountAndCheck(l, 3);
        SetListCountAndCheck(l, 0);
    }
    
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    void RemoveNullMembersAndCheck<T>(IList<T> list) {
        list.RemoveNullMembers();
        list.Loop((T member) => {
            Assert.IsNotNull(member);
        });
    }

    void SetListCountAndCheck<T>(List<T> list, int count) {
        list.SetCount(count);
        Assert.AreEqual(list.Count, count, $"List count: {list.Count}, expected: {count}. ");
    }
    
    
}
        
} //end namespace
