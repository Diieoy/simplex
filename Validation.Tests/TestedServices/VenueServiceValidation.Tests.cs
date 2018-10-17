using BLLStandard.DTO;
using BLLStandard.Exceptions;
using BLLStandard.Services;
using NUnit.Framework;
using System;
using UnitTests.FakeRepositories;

namespace UnitTests.TestedServices
{
    [TestFixture]
    public class VenueServiceValidation
    {
        private VenueService service;

        [SetUp]
        public void Init()
        {
            service = new VenueService(new FakeVenueRepository());
        }

        [Test]
        public void Create_Correct_Venue()
        {
            //Arrange
            var venueDTO = new VenueDTO { Name = "one", Description = "o", Address = "ads", Phone = "123" };

            //Act
            service.Create(venueDTO);
            var res = service.GetByName("one");

            //Assert
            Assert.AreEqual(res.Name, "one");
        }

        [Test]
        public void Try_To_Create_Incorrect_Venue()
        {
            //Arrange
            var venueDTO = new VenueDTO { Name = "one", Description = "o", Address = "ads", Phone = "123" };
            var incorrectVenueDTO = new VenueDTO { Name = "one", Description = "o", Address = "ads", Phone = "123" };

            //Act
            service.Create(venueDTO);

            //Assert
            Assert.Throws<NotUniqueNameException>(() => service.Create(incorrectVenueDTO));
        }

        [Test]
        public void Update_With_Correct_Venue()
        {
            //Arrange
            var venueDTO = new VenueDTO { Name = "test", Description = "test", Address = "asd", Phone = "123" };

            //Act
            service.Create(venueDTO);
            var venueDTOForUpdate = new VenueDTO { Id = 0, Name = "update", Description = "update", Address = "rrr", Phone = "1234" };
            service.Update(venueDTOForUpdate);

            //Assert
            Assert.AreEqual(service.GetById(0).Name, "update");
        }

        [Test]
        public void Try_To_Update_With_Incorrect_Venue()
        {
            //Arrange
            var venueDTO = new VenueDTO { Name = "test", Description = "test", Address = "asd", Phone = "123" };

            //Act
            service.Create(venueDTO);
            var incorrectVenueDTOForUpdate = new VenueDTO { Name = "test", Description = "test", Address = "asd", Phone = "123" };

            //Assert
            Assert.Throws<NotUniqueNameException>(() => service.Update(incorrectVenueDTOForUpdate));
        }
    }
}
