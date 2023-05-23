using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public List<ExpenseTag> Tags { get; set; } = new List<ExpenseTag>();
        public ExpenseCategory? Category { get; set; }
        public User? User { get; set; }
    }
}
