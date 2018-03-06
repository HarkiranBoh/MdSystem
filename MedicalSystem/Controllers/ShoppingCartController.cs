using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    [Route("Equipment/[controller]/[action]/{data?}")]
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
        [HttpGet]
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

        public IActionResult RemoveFromCart(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<ShoppingCartItem>(data);
            
            
                _shoppingCart.RemoveFromCart(jsonData.ShoppingCartItemId);
               
            
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(string data)
         {

            //parse the data that's been sent
            var jsonData = JsonConvert.DeserializeObject<ShoppingCartItem>(data);

            //call Updatecart method in model and pass data
            _shoppingCart.UpdateCart(jsonData.ShoppingCartItemId, jsonData.EquipmentId, jsonData.Amount);

            return RedirectToAction("Index");
                
        }


        public IActionResult ClearCart(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<ShoppingCartViewModel>(data);
            _shoppingCart.ClearCart(jsonData.ShoppingCartId);
            return RedirectToAction("Index");
        }
    }
    }

