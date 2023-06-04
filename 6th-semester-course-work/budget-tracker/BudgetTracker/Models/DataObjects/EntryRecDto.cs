using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.DataObjects
{
    public class EntryRecDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Interval must be positive.")]
        public int RecurringInterval { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Please, type in valid amount.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? StringTags { get; set; }
    }
}
