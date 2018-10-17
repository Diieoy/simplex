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
    public class Venue
    {
        private VenueService service;

        [SetUp]
        public void Init()
        {
            service = new VenueService(new VenueRepository());
        }

        [Test]
        public void Create_Correct_Venue()
        {
            //Arrange
            var venueDTO = new VenueDTO() { Name = "name", Description = "test", Address = "asd", Phone = "123" };

            //Act
            service.Create(venueDTO);
            var res = service.GetByName("name");

            //Assert
            Assert.AreEqual(venueDTO.Name, res.Name);

            service.Delete(res.Id);
        }

        [Test]
        public void Try_To_Create_Incorrect_Venue()
        {
            //Arrange
            var venueDTO = new VenueDTO() { Name = "name", Description = "test", Address = "asd", Phone = "123" };
            var incorrectVenueDTO = new VenueDTO() { Name = "name", Description = "test", Address = "asd", Phone = "123" };

            //Act
            service.Create(venueDTO);

            //Assert
            Assert.Throws<NotUniqueNameException>(() => service.Create(incorrectVenueDTO));

            service.Delete(service.GetByName("name").Id);
        }

        [Test]
        public void Delete()
        {
            //Arrange
            var venueDTO = new VenueDTO() { Name = "name", Description = "test", Address = "asd", Phone = "123" };
            service.Create(venueDTO);
            int id = service.GetByName("name").Id;

            //Act
            service.Delete(id);
            var res = service.GetByName("name");

            //Assert
            Assert.AreEqual(res, null);
        }

        [Test]
        public void Update()
        {
            //Arrange
            var venueDTO = new VenueDTO() { Name = "name", Description = "test", Address = "asd", Phone = "123" };
            var venueForUpdate = new VenueDTO() { Name = "upd", Description = "upd", Address = "upd", Phone = "15553" };
            service.Create(venueDTO);
            venueForUpdate.Id = service.GetByName("name").Id;

            //Act
            service.Update(venueForUpdate);

            //Assert
            Assert.AreEqual(venueForUpdate.Name, service.GetByName("upd").Name);

            service.Delete(service.GetByName("upd").Id);
        }

        [Test]
        public void GetAll()
        {
            //Arrange
            var venueDTO = new VenueDTO() { Name = "test", Description = "test", Address = "asd", Phone = "123" };
            service.Create(venueDTO);

            //Act
            var res = service.GetAll().ToList();

            //Assert
            Assert.AreEqual(res.Count, 2);

            service.Delete(service.GetByName("test").Id);
        }

        [Test]
        public void GetById()
        {
            //Assert
            Assert.AreEqual(service.GetById(1).Name, "Arena");
        }

        [Test]
        public void GetByName()
        {
            //Arrange
            var venueDTO = new VenueDTO() { Name = "name", Description = "test", Address = "asd", Phone = "123" };
            service.Create(venueDTO);

            //Assert
            Assert.AreEqual(service.GetByName("name").Name, "name");

            service.Delete(service.GetByName("name").Id);
        }
    }
}
