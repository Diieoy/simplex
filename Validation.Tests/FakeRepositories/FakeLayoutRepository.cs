using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace UnitTests.FakeRepositories
{
    public class FakeLayoutRepository : ILayoutRepository
    {
        private List<Layout> list;
        private int increment { get; set; }

        public FakeLayoutRepository()
        {
            list = new List<Layout>();
            increment = 1;
        }

        public void Create(Layout obj)
        {
            obj.Id = increment;
            list.Add(obj);
            increment++;
        }

        public void Create(IEnumerable<Layout> layouts)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            list.Remove(list.Find(x => x.Id == id));
        }

        public IEnumerable<Layout> GetAll()
        {
            return list;
        }

        public Layout GetById(int id)
        {
            return list.Find(x => x.Id == id);
        }

        public Layout GetByName(string name)
        {
            return list.Find(x => x.Name == name);
        }

        public void Update(Layout obj)
        {
            list.Find(x => x.Id == obj.Id).Name = obj.Name;
            list.Find(x => x.Id == obj.Id).Description = obj.Description;
            list.Find(x => x.Id == obj.Id).VenueId = obj.VenueId;
        }
    }
}
