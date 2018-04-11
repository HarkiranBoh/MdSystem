using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Authentication
{
    public class MedicalEquipmentSystemClaimTypes
    {
        public static List<string> ClaimsList { get; set; } = new List<string> { "Delete Item", "Add Item", "Age for ordering" };
    }
}
