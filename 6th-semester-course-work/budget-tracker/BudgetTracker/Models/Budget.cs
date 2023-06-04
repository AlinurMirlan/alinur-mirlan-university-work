using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public List<Entry> Entries { get; set; } = new List<Entry>();
        public Category? Category { get; set; }
    }
}
