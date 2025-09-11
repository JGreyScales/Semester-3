# INTRO
Build a small Coffee Shop Order Builder with a clean class design and a robust unit test

# Technology & Conventions
- Language/Framework: C# + MSTest
-  Projects:
    -  `CoffeeOrder` (production code)
    -  `CoffeeOrder.Tests` (MSTest unit tests)
-  Naming: Explanatory test method names, e.g., `Validate_MissingBase_ReturnsInvalid()`
-  AAA: Every test must follow Arrange–Act–Assert with blank lines separating sections.
- Floating point: Not required beyond simple price arithmetic; if used, keep to `decimal` for currency.
- Minimum test count: ≥ 25 test methods spanning validator, classifier, calculator, promotions, and formatter classes.





# Required Class Set (7 classes)

These are required to guide architecture and enable modular, testable design. Implement public APIs you deem appropriate (signatures below are suggestions).

---
**Price calculations**
*decimals = multiplication*
*$ values = additions*
*starts from lowest scope & expands out, bringing the scope value with it*
(((
size: small: $1, medium: $1.25, large: $1.50, extraLarge: $2.00
milk: None: 0.7, milk: 1.0, cream: 1.0, oat milk: 1.3, soy milk: 1.2, almond milk: 1.5
shots: 0: 1.0, 1: 0.7, 2: 1.0, 3: 1.3, 4: 1.6
)
  drink: Coffee: 1.0, Chai-Tea: 1.1, Latte: 1.3, Espresso: 0.8, Cappuccino: 1.2, Decaf: 0.9
  )
    syrups: chocolate: $1.0, strawberry: $1.0, vanilla: $0.5
    toppings: cinnamon: $0.3, milk foam: $0.2, espresso foam: $0.5, matcha: $0.7
    )
      TAX: 1.13

---
**Allergen Causers**
- Milk (dairy)
- Cream (dairy)
- Oat Milk (gluten)
- Soy milk (soy)
- Almond milk (nuts)
- Chocolate (nuts & dairy)
- Milk Foam (dairy)
- Esoresso Foam (dairy)

**Vegan Options**
- *milk*
  - None
  - Oat Milk
  - Soy Milk
  - Almond Milk
- *toppings*
  - Cinnomon
  - Matcha
- *syrups*
  - Vanilla

---
## 1. Beverage (model)

- **Properties:**  
  - `BaseDrink` (string) (Coffee, Chai-Tea, Latte, Espresso, Cappuccino, Decaf)  
  - `Size` (byte) (0-100) (small: 25, medium: 50, large: 75, extra large: 100)
  - `Temp` (byte) (0-100) (veryCold: 0, cold: 25, roomtemp: 50, hot: 75 , veryHot: 100)
  - `Milk` (string) (None, milk, cream, oat milk, soy milk, almond milk)  
  - `Shots` (byte) (0–4)  
  - `Syrups` (List<string>) (0–5, list)  (Chocolate, Strawberry, Vanilla)
  - `Toppings` (List<string>) (Cinnamon, Milk Foam, Espresso Foam, Matcha)
  - `IsDecaf` (bool)
  - `IsKidFriendly` (bool)
  - `isVegan` (bool)
  - `Allergens` (List<string>)
  - `Price` (decimal)
  - `Failures` (List<string>)

- **Methods**
  - `Constructor`(toppings: list<String>, syrups: list<String>, shots: byte, milk: str, temp: byte, size: byte, drink: str) -> Beverage object
  - `getFailures` -> List<string>
  - `getPrice` -> decimal
  - `getAllergens` -> List<string>
  - `getIsVegan` -> bool
  - `getIsKidFriendly` -> bool
  - `getToppings` -> List<string>
  - `getSyrups` -> List<string>
  - `getShots` -> byte
  - `getMilk` -> str
  - `getTemp` -> byte
  - `getSize` -> byte
  - `getBaseDrink` -> str

