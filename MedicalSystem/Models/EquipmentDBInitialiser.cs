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
            new Equipment { Id = 1, Name = "Product1", Price = 12.95M, ShortDescription = "Test", LongDescription = "Test_Long.", ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",  ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg" }
           
            );

                context.SaveChanges();
            }
        
        }
    }
}
