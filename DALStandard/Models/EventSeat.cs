using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class EventSeat
    {
        public int Id { get; set; }
        public int EventAreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int State { get; set; }

        public EventArea EventArea { get; set; }
    }
}
