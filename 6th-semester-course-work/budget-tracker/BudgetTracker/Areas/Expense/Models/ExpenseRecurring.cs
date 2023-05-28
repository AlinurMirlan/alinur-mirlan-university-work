using BudgetTracker.Models;

namespace BudgetTracker.Areas.Expense.Models
{
    public class ExpenseRecurring
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int ExpenseId { get; set; }
        public int RecurringInterval { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public Expense? Expense { get; set; }
        public User? User { get; set; }
    }
}
