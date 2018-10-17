using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace UnitTestsStandard.FakeRepositories
{
    public class FakeAreaRepository : IAreaRepository
    {
        private List<Area> list;
        private int increment { get; set; }

        public FakeAreaRepository()
        {
            list = new List<Area>();
            increment = 1;
        }

        public void Create(Area obj)
        {
            obj.Id = increment;
            list.Add(obj);
            increment++;
        }

        public void Create(IEnumerable<Area> areas)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            list.Remove(list.Find(x => x.Id == id));
        }

        public IEnumerable<Area> GetAll()
        {
            return list;
        }

        public Area GetById(int id)
        {
            return list.Find(x => x.Id == id);
        }

        public void Update(Area obj)
        {
            list.Find(x => x.Id == obj.Id).Description = obj.Description;
            list.Find(x => x.Id == obj.Id).LayoutId = obj.LayoutId;
            list.Find(x => x.Id == obj.Id).CoordX = obj.CoordX;
            list.Find(x => x.Id == obj.Id).CoordY = obj.CoordY;
        }
    }
}
