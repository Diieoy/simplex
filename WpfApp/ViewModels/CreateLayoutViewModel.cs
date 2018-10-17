using WpfApp.Models;
using WpfApp.Resources;

namespace WpfApp.ViewModels
{
    public class CreateLayoutViewModel : ManagerViewModel
    {
        private LayoutModel layoutModel;

        public CreateLayoutViewModel(ManagerService managerService, LayoutModel layoutModel) : base(managerService)
        {
            this.layoutModel = layoutModel;
        }

        public LayoutModel LayoutModel
        {
            get { return layoutModel; }
            set
            {
                layoutModel = value;
                NotifyOfPropertyChange(() => layoutModel);
            }
        }

        public async void Create()
        {
            if (string.IsNullOrEmpty(layoutModel.Name) || string.IsNullOrEmpty(layoutModel.Description))
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.InputError));
            }
            else
            {                
                var layout = new LayoutModel()
                {
                    Name = layoutModel.Name,
                    VenueId = layoutModel.VenueId,
                    Description = layoutModel.Description,
                };

                await managerService.CreateLayoutAsync(layout);
                TryClose();
            }
        }
    }
}
