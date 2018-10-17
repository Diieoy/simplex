using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.DTO;
using WcfServiceLibrary.ServicesInterfaces;

namespace WcfServiceLibrary.Services
{
    public class SeatService : ISeatServicece
    {
        private BLLStandard.ServicesInterfaces.ISeatServicece seatServicece;

        public SeatService(BLLStandard.ServicesInterfaces.ISeatServicece seatServicece)
        {
            this.seatServicece = seatServicece;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(IEnumerable<SeatDTO> seatDTOs)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(SeatDTO obj)
        {
            seatServicece.Create(FromSeatDTOToBLLStandardSeatDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Delete(int id)
        {
            seatServicece.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public IEnumerable<SeatDTO> GetAll()
        {
            var bllSeatDTOs = seatServicece.GetAll();
            List<SeatDTO> list = new List<SeatDTO>();

            foreach (var item in bllSeatDTOs)
            {
                list.Add(FromBLLStandardSeatDTOToSeatDTO(item));
            }

            return list;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public SeatDTO GetById(int id)
        {
            var bllSeatDTO = seatServicece.GetById(id);

            return FromBLLStandardSeatDTOToSeatDTO(bllSeatDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "venue_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Update(SeatDTO obj)
        {
            seatServicece.Update(FromSeatDTOToBLLStandardSeatDTO(obj));
        }

        private BLLStandard.DTO.SeatDTO FromSeatDTOToBLLStandardSeatDTO(SeatDTO seatDTO)
        {
            return new BLLStandard.DTO.SeatDTO
            {
                Id = seatDTO.Id,
                AreaId = seatDTO.AreaId,
                Row = seatDTO.Row,
                Number = seatDTO.Number
            };
        }

        private SeatDTO FromBLLStandardSeatDTOToSeatDTO(BLLStandard.DTO.SeatDTO bllStandardSeatDTO)
        {
            return new SeatDTO
            {
                Id = bllStandardSeatDTO.Id,
                AreaId = bllStandardSeatDTO.AreaId,
                Row = bllStandardSeatDTO.Row,
                Number = bllStandardSeatDTO.Number
            };
        }
    }
}
