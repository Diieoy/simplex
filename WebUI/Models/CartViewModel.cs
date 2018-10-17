using System;

namespace WebUI.Models
{
    public class CartViewModel
    {
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }
        public string VenuePhone { get; set; }

        public string LayoutName { get; set; }

        public string EventName { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeFinish { get; set; }

        public string EventAreaDescription { get; set; }
        public decimal EventAreaPrice { get; set; }
        public int EventSeatId { get; set; }
        public int EventSeatRow { get; set; }
        public int EventSeatNumber { get; set; }
        public int EventSeatState { get; set; }

        public string UserId { get; set; }
        public decimal UserAccount { get; set; }
    }
}