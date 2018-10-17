using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        public void Create(Venue obj)
        {
            using(MyDbContext db = new MyDbContext())
            {
                db.Venue.Add(obj);
                db.SaveChanges();
            }          
        }

        public void Create(IEnumerable<Venue> venues)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Venue.AddRange(venues);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using(MyDbContext db = new MyDbContext())
            {
                db.Venue.Remove(db.Venue.Find(id));
                db.SaveChanges();
            }
        }

        public IEnumerable<Venue> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Venue.ToList();
            }
        }

        public Venue GetById(int id)
        {
            using(MyDbContext db = new MyDbContext())
            {
                return db.Venue.FirstOrDefault(x => x.Id == id);
            }
        }

        public Venue GetByName(string name)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Venue.FirstOrDefault(x => x.Name == name);
            }
        }

        public void Update(Venue obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var v = db.Venue.Find(obj.Id);
                v.Name = obj.Name;
                v.Description = obj.Description;
                v.Address = obj.Address;
                v.Phone = obj.Phone;
                db.SaveChanges();
            }
        }
    }
}
