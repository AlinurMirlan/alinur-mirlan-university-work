using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Entities
{
    public class Tab
    {
        public int Id { get; set; }
        public int TabNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<TabDish> TabDishes { get; set; } = new List<TabDish>();
        public int DishesCount => TabDishes.Aggregate(0, (total, next) => total + next.Quantity);
        public decimal Cost => TabDishes.Sum(p => p.Price * p.Quantity);

        public Tab(int tabNumber, DateTime orderDate, params TabDish[] dishes)
        {
            TabNumber = tabNumber;
            OrderDate = orderDate;
            TabDishes.AddRange(dishes);
        }

        public Tab(int tabNumber, DateTime orderDate)
        {
            TabNumber = tabNumber;
            OrderDate = orderDate;
        }
        public Tab() { }

        public void AddDish(Dish dish)
        {
            TabDish? tabDish = TabDishes.FirstOrDefault(c => c.Id == dish.Id);

            if (tabDish is null)
            {
                tabDish = new(dish);
                TabDishes.Add(tabDish);
            }
            else
            {
                tabDish.Quantity++;
            }
        }
        public void OrderUpDish(int dishId)
        {
            TabDish? tabDish = TabDishes.FirstOrDefault(c => c.Id == dishId);
            if (tabDish is not null)
                tabDish.Quantity++;
        }

        public void OrderDownDish(int dishId)
        {
            TabDish? tabDish = TabDishes.FirstOrDefault(c => c.Id == dishId);
            if (tabDish is null)
                return;

            if (tabDish.Quantity > 1) tabDish.Quantity--;
            else
            {
                TabDishes.Remove(tabDish);
            }
        }
    }
}
