using System;
using System.Collections.Generic;
using WcfServiceLibrary.DTO;
using WcfServiceLibrary.ServicesInterfaces;

namespace WcfServiceLibrary.Services
{
    public class PublicService : IPublicService
    {
        private BLLStandard.ServicesInterfaces.IEventService eventService;

        public PublicService(BLLStandard.ServicesInterfaces.IEventService eventService)
        {
            this.eventService = eventService;
        }

        public void Create(EventSeatDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Create(EventDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEventSeat(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventDTO> GetAll()
        {
            var bllEventDTOs = eventService.GetAll();
            List<EventDTO> list = new List<EventDTO>();

            foreach (var item in bllEventDTOs)
            {
                list.Add(FromBLLStandardEventDTOToEventDTO(item));
            }

            return list;
        }

        public IEnumerable<EventAreaDTO> GetAllEventAreasDTOByEventId(int eventId)
        {
            var bllEventAreaDTOs = eventService.GetAllEventAreasDTOByEventId(eventId);
            List<EventAreaDTO> list = new List<EventAreaDTO>();

            foreach (var item in bllEventAreaDTOs)
            {
                list.Add(FromBLLStandardEventAreaDTOToEventAreaDTO(item));
            }

            return list;
        }

        public IEnumerable<EventSeatDTO> GetAllEventSeatDTOByEventId(int eventId)
        {
            var bllStandardEventSeatDTOs = eventService.GetAllEventSeatDTOByEventId(eventId);

            List<EventSeatDTO> list = new List<EventSeatDTO>();

            foreach (var item in bllStandardEventSeatDTOs)
            {
                list.Add(FromBLLStandardEventSeatDTOToEventSeatDTO(item));
            }

            return list;
        }

        public EventDTO GetById(int id)
        {
            var bllEventDTO = eventService.GetById(id);

            return FromBLLStandardEventDTOToEventDTO(bllEventDTO);
        }

        public EventInfoDTO GetEventInfo(int eventId)
        {
            return FromBLLStandardEventInfoDTOToEventInfoDTO(eventService.GetEventInfo(eventId));
        }    

        public void Update(EventAreaDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Update(EventDTO obj)
        {
            throw new NotImplementedException();
        }        

        private EventDTO FromBLLStandardEventDTOToEventDTO(BLLStandard.DTO.EventDTO bllStandardEventDTO)
        {
            return new EventDTO()
            {
                Id = bllStandardEventDTO.Id,
                Name = bllStandardEventDTO.Name,
                Description = bllStandardEventDTO.Description,
                DateTimeStart = bllStandardEventDTO.DateTimeStart,
                DateTimeFinish = bllStandardEventDTO.DateTimeFinish,
                ImageUrl = bllStandardEventDTO.ImageUrl,
                LayoutId = bllStandardEventDTO.LayoutId
            };
        }        

        private EventAreaDTO FromBLLStandardEventAreaDTOToEventAreaDTO(BLLStandard.DTO.EventAreaDTO bllStandardEventAreaDTO)
        {
            return new EventAreaDTO()
            {
                Id = bllStandardEventAreaDTO.Id,
                EventId = bllStandardEventAreaDTO.EventId,
                LayoutId = bllStandardEventAreaDTO.LayoutId,
                Description = bllStandardEventAreaDTO.Description,
                CoordX = bllStandardEventAreaDTO.CoordX,
                CoordY = bllStandardEventAreaDTO.CoordY,
                Price = bllStandardEventAreaDTO.Price
            };
        }        

        private EventSeatDTO FromBLLStandardEventSeatDTOToEventSeatDTO(BLLStandard.DTO.EventSeatDTO bllStandardEventSeatDTO)
        {
            return new EventSeatDTO()
            {
                Id = bllStandardEventSeatDTO.Id,
                EventAreaId = bllStandardEventSeatDTO.EventAreaId,
                Row = bllStandardEventSeatDTO.Row,
                Number = bllStandardEventSeatDTO.Number,
                State = bllStandardEventSeatDTO.State
            };
        }

        private EventInfoDTO FromBLLStandardEventInfoDTOToEventInfoDTO(BLLStandard.DTO.EventInfoDTO bllStandardEventInfoDTO)
        {
            return new EventInfoDTO
            {
                VenueName = bllStandardEventInfoDTO.VenueName,
                VenueAddress = bllStandardEventInfoDTO.VenueAddress,
                VenueDescription = bllStandardEventInfoDTO.VenueDescription,
                VenuePhone = bllStandardEventInfoDTO.VenuePhone,
                LayoutName = bllStandardEventInfoDTO.LayoutName,
                LayoutDescription = bllStandardEventInfoDTO.LayoutDescription
            };
        }
    }
}
