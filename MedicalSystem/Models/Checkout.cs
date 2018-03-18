using MedicalSystem.Authentication;
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

        public User GetLoggedInUserDetails(string userName)
        {
            var rec = _appDbContext.Users.Where(usr => usr.UserName == userName).FirstOrDefault();
            User user = null;
            if(rec != null)
            {
                user = new User();
                user.HospitalName = rec.HospitalName;
                user.UserName = rec.UserName;
            }
            return user;
        }
    }
}
        


    

