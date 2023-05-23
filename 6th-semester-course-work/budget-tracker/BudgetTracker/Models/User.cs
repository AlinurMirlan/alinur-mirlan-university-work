using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class User : IdentityUser
    {
        [Column(TypeName = "money")]
        public decimal AccountBalance { get; set; }
        public List<Budget> Budgets { get; set; } = new List<Budget>();
        public List<Income> Incomes { get; set; } = new List<Income>();
        public List<IncomeRecurring> IncomesRecurring { get; set; } = new List<IncomeRecurring>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<ExpenseRecurring> ExpensesRecurring { get; set; } = new List<ExpenseRecurring>();
    }
}

