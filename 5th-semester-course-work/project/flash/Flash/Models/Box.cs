using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flash.Models;

[Table("Box")]
public partial class Box
{
    [Key]
    public int Id { get; set; }

    public int RepetitionInterval { get; set; }

    [InverseProperty("Box")]
    public virtual ICollection<Flashcard> Flashcards { get; } = new List<Flashcard>();
}
