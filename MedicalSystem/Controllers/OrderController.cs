using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;
using MedicalSystem.Authentication;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Checkout _checkout;



        //dependancy injecton constructor 
        public OrderController(IOrderRepository orderRepository, Checkout checkout)
        {
            _orderRepository = orderRepository;
            _checkout = checkout;
            
        }
        //returns the checkout page
        
        public IActionResult Checkout()
        {
            //get user information when logged in from httpcontext
            string userId = HttpContext.User.Identity.Name;
            var loggedInUser = _checkout.GetLoggedInUserDetails(userId);

            var CheckoutViewModel = new CheckoutViewModel
            {
                UserName = loggedInUser.UserName,
                HospitalName = loggedInUser.HospitalName
                

            };

            return View(CheckoutViewModel);
        }

        public IActionResult OrderStatus()
        {
            return View();
        }
    }
}
