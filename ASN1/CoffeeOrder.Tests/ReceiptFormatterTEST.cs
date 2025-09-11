using Microsoft.VisualBasic;

namespace CoffeeOrder.Tests;

[TestClass]
public sealed class ReceiptFormatterTests
{
    private Order orderOBJ;
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

        orderOBJ = new Order();
        orderOBJ.addBeverage(bev1);
        orderOBJ.addBeverage(bev2);
        orderOBJ.addName("Test Name");
        orderOBJ.addDate();
        orderOBJ.addDiscounts();

    }

    // Typical cases
    [TestMethod]
    public void validate_lineItemsValid_returnTrue()
    {
        // Arrange
        string expectedContain = "Latte";
        using (StringWriter sw = new StringWriter()){
            Console.SetOut(sw);

        // Act
            ReceiptFormatter.printReceipt(orderOBJ);
            string output = sw.ToString();


        // Assert
        StringAssert.Contains(output, expectedContain);
        }

    }

    [TestMethod]
    public void validate_discountsValid_returnsTrue()
    {
        // Arrange
        string expectedContain = "BUY ONE GET ONE FREE SAME PRICE OR CHEAPER";
        string expectedContain2 = "Discount:";
        using (StringWriter sw = new StringWriter()){
            Console.SetOut(sw);

        // Act
            ReceiptFormatter.printReceipt(orderOBJ);
            string output = sw.ToString();


        // Assert
        StringAssert.Contains(output, expectedContain);
        StringAssert.Contains(output, expectedContain2);

        }
    }

    [TestMethod]
    public void validate_subTotalsValid_returnsTrue()
    {
        // Arrange
        string expectedContain = "7.60";
        using (StringWriter sw = new StringWriter()){
            Console.SetOut(sw);

        // Act
            ReceiptFormatter.printReceipt(orderOBJ);
            string output = sw.ToString();


        // Assert
        StringAssert.Contains(output, expectedContain);
        }
    }

        [TestMethod]
    public void validate_totalsValid_returnsTrue()
    {
        // Arrange
        string expectedContain = "3.80";
        using (StringWriter sw = new StringWriter()){
            Console.SetOut(sw);

        // Act
            ReceiptFormatter.printReceipt(orderOBJ);
            string output = sw.ToString();


        // Assert
        StringAssert.Contains(output, expectedContain);
        }
    }

    [TestMethod]
    public void validate_warningsValid_returnsTrue()
    {
        // Arrange
        string expectedContain = "Dairy";
        using (StringWriter sw = new StringWriter()){
            Console.SetOut(sw);

        // Act
            ReceiptFormatter.printReceipt(orderOBJ);
            string output = sw.ToString();


        // Assert
        StringAssert.Contains(output, expectedContain);
        }
    }

    // 5 minute grace period
    [TestMethod]
    public void validate_validDOC_retunsTrue()
    {
    string expectedContain = DateTime.Now.ToString("yyyy-MM-dd HHmm");
    // remove the most accurate form of measure to give a 10 minute window of error
    expectedContain = expectedContain.Substring(0, expectedContain.Length - 1);
        using (StringWriter sw = new StringWriter()){
            Console.SetOut(sw);

        // Act
            ReceiptFormatter.printReceipt(orderOBJ);
            string output = sw.ToString();


        // Assert
        StringAssert.Contains(output, expectedContain);
        }
    }

    [TestMethod]
    public void validate_nameIncluded_returnsTrue()
    {
        // Arrange
        string expectedContain = "Test Name";
        using (StringWriter sw = new StringWriter()){
            Console.SetOut(sw);

        // Act
            ReceiptFormatter.printReceipt(orderOBJ);
            string output = sw.ToString();


        // Assert
        StringAssert.Contains(output, expectedContain);
        }
    }

    // Edge cases

    // Negative cases
}
