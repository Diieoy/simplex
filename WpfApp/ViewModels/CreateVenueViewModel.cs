using System;
using WpfApp.DTO;
using WpfApp.Models;
using WpfApp.Resources;

namespace WpfApp.ViewModels
{
    public class CreateVenueViewModel : ManagerViewModel
    {
        private VenueModel venueModel;

        public CreateVenueViewModel(ManagerService managerService) : base(managerService)
        {
            venueModel = new VenueModel();
        }

        public VenueModel VenueModel
        {
            get { return venueModel; }
            set
            {
                venueModel = value;
                NotifyOfPropertyChange(() => venueModel);
            }
        }

        public async void Create()
        {
            if (!String.IsNullOrEmpty(venueModel.Name) && !String.IsNullOrEmpty(venueModel.Description)
                && !String.IsNullOrEmpty(venueModel.Address) && !String.IsNullOrEmpty(venueModel.Phone))
            {
                var venue = new VenueModel()
                {
                    Name = venueModel.Name,
                    Description = venueModel.Description,
                    Address = venueModel.Address,
                    Phone = venueModel.Phone
                };

                await managerService.CreateVenueAsync(venue);
                TryClose();
            }
            else
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.InputError));
            }           
        }
    }
}
