using System.Linq;
using System.Runtime.InteropServices; // using LINQ for fast List<string> parsing

public class BeverageClassifier {

    // Properties

    // Constructor

    // Methods

    // Cannot include
    // - Espresso Foam
    // - Latte
    // - Espresso
    // - Cappuccino
    // - Coffee
    // - Chai-Tea
    public static bool isDecaf(Beverage beverageOBJ)
    {
        return beverageOBJ.getBaseDrink() == "Decaf" && !beverageOBJ.getToppings().Any(str => str.Contains("Espresso Foam"));
    }

    public static bool isVegan(Beverage beverageOBJ){
        List<string> milkOptions = new List<string> {"None", "Oat Milk", "Soy Milk", "Almond Milk"};
        List<string> toppingOptions = new List<string> {"Cinnamon", "Matcha"};
        List<string> syrupOptions = new List<string> {"Vanilla"};

        if (!milkOptions.Any(str => str.Contains(beverageOBJ.getMilk()))) {return false;}

        foreach (string topping in beverageOBJ.getToppings()){
            if (!toppingOptions.Any(str => str.Contains(topping))) {return false;}
        }

        foreach (string syrup in beverageOBJ.getSyrups()){
            if (!syrupOptions.Any(str => str.Contains(syrup))) {return false;}
        }
        return true;
    }

    public static bool isKidSafe(Beverage beverageOBJ){
        return beverageOBJ.getTemp() <= 75 && !beverageOBJ.getToppings().Any(str => str.Contains("Espresso Foam"));
    }

    public static List<string> getAllergens(Beverage beverageOBJ){
        List<string> Allergens =  new List<string>([]);

        if (beverageOBJ.getMilk() == "Milk") {Allergens.Add("Contains Dairy");}
        if (beverageOBJ.getMilk() == "Cream") {Allergens.Add("Contains Dairy");}
        if (beverageOBJ.getMilk() == "Oat Milk") {Allergens.Add("Contains Gluten");}
        if (beverageOBJ.getMilk() == "Soy Milk") {Allergens.Add("Contains Soy");}
        if (beverageOBJ.getMilk() == "Almond Milk") {Allergens.Add("Contains Nuts");}

        if (beverageOBJ.getSyrups().Any(str => str.Contains("Chocolate"))) {
            Allergens.Add("Contains Dairy");
            Allergens.Add("Contains Nuts");
        }

        if (beverageOBJ.getToppings().Any(str => str.Contains("Espresso Foam"))) {
            Allergens.Add("Contains Dairy");
        }


        return Utils.dedupeStringList(Allergens);
    }
}