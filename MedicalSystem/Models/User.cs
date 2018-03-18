using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class User
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string UserName { get; set;}
        public string Email { get; set;}
        public string HospitalName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }

       
    }
}
