using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;

namespace BudgetTracker.Models.ViewModels
{
    public class EntriesRecVm
    {
        public OrderByDto Order { get; set; } = new OrderByDto();
        public EntryRecFormVm? NewEntry { get; set; }
        public EntrySearchFormVm? SearchForm { get; set; }
        public IEnumerable<EntryRecurring> EntriesRec { get; set; } = Enumerable.Empty<EntryRecurring>();
        public required PagingInfo PagingInfo { get; set; }
    }
}
