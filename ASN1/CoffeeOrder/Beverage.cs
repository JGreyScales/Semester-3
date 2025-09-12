public class Beverage 

{

    private enum sizeEnum {
        Small = 25,
        Medium = 50,
        Large = 75,
        ExtraLarge = 100
    }

    private enum tempEnum {
        VeryCold = 0,
        Cold = 25,
        RoomTemp = 50,
        Hot = 75,
        VeryHot = 100
    }


    // Properties
    private string BaseDrink {set; get;}
    private byte Size {set; get;}
    private byte Temp {set; get;}
    private string Milk {set; get;}
    private byte Shots {set; get;}
    private List<string> Syrups {set; get;}
    private List<string> Toppings {set; get;}
    private bool IsDecaf {set; get;}
    private bool IsKidFriendly {set; get;}
    private bool isVegan {set; get;}
    private List<string> Allergens {set; get;}
    private decimal Price {set; get;}
    private List<string> Failures {set; get;}


    // Constructor
    public Beverage(string BaseDrink, byte Size, byte Temp, string Milk, byte Shots, List<string> Syrups, List<string> Toppings){
        this.BaseDrink = BaseDrink;
        this.Size = Size;
        this.Temp = Temp;
        this.Milk = Milk;
        this.Shots = Shots;
        this.Syrups = Syrups;
        this.Toppings = Toppings;

        
        this.Failures = OrderValidator.listErrors(this);
    }


    //Methods

    public List<string> getFailures(){
        return this.Failures;
    }

    public decimal getPrice(){
        return this.Price;
    }

    public List<string> getAllergens(){
        return this.Allergens;
    }

    public bool getIsVegan(){
        return this.isVegan;
    }

    public bool getIsKidFriendly(){
        return this.IsKidFriendly;
    }

    public bool getIsDecaf(){
        return this.IsDecaf;
    }

    public List<string> getToppings(){
        return this.Toppings;
    }

    public List<string> getSyrups(){
        return this.Syrups;
    }

    public byte getShots(){
        return this.Shots;
    }

    public string getMilk(){
        return this.Milk;
    }

    public byte getTemp(){
        return this.Temp;
    }

    public string getTempString(){
        // pass a random value to the function so it can self delcare the enumType
        // and then process the rest of the values
        return Utils.getClosetEnum(tempEnum.RoomTemp, this.getTemp());
    }

    public string getSizeString(){
        // pass a random value to the function so it can self delcare the enumType
        // and then process the rest of the values
        return Utils.getClosetEnum(sizeEnum.Medium, this.getSize());
    }

    public byte getSize(){
        return this.Size;
    }

    public string getBaseDrink(){
        return this.BaseDrink;
    }
}