using System;
using System.Collections.Generic;
using WebUI.WcfPublicServiceReference;

namespace WebUI.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeFinish { get; set; }
        public string ImageUrl { get; set; }
        public int LayoutId { get; set; }

        public string VenueName { get; set; }
        public string VenueDescription { get; set; }
        public string VenueAddress { get; set; }
        public string VenuePhone { get; set; }
        public string LayoutName { get; set; }
        public string LayoutDescription { get; set; }

        public List<EventAreaDTO> EventAreaDTOs { get; set; }
        public List<EventSeatDTO> EventSeatDTOs { get; set; }
    }
}