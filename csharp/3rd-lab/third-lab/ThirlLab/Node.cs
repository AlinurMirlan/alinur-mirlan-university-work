using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdLab
{
    [Serializable]
    public class Node
    {
#pragma warning disable S1104 // Fields should not have public accessibility
        public int data;
        public Node next;
#pragma warning restore S1104 // Fields should not have public accessibility

        public Node(int data, Node next)
        {
            this.data = data;
            this.next = next;
        }

        public override string ToString() => $"{data}, {next}";
    }
}
