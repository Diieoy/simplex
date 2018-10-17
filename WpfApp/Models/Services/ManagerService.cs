using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DTO;
using WpfApp.Models.Services;
using WpfApp.Resources;
using WpfApp.ViewModels;

namespace WpfApp.Models
{
    public class ManagerService
    {
        static BindableCollection<VenueModel> venues = new BindableCollection<VenueModel>();
        static BindableCollection<LayoutModel> layouts = new BindableCollection<LayoutModel>();

        public BindableCollection<VenueModel> Venues { get { return venues; } set { venues = value; } }
        public BindableCollection<LayoutModel> Layouts { get { return layouts; } set { layouts = value; } }

        private WcfVenueServiceReference.VenueServiceClient GetVenueServiceClient(string name, string password)
        {
            var client = new WcfVenueServiceReference.VenueServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        private WcfLayoutServiceReference.LayoutServiceClient GetLayoutServiceClient(string name, string password)
        {
            var client = new WcfLayoutServiceReference.LayoutServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        private WcfAreaServiceReference.AreaServiceClient GetAreaServiceClient(string name, string password)
        {
            var client = new WcfAreaServiceReference.AreaServiceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        private WcfSeatServiceReference.SeatServiceceClient GetSeatServiceceClient(string name, string password)
        {
            var client = new WcfSeatServiceReference.SeatServiceceClient();
            client.ClientCredentials.UserName.UserName = name;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }

        //Venue flow
        public async Task GetAllVenuesAsync()
        {            
            var venueClient = GetVenueServiceClient(UserInfo.Username, UserInfo.Token);

            var venues = await venueClient.GetAllAsync();

            var list = new List<VenueModel>();
            foreach (var item in venues)
            {
                list.Add(new VenueModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Address = item.Address,
                    Phone = item.Phone
                });
            }

            Venues.Clear();
            Venues.AddRange(list);
        }

        public async Task DeleteVenueAsync(int venueId)
        {
            var venueClient = GetVenueServiceClient(UserInfo.Username, UserInfo.Token);

            var allLayoutsByVenueId = GetAllLayoutsByVenueId(venueId);
            foreach (var item in allLayoutsByVenueId)
            {
                await DeleteLayoutAsync(item.Id);
            }

            await venueClient.DeleteAsync(venueId);
        }

        public async Task CreateVenueAsync(VenueModel model)
        {
            var venueClient = GetVenueServiceClient(UserInfo.Username, UserInfo.Token);

            try
            {
                await venueClient.CreateAsync(new WcfVenueServiceReference.VenueDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    Phone = model.Phone
                });
            }
            catch (Exception e)
            {
                if (e.Message.Equals("name must be unique"))
                {
                    var wm = IoC.Get<WindowManager>();
                    wm.ShowDialog(new ErrorViewModel(MyResources.NMBU));
                }
            }           
        }

        public async Task UpdateVenueAsync(VenueModel model)
        {
            var venueClient = GetVenueServiceClient(UserInfo.Username, UserInfo.Token);

            try
            {
                await venueClient.UpdateAsync(new WcfVenueServiceReference.VenueDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    Phone = model.Phone
                });
            }
            catch (Exception e)
            {
                if (e.Message.Equals("name must be unique"))
                {
                    var wm = IoC.Get<WindowManager>();
                    wm.ShowDialog(new ErrorViewModel(MyResources.NMBU));
                }
            }            
        }


        //Layout flow
        public async Task GetAllLayoutsAsync()
        {
            var layoutClient = GetLayoutServiceClient(UserInfo.Username, UserInfo.Token);

            var layouts = await layoutClient.GetAllAsync();
            var list = new List<LayoutModel>();
            foreach (var item in layouts)
            {
                list.Add(new LayoutModel
                {
                    Id = item.Id,
                    VenueId = item.VenueId,
                    Name = item.Name,
                    Description = item.Description,                    
                });
            }
            
            Layouts.Clear();
            Layouts.AddRange(list.OrderBy(x => x.Name));
        }

        public BindableCollection<LayoutModel> GetAllLayoutsByVenueId(int venueId)
        {
            var layoutClient = GetLayoutServiceClient(UserInfo.Username, UserInfo.Token);

            var layouts =  layoutClient.GetAll();
            var list = new List<LayoutModel>();
            foreach (var item in layouts)
            {
                list.Add(new LayoutModel
                {
                    Id = item.Id,
                    VenueId = item.VenueId,
                    Name = item.Name,
                    Description = item.Description,
                });
            }

            var layoutsByVenueId = list.FindAll(x => x.VenueId == venueId).OrderBy(x => x.Name).ToList();
            BindableCollection<LayoutModel> col = new BindableCollection<LayoutModel>();
            col.AddRange(layoutsByVenueId);

            return col;
        }

        public async Task CreateLayoutAsync(LayoutModel model)
        {
            var layoutClient = GetLayoutServiceClient(UserInfo.Username, UserInfo.Token);

            try
            {
                await layoutClient.CreateAsync(new WcfLayoutServiceReference.LayoutDTO
                {
                    Name = model.Name,
                    VenueId = model.VenueId,
                    Description = model.Description,
                });
            }
            catch (Exception e)
            {
                if (e.Message.Equals("name must be unique"))
                {
                    var wm = IoC.Get<WindowManager>();
                    wm.ShowDialog(new ErrorViewModel(MyResources.NMBU));
                }
            }         
        }

