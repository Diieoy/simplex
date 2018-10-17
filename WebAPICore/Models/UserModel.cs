using System.ComponentModel.DataAnnotations;

namespace WebAPICore.Models
{
    public enum UserLanguage { en = 1, ru, be }

    public class UserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string TimeZone { get; set; }

        public UserLanguage UserLanguage { get; set; }
    }
}
