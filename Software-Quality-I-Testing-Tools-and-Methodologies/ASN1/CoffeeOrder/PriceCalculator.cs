public class PriceCalculator {
    // Properties
    
    // Constructor

    // Methods
    public static decimal roundValueToNickle(decimal value){
        // Multiply by 20 to scale to nearest 0.05
        value *= 20;

        // Round to nearest whole number (which gives us the nearest multiple of 0.05)
        value = Math.Round(value);

        // Divide by 20 to scale back down to original decimal place
        value /= 20;

        return value;
    }

    public static decimal calculatePrice(Beverage BeverageOBJ){
        decimal price = 0.0M;
        // my brain wasnt braining, I kept tryina make a function to model this
        // but its not linear, and I just opted to do this
        decimal size = (decimal)BeverageOBJ.getSize();
        if (size == 100M) {
            price = 2.0M;
        } else {
            price = (0.01M*size)+0.75M;
        }

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

        return roundValueToNickle(price * 1.13M);
    }

    public static decimal calculateOrderPriceWithDiscount(Order orderOBJ){
        decimal total = 0.0m;

        foreach (Beverage bev in orderOBJ.getBeverages()){
            total += bev.getPrice();
        }
        
        total /= 1.13M; // remove tax from the calculation

        orderOBJ.addDiscounts();
        total -= orderOBJ.getDiscount().getDiscount();
        return roundValueToNickle(total * 1.13M); // add the tax back now that the discount is applied
    }
}