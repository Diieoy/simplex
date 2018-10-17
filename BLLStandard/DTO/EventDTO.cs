﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BLLStandard.DTO
{
    public class EventDTO
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
        public int LayoutId { get; set; }
    }
}
