using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int UnitId { get; set; }

        public Ingredient(string name, decimal price, int unitId)
        {
            Name = name;
            Price = price;
            UnitId = unitId;
        }
    }
}
