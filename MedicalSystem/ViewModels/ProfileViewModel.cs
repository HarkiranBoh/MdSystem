/*
 The view model for the profile of the user that can be editable
 This is for the dashboard.cshtml
 */
namespace MedicalSystem.ViewModels
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string HospitalName { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public string Expiration { get; set; }
    }
}
