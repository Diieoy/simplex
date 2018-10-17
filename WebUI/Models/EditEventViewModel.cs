using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class EditEventViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStart { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeFinish { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]        
        public string LayoutName { get; set; }

        public List<string> LayoutsNames { get; set; }
    }
}