using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class Event
    {
        public Event()
        {
            EventArea = new HashSet<EventArea>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeFinish { get; set; }
        public string ImageUrl { get; set; }
        public int LayoutId { get; set; }

        public Layout Layout { get; set; }
        public ICollection<EventArea> EventArea { get; set; }
    }
}