- **Requirements:**  
  - Keep it simple and immutable where possible (or avoid mutating after construction).

---

## 2. OrderValidator

- **Responsibility:**  
  - Validates a single `Beverage`

- **methods**
  - `listErrors`(Beverage: Beverage) -> list(string)
  - static `checkSyrups`(List<string>) -> bool
  - static `checkToppings`(List<string>) -> bool
  - static `checkBaseDrink`(string) -> bool
  - static `checkMilk`(string) -> bool
  - static `checkSize`(byte) -> bool
  - static `checkTemp`(byte) -> bool
  - static `checkShots`(byte, string) -> bool

- **Example Rules:**  
  - Required fields present  
  - Incompatible options (e.g., Hot *and* Iced)  
  - Dairy *and* plant milk not both selected  
  - Shot/syrup limits  
  - Allergen flags (nut milks)  

- **Output:**  
  - Return a structured result (e.g., `ValidationResult` with `IsValid` + `Errors`).

# Order
- **Properties:** 
  - `Beverages` (list<BeverageObjects>)
  - `Discount` (PromotionalDiscountObject)
  - `Name` (string)
  - `Date` (string)

- **methods**
  - `Constructor`(void)
  - `addBeverage`(Beverage: Beverage) -> void
  - `getBeverages`() -> List<Beverage>
  - `addName`(name: string) -> void
  - `getName`() -> string
  - static `addDate`(void) -> void
  - `getDate`(void) -> string
  - `getDiscount`(void) -> PromotionalDiscountObject
  - `addDiscounts`(void) -> void



---

## 3. BeverageClassifier

- **Methods**
  - static `isDecaf`(Beverage: Beverage) -> bool
  - static `isVegan`(Beverage: Beverage) -> bool
  - static `isKidSafe`(Beverage: Beverage) -> bool
  - static `getAllergens`(Beverage: Beverage) -> List<string>

- **Responsibility:**  
  - Categories/labels: `Caffeinated/Decaf`, `DairyFree`, `VeganFriendly`, `KidSafe` (e.g., no espresso).  

- **Constraints:**  
  - Keep logic independent of I/O and pricing.

---

## 4. PriceCalculator

- **Methods**
  - static `calculatePrice`(Beverage: Beverage) -> decimal
  - static `calculateOrderPriceWithDiscount`(Order) -> decimal

- **Responsibility:**  
  - Calculate base price by size + add-on pricing  
  - Calculate totals per beverage and for the order  

- **Constraints:**  
  - Use **decimal** for currency; avoid floating-point rounding issues.

---

## 5. PromotionHelper
- **Properties**
  - `discount` (decimal)
  - `discountReason` (string)

- **Methods**
  - `constructor`(Beverages: list(Beverage)) -> object
  - `getDiscount`() -> decimal
  - `getDiscountReason`() -> String

  **Private**
  - BOGODiscount(Beverages: list(Beverage)) -> decimal
  - happyHourDiscount(Beverages: list(Beverage)) -> decimal
  - selectBestDiscount(tuple(decimal, discountName)) -> void

- **Responsibility:**  
  - Apply coupons/promos (e.g., BOGO, `HAPPYHOUR` 20% off **Hot** drinks only).  
    -`BOGO`
    -`HAPPYHOUR`

- **Output:**  
  - Return applied discounts/justification separate from pricing logic to preserve testability.

---

## 6. ReceiptFormatter
- **Methods**
  - `PrintReceipt`(Order: OrderOBJ) -> void

- **Responsibility:**  
  - Produce receipt text with line items, discounts, totals, and warnings (e.g., allergens).  

- **Requirements:**  
  - Ensure the receipt text includes your name and the date/time of creation.  
  - Keep formatting deterministic for easy assertions.

---

