// this is a nice & contained way to handle multiple beverages & then apply discounts on them as a group entity
public class Order {
    // Properties
    private List<Beverage> Beverages {set; get;} 
    private PromotionHelper Discount {set; get;} // update
    private string Name {set; get;}
    private string Date {set; get;}


    // Constructor
    public Order(){
        this.Name = "";
        this.Date = ""; // set to suppress a compiler warning saying Date must not be nullable when existing the constructor
        this.addDate();
        this.Beverages = new List<Beverage>();
        this.Discount = new PromotionHelper(this.Beverages);
    }

    // Methods

    // false on failure
    public bool addBeverage(Beverage beverageOBJ){
        // an order cannot have more than 20 beverages
        if (this.Beverages.Count < Constants.MAX_BEVERAGES){
            this.Beverages.Add(beverageOBJ);
            return true;
        }
        return false;
    }

    public List<Beverage> getBeverages(){
        return this.Beverages;
    }

    public void addName(string name){
        this.Name = name;
    }

    public string getName(){
        return this.Name;
    }

    private void addDate(){
        this.Date = DateTime.Now.ToString("yyyy-MM-dd HHmm");
    }

    public string getDate(){
        return this.Date;
    }

    public void addDiscounts(){
        PromotionHelper promotionOBJ = new PromotionHelper(this.Beverages);
        this.Discount = promotionOBJ;
    }

    public PromotionHelper getDiscount(){
        return this.Discount;
    }
}