using BudgetTracker.Models;

namespace BudgetTracker.Repositories
{
    public interface IIncomeRepository
    {
        IEnumerable<IncomeCategory> GetCategories();
        IncomeCategory InsertCategory(IncomeCategory category);
    }
}
