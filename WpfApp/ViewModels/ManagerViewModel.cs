using Caliburn.Micro;
using System.Linq;
using WpfApp.DTO;
using WpfApp.Models;
using WpfApp.Resources;

namespace WpfApp.ViewModels
{
    public class ManagerViewModel : Screen
    {
        protected ManagerService managerService;
        protected IWindowManager windowManager;
        private BindableCollection<VenueModel> venues;
        private BindableCollection<LayoutModel> layoutsByVenueId;
        private BindableCollection<AreaModel> areasByLayoutId;
        private BindableCollection<SeatModel> seatsByAreaId;
        private VenueModel selectedVenue;
        private LayoutModel selectedLayout;
        private AreaModel selectedArea;
        private SeatModel selectedSeat;

        public ManagerViewModel(ManagerService managerService)
        {
            windowManager = IoC.Get<IWindowManager>();
            this.managerService = managerService;          
        }

        public BindableCollection<VenueModel> Venues
        {
            get { return venues; }
            set
            {
                venues = value;
                LayoutsByVenueId = null;
                AreasByLayoutId = null;
                SeatsByAreaId = null;
                NotifyOfPropertyChange(() => venues);
            }
        }

        public BindableCollection<LayoutModel> LayoutsByVenueId
        {
            get { return layoutsByVenueId; }
            set
            {
                layoutsByVenueId = value;
                AreasByLayoutId = null;
                SeatsByAreaId = null;
                NotifyOfPropertyChange(() => layoutsByVenueId);
            }
        }

        public BindableCollection<AreaModel> AreasByLayoutId
        {
            get { return areasByLayoutId; }
            set
            {
                areasByLayoutId = value;
                SeatsByAreaId = null;
                NotifyOfPropertyChange(() => areasByLayoutId);
            }
        }

        public BindableCollection<SeatModel> SeatsByAreaId
        {
            get { return seatsByAreaId; }
            set
            {
                seatsByAreaId = value;
                NotifyOfPropertyChange(() => seatsByAreaId);
            }
        }

        public VenueModel SelectedVenue
        {
            get
            {
                if (selectedVenue == null)
                {
                    selectedVenue = new VenueModel();
                }
                return selectedVenue;
            }
            set
            {
                selectedVenue = value;
                if(selectedVenue != null)
                {
                    if(LayoutsByVenueId != null)
                    {
                        LayoutsByVenueId.Clear();
                    }

                    LayoutsByVenueId = managerService.GetAllLayoutsByVenueId(selectedVenue.Id);
                }
                NotifyOfPropertyChange(() => selectedVenue);
            }
        }

        public LayoutModel SelectedLayout
        {
            get
            {
                if (selectedLayout == null)
                {
                    selectedLayout = new LayoutModel();
                }
                return selectedLayout;
            }
            set
            {
                selectedLayout = value;
                if (selectedLayout != null)
                {
                    if(AreasByLayoutId != null)
                    {
                        AreasByLayoutId.Clear();
                    }

                    AreasByLayoutId = managerService.GetAllAreasByLayoutId(selectedLayout.Id);
                }
                NotifyOfPropertyChange(() => selectedLayout);
            }
        }

        public AreaModel SelectedArea
        {
            get
            {
                if (selectedArea == null)
                {
                    selectedArea = new AreaModel();
                }
                return selectedArea;
            }
            set
            {
                selectedArea = value;
                if(selectedArea != null)
                    SeatsByAreaId = managerService.GetAllSeatsByAreaId(selectedArea.Id);
                NotifyOfPropertyChange(() => selectedArea);
            }
        }

        public SeatModel SelectedSeat
        {
            get
            {
                if (selectedSeat == null)
                {
                    selectedSeat = new SeatModel();
                }
                return selectedSeat;
            }
            set
            {
                selectedSeat = value;
                NotifyOfPropertyChange(() => selectedSeat);
            }
        }

        //Venue operations
        public async void CreateVenue()
        {
            windowManager.ShowDialog(new CreateVenueViewModel(managerService));

            await managerService.GetAllVenuesAsync();
        }

