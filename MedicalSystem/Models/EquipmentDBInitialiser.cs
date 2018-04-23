using MedicalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public static class EquipmentDBInitialiser
    {
        public static void ProductInformation(AppDbContext context)
        {
            if (!context.Equipment.Any())
            {
                context.AddRange
              (
           
            );

                context.SaveChanges();
            }
        
        }
    }
}
