using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.CustomExceptions;
using WcfServiceLibrary.DTO;
using WcfServiceLibrary.ServicesInterfaces;

namespace WcfServiceLibrary.Services
{
    public class LayoutService : ILayoutService
    {
        private BLLStandard.ServicesInterfaces.ILayoutService layoutService;

        public LayoutService(BLLStandard.ServicesInterfaces.ILayoutService layoutService)
        {
            this.layoutService = layoutService;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public bool IsLayoutNameUniqueByVenueId(LayoutDTO obj)
        {
            return layoutService.IsLayoutNameUniqueByVenueId(FromLayoutDTOToBLLStandardLayoutDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(IEnumerable<LayoutDTO> layoutDTOs)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(LayoutDTO obj)
        {
            if (!IsLayoutNameUniqueByVenueId(obj))
            {
                throw new FaultException<NotUniqueNameException>(new NotUniqueNameException("name must be unique"), new FaultReason("name must be unique"));
            }

            layoutService.Create(FromLayoutDTOToBLLStandardLayoutDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Delete(int id)
        {
            layoutService.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public IEnumerable<LayoutDTO> GetAll()
        {
            var bllLayoutDTOs = layoutService.GetAll();
            List<LayoutDTO> list = new List<LayoutDTO>();

            foreach (var item in bllLayoutDTOs)
            {
                list.Add(FromBLLStandardLayoutDTOToLayoutDTO(item));
            }

            return list;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        public LayoutDTO GetById(int id)
        {
            var bllLayoutDTO = layoutService.GetById(id);

            return FromBLLStandardLayoutDTOToLayoutDTO(bllLayoutDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public LayoutDTO GetByName(string name)
        {
            var bllLayoutDTO = layoutService.GetByName(name);

            return FromBLLStandardLayoutDTOToLayoutDTO(bllLayoutDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Update(LayoutDTO obj)
        {
            if (!IsLayoutNameUniqueByVenueId(obj))
            {
                throw new FaultException<NotUniqueNameException>(new NotUniqueNameException("name must be unique"), new FaultReason("name must be unique"));
            }

            layoutService.Update(FromLayoutDTOToBLLStandardLayoutDTO(obj));
        }

        private BLLStandard.DTO.LayoutDTO FromLayoutDTOToBLLStandardLayoutDTO(LayoutDTO layoutDTO)
        {
            return new BLLStandard.DTO.LayoutDTO
            {
                Id = layoutDTO.Id,
                Name = layoutDTO.Name,
                VenueId = layoutDTO.VenueId,
                Description = layoutDTO.Description
            };
        }

        private LayoutDTO FromBLLStandardLayoutDTOToLayoutDTO(BLLStandard.DTO.LayoutDTO bllStandardLayoutDTO)
        {
            return new LayoutDTO
            {
                Id = bllStandardLayoutDTO.Id,
                Name = bllStandardLayoutDTO.Name,
                VenueId = bllStandardLayoutDTO.VenueId,
                Description = bllStandardLayoutDTO.Description
            };
        }        
    }
}