        public async void DeleteVenue()
        {  
            if (selectedVenue.Id != 0)
            {
                await managerService.DeleteVenueAsync(selectedVenue.Id);
                await managerService.GetAllVenuesAsync();

                LayoutsByVenueId = null;
                SeatsByAreaId = null;
                AreasByLayoutId = null;              
            }
            else
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectVenueMsg));
            }  
        }

        public async void UpdateVenue()
        {
            if (selectedVenue.Id == 0)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectVenueMsg));
            }
            else
            {
                await managerService.UpdateVenueAsync(selectedVenue);
                await managerService.GetAllVenuesAsync();                       
            }           
        }  
        
        //Layout operations
        public void CreateLayout()
        {
            var layoutModel = new LayoutModel();
            layoutModel.VenueId = selectedVenue.Id;

            windowManager.ShowDialog(new CreateLayoutViewModel(managerService, layoutModel));
            LayoutsByVenueId = managerService.GetAllLayoutsByVenueId(selectedVenue.Id);
        }

        public async void DeleteLayout()
        {
            if (selectedLayout.Id == 0)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectLayoutMsg));
            }
            else
            {
                await managerService.DeleteLayoutAsync(selectedLayout.Id);
                LayoutsByVenueId = managerService.GetAllLayoutsByVenueId(selectedVenue.Id);
                SeatsByAreaId = null;
                AreasByLayoutId = null;
            }
        }

        public async void UpdateLayout()
        {
            if (selectedLayout.Id == 0)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectLayoutMsg));
            }
            else
            {
                selectedLayout.VenueId = selectedVenue.Id;                
                await managerService.UpdateLayoutAsync(selectedLayout);
                LayoutsByVenueId = managerService.GetAllLayoutsByVenueId(SelectedVenue.Id);
            }           
        }


        //Area operations
        public void CreateArea()
        {
            var areaModel = new AreaModel();
            if (SelectedLayout.Id == 0)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectLayoutMsg));
            }
            else
            {
                areaModel.LayoutId = selectedLayout.Id;

                windowManager.ShowDialog(new CreateAreaViewModel(managerService, areaModel));
                AreasByLayoutId = managerService.GetAllAreasByLayoutId(selectedLayout.Id);
            }
        }

        public async void UpdateArea()
        {
            if (SelectedArea.Id == 0)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectAreaMsg));
            }
            else
            {
                selectedArea.LayoutId = selectedLayout.Id;
                await managerService.UpdateAreaAsync(selectedArea);         
            }

            AreasByLayoutId = managerService.GetAllAreasByLayoutId(selectedLayout.Id);
        }

        public async void DeleteArea()
        {
            if (SelectedArea.Id == 0)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectAreaMsg));
            }
            else
            {
                await managerService.DeleteSeatMapAsync(selectedArea.Id);
                await managerService.DeleteAreaAsync(selectedArea.Id);

                seatsByAreaId.Clear();
                AreasByLayoutId = managerService.GetAllAreasByLayoutId(selectedLayout.Id);
            }
        }


        //Seat map operations
        public async void UpdateSeatMap()
        {
            if (SeatsByAreaId == null)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.SelectAreaMsg));
            }
            else
            {
                var seats = SeatsByAreaId;
                int coordX = selectedArea.CoordX;

                int row = 1;
                int number = 1;
                int step = 0;

                var finalList = new BindableCollection<SeatModel>();
                foreach (var item in seats)
                {
                    if (step == coordX)
                    {
                        row++;
                        number = 1;
                        step = 0;
                    }

                    if (item.Number != -1)
                    {
                        item.Row = row;
                        item.Number = number;
                        number++;
                        finalList.Add(item);
                    }
                    else
                    {
                        item.Row = row;
                        finalList.Add(item);
                    }

                    step++;
                }

                await managerService.UpdateSeatMap(finalList);
            }
        }

        public void PressButton(SeatModel selected)
        {
            var seat = SeatsByAreaId.First(x => x.Id == selected.Id);
            if (seat.Number != -1)
            {
                SeatsByAreaId.First(x => x.Id == selected.Id).Number = -1;
            }
            else
            {
                SeatsByAreaId.First(x => x.Id == selected.Id).Number = 1;
            }
        }
    }
}
