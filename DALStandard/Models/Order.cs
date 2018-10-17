using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime DateTimeOrder { get; set; }

        public User User { get; set; }
    }
}
