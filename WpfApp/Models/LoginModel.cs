using System.ComponentModel;

namespace WpfApp.Models
{
    public class LoginModel : IDataErrorInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }

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
                    case "Password":
                        if(Password == "")
                        {
                            result = "Required!";
                        }
                        break;
                }
                return result;
            }
        }

        public string Error
        {
            get { return null; }
        }
    }
}
