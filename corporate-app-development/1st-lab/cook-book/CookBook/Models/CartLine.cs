using CookBook.Library.Entities;

namespace CookBook.Models
{
    public class CartLine
    {
        public Dish Dish { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
