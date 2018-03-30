using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MedicalSystem.Authentication;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    public class HomeController : Controller
    {
        //private field for the equipment reposistiry interface
        private readonly IEquipmentRepository _equipmentRepository;
 
        //dependancy injection of the constructor 
        public HomeController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
        
         return View();
        }

        //route attribut will route to the about url
        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
    }
}
