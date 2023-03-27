using SeventhLab.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventhLab.ShoeMakers
{
    public class NewBalanceShoes : IShoeMaker
    {
        private static readonly string[] models =
        {
            "New Balance 990",
            "New Balance 574",
            "New Balance 991",
            "New Balance 993"
        };

        public string Brand => "New Balance";

        public string DeliverShoe()
        {
            int durationInMillis = 1000 + Generator.Random.Next(3000);
            Thread.Sleep(durationInMillis);
            string letterCode = Generator.GenerateSerialNumber();
            return $"{models[Generator.Random.Next(models.Length)]}{letterCode}";
        }
    }
}
