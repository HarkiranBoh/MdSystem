using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Details(int id)
        {
            var Equipment = _equipmentRepository.GetEquipmentById(id);
            if (Equipment == null)
                return NotFound();

            return View(Equipment);
        }
    }
}
