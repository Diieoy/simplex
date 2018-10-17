using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DALStandard.Repositories
{
    public class EventRepository : IEventRepository
    {
        public void Create(Event obj)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Database.ExecuteSqlCommand("CreateEvent @Name, @Description, @DateTimeStart, @DateTimeFinish, @ImageUrl, @LayoutId",
                    new SqlParameter("@Name", obj.Name),
                    new SqlParameter("@Description", obj.Description),
                    new SqlParameter("@DateTimeStart", obj.DateTimeStart),
                    new SqlParameter("@DateTimeFinish", obj.DateTimeFinish),
                    new SqlParameter("@ImageUrl", obj.ImageUrl),
                    new SqlParameter("@LayoutId", obj.LayoutId));
            }
        }

        public void Delete(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {        
                db.Database.ExecuteSqlCommand("DeleteEvent @Id", new SqlParameter("@Id", id));
            }
        }

        public IEnumerable<Event> GetAll()
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Event.ToList();           
            }
        }

        public Event GetById(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return db.Event.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Update(Event obj)
        {
            using(MyDbContext db = new MyDbContext())
            {
                var eventFromDb = db.Event.Find(obj.Id);

                if(obj.LayoutId == eventFromDb.LayoutId)
                {
                    eventFromDb.Name = obj.Name;
                    eventFromDb.Description = obj.Description;
                    eventFromDb.DateTimeStart = obj.DateTimeStart;
                    eventFromDb.DateTimeFinish = obj.DateTimeFinish;
                    eventFromDb.ImageUrl = obj.ImageUrl;
                    db.SaveChanges();
                }
                else
                {
                    Delete(obj.Id);
                    Create(obj);
                }               
            }
        }

        public bool AreThereAnySeats(int layoutId)
        {
            using(MyDbContext db = new MyDbContext())
            {
                int result = 0;
                using (var command = db.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT Count(*) FROM Seat INNER JOIN Area ON Seat.AreaId=Area.Id INNER JOIN Layout ON Area.LayoutId=Layout.Id WHERE Layout.Id=" + layoutId;
                    command.CommandType = System.Data.CommandType.Text;

                    db.Database.OpenConnection();
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                            
                        }
                    }

                }

                return (result != 0);
            }            
        }

        public IEnumerable<Event> GetEventsByLayoutId(int layoutId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var query = (from e in db.Event
                            where e.LayoutId == layoutId
                            select e).ToList();

                return query;
            }
        }

        public IEnumerable<Event> GetAllEventsByName(string name)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var query = (from e in db.Event
                             where e.Name == name
                             select e).ToList();

                return query;
            }
        }

        public List<string> GetEventInfo(int eventId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var info = from v in db.Venue
                           join l in db.Layout on v.Id equals l.VenueId
                           join e in db.Event on l.Id equals e.LayoutId
                           where e.Id == eventId
                           select new { VenueName = v.Name, VenueDescription = v.Description, VenueAddress = v.Address, VenuePhone = v.Phone, LayoutName = l.Name, LayoutDescription = l.Description };

                List<string> list = new List<string>();

                foreach (var item in info)
                {
                    list.Add(item.VenueName);
                    list.Add(item.VenueDescription);
                    list.Add(item.VenueAddress);
                    list.Add(item.VenuePhone);
                    list.Add(item.LayoutName);
                    list.Add(item.LayoutDescription);
                }

                return list;
            }
        }

        public List<EventSeat> GetAllEventSeatByEventId(int eventId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var list = (from e in db.Event
                           join ea in db.EventArea on e.Id equals ea.EventId
                           join es in db.EventSeat on ea.Id equals es.EventAreaId
                           where e.Id == eventId
                           select es).ToList();

                return list;
            }
        }
    }
}
