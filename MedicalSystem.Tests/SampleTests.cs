using MedicalSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MedicalSystem.Tests
{
    public class SampleTests
    {
        //attribute tells us it should be run in the test runner.
        [Fact]
        public void CanUpdateEquipmentPrice()
        {
            // Arrange
            var equipment = new Equipment { Name = "Sample pie", Price = 12.95M };
            // Act
            equipment.Price = 20M;
            //Assert
            Assert.Equal(20M, equipment.Price);
        }

        [Fact]
        public void CanUpdateEquipmentName()
        {
            // Arrange
            var equipment = new Equipment { Name = "Sample equipment", Price = 12.95M };
            // Act
            equipment.Name = "Another one";
            //Assert
            Assert.Equal("Another one", equipment.Name);
        }
    }
}
