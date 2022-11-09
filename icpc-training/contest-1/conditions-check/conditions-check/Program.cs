using System;
using System.Linq;

namespace conditions_check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int amount = 0;
            for (int i = 0; i < n; i++)
            {
                long[] numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
                long member = numbers[2] - numbers[1];
                if (numbers[0] == 0)
                {
                    if (member == 0)
                        amount++;
                }
                else if (member >= 0 && member % numbers[0] == 0)
                {
                    amount++;
                }
            }

            Console.WriteLine(amount);
        }
    }
}

// Denote bombs as ones and then 


// Rome -> int. int -> rome
// Rome -> int. a < b ? b - a; a > n ? a + b



/*using System;
using System.Linq;

namespace conditions_check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int amount = 0;
            for (int i = 0; i < n; i++)
            {
                long[] numbers = Console.ReadLine().Split(' ').Select(s => long.Parse(s)).ToArray();
                long member = numbers[2] - numbers[1];
                if (member > 0 && member % numbers[0] == 0)
                {
                    amount++;
                }
            }

            Console.WriteLine(amount);
        }
    }
}*/