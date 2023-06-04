using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.ViewModels
{
    public class RegistrationVm
    {
        private const string PasswordLengthErrorMessage = "Password must be at least 8 characters long.";

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = PasswordLengthErrorMessage)]
        public required string Password { get; set; }

        [Display(Name = "Password confirmation")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = PasswordLengthErrorMessage)]
        public required string ConfirmPassword { get; set; }

        [Display(Name = "Initial balance")]
        [DataType(DataType.Currency, ErrorMessage = "Please, type in valid amount.")]
        [Range(0, double.MaxValue, ErrorMessage = "Initial balance must be positive.")]
        public decimal InitialBalance { get; set; } = 0;

        public bool RememberMe { get; set; }
    }
}
