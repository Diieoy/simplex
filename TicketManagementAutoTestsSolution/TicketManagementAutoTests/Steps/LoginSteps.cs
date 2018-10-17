using NUnit.Framework;
using TechTalk.SpecFlow;
using TicketManagementAutoTests.WebPages;

namespace TicketManagementAutoTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private static HomePage HomePage => PageFactory.Instance.Get<HomePage>();

        [Given(@"User is on localhost")]
        public void GivenUserIsOnLocalhost()
        {
            HomePage.Open();
        }

        [Given(@"User logged in as a user")]
        public void UserLoggedInAsAUser()
        {
            HomePage.Open();
            HomePage.LoggedInAsUser();
        }

        [When(@"(?:User )?[Cc]licks Login button")]
        public void WhenUserClicksLoginButton()
        {
            HomePage.LoginButton.Click();
        }

        [When(@"Enters ""(.*)"" to user input")]
        public void WhenEntersToUserNameInput(string inputString)
        {
            HomePage.UserNameInput.SendKeys(inputString);
        }

        [When(@"Enters ""(.*)"" to password input")]
        public void WhenEntersToPasswordField(string inputString)
        {
            HomePage.PasswordInput.SendKeys(inputString);
        }

        [When(@"Clicks Enter button")]
        public void WhenClicksEnterButton()
        {
            HomePage.EnterButton.Click();
        }

        [Then(@"Welcome line has message ""(.*)""")]
        public void ThenWelcomeLineHasUserName(string expectedMessage)
        {
            var message = HomePage.WelcomeLine.Text;
            Assert.AreEqual(message, expectedMessage);
            HomePage.LogoutButton.Click();
        }

        [Then(@"Login page has error ""(.*)""")]
        public void ThenErrorLineHasErrorMessage(string expectedErrorMesssage)
        {
            var message = HomePage.ErrorLine.Text;
            Assert.AreEqual(message, expectedErrorMesssage);
        }
    }
}
