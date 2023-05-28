using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Areas.Income.Models
{
    public class IncomeCategory
    {
        public int Id { get; set; }

        [Display(Name = "Category name")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Name of the category must be at least 2 characters long.")]
        public required string CategoryName { get; set; }
    }
}
