namespace CoffeeOrder.Tests;

[TestClass]
public sealed class PromotionHelper
{

    // Typical cases
    [TestMethod]
    public void validate_happyHourOnHotDrink_returnsTrue()
    {
    }

    [TestMethod]
    public void validate_happyHourOnVeryHotDrink_returnsTrue()
    {
    }
    
    [TestMethod]
    public void validate_bogoOnCheaperDrink_returnsTrue()
    {
    }
    
    // Edge cases
    [TestMethod]
    public void validate_bogoOnEqualDrink_returnsTrue()
    {
    }


    // Negative cases

    [TestMethod]
    public void validate_happyHourOnColdDrink_returnsFalse()
    {
    }

    
    [TestMethod]
    public void validate_happyHourOnVeryColdDrink_returnsFalse()
    {
    }

    [TestMethod]
    public void validate_bogoOnMoreExpensiveDrink_returnsFalse()
    {
    }









}
