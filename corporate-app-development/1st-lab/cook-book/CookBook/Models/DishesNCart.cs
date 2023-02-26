using CookBook.Library.Entities;

namespace CookBook.Models
{
    public class DishesNCart
    {
        public Cart Cart { get; set; }
        public IList<Dish> Dishes { get; set; }
        public bool OrderFinished { get; set; }
    }
}
