using MedicalSystem.Controllers;
using MedicalSystem.Models;
using MedicalSystem.Tests.Model;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MedicalSystem.Tests
{
    public class EquipmentManagerControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_ContainsAllEquipment()
        {
            //arrange
            
            var mockEquipmentRepository = MockData.GetEquipmentRepository();

            var equipmentManagerController = new EquipmentManagerController(mockEquipmentRepository.Object);

            //act
            var result = equipmentManagerController.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var equipment = Assert.IsAssignableFrom<IEnumerable<Equipment>>(viewResult.ViewData.Model);
            Assert.Equal(2, equipment.Count());
        }

        [Fact]
        public void AddEquipment_Redirects_ValidEquipmnetViewModel()
        {
            //arrange
            var equipmentEditViewModel = new EquipmentEditViewModel();
            var mockEquipmentRepository = MockData.GetEquipmentRepository();
            var equipment = mockEquipmentRepository.Object.GetEquipmentById(1);
            equipmentEditViewModel.Equipment = equipment;
           
            

            var equipmentManagerController = new EquipmentManagerController(mockEquipmentRepository.Object);

            //act
            var result = equipmentManagerController.AddEquipment(equipmentEditViewModel);

            //assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
