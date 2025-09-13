public class PromotionHelper{
    // Properties
    private decimal discount {get; set;}
    private string discountReason {set; get;}
    // Constructor
    public PromotionHelper(List<Beverage> beverages)
    {
        // calculate the discounts based on the beverage list, add it to the properties
        var bestDeal = (0.0M, ""); // declare tuple
        Beverage? mostExpensiveBev = beverages.OrderByDescending(o => o.getPrice()).FirstOrDefault(); // default is null
        foreach (Beverage bev in beverages){
            // BOGO test
            if (bev.getTemp() > 50) {
                decimal dealAmount = bev.getPrice() * 0.2M; // how much will be discounted
                if (dealAmount > bestDeal.Item1){
                    bestDeal.Item1 = dealAmount;
                    bestDeal.Item2 = "HAPPYHOUR 20% OFF HOT DRINK";
                }
            }
    
            if (bev != mostExpensiveBev){ //ensure we arent looking at the same memory address
                decimal dealAmount = bev.getPrice();
                if (dealAmount > bestDeal.Item1){
                    bestDeal.Item1 = dealAmount;
                    bestDeal.Item2 = "BUY ONE GET ONE FREE SAME PRICE OR CHEAPER";
                }
            }
        }

        discount = PriceCalculator.roundValueToNickle(bestDeal.Item1);
        discountReason = bestDeal.Item2;

    }

    // Methods
    public decimal getDiscount(){
        return this.discount;
    }

    public string getDiscountReason(){
        return this.discountReason;
    }
}