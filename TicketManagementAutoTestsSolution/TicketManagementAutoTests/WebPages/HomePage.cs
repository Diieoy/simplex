using System;
using OpenQA.Selenium;

namespace TicketManagementAutoTests.WebPages
{
    public class HomePage : AbstractPage
    {
        public HomePage(IWebDriver driver) : base(driver){}

        public IWebElement LoginButton => FindByCss("#login");
        public IWebElement UserNameInput => FindByCss("#UserName");
        public IWebElement PasswordInput => FindByCss("#Password");
        public IWebElement EnterButton => FindByCss("[name='EnterInput']");
        public IWebElement WelcomeLine => FindByCss("[name='WelcomeLine']");
        public IWebElement ErrorLine => FindByCss("[name='ErrLine']");
        public IWebElement LogoutButton => FindByCss("[name='LogoutButton']");
        public IWebElement ForAutotestEvent => Driver.FindElement(By.XPath(".//a[text()='For Autotest']"));
        public IWebElement FirstSeatOfForAutotestEvent => Driver.FindElement(By.XPath(".//div[contains(@class, 'details_event')][1]/p/a"));
        public IWebElement BuyButton => Driver.FindElement(By.XPath(".//a[text()='Buy']"));
        public IWebElement SuccessMessageLine => FindByCss("#SuccessMessage");
        public IWebElement PurchaseHistoryButton => FindByCss("#PurchaseHistory");

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:61294";
        }

        public void LoggedInAsManager()
        {
            LoginButton.Click();
            UserNameInput.SendKeys("manager");
            PasswordInput.SendKeys("manager");
            EnterButton.Click();
        }

        public void LoggedInAsUser()
        {
            LoginButton.Click();
            UserNameInput.SendKeys("user");
            PasswordInput.SendKeys("user");
            EnterButton.Click();
        }

        public bool IsATicketInCart()
        {
            try
            {
                Driver.FindElement(By.XPath(".//tr[td[text()='For Autotest'] and td[text()='11/7/2022 3:00:00 PM']]"));
            } catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }
    }
}
