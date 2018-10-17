using BLLStandard.DTO;
using BLLStandard.Exceptions;
using BLLStandard.ServicesInterfaces;
using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLLStandard.Services
{
    public class EventService : IEventService
    {
        private IEventRepository eventRepository;
        private IEventAreaRepository eventAreaRepository;
        private IEventSeatRepository eventSeatRepository;

        public EventService(IEventRepository eventRepository, IEventAreaRepository eventAreaRepository, IEventSeatRepository eventSeatRepository)
        {
            this.eventRepository = eventRepository;
            this.eventAreaRepository = eventAreaRepository;
            this.eventSeatRepository = eventSeatRepository;
        }

        public void Create(EventDTO obj)
        {
            if (!IsEventValid(obj.Id, obj.DateTimeStart, obj.DateTimeFinish, obj.LayoutId))
            {
                throw new InvalidEventException();
            }

            eventRepository.Create(EventDTOToEvent(obj));
        }

        public void Create(EventSeatDTO obj)
        {
            var list = GetAllEventSeatsDTOByEventAreaId(obj.EventAreaId).ToList();

            if(list.Exists(x => x.Row == obj.Row))
            {
                if(list.FindAll(x => x.Row == obj.Row).Exists(x => x.Number == obj.Number))
                {
                    throw new CanNotCreateEventSeatException();
                }
            }

            eventSeatRepository.Create(EventSeatDTOToEventSeat(obj));
        }

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
                        throw new CanNotDeleteEventException();
                    }
                }
            }

            eventRepository.Delete(id);
        }

        public void DeleteEventSeat(int id)
        {
            if (eventSeatRepository.GetById(id).State != 0)
            {
                throw new CanNotDeleteEventSeatException();
            }            

            eventSeatRepository.Delete(id);
        }

        public IEnumerable<EventDTO> GetAll()
        {
            var list = new List<EventDTO>();

            foreach (var item in eventRepository.GetAll())
            {
                list.Add(EventToEventDTO(item));
            }

            return list;
        }

        public IEnumerable<EventAreaDTO> GetAllEventAreaDTOs()
        {
            var list = new List<EventAreaDTO>();

            foreach (var item in eventAreaRepository.GetAll())
            {
                list.Add(EventAreaToEventAreaDTO(item));
            }

            return list;
        }
        
        public IEnumerable<EventSeatDTO> GetAllEventSeatDTOByEventId(int eventId)
        {
            List<EventSeatDTO> list = new List<EventSeatDTO>();

            foreach (var item in eventRepository.GetAllEventSeatByEventId(eventId))
            {
                list.Add(EventSeatToEventSeatDTO(item));
            }

            return list;
        }

        public EventDTO GetById(int id)
        {
            var evnt = eventRepository.GetById(id);

            if (evnt == null)
            {
                return null;
            }

            return EventToEventDTO(evnt);
        }

        public EventAreaDTO GetEventAreaDTOById(int id)
        {
            var evnt = eventAreaRepository.GetById(id);

            if(evnt == null)
            {
                return null;
            }

            return EventAreaToEventAreaDTO(evnt);
        }

        public EventSeatDTO GetEventSeatDTOById(int id)
        {
            var evnt = eventSeatRepository.GetById(id);

            return EventSeatToEventSeatDTO(evnt);
        }

        public EventInfoDTO GetEventInfo(int eventId)
        {
            var list = eventRepository.GetEventInfo(eventId);

            return new EventInfoDTO { VenueName = list[0], VenueDescription = list[1], VenueAddress = list[2], VenuePhone = list[3], LayoutName = list[4], LayoutDescription = list[5] };
        }

        public void Update(EventDTO obj)
        {
            if (!IsEventValid(obj.Id, obj.DateTimeStart, obj.DateTimeFinish, obj.LayoutId))
            {
                throw new InvalidEventException();
            }

            eventRepository.Update(EventDTOToEvent(obj));
        }

        public void Update(EventAreaDTO obj)
        {
            eventAreaRepository.Update(EventAreaDTOToEventArea(obj));
        }

        public void Update(EventSeatDTO obj)
        {
            eventSeatRepository.Update(EventSeatDTOToEventSeat(obj));
        }               

        public IEnumerable<EventDTO> GetAllEventsDTOByName(string name)
        {
            var list = new List<EventDTO>();

            foreach (var item in eventRepository.GetAllEventsByName(name))
            {
                list.Add(EventToEventDTO(item));
            }

            return list;
        }

        public IEnumerable<EventAreaDTO> GetAllEventAreasDTOByEventId(int eventId)
        {           
            var areas = eventAreaRepository.GetAll().Where(x => x.EventId == eventId);

            List<EventAreaDTO> list = new List<EventAreaDTO>();

            foreach (var item in areas)
            {
                list.Add(EventAreaToEventAreaDTO(item));
            }

            return list;
        }

        public IEnumerable<EventSeatDTO> GetAllEventSeatsDTOByEventAreaId(int areaId)
        {
            var seats = eventSeatRepository.GetAll().Where(x => x.EventAreaId == areaId);

            List<EventSeatDTO> list = new List<EventSeatDTO>();

            foreach (var item in seats)
            {
                list.Add(EventSeatToEventSeatDTO(item));
            }

            return list;
        }

        public bool IsEventValid(int eventDTOId, DateTime dateTimeStart, DateTime dateTimeFinish, int layoutId)
        {
            if ((dateTimeStart >= dateTimeFinish) || (dateTimeStart <= DateTime.Now) || !eventRepository.AreThereAnySeats(layoutId))
            {
                return false;
            }

            foreach (var item in eventRepository.GetEventsByLayoutId(layoutId))
            {
                if (item.Id != eventDTOId)
                {
                    if ((dateTimeStart >= item.DateTimeStart && dateTimeStart <= item.DateTimeFinish) ||
                    (dateTimeFinish >= item.DateTimeStart && dateTimeFinish <= item.DateTimeFinish))
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        private EventDTO EventToEventDTO(Event item)
        {
            return new EventDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DateTimeStart = item.DateTimeStart,
                DateTimeFinish = item.DateTimeFinish,
                ImageUrl = item.ImageUrl,
                LayoutId = item.LayoutId
            };
        }

        private Event EventDTOToEvent(EventDTO item)
        {
            return new Event
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DateTimeStart = item.DateTimeStart,
                DateTimeFinish = item.DateTimeFinish,
                ImageUrl = item.ImageUrl,
                LayoutId = item.LayoutId
            };
        }

        private EventAreaDTO EventAreaToEventAreaDTO(EventArea item)
        {
            return new EventAreaDTO
            {
                Id = item.Id,
                EventId = item.EventId,
                LayoutId = item.LayoutId,
                Description = item.Description,
                CoordX = item.CoordX,
                CoordY = item.CoordY,
                Price = item.Price
            };
        }

        private EventArea EventAreaDTOToEventArea(EventAreaDTO item)
        {
            return new EventArea
            {
                Id = item.Id,
                EventId = item.EventId,
                LayoutId = item.LayoutId,
                Description = item.Description,
                CoordX = item.CoordX,
                CoordY = item.CoordY,
                Price = item.Price
            };
        }

        private EventSeatDTO EventSeatToEventSeatDTO(EventSeat item)
        {
            return new EventSeatDTO
            {
                Id = item.Id,
                EventAreaId = item.EventAreaId,
                Row = item.Row,
                Number = item.Number,
                State = item.State
            };
        }

        private EventSeat EventSeatDTOToEventSeat(EventSeatDTO item)
        {
            return new EventSeat
            {
                Id = item.Id,
                EventAreaId = item.EventAreaId,
                Row = item.Row,
                Number = item.Number,
                State = item.State
            };
        }
    }
}
