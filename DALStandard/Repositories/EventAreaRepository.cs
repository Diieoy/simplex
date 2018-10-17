using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class EventAreaRepository : IEventAreaRepository
    {
        public void Create(EventArea obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.EventArea.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.EventArea.Remove(db.EventArea.Find(id));
                db.SaveChanges();
            }
        }

        public IEnumerable<EventArea> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.EventArea.ToList();
            }
        }

        public EventArea GetById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.EventArea.Find(id);
            }
        }

        public void Update(EventArea obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var e = db.EventArea.Find(obj.Id);

                e.LayoutId = obj.LayoutId;
                e.Description = obj.Description;
                e.CoordX = obj.CoordX;
                e.CoordY = obj.CoordY;
                e.Price = obj.Price;

                db.SaveChanges();
            }
        }
    }
}
