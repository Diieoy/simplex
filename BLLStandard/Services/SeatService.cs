using BLLStandard.DTO;
using BLLStandard.ServicesInterfaces;
using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLLStandard.Services
{
    public class SeatService : ISeatServicece
    {
        private ISeatRepository repository;

        public SeatService(ISeatRepository repository)
        {
            this.repository = repository;
        }

        public void Create(IEnumerable<SeatDTO> seatDTOs)
        {
            var list = new List<Seat>();

            foreach (var item in seatDTOs)
            {
                if (!IsRowAndNumberUniqueByAreaId(item.AreaId, item.Row, item.Number))
                {
                    throw new Exception("Not a unique seat!");
                }

                repository.Create(new Seat { AreaId = item.AreaId, Row = item.Row, Number = item.Number });
            }

            repository.Create(list);
        }

        public void Create(SeatDTO obj)
        {
            if (!IsRowAndNumberUniqueByAreaId(obj.AreaId, obj.Row, obj.Number))
            {
                throw new Exception("Not a unique seat!");
            }

            repository.Create(new Seat { AreaId = obj.AreaId, Row = obj.Row, Number = obj.Number });
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<SeatDTO> GetAll()
        {
            var list = new List<SeatDTO>();

            foreach (var item in repository.GetAll())
            {
                list.Add(new SeatDTO { Id = item.Id, AreaId = item.AreaId, Row = item.Row, Number = item.Number });
            }

            return list;
        }

        public SeatDTO GetById(int id)
        {
            var seat = repository.GetById(id);

            if (seat == null)
            {
                return null;
            }

            return new SeatDTO { Id = seat.Id, AreaId = seat.AreaId, Row = seat.Row, Number = seat.Number };
        }

        public void Update(SeatDTO obj)
        {
            if (!IsRowAndNumberUniqueByAreaId(obj.AreaId, obj.Row, obj.Number))
            {
                throw new Exception("Not a unique seat!");
            }

            repository.Update(new Seat { Id = obj.Id, AreaId = obj.AreaId, Row = obj.Row, Number = obj.Number });
        }

        private bool IsRowAndNumberUniqueByAreaId(int areaId, int row, int number)
        {
            var listByAreaId = repository.GetAll().ToList().FindAll(x => x.AreaId == areaId);

            if (listByAreaId.Exists(x => x.Row == row))
            {
                if (listByAreaId.FindAll(x => x.Row == row).Exists(x => x.Number == number))
                {
                    return number == -1;
                }
            }

            return true;
        }
    }
}
