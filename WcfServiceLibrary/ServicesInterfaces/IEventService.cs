using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.CustomExceptions;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface IEventService : IService<EventDTO>
    {
        [OperationContract(Name = "CreateEventSeats")]
        void Create(EventSeatDTO obj);

        [OperationContract(Name = "UpdateEventAreas")]
        void Update(EventAreaDTO obj);

        [OperationContract(Name = "UpdateEventSeat")]
        void Update(EventSeatDTO obj);

        [OperationContract]
        EventSeatDTO GetEventSeatDTOById(int id);

        [OperationContract]
        [FaultContract(typeof(CanNotDeleteEventSeatException))]
        void DeleteEventSeat(int id);

        [OperationContract]
        EventInfoDTO GetEventInfo(int eventId);

        [OperationContract]
        IEnumerable<EventAreaDTO> GetAllEventAreasDTOByEventId(int eventId);

        [OperationContract]
        EventAreaDTO GetEventAreaDTOById(int id);

        [OperationContract]
        IEnumerable<EventSeatDTO> GetAllEventSeatsDTOByEventAreaId(int areaId);

        [OperationContract]
        IEnumerable<EventSeatDTO> GetAllEventSeatDTOByEventId(int eventId);

        [OperationContract]
        bool IsEventValid(int eventDTOId, DateTime dateTimeStart, DateTime dateTimeFinish, int layoutId);
    }
}
