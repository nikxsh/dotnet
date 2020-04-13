using System;
using Tools.Models;

namespace Tools
{
	class Program
	{
		static void Main(string[] args)
		{
			new WineUtility().FetchWines();
			Console.ReadKey();
		}
	}
}
