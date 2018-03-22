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
        private readonly IEquipmentRepository _equipmentRepository;
 
        public HomeController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
        
         return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
    }
}
