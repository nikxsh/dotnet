using System;

namespace Analytic
{
    public class Matching
	{
		public Matching()
        {

			PatternMatching1("xyzabxyz", "abc"); //Yes
			PatternMatching1("xyzabcxyz", "ab"); //Yes
			PatternMatching1("xyzaxyz", "ab"); //Yes
			PatternMatching1("axyzxyz", "ab"); //Yes
			PatternMatching1("xyzxyza", "ab"); //Yes
			PatternMatching1("xyzxyz", "abc"); //No
			PatternMatching1("xyzbcxyz", "abc"); //No
			PatternMatching1("xyzcxyz", "abc"); //No
			PatternMatching1("bxyzxyzc", "abc"); //No


			//PatternMatching2("xyz", "abcd"); //0
			//PatternMatching2("abcd", "abcd"); //1
			//PatternMatching2("12abcdop", "abcd"); //2
			//PatternMatching2("ab123cd", "*abcd"); //0
			//PatternMatching2("ab123cd", "abcd*"); //0
			//PatternMatching2("ab123cd", "a*bcd"); //0
			//PatternMatching2("xab123cdyzxab987cd", "ab*cd"); //2
			//PatternMatching2("xyzab123cd", "ab*cd"); //4
			//PatternMatching2("xyzab123cxyzab678cd", "ab*cd"); //13
		}

		/// <summary>
		/// 1. Print Yes or No for given conditions
		/// 2. If given key's starting matches with input		
		///   Case 1: 
		///		Input: "China's communist party is expansionist and monstrous"
		///		Key: "pa"
		///		Output: Yes
		///   Case 2: 
		///		Input: "China's communist party is expansionist and monstrous"
		///		Key: "par"
		///		Output: Yes		
		///	  Case 3: 
		///		Input: "China's communist arty is expansionist and monstrous"
		///		Key: "par"
		///		Output: No
		/// </summary>
		public void PatternMatching1(string input, string key)
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
		public void PatternMatching2(string input, string key)
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
