using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Flash.Models.Identity;

namespace Flash.Models;

[Table("Deck")]
[Index("Name", Name = "IX_Deck_Name", IsUnique = false)]
[Index("UserId", "Name", Name = "IX_Deck_UserId_Name", IsUnique = true)]
public partial class Deck
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = default!;

    public int? FlashcardsCount { get; set; }

    public float? SuccessRetentionRate { get; set; }

    public float? DifficultyRetentionRate { get; set; }

    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [InverseProperty("Deck")]
    public virtual ICollection<Flashcard> Flashcards { get; } = new List<Flashcard>();

    [ForeignKey("UserId")]
    [InverseProperty("Decks")]
    public virtual User User { get; set; } = default!;
}
