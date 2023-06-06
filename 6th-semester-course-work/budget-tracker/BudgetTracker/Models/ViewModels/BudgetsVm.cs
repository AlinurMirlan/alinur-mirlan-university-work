using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;

namespace BudgetTracker.Models.ViewModels
{
    public class BudgetsVm
    {
        public BudgetFilterVm Filter { get; set; } = new();
        public BudgetFormVm? NewBudget { get; set; }
        public BudgetSearchFormVm? SearchForm { get; set; }
        public IEnumerable<Budget> Budgets { get; set; } = Enumerable.Empty<Budget>();
        public OrderByDto Order { get; set; } = new OrderByDto();
        public required PagingInfo PagingInfo { get; set; }
    }
}