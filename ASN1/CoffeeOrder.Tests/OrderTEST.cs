using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoffeeOrder.Tests;
[TestClass]
public sealed class OrderTests {
    // Typical cases
    [TestMethod]
    public void validate_addDate_returnString()
    {
        // Arrange
        var orderOBJ = new Order();
        string expectedResult = DateTime.Now.ToString("yyyy-MM-dd HHmm");
        
        // Act
        orderOBJ.addDate();

        // Assert
        StringAssert.Equals(expectedResult, orderOBJ.getDate());
    }

    [TestMethod]
    public void validate_addBeverage_returnList(){
        // Arrange
        var orderOBJ = new Order();
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedResultLength = 2;

        // Act
        var bev1 = new Beverage(
            toppings: toppings,
            syrups: syrups,
            shots: shots,
            milk: milk,
            temp: temp,
            size: size,
            drink: drink
        );

        var bev2 = new Beverage(
            toppings: toppings,
            syrups: syrups,
            shots: shots,
            milk: milk,
            temp: temp,
            size: size,
            drink: drink
        );

        orderOBJ.addBeverage(bev1);
        orderOBJ.addBeverage(bev2);
        List<Beverage> orderOBJList = orderOBJ.getBeverages();

        // Assert
        Assert.AreEqual(expectedResultLength, orderOBJList.Count);
        Assert.ReferenceEquals(bev1, orderOBJList[0]);
        Assert.ReferenceEquals(bev2, orderOBJList[1]);
    }

    [TestMethod]
    public void validate_addName_returnString(){
        // Arrange
        var orderOBJ = new Order();
        string name = "testerName";
        string expectedResult = "testerName";

        // Act
        orderOBJ.addName(name);

        // Assert
        StringAssert.Equals(expectedResult, orderOBJ.getName());
    }

    [TestMethod]
    public void validate_addDiscount_returnDiscountOBJ(){
        // Arrange
        var orderOBJ = new Order();
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        string expectedReason = "HAPPYHOUR 20% OFF HOT DRINK";
        decimal expectedDiscount = 0.75m; // 0.76 = (3.8 * 0.8) || rounded to the nickel

        // Act
        var bev1 = new Beverage(
            toppings: toppings,
            syrups: syrups,
            shots: shots,
            milk: milk,
            temp: temp,
            size: size,
            drink: drink
        );

        orderOBJ.addBeverage(bev1);
        orderOBJ.addDiscounts();
        var discountOBJ = orderOBJ.getDiscount();

        // Assert
        Assert.AreEqual(expectedDiscount, discountOBJ.getDiscount());
        StringAssert.Equals(expectedReason, discountOBJ.getDiscountReason());
    }

    // Edge cases

    [TestMethod]
    public void validate_addDiscountEmptyBeverages_returnDiscountOBJ(){
        // Arrange
        var orderOBJ = new Order();

        decimal expectedDiscount = 0.0m;
        string expectedReason = "";

        // Act
        orderOBJ.addDiscounts();
        var discountOBJ = orderOBJ.getDiscount();

        // Assert
        Assert.AreEqual(expectedDiscount, discountOBJ.getDiscount());
        StringAssert.Equals(expectedReason, discountOBJ.getDiscountReason());
    }

    // Negative cases
}