using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using WpfApp.DTO;
using WpfApp.Models;
using WpfApp.Resources;

namespace WpfApp.ViewModels
{
    public class UsersViewModel : Screen
    {
        protected UserService userService;
        protected IWindowManager windowManager;
        private BindableCollection<UserModel> users;
        private BindableCollection<string> allRoles;
        private UserModel selectedUser;
        private string selectedRole;
        private TimeZoneInfo selectedTimeZone;        
        private IEnumerable<UserLanguage> languages;
        private UserLanguage selectedLanguage;
        public ReadOnlyCollection<TimeZoneInfo> TimeZones { get; }

        public UsersViewModel(UserService userService)
        {
            this.userService = userService;
            windowManager = IoC.Get<IWindowManager>();
            users = new BindableCollection<UserModel>();
            allRoles = new BindableCollection<string>();
            TimeZones = TimeZoneInfo.GetSystemTimeZones();            
        }

        public IEnumerable<UserLanguage> Languages
        {
            get { return Enum.GetValues(typeof(UserLanguage)).Cast<UserLanguage>(); }
            set
            {
                languages = value;
                NotifyOfPropertyChange(() => languages);
            }
        }

        public UserLanguage SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                NotifyOfPropertyChange(() => selectedLanguage);
            }
        }

        public BindableCollection<UserModel> Users
        {            
            get { return users; }
            set
            {
                users = value;
                NotifyOfPropertyChange(() => users);
            }
        }

        public BindableCollection<string> AllRoles
        {
            get { return allRoles; }
            set
            {
                allRoles = value;
                NotifyOfPropertyChange(() => allRoles);
            }
        }

        public UserModel SelectedUser
        {
            get
            {
                if (selectedUser == null)
                {
                    selectedUser = new UserModel();
                }
                return selectedUser;
            }
            set
            {
                selectedUser = value;                
                NotifyOfPropertyChange(() => selectedUser);
            }
        }

        public TimeZoneInfo SelectedTimeZone
        {
            get { return selectedTimeZone; }
            set
            {
                selectedTimeZone = value;
                NotifyOfPropertyChange(() => selectedTimeZone);
            }
        }

        public string SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                NotifyOfPropertyChange(() => selectedRole);
            }
        }

        public async void AddUser()
        {
            var viewModel = new CreateUserViewModel(userService);

            await userService.LoadRolesAsync();
            viewModel.AllRoles = userService.AllRoles;

            windowManager.ShowDialog(viewModel);
            
            await userService.LoadUsersAsync();
        }

        public async void UpdateUser()
        {
            var user = new UserModel
            {
                Id = selectedUser.Id,
                UserName = selectedUser.UserName,
                PasswordHash = selectedUser.PasswordHash,
                FirstName = selectedUser.FirstName,
                Surname = selectedUser.Surname,
                Email = selectedUser.Email,
                Account = selectedUser.Account,
                Role = SelectedRole == null ? selectedUser.Role : SelectedRole,
                TimeZone = SelectedTimeZone == null ? selectedUser.TimeZone : SelectedTimeZone.ToString(),
                UserLanguage = SelectedLanguage == 0 ? selectedUser.UserLanguage : SelectedLanguage
            };

            if (String.IsNullOrEmpty(selectedUser.FirstName) || String.IsNullOrEmpty(selectedUser.Surname) ||
                String.IsNullOrEmpty(selectedUser.Email))
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.FillAllFields));
            }
            else
            {
                var regx = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
                if (selectedUser.Email != null && !Regex.IsMatch(selectedUser.Email, regx))
                {
                    windowManager.ShowDialog(new ErrorViewModel(MyResources.InvalidEmail));
                }
                else
                {
                    SelectedUser = await userService.UpdateAsync(user);
                    await userService.LoadUsersAsync();
                }                
            }          
        }

        public void DeleteUser()
        {            
            if(selectedUser.Id == null)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectUserMsg));
            }
            userService.Delete(selectedUser.Id);
            users.Remove(selectedUser);
        }
                    
    }   
}
