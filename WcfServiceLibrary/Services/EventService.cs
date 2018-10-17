using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel;
using WcfServiceLibrary.CustomExceptions;
using WcfServiceLibrary.DTO;
using System.Linq;
using WcfServiceLibrary.ServicesInterfaces;

namespace WcfServiceLibrary.Services
{
    public class EventService : IEventService
    {
        private BLLStandard.ServicesInterfaces.IEventService eventService;

        public EventService(BLLStandard.ServicesInterfaces.IEventService eventService)
        {
            this.eventService = eventService;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public bool IsEventValid(int eventDTOId, DateTime dateTimeStart, DateTime dateTimeFinish, int layoutId)
        {
            return eventService.IsEventValid(eventDTOId, dateTimeStart, dateTimeFinish, layoutId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(EventSeatDTO obj)
        {
            var list = GetAllEventSeatsDTOByEventAreaId(obj.EventAreaId).ToList();

            if (list.Exists(x => x.Row == obj.Row))
            {
                if (list.FindAll(x => x.Row == obj.Row).Exists(x => x.Number == obj.Number))
                {
                    throw new FaultException<CanNotCreateEventSeatException>(new CanNotCreateEventSeatException("can't create seat"), new FaultReason("can't create seat"));
                }
            }

            eventService.Create(FromEventSeatDTOToBLLStandardEventSeatDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Create(EventDTO obj)
        {
            if (!IsEventValid(obj.Id, obj.DateTimeStart, obj.DateTimeFinish, obj.LayoutId))
            {
                throw new FaultException<InvalidEventException>(new InvalidEventException("invalid event"), new FaultReason("invalid event"));
            }

            eventService.Create(FromEventDTOToBLLStandardEventDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Delete(int id)
        {
            var eventAreasDTO = GetAllEventAreasDTOByEventId(id);

            foreach (var eventAreaDTO in eventAreasDTO)
            {
                var seatsDTO = GetAllEventSeatsDTOByEventAreaId(eventAreaDTO.Id);

                foreach (var s in seatsDTO)
                {
                    if (s.State != 0)
                    {
                        throw new FaultException<CanNotDeleteEventException>(new CanNotDeleteEventException("can't delete event"), new FaultReason("can't delete event"));
                    }
                }
            }

            eventService.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void DeleteEventSeat(int id)
        {
            if (eventService.GetEventSeatDTOById(id).State != 0)
            {
                throw new FaultException<CanNotDeleteEventSeatException>(new CanNotDeleteEventSeatException("can't delete event"), new FaultReason("can't delete event"));
            }

            eventService.DeleteEventSeat(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
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

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
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

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public IEnumerable<EventSeatDTO> GetAllEventSeatsDTOByEventAreaId(int areaId)
        {
            var bllEventSeatDTOs = eventService.GetAllEventSeatsDTOByEventAreaId(areaId);
            List<EventSeatDTO> list = new List<EventSeatDTO>();

            foreach (var item in bllEventSeatDTOs)
            {
                list.Add(FromBLLStandardEventSeatDTOToEventSeatDTO(item));
            }

            return list;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        public EventDTO GetById(int id)
        {
            var bllEventDTO = eventService.GetById(id);

            if (bllEventDTO == null)
            {
                return null;
            }

            return FromBLLStandardEventDTOToEventDTO(bllEventDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        public EventAreaDTO GetEventAreaDTOById(int id)
        {
            var bllEventAreaDTO = eventService.GetEventAreaDTOById(id);

            if (bllEventAreaDTO == null)
            {
                return null;
            }

            return FromBLLStandardEventAreaDTOToEventAreaDTO(bllEventAreaDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        public EventSeatDTO GetEventSeatDTOById(int id)
        {
            var bllEventSeatDTO = eventService.GetEventSeatDTOById(id);

            return FromBLLStandardEventSeatDTOToEventSeatDTO(bllEventSeatDTO);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
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

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Update(EventAreaDTO obj)
        {
            eventService.Update(FromEventAreaDTOToBLLStandardEventAreaDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public void Update(EventDTO obj)
        {
            if (!IsEventValid(obj.Id, obj.DateTimeStart, obj.DateTimeFinish, obj.LayoutId))
            {
                throw new FaultException<InvalidEventException>(new InvalidEventException("invalid event"), new FaultReason("invalid event"));
            }

            eventService.Update(FromEventDTOToBLLStandardEventDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        public void Update(EventSeatDTO obj)
        {
            eventService.Update(FromEventSeatDTOToBLLStandardEventSeatDTO(obj));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "event_manager")]
        public EventInfoDTO GetEventInfo(int eventId)
        {
            return FromBLLStandardEventInfoDTOToEventInfoDTO(eventService.GetEventInfo(eventId));         
        }

        private BLLStandard.DTO.EventDTO FromEventDTOToBLLStandardEventDTO(EventDTO eventDTO)
        {
            return new BLLStandard.DTO.EventDTO()
            {
                Id = eventDTO.Id,
                Name = eventDTO.Name,
                Description = eventDTO.Description,
                DateTimeStart = eventDTO.DateTimeStart,
                DateTimeFinish = eventDTO.DateTimeFinish,
                ImageUrl = eventDTO.ImageUrl,
                LayoutId = eventDTO.LayoutId
            };
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

        private BLLStandard.DTO.EventAreaDTO FromEventAreaDTOToBLLStandardEventAreaDTO(EventAreaDTO eventAreaDTO)
        {
            return new BLLStandard.DTO.EventAreaDTO()
            {
                Id = eventAreaDTO.Id,
                EventId = eventAreaDTO.EventId,
                LayoutId = eventAreaDTO.LayoutId,
                Description = eventAreaDTO.Description,
                CoordX = eventAreaDTO.CoordX,
                CoordY = eventAreaDTO.CoordY,
                Price = eventAreaDTO.Price
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

        private BLLStandard.DTO.EventSeatDTO FromEventSeatDTOToBLLStandardEventSeatDTO(EventSeatDTO eventSeatDTO)
        {
            return new BLLStandard.DTO.EventSeatDTO()
            {
                Id = eventSeatDTO.Id,
                EventAreaId = eventSeatDTO.EventAreaId,
                Row = eventSeatDTO.Row,
                Number = eventSeatDTO.Number,
                State = eventSeatDTO.State
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

        private BLLStandard.DTO.EventInfoDTO FromEventInfoDTOToBLLStandardEventDTO(EventInfoDTO eventInfoDTO)
        {
            return new BLLStandard.DTO.EventInfoDTO
            {
                VenueName = eventInfoDTO.VenueName,
                VenueAddress = eventInfoDTO.VenueAddress,
                VenueDescription = eventInfoDTO.VenueDescription,
                VenuePhone = eventInfoDTO.VenuePhone,
                LayoutName = eventInfoDTO.LayoutName,
                LayoutDescription = eventInfoDTO.LayoutDescription
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
