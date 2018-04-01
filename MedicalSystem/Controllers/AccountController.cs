using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MedicalSystem.ViewModels;
using MedicalSystem.Authentication;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    public class AccountController : Controller
    {
        //readonly method
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        //dependacy injection that inject the sign in manager and the user manager
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            //checking to see if input is valid - if not return the user back to the login view model
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "User name/Password not found");
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            //return View(new RegisterViewModel());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel RegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = RegisterViewModel.UserName, Email = RegisterViewModel.Email,
                    AddressLine1 = RegisterViewModel.AddressLine1, AddressLine2 = RegisterViewModel.AddressLine2,
                    HospitalName = RegisterViewModel.HospitalName, postcode = RegisterViewModel.postcode  };

                //set the password that the user passed in
                var result = await _userManager.CreateAsync(user, RegisterViewModel.Password);
                
                //check the result - sucessfully created the user with the account
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                {
                    //add all errors to the model state
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(RegisterViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }

}
