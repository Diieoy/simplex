using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TicketManagementAutoTests.WebPages
{
    public class EventManagerHomePage : HomePage
    {
        public IWebElement MenuDropDownButton => FindByCss("[name='menuDropDown']");
        public IWebElement EventMenuButton => FindByCss("[name='EventMenu']");
        public IWebElement CreateEventButton => FindByCss("[name='CreateEventButton']");
        public IWebElement EventNameInput => FindByCss("#Name");
        public IWebElement DescriptionInput => FindByCss("#Description");
        public IWebElement DateTimeStartInput => FindByCss("#DateTimeStart");
        public IWebElement DateTimeFinishInput => FindByCss("#DateTimeFinish");
        public IWebElement ImageUrlInput => FindByCss("#ImageUrl");
        public IWebElement CreateButton => FindByCss("#CreateButton");
        public IWebElement PriceInput => FindByCss("#EventAreaDTOs_0__Price");
        public IWebElement SaveButton => FindByCss("#SaveButton");
        public IWebElement EditForNewEvent => Driver.FindElement(By.XPath($".//tr[last()]//a[text()='Edit']"));
        public IWebElement DeleteForNewEvent => Driver.FindElement(By.XPath($".//tr[last()]//a[text()='Delete']"));        

        public EventManagerHomePage(IWebDriver driver) : base(driver) {}

        public void LoggedInAsManagerOpenHomePage()
        {
            Open();
            LoggedInAsManager();
        }

        public void SelectLayoutFromDropDownByName(string name)
        {
            SelectElement select = new SelectElement(FindByCss("#LayoutName"));
            select.SelectByText(name);
        }

        public void ClickOkOnConfirmForm()
        {
            Driver.SwitchTo().Alert().Accept();
        }

        public bool CheckEvent(string name)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            var xPath = ".//td[normalize-space()='" + name + "']";
            
            try
            {                
                Driver.FindElement(By.XPath(xPath)).Text.Equals(name);
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;            
        }
    }
}
