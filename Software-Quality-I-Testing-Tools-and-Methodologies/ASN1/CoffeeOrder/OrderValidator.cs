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

        if (toppings.Count > Constants.MAX_SYRUP_TOPPINGS) {return true;}


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

        if (syrups.Count > Constants.MAX_SYRUP_TOPPINGS) {return true;}

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
        return size > Constants.MAX_SCALE_SIZE || size < Constants.MIN_SCALE_SIZE;
    }

    public static bool checkTemp(byte temp){
        return temp > Constants.MAX_SCALE_SIZE || temp < Constants.MIN_SCALE_SIZE;
    }

    public static bool checkShots(byte shots, string milk){
        return shots < Constants.MIN_SCALE_SIZE | shots > Constants.MAX_SHOT_COUNT || (shots > Constants.MIN_SCALE_SIZE && milk == "None");
    }
}