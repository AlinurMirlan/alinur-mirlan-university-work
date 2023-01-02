using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Flash.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flash.Models;

[Table("Deck")]
[Index("Name", Name = "IX_Deck_Name", IsUnique = true)]
public partial class Deck
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public int FlashcardsCount { get; set; }

    [InverseProperty("Deck")]
    public virtual ICollection<Flashcard> Flashcards { get; } = new List<Flashcard>();

    [ForeignKey("UserId")]
    [InverseProperty("Decks")]
    public virtual User User { get; set; }

    public Deck(User user, string name)
    {
        this.Name = name;
        this.User = user;
    }
}
