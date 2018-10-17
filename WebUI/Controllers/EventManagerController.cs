using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.WcfAreaServiceReference;
using WebUI.WcfEventServiceReference;
using WebUI.WcfLayoutServiceReference;

namespace WebUI.Controllers
{
    public class EventManagerController : Controller
    {
        private EventServiceClient GetEventServiceClient(string name, string password)
        {
            var client = new EventServiceClient();
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

        private AreaServiceClient GetAreaServiceClient(string name, string password)
        {
            var client = new AreaServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        public ActionResult Index(string message)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            ViewBag.Message = message;
            var res = eventClient.GetAll();

            eventClient.Close();

            return View(res);
        }

        public ActionResult CreateEvent(string errorMessage)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var layoutClient = GetLayoutServiceClient(User.Identity.Name, token);

            ViewBag.Error = errorMessage;

            List<string> list = new List<string>();
            foreach (var item in layoutClient.GetAll())
            {
                list.Add(item.Name);
            }

            layoutClient.Close();

            return View(new ForEventManagerViewModel { LayoutsNames = list });
        }

        [HttpPost]
        public ActionResult CreateEvent(ForEventManagerViewModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var layoutClient = GetLayoutServiceClient(User.Identity.Name, token);

            List<string> list = new List<string>();
            foreach (var item in layoutClient.GetAll())
            {
                list.Add(item.Name);
            }

            if (!ModelState.IsValid)
            {
                return View(new ForEventManagerViewModel { LayoutsNames = list });
            }

            layoutClient.Close();

            return RedirectToAction("SetPricesOnAreas", model);
        }

        public ActionResult SetPricesOnAreas(ForEventManagerViewModel model)
        {
            if (model.EventAreaDTOs != null && model.EventAreaDTOs.FirstOrDefault(x => x.Price == 0) != null)
            {
                ViewBag.Error = Resources.EventManagerControllerTexts.AllPricesMustBeSet;
                return View(model);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            var layoutClient = GetLayoutServiceClient(User.Identity.Name, token);

            if (model.EventAreaDTOs != null && model.EventAreaDTOs.FirstOrDefault(x => x.Price == 0) == null)
            {
                try
                {
                    eventClient.Create(new EventDTO
                    {
                        Name = model.Name,
                        Description = model.Description,
                        DateTimeStart = model.DateTimeStart,
                        DateTimeFinish = model.DateTimeFinish,
                        ImageUrl = model.ImageUrl,
                        LayoutId = layoutClient.GetByName(model.LayoutName).Id
                    });
                }
                catch (FaultException<WcfEventServiceReference.InvalidEventException>)
                {
                    var err = Resources.EventManagerControllerTexts.EventCreatingError;
                    return RedirectToAction("CreateEvent", new { errorMessage = err });
                }

                var createdEventId = eventClient.GetAll().Last().Id;
                var createdEventAreaDTOs = eventClient.GetAllEventAreasDTOByEventId(createdEventId);

                for (int i = 0; i < createdEventAreaDTOs.Count(); i++)
                {
                    createdEventAreaDTOs.ElementAt(i).Price = model.EventAreaDTOs.ElementAt(i).Price;                    
                    eventClient.UpdateEventAreas(createdEventAreaDTOs.ElementAt(i));
                }

                string message = Resources.EventManagerControllerTexts.NewEventCreated;
                eventClient.Close();
                layoutClient.Close();

                return RedirectToAction("Index", new { message });
            }


            var areaClient = GetAreaServiceClient(User.Identity.Name, token);

            var layoutId = layoutClient.GetByName(model.LayoutName).Id;
            var areas = areaClient.GetAll().Where(x => x.LayoutId == layoutId);

            var eventAreaDTOs = new List<EventAreaDTO>();
            foreach (var item in areas)
            {
                eventAreaDTOs.Add(new EventAreaDTO
                {
                    LayoutId = item.LayoutId,
                    Description = item.Description,
                    CoordX = item.CoordX,
                    CoordY = item.CoordY
                });
            }
           
            model.EventAreaDTOs = eventAreaDTOs;

            eventClient.Close();
            layoutClient.Close();
            areaClient.Close();

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            string message = "";

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            try
            {
                eventClient.Delete(id);
            }
            catch (FaultException<WcfEventServiceReference.CanNotDeleteEventException>)
            {
                message = Resources.EventManagerControllerTexts.CanNotDeleteEventWithBoughtTickets;
                return RedirectToAction("Index", new { message });
            }

            message = Resources.EventManagerControllerTexts.EventDeleted;
            eventClient.Close();

            return RedirectToAction("Index", new { message });
        }

        public ActionResult CreateEventSeat(int id)
        {
            return View(new EventSeatDTO { EventAreaId = id });
        }

        [HttpPost]
        public ActionResult CreateEventSeat(EventSeatDTO eventSeatDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            try
            {
                eventClient.CreateEventSeats(new EventSeatDTO
                {
                    EventAreaId = eventSeatDTO.EventAreaId,
                    Row = eventSeatDTO.Row,
                    Number = eventSeatDTO.Number
                });
            }
            catch (FaultException<WcfEventServiceReference.CanNotCreateEventSeatException> e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View(eventSeatDTO);
            }

            int eventAreaId = eventSeatDTO.EventAreaId;

            eventClient.Close();

            return RedirectToAction("DetailsEventArea", new { id = eventAreaId });
        }

        public ActionResult EditEventArea(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            var result = eventClient.GetEventAreaDTOById(id);

            eventClient.Close();

            return View(result);
        }

        [HttpPost]
        public ActionResult EditEventArea(EventAreaDTO eventAreaDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(eventAreaDTO);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            eventClient.UpdateEventAreas(eventAreaDTO);

            var result = eventAreaDTO.EventId;

            eventClient.Close();

            return RedirectToAction("DetailsEvent", new { id = result });
        }

        public ActionResult DetailsEvent(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var evemtClient = GetEventServiceClient(User.Identity.Name, token);

            var result = evemtClient.GetAllEventAreasDTOByEventId(id);

            evemtClient.Close();

            return View(result);
        }

        public ActionResult DetailsEventArea(int id, string errorMessage)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            ViewBag.ErrorMessage = errorMessage;

            var result = eventClient.GetAllEventSeatsDTOByEventAreaId(id);

            eventClient.Close();

            return View(result);
        }

