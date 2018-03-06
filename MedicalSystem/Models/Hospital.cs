using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string HospitalName {get; set;}
        public string HospitalAddress { get; set; }
        public string HospitalPhone { get; set; }
    }
}
