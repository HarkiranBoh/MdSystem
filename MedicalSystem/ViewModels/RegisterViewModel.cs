using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.ViewModels
{
    //view model for the resiger page - get, set properties of the register 
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FisrtName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your Email"), MaxLength(256), EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password, Enter Alphanumeric "), MinLength(6), MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "PostCode")]
        public string postcode { get; set; }
    }
}
