using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebUI.WcfEventServiceReference;

namespace WebUI.Models
{
    public class ForEventManagerViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateTimeStart { get; set; }

        [Required]
        public DateTime DateTimeFinish { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string LayoutName { get; set; }

        public List<string> LayoutsNames { get; set; }

        public List<EventAreaDTO> EventAreaDTOs { get; set; }
    }
}