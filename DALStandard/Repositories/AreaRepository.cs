using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        public void Create(Area obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Area.Add(obj);
                db.SaveChanges();
            }
        }

        public void Create(IEnumerable<Area> areas)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Area.AddRange(areas);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Area.Remove(db.Area.Find(id));
                db.SaveChanges();
            }
        }

        public IEnumerable<Area> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Area.ToList();
            }
        }

        public Area GetById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Area.Find(id);
            }
        }

        public void Update(Area obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var a = db.Area.Find(obj.Id);
                a.LayoutId = obj.LayoutId;
                a.Description = obj.Description;
                a.CoordX = obj.CoordX;
                a.CoordY = obj.CoordY;
                db.SaveChanges();
            }
        }
    }
}