        public ActionResult DeleteEventSeat(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            var es = eventClient.GetEventSeatDTOById(id);
            string message = "";

            try
            {
                eventClient.DeleteEventSeat(id);
            }
            catch (FaultException<CanNotDeleteEventSeatException>)
            {
                message = Resources.EventManagerControllerTexts.CanNotDeleteSeatAlreadyBought;
            }

            eventClient.Close();

            return RedirectToAction("DetailsEventArea", new { id = es.EventAreaId, errorMessage = message });
        }

        public ActionResult EditEvent(int id)
        {
            List<string> list = new List<string>();

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var layoutClient = GetLayoutServiceClient(User.Identity.Name, token);

            var eventClient = GetEventServiceClient(User.Identity.Name, token);

            foreach (var item in layoutClient.GetAll())
            {
                list.Add(item.Name);
            }

            var e = eventClient.GetById(id);


            layoutClient.Close();
            eventClient.Close();

            return View(new EditEventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                DateTimeStart = e.DateTimeStart,
                DateTimeFinish = e.DateTimeFinish,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                LayoutsNames = list
            });
        }

        [HttpPost]
        public ActionResult EditEvent(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var token = identity.FindFirst(ClaimTypes.Authentication).Value;
            var eventClient = GetEventServiceClient(User.Identity.Name, token);
            var layoutClient = GetLayoutServiceClient(User.Identity.Name, token);


            List<string> list = new List<string>();

            foreach (var item in layoutClient.GetAll())
            {
                list.Add(item.Name);
            }

            var updEventDTO = new EventDTO
            {
                Id = model.Id,
                Name = model.Name,
                DateTimeStart = model.DateTimeStart,
                DateTimeFinish = model.DateTimeFinish,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                LayoutId = layoutClient.GetByName(model.LayoutName).Id
            };

            if (updEventDTO.LayoutId == eventClient.GetById(model.Id).LayoutId)
            {
                try
                {
                    eventClient.Update(updEventDTO);
                }
                catch (FaultException<WcfEventServiceReference.InvalidEventException> e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    model.LayoutsNames = list;
                    return View(model);
                }

                var message = Resources.EventManagerControllerTexts.EventUpdated;

                eventClient.Close();
                layoutClient.Close();

                return RedirectToAction("Index", new { message });
            }
            else
            {
                var newModel = new ForEventManagerViewModel()
                {
                    Name = model.Name,
                    Description = model.Description,
                    DateTimeStart = model.DateTimeStart,
                    DateTimeFinish = model.DateTimeFinish,
                    ImageUrl = model.ImageUrl,
                    LayoutName = model.LayoutName,
                };

                eventClient.Delete(model.Id);

                eventClient.Close();
                layoutClient.Close();

                return RedirectToAction("SetPricesOnAreas", newModel);
            }
        }
    }
}