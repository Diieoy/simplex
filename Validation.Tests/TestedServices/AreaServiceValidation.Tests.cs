using BLLStandard.DTO;
using BLLStandard.Exceptions;
using BLLStandard.Services;
using NUnit.Framework;
using System;
using UnitTests.FakeRepositories;

namespace UnitTests.TestedServices
{
    [TestFixture]
    public class AreaServiceValidation
    {
        private AreaService service;

        [SetUp]
        public void Init()
        {
            service = new AreaService(new FakeAreaRepository());
        }

        [Test]
        public void Create_Correct_Area()
        {
            //Arrange
            var areaDTO = new AreaDTO { LayoutId = 0, Description="area1", CoordX = 1, CoordY = 1};

            //Act
            service.Create(areaDTO);
            var res = service.GetById(1);

            //Assert
            Assert.AreEqual(res.Description, "area1");
        }

        [Test]
        public void Try_To_Create_Incorrect_Area()
        {
            //Arrange
            var areaDTO = new AreaDTO { LayoutId = 5, Description = "test", CoordX = 1, CoordY = 1 };
            var incorrectAreaDTO = new AreaDTO { LayoutId = 5, Description = "test", CoordX = 2, CoordY = 2 };

            //Act
            service.Create(areaDTO);
            //Assert
            Assert.Throws<NotUniqueDescriptionException>(() => service.Create(incorrectAreaDTO));
        }

        [Test]
        public void Update_With_Correct_Description()
        {
            //Arrange
            var areaDTO = new AreaDTO { LayoutId = 5, Description = "test", CoordX = 1, CoordY = 1 };

            //Act
            service.Create(areaDTO);
            var areaDTOForUpdate = new AreaDTO { Id = 1, LayoutId = 5, Description = "update", CoordX = 2, CoordY = 2 };
            service.Update(areaDTOForUpdate);

            //Assert
            Assert.AreEqual(service.GetById(1).Description, "update");
        }

        [Test]
        public void Try_To_Update_With_Incorrect_Layout()
        {
            //Arrange
            var areaDTO = new AreaDTO { LayoutId = 5, Description = "test", CoordX = 1, CoordY = 1 };
            var areaDTO2 = new AreaDTO { LayoutId = 5, Description = "for update", CoordX = 2, CoordY = 2 };

            //Act
            service.Create(areaDTO);
            service.Create(areaDTO2);
            var incorrectAreaDTOWithIncorrectDescription = service.GetById(2);
            incorrectAreaDTOWithIncorrectDescription.Description = "test";

            //Assert
            Assert.Throws<NotUniqueDescriptionException>(() => service.Update(incorrectAreaDTOWithIncorrectDescription));
        }
    }
}
