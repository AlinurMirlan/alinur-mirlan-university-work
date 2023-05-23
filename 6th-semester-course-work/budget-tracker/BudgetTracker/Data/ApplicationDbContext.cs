using BudgetTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseRecurring> ExpensesRecurring { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenseTag> ExpenseTags { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeRecurring> IncomesRecurring { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<IncomeTag> IncomeTags { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ExpenseRecurring>()
                .HasOne(e => e.Expense)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<IncomeRecurring>()
                .HasOne(e => e.Income)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
