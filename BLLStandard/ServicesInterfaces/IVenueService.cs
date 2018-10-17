using BLLStandard.DTO;
using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface IVenueService : IService<VenueDTO>
    {
        void Create(IEnumerable<VenueDTO> venueDTOs);
        VenueDTO GetByName(string name);
        bool IsNameUnique(VenueDTO obj);
    }
}
