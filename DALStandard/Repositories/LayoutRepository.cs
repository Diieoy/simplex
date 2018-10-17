using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class LayoutRepository : ILayoutRepository
    {
        public void Create(Layout obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Layout.Add(obj);
                db.SaveChanges();
            }
        }

        public void Create(IEnumerable<Layout> layouts)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Layout.AddRange(layouts);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Layout.Remove(db.Layout.Find(id));
                db.SaveChanges();
            }
        }

        public IEnumerable<Layout> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Layout.ToList();
            }
        }

        public Layout GetById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Layout.Find(id);
            }
        }

        public Layout GetByName(string name)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Layout.FirstOrDefault(x => x.Name == name);
            }
        }

        public void Update(Layout obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var l = db.Layout.Find(obj.Id);
                l.Name = obj.Name;
                l.Description = obj.Description;
                l.VenueId = obj.VenueId;
                db.SaveChanges();
            }
        }
    }
}
