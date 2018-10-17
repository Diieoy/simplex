using System;
using System.Collections.Generic;
using WcfServiceLibrary.DTO;
using WcfServiceLibrary.ServicesInterfaces;
using System.Security.Permissions;
using System.ServiceModel;
using WcfServiceLibrary.CustomExceptions;

namespace WcfServiceLibrary.Services
{
    public class AreaService : IAreaService
    {
        private BLLStandard.ServicesInterfaces.IAreaService areaService;

        public AreaService(BLLStandard.ServicesInterfaces.IAreaService areaService)
        {
            this.areaService = areaService;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public bool IsDescriptionUniqueByLayoutId(AreaDTO obj)
        {
            return areaService.IsDescriptionUniqueByLayoutId(FromAreaDTOToBLLStandardAreaDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(IEnumerable<AreaDTO> areaDTOs)
        {            
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(AreaDTO obj)
        {
            if (!IsDescriptionUniqueByLayoutId(obj))
            {
                throw new FaultException<NotUniqueDescriptionException>(new NotUniqueDescriptionException("description must be unique"), new FaultReason("description must be unique"));
            }

            areaService.Create(FromAreaDTOToBLLStandardAreaDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Delete(int id)
        {
            areaService.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public IEnumerable<AreaDTO> GetAll()
        {
            var bllAreaDTOs = areaService.GetAll();
            List<AreaDTO> list = new List<AreaDTO>();

            foreach (var item in bllAreaDTOs)
            {
                list.Add(FromBLLStandardAreaDTOToAreaDTO(item));
            }

            return list;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public AreaDTO GetById(int id)
        {
            var bllAreaDTO = areaService.GetById(id);

            return FromBLLStandardAreaDTOToAreaDTO(bllAreaDTO);
        }        

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Update(AreaDTO obj)
        {
            if (!IsDescriptionUniqueByLayoutId(obj))
            {
                throw new FaultException<NotUniqueDescriptionException>(new NotUniqueDescriptionException("description must be unique"), new FaultReason("description must be unique"));
            }

            areaService.Update(FromAreaDTOToBLLStandardAreaDTO(obj));
        }

        private BLLStandard.DTO.AreaDTO FromAreaDTOToBLLStandardAreaDTO(AreaDTO areaDTO)
        {
            return new BLLStandard.DTO.AreaDTO
            {
                Id = areaDTO.Id,
                LayoutId = areaDTO.LayoutId,
                Description = areaDTO.Description,
                CoordX = areaDTO.CoordX,
                CoordY = areaDTO.CoordY
            };
        }

        private AreaDTO FromBLLStandardAreaDTOToAreaDTO(BLLStandard.DTO.AreaDTO bllStandardAreaDTO)
        {
            return new AreaDTO
            {
                Id = bllStandardAreaDTO.Id,
                LayoutId = bllStandardAreaDTO.LayoutId,
                Description = bllStandardAreaDTO.Description,
                CoordX = bllStandardAreaDTO.CoordX,
                CoordY = bllStandardAreaDTO.CoordY
            };
        }
    }
}
