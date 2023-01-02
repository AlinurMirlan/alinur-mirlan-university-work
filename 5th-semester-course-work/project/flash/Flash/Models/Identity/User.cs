using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flash.Models.Identity;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string EmailAddress { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Deck> Decks { get; } = new List<Deck>();

    public User(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }
}
