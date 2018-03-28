using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class Order
    {
       
        public int OrderId { get; set; }

        public List<OrderDetail> OrderLines { get; set; }

        [Display(Name = "Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }

       
        [StringLength(100)]
        [Display(Name = "Hospital Address")]
        public string HospitalAdress { get; set; }

       
        [Display(Name = "Post code")]
        [StringLength(10, MinimumLength = 4)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

       
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal SubTotal { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }

        public string ShoppingCartId { get; set; }
    }
    }

