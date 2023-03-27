using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Entities
{
    public class IngredientExpenditure
    {
        public string Ingredient { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public double Amount { get; set; }
        public double Cost { get; set; }
    }
}
