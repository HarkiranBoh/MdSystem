using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Equipment Equipment { get; set; }
        public int EquipmentId { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public decimal SubTotal { get; set; }
        
    }
}
