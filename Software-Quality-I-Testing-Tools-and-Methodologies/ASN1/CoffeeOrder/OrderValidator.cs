using System.Linq;
public class OrderValidator{

    // Properties

    // Constructor
    public OrderValidator(){}   // empty constructor

    // Methods
    public static List<string> listErrors(Beverage beverageOBJ){
        List<string> Failures = new List<string>([]);
        
        if (OrderValidator.checkSyrups(beverageOBJ.getSyrups())){
            Failures.Add("Invalid Syrups Option");
        }

        if (OrderValidator.checkToppings(beverageOBJ.getToppings())){
            Failures.Add("Invalid Toppings Option");
        }

        if (OrderValidator.checkBaseDrink(beverageOBJ.getBaseDrink())){
            Failures.Add("Invalid BaseDrink Option");
        }

        if (OrderValidator.checkMilk(beverageOBJ.getMilk())){
            Failures.Add("Invalid Milk Option");
        }

        if (OrderValidator.checkSize(beverageOBJ.getSize())){
            Failures.Add("Invalid Size Option");
        }

        if (OrderValidator.checkTemp(beverageOBJ.getTemp())){
            Failures.Add("Invalid temp Option");
        }

        if (OrderValidator.checkShots(beverageOBJ.getShots(), beverageOBJ.getMilk())){
            Failures.Add("Invalid Shots Amount");
        }

        return Failures; 
    }

    public static bool checkToppings(List<string> toppings){

        if (toppings.Count > 5) {return true;}


        List<string> validOptions = new List<string> {"Cinnamon", "Milk Foam", "Espresso Foam", "Matcha"};

        foreach(string topping in toppings){
            // if any syrup is not in the list of valids
            if (!validOptions.Any(str => str.Contains(topping))){
                return true;
            }
        }
        return false;
    }

    public static bool checkSyrups(List<string> syrups){

        if (syrups.Count > 5) {return true;}

        List<string> validOptions = new List<string> {"Chocolate", "Strawberry", "Vanilla"};

        foreach(string syrup in syrups){
            // if any topping is not in the list of valids
            if (!validOptions.Any(str => str.Contains(syrup))){
                return true;
            }
        }
        return false;
    }

    public static bool checkBaseDrink(string baseDrink){
        if (baseDrink == null){return true;}

        List<string> validOptions = new List<string> {"Coffee", "Chai-Tea", "Latte", "Espresso", "Cappuccino", "Decaf"};

        // if any topping is not in the list of valids
        if (!validOptions.Any(str => str.Contains(baseDrink))){
            return true;
        }
        return false;
    }

    public static bool checkMilk(string milk){
        if (milk == null) {return true;}

        List<string> validOptions = new List<string> {"None", "Milk", "Cream", "Oat Milk", "Soy Milk", "Almond Milk"};

        // if any topping is not in the list of valids
        if (!validOptions.Any(str => str.Contains(milk))){
            return true;
        }
        return false;
    }

    public static bool checkSize(byte size){
        return size > 100 || size < 0;
    }

    public static bool checkTemp(byte temp){
        return temp > 100 || temp < 0;
    }

    public static bool checkShots(byte shots, string milk){
        return shots < 0 | shots > 4 || (shots > 0 && milk == "None");
    }
}