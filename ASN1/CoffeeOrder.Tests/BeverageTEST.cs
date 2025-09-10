using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoffeeOrder.Tests;

[TestClass]
public sealed class BeverageTEST
{

    // Typical cases
    [TestMethod]
    public void valid_drink_returnsDrink()
    {

        // arrange
        List<string> toppings = new List<string>(["Milk Foam", "Macha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 0;
        List<string> Expectedtoppings = new List<string>(["Milk Foam", "Macha"]);
        List<string> Exceptedsyrups = new List<string>(["Vanilla"]);
        byte Exceptedshots = 2;
        string Exceptedmilk = "Milk";
        byte Exceptedtemp = 75;
        byte Exceptedsize = 75;
        string Excepteddrink = "Latte";
        bool ExpectedIsDecaf = false;
        bool ExpectedIsKidFriendly = false;
        bool ExpectedIsVegan = false;
        decimal ExpectedPrice = 3.80M; // 3.7855M unrounded
        int ExpectedAllergensCount = 0;

        // act
        var bev = new Beverage(
            toppings: toppings,
            syrups: syrups,
            shots: shots,
            milk: milk,
            temp: temp,
            size: size,
            drink: drink
        );

        // assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(Excepteddrink, bev.getBaseDrink());
        Assert.AreEqual(Exceptedmilk, bev.getMilk());
        Assert.AreEqual(Exceptedtemp, bev.getTemp());
        Assert.AreEqual(Exceptedsize, bev.getSize());
        Assert.AreEqual(ExpectedIsDecaf, bev.getIsDecaf());
        Assert.AreEqual(ExpectedIsKidFriendly, bev.getIsKidFriendly());
        Assert.AreEqual(ExpectedIsVegan, bev.getIsVegan());
        Assert.AreEqual(Exceptedshots, bev.getShots());
        Assert.AreEqual(ExpectedPrice, bev.getPrice());
        Assert.AreEqual(ExpectedAllergensCount, bev.getAllergens().Count);
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
