using System;

namespace Tools.Models
{
	public class Singer
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public class Concert
	{
		public int SingerId { get; set; }
		public int ConcertCount { get; set; }
		public int Year { get; set; }
	}

	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public class Pet
	{
		public string Name { get; set; }
		public int OwnerId { get; set; }
	}

	public class Product
	{
		public string Name { get; set; }
		public int CategoryID { get; set; }
	}

	public struct Category
	{
		public string Name { get; set; }
		public int ID { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class HelpAttribute : Attribute
	{
		public string Topic { get; set; }// Named parameter
		private string Url { get; set; }

		public HelpAttribute(string url)  // Positional parameter
		{
			Url = url;
		}
	}	
}