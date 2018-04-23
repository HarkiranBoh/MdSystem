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
        //uses the IViewComponentResult Invoke() method
        public IViewComponentResult Invoke()
        {
            //Variable menuItems that contain a List of type Admin Menu Item
            var menuItems = new List<AdminMenuItem> {

            new AdminMenuItem()
            {
                 DisplayValue = "User management",
                 ActionValue = "UserManagement"

            },
                //new meni item created displaying Role Mananager with the display and action value being set
            new AdminMenuItem()
            {
                DisplayValue = "Role management",
                ActionValue = "RoleManagement"

            }};

            //returns the View with the menuItems passes in
            return View(menuItems);
        }
    }

    //A class for the AdminMenuItem that sets the properties for Display and Action
    public class AdminMenuItem
    {
        public string DisplayValue { get; set; }
        public string ActionValue { get; set; }
    }
}
