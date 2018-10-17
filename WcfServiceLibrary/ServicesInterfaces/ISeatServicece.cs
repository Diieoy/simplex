using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface ISeatServicece : IService<SeatDTO>
    {
        [OperationContract(Name = "CreateSeats")]
        void Create(IEnumerable<SeatDTO> seatDTOs);
    }
}
