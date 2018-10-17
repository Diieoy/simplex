using BLLStandard.DTO;
using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface ISeatServicece : IService<SeatDTO>
    {
        void Create(IEnumerable<SeatDTO> seatDTOs);
    }
}
