using Caliburn.Micro;
using LocalizatorHelper;
using System;
using WpfApp.Models;
using WpfApp.Models.Services;
using WpfApp.Resources;

namespace WpfApp.ViewModels
{
    public class LoginViewModel : Screen
    {
        private UserService userService = new UserService();
        private ManagerService managerService = new ManagerService();
        private LoginModel model = new LoginModel();

        public LoginModel Model
        {
            get { return model; }
            set
            {
                model = value;
                NotifyOfPropertyChange(() => model);
            }
        }

        public async void LoginButton()
        {
            var asd = IoC.Get<LoginModel>();
            var wm = IoC.Get<IWindowManager>();

            if (String.IsNullOrEmpty(model.UserName) || String.IsNullOrEmpty(model.Password))
            {
                wm.ShowDialog(new ErrorViewModel(MyResources.InputError));
            }
            else
            {
                UserInfo.Token = await userService.LoginAsync(model);
                UserInfo.Username = model.UserName;
                string role = "";

                if (UserInfo.Token != null)
                {
                    role = await userService.GetUserRoleAsync(UserInfo.Username, UserInfo.Token);

                    if (role == "venue_manager")
                    {
                        wm.ShowWindow(new MainViewModel(userService, managerService));
                        TryClose();
                    }
                }
                else
                {
                    wm.ShowDialog(new ErrorViewModel(MyResources.AuthorizationError));
                }
            }
        }

        public void RuMenu()
        {
            ResourceManagerService.ChangeLocale("ru-RU");
        }

        public void EnMenu()
        {
            ResourceManagerService.ChangeLocale("en-US");
        }

        public void BeMenu()
        {
            ResourceManagerService.ChangeLocale("be-BE");
        }
    }
}
