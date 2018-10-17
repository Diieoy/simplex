using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventSeatId { get; set; }

        public User User { get; set; }
    }
}
