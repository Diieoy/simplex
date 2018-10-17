using Caliburn.Micro;
using LocalizatorHelper;
using System.Windows;
using WpfApp.Resources;
using WpfApp.ViewModels;

namespace WpfApp
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {            
            Initialize();            
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            ResourceManagerService.RegisterManager("MyResources", MyResources.ResourceManager, true);           
            DisplayRootViewFor<LoginViewModel>();
        }
    }
}
