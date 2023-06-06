using BudgetTracker.Models.DataObjects;

namespace BudgetTracker.Models.ViewModels
{
    public class EntriesVm
    {
        public OrderByDto Order { get; set; } = new OrderByDto();
        public EntryFormVm? NewEntry { get; set; }
        public EntrySearchFormVm? SearchForm { get; set; }
        public IEnumerable<Entry> Entries { get; set; } = Enumerable.Empty<Entry>();
        public required PagingInfo PagingInfo { get; set; }
        public int? BudgetId { get; set; }
    }
}
