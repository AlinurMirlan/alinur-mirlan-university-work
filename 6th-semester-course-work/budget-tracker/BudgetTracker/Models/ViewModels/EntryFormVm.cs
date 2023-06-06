using BudgetTracker.Models.DataObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Models.ViewModels
{
    public class EntryFormVm
    {
        public EntryDto EntryDto { get; set; } = new();
        public required EntryName EntryType { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
