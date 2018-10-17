using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface IAreaService : IService<AreaDTO>
    {
        [OperationContract(Name = "CreateAreas")]
        void Create(IEnumerable<AreaDTO> areaDTOs);

        [OperationContract]
        bool IsDescriptionUniqueByLayoutId(AreaDTO obj);
    }
}
