using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace UnitTestsStandard.FakeRepositories
{
    public class FakeSeatRepository : ISeatRepository
    {
        private List<Seat> list;

        public FakeSeatRepository()
        {
            list = new List<Seat>();
        }

        public void Create(Seat obj)
        {
            list.Add(obj);
        }

        public void Create(IEnumerable<Seat> list)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Seat> GetAll()
        {
            return list;
        }

        public Seat GetById(int id)
        {
            return list.Find(x => x.Id == id);
        }

        public void Update(Seat obj)
        {
            list.Find(x => x.Id == obj.Id).AreaId = obj.AreaId;
            list.Find(x => x.Id == obj.Id).Row = obj.Row;
            list.Find(x => x.Id == obj.Id).Number = obj.Number;
        }
    }
}
