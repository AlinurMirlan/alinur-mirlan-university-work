using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab.entities
{
    internal class Product
    {
        // To deserialize a json object, its properties have to match those of the C# class.
        public string Code { get; set; }
        public string Category { get; set; } 
        public string Country { get; set; }

        public Product(string code, string category, string country)
        {
            Code = code;
            Category = category;
            Country = country;
        }
    }
}
