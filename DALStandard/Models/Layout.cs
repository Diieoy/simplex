using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class Layout
    {
        public Layout()
        {
            Area = new HashSet<Area>();
            Event = new HashSet<Event>();
            EventArea = new HashSet<EventArea>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int VenueId { get; set; }
        public string Description { get; set; }

        public Venue Venue { get; set; }
        public ICollection<Area> Area { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<EventArea> EventArea { get; set; }
    }
}
