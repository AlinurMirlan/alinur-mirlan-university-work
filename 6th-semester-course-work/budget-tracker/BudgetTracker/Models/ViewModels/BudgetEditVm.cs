using BudgetTracker.Models.DataObjects;

namespace BudgetTracker.Models.ViewModels
{
    public class BudgetEditVm
    {
        public int ReturnPage { get; set; } = 1;
        public BudgetDto BudgetDto { get; set; } = new();
    }
}
