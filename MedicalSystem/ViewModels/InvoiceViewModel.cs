using MedicalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.ViewModels
{
    public class InvoiceViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string HospitalName { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }

        public List<OrderDetail> OrderLines { get; set; }

        public int OrderId { get; set; }
        public int Total { get; set; }

    }
}
