using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    [Route("Equipment/[controller]/[action]/{id?}")]
    public class ShoppingCartController : Controller
    {
        //will be displayed through contr injection
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IEquipmentRepository equipmentRepository, ShoppingCart shoppingCart)
        {
            _equipmentRepository = equipmentRepository;
            _shoppingCart = shoppingCart;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var ShoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(ShoppingCartViewModel);
        }

         public IActionResult AddToCart(int equipmentId)
         {
            var selectedEquipment = _equipmentRepository.GetEquipmentById(equipmentId);
              if (selectedEquipment != null)
              {
                  _shoppingCart.AddToCart(selectedEquipment, 1);

              }
            return RedirectToAction("Index");
            } 

        public IActionResult RemoveFromCart(int equipmentId)
        {
            var selectedEquipment = _equipmentRepository.GetEquipmentById(equipmentId);
            if (selectedEquipment != null)
            {
                _shoppingCart.RemoveFromCart(selectedEquipment);
               
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
              _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }

       /* public ActionResult UpdateCart()
        {
            
        }*/
    }
    }

