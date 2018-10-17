using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TicketManagementAutoTests.WebPages
{
    public abstract class AbstractPage
    {
        protected IWebDriver Driver;

        protected AbstractPage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement FindByCss(string css)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css)));

            return Driver.FindElement(By.CssSelector(css));
        }
    }
}
