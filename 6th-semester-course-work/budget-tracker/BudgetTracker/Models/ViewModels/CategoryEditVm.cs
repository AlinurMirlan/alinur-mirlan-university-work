using BudgetTracker.Models.DataObjects;

namespace BudgetTracker.Models.ViewModels
{
    public class CategoryEditVm
    {
        public int ReturnPage { get; set; } = 1;
        public required CategoryDto CategoryDto { get; set; }
    }
}
