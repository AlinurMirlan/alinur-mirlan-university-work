using BudgetTracker.Data;
using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Update;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public ValueTask<EntryRecurring?> GetEntryRecAsync(int entryRecId)
        {
            return dbContext.EntriesRecurring.FindAsync(entryRecId);
        }

        public async Task InsertEntryAsync(Entry entry)
        {
            var category = await dbContext.Categories.FindAsync(entry.CategoryId) ?? throw new InvalidOperationException("Category with the given id doesn't exist.");
            int entryTypeId = category.EntryTypeId;
            for (int i = 0; i < entry.Tags.Count; i++)
            {
                var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.UserId == category.UserId && t.TagName == entry.Tags[i].TagName && t.EntryTypeId == entryTypeId);
                if (tag is null)
                {
                    entry.Tags[i].EntryTypeId = entryTypeId;
                    continue;
                }

                entry.Tags[i] = tag;
            }

            var user = await dbContext.Users.FindAsync(category.UserId) ?? throw new InvalidOperationException("User with the given id doesn't exist.");

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

        public async Task InsertBudgetAsync(Budget budget)
        {
            var category = await dbContext.Categories.FindAsync(budget.CategoryId) ?? throw new InvalidOperationException($"There is no {nameof(Category)} with the given id: {budget.CategoryId}");
            if (category.BudgetId is not null)
            {
                throw new InvalidOperationException($"{nameof(Budget)} with the id: {category.Id} alreasy has an active budget.");
            }

            await AddAsync(budget);
            category.BudgetId = budget.Id;
            await dbContext.SaveChangesAsync();
        }

        public async Task EditEntryAsync(Entry updatedEntry)
        {
            var originalEntry = await dbContext.Entries
                .Include(e => e.Tags)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == updatedEntry.Id) ?? throw new InvalidOperationException("There's no user with the given Id");

            originalEntry.Description = updatedEntry.Description;
            var originalAmount = originalEntry.Amount;
            originalEntry.Amount = updatedEntry.Amount;
            if (updatedEntry.Amount != originalAmount)
            {
                var difference = originalAmount - updatedEntry.Amount;
                originalEntry.TotalAmount += difference;
                var entries = dbContext.Entries.Where(e => e.Date > originalEntry.Date && e.Id != originalEntry.Id);
                var user = await dbContext.Users.FindAsync(originalEntry.Category!.UserId) ?? throw new ArgumentException($"Given {nameof(Category)}'s {nameof(Category.UserId)} is not defined.");
                user.AccountBalance += difference;
                foreach (var entry in entries)
                {
                    entry.TotalAmount += difference;
                }
            }

            originalEntry.Tags.Clear();
            foreach (var updatedTag in updatedEntry.Tags)
            {
                var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.UserId == originalEntry.Category!.UserId && t.TagName == updatedTag.TagName && t.EntryTypeId == originalEntry.Category!.EntryTypeId);
                if (tag is null)
                {
                    tag = updatedTag;
                    tag.EntryTypeId = originalEntry.Category!.EntryTypeId;
                }

                originalEntry.Tags.Add(tag);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task EditEntryRecurringAsync(EntryRecurring updatedEntryRec)
        {
            var originalEntryRec = await dbContext.EntriesRecurring
                .Include(e => e.Tags)
                        .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == updatedEntryRec.Id) ?? throw new InvalidOperationException("There's no user with the given Id");

            originalEntryRec.Amount = updatedEntryRec.Amount;
            originalEntryRec.Description = updatedEntryRec.Description;
            originalEntryRec.Tags.Clear();
            foreach (var updatedTag in updatedEntryRec.Tags)
            {
                var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.UserId == originalEntryRec.Category!.UserId && t.TagName == updatedTag.TagName && t.EntryTypeId == originalEntryRec.Category!.EntryTypeId);
                if (tag is null)
                {
                    tag = updatedTag;
                    tag.EntryTypeId = originalEntryRec.Category!.EntryTypeId;
                }

                originalEntryRec.Tags.Add(tag);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task EditBudgetAsync(Budget updatedBudget)
        {
            var originalBudget = await dbContext.Budgets.FindAsync(updatedBudget.Id) ?? throw new InvalidOperationException($"There's no {nameof(Budget)} with the given Id: {updatedBudget.Id}");
            originalBudget.Amount = updatedBudget.Amount;
            await dbContext.SaveChangesAsync();
        }

        public async Task EditCategoryAsync(Category updatedCategory)
        {
            var originalCategory = dbContext.Categories.FirstOrDefault(c => c.Id == updatedCategory.Id) ?? throw new InvalidOperationException($"There is no {nameof(Category)} with the given id: {nameof(Category.Id)}");
            var existentCategory = dbContext.Categories.FirstOrDefault(c => c.UserId == originalCategory.UserId && c.CategoryName == updatedCategory.CategoryName && c.Id != originalCategory.Id);
            if (existentCategory is not null)
            {
                throw new InvalidOperationException($"There is already a {nameof(Category)} with the given id.");
            }

            originalCategory.CategoryName = updatedCategory.CategoryName;
            await dbContext.SaveChangesAsync();
        }

        public Task SaveChangesAsync() => dbContext.SaveChangesAsync();

        public async Task InsertEntryRecAsync(EntryRecurring entryRec)
        {
            var category = await dbContext.Categories.FindAsync(entryRec.CategoryId) ?? throw new InvalidOperationException("Category with the given id doesn't exist.");
            int entryTypeId = category.EntryTypeId;
            for (int i = 0; i < entryRec.Tags.Count; i++)
            {
                var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.TagName == entryRec.Tags[i].TagName && t.EntryTypeId == entryTypeId);
                if (tag is null)
                {
                    entryRec.Tags[i].EntryTypeId = entryTypeId;
                    continue;
                }

                entryRec.Tags[i] = tag;
            }

            await dbContext.EntriesRecurring.AddAsync(entryRec);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await dbContext.Categories.Where(c => c.Id == categoryId).Include(c => c.Entries).FirstOrDefaultAsync() ?? throw new InvalidOperationException();
            for (int i = 0; i < category.Entries.Count; i++)
            {
                await DeleteEntryAsync(category.Entries[i]);
            }

            dbContext.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<Category> GetCategories(string userId, EntryName entryType)
        {
            return dbContext.Categories.Where(c => c.UserId == userId && c.EntryTypeId == (int)entryType);
        }

        public IEnumerable<Category> GetCategories(string userId, string? categoryName, EntryName entryType, int page, int itemsPerPage, out int totalItems)
        {
            var query = dbContext.Categories.Where(c => c.UserId == userId && c.EntryTypeId == (int)entryType);
            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(c => c.CategoryName.StartsWith(categoryName));
            }

            query = query.Select(c => new Category()
            {
                CategoryName = c.CategoryName,
                BudgetId = c.BudgetId,
                EntryTypeId = c.EntryTypeId,
                Id = c.Id,
                UserId = c.UserId,
                TotalExpenses = c.Entries.Sum(e => e.Amount)
            });
            totalItems = query.Count();
            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);
        }

        public async Task<Category?> GetCategoryAsync(int categoryId)
        {
            return await dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<(int totalItems, IEnumerable<Budget> budgets)> GetBudgetsAsync(OrderBy order, FilterOption filterOption, BudgetCriteriaDto? searchCriteria, string userId, int page, int itemsPerPage)
        {
            var query = filterOption switch
            {
                FilterOption.Active => dbContext.Budgets.Where(b => b.Category!.UserId == userId && b.Category.BudgetId == b.Id),
                FilterOption.All => (from b in dbContext.Budgets
                                     join c in dbContext.Categories.Where(c => c.UserId == userId)
                                    on b.CategoryId equals c.Id into grouping
                                    from g in grouping.DefaultIfEmpty()
                                    select new Budget()
                                    {
                                        Id = b.Id,
                                        Amount = b.Amount,
                                        Category = g,
                                        CategoryId = b.CategoryId,
                                        Date = b.Date,
                                        Entries = b.Entries
                                    }),
                _ => throw new ArgumentException($"Invalid value for the {nameof(FilterOption)} enum: {(int)filterOption}."),
            };;
            if (searchCriteria is not null)
            {
                if (searchCriteria.CategoryId.HasValue && searchCriteria.CategoryId > 0)
                {
                    query = query.Where(b => b.CategoryId == searchCriteria.CategoryId);
                }
                if (searchCriteria.DateStart.HasValue)
                {
                    query = query.Where(b => b.Date >= searchCriteria.DateStart);
                }
                if (searchCriteria.DateEnd.HasValue)
                {
                    query = query.Where(b => b.Date < searchCriteria.DateEnd);
                }
            }

            query = order switch
            {
                OrderBy.Amount | OrderBy.Ascending => query.OrderBy(i => i.Amount),
                OrderBy.Amount | OrderBy.Descending => query.OrderByDescending(i => i.Amount),
                OrderBy.Date | OrderBy.Ascending => query.OrderBy(i => i.Date),
                OrderBy.Date | OrderBy.Descending => query.OrderByDescending(i => i.Date),
                _ => throw new ArgumentException($"Invalid combination of the {nameof(order)}."),
            };
            var totalItems = await query.CountAsync();
            if (filterOption == FilterOption.All)
            {
                return (totalItems, query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage));
            }
            return (totalItems, query
                .Include(b => b.Entries)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage));
        }
        public Task<(int totalItems, IEnumerable<Entry> entries)> GetEntriesAsync(OrderBy order, EntryCriteriaDto? searchCriteria, string userId, EntryName entryType, int page, int itemsPerPage)
        {
            if (page <= 0)
            {
                throw new ArgumentException($"{page} is not a valid page number.");
            }

            var query = dbContext.Entries.Include(i => i.Tags).Include(i => i.Category)
                .Where(e => e.Category!.UserId == userId && e.Category!.EntryTypeId == (int)entryType);
            return GetEntries(order, searchCriteria, query, page, itemsPerPage);
        }

        public Task<(int totalItems, IEnumerable<Entry> entries)> GetEntriesAsync(OrderBy order, EntryCriteriaDto? searchCriteria, string userId, int? budgetId, EntryName entryType, int page, int itemsPerPage)
        {
            if (page <= 0)
            {
                throw new ArgumentException($"{page} is not a valid page number.");
            }

            var query = dbContext.Entries.Include(i => i.Tags).Include(i => i.Category)
                .Where(e => e.Category!.UserId == userId && e.Category!.EntryTypeId == (int)entryType);
            if (budgetId.HasValue)
            {
                query = query.Where(e => e.BudgetId != null && e.BudgetId == budgetId);
            }

            return GetEntries(order, searchCriteria, query, page, itemsPerPage);
        }

        public Task<(int totalItems, IEnumerable<Entry> entries)> GetEntriesAsync(OrderBy order, EntryCriteriaDto? searchCriteria, string userId, int page, int itemsPerPage)
        {
            if (page <= 0)
            {
                throw new ArgumentException($"{page} is not a valid page number.");
            }

            var query = dbContext.Entries.Include(i => i.Tags).Include(i => i.Category)
                .Where(e => e.Category!.UserId == userId);
            return GetEntries(order, searchCriteria, query, page, itemsPerPage);
        }

        public async Task<(int totalItems, IEnumerable<EntryRecurring> entriesRec)> GetEntriesRecAsync(OrderBy order, EntryCriteriaDto? searchCriteria, string userId, EntryName entryType, int page, int itemsPerPage)
        {
            if (page <= 0)
            {
                throw new ArgumentException($"{page} is not a valid page number.");
            }

            var query = dbContext.EntriesRecurring
                .Include(e => e.Category)
                .Include(e => e.Tags)
                .Where(e => e.Category!.UserId == userId && e.Category!.EntryTypeId == (int)entryType);
            if (searchCriteria is not null)
            {
                if (searchCriteria.CategoryId.HasValue && searchCriteria.CategoryId > 0)
                {
                    query = query.Where(e => e.CategoryId == searchCriteria.CategoryId);
                }
                if (searchCriteria.DateStart.HasValue)
                {
                    query = query.Where(e => e.StartDate > searchCriteria.DateStart);
                }
                if (searchCriteria.DateEnd.HasValue)
                {
                    query = query.Where(e => e.StartDate < searchCriteria.DateStart);
                }
                if (!string.IsNullOrEmpty(searchCriteria.StringTags))
                {
                    var tags = searchCriteria.StringTags.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var tag in tags)
                    {
                        var trimmedTag = tag.Trim();
                        query = query.Where(e => e.Tags.Any(e => e.TagName == trimmedTag));
                    }
                }
                if (!string.IsNullOrEmpty(searchCriteria.Description))
                {
                    query = query.Where(e => e.Description!.StartsWith(searchCriteria.Description));
                }
            }

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
        
        public async Task DeleteEntryAsync(Entry entryToDelete)
        {
            if (entryToDelete.Category is null)
            {
                throw new ArgumentException($"Given {nameof(Entry)}'s {nameof(Category)} instance is not defined.");
            }

            EntryName entryType = (EntryName)entryToDelete.Category.EntryTypeId;
            if (!Enum.IsDefined(entryType))
            {
                throw new InvalidOperationException($"{entryToDelete.Category.EntryTypeId} is not a valid value for {nameof(EntryName)} enum.");
            }

            await UpdateAccountBalanceAndEntriesTotalAmounts(entryToDelete, entryType);
            dbContext.Entries.Entry(entryToDelete).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }

        private async Task UpdateAccountBalanceAndEntriesTotalAmounts(Entry relativeEntry, EntryName entryType)
        {
            var user = await dbContext.Users.FindAsync(relativeEntry.Category!.UserId) ?? throw new ArgumentException($"Given {nameof(Category)}'s {nameof(Category.UserId)} is not defined.");

            var entries = dbContext.Entries.Where(e => e.Date > relativeEntry.Date && e.Id != relativeEntry.Id);
            bool wasEntryIncome = entryType == EntryName.Income;
            user.AccountBalance = wasEntryIncome ? user.AccountBalance - relativeEntry.Amount : user.AccountBalance + relativeEntry.Amount;
            foreach (var entry in entries)
            {
                entry.TotalAmount = wasEntryIncome ? entry.TotalAmount - relativeEntry.Amount : entry.TotalAmount + relativeEntry.Amount;
            }
        }

        public async Task CloseCategoryBudget(int categoryId)
        {
            var category = await dbContext.Categories.FindAsync(categoryId) ?? throw new InvalidOperationException($"There is no {nameof(Category)} with the given id: {categoryId}");

            category.BudgetId = null;
            await dbContext.SaveChangesAsync();
        }

        private static async Task<(int totalItems, IEnumerable<Entry> entries)> GetEntries(OrderBy order, EntryCriteriaDto? searchCriteria, IQueryable<Entry> query, int page, int itemsPerPage)
        {
            if (searchCriteria is not null)
            {
                if (searchCriteria.CategoryId.HasValue && searchCriteria.CategoryId > 0)
                {
                    query = query.Where(e => e.CategoryId == searchCriteria.CategoryId);
                }
                if (searchCriteria.DateStart.HasValue)
                {
                    query = query.Where(e => e.Date >= searchCriteria.DateStart);
                }
                if (searchCriteria.DateEnd.HasValue)
                {
                    query = query.Where(e => e.Date < searchCriteria.DateEnd);
                }
                if (!string.IsNullOrEmpty(searchCriteria.StringTags))
                {
                    var tags = searchCriteria.StringTags.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var tag in tags)
                    {
                        var trimmedTag = tag.Trim();
                        query = query.Where(e => e.Tags.Any(e => e.TagName == trimmedTag));
                    }
                }
                if (!string.IsNullOrEmpty(searchCriteria.Description))
                {
                    query = query.Where(e => e.Description!.StartsWith(searchCriteria.Description));
                }
            }

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
    }
}
