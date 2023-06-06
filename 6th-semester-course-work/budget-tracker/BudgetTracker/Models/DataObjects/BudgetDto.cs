using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.DataObjects
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Please, type in valid amount.")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal Amount { get; set; }
    }
}
