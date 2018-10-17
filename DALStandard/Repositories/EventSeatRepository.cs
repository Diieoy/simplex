using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DALStandard.Repositories
{
    public class EventSeatRepository : IEventSeatRepository
    {
        public void Create(EventSeat obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.EventSeat.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.EventSeat.Remove(db.EventSeat.Find(id));
                db.SaveChanges();
            }
        }

        public IEnumerable<EventSeat> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.EventSeat.ToList();
            }
        }

        public EventSeat GetById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.EventSeat.Find(id);
            }
        }

        public void Update(EventSeat obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var s = db.EventSeat.Find(obj.Id);

                s.EventAreaId = obj.EventAreaId;
                s.Row = obj.Row;
                s.Number = obj.Number;
                s.State = obj.State;

                db.SaveChanges();
            }
        }
    }
}
