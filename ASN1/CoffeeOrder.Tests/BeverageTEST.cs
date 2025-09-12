namespace CoffeeOrder.Tests;

[TestClass]
public sealed class BeverageTests
{

    // Typical cases
    [TestMethod]
    public void valid_drink_returnsDrink()
    {

        // Arrange
        List<string> toppings = new List<string>{"Milk Foam", "Matcha"};
        List<string> syrups = new List<string>{"Vanilla"};
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 0;
        List<string> Expectedtoppings = new List<string> {"Milk Foam", "Matcha"};
        List<string> Exceptedsyrups = new List<string> {"Vanilla"};
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

        // Assert
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
        Assert.AreEqual(Expectedtoppings, bev.getToppings());
        Assert.AreEqual(Exceptedsyrups, bev.getSyrups());

    }


    [TestMethod]
    public void validate_getSizeString_returnsString(){
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 3;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        string expectedResult = "Large";

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

        string result = bev.getSizeString();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]
    public void validate_getTempString_returnString(){
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 3;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        string expectedResult = "Hot";

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

        string result = bev.getTempString();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }


    // Edge cases
    [TestMethod]
    public void invalid_minimumShots_returnsFailureFlag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        int value = -1;
        byte shots = (byte)value; // truncates & wraps to 255
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid Shots Amount";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    [TestMethod]
    public void invalid_maximumShots_returnsFailureFlag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 5;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid Shots Amount";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    [TestMethod]
    public void invalid_maxSyrups_returnsFailureFlag()
    {
         // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla", "Strawberry", "Chocolate", "Chocolate", "Vanilla", "Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid Syrups Option";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    // Negative cases
    [TestMethod]
    public void invalid_baseDrink_returnsFailureFlag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Orange Juice";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid BaseDrink Option";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    [TestMethod]
    public void invalid_size_returnsFailureflag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 125;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid Size Option";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    [TestMethod]
    public void invalid_temp_returnsFailureflag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 125;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid temp Option";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    [TestMethod]
    public void invalid_milkOption_returnsFailureFlag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Rocks";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid Milk Option";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    [TestMethod]
    public void invalid_syrupOption_returnsFailureFlag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla", "Poprocks"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid Syrup Option";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }

    [TestMethod]
    public void invalid_toppingsOption_returnsFailureFlag()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha", "Pebbles"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        int expectedFailureCount = 1;
        string expectedFailureMessage = "Invalid Toppings Option";

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

        // Assert
        Assert.AreEqual(expectedFailureCount, bev.getFailures().Count);
        Assert.AreEqual(expectedFailureMessage, bev.getFailures()[0]);
    }
}
