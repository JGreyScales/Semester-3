using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
class Program
{
    static void Main()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        using var driver = new ChromeDriver(options);
        // Demo A: example.com
        driver.Navigate().GoToUrl("https://example.com");
        string title = driver.Title;
        string h1 = driver.FindElement(By.CssSelector("h1")).Text;
        Console.WriteLine("[Example.com]");
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"H1: {h1}");
        // Demo B: conestogac.on.ca
        driver.Navigate().GoToUrl("https://www.conestogac.on.ca");
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        IWebElement heading = wait.Until(drv =>
        {
            try
            {
                var el = drv.FindElement(By.CssSelector("h1"));
                return !string.IsNullOrWhiteSpace(el.Text) ? el : null;
            }
            catch { return null; }
        });
        Console.WriteLine("[Conestoga]");
        Console.WriteLine($"Title: {driver.Title}");
        // outputs discovered heading value
        Console.WriteLine($"Top-level heading discovered: {heading.Text}");
    }
}