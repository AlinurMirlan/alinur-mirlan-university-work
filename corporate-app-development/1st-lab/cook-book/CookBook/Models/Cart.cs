using CookBook.Library.Entities;

namespace CookBook.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public int ItemsCount { get; set; } = 0;
        public void AddOneItem(Dish dish)
        {
            CartLine? viewModel = Lines.FirstOrDefault(c => c.Dish.Id == dish.Id);
            ItemsCount++;

            if (viewModel is null)
            {
                Lines.Add(new CartLine
                {
                    Dish = dish,
                    Quantity = 1
                });
            }
            else
            {
                viewModel.Quantity++;
            }
        }

        public void RemoveLine(Dish dish) => Lines.RemoveAll(p => p.Dish.Id == dish.Id);

        public decimal ComputeTotalValue() => Lines.Sum(p => p.Dish.Price * p.Quantity);

        public void Clear() => Lines.Clear();
    }
}
