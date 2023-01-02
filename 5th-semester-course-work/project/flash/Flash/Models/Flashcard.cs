using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flash.Models;

[Table("Flashcard")]
public partial class Flashcard
{
    [Key]
    public int Id { get; set; }

    public int BoxId { get; set; }

    public int DeckId { get; set; }

    [Required]
    public string Front { get; set; }

    [Required]
    public string Back { get; set; }

    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? RepetitionDate { get; set; }

    [ForeignKey("BoxId")]
    [InverseProperty("Flashcards")]
    public virtual Box Box { get; set; }

    [ForeignKey("DeckId")]
    [InverseProperty("Flashcards")]
    public virtual Deck Deck { get; set; }

    public Flashcard(Deck deck, Box box, string front, string back)
    {
        this.Deck = deck;
        this.Box = box;
        this.Front = front;
        this.Back = back;
    }
}
