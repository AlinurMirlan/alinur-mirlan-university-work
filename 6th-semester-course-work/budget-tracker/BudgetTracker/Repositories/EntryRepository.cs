using AutoMapper;
using BudgetTracker.Data;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Repositories
{
    public class EntryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EntryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task InsertCategoryAsync(Category category, EntryName entryType)
        {
            var existentCategory = await dbContext.Categories.Where(c => c.UserId == category.UserId && c.CategoryName == category.CategoryName && c.EntryTypeId == (int)entryType).FirstOrDefaultAsync();
            if (existentCategory is not null)
            {
                throw new InvalidOperationException("Category with the given name already exists.");
            }

            category.EntryTypeId = (int)entryType;
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task InsertEntryAsync(string userId, Entry entry)
        {
            var category = await dbContext.Categories.FindAsync(entry.CategoryId) ?? throw new InvalidOperationException("Category with the given id doesn't exist.");
            int entryTypeId = category.EntryTypeId;
            for (int i = 0; i < entry.Tags.Count; i++)
            {
                var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.TagName == entry.Tags[i].TagName && t.EntryTypeId == entryTypeId);
                if (tag is null)
                {
                    entry.Tags[i].EntryTypeId = entryTypeId;
                    continue;
                }

                entry.Tags[i] = tag;
            }

            var user = await dbContext.Users.FindAsync(userId) ?? throw new InvalidOperationException("User with the given id doesn't exist.");

            if (category.EntryTypeId == (int)EntryName.Income)
            {
                user.AccountBalance += entry.Amount;
            }
            else
            {
                user.AccountBalance -= entry.Amount;
                if (category.BudgetId.HasValue)
                {
                    entry.BudgetId = category.BudgetId;
                }
            }

            entry.TotalAmount = user.AccountBalance;
            await dbContext.Entries.AddAsync(entry);
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<Category> GetCategories(string userId, EntryName entryType)
        {
            return dbContext.Categories.Where(c => c.UserId == userId && c.EntryTypeId == (int)entryType);
        }

        public IEnumerable<Category> GetCategories(string userId, EntryName entryType, int page, int itemsPerPage, out int totalItems)
        {
            var query = dbContext.Categories.Where(c => c.UserId == userId && c.EntryTypeId == (int)entryType);
            totalItems = query.Count();
            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);
        }

        public async Task<Category?> GetCategoryAsync(string userId, string? categoryName)
        {
            if (categoryName is null)
            {
                return null;
            }

            return await dbContext.Categories.Where(c => c.UserId == userId && c.CategoryName == categoryName).FirstOrDefaultAsync();
        }

        public async Task<Category?> GetCategoryAsync(int categoryId)
        {
            return await dbContext.Categories.FindAsync(categoryId);
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

        public async Task<(int totalItems, IEnumerable<Entry> entries)> GetEntriesAsync(OrderBy order, string userId, EntryName entryType, int page, int itemsPerPage)
        {
            if (page <= 0)
            {
                throw new ArgumentException($"{page} is not a valid page number.");
            }

            var query = dbContext.Entries.Include(i => i.Tags).Include(i => i.Category)
                .Where(e => e.Category!.UserId == userId && e.Category!.EntryTypeId == (int)entryType);
            query = order switch
            {
                OrderBy.Amount | OrderBy.Ascending => query.OrderBy(i => i.Amount),
                OrderBy.Amount | OrderBy.Descending => query.OrderByDescending(i => i.Amount),
                OrderBy.Date | OrderBy.Ascending => query.OrderBy(i => i.Date),
                OrderBy.Date | OrderBy.Descending => query.OrderByDescending(i => i.Date),
                _ => throw new ArgumentException($"Invalid combination of the {nameof(order)}."),
            };
            var totalItems = await query.CountAsync();
            return (totalItems, query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage));
        }

        public async Task<(int totalItems, IEnumerable<EntryRecurring> entriesRec)> GetIncomesRecAsync(OrderBy order, string userId, EntryName entryType, int page, int itemsPerPage)
        {
            if (page <= 0)
            {
                throw new ArgumentException($"{page} is not a valid page number.");
            }

            var query = dbContext.EntriesRecurring
                .Include(e => e.Category)
                .Where(e => e.Category!.UserId == userId && e.Category!.EntryTypeId == (int)entryType);
            query = order switch
            {
                OrderBy.Amount | OrderBy.Ascending => query.OrderBy(i => i.Amount),
                OrderBy.Amount | OrderBy.Descending => query.OrderByDescending(i => i.Amount),
                OrderBy.Date | OrderBy.Ascending => query.OrderBy(i => i.StartDate),
                OrderBy.Date | OrderBy.Descending => query.OrderByDescending(i => i.StartDate),
                _ => throw new ArgumentException($"Invalid combination of the {nameof(order)}."),
            };
            var totalItems = await query.CountAsync();
            return (totalItems, query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage));
        }
    }
}
