using BLLStandard.DTO;
using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface ILayoutService : IService<LayoutDTO>
    {
        void Create(IEnumerable<LayoutDTO> layoutDTOs);
        LayoutDTO GetByName(string name);
        bool IsLayoutNameUniqueByVenueId(LayoutDTO obj);
    }
}
