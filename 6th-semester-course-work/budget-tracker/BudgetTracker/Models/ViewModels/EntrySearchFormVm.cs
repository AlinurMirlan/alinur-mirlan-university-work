﻿using BudgetTracker.Models.DataObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Models.ViewModels
{
    public class EntrySearchFormVm
    {
        public EntryName? EntryType { get; set; }
        public EntryCriteriaDto Criteria { get; set; } = new();

        private List<SelectListItem> categories = new();
        public List<SelectListItem> Categories
        {
            get => categories;
            set
            {
                categories = new() { new SelectListItem("", null) };
                categories.AddRange(value);
            } 
        }
    }
}
