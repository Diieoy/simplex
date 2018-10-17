using System.ComponentModel;

namespace WpfApp.DTO
{
    public enum UserLanguage { en = 1, ru, be }

    public class UserModel : IDataErrorInfo
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public decimal Account { get; set; }

        public string TimeZone { get; set; }

        public UserLanguage UserLanguage { get; set; }

        public string Role { get; set; }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "UserName":
                        if (UserName == "")
                        {
                            result = "Required!";
                        }
                        break;
                    case "FirstName":
                        if (FirstName == "")
                        {
                            result = "Required!";
                        }
                        break;
                    case "Surname":
                        if (Surname == "")
                        {
                            result = "Required!";
                        }
                        break;
                    case "Email":
                        if (Email == "")
                        {
                            result = "Required!";
                        }
                        break;
                    case "PasswordHash":
                        if (PasswordHash == "")
                        {
                            result = "Required!";
                        }
                        break;
                    case "Role":
                        if (Role == "")
                        {
                            result = "Required!";
                        }
                        break;

                }
                return result;
            }
        }

        public string Error { get { return null; } }
    }
}
