using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class Stock
    {
        public int SupplierId { get; set; }
        public int SupplierNumber { get; set; }
        public int StockId { get; set; }
        public int StockNumber { get; set; }
    }
}
