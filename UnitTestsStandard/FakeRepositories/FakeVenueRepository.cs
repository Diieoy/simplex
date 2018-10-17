using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;

namespace UnitTestsStandard.FakeRepositories
{
    public class FakeVenueRepository : IVenueRepository
    {
        private List<Venue> list;

        public FakeVenueRepository()
        {
            list = new List<Venue>();
        }

        public void Create(IEnumerable<Venue> venues)
        {
            list.AddRange(venues);
        }

        public void Create(Venue obj)
        {
            list.Add(obj);
        }

        public void Delete(int id)
        {
            list.Remove(list.Find(x => x.Id == id));
        }

        public IEnumerable<Venue> GetAll()
        {
            return list;
        }

        public Venue GetById(int id)
        {
            return list.Find(x => x.Id == id);
        }

        public Venue GetByName(string name)
        {
            return list.Find(x => x.Name == name);
        }

        public void Update(Venue obj)
        {
            list.Find(x => x.Id == obj.Id).Name = obj.Name;
            list.Find(x => x.Id == obj.Id).Description = obj.Description;
            list.Find(x => x.Id == obj.Id).Address = obj.Address;
            list.Find(x => x.Id == obj.Id).Phone = obj.Phone;
        }
    }
}
