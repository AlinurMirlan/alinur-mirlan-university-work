namespace BudgetTracker.Models.ViewModels
{
    public class BudgetVm
    {
        public required Budget Budget { get; set; }
        public required FilterOption FilterOption { get; set; }
    }
}
