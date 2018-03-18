﻿using MedicalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.ViewModels
{
    public class CheckoutViewModel
    {
        public string UserName { get; set; }
        public string HospitalName { get; set; }
        public string Email { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        
    }
}
