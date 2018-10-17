using Caliburn.Micro;
using System;
using System.Linq;
using WpfApp.Models;
using WpfApp.Resources;

namespace WpfApp.ViewModels
{
    public class CreateAreaViewModel : ManagerViewModel
    {
        private AreaModel areaModel;
        private BindableCollection<SeatModel> seats;
        private string description;
        private int coordX;
        private int coordY;

        public CreateAreaViewModel(ManagerService managerService, AreaModel areaModel) : base(managerService)
        {
            this.areaModel = areaModel;
            CoordX = 2;
            CoordY = 3;
            Seats = new BindableCollection<SeatModel>();
            Seats.Add(new SeatModel
            {
                AreaId = areaModel.Id,
                Row = 0,
                Number = 0
            });
            Seats.Add(new SeatModel
            {
                AreaId = areaModel.Id,
                Row = 0,
                Number = 0
            });
            Seats.Add(new SeatModel
            {
                AreaId = areaModel.Id,
                Row = 0,
                Number = 0
            });
            Seats.Add(new SeatModel
            {
                AreaId = areaModel.Id,
                Row = 0,
                Number = 0
            });
            Seats.Add(new SeatModel
            {
                AreaId = areaModel.Id,
                Row = 0,
                Number = 0
            });
            Seats.Add(new SeatModel
            {
                AreaId = areaModel.Id,
                Row = 0,
                Number = 0
            });
        }

        public AreaModel AreaModel
        {
            get { return areaModel; }
            set
            {
                areaModel = value;
                NotifyOfPropertyChange(() => areaModel);
            }
        }

        public BindableCollection<SeatModel> Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                NotifyOfPropertyChange(() => seats);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyOfPropertyChange(() => description);
            }
        }

        public int CoordX
        {
            get { return coordX; }
            set
            {
                coordX = value;
                if(Seats != null)
                    Seats.Clear();
                NotifyOfPropertyChange(() => coordX);
            }
        }

        public int CoordY
        {
            get { return coordY; }
            set
            {
                coordY = value;
                if (Seats != null)
                    Seats.Clear();
                NotifyOfPropertyChange(() => coordY);
            }
        }

        public void SaveChanges()
        {
            Seats.Clear();
            var num = coordX * coordY;
            for (int i = 0; i < num; i++)
            {
                Seats.Add(new SeatModel
                {
                    Id = i,
                    AreaId = areaModel.Id,
                    Row = 0,
                    Number = 0
                });
            }
        }

        public void ClickButton(SeatModel selected)
        {
            var seat = Seats.FirstOrDefault(x => x.Id == selected.Id);
            if (seat.Number != -1)
            {
                Seats.First(x => x.Id == selected.Id).Number = -1;
            }
            else
            {
                Seats.First(x => x.Id == selected.Id).Number = 1;
            }
        }

        public async void CreateNewMap()
        {
            if (Description == null || CoordX <= 0 || CoordY <= 0)
            {
                windowManager.ShowDialog(new ErrorViewModel(MyResources.InputError));
            }
            else
            {
                areaModel.Description = Description;
                areaModel.CoordX = CoordX;
                areaModel.CoordY = CoordY;

                try
                {
                    await managerService.CreateAreaAsync(areaModel);

                    var newSeatMap = Seats;

                    int row = 1;
                    int number = 1;
                    int step = 0;
                    int areaId = managerService.GetAllAreasByLayoutId(areaModel.LayoutId).Last().Id;

                    var finalList = new BindableCollection<SeatModel>();
                    foreach (var item in newSeatMap)
                    {
                        item.AreaId = areaId;
                        if (step == CoordX)
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

                    managerService.CreateSeatMap(finalList);

                    TryClose();
                }
                catch (Exception e)
                {
                    if(e.Message.Equals("description must be unique"))
                    {
                        windowManager.ShowDialog(new ErrorViewModel(MyResources.DMBU));
                    }
                }
            }           
        }
    }
}
