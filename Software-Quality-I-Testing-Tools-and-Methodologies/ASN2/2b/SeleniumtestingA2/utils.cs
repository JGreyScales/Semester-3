using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace utils {
    class Utils {
        public void enterData(
            // fills in the dataform data
            string firstName,
            string lastName,
            string address,
            string city,
            string postalCode,
            string phoneNumber,
            string EmailAddress,
            CanadianProvince province,
            int participants,
            AvaliableDays selectedDay,
            IWebDriver driver)
        {
            try {
                IWebElement firstNameInput = driver.FindElement(By.Id("firstName"));
                IWebElement lastNameInput = driver.FindElement(By.Id("lastName"));
                IWebElement addressInput = driver.FindElement(By.Id("address"));
                IWebElement cityInput = driver.FindElement(By.Id("city"));
                IWebElement postalCodeInput = driver.FindElement(By.Id("postalCode"));
                IWebElement phoneNumberInput = driver.FindElement(By.Id("phone"));
                IWebElement emailAddressInput = driver.FindElement(By.Id("email"));
                IWebElement provinceInput = driver.FindElement(By.Id("province"));
                SelectElement selectProvince = new SelectElement(provinceInput);
                IWebElement participantsInput = driver.FindElement(By.Id("numberOfParticipants"));
                IWebElement selectedDayInput = driver.FindElement(By.Id("daysRegistered"));
                SelectElement selectDay = new SelectElement(selectedDayInput);


                firstNameInput.SendKeys(firstName);
                lastNameInput.SendKeys(lastName);
                addressInput.SendKeys(address);
                cityInput.SendKeys(city);
                postalCodeInput.SendKeys(postalCode);
                phoneNumberInput.SendKeys(phoneNumber);
                emailAddressInput.SendKeys(EmailAddress);
                selectProvince.SelectByIndex((int)province); // select by index to ensure that the list integritity is intact on the GUI
                participantsInput.SendKeys(participants.ToString());
                selectDay.SelectByIndex((int)selectedDay);
            } catch (NoSuchElementException) {
                return;
            }

            return;
        }

        public void submit(IWebDriver driver)
        // submits the dataform
        {
            try {
                IWebElement registerButton = driver.FindElement(By.Id("btnSubmit"));
                registerButton.Click();
            } catch (NoSuchElementException) {
                return;
            }

            return;
        }

        public bool validateDropDownValue(string expected, IWebElement dropdown){
            // checks the value of a dropdown element with a string value
            SelectElement select = new SelectElement(dropdown);
            IWebElement selectedOption = select.SelectedOption;
            string? actual = selectedOption.GetAttribute("value");

            return string.Equals(expected, actual, StringComparison.Ordinal); // Ordinal = byte-wise string checking
        }

        public string? retrievePriceValue(IWebElement priceElement){
            return priceElement.GetAttribute("value");
        }
    }
}