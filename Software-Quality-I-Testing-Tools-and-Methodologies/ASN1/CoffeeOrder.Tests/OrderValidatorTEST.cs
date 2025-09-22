#pragma warning disable CS8604 // Possible null reference argument.


namespace CoffeeOrder.Tests;

[TestClass]
public sealed class OrderValidatorTests
{
    // Typical Cases
    [TestMethod]
    public void validate_syrupsNotPresent_returnsFalse()
    {
        // Arrange
        List<string> emptyStringList = [];

        // Act
        bool result = OrderValidator.checkSyrups(emptyStringList);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void validate_toppingsNotPresent_returnsFalse()
    {
        // Arrange
        List<string> emptyStringList = [];

        // Act
        bool result = OrderValidator.checkToppings(emptyStringList);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void validate_milkNotPresent_returnsTrue(){
        // Arrange
        string? milk = null;

        // Act
        bool result = OrderValidator.checkMilk(milk);

        // Assert
        Assert.IsTrue(result);
    }

    public void validate_milkIsNone_returnsFalse(){
        // Arrange
        string milk = "None";

        // Act
        bool result = OrderValidator.checkMilk(milk);

        // Assert
        Assert.IsFalse(result);
    }

    // Edge Cases
    [TestMethod]
    public void validate_syrupsNotValid_returnsTrue()
    {
        // Arrange
        List<string> syrups = new List<string>(["invalidOption"]);

        // Act
        bool result = OrderValidator.checkToppings(syrups);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void validate_syrupsValid_returnsFalse()
    {
        // Arrange
        List<string> syrups = new List<string>(["Chocolate"]);

        // Act
        bool result = OrderValidator.checkSyrups(syrups);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void validate_milkNotValid_returnsTrue(){
        // Arrange
        string milk = "rock";

        // Act
        bool result = OrderValidator.checkMilk(milk);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void validate_milkValid_returnsFalse(){
        // Arrange
        string milk = "Cream";

        // Act
        bool result = OrderValidator.checkMilk(milk);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void validate_toppingsNotValid_returnsTrue()
    {
        // Arrange
        List<string> toppings = new List<string>(["invalidOption"]);

        // Act
        bool result = OrderValidator.checkToppings(toppings);

        // Assert
        Assert.IsTrue(result);
        
    }

    [TestMethod]
    public void validate_toppingsValid_returnsFalse()
    {
        // Arrange
        List<string> toppings = new List<string>(["Cinnamon"]);

        // Act
        bool result = OrderValidator.checkToppings(toppings);

        // Assert
        Assert.IsFalse(result);
        
    }
    

    // Negative Cases
    [TestMethod]
    public void errorCheck_baseDrinkNotPresent_returnsTrue()
    {
        // Arrange
        string? baseDrink = null;

        // Act
        bool result = OrderValidator.checkBaseDrink(baseDrink);

        // Assert
        Assert.IsTrue(result);
    }


    

    [TestMethod]
    public void errorCheck_shotsBelowMinimum_returnsTrue()
    {
        // Arrange
        int value = -1;
        byte shots = (byte)value;
        string milk = "Milk";

        // Act
        bool result = OrderValidator.checkShots(shots, milk);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void errorCheck_shotsAboveMaximum_returnsTrue()
    {
        // Arrange
        byte shots = 5;
        string milk = "Milk";

        // Act
        bool result = OrderValidator.checkShots(shots, milk);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void errorCheck_milkNoneShotsMoreThan0_returnsTrue(){
        // Arrange
        byte shots = 1;
        string milk = "None";

        // Act
        bool result = OrderValidator.checkShots(shots, milk);

        // Assert
        Assert.IsTrue(result);
    }
}
