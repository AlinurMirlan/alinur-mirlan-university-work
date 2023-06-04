namespace BudgetTracker.Models.ViewModels
{
    public class EntryVm
    {
        public Entry Entry { get; set; }
        public char Sign { get; set; }

        public EntryVm(Entry entry, char sign)
        {
            Entry = entry;
            Sign = sign;
        }
    }
}
