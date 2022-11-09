namespace very_easy_task;
public class Program
{
    static void Main(string[] args)
    {
        HashSet<int> set = new();
        LinkedList<int> list = new();
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (!set.Contains(number))
                {
                    list.AddLast(number);
                    set.Add(number);
                }

                continue;
            }

            break;
        }

        foreach (int node in list)
        {
            Console.WriteLine(node);
        }
    }
}