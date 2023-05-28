using BudgetTracker.Areas.Income.Models;
using BudgetTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Areas.Income.Repositories
{
    public class IncomeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public IncomeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IncomeCategory> InsertCategoryAsync(IncomeCategory category)
        {
            return (await dbContext.IncomeCategories.AddAsync(category)).Entity;
        }

        public IEnumerable<IncomeCategory> GetCategories()
        {
            return dbContext.IncomeCategories;
        }

        public async Task<IncomeCategory?> GetCategoryAsync(string? categoryName)
        {
            if (categoryName is null)
            {
                return null;
            }

            return await dbContext.IncomeCategories.Where(c => c.CategoryName == categoryName).FirstOrDefaultAsync();
        }

        public async Task<IncomeCategory?> GetCategoryAsync(int categoryId)
        {
            return await dbContext.IncomeCategories.FindAsync(categoryId);
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
