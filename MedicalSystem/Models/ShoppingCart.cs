using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class ShoppingCart
    {
            //private fields have been created of my db context
            private readonly AppDbContext _appDbContext;
            //class shopping cart takes an instance of appDbContext
            private ShoppingCart(AppDbContext appDbContext)
            {
                //make field equal to parameter appDbContext
                _appDbContext = appDbContext;
            }


        //public string ShoppingCartId get and set it.
        public string ShoppingCartId { get; set; }
           
            //List of type<ShoppingCartItem> 
            public List<ShoppingCartItem> ShoppingCartItems { get; set; }

            //get the cart using sessions
            public static ShoppingCart GetCart(IServiceProvider services)
            {
                ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                    .HttpContext.Session;

                var context = services.GetService<AppDbContext>();

                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

                session.SetString("CartId", cartId);

                return new ShoppingCart(context) { ShoppingCartId = cartId };
            }
            //add to cart funtion takes parameters equipment and int amouut
            public void AddToCart(Equipment equipment, int amount)
            {
                //in the shopping cart items table get the single or default equipment and shopping cart  
                var shoppingCartItem =_appDbContext.ShoppingCartItems.SingleOrDefault(s => s.Equipment.Id == equipment.Id && s.ShoppingCartId == ShoppingCartId);

                //if there is no shopping items in the cart
                if (shoppingCartItem == null)
                {
                    //creates a new shopping cart object
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = ShoppingCartId,
                        Equipment = equipment,
                        Amount = amount,
                        SubTotal = equipment.Price * amount
                    };

                    //then adds the varable to the db
                    _appDbContext.ShoppingCartItems.Add(shoppingCartItem); 

                }
                else
                {
                    //increment the amount or quantity of equipemnt
                    shoppingCartItem.Amount++;
                }
                //save shanges in db
                _appDbContext.SaveChanges();
            }

            //remove from cart function had a parameter int ShoppingCartId
            public void RemoveFromCart(int ShoppingCartItemId)
            {
            //var shoppingcartitem. get from shoppingCartItems where shoppingCartItemId == shopping cart item id get first or default
            var shoppingCartItem = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartItemId == ShoppingCartItemId).FirstOrDefault();
              //DbContext in shopping cart items table remove ShoppingCartItem varible
             _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
              //save changes      
             _appDbContext.SaveChanges();

            }

            //Get Shopping Cart Items in list 
            public List<ShoppingCartItem> GetShoppingCartItems()
            {
                //return shopping cart items in db where shopping cart id == shopping cart id passed in and also include the equipent list
                return ShoppingCartItems ?? 
                (ShoppingCartItems = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Equipment).ToList());
           
            }
            
            //function clear cart takes the guid clear parameter
            public void ClearCart(Guid clear)
            {
                //shopping cart items table get shopping cart id equal to the shopping cart item being passed then to list it as there can be more than one item 
                var cartItems = _appDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId).ToList();
                
                //in the database remove all the items from the database
                _appDbContext.ShoppingCartItems.RemoveRange(cartItems);
            
                //then database save
                _appDbContext.SaveChanges();
            }


            //get shopping cart total method returns a decimal value and takes no parametes
            public decimal GetShoppingCartTotal()
            {
                //find the id of the shopping cart in the shopping cart items table - select the equipment price from table multiplied by the amout or quantity and calculate the sum. 
                var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Equipment.Price * c.Amount).Sum();
                //return the variable total...
                return total;
            }

            
            //this is the update cart function which passses in 3 parameters and returns no value.
            public void UpdateCart(int shoppingCartItemId, int equipmentId, int amount)
            {
            //get shopping cart item record from db selects the shopping cart item row first value or default and is put in a variable 
            //get the equipmentId record from the equipment table where the equipment id selected is the same as in the equipment table first or default

            var shoppingCartItem = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartItemId == shoppingCartItemId).FirstOrDefault();
                
            var equipment = _appDbContext.Equipment.Where(e => e.Id == equipmentId).FirstOrDefault();

            
            if (shoppingCartItem != null)
            {
                //set the shoppincartItem.Amount equal to amount being passed in the view
                shoppingCartItem.Amount = amount;
                //set the subtotal of the shopping cart item equal to the shopping cart item amount multiplied with the equipment price.
                shoppingCartItem.SubTotal = shoppingCartItem.Amount * equipment.Price;
            }
                //update the variable holding the shopping cart item id row in database
                _appDbContext.Update(shoppingCartItem);
            
            //save changes in the database
            _appDbContext.SaveChanges();
            
        }
    }
}


