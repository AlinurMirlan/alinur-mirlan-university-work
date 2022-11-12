using System;

namespace approximate_search_e
{
	public class Program
	{
		static void Main(string[] args)
		{
			string[] initialValues = Console.ReadLine()!.Split(' ');
			int N = int.Parse(initialValues[0]);
			int K = int.Parse(initialValues[1]);
			string[] numOfTowns = Console.ReadLine()!.Split(' ');
			string[] numOfRequests = Console.ReadLine()!.Split(' ');
			int[] town = new int[N];
			int[] request = new int[K];
			for (int i = 0; i < N; i++)
				town[i] = int.Parse(numOfTowns[i]);

			for (int i = 0; i < K; i++)
				request[i] = int.Parse(numOfRequests[i]);

			bool isOutOfBounds = false;
			for (int i = 0; i < K; i++)
			{
				int left = 0, right = N - 1;
				int middleIndex = (left + right) / 2;
				do
				{
					if (town[middleIndex] == request[i])
						break;
					else if (town[N - 1] < request[i])
					{
						middleIndex = N - 1;
						isOutOfBounds = true;
						Console.WriteLine(town[middleIndex]);
						break;
					}
					else if (town[middleIndex] > request[i])
						right = middleIndex;
					else
						left = middleIndex;

					middleIndex = (left + right) / 2;
				} 
				while (right > left + 1);

				if (!isOutOfBounds)
				{
					if (town[middleIndex + 1] - request[i] < request[i] - town[middleIndex])
						Console.WriteLine(town[middleIndex + 1]);
					else
						Console.WriteLine(town[middleIndex]);
				}

				isOutOfBounds = false;
			}
		}
	}
}