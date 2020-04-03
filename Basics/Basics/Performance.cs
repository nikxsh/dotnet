using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Basics
{
	/// <summary>
	/// - To Test whether List.contains(), Dictinory or HashSet.Contains() is better
	/// </summary>
	public class Performance
	{
		public void Play()
		{
			var list = new List<string>();
			for (int i = 1; i <= 100000; i++)
			{
				list.Add("Name" + i);
			}

			Console.WriteLine("List");
			var watch = Stopwatch.StartNew();
			watch = Stopwatch.StartNew();
			list.Add("Add Test");
			watch.Stop();
			Display(watch.Elapsed, "Add Item");

			Console.WriteLine("");
			Console.WriteLine("Contains");

			Console.WriteLine("");
			list.Contains("Name10000");
			watch.Stop();
			Display(watch.Elapsed, "Name10000");

			watch = Stopwatch.StartNew();
			list.Contains("Name500");
			watch.Stop();
			Display(watch.Elapsed, "Name500");

			watch = Stopwatch.StartNew();
			list.Contains("Name100000");
			watch.Stop();
			Display(watch.Elapsed, "Name100000");

			var dictinory = new Dictionary<int, string>();
			for (int i = 1; i <= 100000; i++)
			{
				dictinory.Add(i, "Name" + i);
			}

			Console.WriteLine("-------------------------------------------------------");
			Console.WriteLine("");
			Console.WriteLine("Dictionary");

			watch = Stopwatch.StartNew();
			dictinory.Add(100001, "Item");
			watch.Stop();
			Display(watch.Elapsed, "Add Item");


			Console.WriteLine("");
			Console.WriteLine("ContainsValue");

			watch = Stopwatch.StartNew();
			dictinory.ContainsValue("Name10000");
			watch.Stop();
			Display(watch.Elapsed, "Name10000");
			watch = Stopwatch.StartNew();
			dictinory.ContainsValue("Name500");
			Display(watch.Elapsed, "Name500");

			watch = Stopwatch.StartNew();
			dictinory.ContainsValue("Name100000");
			watch.Stop();
			Display(watch.Elapsed, "Name100000");

			Console.WriteLine("");
			Console.WriteLine("ContainsKey");

			watch = Stopwatch.StartNew();
			dictinory.ContainsKey(10000);
			watch.Stop();
			Display(watch.Elapsed, "10000");

			watch = Stopwatch.StartNew();
			dictinory.ContainsKey(500);
			watch.Stop();
			Display(watch.Elapsed, "500");

			watch = Stopwatch.StartNew();
			dictinory.ContainsKey(100000);
			watch.Stop();
			Display(watch.Elapsed, "100000");

			Console.WriteLine("-------------------------------------------------------");
			var hashedSet = new HashSet<string>(list);
			Console.WriteLine("");
			Console.WriteLine("HashedSet");
			watch = Stopwatch.StartNew();
			hashedSet.Add("Add Test");
			watch.Stop();
			Display(watch.Elapsed, "Add Item");

			Console.WriteLine("");
			Console.WriteLine("HashedSet.Contains()");
			watch = Stopwatch.StartNew();
			hashedSet.Contains("Name10000");
			watch.Stop();
			Display(watch.Elapsed, "Name10000");

			watch = Stopwatch.StartNew();
			hashedSet.Contains("Name500");
			watch.Stop();
			Display(watch.Elapsed, "Name500");

			watch = Stopwatch.StartNew();
			hashedSet.Contains("Name100000");
			watch.Stop();
			Display(watch.Elapsed, "Name100000");
		}

		private void Display(TimeSpan timespan, string input)
		{
			Console.WriteLine("");
			int value = Convert.ToInt32(timespan.Ticks);

			for (int i = 0; i < value;)
			{
				if (value <= 10)
					i += 7;  //2
				else if (value <= 15)
					i += 10; // 15
				else if (value <= 30)
					i += 12; // 15
				else if (value <= 40)
					i += 14; // 15
				else if (value <= 50)
					i += 16; // 15
				else if (value <= 60)
					i += 18; // 15
				else if (value <= 70)
					i += 20; // 15
				else if (value <= 80)
					i += 22; // 15
				else if (value <= 90)
					i += 25; // 15
				else if (value <= 100)
					i += 28; // 30
				else if (value <= 200)
					i += 35; //50
				else if (value <= 600)
					i += 50; //60
				else if (value <= 800)
					i += 70; //80
				else if (value <= 1000)
					i += 100; //100 
				else if (value <= 2000)
					i += 150; //120 
				else if (value <= 3000)
					i += 175; //140
				else if (value <= 4000)
					i += 200; //160
				else if (value <= 7200)
					i += 225; //180
				else if (value <= 9000)
					i += 250; //200  
				else if (value <= 15000)
					i += 300; //220 
				else if (value <= 30000)
					i += 400; //240
				else if (value <= 49000)
					i += 600; //260
				else
					i += 1000;

				Console.Write("█");
			}

			Console.WriteLine(" : ({0}) {1}", input, timespan.Ticks + " Ticks");
		}
	}

}
