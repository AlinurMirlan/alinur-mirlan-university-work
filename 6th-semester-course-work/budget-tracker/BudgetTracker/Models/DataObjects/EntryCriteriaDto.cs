namespace BudgetTracker.Models.DataObjects
{
    public class EntryCriteriaDto
    {
        public int? CategoryId { get; set; }
        public DateTime? DateStart { get; set; } = null;
        public DateTime? DateEnd { get; set; } = null;
        public string? Description { get; set; }
        public string? StringTags { get; set; }
    }
}
