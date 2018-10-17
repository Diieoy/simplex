using NUnit.Framework;
using System.Data.SqlClient;
using TechTalk.SpecFlow;
using TicketManagementAutoTests.WebPages;

namespace TicketManagementAutoTests.Steps
{
    [Binding]
    public class BuyATicketSteps
    {
        private static HomePage HomePage => PageFactory.Instance.Get<HomePage>();

        [Given(@"User logged in as a manager")]
        public void GivenUserLoggedInAsAManager()
        {
            HomePage.Open();
            HomePage.LoggedInAsManager();            
        }

        [When(@"User chose and clicks For Autotest event")]
        public void WhenManagerChoseAndClicksForAutotestEvent()
        {
            HomePage.ForAutotestEvent.Click();
        }

        [When(@"User chose and clicks Add to cart on first seat")]
        public void WhenManagerChoseAndClicksAddToCartOnFirstSeat()
        {
            HomePage.FirstSeatOfForAutotestEvent.Click();
        }

        [When(@"User clicks Buy button")]
        public void WhenManagerClicksBuyButton()
        {
            HomePage.BuyButton.Click();
        }

        [Then(@"User see ""(.*)"" message in SuccessMessage line")]
        public void ThenManagerSeeMessageInSuccessMessageLine(string expectedString)
        {
            var msg = HomePage.SuccessMessageLine.Text;
            Assert.AreEqual(msg, expectedString);
        }

        [Then(@"User see his ticket in his cart")]
        public void asdasasd()
        {
            HomePage.PurchaseHistoryButton.Click();
            Assert.AreEqual(HomePage.IsATicketInCart(), true);
        }

        [AfterScenario("DeleteEventDeleteOrderReturnMoneyAfter")]
        public void DeleteEventAfter()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MyModelContext"].ToString();
            SqlConnection con = new SqlConnection(connStr);
            con.Open();

            SqlCommand cmd = new SqlCommand("Select [Id] from [Event] where Name='For Autotest'", con);
            var reader = cmd.ExecuteReader();
            int eventId = 0;
            while (reader.Read())
            {
                eventId = reader.GetInt32(0);
            }
            reader.Close();

            cmd = new SqlCommand("select MIN(EventSeat.Id), MIN(Price) from [Event] inner join EventArea on [Event].Id=EventArea.EventId inner join EventSeat on EventArea.Id=EventSeat.EventAreaId where [Event].Name='For Autotest'", con);
            reader = cmd.ExecuteReader();
            int eventSeatId = 0;
            decimal price = 0;
            while (reader.Read())
            {
                eventSeatId = reader.GetInt32(0);
                price = reader.GetDecimal(1);
            }
            reader.Close();

            cmd = new SqlCommand("select UserId from [Order] where SeatId=" + eventSeatId, con);
            reader = cmd.ExecuteReader();
            string userId = "";
            while (reader.Read())
            {
                userId = reader.GetString(0);
            }
            reader.Close();

            cmd = new SqlCommand("delete from [Order] where SeatId=" + eventSeatId, con);
            cmd.ExecuteNonQuery();           

            cmd = new SqlCommand("update [User] set Account+=" + price + " where Id='" + userId + "'", con);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("DeleteEvent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", eventId));
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
