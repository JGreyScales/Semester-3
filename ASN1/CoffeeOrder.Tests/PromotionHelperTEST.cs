namespace CoffeeOrder.Tests;

[TestClass]
public sealed class PromotionHelperTests
{

    // Typical cases
    [TestMethod]
    public void validate_happyHourOnHotDrink_returnsDiscount()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "HAPPYHOUR 20% OFF HOT DRINK";
        decimal expectedDiscount = 0.75m;

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

        List<Beverage> bevList = new List<Beverage>([bev1]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }

    [TestMethod]
    public void validate_happyHourOnVeryHotDrink_returnsDiscount()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 100;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "HAPPYHOUR 20% OFF HOT DRINK";
        decimal expectedDiscount = 0.75m;

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

        List<Beverage> bevList = new List<Beverage>([bev1]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }
    
    [TestMethod]
    public void validate_bogoOnCheaperDrink_returnsDiscount()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> toppings2 = new List<string>();
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 25;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "BUY ONE GET ONE FREE SAME PRICE OR CHEAPER";
        decimal expectedDiscount = 2.45m;

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
            toppings: toppings2,
            syrups: syrups,
            shots: shots,
            milk: milk,
            temp: temp,
            size: size,
            drink: drink
        );

        List<Beverage> bevList = new List<Beverage>([bev1, bev2]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }

    [TestMethod]
    public void validate_selectsBOGOWhenBetterDeal_returnsBOGODiscount(){

        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> toppings2 = new List<string>();
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 25;
        byte temp2 = 100;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "BUY ONE GET ONE FREE SAME PRICE OR CHEAPER";
        decimal expectedDiscount = 2.45m;

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
            toppings: toppings2,
            syrups: syrups,
            shots: shots,
            milk: milk,
            temp: temp2,
            size: size,
            drink: drink
        );

        List<Beverage> bevList = new List<Beverage>([bev1, bev2]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }

    [TestMethod]
    public void validate_selectsHappyHourWhenBetterDeal_returnsHappyHourDiscount(){
// Arrange
        List<string> toppings = new List<string>();
        List<string> toppings2 = new List<string>(["Matcha", "Matcha"]);
        List<string> syrups = new List<string>();
        byte shots = 0;
        byte shots2 = 4;
        string milk = "None";
        string milk2 = "Almond Milk";
        byte temp = 25;
        byte temp2 = 100;
        byte size = 25;
        byte size2 = 100;
        string drink = "Espresso";
        string drink2 = "Latte";

        string expectedDiscountReason = "HAPPYHOUR 20% OFF HOT DRINK";
        decimal expectedDiscount = 1.70m;

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
            toppings: toppings2,
            syrups: syrups,
            shots: shots2,
            milk: milk2,
            temp: temp2,
            size: size2,
            drink: drink2
        );

        List<Beverage> bevList = new List<Beverage>([bev1, bev2]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }
    
    // Edge cases
    [TestMethod]
    public void validate_bogoOnEqualDrink_returnsDiscount()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "BUY ONE GET ONE FREE SAME PRICE OR CHEAPER";
        decimal expectedDiscount = 3.80m;

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

        List<Beverage> bevList = new List<Beverage>([bev1, bev2]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }


    // Negative cases


    [TestMethod]
    public void vlaidate_happyHourOnRoomTempDrink_returnsNoDiscount(){
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 50;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "";
        decimal expectedDiscount = 0m;

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

        List<Beverage> bevList = new List<Beverage>([bev1]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }

    [TestMethod]
    public void validate_happyHourOnColdDrink_returnsNoDiscount()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 25;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "";
        decimal expectedDiscount = 0m;

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

        List<Beverage> bevList = new List<Beverage>([bev1]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }

    
    [TestMethod]
    public void validate_happyHourOnVeryColdDrink_returnsNoDiscount()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 0;
        byte size = 75;
        string drink = "Latte";

        string expectedDiscountReason = "";
        decimal expectedDiscount = 0m;

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

        List<Beverage> bevList = new List<Beverage>([bev1]);
        var PromotionHelperOBJ = new PromotionHelper(bevList);
    
        // Assert
        Assert.AreEqual(expectedDiscount, PromotionHelperOBJ.getDiscount());
        StringAssert.Equals(expectedDiscountReason, PromotionHelperOBJ.getDiscountReason());
    }
}
