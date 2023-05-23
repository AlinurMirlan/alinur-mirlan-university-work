using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public required string Password { get; set; }

        [Display(Name = "Password confirmation")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public required string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
