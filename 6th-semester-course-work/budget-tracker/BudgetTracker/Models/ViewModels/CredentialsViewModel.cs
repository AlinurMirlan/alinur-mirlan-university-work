using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.ViewModels
{
    public class CredentialsViewModel
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
