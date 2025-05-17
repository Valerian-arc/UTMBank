using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 25 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 25 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(50, ErrorMessage = "Email must not exceed 50 characters")]
        public string Email { get; set; }
    }
}