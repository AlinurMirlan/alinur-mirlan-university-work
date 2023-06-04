namespace BudgetTracker.Models.ViewModels
{
    [Flags]
    public enum OrderBy
    {
        None = 0,
        Ascending = 1,
        Descending = 2,
        Date = 4,
        Amount = 8
    }
}
