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
        public List<Dish> Dishes { get; } = new List<Dish>();
        public decimal Cost
        {
            get
            {
                decimal total = 0;
                foreach (Dish dish in Dishes)
                    total += dish.Price;

                return total - (total * .05m);
            }
        }

        public Tab(int tabNumber, DateTime orderDate, params Dish[] dishes)
        {
            TabNumber = tabNumber;
            OrderDate = orderDate;
            Dishes.AddRange(dishes);
        }
    }
}
