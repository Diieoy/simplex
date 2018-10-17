using Caliburn.Micro;
using LocalizatorHelper;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        private UserService userService;
        private ManagerService managerService;

        public MainViewModel(UserService userService, ManagerService managerService)
        {
            this.managerService = managerService;
            this.userService = userService;
        }

        public async void LoadUsersPage()
        {            
            var viewModel = new UsersViewModel(userService);
            
            await userService.LoadUsersAsync();
            await userService.LoadRolesAsync();
            viewModel.Users = userService.Users;
            viewModel.AllRoles = userService.AllRoles;

            ActivateItem(viewModel);
        }

        public async void LoadManagerPage()
        {
            var viewModel = new ManagerViewModel(managerService);

            await managerService.GetAllVenuesAsync();
            viewModel.Venues = managerService.Venues;

            ActivateItem(viewModel);
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
