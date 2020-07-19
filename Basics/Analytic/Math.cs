using System;
using System.Collections.Generic;
using System.Linq;

namespace Analytic
{
	public class Math
	{

		public Math()
		{
			Problem2(new List<int> { 1, 1, 1, 2 });
			Problem2(new List<int> { 1, 2, 1, 1, 2 });
			Problem2(new List<int> { 100, 2, 4, 2, 3, 2, 100 });
		}

		/// <summary>
		/// 1. Take Max value from input array
		/// 2. Divide by 2 and get floor value i.e. 3/2 = 1.5 ~ 2
		/// 3. Replace this value with previous
		/// 4. Repeat this process K times
		/// 5. Return sum of all elements
		/// </summary>
		public void Problem1(int[] input, int k)
		{
			for (int i = 0; i < k; i++)
			{
				var min = input.Max();
				var divideBy2 = decimal.Ceiling((min / 2m));
				var minIndex = Array.IndexOf(input, min);
				input[minIndex] = (int)divideBy2;
			}
			Console.WriteLine(input.Sum());
		}

		public void Problem2(List<int> input)
		{
			List<int> subSequence = new List<int>();
			for (int i = 0; i < input.Count; i++)
			{
				IEnumerable<int> nextRange = input.Skip(i + 1);
				foreach (var item in nextRange)
				{
					if (input[i] == item)
					{
						subSequence.Add(input[i]);
						break;
					}
				}
			}

			foreach (var item in input)
				Console.Write($"{item} ");

			Console.Write(" >> ");

			foreach (var item in subSequence)
				Console.Write($"{item} ");

			Console.WriteLine();
		}
	}
}
