using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class EventArea
    {
        public EventArea()
        {
            EventSeat = new HashSet<EventSeat>();
        }

        public int Id { get; set; }
        public int EventId { get; set; }
        public int LayoutId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public decimal Price { get; set; }

        public Event Event { get; set; }
        public Layout Layout { get; set; }
        public ICollection<EventSeat> EventSeat { get; set; }
    }
}
