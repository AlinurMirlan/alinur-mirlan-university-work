using CookBook.Library.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CookBook.Models
{
    public class IngredientUnitsViewModel
    {
        public Ingredient Ingredient { get; set; } = default!;
        public List<SelectListItem> Units { get; set; } = default!;
        public List<string> ErrorMessages { get; set; } = new();
    }
}
