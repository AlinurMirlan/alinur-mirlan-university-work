using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flash.Models.Identity;

[Table("User")]
[Index("EmailAddress", Name = "IX_User_Email", IsUnique = true)]
[Index("EmailAddress", "Password", Name = "IX_User_Email_Password")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Email address field must be filled.")]
    [DataType(DataType.EmailAddress)]
    [StringLength(255)]
    public string EmailAddress { get; set; } = default!;

    [Required(ErrorMessage = "Password field must be filled.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [StringLength(50)]
    public string Password { get; set; } = default!;

    [InverseProperty("User")]
    public virtual ICollection<Deck> Decks { get; } = new List<Deck>();
}
