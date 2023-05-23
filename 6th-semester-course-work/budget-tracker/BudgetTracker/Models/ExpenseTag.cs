﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
    public class ExpenseTag
    {
        public int Id { get; set; }
        public required string TagName { get; set; }
        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}