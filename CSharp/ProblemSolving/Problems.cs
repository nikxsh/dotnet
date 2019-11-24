using System;
using System.Linq;
using System.Text;

namespace CSharp
{
	public class Problems
	{
		public void Play(string[] args)
		{
			//new ProblemSolving.NetworkAndGraph().Run(args[0]);

			string value = Console.ReadLine();
		}

		private void CaesarCipher(int length, string input, int rotateBy)
		{
			//65(A)-90(Z) | 97(a)-122(z) | total: 26
			var charArray = input.ToCharArray();
			var output = new StringBuilder();

			int rotationIndex = rotateBy % 26;

			foreach (char item in charArray)
			{
				var cipherAscii = item + rotationIndex;

				if (item >= 65 && item <= 90)
				{
					if (cipherAscii > 90)
						output.Append((char)(64 + (cipherAscii - 90)));
					else
						output.Append((char)cipherAscii);
				}
				else if (item >= 97 && item <= 122)
				{
					if (cipherAscii > 122)
						output.Append((char)(96 + (cipherAscii - 122)));
					else
						output.Append((char)cipherAscii);
				}
				else
					output.Append(item);
			}

			Console.WriteLine("{0}", output.ToString());
		}

		private void Pangrams(string input)
		{
			var alphabateBucket = new bool[26];
			var searchArray = input.ToCharArray();

			foreach (var item in searchArray)
			{
				if (item >= 97 && item <= 122)
				{
					var aplhaIndex = 25 - (122 - item);
					var bucketValue = alphabateBucket.ElementAt(aplhaIndex);
					if (bucketValue)
						continue;
					else
						alphabateBucket[aplhaIndex] = true;
				}
			}

			var isPangram = alphabateBucket.Aggregate((first, next) => next && first);

			if (isPangram)
				Console.WriteLine("pangram");
			else
				Console.WriteLine("not pangram");

		}

		private void StringWithinString(string input, string key)
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
	}
}