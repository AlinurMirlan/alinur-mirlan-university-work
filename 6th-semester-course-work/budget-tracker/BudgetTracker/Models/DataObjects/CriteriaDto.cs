namespace BudgetTracker.Models.DataObjects
{
    public class CriteriaDto
    {
        public int CategoryId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? Description { get; set; }
        public string? StringTags { get; set; }
    }
}
