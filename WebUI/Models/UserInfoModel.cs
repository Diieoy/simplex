namespace WebUI.Models
{
    public class UserInfoModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string TimeZone { get; set; }

        public UserLanguage UserLanguage { get; set; }
    }
}