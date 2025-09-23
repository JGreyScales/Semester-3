using System.Reflection;

public class AppDriver{
    // Properties

    // Constructor
    public AppDriver(){
        while(true){
            // Map app flow is stored here.
            printWelcomeScreen();

            Order orderOBJ = new Order();
            bool ordering = true;
            while (ordering){
                Beverage bev = Main_GetDrink();
                if (bev.getFailures().Count > 0){
                    Console.WriteLine("Invalid Beverage");
                    continue;
                }
                orderOBJ.addBeverage(bev);

                Console.WriteLine("Do you want to add another drink?");
                Console.WriteLine(" 1. Yes");
                Console.WriteLine(" 2. No");
                byte answer = GetByte(1,2);
                if (answer == 2){ ordering = false;}
            }

            orderOBJ.addDiscounts();
            orderOBJ.addName(getName());


            ReceiptFormatter.printReceipt(orderOBJ);
        }

    }

    // Methods
    public Beverage Main_GetDrink(){
        return new Beverage(BaseDrink: getBaseDrink(), Size: getBeverageSize(), Temp:getBeverageTemp(), Milk:getMilkType(), Shots:getShotCount(), Syrups:getSyrups(), Toppings:getToppings());
    }
    public static void printWelcomeScreen(){
        // I tried to use AI to make this nice
        // and the most incomprehensible, delusional banner art was made.
        Console.WriteLine("Welcome to the Coffee Order Machine!"); 
        Console.WriteLine("Please interact with the machine to begin.");
        Console.ReadLine(); 

    }
    private static byte GetByte(byte min = 0, byte max = 255){
        // gets a single byte value from the user, upper & lower limits are optional
        byte answer = 255;
        while (answer > 200) {
            // if failure
            if(!byte.TryParse(Console.ReadLine(), out answer)){
                Console.WriteLine($"invalid byte, must be between {min}-{max}");
            } else if (min > answer || max < answer) {
                answer = 255;
                Console.WriteLine($"invalid byte, must be between {min}-{max}");
            }
        }
        return answer;
    }
    private static string getBaseDrink(){
        Console.WriteLine("Please select what base drink you want:");
        Console.WriteLine(" 1. Coffee");
        Console.WriteLine(" 2. Chai-Tea");
        Console.WriteLine(" 3. Latte");
        Console.WriteLine(" 4. Espresso");
        Console.WriteLine(" 5. Cappuccino");
        Console.WriteLine(" 6. Decaf");
        byte answer = GetByte(1, 6);
        switch(answer) {
            case(1): return "Coffee";
            case(2): return "Chai-Tea";
            case(3): return "Latte";
            case(4): return "Espresso";
            case(5): return "Cappuccino";
            case(6): return "Decaf";
            default: return "";
        }
    }

    private static byte getBeverageSize(){
        Console.WriteLine("Please select what size you want:");
        Console.WriteLine(" 1. Small");
        Console.WriteLine(" 2. Medium");
        Console.WriteLine(" 3. Large");
        Console.WriteLine(" 4. Extra Large");
        byte answer = GetByte(1, 4);
        switch(answer) {
            case(1): return 25;
            case(2): return 50;
            case(3): return 75;
            case(4): return 100;
            default: return 255;
        }
    }

    private static byte getBeverageTemp(){
        Console.WriteLine("Please select what temperature you want:");
        Console.WriteLine(" 1. Very Cold");
        Console.WriteLine(" 2. Cold");
        Console.WriteLine(" 3. Room Temp");
        Console.WriteLine(" 4. Hot");
        Console.WriteLine(" 5. Very Hot");
        byte answer = GetByte(1, 5);
        switch(answer) {
            case(1): return 0;
            case(2): return 25;
            case(3): return 50;
            case(4): return 75;
            case(5): return 100;
            default: return 255;
        }
    }

    private static string getMilkType(){
        Console.WriteLine("Please select what milk you want:");
        Console.WriteLine(" 1. None");
        Console.WriteLine(" 2. Milk");
        Console.WriteLine(" 3. Cream");
        Console.WriteLine(" 4. Oat Milk");
        Console.WriteLine(" 5. Soy Milk");
        Console.WriteLine(" 6. Almond Milk");
        byte answer = GetByte(1, 6);
        switch(answer) {
            case(1): return "None";
            case(2): return "Milk";
            case(3): return "Cream";
            case(4): return "Oat Milk";
            case(5): return "Soy Milk";
            case(6): return "Almond Milk";
            default: return "";
        }
    }

    private static byte getShotCount(){
        Console.WriteLine("Please select how many shots you want:");
        Console.WriteLine(" 1");
        Console.WriteLine(" 2");
        Console.WriteLine(" 3");
        Console.WriteLine(" 4");
        byte answer = GetByte(1, 4);
        return answer;
    }

    private static List<string> getSyrups(){
        List<string> syrups = new List<string>([]);
        byte loopCount = 0;
        bool adding = true;
        // the user can add up to 5 syrups
        while (adding && loopCount < Constants.MAX_SYRUP_TOPPINGS){
            Console.WriteLine("Please select what syrup you want:");
            Console.WriteLine(" 1. Stop adding syrups");
            Console.WriteLine(" 2. Chocolate");
            Console.WriteLine(" 3. Strawberry");
            Console.WriteLine(" 4. Vanilla");
            byte answer = GetByte(1, 4);
            switch(answer) {
                case(1): adding = false; break;
                case(2): syrups.Add("Chocolate"); break;
                case(3): syrups.Add("Strawberry"); break;
                case(4): syrups.Add("Vanilla"); break;
                default: break;
            }
            loopCount++;
        }
        return syrups;
    }

    private static List<string> getToppings(){
        List<string> toppings = new List<string>([]);
        byte loopCount = 0;
        bool adding = true;
        // the user can add up to 5 toppings
        while (adding && loopCount < Constants.MAX_SYRUP_TOPPINGS){
            Console.WriteLine("Please select what topping you want:");
            Console.WriteLine(" 1. Stop adding toppings");
            Console.WriteLine(" 2. Cinnamon");
            Console.WriteLine(" 3. Milk Foam");
            Console.WriteLine(" 4. Espresso Foam");
            Console.WriteLine(" 5. Matcha");
            byte answer = GetByte(1, 5);
            switch(answer) {
                case(1): adding = false; break;
                case(2): toppings.Add("Cinnamon"); break;
                case(3): toppings.Add("Milk Foam"); break;
                case(4): toppings.Add("Espresso Foam"); break;
                case(5): toppings.Add("Matcha"); break;
                default: break;
            }
            loopCount++;
        }
        return toppings;
    }

    private static string getName(){
        Console.Write("Please enter your name:");
        string? answer = null;
        while (answer == null){
            answer = Console.ReadLine();
        }
        Console.WriteLine("");
        return answer;
    }

}