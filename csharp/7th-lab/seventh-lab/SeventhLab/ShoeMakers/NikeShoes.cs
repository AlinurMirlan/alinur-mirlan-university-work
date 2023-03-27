using SeventhLab.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventhLab.ShoeMakers
{
    public class NikeShoes : IShoeMaker
    {
        private static readonly string[] models =
        {
            "Nike Air Force",
            "Nike Blazer",
            "Nike Gown",
            "Nike Wallows"
        };

        public string Brand => "Nike";

        public string DeliverShoe()
        {
            int durationInMillis = 1000 + Generator.Random.Next(3000);
            Thread.Sleep(durationInMillis);
            string letterCode = Generator.GenerateSerialNumber();
            return $"{models[Generator.Random.Next(models.Length)]}{letterCode}";
        }
    }
}
