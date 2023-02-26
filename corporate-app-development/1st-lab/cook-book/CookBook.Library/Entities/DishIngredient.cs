using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Entities
{
    public class DishIngredient : Ingredient
    {
        public double Amount { get; set; }

        public DishIngredient(string name, decimal price, string unit, double amount) : base(name, price, unit)
        {
            Amount = amount;
        }

        public DishIngredient() { }
    }
}
