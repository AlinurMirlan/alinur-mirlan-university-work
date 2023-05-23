namespace BudgetTracker.Models
{
    public class IncomeRecurring
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int IncomeId { get; set; }
        public int RecurringInterval { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public Income? Income { get; set; }
        public User? User { get; set; }
    }
}
