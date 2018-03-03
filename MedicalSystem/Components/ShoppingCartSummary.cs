using MedicalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalSystem.ViewModels;


namespace MedicalSystem.Components
{
    public class ShoppingCartSummary : ViewComponent //inherits from viewcompont
    {
        //created a local intance of shoppingcart 
        private readonly ShoppingCart _shoppingCart;
        
        //constructor getting in via dependancy injection - the shopping cart registed in the sevices collection in the start up
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        //will automatically be excecuted when view component is excecuted so when displayed on view
        public IViewComponentResult Invoke()
        {
            //the variable items will contain the shopping cart items
            var items = _shoppingCart.GetShoppingCartItems();
            //var items = new List<ShoppingCartItem>() { new ShoppingCartItem(), new ShoppingCartItem() };
            _shoppingCart.ShoppingCartItems = items;

            //the ShoppingCartViewModel will contain a shoppingcartviewmodel object
            var ShoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            //this will return the shopping cart view model as a view
            return View(ShoppingCartViewModel);
        }
    }
}
