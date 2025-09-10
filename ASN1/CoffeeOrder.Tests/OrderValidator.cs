namespace CoffeeOrder.Tests;

[TestClass]
public sealed class OrderValidator
{
    // Typical Cases

    [TestMethod]
    public void validate_milkNotPresent_returnsTrue()
    {
    }
    // Edge Cases
    [TestMethod]
    public void validate_dairyAndPlantMilk_returnsFalse()
    {
    }
    // Negative Cases
    [TestMethod]
    public void errorCheck_baseDrinkNotPresent_returnsFalse()
    {
    }

    [TestMethod]
    public void errorCheck_sizeNotPresent_returnsFalse()
    {
    }

    [TestMethod]
    public void errorCheck_tempNotPresent_returnsFalse()
    {
    }

    [TestMethod]
    public void errorCheck_minimumShotCountInvalid_returnsFalse()
    {
    }

    [TestMethod]
    public void errorCheck_maximumShotCountInvalid_returnsFalse()
    {
    }

    [TestMethod]
    public void errorCheck_maximumSyrupCountInvalid_returnsFalse()
    {
    }

}
