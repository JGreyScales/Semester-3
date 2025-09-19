namespace CoffeeOrder.Tests;

[TestClass]
public sealed class utilTESTS{

    private enum filledEnum {low = 25, mid = 50, high = 75}
    private filledEnum filledEnumOBJ {get;}
    private enum emptyEnum {};
    private emptyEnum emptyEnumObj {get;}

    // typical tests
    [TestMethod]
    public void valid_enums_returnsCloset(){
        // Arrange
        byte value = 56;
        string expectedResult = "mid";

        // Act
        string result = Utils.getClosetEnum(filledEnumOBJ, value);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]
    public void valid_twoStringLists_returnsStringList(){
        // Arrange
        List<string> list1 = new List<string>{"a", "b"};
        List<string> list2 = new List<string>{"1", "2"};
        string expectedResult0 = "a";
        string expectedResult1 = "b";
        string expectedResult2 = "1";
        string expectedResult3 = "2";

        // Act
        List<string> result = Utils.mergeStringLists(list1, list2);

        // Assert
        Assert.AreEqual(expectedResult0, result[0]);
        Assert.AreEqual(expectedResult1, result[1]);
        Assert.AreEqual(expectedResult2, result[2]);
        Assert.AreEqual(expectedResult3, result[3]);
    }

    [TestMethod]
    public void validate_dedupeStringList_returnsStringList(){
        // Arrange
        List<string> emptyList = new List<string>{"index0", "index0", "index1", "index2", "index1"};
        string expectedResult0 = "index0";
        string expectedResult1 = "index1";
        string expectedResult2 = "index2";

        // Act
        List<string> result = Utils.dedupeStringList(emptyList);

        // Assert
        Assert.AreEqual(expectedResult0, result[0]);
        Assert.AreEqual(expectedResult1, result[1]);
        Assert.AreEqual(expectedResult2, result[2]);
    }

    // edge cases
    [TestMethod]
    public void errorCheck_emptyListDedupe_returnsStringList(){
        // Arrange
        List<string> emptyList = new List<string>([]);
        int expectedResultCount = 0;

        // Act
        int result = Utils.dedupeStringList(emptyList).Count;

        // Assert
        Assert.AreEqual(expectedResultCount, result);
    }


    // negative cases
    [TestMethod]
    public void errorCheck_emptyEnum_returnsString(){
        // Arrange
        byte value = 204;
        string expectedResult = "";

        // Act
        string result = Utils.getClosetEnum(emptyEnumObj, value);

        // Assert
        Assert.AreEqual(expectedResult, result);

    }

    [TestMethod]
    public void errorCheck_oneStringListNull_returnsStringList(){
        // Arrange
        List<string> list1 = new List<string>();
        List<string> list2 = new List<string>{"xx"};
        int expectedCount = 1;

        // Act
        int result = Utils.mergeStringLists(list1, list2).Count;

        // Assert
        Assert.AreEqual(expectedCount, result);
    }

    [TestMethod]
    public void errorCheck_twoStringListsNull_returnsStringList(){
        // Arrange
        List<string> list1 = new List<string>();
        List<string> list2 = new List<string>();
        int expectedCount = 0;

        // Act
        int result = Utils.mergeStringLists(list1, list2).Count;

        // Assert
        Assert.AreEqual(expectedCount, result);
    }
    
}