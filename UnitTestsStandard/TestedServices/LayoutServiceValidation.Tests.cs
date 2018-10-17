using System;
using BLLStandard.DTO;
using BLLStandard.Exceptions;
using BLLStandard.Services;
using NUnit.Framework;
using UnitTestsStandard.FakeRepositories;

namespace UnitTestsStandard.TestedServices
{
	[TestFixture]
    public class LayoutServiceValidation
    {
        private LayoutService service;

        [SetUp]
        public void Init()
        {
            service = new LayoutService(new FakeLayoutRepository());
        }

        [Test]
        public void Create_Correct_Layout()
        {
            //Arrange
            var layoutDTO = new LayoutDTO { Name = "one", Description = "o", VenueId = 1 };

            //Act
            service.Create(layoutDTO);
            var res = service.GetByName("one");

            //Assert
            Assert.AreEqual(res.Name, "one");
        }

        [Test]
        public void Try_To_Create_Incorrect_Layout()
        {
            //Arrange
            var layoutDTO = new LayoutDTO { Name = "one", Description = "o", VenueId = 1 };
            var incorrectLayoutDTO = new LayoutDTO { Name = "one", Description = "o", VenueId = 1 };

            //Act
            service.Create(layoutDTO);

            //Assert
            Assert.Throws<NotUniqueNameException>(() => service.Create(incorrectLayoutDTO));
        }

        [Test]
        public void Update_With_Correct_Layout()
        {
            //Arrange
            var layoutDTO = new LayoutDTO { Name = "test", Description = "test", VenueId = 1 };

            //Act
            service.Create(layoutDTO);
            var layoutDTOForUpdate = new LayoutDTO { Id = 1, Name = "update", Description = "update", VenueId = 2 };
            service.Update(layoutDTOForUpdate);

            //Assert
            Assert.AreEqual(service.GetById(1).Name, "update");
        }

        [Test]
        public void Try_To_Update_With_Incorrect_Layout()
        {
            //Arrange
            var layoutDTO = new LayoutDTO { Name = "test", Description = "test", VenueId = 1 };

            //Act
            service.Create(layoutDTO);
            var incorrectLayoutDTOForUpdate = new LayoutDTO { Id = 0, Name = "test", Description = "update", VenueId = 1 };

            //Assert
            Assert.Throws<NotUniqueNameException>(() => service.Update(incorrectLayoutDTOForUpdate));
        }
    }
}
