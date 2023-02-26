using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? DishType { get; set; }
        public List<DishIngredient> Ingredients { get; set; } = new();

        public Dish(string name, decimal price, string dishType)
        {
            Name = name;
            Price = price;
            DishType = dishType;
        }

        public Dish() { }
    }
}
