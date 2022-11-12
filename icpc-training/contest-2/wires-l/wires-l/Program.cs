namespace wires_l
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            int wireCount = int.Parse(input[0]);
            int segmentCount = int.Parse(input[1]);
            int[] wires = new int[wireCount];
            for (int i = 0; i < wireCount; i++)
                wires[i] = int.Parse(Console.ReadLine());

            Array.Sort(wires);
        }
    }
}