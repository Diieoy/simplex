using System;

namespace WebUI.Models
{
    public class PurchaseHistoryViewModel
    {
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }

        public string LayoutName { get; set; }

        public string EventName { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeFinish { get; set; }

        public string EventAreaDescription { get; set; }
        public decimal EventAreaPrice { get; set; }
        public int EventSeatRow { get; set; }
        public int EventSeatNumber { get; set; }

        public DateTime DateTimeOrder { get; set; }
    }
}