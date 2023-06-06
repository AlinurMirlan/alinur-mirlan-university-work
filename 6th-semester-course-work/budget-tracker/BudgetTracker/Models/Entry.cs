using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public Category? Category { get; set; }
        public int? EntryRecurringId { get; set; }
        public EntryRecurring? EntryRecurring { get; set; }
        public int? BudgetId { get; set; }
        public Budget? Budget { get; set; }

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
