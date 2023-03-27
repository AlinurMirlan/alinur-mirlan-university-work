using CookBook.Library.Entities;

namespace CookBook.Models
{
    public class ExpenditureViewModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public IList<IngredientExpenditure> Expenditures { get; set; }
    }
}
