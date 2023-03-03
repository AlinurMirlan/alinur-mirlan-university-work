using CookBook.Library.Entities;

namespace CookBook.Models
{
    public class MenuTabViewModel
    {
        public Tab Tab { get; set; }
        public IList<Dish> Dishes { get; set; }
        public bool OrderFinished { get; set; }
    }
}
