using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Unit { get; set; }

        public Ingredient(string name, decimal price, string unit)
        {
            Name = name;
            Price = price;
            Unit = unit;
        }

        public Ingredient() { }
    }
}
