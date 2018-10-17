using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface IPublicService : IService<EventDTO>
    {
        [OperationContract]
        EventInfoDTO GetEventInfo(int eventId);

        [OperationContract]
        IEnumerable<EventAreaDTO> GetAllEventAreasDTOByEventId(int eventId);

        [OperationContract]
        IEnumerable<EventSeatDTO> GetAllEventSeatDTOByEventId(int eventId);
    }
}
