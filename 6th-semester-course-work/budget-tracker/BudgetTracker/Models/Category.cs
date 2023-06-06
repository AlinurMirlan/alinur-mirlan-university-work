using BudgetTracker.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public int EntryTypeId { get; set; }
        public EntryType? EntryType { get; set; }
        public required string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public List<Entry> Entries { get; set; } = new List<Entry>();
        public int? BudgetId { get; set; }
        public Budget? Budget { get; set; }

        [NotMapped]
        public decimal TotalExpenses { get; set; } = 0;
    }
}
