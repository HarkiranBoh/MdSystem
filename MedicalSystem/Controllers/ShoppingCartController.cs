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
    //this defines the route url of the shopping cart page - this will route to the equipment/the controller - shoppingCart - the action/ which is defined in this class
    [Route("Equipment/[controller]/[action]/{data?}")]
    public class ShoppingCartController : Controller
    {
        //private fields made for the IequipmentRepository and the ShoppingCart
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ShoppingCart _shoppingCart;

        //this is a constructor thats takes in the equipmentRepository class and the shoppingCart as parameters
        public ShoppingCartController(IEquipmentRepository equipmentRepository, ShoppingCart shoppingCart)
        {
            // the parameters are assigned to the fields 
            _equipmentRepository = equipmentRepository;
            _shoppingCart = shoppingCart;
        }
        
        [HttpGet]
        //
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

        //Add to cart controller action is passes the equipmentId parameter
         public IActionResult AddToCart(int equipmentId)
         {
            //variable created contains the equipment id from the database
            var selectedEquipment = _equipmentRepository.GetEquipmentById(equipmentId);
            //if the equipment id is not null
            if (selectedEquipment != null)
              {     
                    //then add apply the add to cart method and pass the equipment and the amount
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
           // return RedirectToAction("Index");
            string ReturnURL = "/Equipment/ShoppingCart/Index";
            return Json(ReturnURL);
        }
    }
    }

