using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Model;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        public HomeController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var equipment = _equipmentRepository.GetAllEquipment().OrderBy(e => e.Name);

            var homeViewModel = new HomeViewModel()
            {
                equipment = equipment.ToList(),
                Title = "Welcome to the online medical store"
            };

            return View(homeViewModel);
        }
    }
}
