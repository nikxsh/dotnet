using System;
using System.Collections.Generic;
using System.Text;

namespace Analytic
{
	public class Matching
	{
		public void StringInString(string input, string key)
		{
			var charArray = key.ToCharArray();
			var searchArray = input.ToCharArray();

			int n = 0;

			for (int i = 0; i < searchArray.Length;)
			{
				if (charArray.Length == n + 1)
					break;

				if (searchArray[i] == charArray[n])
				{
					while (searchArray[i] == charArray[n])
					{
						i++;
						if (i >= searchArray.Length || charArray[n + 1] == searchArray[i])
							break;
					}

					n++;
					continue;
				}

				i++;
			}

			if (charArray.Length == n + 1)
				Console.WriteLine("YES");
			else
				Console.WriteLine("NO");
		}

		/// <summary>
		/// 1. Print starting index if strig matched
		/// 2. Find string with * characted i.e. 
		///    Case 1:
		///       input: ouabcd12po
		///       tofind: abcd
		///       output: 3
		///    Case 2:
		///       input: xyzab123cd
		///       tofind: ab*cd
		///       output: 4    
		///    Case 3:
		///       input: xyzab123cdxyab123cdxy
		///       tofind: ab*cd
		///       output: 4  
		/// </summary>
		public void PatternMatching1(string input, string key)
		{
			var keyIndex = 0;
			var inputIndex = 0;

			var foundLength = 0;
			var startIndex = -1;

			while (inputIndex < input.Length && keyIndex < key.Length)
			{
				if (input[inputIndex] == key[keyIndex])
				{
					if (foundLength == 0)
						startIndex = inputIndex;

					if ((keyIndex + 1) < key.Length && key[keyIndex + 1] == '*')
					{
						keyIndex += 2;
						do
						{
							inputIndex++;
							if (input[inputIndex] == key[keyIndex]) break;
						} while (inputIndex < input.Length);
					}
					else
					{
						inputIndex++;
						keyIndex++;
					}
					foundLength ++;
					continue;
				}
				else if(foundLength == key.Length)
				{
					break;
				}
				else
				{
					foundLength = 0;
					startIndex = -1;
					keyIndex = 0;
					inputIndex++;
					continue;
				}
			}

			Console.WriteLine($"{key} in {input} first at {startIndex + 1}");

		}
	}
}
