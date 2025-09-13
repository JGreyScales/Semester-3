public class PriceCalculator {
    // Properties
    
    // Constructor

    // Methods
    public static decimal roundValueToNickle(decimal value){
        decimal valueToAdd = 0M;
        int hundredthsPlace = (int)(value * 100) % 10;

        // we can type cast this since we know it will be a single digit value & not overflow
        if (hundredthsPlace < 3) {
            valueToAdd = ((decimal)hundredthsPlace * -1) -1;
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
                default: break;
            }

        if (BeverageOBJ.getShots() != 0){
            price *= (0.3M * BeverageOBJ.getShots()) + 0.4M;
        }

        switch (BeverageOBJ.getBaseDrink()) 
            {
                case "Coffee": break; // multiply by 1.0
                case "Chai-Tea": price *= 1.1M; break;
                case "Latte": price *= 1.3M; break;
                case "Espresso": price *= 0.8M; break;
                case "Cappuccino": price *= 1.2M; break;
                case "Decaf": price *= 0.9M; break;
                default: break;
            }

        foreach (string syrup in BeverageOBJ.getSyrups()){
            switch (syrup) 
                {
                    case "Chocolate": // overflow into Strawberry case (aka, two valid cases for the same output)
                    case "Strawberry": price += 1.0M; break;
                    case "Vanilla": price += 0.5M; break;
                    default: break;
                }
        }

        foreach (string topping in BeverageOBJ.getToppings()){
            switch (topping)
                {
                    case "Cinnamon": price += 0.3M; break;
                    case "Milk Foam": price += 0.2M; break;
                    case "Espresso Foam": price += 0.5M; break;
                    case "Matcha": price += 0.7M; break;
                    default: break;
                }
        }

        return price * 1.13M;
    }

    public static decimal calculateOrderPriceWithDiscount(Order orderOBJ){
        decimal total = 0.0m;

        foreach (Beverage bev in orderOBJ.getBeverages()){
            total += bev.getPrice();
        }
        
        total /= 1.13M; // remove tax from the calculation

        orderOBJ.addDiscounts();
        total -= orderOBJ.getDiscount().getDiscount();
        return total * 1.13M; // add the tax back now that the discount is applied
    }
}