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
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DishTypeId { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();

        public Dish(string name, decimal price, int dishTypeId)
        {
            Name = name;
            Price = price;
            DishTypeId = dishTypeId;
        }
    }
}
