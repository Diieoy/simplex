using NUnit.Framework;
using TechTalk.SpecFlow;
using TicketManagementAutoTests.WebPages;

namespace TicketManagementAutoTests.Steps
{
    [Binding]
    public class EventCRUDSteps
    {
        private static EventManagerHomePage EventManagerHomePage => PageFactory.Instance.Get<EventManagerHomePage>();               

        [Given(@"User is logged in to the system as manager")]
        public void GivenManagerIsOnLocalhost()
        {
            EventManagerHomePage.LoggedInAsManagerOpenHomePage();
        }

        [Given(@"Click Edit button on new event")]
        public void GivenClickEditButtonOnNewEvent()
        {
            EventManagerHomePage.EditForNewEvent.Click();
        }

        [Given(@"Click Delete button on new event")]
        public void GivenClickDeleteButtonOnNewEvent()
        {
            EventManagerHomePage.DeleteForNewEvent.Click();
        }


        [When(@"Click MenuDropDown button")]
        public void WhenIClicksMenuDropDownButton()
        {
            EventManagerHomePage.MenuDropDownButton.Click();
        }

        [When(@"Click EventMenu button")]
        public void WhenIClickEventMenuButton()
        {
            EventManagerHomePage.EventMenuButton.Click();
        }

        [When(@"Click CreateEvent button")]
        public void WhenIClickCreateEventButton()
        {
            EventManagerHomePage.CreateEventButton.Click();
        }

        [When(@"Enter ""(.*)"" to Name input")]
        public void WhenEnterToNameInput(string inputString)
        {
            EventManagerHomePage.EventNameInput.Clear();
            EventManagerHomePage.EventNameInput.SendKeys(inputString);
        }

        [When(@"Enter ""(.*)"" to Description input")]
        public void WhenEnterToDescriptionInput(string inputString)
        {
            EventManagerHomePage.DescriptionInput.Clear();
            EventManagerHomePage.DescriptionInput.SendKeys(inputString);
        }

        [When(@"Enter ""(.*)"" to DateTimeStart input")]
        public void WhenEnterToDateTimeStartInput(string inputString)
        {
            EventManagerHomePage.DateTimeStartInput.Clear();
            EventManagerHomePage.DateTimeStartInput.SendKeys(inputString);
        }

        [When(@"Enter ""(.*)"" to DateTimeFinish input")]
        public void WhenEnterToDateTimeFinishInput(string inputString)
        {
            EventManagerHomePage.DateTimeFinishInput.Clear();
            EventManagerHomePage.DateTimeFinishInput.SendKeys(inputString);
        }

        [When(@"Enter ""(.*)"" to ImageUrl input")]
        public void WhenEnterImageUrlInput(string inputString)
        {
            EventManagerHomePage.ImageUrlInput.Clear();
            EventManagerHomePage.ImageUrlInput.SendKeys(inputString);           
        }

        [When(@"Choose ""(.*)"" in LayoutName dropdown menu")]
        public void WhenChooseLayout2InLayoutNameDropDown(string inputString)
        {
            EventManagerHomePage.SelectLayoutFromDropDownByName(inputString);           
        }

        [When(@"Click Create button")]
        public void WhenIClickCreateButton()
        {
            EventManagerHomePage.CreateButton.Click();            
        }

        [When(@"Enter ""(.*)"" to Price input")]
        public void WhenEnterPrice(string inputString)
        {
            EventManagerHomePage.PriceInput.Clear();
            EventManagerHomePage.PriceInput.SendKeys(inputString);           
        }

        [When(@"Click Save button")]
        public void WhenClickSaveButton()
        {
            EventManagerHomePage.SaveButton.Click();
        }        

        [When(@"Click OK on confirm form")]
        public void WhenClickOKOnConfirmForm()
        {
            EventManagerHomePage.ClickOkOnConfirmForm();
        }    

        [Then(@"Event with name ""(.*)"" appeared in system")]
        public void ThenEventAppearedInSystem(string expectedString)
        {
            Assert.AreEqual(EventManagerHomePage.CheckEvent(expectedString), true);
        }

