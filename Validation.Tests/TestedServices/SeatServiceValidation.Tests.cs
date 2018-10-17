using BLLStandard.DTO;
using BLLStandard.Services;
using NUnit.Framework;
using System;
using UnitTests.FakeRepositories;

namespace UnitTests.TestedServices
{
    [TestFixture]
    public class SeatServiceValidation
    {
        private SeatService service;

        [SetUp]
        public void Init()
        {
            service = new SeatService(new FakeSeatRepository());
        }

        [Test]
        public void Create_Correct_Seat()
        {
            //Arrange
            var seatDTO = new SeatDTO { AreaId = 1, Row = 1, Number = 1 };

            //Act
            service.Create(seatDTO);
            var res = service.GetById(0);

            //Assert
            Assert.AreEqual(seatDTO.AreaId, res.AreaId);
            Assert.AreEqual(seatDTO.Row, res.Row);
            Assert.AreEqual(seatDTO.Number, res.Number);
        }

        [Test]
        public void Try_To_Create_Incorrect_Seat()
        {
            //Arrange
            var seatDTO = new SeatDTO { AreaId = 1, Row = 1, Number = 1 };
            var incorrectSeatDTO = new SeatDTO { AreaId = 1, Row = 1, Number = 1 };

            //Act
            service.Create(seatDTO);

            //Assert
            Assert.Throws<Exception>(() => service.Create(incorrectSeatDTO));
        }

        [Test]
        public void Update_With_Correct_Seat()
        {
            //Arrange
            var seatDTO = new SeatDTO { AreaId = 1, Row = 1, Number = 1 };

            //Act
            service.Create(seatDTO);
            var seatDTOForUpdate = new SeatDTO { Id = 0, AreaId = 1, Row = 1, Number = 2 };
            service.Update(seatDTOForUpdate);

            //Assert
            Assert.AreEqual(service.GetById(0).AreaId, 1);
            Assert.AreEqual(service.GetById(0).Row, 1);
            Assert.AreEqual(service.GetById(0).Number, 2);
        }

        [Test]
        public void Try_To_Update_With_Incorrect_Seat()
        {
            //Arrange
            var seatDTO = new SeatDTO { AreaId = 1, Row = 1, Number = 1 };

            //Act
            service.Create(seatDTO);
            var incorrectSeatDTOForUpdate = new SeatDTO { AreaId = 1, Row = 1, Number = 1 };

            //Assert
            Assert.Throws<Exception>(() => service.Update(incorrectSeatDTOForUpdate));
        }
    }
}
