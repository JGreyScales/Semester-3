public class PriceCalculator {
    // Properties
    
    // Constructor

    // Methods
    public static decimal roundValueToNickle(decimal value){
        decimal valueToAdd = 0M;
        int hundredthsPlace = (int)(value * 100) % 10;

        // we can type cast this since we know it will be a single digit value & not overflow
        if (hundredthsPlace < 3) {
            valueToAdd = (decimal)hundredthsPlace * -1;
        } else {
            valueToAdd = 5M - (decimal)hundredthsPlace;
        }

        value += (valueToAdd / 100);
        return Math.Round(value,2);
    }

    public static decimal calculatePrice(Beverage BeverageOBJ){
        decimal price = 0.0M;
        price += ((1/75) * BeverageOBJ.getSize()) + (2/3);
        // milk price
        switch (BeverageOBJ.getMilk())
            {
                case "None": price *= 0.7M; break;
                case "Milk": break; // multiply by 1.0
                case "Cream": break; // multiply by 1.0
                case "Oat Milk": price *= 1.3M; break;
                case "Soy Milk": price *= 1.2M; break;
                case "Almond Milk": price *= 1.5M; break;
                default: price *= 0.0M; break;
            }

        if (BeverageOBJ.getShots() != 0){
            price *= (0.3M * BeverageOBJ.getShots()) + 0.4M;
        }

        return price * 1.13M;
    }

    public static decimal calculateOrderPriceWithDiscount(Order orderOBJ){
        return 0.0m;
    }
}