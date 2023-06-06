using BudgetTracker.Models.DataObjects;

namespace BudgetTracker.Models.ViewModels
{
    public class EntryEditVm
    {
        public int? ReturnBudgetId { get; set; }
        public int ReturnPage { get; set; } = 1;
        public EntryDto EntryDto { get; set; } = new();
        public required EntryName? EntryType { get; set; }
    }
}
