using BudgetTracker.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class EntryRecurring
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public Category? Category { get; set; }
        public int RecurringInterval { get; set; }
        public List<Entry> Entries { get; set; } = new List<Entry>();

        [NotMapped]
        public string StringTags 
        {
            get
            {
                string stringTags = Tags.Aggregate("", (stringTags, tag) => stringTags + $"{tag.TagName}, ");
                if (!string.IsNullOrEmpty(stringTags))
                {
                    stringTags = stringTags[..^2];
                }

                return stringTags;
            }
        }
    }
}
