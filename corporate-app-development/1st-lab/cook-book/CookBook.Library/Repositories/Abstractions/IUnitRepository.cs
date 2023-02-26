using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Repositories.Abstractions
{
    public interface IUnitRepository
    {
        public IEnumerable<string> GetAllUnits();
        public string? GetUnit(int id);
    }
}
