using MedicalSystem.Models;
using System;
using System.Collections.Generic;
using Moq;

namespace MedicalSystem.Tests.Model
{
    public class MockData
    {
        public static Mock<IEquipmentRepository> GetEquipmentRepository()
        {
            var equipment = new List<Equipment>
            {
                new Equipment
                {
                    Name = "TestName",
                    Price = 12.95M,
                    ShortDescription = "short test",
                    LongDescription =
                        "test",
                  
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",
                    InStock = true,
                   
                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg",
                  
                },
                 new Equipment
                {
                    Name = "TestName2",
                    Price = 15.95M,
                    ShortDescription = "My God!",
                    LongDescription =
                        "Long test",
                  
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg",
                    InStock = true,

                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg",
                    
                },
            };

            var mockEquipmentRepository = new Mock<IEquipmentRepository>();
            mockEquipmentRepository.Setup(repo => repo.GetAllEquipment()).Returns(equipment);
            mockEquipmentRepository.Setup(repo => repo.GetEquipmentById(It.IsAny<int>())).Returns(equipment[0]);
            return mockEquipmentRepository;
        }
    }
}
