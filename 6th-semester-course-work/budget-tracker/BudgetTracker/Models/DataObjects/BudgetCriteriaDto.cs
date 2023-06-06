namespace BudgetTracker.Models.DataObjects
{
    public class BudgetCriteriaDto
    {
        public int? CategoryId { get; set; }
        public DateTime? DateStart { get; set; } = null;
        public DateTime? DateEnd { get; set; } = null;
    }
}
