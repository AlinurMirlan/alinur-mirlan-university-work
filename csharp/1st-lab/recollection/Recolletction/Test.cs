using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recolletction
{
    internal class Test
    {
        public string? Subject { get; set; }
        public bool IsCredited { get; set; }

        public override string ToString() => $"Subject: {Subject ?? "None"} | Credited: {IsCredited}";
    }
}
