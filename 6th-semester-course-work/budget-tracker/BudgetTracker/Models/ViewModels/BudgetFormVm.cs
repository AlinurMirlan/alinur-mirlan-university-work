using BudgetTracker.Models.DataObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Models.ViewModels
{
    public class BudgetFormVm
    {
        public BudgetDto BudgetDto { get; set; } = new();
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
