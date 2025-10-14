using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CA1859 // Use concrete types when possible for improved performance


public enum CanadianProvince
{
    AB,
    BC,
    MB,
    NB,
    NL,
    NT,
    NS,
    NU,
    ON,
    PE,
    QC,
    SK,
    YT
}


public enum AvaliableDays
{
    Day1,
    Day2,
    BothDays
}

namespace SeleniumtestingA2
{

    using utils;
    [TestClass]
    // KNOWN ERRORS IN THIS TEST_CLASS
    // error stems from phone number parsing in format (xxx)xxx-xxxx
    // error stems from Provinces list not being alphabetized 
    public sealed class province_tests
    {

        private IWebDriver driver;
        private Utils utils;


        [TestInitialize]
        public void setUp()
        {
            ChromeOptions handlingSSL = new ChromeOptions();
            handlingSSL.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(handlingSSL);
            driver.Navigate().GoToUrl("https://localhost/register/index.html");
            utils = new Utils();
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }


        [TestMethod]
        public void input_province_alberta()
        {
            // arrange

            string firstName = "Liam";
            string lastName = "Turner";
            string address = "123 Maple Street";
            string city = "Calgary";
            string postalCode = "T2P 1A4";
            string phoneNumber = "403-555-1234";
            string emailAddress = "liam.turner@example.com";
            CanadianProvince province = CanadianProvince.AB;
            int participants = 2;
            AvaliableDays selectedDay = AvaliableDays.Day1;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;

            // assert

            Assert.AreEqual(expectedValue, actualValue);
            // error stems from Provinces list not being alphabetized 
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_british_columbia()
        {
            // arrange
            string firstName = "Olivia";
            string lastName = "Nguyen";
            string address = "456 Oak Avenue";
            string city = "Edmonton";
            string postalCode = "T5K 0B2";
            string phoneNumber = "(780)555-5678";
            string emailAddress = "olivia.nguyen@example.com";
            CanadianProvince province = CanadianProvince.BC;
            int participants = 1;
            AvaliableDays selectedDay = AvaliableDays.BothDays;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            // error stems from phone number parsing in format (xxx)xxx-xxxx
            Assert.AreEqual(expectedValue, actualValue);
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_manitoba()
        {
            // arrange
            string firstName = "Ethan";
            string lastName = "Brown";
            string address = "789 Birch Road";
            string city = "Red Deer";
            string postalCode = "T4N 3T5";
            string phoneNumber = "403-555-4321";
            string emailAddress = "ethan.brown@example.com";
            CanadianProvince province = CanadianProvince.MB;
            int participants = 3;
            AvaliableDays selectedDay = AvaliableDays.Day2;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            Assert.AreEqual(expectedValue, actualValue);
            // error stems from Provinces list not being alphabetized 
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_new_brunswick()
        {
            // arrange
            string firstName = "Sophia";
            string lastName = "Martinez";
            string address = "321 Cedar Lane";
            string city = "Lethbridge";
            string postalCode = "T1J 1Z4";
            string phoneNumber = "(403)555-8765";
            string emailAddress = "sophia.martinez@example.com";
            CanadianProvince province = CanadianProvince.NB;
            int participants = 2;
            AvaliableDays selectedDay = AvaliableDays.Day1;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            // error stems from phone number parsing in format (xxx)xxx-xxxx
            Assert.AreEqual(expectedValue, actualValue);
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_newfoundland_and_labrador()
        {
            // arrange
            string firstName = "Noah";
            string lastName = "Wilson";
            string address = "654 Spruce Street";
            string city = "Medicine Hat";
            string postalCode = "T1A 3S4";
            string phoneNumber = "403-555-9876";
            string emailAddress = "noah.wilson@example.com";
            CanadianProvince province = CanadianProvince.NL;
            int participants = 1;
            AvaliableDays selectedDay = AvaliableDays.Day2;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            Assert.AreEqual(expectedValue, actualValue);
            // error stems from Provinces list not being alphabetized 
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_northwest_territories()
        {
            // arrange
            string firstName = "Emma";
            string lastName = "Lee";
            string address = "987 Willow Blvd";
            string city = "Grande Prairie";
            string postalCode = "T8V 4H5";
            string phoneNumber = "(780)555-3456";
            string emailAddress = "emma.lee@example.com";
            CanadianProvince province = CanadianProvince.NT;
            int participants = 4;
            AvaliableDays selectedDay = AvaliableDays.BothDays;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            // error stems from phone number parsing in format (xxx)xxx-xxxx
            Assert.AreEqual(expectedValue, actualValue);
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_nova_scotia()
        {
            // arrange
            string firstName = "Mason";
            string lastName = "Patel";
            string address = "213 Poplar Drive";
            string city = "Spruce Grove";
            string postalCode = "T7X 2H3";
            string phoneNumber = "780-555-6543";
            string emailAddress = "mason.patel@example.com";
            CanadianProvince province = CanadianProvince.NS;
            int participants = 2;
            AvaliableDays selectedDay = AvaliableDays.Day1;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            Assert.AreEqual(expectedValue, actualValue);
            // error stems from Provinces list not being alphabetized 
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_nunavut()
        {
            // arrange
            string firstName = "Ava";
            string lastName = "Robinson";
            string address = "456 Aspen Crescent";
            string city = "St. Albert";
            string postalCode = "T8N 1V2";
            string phoneNumber = "(780)555-2134";
            string emailAddress = "ava.robinson@example.com";
            CanadianProvince province = CanadianProvince.NU;
            int participants = 1;
            AvaliableDays selectedDay = AvaliableDays.BothDays;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            // error stems from phone number parsing in format (xxx)xxx-xxxx
            Assert.AreEqual(expectedValue, actualValue);
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_ontario()
        {
            // arrange
            string firstName = "Logan";
            string lastName = "Wong";
            string address = "789 Hemlock Street";
            string city = "Airdrie";
            string postalCode = "T4B 2A3";
            string phoneNumber = "403-555-7890";
            string emailAddress = "logan.wong@example.com";
            CanadianProvince province = CanadianProvince.ON;
            int participants = 3;
            AvaliableDays selectedDay = AvaliableDays.Day2;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            Assert.AreEqual(expectedValue, actualValue);
            // error stems from Provinces list not being alphabetized 
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_prince_edward_island()
        {
            // arrange
            string firstName = "Isabella";
            string lastName = "Clark";
            string address = "321 Redwood Way";
            string city = "Okotoks";
            string postalCode = "T1S 1A1";
            string phoneNumber = "(403)555-6789";
            string emailAddress = "isabella.clark@example.com";
            CanadianProvince province = CanadianProvince.PE;
            int participants = 2;
            AvaliableDays selectedDay = AvaliableDays.Day1;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            // error stems from phone number parsing in format (xxx)xxx-xxxx
            Assert.AreEqual(expectedValue, actualValue);
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_quebec()
        {
            // arrange
            string firstName = "Lucas";
            string lastName = "White";
            string address = "654 Dogwood Lane";
            string city = "Fort McMurray";
            string postalCode = "T9H 3E2";
            string phoneNumber = "780-555-1122";
            string emailAddress = "lucas.white@example.com";
            CanadianProvince province = CanadianProvince.QC;
            int participants = 1;
            AvaliableDays selectedDay = AvaliableDays.BothDays;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            Assert.AreEqual(expectedValue, actualValue);
            // error stems from Provinces list not being alphabetized 
            Assert.IsTrue(dropDownValid);
        }

        [TestMethod]
        public void input_province_saskatchewan()
        {
            // arrange
            string firstName = "Mia";
            string lastName = "Scott";
            string address = "987 Elm Court";
            string city = "Sherwood Park";
            string postalCode = "T8A 2G5";
            string phoneNumber = "(780)555-3344";
            string emailAddress = "mia.scott@example.com";
            CanadianProvince province = CanadianProvince.SK;
            int participants = 2;
            AvaliableDays selectedDay = AvaliableDays.Day2;

            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act

            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            bool dropDownValid = utils.validateDropDownValue(province.ToString(), driver.FindElement(By.Id("province")));

            utils.submit(driver);
            string actualValue = driver.Url;


            // assert

            // error stems from phone number parsing in format (xxx)xxx-xxxx
            Assert.AreEqual(expectedValue, actualValue);
            Assert.IsTrue(dropDownValid);
        }

        // Yukon does not exist in the form, and only 12 test cases are allowed
    }


    [TestClass]
    public sealed class participant_tests
    {
        private IWebDriver driver;
        private Utils utils;

        private string firstName;
        private string lastName;
        private string address;
        private string city;
        private string postalCode;
        private string phoneNumber;
        private string emailAddress;
        CanadianProvince province;
        AvaliableDays selectedDay;

        [TestInitialize]
        public void setUp()
        {
            ChromeOptions handlingSSL = new ChromeOptions();
            handlingSSL.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(handlingSSL);
            driver.Navigate().GoToUrl("https://localhost/register/index.html");

            utils = new Utils();

            firstName = "Liam";
            lastName = "Turner";
            address = "123 Maple Street";
            city = "Calgary";
            postalCode = "T2P 1A4";
            phoneNumber = "403-555-1234";
            emailAddress = "liam.turner@example.com";
            province = CanadianProvince.ON;
            selectedDay = AvaliableDays.Day1;
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestMethod]
        public void input_1_participant()
        {
            // arrange
            int participants = 1;
            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            utils.submit(driver);
            string actualValue = driver.Url;


            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void input_2_participant()
        {
            // arrange
            int participants = 2;
            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            utils.submit(driver);
            string actualValue = driver.Url;


            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void input_3_participant()
        {
            // arrange
            int participants = 3;
            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            utils.submit(driver);
            string actualValue = driver.Url;


            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void input_4_participant()
        {
            // arrange
            int participants = 4;
            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            utils.submit(driver);
            string actualValue = driver.Url;


            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void input_5_participant()
        {
            // arrange
            int participants = 5;
            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            utils.submit(driver);
            string actualValue = driver.Url;


            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void input_6_participant()
        {
            // arrange
            int participants = 6;
            string expectedValue = "https://localhost/register/viewRegistration.html";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, selectedDay, driver);
            utils.submit(driver);
            string actualValue = driver.Url;


            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }

    [TestClass]
    public sealed class registrants_tests
    {

        private IWebDriver driver;
        private Utils utils;

        private string firstName;
        private string lastName;
        private string address;
        private string city;
        private string postalCode;
        private string phoneNumber;
        private string emailAddress;
        CanadianProvince province;

        [TestInitialize]
        public void setUp()
        {
            ChromeOptions handlingSSL = new ChromeOptions();
            handlingSSL.AcceptInsecureCertificates = true;
            driver = new ChromeDriver(handlingSSL);
            driver.Navigate().GoToUrl("https://localhost/register/index.html");

            utils = new Utils();

            firstName = "Liam";
            lastName = "Turner";
            address = "123 Maple Street";
            city = "Calgary";
            postalCode = "T2P 1A4";
            phoneNumber = "403-555-1234";
            emailAddress = "liam.turner@example.com";
            province = CanadianProvince.ON;

        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }


        [TestMethod]
        public void input_day_1_participants_1()
        {
            // arrange
            int participants = 1;
            AvaliableDays day = AvaliableDays.Day1;
            string expectedResult = "$450.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_day_1_participants_3()
        {
            // arrange
            int participants = 3;
            AvaliableDays day = AvaliableDays.Day1;
            string expectedResult = "$1350.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_day_1_participants_5()
        {
            // arrange
            int participants = 5;
            AvaliableDays day = AvaliableDays.Day1;
            string expectedResult = "$2250.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_day_2_participants_1()
        {
            // arrange
            int participants = 1;
            AvaliableDays day = AvaliableDays.Day2;
            string expectedResult = "$350.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_day_2_participants_3()
        {
            // arrange
            int participants = 3;
            AvaliableDays day = AvaliableDays.Day2;
            string expectedResult = "$1050.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_day_2_participants_5()
        {
            // arrange
            int participants = 5;
            AvaliableDays day = AvaliableDays.Day2;
            string expectedResult = "$1750.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }


        //  day 3 is day 1 + day 2
        [TestMethod]
        public void input_day_3_participants_1()
        {
            // arrange
            int participants = 1;
            AvaliableDays day = AvaliableDays.BothDays;
            string expectedResult = "$750.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_day_3_participants_3()
        {
            // arrange
            int participants = 3;
            AvaliableDays day = AvaliableDays.BothDays;
            string expectedResult = "$2250.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_day_3_participants_5()
        {
            // arrange
            int participants = 5;
            AvaliableDays day = AvaliableDays.BothDays;
            string expectedResult = "$3750.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }


        [TestMethod]
        public void input_6_participants_day_1()
        {
            // arrange
            int participants = 6;
            AvaliableDays day = AvaliableDays.Day1;
            string expectedResult = "$2430.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_6_participants_day_2()
        {
            // arrange
            int participants = 6;
            AvaliableDays day = AvaliableDays.Day2;
            string expectedResult = "$189.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_6_participants_day_3()
        {
            // arrange
            int participants = 6;
            AvaliableDays day = AvaliableDays.BothDays;
            string expectedResult = "$4050.00";

            // act
            utils.enterData(firstName, lastName, address, city, postalCode, phoneNumber, emailAddress, province, participants, day, driver);
            utils.submit(driver);
            string? actualValue = utils.retrievePriceValue(driver.FindElement(By.Id("price")));


            // assert
            Assert.AreEqual(expectedResult, actualValue);
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
        public void input_select_the_event()
        {
            // arrange
            driver.Navigate().GoToUrl("https://localhost/register/viewRegistration.html");
            string expectedResult = "https://localhost/register/index.html";

            // act

            try
            {
                IWebElement theEvent = driver.FindElement(By.LinkText("THE Event!"));
                theEvent.Click();
            }
            catch (NoSuchElementException)
            {
                Assert.IsTrue(false);
            }

            string actualValue = driver.Url;

            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_select_home_page()
        {
            // arrange
            driver.Navigate().GoToUrl("https://localhost/register/viewRegistration.html");
            string expectedResult = "https://localhost/register/index.html";

            // act

            try
            {
                IWebElement theEvent = driver.FindElement(By.LinkText("Home"));
                theEvent.Click();
            }
            catch (NoSuchElementException)
            {
                Assert.IsTrue(false);
            }

            string actualValue = driver.Url;

            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

        [TestMethod]
        public void input_select_conestoga_college()
        {
            // arrange
            driver.Navigate().GoToUrl("https://localhost/register/viewRegistration.html");
            string expectedResult = "https://localhost/register/index.html";


            // act

            try
            {
                IWebElement theEvent = driver.FindElement(By.LinkText("Link to Conestoga College"));
                theEvent.Click();
            }
            catch (NoSuchElementException)
            {
                Assert.IsTrue(false);
            }
            string actualValue = driver.Url;

            // assert
            Assert.AreEqual(expectedResult, actualValue);
        }

    }
}
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.