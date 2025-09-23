using System;
using System.IO;

namespace CoffeeOrder.Tests;

[TestClass]
[DoNotParallelize] // because of the stream writers, this must not be parallelized
public sealed class ReceiptFormatterTests
{
    private Order orderOBJ = new Order();
    private StringWriter? stringWriter;
    [TestInitialize]
    public void Init(){
        // Arrange
        List<string> toppings = new List<string>(["Milk Foam", "Matcha"]);
        List<string> syrups = new List<string>(["Vanilla"]);
        byte shots = 2;
        string milk = "Milk";
        byte temp = 75;
        byte size = 75;
        string drink = "Latte";

        // Act
        var bev1 = new Beverage(
            Toppings: toppings,
            Syrups: syrups,
            Shots: shots,
            Milk: milk,
            Temp: temp,
            Size: size,
            BaseDrink: drink
        );

        var bev2 = new Beverage(
            Toppings: toppings,
            Syrups: syrups,
            Shots: shots,
            Milk: milk,
            Temp: temp,
            Size: size,
            BaseDrink: drink
        );

        orderOBJ.addBeverage(bev1);
        orderOBJ.addBeverage(bev2);
        orderOBJ.addName("Test Name");
        orderOBJ.addDiscounts();

        stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
    }


    [TestMethod]
    // because of how the method testing works, output kept getting mixed
    // I tried to increase & decrease the scope of test parralization and ended up just turning it completely off
    // issues persisted so this was a last result attempt

    // I ended up discovering how to fix parralization issues with the [DoNotParallelize] tag
    // but this was moments before submission so...
    public void validate_oneGo_returnsReceiptString()
    {
        // Arrange
        string expectedContain = "Latte";
        string expectedContain2 = "BUY ONE GET ONE FREE SAME PRICE OR CHEAPER";
        string expectedContain3 = "_DISCOUNT:";
        string expectedContain4 = "6.75";
        string expectedContain5 = "3.8";
        string expectedContain6 = "Dairy";
        string expectedContain7 = DateTime.Now.ToString("yyyy-MM-dd HHmm");
        string expectedContain8 = "Test Name";
        string expectedContain9 = "Is Decaf";
        string expectedContain10 = "Is Kid Friendly";
        string expectedContain11 = "Is Vegan";
        // remove the most accurate form of measure to give a 10 minute window of error
        expectedContain7 = expectedContain7[..^1]; // recommended quick fix for syntax simplfying by VSC dotnet extension

        // Act
        ReceiptFormatter.printReceipt(orderOBJ);
        Assert.IsNotNull(stringWriter); // this must be here to stop a compiler warning
        // I am very much an advocate for keeping all warnings active & resolving it
        string output = stringWriter.ToString();

        // Assert
        StringAssert.Contains(output, expectedContain);
        StringAssert.Contains(output, expectedContain2);
        StringAssert.Contains(output, expectedContain3);
        StringAssert.Contains(output, expectedContain4);
        StringAssert.Contains(output, expectedContain5);
        StringAssert.Contains(output, expectedContain6);
        StringAssert.Contains(output, expectedContain7);
        StringAssert.Contains(output, expectedContain8);
        StringAssert.Contains(output, expectedContain9);
        StringAssert.Contains(output, expectedContain10);
        StringAssert.Contains(output, expectedContain11);
    }

    // PENDING TESTS
    [TestMethod]
    public void validate_savingFileCreatesFile_returnsFile(){
        // Arrange
        string expectedFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReceiptFile.txt");

        // Act
        ReceiptFormatter.saveReceipt(orderOBJ); // assuming this just hijacks the printReceipt method using a streamwriter
        bool result = File.Exists(expectedFile);

        // Assert
        Assert.IsTrue(result);
    }


    [TestMethod]
    public void validate_savingFileHasFileContent_returnsString(){
        // Arrange
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReceiptFile.txt");
        string expectedContain = "Latte";

        // Act
        ReceiptFormatter.saveReceipt(orderOBJ); // assuming this just hijacks the printReceipt method using a streamwriter
        using StreamReader reader = new(filePath);
        string result = reader.ReadToEnd();

        // Assert
        StringAssert.Contains(result, expectedContain);
    }


    // // Typical cases
    // [TestMethod]
    // public void validate_lineItemsValid_returnTrue()
    // {
    //     // Arrange
    //     string expectedContain = "Latte";
    
    //     // Act
    //     ReceiptFormatter.printReceipt(orderOBJ);
    //     string output = stringWriter.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    // }

    // [TestMethod]
    // public void validate_discountsValid_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "BUY ONE GET ONE FREE SAME PRICE OR CHEAPER";
    //     string expectedContain2 = "Discount:";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     StringAssert.Contains(output, expectedContain2);

    //     }
    // }

    // [TestMethod]
    // public void validate_subTotalsValid_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "7.60";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    //     [TestMethod]
    // public void validate_totalsValid_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "3.80";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    // [TestMethod]
    // public void validate_warningsValid_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "Dairy";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    // // 5 minute grace period
    // [TestMethod]
    // public void validate_validDOC_retunsTrue()
    // {
    // string expectedContain = DateTime.Now.ToString("yyyy-MM-dd HHmm");
    // // remove the most accurate form of measure to give a 10 minute window of error
    // expectedContain = expectedContain.Substring(0, expectedContain.Length - 1);
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    // [TestMethod]
    // public void validate_nameIncluded_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "Test Name";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    // [TestMethod]
    // public void validate_isDecafIncluded_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "Is Decaf";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    // [TestMethod]
    // public void validate_isKidFriendlyIncluded_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "Is Kid Friendly";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    // [TestMethod]
    // public void validate_isVeganIncluded_returnsTrue()
    // {
    //     // Arrange
    //     string expectedContain = "Is Vegan";
    //     using (StringWriter sw = new StringWriter()){
    //         Console.SetOut(sw);

    //     // Act
    //         ReceiptFormatter.printReceipt(orderOBJ);
    //         string output = sw.ToString();


    //     // Assert
    //     StringAssert.Contains(output, expectedContain);
    //     }
    // }

    // // Edge cases

    // // Negative cases
}
