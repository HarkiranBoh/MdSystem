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
    //Authorise the roles that have the Adminstrators role
      [Authorize(Roles = "Administrators")]
    
    public class EquipmentManagerController : Controller
    {
          
        private IEquipmentRepository _equipmentRepository;

        //dependancy injection
        public EquipmentManagerController(IEquipmentRepository equipmentRepository)
            {
                _equipmentRepository = equipmentRepository;
               
            }

        //view index that returns all the equipment 
            public ViewResult Index()
            {
                
             var equipment = _equipmentRepository.GetAllEquipment().OrderBy(e => e.Name);
            return View(equipment);
            }

        //Action method - Add equipment that returns the edit view model
            public IActionResult AddEquipment()
            {
                 var equipment = _equipmentRepository.GetAllEquipment();
                var equipmentEditViewModel = new EquipmentEditViewModel
                {
                   
                };
                return View(equipmentEditViewModel);
            }

        //post attribute will only be invoked for post requests
            [HttpPost]
            public IActionResult AddEquipment(EquipmentEditViewModel equipmentEditViewModel)
            {
                //custom validation rules
              // if (ModelState.GetValidationState("Equipment.Price") == ModelValidationState.Valid
                //   || equipmentEditViewModel.Equipment.Price < 0)
                  // ModelState.AddModelError(nameof(equipmentEditViewModel.Equipment.Price), "The price of the equipment should be higher than 0");

               
            //checks to see if inputed data is valig
                if (ModelState.IsValid)
                {
                //this will create the Equipment and store it in the database. The page will then redirect to the current page
                    _equipmentRepository.CreateEquipment(equipmentEditViewModel.Equipment);
                    return RedirectToAction("Index");
                }

                return View(equipmentEditViewModel);
            }

        //get request that will get edit equipment with the equipment id being passed
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
        //seperate HTTP post for the edit equipment will update the changes and pass the view back.
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

        //delete will get the id of the equipment needing to be deleted.
            [HttpPost]
            public IActionResult DeleteEquipment(int Id)
            {
            _equipmentRepository.DeleteEquipment(Id);
            return RedirectToAction("Index");
        }


            //[AcceptVerbs("Get", "Post")]
            //public IActionResult CheckIfEquipmentNameAlreadyExists([Bind(Prefix = "Equipment.Name")]string name)
            //{
            //    var equipment = _equipmentRepository.GetAllEquipment().FirstOrDefault(p => p.Name == name);
            //    return equipment == null ? Json(true) : Json("That name is already taken");
            //}
        }
}
