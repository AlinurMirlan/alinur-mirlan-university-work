using BudgetTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.DataObjects
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Display(Name = "Category name")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Name of the category must be at least 2 characters long.")]
        public string? CategoryName { get; set; }
        public required string UserId { get; set; }
    }
}
