using BudgetTracker.Data;
using BudgetTracker.Models;

namespace BudgetTracker.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public IncomeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IncomeCategory InsertCategory(IncomeCategory category)
        {
            return dbContext.IncomeCategories.Add(category).Entity;
        }
        
        public IEnumerable<IncomeCategory> GetCategories()
        {
            return dbContext.IncomeCategories;
        }
    }
}
