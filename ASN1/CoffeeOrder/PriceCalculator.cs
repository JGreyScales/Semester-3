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
        return 0.0m;
    }

    public static decimal calculateOrderPriceWithDiscount(Order orderOBJ){
        return 0.0m;
    }
}