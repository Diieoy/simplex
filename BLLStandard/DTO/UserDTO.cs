using Microsoft.AspNet.Identity;

namespace BLLStandard.DTO
{
    public class UserDTO : IUser<string>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public decimal Account { get; set; }

        public string TimeZone { get; set; }

        public string Language { get; set; }
    }
}
