using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flash.Models;

[Table("Flashcard")]
public partial class Flashcard
{
    [Key]
    public int Id { get; set; }

    public int DeckId { get; set; }

    [Required]
    public string Front { get; set; } = default!;

    [Required]
    public string Back { get; set; } = default!;

    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? RepetitionDate { get; set; }

    public int? RepetitionInterval { get; set; }

    [ForeignKey("DeckId")]
    [InverseProperty("Flashcards")]
    public virtual Deck Deck { get; set; } = default!;
}
