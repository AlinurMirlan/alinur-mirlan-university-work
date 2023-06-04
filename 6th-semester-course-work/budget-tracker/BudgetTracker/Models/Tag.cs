using BudgetTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Models
{
    [Index(nameof(TagName), IsUnique = true)]
    public class Tag
    {
        public int Id { get; set; }
        public required string TagName { get; set; }
        public int EntryTypeId { get; set; }
        public EntryType? EntryType { get; set; }
        public List<Entry> Entries { get; set; } = new List<Entry>();
        public List<EntryRecurring> EntriesRecurring { get; set; } = new List<EntryRecurring>();
        public required string UserId { get; set; }
        public User? User { get; set; }
    }
}
