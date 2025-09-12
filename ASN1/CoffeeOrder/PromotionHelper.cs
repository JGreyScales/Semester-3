public class PromotionHelper{
    // Properties
    private decimal discount {get; set;}
    private string discountReason {set; get;}
    // Constructor
    public PromotionHelper(List<Beverage> Beverages){}

    // Methods
    public decimal getDiscount(){
        return this.discount;
    }

    public string getDiscountReason(){
        return this.discountReason;
    }
}