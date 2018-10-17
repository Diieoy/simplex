using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface IVenueService : IService<VenueDTO>
    {
        [OperationContract(Name = "CreateVenues")]
        void Create(IEnumerable<VenueDTO> venueDTOs);

        [OperationContract]
        VenueDTO GetByName(string name);

        [OperationContract]
        bool IsNameUnique(VenueDTO obj);
    }
}
