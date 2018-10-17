using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class Seat
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }

        public Area Area { get; set; }
    }
}
