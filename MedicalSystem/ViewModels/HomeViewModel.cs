using MedicalSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }
        public List<Equipment> equipment;
    }
}
