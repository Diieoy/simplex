using BLLStandard.DTO;
using System;
using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface IEventService : IService<EventDTO>
    {
        void Create(EventSeatDTO obj);
        void Update(EventAreaDTO obj);
        void Update(EventSeatDTO obj);
        EventSeatDTO GetEventSeatDTOById(int id);
        void DeleteEventSeat(int id);
        EventInfoDTO GetEventInfo(int eventId);
        IEnumerable<EventAreaDTO> GetAllEventAreasDTOByEventId(int eventId);
        EventAreaDTO GetEventAreaDTOById(int id);
        IEnumerable<EventSeatDTO> GetAllEventSeatsDTOByEventAreaId(int areaId);
        IEnumerable<EventSeatDTO> GetAllEventSeatDTOByEventId(int eventId);
        bool IsEventValid(int eventDTOId, DateTime dateTimeStart, DateTime dateTimeFinish, int layoutId);
    }
}
