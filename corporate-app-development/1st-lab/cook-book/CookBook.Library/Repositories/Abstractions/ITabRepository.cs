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
        public int AddTab(Tab tab, Dish[] dishes);
        public Tab GetTab(int tabId);
    }
}
