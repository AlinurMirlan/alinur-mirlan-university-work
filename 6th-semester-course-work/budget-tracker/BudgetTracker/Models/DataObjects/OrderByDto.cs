using BudgetTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Models.DataObjects
{
    public class OrderByDto
    {
        public int Direction { get; set; } = 2;
        public int Property { get; set; } = 4;
        public List<SelectListItem> DirectionOptions { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem("Descending", 2.ToString()),
            new SelectListItem("Ascending", 1.ToString())
        };
        public List<SelectListItem> PropertyOptions { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem(nameof(Entry.Date), 4.ToString()),
            new SelectListItem(nameof(Entry.Amount), 8.ToString())
        };
    }
}
