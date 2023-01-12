using System.ComponentModel.DataAnnotations;

namespace Flash.Models.Identity
{
    public class UserEdit : User
    {
        [Required(ErrorMessage = "New password field must be filled.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters long.")]
        public string NewPassword { get; set; } = default!;
    }
}
