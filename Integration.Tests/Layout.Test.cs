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
    public class Layout
    {
        private LayoutService service;

        [SetUp]
        public void Init()
        {
            service = new LayoutService(new LayoutRepository());
        }

        [Test]
        public void Create_Correct_Layout()
        {
            //Arrange
            var layoutDTO = new LayoutDTO() { Name = "test", Description = "test", VenueId = 1 };

            //Act
            service.Create(layoutDTO);
            var res = service.GetByName("test");

            //Assert
            Assert.AreEqual(layoutDTO.Name, res.Name);

            service.Delete(res.Id);
        }

        [Test]
        public void Try_To_Create_Incorrect_Layout()
        {
            //Arrange
            var layoutDTO = new LayoutDTO() { Name = "test", Description = "test", VenueId = 1 };
            var incorrectLayoutDTO = new LayoutDTO() { Name = "test", Description = "test", VenueId = 1 };

            //Act
            service.Create(layoutDTO);

            //Assert
            Assert.Throws<NotUniqueNameException>(() => service.Create(incorrectLayoutDTO));

            service.Delete(service.GetByName("test").Id);
        }

        [Test]
        public void Delete()
        {
            //Arrange
            var layoutDTO = new LayoutDTO() { Name = "test", Description = "test", VenueId = 1 };
            service.Create(layoutDTO);
            int id = service.GetByName("test").Id;

            //Act
            service.Delete(id);
            var res = service.GetByName("test");

            //Assert
            Assert.AreEqual(res, null);
        }

        [Test]
        public void Update()
        {
            //Arrange
            var layoutDTO = new LayoutDTO() { Name = "test", Description = "test", VenueId = 1 };
            var layoutForUpdate = new LayoutDTO() { Name = "update", Description = "update", VenueId = 1 };
            service.Create(layoutDTO);
            layoutForUpdate.Id = service.GetByName("test").Id;

            //Act
            service.Update(layoutForUpdate);

            //Assert
            Assert.AreEqual(layoutForUpdate.Name, service.GetByName("update").Name);

            service.Delete(service.GetByName("update").Id);
        }

        [Test]
        public void GetAll()
        {
            var res = service.GetAll().ToList();

            Assert.AreEqual(res.Count, 3);
        }

        [Test]
        public void GetById()
        {
            //Assert
            Assert.AreEqual(service.GetById(1).Name, "Layout 1");
        }

        [Test]
        public void GetByName()
        {
            //Assert
            Assert.AreEqual(service.GetByName("Layout 1").Name, "Layout 1");
        }
    }
}
