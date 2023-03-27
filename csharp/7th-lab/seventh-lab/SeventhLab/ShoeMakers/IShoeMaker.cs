using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventhLab.ShoeMakers
{
    public interface IShoeMaker
    {
        string Brand { get; }
        public string DeliverShoe();
    }
}
