using System.ComponentModel.DataAnnotations;

namespace BLLStandard.DTO
{
    public class EventAreaDTO
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int LayoutId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CoordX { get; set; }

        [Required]
        public int CoordY { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
