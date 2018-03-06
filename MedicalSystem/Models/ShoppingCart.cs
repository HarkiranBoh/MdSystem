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
            private readonly AppDbContext _appDbContext;
            private ShoppingCart(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public string ShoppingCartId { get; set; }

            public List<ShoppingCartItem> ShoppingCartItems { get; set; }

            public static ShoppingCart GetCart(IServiceProvider services)
            {
                ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                    .HttpContext.Session;

                var context = services.GetService<AppDbContext>();
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

                session.SetString("CartId", cartId);

                return new ShoppingCart(context) { ShoppingCartId = cartId };
            }

            public void AddToCart(Equipment equipment, int amount)
            {
                var shoppingCartItem =
                        _appDbContext.ShoppingCartItems.SingleOrDefault(
                            s => s.Equipment.Id == equipment.Id && s.ShoppingCartId == ShoppingCartId);

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = ShoppingCartId,
                        Equipment = equipment,
                        Amount = amount,
                        SubTotal = equipment.Price * amount
                    };

                    _appDbContext.ShoppingCartItems.Add(shoppingCartItem); 

                }
                else
                {
                    shoppingCartItem.Amount++;
                }
                _appDbContext.SaveChanges();
            }

            public void RemoveFromCart(int ShoppingCartItemId)
            {
            var shoppingCartItem = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartItemId == ShoppingCartItemId).FirstOrDefault();
                 
                    
             _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                    
             _appDbContext.SaveChanges();

            }

            public List<ShoppingCartItem> GetShoppingCartItems()
            {
                return ShoppingCartItems ??
                       (ShoppingCartItems =
                           _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                               .Include(s => s.Equipment)
                               .ToList());
           
            }

            public void ClearCart(Guid clear)
            {
                var cartItems = _appDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId).ToList();

                _appDbContext.ShoppingCartItems.RemoveRange(cartItems);
            
                _appDbContext.SaveChanges();
            }



            public decimal GetShoppingCartTotal()
            {
                var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Select(c => c.Equipment.Price * c.Amount).Sum();
                return total;
            }

       public void UpdateCart(int shoppingCartItemId, int equipmentId, int amount)
        {
            //get shopping cart item record from db
            var shoppingCartItem = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartItemId == shoppingCartItemId).FirstOrDefault();
            var equipment = _appDbContext.Equipment.Where(e => e.Id == equipmentId).FirstOrDefault();

            //update quantity
            if (shoppingCartItem != null)
            {
                shoppingCartItem.Amount = amount;
                shoppingCartItem.SubTotal = shoppingCartItem.Amount * equipment.Price;
            }
            _appDbContext.Update(shoppingCartItem);
            //save changes
            _appDbContext.SaveChanges();
            
        }
    }
}


