using MedicalSystem.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class Checkout
    {

        private readonly AppDbContext _appDbContext;
        

        public Checkout(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<ShoppingCartItem> GetItems(Guid shoppingCartId)
        {
            return _appDbContext.ShoppingCartItems.Where(i => i.ShoppingCartId == shoppingCartId.ToString()).ToList();
        }

       public List<ShoppingCartItem> GetEquipment(string shoppingCartId)
        {

            
            return _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == shoppingCartId).Include(s => s.Equipment).ToList();

        }

        
        public decimal GetShoppingCartTotal(string ShoppingCartId)
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Equipment.Price * c.Amount).Sum();
           
            return total;
        }


        public User GetLoggedInUserDetails(string userName)
        {
            var rec = _appDbContext.Users.Where(usr => usr.UserName == userName).FirstOrDefault();
            User user = null;
            if(rec != null)
            {
                user = new User();
                user.HospitalName = rec.HospitalName;
                user.UserName = rec.UserName;
                user.Email = rec.Email;
                user.PostCode = rec.postcode;
                user.AddressLine1 = rec.AddressLine1;
                user.AddressLine2 = rec.AddressLine2;
                user.FirstName = rec.FirstName;
                user.LastName = rec.LastName;
            }
            return user;
        }
    }
}
        


    

