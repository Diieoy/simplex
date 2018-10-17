using WcfServiceLibrary.DTO;
using WcfServiceLibrary.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.ServiceModel;
using WcfServiceLibrary.CustomExceptions;

namespace WcfServiceLibrary.Services
{
    public class VenueService : IVenueService
    {
        private BLLStandard.ServicesInterfaces.IVenueService venueService;

        public VenueService(BLLStandard.ServicesInterfaces.IVenueService venueService)
        {
            this.venueService = venueService;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public bool IsNameUnique(VenueDTO obj)
        {
            return venueService.IsNameUnique(FromVenueDTOToBLLStandardVenueDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(IEnumerable<VenueDTO> venueDTOs)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(VenueDTO obj)
        {
            if (!IsNameUnique(obj))
            {
                throw new FaultException<NotUniqueNameException>(new NotUniqueNameException("name must be unique"), new FaultReason("name must be unique"));
            }

            venueService.Create(FromVenueDTOToBLLStandardVenueDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Delete(int id)
        {
            venueService.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public IEnumerable<VenueDTO> GetAll()
        {
            var bllVenueDTOs = venueService.GetAll();
            List<VenueDTO> list = new List<VenueDTO>();

            foreach (var item in bllVenueDTOs)
            {
                list.Add(FromBLLStandardVenueDTOToVenueDTO(item));
            }

            return list;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        public VenueDTO GetById(int id)
        {
            var bllVenueDTO = venueService.GetById(id);

            return FromBLLStandardVenueDTOToVenueDTO(bllVenueDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public VenueDTO GetByName(string name)
        {
            var bllVenueDTO = venueService.GetByName(name);

            return FromBLLStandardVenueDTOToVenueDTO(bllVenueDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Update(VenueDTO obj)
        {
            if (!IsNameUnique(obj))
            {
                throw new FaultException<NotUniqueNameException>(new NotUniqueNameException("name must be unique"), new FaultReason("name must be unique"));
            }

            venueService.Update(FromVenueDTOToBLLStandardVenueDTO(obj));
        }

        private BLLStandard.DTO.VenueDTO FromVenueDTOToBLLStandardVenueDTO(VenueDTO venueDTO)
        {
            return new BLLStandard.DTO.VenueDTO
            {
                Id = venueDTO.Id,
                Name = venueDTO.Name,
                Description = venueDTO.Description,
                Address = venueDTO.Address,
                Phone = venueDTO.Phone
            };
        }

        private VenueDTO FromBLLStandardVenueDTOToVenueDTO(BLLStandard.DTO.VenueDTO bllStandardVenueDTO)
        {
            return new VenueDTO
            {
                Id = bllStandardVenueDTO.Id,
                Name = bllStandardVenueDTO.Name,
                Description = bllStandardVenueDTO.Description,
                Address = bllStandardVenueDTO.Address,
                Phone = bllStandardVenueDTO.Phone
            };
        }
    }
}