## 7. AppDriver (thin UI/CLI)
- **Methods**
  - `Main`(void)
  - static `printWelcomeScreen`(void) -> void
  - `GetInt`(void) -> int
  - `getDrink`(void) -> string
  - `getBeverageSize`(void) -> int
  - `getBeverageTemp`(void) -> int
  - `getMilkType`(void) -> string
  - `getShotCount`(void) -> int
  - `getSyrups`(void) -> list(string)
  - `getToppings`(void) -> list(string)
  - `getName`(void) -> string

- **Responsibility:**  
  - Minimal glue to demonstrate building an order and printing a receipt.  

- **Note:**  
  - While needed, it is not the grading focus.


# Functional Requirements (examples to anchor your design)

- **Required fields:**  
  - `BaseDrink` and `Size` must be present.

- **Mutually exclusive:**  
  - `Temp` is either `Hot` or `Iced` (not both).

- **Milk selection:**  
  - May specify *either* dairy **or** plant milk (or neither), **not both**.

- **Limits:**  
  - `Shots` in [0..4],  
  - `Syrups.Count` in [0..5];  
  - Negative counts invalid.

- **Allergen:**  
  - Plant milks like Almond should flag **“contains tree nuts”** (warning or error—your design; be consistent).

- **Classification:**  
  - `KidSafe` if no espresso shots (or `IsDecaf == true`) and `Temp != “ExtraHot”` (if you model temperature granularity).

- **Pricing:**  
  - Base price by `Size` + per-addon prices.

- **Promotions:**  
  - e.g., `HAPPYHOUR` (20% off **Hot** drinks only);  
  - `BOGO` (buy one get one of equal/lesser value free once per order).

- **Receipt:**  
  - Shows line items, discounts, totals; includes allergen notices when applicable.

> You do not need to implement every rule above to 100% breadth. Aim for a coherent feature set that supports thorough testing across the classes, with at least 25 unit tests overall.

---

# Testing Requirements

- Write tests that cover:

  - **Typical cases:**  
    - valid latte/tea with common customizations

  - **Edge cases:**  
    - max shots/syrups,  
    - empty lists,  
    - no milk,  
    - iced chocolate for kids

  - **Negative cases:**  
    - missing base/size,  
    - dairy + plant milk,  
    - invalid counts,  
    - coupon applied to ineligible drink

- Use AAA in all tests and explanatory names.

- Keep tests fast, autonomous, and isolated (no external I/O; pure functions preferred).



# Submission Quality (what we expect)

- Project builds and tests run without modification in a clean environment.
- Clear structure, names, and consistent conventions.
- No extraneous binaries; keep repo/zip lean.
- Deterministic tests (no randomness, time-dependence, or external I/O).

---

# Evaluation Rubric (100%)

| Criterion           | Weight | What to Demonstrate                                                                                                                                       |
|---------------------|--------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|
| Correctness of Tests| 25%    | Assertions accurately validate behavior; tests fail when behavior is wrong and pass when corrected. Two (2) Red tests are present showing TDD development. |
| Test Coverage       | 20%    | Typical + edge + negative cases across required classes; no obvious gaps in critical rules.                                                              |
| Framework Usage     | 15%    | MSTest conventions, AAA structure, explanatory names, clean fixtures.                                                                                    |
| Code Quality        | 15%    | Production and test code are readable, small, single-purpose; SRP applied; no duplication after refactor.                                                |
| Documentation/Comments | 10%  | Concise comments explaining non-obvious intent; `README.md` with how-to-run.                                                                             |
| Reflection          | 5%     | Thoughtful Start–Stop–Continue connecting choices to outcomes.                                                                                           |
| Submission Quality  | 10%    | Compiles, clean structure, correct naming, reproducible test run.                                                                                       |

---

# Notes & Tips

- Prefer pure functions and small objects; they are easier to test thoroughly.
- Keep I/O out of the core logic. The `AppDriver` should be thin.
- Make promotions deterministic: define precise conditions and test them explicitly.
- If you add new rules, add new tests first, then implement.
- When tests feel awkward to write, inspect your design — it’s often a hint to refactor.
