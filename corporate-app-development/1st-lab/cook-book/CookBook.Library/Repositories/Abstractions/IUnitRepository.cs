using CookBook.Library.Entities;
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
        public IEnumerable<Unit> GetAll();
        public string? GetUnit(int id);
        public void DeleteUnit(int id);
        public void EditUnitName(int id, string name);
        public int AddUnit(string unitName);
    }
}
