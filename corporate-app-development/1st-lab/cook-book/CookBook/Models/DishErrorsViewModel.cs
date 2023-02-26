using CookBook.Library.Entities;
using System.Data.SqlClient;

namespace CookBook.Models
{
    public class DishErrorsViewModel
    {
        public Dish Dish { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
    }
}
