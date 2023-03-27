using CookBook.Library.Entities;

namespace CookBook.Models
{
    public class UnitsViewModel
    {
        public IEnumerable<Unit> Units { get; set; } = Enumerable.Empty<Unit>();
        public bool DeletedUnitIsInUse { get; set; }
        public string? DeletedUnitName { get; set; }
        public string? ErrorMessage { get; set; }
        public Unit Unit { get; set; }
    }
}
