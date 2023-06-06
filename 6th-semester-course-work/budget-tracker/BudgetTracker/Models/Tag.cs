using BudgetTracker.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BudgetTracker.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public required string TagName { get; set; }
        public int EntryTypeId { get; set; }
        public EntryType? EntryType { get; set; }

        [JsonIgnore]
        public List<Entry> Entries { get; set; } = new List<Entry>();
        [JsonIgnore]
        public List<EntryRecurring> EntriesRecurring { get; set; } = new List<EntryRecurring>();
        public required string UserId { get; set; }
        public User? User { get; set; }
    }
}
