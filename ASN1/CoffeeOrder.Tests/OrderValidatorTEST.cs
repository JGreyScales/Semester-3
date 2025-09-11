using System.Configuration.Assemblies;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
        bool result = orderValidator.checkSyrups(emptyStringList);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void validate_toppingsNotPresent_returnsFalse()
    {
        // Arrange
        List<string> emptyStringList = [];

        // Act
        bool result = orderValidator.checkToppings(emptyStringList);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void validate_milkNotPresent_returnsFalse(){
        // Arrange
        string? milk = null;

        // Act
        bool result = orderValidator.checkMilk(milk);

        // Assert
        Assert.IsFalse(result);
    }

    public void validate_milkIsNone_returnsFalse(){
        // Arrange
        string milk = "None";

        // Act
        bool result = orderValidator.checkMilk(milk);

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
        bool result = orderValidator.checkToppings(syrups);

        // Assert
        Assert.IsTrue(result);
    }

    public void validate_milkNotValid_returnsTrue(){
        // Arrange
        string milk = "rock";

        // Act
        bool result = orderValidator.checkMilk(milk);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void validate_toppingsNotValid_returnsTrue()
    {
        // Arrange
        List<string> toppings = new List<string>(["invalidOption"]);

        // Act
        bool result = orderValidator.checkToppings(toppings);

        // Assert
        Assert.IsTrue(result);
        
    }

    // Negative Cases
    [TestMethod]
    public void errorCheck_baseDrinkNotPresent_returnsTrue()
    {
        // Arrange
        string? baseDrink = null;

        // Act
        bool result = orderValidator.checkBaseDrink(baseDrink);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void errorCheck_sizeNotPresent_returnsTrue()
    {
        // Arrange
        byte? size = null;
        
        // Act
        bool result = orderValidator.checkSize(size);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void errorCheck_tempNotPresent_returnsTrue()
    {
        // Arrange
        byte? temp = null;
        
        // Act
        bool result = orderValidator.checkTemp(temp);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void errorCheck_shotsNotPresent_returnsTrue()
    {
        // Arrange
        byte? shots = null;
        string milk = "Milk";
        
        // Act
        bool result = orderValidator.checkShots(shots, milk);

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
        bool result = orderValidator.checkShots(shots, milk);

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
        bool result = orderValidator.checkShots(shots, milk);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void errorCheck_milkNoneShotsMoreThan0_returnsTrue(){
        // Arrange
        byte shots = 1;
        string milk = "None";

        // Act
        bool result = orderValidator.checkShots(shots, milk);

        // Assert
        Assert.IsTrue(result);
    }
}
