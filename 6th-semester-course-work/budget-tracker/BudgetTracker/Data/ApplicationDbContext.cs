using BudgetTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BudgetTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryRecurring> EntriesRecurring { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<EntryType> EntryTypes { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EntryType>().Property(t => t.Type).HasConversion<string>();
            builder.Entity<Budget>()
                .HasOne(b => b.Category)
                .WithOne(c => c.Budget)
                .HasForeignKey<Category>(c => c.BudgetId);
        }
    }
}
    