using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab.entities
{
    internal class Purchase
    {
        public int ConsumerCode { get; set; }
        public string ProductCode { get; set; }
        public string ShopName { get; set; }

        public Purchase(int consumerCode, string productCode, string shopName)
        {
            ConsumerCode = consumerCode;
            ProductCode = productCode;
            ShopName = shopName;
        }
    }
}
