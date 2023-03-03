using CookBook.Library.Entities;

namespace CookBook.Models
{
    public class OrdersViewModel
    {
        public IList<Tab> Tabs { get; set; }
        public bool OrderByDescending { get; set; }
        public DateTime Date { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
