using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalSystem.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string HospitalName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string postcode { get; set; }

    }
}
