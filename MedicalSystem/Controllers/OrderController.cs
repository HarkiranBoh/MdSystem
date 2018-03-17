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
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;


        //dependancy injecton constructor 
        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }
        //returns the checkout page
        
        public IActionResult Checkout()
        {

            

            var RegisterViewModel = new RegisterViewModel()

            {
                
                
            };
            
            
            return View();
        }

        public IActionResult OrderStatus()
        {
            return View();
        }
    }
}
