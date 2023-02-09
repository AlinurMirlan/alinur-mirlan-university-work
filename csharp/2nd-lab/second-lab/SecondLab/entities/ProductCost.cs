using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab.entities
{
    internal class ProductCost
    {
        public string ProductCode { get; set; }
        public string ShopName { get; set; }
        public int Cost { get; set; }

        public ProductCost(string productCode, string shopName, int cost)
        {
            ProductCode = productCode;
            ShopName = shopName;
            Cost = cost;
        }
    }
}
