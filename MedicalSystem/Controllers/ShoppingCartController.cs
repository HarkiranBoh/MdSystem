using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    public class ShoppingCartController : Controller
    {

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

        /* public RedirectToActionResult AddToCart(int equipmentId)
         {
            var selectedEquipment = _equipmentRepository.Equipment.FirstOrDefault(e => e.id == equipmentId);
             if (selectedEquipment != null)
             {
                 _shoppingCart.AddToCart(selectedEquipment, 1);

             }
             return RedirectToAction("Index");
    }*/
    }
}
