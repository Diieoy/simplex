using System;

namespace BLLStandard.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SeatId { get; set; }

        public DateTime DateTimeOrder { get; set; }
    }
}
