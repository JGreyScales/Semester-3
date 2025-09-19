/// ORDER FOR NAME
/// DOC: yyyy-MM-dd HHmm
/// 
/// LINE ITEM 1
/// LINE ITEM 2
/// LINE ITEM 3
/// LINE ITEM ...
/// 
/// SUBTOTAL: XX.XX
/// DISCOUNT: XX.XX
/// TAX: XX.XX
/// TOTAL: XX.XX
/// 
/// ALLERGEN NOTICES:
/// LINE ITEM 1
/// LINE ITEM 2
/// LINE ITEM 3
/// LINE ITEM ...
///
/// OTHER NOTICES:
/// Is Decaf: (true/false)
/// Is Kid Friendly: (true/false)
/// Is Vegan: (true/false)

public class ReceiptFormatter {
    // Properties

    // Constructor

    // Methods
    public static void printReceipt(Order orderOBJ){
        // $ for formatted string literals to allow for inline var calls
        List<Beverage> beverages = orderOBJ.getBeverages();
        bool isDecaf = true;
        bool isKidFriendly = true;
        bool isVegan = true;
        List<string> allergenList = new List<string>([]);
        decimal total = 0.0m;


        Console.WriteLine($"ORDER FOR: {orderOBJ.getName()}");
        Console.WriteLine($"DOC: {orderOBJ.getDate()}\n"); // empty line at end
        for (byte i = 0; i < beverages.Count; i++){
            total += beverages[i].getPrice();
            if (!beverages[i].getIsDecaf()) {isDecaf = false;}
            if (!beverages[i].getIsKidFriendly()) {isKidFriendly = false;}
            if (!beverages[i].getIsVegan()) {isVegan = false;}
            allergenList = Utils.mergeStringLists(allergenList, beverages[i].getAllergens());

            Console.Write(beverages[i].getSizeString());
            Console.Write($" {beverages[i].getTempString()}");
            Console.Write($" {beverages[i].getBaseDrink()}");
            Console.Write($" | {beverages[i].getShots()}");
            Console.Write($"-{beverages[i].getMilk()} | ");
            foreach (string syrup in beverages[i].getSyrups()){
                Console.Write($" {syrup} ");

            }

            Console.Write($" | ");
            foreach (string topping in beverages[i].getToppings()){
                Console.Write($" {topping} ");
            }
            Console.Write($"\n");
        }
        Console.WriteLine("");
        Console.WriteLine($"_SUBTOTAL: {PriceCalculator.roundValueToNickle(total / 1.13M)}");
        Console.WriteLine($"_DISCOUNT: {orderOBJ.getDiscount().getDiscount()} | {orderOBJ.getDiscount().getDiscountReason()}");
        Console.WriteLine($"_TAX: {PriceCalculator.roundValueToNickle(total - (total / 1.13M))}");
        Console.WriteLine($"TOTAL: {PriceCalculator.roundValueToNickle(total - orderOBJ.getDiscount().getDiscount())}");
        Console.Write($"\n");
        Console.WriteLine($"ALLERGEN NOTICES:");
        foreach (string allergen in allergenList){
            Console.WriteLine($" allergen");
        }
        Console.WriteLine($"\nOTHER NOTICES:");
        Console.WriteLine($" Is Decaf: {isDecaf}");
        Console.WriteLine($" Is Kid Friendly: {isKidFriendly}");
        Console.WriteLine($" Is Vegan: {isVegan}");
    }
}