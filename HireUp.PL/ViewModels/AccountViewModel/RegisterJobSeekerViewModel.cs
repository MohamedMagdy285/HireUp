
using System.ComponentModel.DataAnnotations;

namespace HireUp.PL.ViewModels.AccountViewModel
{
    public class RegisterJobSeekerViewModel
    {
        [Required(ErrorMessage = "National ID is required")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be exactly 14 digits")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must contain only digits")]
        public string NationalNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select a country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City/Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms and conditions")]
        public bool Terms { get; set; }
    }
}
