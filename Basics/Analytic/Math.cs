using System;
using System.Linq;

namespace Analytic
{
	public class Math
	{
		/// <summary>
		/// 1. Take min value from input array
		/// 2. Divide by 2 and get floor value i.e. 3/2 = 1.5 ~ 2
		/// 3. Replace this value with previous
		/// 4. Repeat this process K times
		/// 5. Return sum of all elements
		/// </summary>
		public void Problem1(int[] input, int k)
		{
			for(int i = 0; i < k; i++)
			{
				var min = input.Min();
				var divideBy2 = decimal.Round((min / 2m), MidpointRounding.AwayFromZero);
				var minIndex = Array.IndexOf(input, min);
				input[minIndex] = (int) divideBy2;
			}
			Console.WriteLine(input.Sum());
		}
	}
}
