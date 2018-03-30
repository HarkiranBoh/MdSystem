using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;
using MedicalSystem.Authentication;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    //set the route of the view connected to this controller
    [Route("Order/[controller]/[action]/{data?}")]
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
        [HttpGet]
        public IActionResult Checkout(string data)
        {
        
            var items = _checkout.GetEquipment(data);
            //get user information when logged in from httpcontext
            string userId = HttpContext.User.Identity.Name;
            //get the logged in user details
            var loggedInUser = _checkout.GetLoggedInUserDetails(userId);
            
            //in the variable called CheckoutViewModel create a new checkout view model object setting the properties defined.
            var CheckoutViewModel = new CheckoutViewModel
            {
                UserName = loggedInUser.UserName,
                FirstName = loggedInUser.FirstName,
                LastName = loggedInUser.LastName,
                HospitalName = loggedInUser.HospitalName,
                Email = loggedInUser.Email,
                AddressLine1 = loggedInUser.AddressLine1,
                AddressLine2 = loggedInUser.AddressLine2,
                PostCode = loggedInUser.PostCode,
                ShoppingCartItems = items,
                ShoppingCartId = data
                
            };
            //return the view with the variable - checkoutview passed in
            return View(CheckoutViewModel);
        }

        //post atribute
        [HttpPost]
        public IActionResult ConfirmOrder(CheckoutViewModel checkout)
        {
            //create an order object with the properties set
            var order = new Order {

                Name = checkout.FirstName + " " + checkout.LastName,
                HospitalName = checkout.HospitalName,
                HospitalAdress = checkout.AddressLine1 + " " + checkout.AddressLine2,
                PostCode = checkout.PostCode,
                ShoppingCartId = checkout.ShoppingCartId,
                
            };
            //create the order 
            _orderRepository.CreateOrder(order);
            //then return the view.
            return View(order);
        }
    }

}
