using System;
using System.Linq;
using System.Text;

namespace CSharp
{
	public class Problems
	{
		public void Play(string[] args)
		{
			NetworkAndGraph(args[0]);

			string value = Console.ReadLine();
		}

		private void NetworkAndGraph(string name)
		{
			var network = new Network();

			var nodes = new Node[] {
					new Node { Data = "Bob", Index = 0 },
					new Node { Data = "John", Index = 1 },
					new Node { Data = "Mike", Index = 2 },
					new Node { Data = "Jill", Index = 3 },
					new Node { Data = "Leah", Index = 4 },
					new Node { Data = "Emma", Index = 5 },
					new Node { Data = "Shane", Index = 6 },
					new Node { Data = "Liz", Index = 7 },
					new Node { Data = "Allen", Index = 8 },
					new Node { Data = "Lisa", Index = 9 },
				};

			network.Create(nodes.Length);
			network.AddRelation(0, 1);
			network.AddRelation(0, 3);
			network.AddRelation(0, 5);
			network.AddRelation(0, 2);
			network.AddRelation(0, 2);
			network.AddRelation(1, 0);
			network.AddRelation(1, 3);
			network.AddRelation(1, 6);
			network.AddRelation(1, 4);
			network.AddRelation(2, 0);
			network.AddRelation(2, 3);
			network.AddRelation(2, 5);
			network.AddRelation(3, 0);
			network.AddRelation(3, 1);
			network.AddRelation(3, 2);
			network.AddRelation(3, 4);
			network.AddRelation(3, 5);
			network.AddRelation(3, 6);
			network.AddRelation(4, 1);
			network.AddRelation(4, 3);
			network.AddRelation(4, 6);
			network.AddRelation(5, 2);
			network.AddRelation(5, 3);
			network.AddRelation(5, 6);
			network.AddRelation(6, 3);
			network.AddRelation(6, 4);
			network.AddRelation(6, 5);
			network.AddRelation(7, 5);
			network.AddRelation(7, 6);
			network.AddRelation(7, 8);
			network.AddRelation(8, 7);
			network.AddRelation(8, 9);
			network.AddRelation(9, 8);

			var node = nodes.FirstOrDefault(x => x.Data.Equals(name, StringComparison.InvariantCultureIgnoreCase));
			Console.WriteLine(network.GetRelationships(node));
		}

		/// <summary>
		/// CaesarCipher
		/// </summary>
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

		/// <summary>
		/// Pangrams are sentences constructed by using every letter of the alphabet at least once
		/// </summary>
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

	#region GraphProblems 
	class Node
	{
		public int Index { get; set; }
		public string Data { get; set; }
	}

	class Network
	{
		public bool[,] NetworkMatrix { get; set; }
		public int Total { get; set; }

		public void Create(int total)
		{
			Total = total;
			NetworkMatrix = new bool[Total, Total];
		}

		public void AddRelation(int from, int to)
		{
			NetworkMatrix[from, to] = true;
		}

		public int GetRelationships(Node node)
		{
			var total = 0;

			for (int i = 0; i < Total; i++)
				if (NetworkMatrix[node.Index, i])
					total += 1;

			return total;
		}
	}
	#endregion

}