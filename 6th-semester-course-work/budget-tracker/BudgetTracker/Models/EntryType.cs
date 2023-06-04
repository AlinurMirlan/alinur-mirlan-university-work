namespace BudgetTracker.Models
{
    public class EntryType
    {
        public int Id { get; set; }
        public EntryName Type { get; set; }
    }

    public enum EntryName
    {
        Income = 1,
        Expense = 2
    }
}
