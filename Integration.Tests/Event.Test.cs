using BLLStandard.DTO;
using BLLStandard.Exceptions;
using BLLStandard.Services;
using DALStandard.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace Integration.Tests
{
    [TestFixture]
    public class Event
    {
        private EventService service;

        [SetUp]
        public void Init()
        {
            service = new EventService(new EventRepository(), new EventAreaRepository(), new EventSeatRepository());
        }

        [Test]
        public void CreateEvent_Correct_DateTime()
        {
            //Arrange
            EventDTO eventDTO = new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019 10:00"),
                DateTimeFinish = DateTime.Parse("01-01-2019 11:00"),
                ImageUrl = "url",
                LayoutId = 1
            };

            //Act
            service.Create(eventDTO);
            var res = service.GetAllEventsDTOByName(eventDTO.Name).ToList()[0];

            //Assert
            Assert.AreNotEqual(res, null);

            service.Delete(res.Id);
        }

        [Test]
        public void CreateEvent_Incorrect_DateTime()
        {
            //Arrange
            EventDTO eventDTO = new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2010"),
                DateTimeFinish = DateTime.Parse("01-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            };

            //Assert
            Assert.Throws<InvalidEventException>(() => service.Create(eventDTO));
        }

        [Test]
        public void Try_To_Create_Event_To_Layout_Without_Seats()
        {
            //Arrange
            EventDTO eventDTO = new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 3
            };

            //Assert
            Assert.Throws<InvalidEventException>(() => service.Create(eventDTO));
        }

        [Test]
        public void Update()
        {
            //Arrange
            service.Create(new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            });

            var e = service.GetAllEventsDTOByName("first").ToList()[0];

            EventDTO eventDTO = new EventDTO
            {
                Id = e.Id,
                Name = "update",
                Description = "up",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 2
            };

            //Act
            service.Update(eventDTO);
            var eventFromDb = service.GetAllEventsDTOByName("update").ToList()[0];

            //Assert
            Assert.AreNotEqual(eventFromDb, null);

            service.Delete(eventFromDb.Id);
        }

        [Test]
        public void Update_EventAreaDTO()
        {
            //Arrange
            service.Create(new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 2
            });

            //Act
            var eventAreaDTO = service.GetAllEventAreaDTOs().First();
            service.Update(new EventAreaDTO
            {
                Id = eventAreaDTO.Id,
                EventId = eventAreaDTO.EventId,
                LayoutId = eventAreaDTO.LayoutId,
                Description = "upd",
                CoordX = 10,
                CoordY = 20,
                Price = 100
            });           

            //Assert
            Assert.AreEqual(service.GetEventAreaDTOById(eventAreaDTO.Id).Description, "upd");
            Assert.AreEqual(service.GetEventAreaDTOById(eventAreaDTO.Id).CoordX, 10);
            Assert.AreEqual(service.GetEventAreaDTOById(eventAreaDTO.Id).CoordY, 20);
            Assert.AreEqual(service.GetEventAreaDTOById(eventAreaDTO.Id).Price, 100);

            service.Delete(service.GetAllEventsDTOByName("first").ToList()[0].Id);
        }

        [Test]
        public void Update_EventSeatDTO()
        {
            //Arrange
            service.Create(new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 2
            });

            //Act
            var eventSeatDTO = service.GetAllEventSeatDTOByEventId(service.GetAllEventsDTOByName("first").First().Id).First();
            service.Update(new EventSeatDTO
            {
                Id = eventSeatDTO.Id,
                EventAreaId = eventSeatDTO.EventAreaId,
                Row = 2,
                Number = 2                
            });

            //Assert
            Assert.AreEqual(service.GetEventSeatDTOById(eventSeatDTO.Id).Row, 2);
            Assert.AreEqual(service.GetEventSeatDTOById(eventSeatDTO.Id).Number, 2);

            service.Delete(service.GetAllEventsDTOByName("first").First().Id);
        }

        [Test]
        public void Try_To_Update_Event_With_Incorrect_DateTime()
        {
            //Arrange
            service.Create(new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            });

            var e = service.GetAllEventsDTOByName("first").ToList()[0];

            EventDTO incorrectEventDTO = new EventDTO
            {
                Id = e.Id,
                Name = "second",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2010"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            };

            //Assert
            Assert.Throws<InvalidEventException>(() => service.Update(incorrectEventDTO));

            service.Delete(e.Id);
        }

        [Test]
        public void Try_To_Update_Event_With_Any_Seats()
        {
            //Arrange
            service.Create(new EventDTO {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            });

            var ev = service.GetAllEventsDTOByName("first").ToList()[0];

            EventDTO incorrectEventDTO = new EventDTO {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 3
            };

            //Assert
            Assert.Throws<InvalidEventException>(() => service.Update(incorrectEventDTO));

            service.Delete(ev.Id);
        }

        [Test]
        public void Delete()
        {
            //Arrange
            service.Create(new EventDTO {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            });

            var e = service.GetAllEventsDTOByName("first").ToList()[0];

            //Act
            service.Delete(e.Id);

            //Assert
            Assert.AreEqual(service.GetById(e.Id), null);
        }   
        
        [Test]
        public void Try_To_Delete_Event_With_A_Bought_Seat()
        {
            //Arrange
            EventDTO eventDTO = new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019 10:00"),
                DateTimeFinish = DateTime.Parse("01-01-2019 11:00"),
                ImageUrl = "url",
                LayoutId = 2
            };

            //Act
            service.Create(eventDTO);
            var e = service.GetAllEventsDTOByName("first").First();
            var eventArea = service.GetAllEventAreaDTOs().First();
            var seat = service.GetAllEventSeatsDTOByEventAreaId(eventArea.Id).First();
            seat.State = 1;
            service.Update(seat);

            //Assert
            Assert.Throws<CanNotDeleteEventException>(() => service.Delete(e.Id));

            seat.State = 0;
            service.Update(seat);
            service.Delete(e.Id);
        }

        [Test]
        public void Try_To_Delete_EventSeat_With_A_Bought_State()
        {
            //Arrange
            EventDTO eventDTO = new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019 10:00"),
                DateTimeFinish = DateTime.Parse("01-01-2019 11:00"),
                ImageUrl = "url",
                LayoutId = 2
            };

            //Act
            service.Create(eventDTO);
            var e = service.GetAllEventsDTOByName("first").First();
            var eventArea = service.GetAllEventAreaDTOs().First();
            var seat = service.GetAllEventSeatsDTOByEventAreaId(eventArea.Id).First();
            seat.State = 1;
            service.Update(seat);

            //Assert
            Assert.Throws<CanNotDeleteEventSeatException>(() => service.DeleteEventSeat(seat.Id));

            seat.State = 0;
            service.Update(seat);
            service.Delete(e.Id);
        }

        [Test]
        public void GetAll()
        {
            //Arrange
            var eventDTO = new EventDTO()
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            };

            //Act
            service.Create(eventDTO);

            //Assert
            Assert.AreEqual(service.GetAll().ToList().Count, 2);

            service.Delete(service.GetAllEventsDTOByName("first").ToList()[0].Id);
        }

        [Test]
        public void GetAllEventAreasDTOs()
        {
            //Arrange
            var eventDTO = new EventDTO()
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            };

            //Act
            service.Create(eventDTO);

            //Assert
            Assert.AreEqual(service.GetAllEventAreaDTOs().Count(), 2);

            service.Delete(service.GetAllEventsDTOByName("first").First().Id);
        }

        [Test]
        public void GetAllEventSeatDTOByEventId()
        {
            //Arrange
            var eventDTO = new EventDTO()
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 2
            };

            //Act
            service.Create(eventDTO);

            //Assert
            Assert.AreEqual(service.GetAllEventSeatDTOByEventId(service.GetAllEventsDTOByName("first").First().Id).Count(), 16);

            service.Delete(service.GetAllEventsDTOByName("first").First().Id);
        }

        [Test]
        public void GetAllEventAreasDTOByEventId()
        {
            //Arrange
            var eventDTO = new EventDTO()
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 1
            };

            //Act
            service.Create(eventDTO);

            //Assert
            Assert.AreEqual(service.GetAllEventAreasDTOByEventId(service.GetAllEventsDTOByName("first").First().Id).Count(), 2);

            service.Delete(service.GetAllEventsDTOByName("first").First().Id);
        }

        [Test]
        public void GetAllEventSeatsDTOByEventAreaId()
        {
            //Arrange
            var eventDTO = new EventDTO()
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019"),
                DateTimeFinish = DateTime.Parse("02-01-2019"),
                ImageUrl = "url",
                LayoutId = 2
            };

            //Act
            service.Create(eventDTO);

            //Assert
            Assert.AreEqual(service.GetAllEventSeatsDTOByEventAreaId(service.GetAllEventAreaDTOs().First().Id).Count(), 16);

            service.Delete(service.GetAllEventsDTOByName("first").First().Id);
        }

        [Test]
        public void GetById()
        {
            //Assert
            Assert.AreEqual(service.GetAllEventsDTOByName("ForTestEvent").ToList()[0].Id, 1);
        }

        [Test]
        public void GetByName()
        {
            //Assert
            Assert.AreEqual(service.GetById(1).Name, "ForTestEvent");
        }

        [Test]
        public void Create_Valid_EventSeat()
        {
            //Arrange
            EventDTO eventDTO = new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019 10:00"),
                DateTimeFinish = DateTime.Parse("01-01-2019 11:00"),
                ImageUrl = "url",
                LayoutId = 2
            };

            //Act
            service.Create(eventDTO);
            var evnt = service.GetAllEventsDTOByName("first").First();
            var eventArea = service.GetAllEventAreaDTOs().First();
            service.Create(new EventSeatDTO { EventAreaId = eventArea.Id, Row = 5, Number = 1 });

            //Assert
            Assert.AreEqual(service.GetAllEventSeatsDTOByEventAreaId(eventArea.Id).Count(), 17);

            service.Delete(evnt.Id);
        }

        [Test]
        public void Try_To_Create_Invalid_EventSeat()
        {
            //Arrange
            EventDTO eventDTO = new EventDTO
            {
                Name = "first",
                Description = "p",
                DateTimeStart = DateTime.Parse("01-01-2019 10:00"),
                DateTimeFinish = DateTime.Parse("01-01-2019 11:00"),
                ImageUrl = "url",
                LayoutId = 2
            };

            //Act
            service.Create(eventDTO);
            var e = service.GetAllEventsDTOByName("first").First();
            var eventArea = service.GetAllEventAreaDTOs().First();           

            //Assert
            Assert.Throws<CanNotCreateEventSeatException>(() => service.Create(new EventSeatDTO { EventAreaId = eventArea.Id, Row = 1, Number = 1 }));

            service.Delete(e.Id);
        }
    }
}
