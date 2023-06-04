using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;

namespace BudgetTracker.Models.ViewModels
{
    public class CategoriesVm
    {
        public CategoryDto? NewCategory { get; set; }
        public CategoryCriteriaDto? CategoryCriteria { get; set; }
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
        public required PagingInfo PagingInfo { get; set; }
    }
}
