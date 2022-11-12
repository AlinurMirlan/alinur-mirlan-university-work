namespace lumberjacks_b
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            int A = int.Parse(input[0]);
            long K = long.Parse(input[1]);
            int B = int.Parse(input[2]);
            long M = long.Parse(input[3]);
            long X = long.Parse(input[4]);

            long sum = 0;
            int days = 0;
            for (long i = 1; ; i++)
            {
                days++;
                if (i % K != 0)
                    sum += A;

                if (i % M != 0)
                    sum += B;

                if (sum >= X)
                    break;
            }

            Console.WriteLine(days);
        }
    }
}