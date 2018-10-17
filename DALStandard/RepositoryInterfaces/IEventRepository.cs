using DALStandard.Models;
using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetEventsByLayoutId(int layoutId);
        IEnumerable<Event> GetAllEventsByName(string name);
        bool AreThereAnySeats(int layoutId);
        List<string> GetEventInfo(int eventId);
        List<EventSeat> GetAllEventSeatByEventId(int eventId);
    }
}
