using System;
using System.Collections.Generic;

namespace DALStandard.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
            Purchase = new HashSet<Purchase>();
            UserRole = new HashSet<UserRole>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public decimal Account { get; set; }
        public string TimeZone { get; set; }
        public string Language { get; set; }

        public ICollection<Order> Order { get; set; }
        public ICollection<Purchase> Purchase { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}
