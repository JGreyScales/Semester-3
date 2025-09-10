namespace CoffeeOrder.Tests;

[TestClass]
public sealed class Beverage
{

    // Typical cases
    [TestMethod]
    public void valid_latte_returnsDrink()
    {
    }

    [TestMethod]
    public void valid_coffee_returnsDrink()
    {
    }

    [TestMethod]
    public void valid_tea_returnsDrink()
    {
    }

    [TestMethod]
    public void valid_espresso_returnsDrink()
    {
    }

    [TestMethod]
    public void valid_cappuccino_returnsDrink()
    {
    }


    [TestMethod]
    public void valid_small_returnsDrink()
    {
    }

    [TestMethod]
    public void valid_medium_returnsDrink()
    {
    }

    [TestMethod]
    public void valid_large_returnsDrink()
    {
    }

    [TestMethod]
    public void valid_extraLarge_returnsDrink()
    {
    }

    [TestMethod]
    public void check_hasPrice_returnsPrice()
    {
    }

    // Edge cases
    [TestMethod]
    public void invalid_minimumShots_returnsFailureFlag()
    {
    }

    [TestMethod]
    public void invalid_maximumShots_returnsFailureFlag()
    {
    }

    [TestMethod]
    public void invalid_maxSyrups_returnsFailureFlag()
    {
    }

    // Negative cases
    [TestMethod]
    public void invalid_baseDrink_returnsFailureFlag()
    {
    }

    [TestMethod]
    public void invalid_size_returnsFailureflag()
    {
    }

    [TestMethod]
    public void invalid_milkOption_returnsFailureFlag()
    {
    }

    [TestMethod]
    public void invalid_plantMilkOption_returnsFailureFlag()
    {
    }


    [TestMethod]
    public void invalid_syrupOption_returnsFailureFlag()
    {
    }

    [TestMethod]
    public void invalid_toppingsOption_returnsFailureFlag()
    {
    }
}
