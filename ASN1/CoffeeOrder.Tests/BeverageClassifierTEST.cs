namespace CoffeeOrder.Tests;

[TestClass]
public sealed class BeverageClassifier
{

    // Typical cases
    [TestMethod]
    public void check_plantMilk_returnsAllegenFlag()
    {
    }

    [TestMethod]
    public void check_lessThanVeryHot_returnsKidFriendly()
    {
    }

    [TestMethod]
    public void check_noEspresso_returnsKidFriendly()
    {
    }

    [TestMethod]
    public void check_decaf_returnsDecaf()
    {
    }

    [TestMethod]
    public void check_caffeine_returnsFalseDecaf()
    {
    }

    [TestMethod]
    public void check_Vegan_returnsIsVegan()
    {
    }
}
