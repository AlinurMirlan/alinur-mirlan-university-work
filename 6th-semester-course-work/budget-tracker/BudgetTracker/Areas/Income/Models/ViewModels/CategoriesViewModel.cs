namespace BudgetTracker.Areas.Income.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public IncomeCategory? NewCategory { get; set; }
        public IEnumerable<IncomeCategory> Categories { get; set; } = Enumerable.Empty<IncomeCategory>();
    }
}
