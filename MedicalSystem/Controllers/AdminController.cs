using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalSystem.Authentication;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
  // [Authorize(Roles="Administrator")]
    public class AdminController : Controller
    {
        //using the UserManger class to work the users of the aplication
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        //UserManager and rolemanger is injected using dependancy injection
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            var users = _userManager.Users;
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        //adds the user asynchronously 
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid) return View(addUserViewModel);

            var user = new ApplicationUser()
            {
                UserName = addUserViewModel.UserName,
                Email = addUserViewModel.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("UserManagement", _userManager.Users);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(addUserViewModel);
        }
        //will create the 
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return RedirectToAction("UserManagement", _userManager.Users);

            var claims = await _userManager.GetClaimsAsync(user);
            var vm = new EditUserViewModel() { Id = user.Id, Email = user.Email, UserName = user.UserName, HospitalName = user.HospitalName};

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            var user = await _userManager.FindByIdAsync(editUserViewModel.Id);

            if (user != null)
            {
                user.Email = editUserViewModel.Email;
                user.UserName = editUserViewModel.UserName;
               
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("UserManagement", _userManager.Users);

                ModelState.AddModelError("", "User not updated, something went wrong.");

                return View(editUserViewModel);
            }

            return RedirectToAction("UserManagement", _userManager.Users);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("UserManagement");
                else
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
            }
            else
            {
                ModelState.AddModelError("", "This user can't be found");
            }
            return View("UserManagement", _userManager.Users);
        }

        //roles managment 
     
        

      
    }
}
