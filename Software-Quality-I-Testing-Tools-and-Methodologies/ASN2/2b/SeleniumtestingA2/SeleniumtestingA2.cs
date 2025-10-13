using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CA1859 // Use concrete types when possible for improved performance

public enum CanadianProvince
{
    Alberta,
    British_Columbia,
    Manitoba,
    New_Brunswick,
    Newfoundland_and_Labrador,
    Northwest_Territories,
    Nova_Scotia,
    Nunavut,
    Ontario,
    Prince_Edward_Island,
    Quebec,
    Saskatchewan,
    Yukon_Territory
}


public enum AvaliableDays {
    Day1,
    Day2,
    BothDays
}

namespace SeleniumtestingA2
{

    [TestClass]
    public sealed class province_tests
    {

        IWebDriver driver;


        [TestInitialize]
        public void setUp()
        {
            ChromeOptions handlingSSL = new ChromeOptions();
            handlingSSL.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(handlingSSL);
            driver.Navigate().GoToUrl("https://localhost/register/index.html");
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        private void enterData(            
            string firstName,
            string lastName,
            string address,
            string city,
            string postalCode,
            string phoneNumber,
            string EmailAddress,
            CanadianProvince province,
            int participants,
            AvaliableDays selectedDay)
        {
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
            selectProvince.SelectByIndex((int)province);
            participantsInput.SendKeys(participants.ToString());
            selectDay.SelectByIndex((int)selectedDay);
            return;
        }

        private void submit(){
            IWebElement registerButton = driver.FindElement(By.Id("btnSubmit"));
            registerButton.Click();
            return;
        }


        [TestMethod]
        public void input_province_alberta()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_british_columbia()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_manitoba()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_new_brunswick()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_newfoundland_and_labrador()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_northwest_territories()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_nova_scotia()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_nunavut()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_ontario()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_prince_edward_island()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_quebec()
        {
            // arrange

            // act

            // assert
            
        }

        [TestMethod]
        public void input_province_saskatchewan()
        {
            // arrange

            // act

            // assert
            
        }

        // Yukon does not exist in the form, and only 12 test cases are allowed
    }


    [TestClass]
    public sealed class participant_tests
    {
        IWebDriver driver;

        [TestInitialize]
        public void setUp()
        {
            ChromeOptions handlingSSL = new ChromeOptions();
            handlingSSL.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(handlingSSL);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestMethod2()
        {
        }

        [TestMethod]
        public void TestMethod3()
        {
        }

        [TestMethod]
        public void TestMethod4()
        {
        }

        [TestMethod]
        public void TestMethod5()
        {
        }

        [TestMethod]
        public void TestMethod6()
        {
        }
    }

    [TestClass]
    public sealed class registrants_tests
    {

        IWebDriver driver;


        [TestInitialize]
        public void setUp()
        {
            ChromeOptions handlingSSL = new ChromeOptions();
            handlingSSL.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(handlingSSL);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }


        [TestMethod]
        public void day1Test1()
        {
        }

        [TestMethod]
        public void day1Test2()
        {
        }

        [TestMethod]
        public void day1Test3()
        {
        }

        [TestMethod]
        public void day2Test1()
        {
        }

        [TestMethod]
        public void day2Test2()
        {
        }

        [TestMethod]
        public void day2Test3()
        {
        }


        //  day 3 is day 1 + day 2
        [TestMethod]
        public void day3Test1()
        {
        }

        [TestMethod]
        public void day3Test2()
        {
        }

        [TestMethod]
        public void day3Test3()
        {
        }

        [TestMethod]
        public void FiveOrMoreParticipants1()
        {
        }

        [TestMethod]
        public void FiveOrMoreParticipants2()
        {
        }

        [TestMethod]
        public void FiveOrMoreParticipants3()
        {
        }
    }

    [TestClass]
    public sealed class navigation_tests
    {
        IWebDriver driver;

        [TestInitialize]
        public void setUp()
        {
            ChromeOptions handlingSSL = new ChromeOptions();
            handlingSSL.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(handlingSSL);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestMethod2()
        {
        }

        [TestMethod]
        public void TestMethod3()
        {
        }
    }
}
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.