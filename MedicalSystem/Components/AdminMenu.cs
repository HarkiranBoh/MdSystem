using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Components
{
    //view component for the admin menu 
    public class AdminMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<AdminMenuItem> { new AdminMenuItem()
                {
                    DisplayValue = "User management",
                    ActionValue = "UserManagement"

                }};

            return View(menuItems);
        }
    }

    public class AdminMenuItem
    {
        public string DisplayValue { get; set; }
        public string ActionValue { get; set; }
    }
}
