using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface ILayoutService : IService<LayoutDTO>
    {
        [OperationContract(Name = "CreateLayouts")]
        void Create(IEnumerable<LayoutDTO> layoutDTOs);

        [OperationContract]
        LayoutDTO GetByName(string name);

        [OperationContract]
        bool IsLayoutNameUniqueByVenueId(LayoutDTO obj);
    }
}
