using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
      [Authorize(Roles = "Administrators")]
    //[Authorize(Policy = "DeleteEquipment")]
    public class EquipmentManagerController : Controller
    {
          
        private IEquipmentRepository _equipmentRepository;

        public EquipmentManagerController(IEquipmentRepository equipmentRepository)
            {
                _equipmentRepository = equipmentRepository;
               
            }

            public ViewResult Index()
            {
                
             var equipment = _equipmentRepository.GetAllEquipment().OrderBy(e => e.Name);
            return View(equipment);
            }

            public IActionResult AddEquipment()
            {
                 var equipment = _equipmentRepository.GetAllEquipment();
                var equipmentEditViewModel = new EquipmentEditViewModel
                {
                   
                };
                return View(equipmentEditViewModel);
            }

            [HttpPost]
            public IActionResult AddEquipment(EquipmentEditViewModel equipmentEditViewModel)
            {
                //custom validation rules
              // if (ModelState.GetValidationState("Equipment.Price") == ModelValidationState.Valid
                //   || equipmentEditViewModel.Equipment.Price < 0)
                  // ModelState.AddModelError(nameof(equipmentEditViewModel.Equipment.Price), "The price of the equipment should be higher than 0");

               

                if (ModelState.IsValid)
                {
                    _equipmentRepository.CreateEquipment(equipmentEditViewModel.Equipment);
                    return RedirectToAction("Index");
                }

                return View(equipmentEditViewModel);
            }


        [HttpGet]
        public IActionResult EditEquipment(int Id)
        {
            var equipment = _equipmentRepository.GetAllEquipment().OrderBy(e => e.Name);

            var equipmentEditViewModel = new EquipmentEditViewModel
            {

                EquipmentId = Id.ToString()

            };
            return View(equipmentEditViewModel);
        }

        [HttpPost]
            public IActionResult EditEquipment(EquipmentEditViewModel equipmentEditViewModel)
            {
            equipmentEditViewModel.Equipment.Id= equipmentEditViewModel.Equipment.Id;

                if (ModelState.IsValid)
                {
                    _equipmentRepository.UpdateEquipment(equipmentEditViewModel.Equipment);
                    return RedirectToAction("Index");
                }
                return View(equipmentEditViewModel);
            }

            [HttpPost]
            public IActionResult DeleteEquipment(int Id)
            {
            _equipmentRepository.DeleteEquipment(Id);
            return RedirectToAction("Index");
        }


            [AcceptVerbs("Get", "Post")]
            public IActionResult CheckIfPieNameAlreadyExists([Bind(Prefix = "Equipment.Name")]string name)
            {
                var equipment = _equipmentRepository.GetAllEquipment().FirstOrDefault(p => p.Name == name);
                return equipment == null ? Json(true) : Json("That name is already taken");
            }
        }
}
