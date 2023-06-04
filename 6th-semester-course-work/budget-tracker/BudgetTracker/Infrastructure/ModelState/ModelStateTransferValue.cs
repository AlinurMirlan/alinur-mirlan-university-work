namespace BudgetTracker.Infrastructure.ModelState
{
    public class ModelStateTransferValue
    {
        public required string Key { get; set; }
        public required string AttemptedValue { get; set; }
        public required object RawValue { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
    }
}
