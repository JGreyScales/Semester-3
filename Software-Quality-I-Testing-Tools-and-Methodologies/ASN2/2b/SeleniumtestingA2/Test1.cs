using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;


namespace SeleniumtestingA2
{
    [TestClass]
    public sealed class menu_tests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CA1859 // Use concrete types when possible for improved performance
        IWebDriver driver;
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        [TestInitialize]
        public void setUp(){
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TearDown(){
            driver.Quit();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }


    [TestClass]
    public sealed class participant_tests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CA1859 // Use concrete types when possible for improved performance
        IWebDriver driver;
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void setUp(){
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TearDown(){
            driver.Quit();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    [TestClass]
    public sealed class registrants_tests
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CA1859 // Use concrete types when possible for improved performance
        IWebDriver driver;
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void setUp(){
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TearDown(){
            driver.Quit();
        }


        [TestMethod]
        public void TestMethod1()
        {
        }
    }

}
