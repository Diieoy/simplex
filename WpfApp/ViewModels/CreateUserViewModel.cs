using System;
using System.Linq;
using System.Text.RegularExpressions;
using WpfApp.DTO;
using WpfApp.Models;
using WpfApp.Models.Services;
using WpfApp.Resources;

namespace WpfApp.ViewModels
{
    public class CreateUserViewModel : UsersViewModel
    {
        private UserModel userModel;

        public CreateUserViewModel(UserService userService) : base(userService)
        {
            userModel = new UserModel();
            SelectedLanguage = Languages.First();
        }

        public UserModel User
        {
            get { return userModel; }
            set
            {
                userModel = value;
                NotifyOfPropertyChange(() => userModel);
            }
        }

        public async void CreateUser()
        {
            var regx = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
            if (String.IsNullOrEmpty(userModel.UserName) || String.IsNullOrEmpty(userModel.PasswordHash) || 
                String.IsNullOrEmpty(userModel.FirstName) || String.IsNullOrEmpty(userModel.Surname) || 
                String.IsNullOrEmpty(userModel.Email) || SelectedTimeZone == null || SelectedRole == null)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.FillAllFields));                                           
            }
            else
            {
                if (userModel.Email != null && !Regex.IsMatch(userModel.Email, regx))
                {
                    windowManager.ShowDialog(new ErrorViewModel(MyResources.InvalidEmail));
                }
                else
                {
                    var token = UserInfo.Token;
                    userModel.Role = SelectedRole;
                    userModel.TimeZone = SelectedTimeZone.ToString();
                    userModel.UserLanguage = SelectedLanguage;

                    if(userModel.PasswordHash != null && userModel.PasswordHash.Count() < 4)
                    {
                        windowManager.ShowDialog(new ErrorViewModel(MyResources.TSP));
                    }
                    else
                    {
                        var result = await userService.RegisterUser(userModel);

                        if (!result.Equals("USERNAME_ALREADY_EXISTS"))
                        {
                            TryClose();
                        }
                        else
                        {
                            windowManager.ShowDialog(new ErrorViewModel(MyResources.UAE));
                        }
                    }                                 
                }
            }          
        }
    }
}
