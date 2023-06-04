using BudgetTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public int EntryTypeId { get; set; }
        public EntryType? EntryType { get; set; }
        public required string UserId { get; set; }
        public User? User { get; set; }
        public List<Entry> Entries { get; set; } = new List<Entry>();
        public int? BudgetId { get; set; }
        public Budget? Budget { get; set; }
    }
}
