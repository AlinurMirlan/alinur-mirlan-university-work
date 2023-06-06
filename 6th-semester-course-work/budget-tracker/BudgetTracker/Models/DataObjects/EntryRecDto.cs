using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.DataObjects
{
    public class EntryRecDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "Interval must be between 1 and 9999")]
        public int RecurringInterval { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Termination date")]
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Please, type in valid amount.")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? StringTags { get; set; }
    }
}
