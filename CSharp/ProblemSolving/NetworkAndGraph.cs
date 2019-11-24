using System;
using System.Linq;

namespace CSharp.ProblemSolving
{
	public class NetworkAndGraph
	{
		public void Run(string name)
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
	}

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
}
