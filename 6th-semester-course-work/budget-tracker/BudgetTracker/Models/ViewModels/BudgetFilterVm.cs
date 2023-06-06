using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Models.ViewModels
{
    public class BudgetFilterVm
    {
        public FilterOption FilterOption { get; set; }
        public List<SelectListItem> FilterOptions { get; set; } = new()
        {
            new(FilterOption.Active.ToString(), FilterOption.Active.ToString()),
            new(FilterOption.All.ToString(), FilterOption.All.ToString()),
        };
    }

    public enum FilterOption
    {
        Active,
        All
    }
}
