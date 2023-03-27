using CookBook.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Repositories.Abstractions
{
    public interface ITabRepository
    {
        public int AddTab(Tab tab);
        public Tab GetTabDishes(int tabId);
        public IList<Tab> GetTabs(bool orderByDescending);
        public IList<Tab> GetTabsByDate(DateTime orderDate, bool orderByDescending);
        public IList<IngredientExpenditure> GetProvisionExpenditure(DateTime dateStart, DateTime dateEnd);
    }
}
