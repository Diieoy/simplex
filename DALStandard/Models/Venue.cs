using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class Venue
    {
        public Venue()
        {
            Layout = new HashSet<Layout>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<Layout> Layout { get; set; }
    }
}
