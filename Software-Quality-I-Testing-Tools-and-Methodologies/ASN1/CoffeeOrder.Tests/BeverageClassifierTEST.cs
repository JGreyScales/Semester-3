namespace CoffeeOrder.Tests;

[TestClass]
public sealed class BeverageClassifierTests
{

    // Typical cases
    [TestMethod]
    public void check_plantMilkAllegenFlag_returnsList()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Almond Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        string expectedValue = "Contains Nuts";
        int expectedCount = 1;

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

        List<string> returnValue = BeverageClassifier.getAllergens(bev);

        // Assert
        Assert.AreEqual(expectedValue, returnValue[0]);
        Assert.AreEqual(expectedCount, returnValue.Count);
        
    }

    [TestMethod]
    public void check_lessThanVeryHotOrEspressoKidFriendly_returnsTrue()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Almond Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Coffee";

        bool expectedValue = true;

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

        bool actualValue = BeverageClassifier.isKidSafe(bev);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void check_decaf_returnsTrue()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Almond Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Decaf";

        bool expectedValue = true;

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

        bool actualValue = BeverageClassifier.isDecaf(bev);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void check_vegan_returnsTrue()
    {
        // Arrange
        List<string> toppings = new List<string>(["Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Almond Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Coffee";

        bool expectedValue = true;

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

        bool actualValue = BeverageClassifier.isVegan(bev);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    // Edge cases

    // Negative cases

    [TestMethod]
    public void check_decaf_returnsFalse()
    {
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Almond Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Coffee";

        bool expectedValue = false;

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

        bool actualValue = BeverageClassifier.isDecaf(bev);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void check_vegan_returnsFalse(){
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Coffee";

        bool expectedValue = false;

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

        bool actualValue = BeverageClassifier.isVegan(bev);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void check_EspressoKidFriendly_returnsFalse(){
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha", "Espresso Foam"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Almond Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Coffee";

        bool expectedValue = false;

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

        bool actualValue = BeverageClassifier.isKidSafe(bev);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void check_VeryHotKidFriendly_returnsFalse(){
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Almond Milk";
        byte temp = 100;
        byte size = 75;
        string drink = "Coffee";

        bool expectedValue = false;

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

        bool actualValue = BeverageClassifier.isKidSafe(bev);

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void check_plantMilkAllegenFlag_returnsEmptyList(){
        // Arrange
        List<string> toppings = new List<string>([]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 0;
        string milk = "None";
        byte temp = 75;
        byte size = 75;
        string drink = "Coffee";

        int expectedCount = 0;

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

        List<string> returnValue = BeverageClassifier.getAllergens(bev);

        // Assert
        Assert.AreEqual(expectedCount, returnValue.Count);
    }

}
