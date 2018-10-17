using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class Area
    {
        public Area()
        {
            Seat = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public int LayoutId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public Layout Layout { get; set; }
        public ICollection<Seat> Seat { get; set; }
    }
}
