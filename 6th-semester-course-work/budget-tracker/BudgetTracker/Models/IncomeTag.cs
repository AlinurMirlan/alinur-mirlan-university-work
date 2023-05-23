using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class IncomeTag
    {
        public int Id { get; set; }
        public required string TagName { get; set; }
        public List<Income> Incomes { get; set; } = new List<Income>();
    }
}