        public async Task UpdateLayoutAsync(LayoutModel model)
        {
            var layoutClient = GetLayoutServiceClient(UserInfo.Username, UserInfo.Token);

            try
            {
                await layoutClient.UpdateAsync(new WcfLayoutServiceReference.LayoutDTO
                {
                    Id = model.Id,
                    VenueId = model.VenueId,
                    Name = model.Name,
                    Description = model.Description,
                });
            }
            catch(Exception e)
            {
                if (e.Message.Equals("name must be unique"))
                {
                    var wm = IoC.Get<WindowManager>();
                    wm.ShowDialog(new ErrorViewModel(MyResources.NMBU));
                }
            }           
        }

        public async Task DeleteLayoutAsync(int layoutId)
        {
            var layoutClient = GetLayoutServiceClient(UserInfo.Username, UserInfo.Token);
            var allAreasByLayoutId = GetAllAreasByLayoutId(layoutId);

            foreach (var item in allAreasByLayoutId)
            {
                await DeleteSeatMapAsync(item.Id);
                await DeleteAreaAsync(item.Id);
            }

            await layoutClient.DeleteAsync(layoutId);
        }


        //Area flow
        public BindableCollection<AreaModel> GetAllAreasByLayoutId(int layoutId)
        {
            var areaClient = GetAreaServiceClient(UserInfo.Username, UserInfo.Token);

            var areas = areaClient.GetAll();
            var list = new List<AreaModel>();
            foreach (var item in areas)
            {
                list.Add(new AreaModel
                {
                    Id = item.Id,
                    LayoutId = item.LayoutId,
                    Description = item.Description,
                    CoordX = item.CoordX,
                    CoordY = item.CoordY
                });
            }

            var areasByLayoutId = list.FindAll(x => x.LayoutId == layoutId).ToList();
            var col = new BindableCollection<AreaModel>();
            col.AddRange(areasByLayoutId);

            return col;
        }

        public async Task CreateAreaAsync(AreaModel model)
        {
            var areaClient = GetAreaServiceClient(UserInfo.Username, UserInfo.Token);

            try
            {
                await areaClient.CreateAsync(new WcfAreaServiceReference.AreaDTO
                {
                    LayoutId = model.LayoutId,
                    Description = model.Description,
                    CoordX = model.CoordX,
                    CoordY = model.CoordY
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteAreaAsync(int areaId)
        {
            var areaClient = GetAreaServiceClient(UserInfo.Username, UserInfo.Token);

            await areaClient.DeleteAsync(areaId);
        }

        public async Task UpdateAreaAsync(AreaModel model)
        {
            var areaClient = GetAreaServiceClient(UserInfo.Username, UserInfo.Token);

            try
            {
                await areaClient.UpdateAsync(new WcfAreaServiceReference.AreaDTO
                {
                    Id = model.Id,
                    LayoutId = model.LayoutId,
                    Description = model.Description,
                    CoordX = model.CoordX,
                    CoordY = model.CoordY
                });
            }
            catch (Exception e)
            {
                if (e.Message.Equals("description must be unique"))
                {
                    var wm = IoC.Get<WindowManager>();
                    wm.ShowDialog(new ErrorViewModel(MyResources.DMBU));
                }
            }           
        }


        //Seat flow
        public BindableCollection<SeatModel> GetAllSeatsByAreaId(int areaId)
        {
            var seatClient = GetSeatServiceceClient(UserInfo.Username, UserInfo.Token);

            var seats = seatClient.GetAll();
            var list = new List<SeatModel>();
            foreach (var item in seats)
            {
                list.Add(new SeatModel
                {
                    Id = item.Id,
                    AreaId = item.AreaId,
                    Row = item.Row,
                    Number = item.Number
                });
            }

            var seatsByAreaId = list.FindAll(x => x.AreaId == areaId).ToList();
            var col = new BindableCollection<SeatModel>();
            col.AddRange(seatsByAreaId);

            return col;
        }

        public void CreateSeatMap(BindableCollection<SeatModel> seats)
        {
            var seatClient = GetSeatServiceceClient(UserInfo.Username, UserInfo.Token);

            foreach (var item in seats)
            {
                seatClient.Create(new WcfSeatServiceReference.SeatDTO
                {
                    AreaId = item.AreaId,
                    Row = item.Row,
                    Number = item.Number
                });
            }
        }

        public async Task UpdateSeatMap(BindableCollection<SeatModel> seats)
        {
            var seatClient = GetSeatServiceceClient(UserInfo.Username, UserInfo.Token);

            await DeleteSeatMapAsync(seats.First().AreaId);
            CreateSeatMap(seats);
        }

        public async Task DeleteSeatMapAsync(int areaId)
        {
            var seatClient = GetSeatServiceceClient(UserInfo.Username, UserInfo.Token);

            var allSeatsByAreaId = GetAllSeatsByAreaId(areaId);

            foreach (var item in allSeatsByAreaId)
            {
                await seatClient.DeleteAsync(item.Id);
            }
        }

    }
}
