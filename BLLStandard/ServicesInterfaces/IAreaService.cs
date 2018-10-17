using BLLStandard.DTO;
using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface IAreaService : IService<AreaDTO>
    {
        void Create(IEnumerable<AreaDTO> areaDTOs);
        bool IsDescriptionUniqueByLayoutId(AreaDTO obj);
    }
}
