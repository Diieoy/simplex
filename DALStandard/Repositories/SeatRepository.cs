using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        public void Create(IEnumerable<Seat> list)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Seat.AddRange(list);
                db.SaveChanges();
            }
        }

        public void Create(Seat obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Seat.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Seat.Remove(db.Seat.Find(id));
                db.SaveChanges();
            }
        }

        public IEnumerable<Seat> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Seat.ToList();
            }
        }

        public Seat GetById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Seat.Find(id);
            }
        }

        public void Update(Seat obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var s = db.Seat.Find(obj.Id);
                s.AreaId = obj.AreaId;
                s.Row = obj.Row;
                s.Number = obj.Number;
                db.SaveChanges();
            }
        }
    }
}