        [Then(@"Event with name ""(.*)"" is not present in system more")]
        public void ThenEventIsNotPresent(string eventName)
        {            
            Assert.AreEqual(EventManagerHomePage.CheckEvent(eventName), false);
        }


        [BeforeScenario("LoggedInAsManagerAndCreateEventForAutotestAndLoggedOutBefore")]
        public void LoginAsAManagerAndCreateEventForAutotestsBefore()
        {
            EventManagerHomePage.LoggedInAsManagerOpenHomePage();
            EventManagerHomePage.MenuDropDownButton.Click();
            EventManagerHomePage.EventMenuButton.Click();
            EventManagerHomePage.CreateEventButton.Click();

            EventManagerHomePage.EventNameInput.Clear();
            EventManagerHomePage.EventNameInput.SendKeys("For Autotest");

            EventManagerHomePage.DescriptionInput.Clear();
            EventManagerHomePage.DescriptionInput.SendKeys("test");

            EventManagerHomePage.DateTimeStartInput.Clear();
            EventManagerHomePage.DateTimeStartInput.SendKeys("11/7/2022 3:00:00 PM");

            EventManagerHomePage.DateTimeFinishInput.Clear();
            EventManagerHomePage.DateTimeFinishInput.SendKeys("11/7/2022 6:00:00 PM");

            EventManagerHomePage.ImageUrlInput.Clear();
            EventManagerHomePage.ImageUrlInput.SendKeys("https://wallpaperbrowse.com/media/images/soap-bubble-1958650_960_720.jpg");

            EventManagerHomePage.SelectLayoutFromDropDownByName("Layout 2");
            EventManagerHomePage.CreateButton.Click();

            EventManagerHomePage.PriceInput.Clear();
            EventManagerHomePage.PriceInput.SendKeys("10");
            EventManagerHomePage.SaveButton.Click();
            EventManagerHomePage.LogoutButton.Click();
        }

        [BeforeScenario("LoggedInAsManagerAndCreateNewEventBefore")]
        public void LoginAsAManagerAndCreateNewEventBefore()
        {
            EventManagerHomePage.LoggedInAsManagerOpenHomePage();
            EventManagerHomePage.MenuDropDownButton.Click();
            EventManagerHomePage.EventMenuButton.Click();
            EventManagerHomePage.CreateEventButton.Click();

            EventManagerHomePage.EventNameInput.Clear();
            EventManagerHomePage.EventNameInput.SendKeys("test");

            EventManagerHomePage.DescriptionInput.Clear();
            EventManagerHomePage.DescriptionInput.SendKeys("test");

            EventManagerHomePage.DateTimeStartInput.Clear();
            EventManagerHomePage.DateTimeStartInput.SendKeys("11/07/2022 15:00");

            EventManagerHomePage.DateTimeFinishInput.Clear();
            EventManagerHomePage.DateTimeFinishInput.SendKeys("11/07/2022 18:00");

            EventManagerHomePage.ImageUrlInput.Clear();
            EventManagerHomePage.ImageUrlInput.SendKeys("https://wallpaperbrowse.com/media/images/soap-bubble-1958650_960_720.jpg");

            EventManagerHomePage.SelectLayoutFromDropDownByName("Layout 2");
            EventManagerHomePage.CreateButton.Click();

            EventManagerHomePage.PriceInput.Clear();
            EventManagerHomePage.PriceInput.SendKeys("7");
            EventManagerHomePage.SaveButton.Click();
        }

        [AfterScenario("DeleteEventAndLogoutAfter")]
        public void DeleteEventAndLogoutAfter()
        {
            EventManagerHomePage.DeleteForNewEvent.Click();
            EventManagerHomePage.ClickOkOnConfirmForm();
            EventManagerHomePage.LogoutButton.Click();
        }

        [AfterScenario("LogoutAfter")]
        public void LogoutAfter()
        {
            EventManagerHomePage.LogoutButton.Click();
        }
    }
}
