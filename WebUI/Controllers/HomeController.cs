using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using PagedList;
using WebUI.WcfPublicServiceReference;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {            
            PublicServiceClient client = new PublicServiceClient();

            var events = client.GetAll().Where(x => x.DateTimeStart > DateTime.Now).OrderBy(x => x.DateTimeStart);
            var list = new List<EventViewModel>();
            foreach (var e in events)
            {
                var eventInfo = client.GetEventInfo(e.Id);

                list.Add(new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    DateTimeStart = e.DateTimeStart,
                    DateTimeFinish = e.DateTimeFinish,
                    ImageUrl = e.ImageUrl,
                    LayoutId = e.LayoutId,

                    VenueName = eventInfo.VenueName,
                    VenueDescription = eventInfo.VenueDescription,
                    VenueAddress = eventInfo.VenueAddress,
                    VenuePhone = eventInfo.VenuePhone,
                    LayoutName = eventInfo.LayoutName,
                    LayoutDescription = eventInfo.LayoutDescription,

                    EventAreaDTOs = client.GetAllEventAreasDTOByEventId(e.Id).ToList(),
                    EventSeatDTOs = client.GetAllEventSeatDTOByEventId(e.Id).ToList()
                });
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            client.Close();
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id)
        {
            PublicServiceClient client = new PublicServiceClient();

            var e = client.GetById(id);
            var eventInfo = client.GetEventInfo(id);

            var evm = new EventViewModel()
            {
                Id = id,
                Name = e.Name,
                Description = e.Description,
                DateTimeStart = e.DateTimeStart,
                DateTimeFinish = e.DateTimeFinish,
                ImageUrl = e.ImageUrl,
                LayoutId = e.LayoutId,

                VenueName = eventInfo.VenueName,
                VenueDescription = eventInfo.VenueDescription,
                VenueAddress = eventInfo.VenueAddress,
                VenuePhone = eventInfo.VenuePhone,
                LayoutName = eventInfo.LayoutName,
                LayoutDescription = eventInfo.LayoutDescription,

                EventAreaDTOs = client.GetAllEventAreasDTOByEventId(e.Id).ToList(),
                EventSeatDTOs = client.GetAllEventSeatDTOByEventId(e.Id).ToList()
            };

            client.Close();
            return View(evm);
        }
    }
}