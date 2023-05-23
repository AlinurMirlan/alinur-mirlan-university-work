using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int CategoryId { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public ExpenseCategory? Category { get; set; }
        public User? User { get; set; }
    }
}
