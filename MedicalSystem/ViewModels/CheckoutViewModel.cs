using MedicalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.ViewModels
{
    public class CheckoutViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string HospitalName { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public string ShoppingCartId { get; set; }
        public List<Equipment> Equipment { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public string Expiration { get; set; }

    }
}
