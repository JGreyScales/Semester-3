namespace CoffeeOrder.Tests;

[TestClass]
public sealed class PriceCalculatorTests
{
    // Typical cases
    [TestMethod]
    public void validate_priceAccuracyCheck_returnsDecimal()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>([]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        decimal ExpectedPrice = 3.20M; // 3.2205M unrounded

        // Act
        var bev = new Beverage(
            Toppings: toppings,
            Syrups: syrups,
            Shots: shots,
            Milk: milk,
            Temp: temp,
            Size: size,
            BaseDrink: drink
        );
        decimal result = PriceCalculator.calculatePrice(bev);

        // Assert
        Assert.AreEqual(ExpectedPrice, result);


    }

    [TestMethod]
    public void validate_OrderPriceCalculationWithDiscount_returnsDecimal()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>([]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        decimal ExpectedPrice = 2.55M; // 3.2205M unrounded * 0.8 = 2.56

        // Act
        var bev = new Beverage(
            Toppings: toppings,
            Syrups: syrups,
            Shots: shots,
            Milk: milk,
            Temp: temp,
            Size: size,
            BaseDrink: drink
        );
        Order orderOBJ = new Order();
        orderOBJ.addBeverage(bev);
        orderOBJ.addDiscounts();
        decimal result = PriceCalculator.calculateOrderPriceWithDiscount(orderOBJ);

        // Assert
        Assert.AreEqual(ExpectedPrice, result);
    }

    [TestMethod]
    public void validate_roundValueToNickle_returnsDecimal(){
        // Arrange
        decimal value = 3.72425M;
        decimal expectedResult = 3.70M;

        // Act
        decimal result = PriceCalculator.roundValueToNickle(value);

        // Result
        Assert.AreEqual(expectedResult, result);
    }

    // Edge cases
    [TestMethod]
    public void invalid_beverageList_returnsDecimal(){
        // Arrange
        Order orderOBJ = new Order();
        decimal expectedResult = 0.0m;

        // Act
        decimal result = PriceCalculator.calculateOrderPriceWithDiscount(orderOBJ);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }


    [TestMethod]
    public void validate_roundValueToNickleRoundDown_returnsDecimal(){
        // Arrange
        decimal value = 3.72M;
        decimal expectedResult = 3.70M;

        // Act
        decimal result = PriceCalculator.roundValueToNickle(value);
        
        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]
    public void validate_roundValueToNickleRoundUp_returnsDecimal(){
        //Arrange
        decimal value = 3.73M;
        decimal expectedResult = 3.75M;

        // Act
        decimal result = PriceCalculator.roundValueToNickle(value);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }


    // Negative cases
}
