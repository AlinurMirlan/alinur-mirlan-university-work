using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Entities
{
    public class TabDish : Dish
    {
        public int Quantity { get; set; }

        public TabDish(string name, decimal price, string dishType) : base(name, price, dishType) { }
        public TabDish(string name, decimal price, string dishType, int quantity) : base(name, price, dishType)
        {
            Quantity = quantity;
        }
        public TabDish(Dish dish)
        {
            Id = dish.Id;
            Name = dish.Name;
            Price = dish.Price;
            DishType = dish.DishType;
            Quantity = 1;
            Ingredients = dish.Ingredients;
        }
        public TabDish() { }
    }
}
