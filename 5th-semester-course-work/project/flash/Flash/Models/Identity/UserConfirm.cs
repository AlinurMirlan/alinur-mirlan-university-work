using System.ComponentModel.DataAnnotations;

namespace Flash.Models.Identity
{
    public class UserConfirm : User
    {
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
