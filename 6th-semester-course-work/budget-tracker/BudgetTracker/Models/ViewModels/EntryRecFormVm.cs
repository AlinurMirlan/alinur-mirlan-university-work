using BudgetTracker.Models.DataObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Models.ViewModels
{
    public class EntryRecFormVm
    {
        public EntryRecDto EntryRecDto { get; set; } = new();
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public required EntryName EntryType { get; set; }
    }
}
