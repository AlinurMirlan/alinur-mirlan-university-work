using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BudgetTracker.Models
{
    public class User : IdentityUser
    {
        [Column(TypeName = "money")]
        public decimal AccountBalance { get; set; }
        [JsonIgnore]
        public List<Category> Categories { get; set; } = new List<Category>();
        [JsonIgnore]
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}

