using BLLStandard.DTO;
using Hangfire;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.WcfEventServiceReference;
using WebUI.WcfLayoutServiceReference;
using WebUI.WcfPurchaseServiceReference;
using WebUI.WcfVenueServiceReference;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private UserManager<UserDTO, string> userManager;

        public CartController(UserManager<UserDTO, string> userManager)
        {
            this.userManager = userManager;
        }

        private PurchaseServiceClient GetPurchaseServiceClient(string name, string password)
        {
            var client = new PurchaseServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        private EventServiceClient GetEventServiceClient(string name, string password)
        {
            var client = new EventServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        private VenueServiceClient GetVenueServiceClient(string name, string password)
        {
            var client = new VenueServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        private LayoutServiceClient GetLayoutServiceClient(string name, string password)
        {
            var client = new LayoutServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        public ActionResult Index(string message)
        {
            if (message != null)
            {
                ViewBag.Message = message;
                return View();
            }

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;

            var purchaseClient = GetPurchaseServiceClient(User.Identity.Name, token);

            var userId = userManager.FindByName(User.Identity.Name).Id;
            var list = purchaseClient.GetAllPurchasesByUserId(userId);
            if (list.Count() != 0)
            {
                List<CartViewModel> cart = new List<CartViewModel>();
                foreach (var item in list)
                {
                    cart.Add(GetCartItemViewModel(item.EventSeatId));
                }

                Session["userAccount"] = userManager.FindById(userId).Account;
                CountTotalValue();

                return View("Cart", cart);
            }

            purchaseClient.Close();

            return View();
        }

        public ActionResult AddItemError(string err)
        {
            ViewBag.Error = err;

            return View();
        }

        public ActionResult AddItem(int eventSeatId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;

            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            if (eventClient.GetEventSeatDTOById(eventSeatId).State != 0)
            {
                ViewBag.Error = Resources.CartControllerTexts.SorryThisTicketAlreadyBought;

                return View("AddItemError");
            }

            var purchaseClient = GetPurchaseServiceClient(User.Identity.Name, token);

            var userId = userManager.FindByName(User.Identity.Name).Id;
            Session["userAccount"] = userManager.FindByName(User.Identity.Name).Account;
            purchaseClient.AddPurchase(new WcfPurchaseServiceReference.PurchaseDTO
            {
                UserId = userId,
                EventSeatId = eventSeatId
            });

            var allPurchases = purchaseClient.GetAllPurchasesByUserId(userId);

            List<CartViewModel> list = new List<CartViewModel>();
            foreach (var item in allPurchases)
            {
                list.Add(GetCartItemViewModel(item.EventSeatId));
            }

            BookASeat(eventSeatId);
            CountTotalValue();

            purchaseClient.Close();
            eventClient.Close();

            var time = Convert.ToDouble(ConfigurationManager.AppSettings["TimeForPurchasingInSeconds"]);
            BackgroundJob.Schedule(() => СlearCartAfterTime(eventSeatId, User.Identity.Name, token), TimeSpan.FromSeconds(time));

            return View("Cart", list);
        }

        public void СlearCartAfterTime(int eventSeatId, string userName, string token)
        {
            if(userName != null)
            {
                var purchaseClient = GetPurchaseServiceClient(userName, token);

                var purchaseDTO = purchaseClient.GetPurchaseByEventSeatId(eventSeatId);

                if (purchaseDTO != null)
                {
                    var userId = purchaseDTO.UserId;

                    var allPurchases = purchaseClient.GetAllPurchasesByUserId(userId);

                    foreach (var item in allPurchases)
                    {
                        CancelReservation(item.EventSeatId, userName, token);
                        purchaseClient.DeletePurchaseByEventSeatId(item.EventSeatId);
                    }                   
                }

                purchaseClient.Close();
            }
        }

        public ActionResult DeleteItem(int eventSeatId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;

            var purchaseClient = GetPurchaseServiceClient(User.Identity.Name, token);

            var userId = userManager.FindByName(User.Identity.Name).Id;

            var allPurchasesBeforeDel = purchaseClient.GetAllPurchasesByUserId(userId);
            if (allPurchasesBeforeDel.Count() == 0)
            {
                string msg = Resources.CartControllerTexts.YouWereAwayTooLong;
                return RedirectToAction("Index", new { message = msg });
            }

            purchaseClient.DeletePurchaseByEventSeatId(eventSeatId);

            var allPurchasesAfterDel = purchaseClient.GetAllPurchasesByUserId(userId);
            List<CartViewModel> listAfterDel = new List<CartViewModel>();

            foreach (var item in allPurchasesAfterDel)
            {
                listAfterDel.Add(GetCartItemViewModel(item.EventSeatId));
            }

            CancelReservation(eventSeatId, User.Identity.Name, token);
            CountTotalValue();

            purchaseClient.Close();

            return View("Cart", listAfterDel);
        }

        public void CountTotalValue()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;

            var purchaseClient = GetPurchaseServiceClient(User.Identity.Name, token);

            var userId = userManager.FindByName(User.Identity.Name).Id;
            var allPurchases = purchaseClient.GetAllPurchasesByUserId(userId);
            List<CartViewModel> list = new List<CartViewModel>();
            foreach (var item in allPurchases)
            {
                list.Add(GetCartItemViewModel(item.EventSeatId));
            }

            Session["totalPrice"] = list.Sum(c => c.EventAreaPrice);
            purchaseClient.Close();
        }

        public ActionResult Buy()
        {
            var dateTimeOfPurchase = DateTime.Now;

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;

            var purchaseClient = GetPurchaseServiceClient(User.Identity.Name, token);


            var userId = userManager.FindByName(User.Identity.Name).Id;
            var allPurchases = purchaseClient.GetAllPurchasesByUserId(userId);
            List<CartViewModel> purchasesModel = new List<CartViewModel>();

            if (allPurchases.Count() == 0)
            {
                string msg = Resources.CartControllerTexts.YouWereAwayTooLong;
                return RedirectToAction("Index", new { message = msg });
            }

            foreach (var item in allPurchases)
            {
                purchasesModel.Add(GetCartItemViewModel(item.EventSeatId));
            }

            if (purchasesModel.First().UserAccount < Convert.ToInt32(Session["totalPrice"]))
            {
                foreach (var item in purchasesModel)
                {
                    CancelReservation(item.EventSeatId, User.Identity.Name, token);
                    purchaseClient.DeletePurchaseByEventSeatId(item.EventSeatId);
                }

                Session["accountError"] = Resources.CartControllerTexts.NotEnoughMoney;
                purchasesModel = null;

                return RedirectToAction("Index");
            }

            foreach (var item in purchasesModel)
            {
                purchaseClient.AddOrder(new WcfPurchaseServiceReference.OrderDTO
                {
                    UserId = item.UserId,
                    SeatId = item.EventSeatId,
                    DateTimeOrder = dateTimeOfPurchase
                });
            }

            var u = userManager.FindByName(User.Identity.Name);
            u.Account -= Convert.ToInt32(Session["totalPrice"]);

            SendAnEmailAboutPurchase(u.UserName, u.Email, Resources.CartControllerTexts.YouBoughtTickets, purchasesModel, dateTimeOfPurchase);

            userManager.Update(u);

            foreach (var item in purchasesModel)
            {
                purchaseClient.DeletePurchaseByEventSeatId(item.EventSeatId);
            }

            purchasesModel = null;
            ViewBag.SuccessMessage = Resources.CartControllerTexts.YouBoughtTicketsAndSendEmail;
            purchaseClient.Close();

            return View("Cart");
        }

        public ActionResult ShowPurchaseHistory()
        {
            return View(GetPurchaseHistoryViewModelList(User.Identity.Name));
        }

        private void SendAnEmailAboutPurchase(string userName, string emailAdress, string subject, List<CartViewModel> purchases, DateTime dateTimeOfPurchase)
        {
            var eventName = purchases.First().EventName;
            var dateTimeStart = purchases.First().DateTimeStart;
            var dateTimeFinish = purchases.First().DateTimeFinish;
            var layoutName = purchases.First().LayoutName;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(Resources.CartControllerTexts.Hello + ", " + userName + "!");
            stringBuilder.AppendLine(Resources.CartControllerTexts.DateTimeOrder + ": " + dateTimeOfPurchase);

            foreach (var item in purchases)
            {
                stringBuilder.AppendLine("*****************************************************************************************");
                stringBuilder.AppendLine(Resources.CartControllerTexts.Event + ": " + item.EventName);
                stringBuilder.AppendLine("  " + Resources.CartControllerTexts.Hall + ": " + item.EventAreaDescription);
                stringBuilder.AppendLine("  " + Resources.CartControllerTexts.EventSeatRow + " - " + item.EventSeatRow);
                stringBuilder.AppendLine("  " + Resources.CartControllerTexts.EventSeatNumber + " - " + item.EventSeatNumber);
                stringBuilder.AppendLine(Resources.CartControllerTexts.VenueAddress + ": " + item.VenueAddress + "'" + item.VenueName + "'" + ", " + Resources.CartControllerTexts.Phone + ": " + item.VenuePhone);
                stringBuilder.AppendLine(Resources.CartControllerTexts.Date + ": " + item.DateTimeStart.ToLongDateString() + ", " + Resources.CartControllerTexts.DateTimeStart + ": " + item.DateTimeStart.ToShortTimeString() + ", " + Resources.CartControllerTexts.DateTimeFinish + ": " + item.DateTimeFinish.ToShortTimeString());
                stringBuilder.AppendLine("*****************************************************************************************");
            }
            stringBuilder.AppendLine(Resources.CartControllerTexts.ThankYouForPurchases + "!");

            MailAddress fromMailAddress = new MailAddress(ConfigurationManager.AppSettings["MailAddress"], "Ticket Manager");
            MailAddress toMailAddress = new MailAddress(emailAdress, userName);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress))
            using (SmtpClient smptClient = new SmtpClient())
            {
                mailMessage.Subject = subject;
                mailMessage.Body = stringBuilder.ToString();
                mailMessage.BodyEncoding = Encoding.Default;

                smptClient.Send(mailMessage);               
            }

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress))
            using (SmtpClient smptClient = new SmtpClient())
            {
                mailMessage.Subject = subject;
                mailMessage.Body = stringBuilder.ToString();

                smptClient.Host = ConfigurationManager.AppSettings["SmtpHost"];
                smptClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                smptClient.EnableSsl = true;
                smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smptClient.UseDefaultCredentials = false;
                smptClient.Credentials = new NetworkCredential(fromMailAddress.Address, ConfigurationManager.AppSettings["MailPassword"]);

                smptClient.Send(mailMessage);
            }
        }

        private void BookASeat(int eventSeatId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            var eSeat = eventClient.GetEventSeatDTOById(eventSeatId);
            eSeat.State = 1;

            eventClient.UpdateEventSeat(eSeat);
            eventClient.Close();
        }

        private void CancelReservation(int eventSeatId, string userName, string token)
        {
            var eventClient = GetEventServiceClient(userName, token);

            var eSeat = eventClient.GetEventSeatDTOById(eventSeatId);
            eSeat.State = 0;

            eventClient.UpdateEventSeat(eSeat);
            eventClient.Close();
        }

        private CartViewModel GetCartItemViewModel(int eventSeatId)
        {
            var u = User.Identity.Name;

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;

            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            var layoutClient = GetLayoutServiceClient(User.Identity.Name, token);

            var venueClient = GetVenueServiceClient(User.Identity.Name, token);

            var eSeat = eventClient.GetEventSeatDTOById(eventSeatId);
            var eArea = eventClient.GetEventAreaDTOById(eSeat.EventAreaId);
            var e = eventClient.GetById(eArea.EventId);
            var layout = layoutClient.GetById(e.LayoutId);
            var venue = venueClient.GetById(layout.VenueId);

            eventClient.Close();
            layoutClient.Close();
            venueClient.Close();

            return new CartViewModel
            {
                VenueName = venue.Name,
                VenueAddress = venue.Address,
                VenuePhone = venue.Phone,

                LayoutName = layout.Name,

                EventName = e.Name,
                DateTimeStart = e.DateTimeStart,
                DateTimeFinish = e.DateTimeFinish,

                EventAreaDescription = eArea.Description,
                EventAreaPrice = eArea.Price,
                EventSeatId = eSeat.Id,
                EventSeatRow = eSeat.Row,
                EventSeatNumber = eSeat.Number,
                EventSeatState = eSeat.State,

                UserId = userManager.FindByName(User.Identity.Name).Id,
                UserAccount = userManager.FindByName(User.Identity.Name).Account
            };
        }

        private List<PurchaseHistoryViewModel> GetPurchaseHistoryViewModelList(string userName)
        {
            var userId = userManager.FindByName(User.Identity.Name).Id;

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var purchaseClient = GetPurchaseServiceClient(User.Identity.Name, token);

            var ordersDTO = purchaseClient.GetAllOrdersByUserId(userId);

            List<CartViewModel> cartList = new List<CartViewModel>();
            foreach (var item in ordersDTO)
            {
                cartList.Add(GetCartItemViewModel(item.SeatId));
            }

            List<PurchaseHistoryViewModel> purchaseHistoryList = new List<PurchaseHistoryViewModel>();
            foreach (var item in cartList)
            {
                purchaseHistoryList.Add(new PurchaseHistoryViewModel
                {
                    VenueName = item.VenueName,
                    VenueAddress = item.VenueAddress,

                    LayoutName = item.LayoutName,

                    EventName = item.EventName,
                    DateTimeStart = item.DateTimeStart,
                    DateTimeFinish = item.DateTimeFinish,
                    EventAreaDescription = item.EventAreaDescription,
                    EventAreaPrice = item.EventAreaPrice,
                    EventSeatRow = item.EventSeatRow,
                    EventSeatNumber = item.EventSeatNumber,

                    DateTimeOrder = ordersDTO.ToList().Find(x => x.SeatId == item.EventSeatId).DateTimeOrder
                });
            }

            purchaseClient.Close();

            return purchaseHistoryList;
        }
    }
}