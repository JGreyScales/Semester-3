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
            toppings: toppings,
            syrups: syrups,
            shots: shots,
            milk: milk,
            temp: temp,
            size: size,
            drink: drink
        );
        decimal result = PriceCalculator.calculatePrice(bev);

        // Assert
        Assert.AreEqual(ExpectedPrice, result);


    }

    [TestMethod]
    public void validate_OrderPriceCalculationWithDiscount_returnsDecimal()
    {
    }

    // Edge cases
    [TestMethod]
    public void invalid_beverageList_returnsDecimal(){
        // Arrange
        List<Beverage> emptyList = new List<Beverage>([]);
        decimal expectedResult = 0.0m;

        // Act
        decimal result = PriceCalculator.calculateOrderPrice(emptyList);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
    // Negative cases






}
