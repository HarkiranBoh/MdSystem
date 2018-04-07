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
        private readonly ProfileRepository _profileRepository;

        //dependancy injection of the constructor 
        public HomeController(IEquipmentRepository equipmentRepository, ProfileRepository profileRepository)
        {
            _equipmentRepository = equipmentRepository;
            _profileRepository = profileRepository;


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

       // [HttpGet]
        //[Route("Dashboard")]
        //public IActionResult Dashboard()
        //{
          //  return View();
        //}

        // [HttpPost]
        //public IActionResult Dashboard(ProfileViewModel profileView)
        //{
        //  var profile = new User();
        // profile.FirstName = profileView.FirstName;
        //return View("Dashboord", profile);
        //}

        //only invoke for http invoke
        [HttpGet]
        public IActionResult Dashboard(string data)
        { 
            //get user information when logged in from httpcontext
            string userId = HttpContext.User.Identity.Name;
            //get the logged in user details
            var loggedInUser = _profileRepository.GetLoggedInUserDetails(userId);


            //in the variable called CheckoutViewModel create a new checkout view model object setting the properties defined.
            var profileViewModel = new ProfileViewModel
            {
                UserName = loggedInUser.UserName,
                FirstName = loggedInUser.FirstName,
                LastName = loggedInUser.LastName,
                HospitalName = loggedInUser.HospitalName,
                Email = loggedInUser.Email,
                AddressLine1 = loggedInUser.AddressLine1,
                AddressLine2 = loggedInUser.AddressLine2,
                PostCode = loggedInUser.PostCode,
                
            };

            //return the view with the variable - checkoutview passed in
            return View(profileViewModel);
        }

        //route contraints
        [HttpPost]
        public IActionResult Dashboard(ProfileViewModel profileViewModel)
        {
            string userId = HttpContext.User.Identity.Name;

             _profileRepository.UpdateUser(userId, profileViewModel);


            return View(profileViewModel);
            
        }
    }
}
