//Copyright (C) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Linq;
using DotNetDemos.CSharpExamples.DynamicLinq;
using System;
using System.Text;
using System.Xml.Linq;

namespace DotNetDemos.CSharpExamples
{

	public class LinqExamples
	{
		public void Examples()
		{
			var example = new JoinExamples();
			//var example = new DynamicLinqExample();
			//var example = new AggregateExample();
			//var example = new SelectManyExmaple();
			//var example = new GeneralExamples();
		}
	}

	public class GeneralExamples
	{
		public GeneralExamples()
		{
			BishopsMoves();
		}

		#region Bishop Moves
		private void BishopsMoves()
		{
			//var positions = GetBoardPostionUsingLambda();
			//positions = positions
			//            .Where(p => BishopCanMoveTo(p, "c6"))
			//            .ToList();

			var postitions = BishopCanMoveToUsingLinq("c6");
		}

		private IEnumerable<string> GetBoardPostionUsingLambda()
		{
			return Enumerable.Range('a', 8)
								  .SelectMany(
													  x => Enumerable.Range('1', 8),
													  (f, r) => string.Format("{0}{1}", (char)f, (char)r)
												 );
		}

		private bool BishopCanMoveTo(string startPos, string targetPos)
		{
			var dx = Math.Abs(startPos[0] - targetPos[0]);
			var dy = Math.Abs(startPos[1] - targetPos[1]);
			return dx == dy && dx != 0;
		}

		private IEnumerable<string> BishopCanMoveToUsingLinq(string intialPosition)
		{
			return (from row in Enumerable.Range('a', 8)
					  from col in Enumerable.Range('1', 8)
					  let dx = Math.Abs(row - intialPosition[0])
					  let dy = Math.Abs(col - intialPosition[1])
					  where dx == dy && dx != 0
					  select string.Format("{0}{1}", (char)row, (char)col)
					  );
		}
		#endregion
	}

	public class JoinExamples
	{
		public JoinExamples()
		{
			//GroupJoin();
			//LeftJoin();
			NonEquiJoin();
		}

		private void GroupJoin()
		{
			var dummyData = new DummyData();

			var query = from person in dummyData.People
							join pet in dummyData.Pets
							on person equals pet.Owner into Gj
							select new
							{
								OwnerName = person.FirstName + " " + person.LastName,
								Pet = Gj
							};

			var xmlQuery = new XElement(
													"PetOwners",
													from person in dummyData.People
													join pet in dummyData.Pets
													on person equals pet.Owner into Gj
													select new XElement(
														  "Person",
														  new XAttribute("FirstName", person.FirstName),
														  new XAttribute("LastName", person.LastName),
														  from subpet in Gj
														  select new XElement("PetName", subpet.Name)
														  )
												);
		}

		private void LeftJoin()
		{
			var dummyData = new DummyData();

			var query = from person in dummyData.People
							join pet in dummyData.Pets
							on person equals pet.Owner into Gj
							from subpet in Gj.DefaultIfEmpty()
							select new
							{
								OwnerName = person.FirstName + " " + person.LastName,
								PetName = subpet == null ? string.Empty : subpet.Name
							};

		}

		private void NonEquiJoin()
		{
			var dummyData = new DummyData();
			var query = from p in dummyData.Products
							let catIds = from c in dummyData.Categories
											 select c.ID
							where catIds.Contains(p.CategoryID)
							select new
							{
								Product = p.Name,
								CategoryId = p.CategoryID
							};
		}
	}

	public class SelectManyExmaple
	{
		public SelectManyExmaple()
		{
			//Example1();
			Example2();
		}

		private void Example1()
		{
			//set up some data for our example
			var tuple1 = new { Name = "Tuple1", Values = new int[] { 1, 2, 3 } };
			var tuple2 = new { Name = "Tuple2", Values = new int[] { 4, 5, 6 } };
			var tuple3 = new { Name = "Tuple3", Values = new int[] { 7, 8, 9 } };

			//put the tuples into a collection
			var tuples = new[] { tuple1, tuple2, tuple3 };

			//"tupleValues" is an IEnumerable<IEnumerable<int>> that contains { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }
			var tupleValues = tuples.Select(t => t.Values);

			//"tupleSelectManyValues" is an IEnumerable<int> that contains { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
			var tupleSelectManyValues = tuples.SelectMany(t => t.Values);
		}

		private void Example2()
		{
			IEnumerable<Singer> singers = GetSingers();
			IEnumerable<Concert> concerts = GetConcerts();

			var singerInConcert = singers.SelectMany(s => concerts
																		 .Where(c => c.SingerId == s.Id)
																		 .Select(sc => new
																		 {
																			 Year = sc.Year,
																			 Count = sc.ConcertCount,
																			 Name = String.Format("{0} {1}", s.FirstName, s.LastName)
																		 })
																  );
		}

		private class Singer
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		private class Concert
		{
			public int SingerId { get; set; }
			public int ConcertCount { get; set; }
			public int Year { get; set; }
		}

		private static IEnumerable<Singer> GetSingers()
		{
			return new List<Singer>()
				{
					 new Singer(){Id = 1, FirstName = "Freddie", LastName = "Mercury"}
					 , new Singer(){Id = 2, FirstName = "Elvis", LastName = "Presley"}
					 , new Singer(){Id = 3, FirstName = "Chuck", LastName = "Berry"}
					 , new Singer(){Id = 4, FirstName = "Ray", LastName = "Charles"}
					 , new Singer(){Id = 5, FirstName = "David", LastName = "Bowie"}
				};
		}

		private static IEnumerable<Concert> GetConcerts()
		{
			return new List<Concert>()
				{
					 new Concert(){SingerId = 1, ConcertCount = 53, Year = 1979}
					 , new Concert(){SingerId = 1, ConcertCount = 74, Year = 1980}
					 , new Concert(){SingerId = 1, ConcertCount = 38, Year = 1981}
					 , new Concert(){SingerId = 2, ConcertCount = 43, Year = 1970}
					 , new Concert(){SingerId = 2, ConcertCount = 64, Year = 1968}
					 , new Concert(){SingerId = 3, ConcertCount = 32, Year = 1960}
					 , new Concert(){SingerId = 3, ConcertCount = 51, Year = 1961}
					 , new Concert(){SingerId = 3, ConcertCount = 95, Year = 1962}
					 , new Concert(){SingerId = 4, ConcertCount = 42, Year = 1950}
					 , new Concert(){SingerId = 4, ConcertCount = 12, Year = 1951}
					 , new Concert(){SingerId = 5, ConcertCount = 53, Year = 1983}
				};
		}
	}

	public class AggregateExample
	{
		private string albums = "2:54,3:48,4:51,3:32,6:15,4:08,5:17,3:13,4:16,3:55,4:53,5:35,4:24";

		public AggregateExample()
		{
			var Sum = albums
							.Split(',')
							.Select(t => "0:" + t)
							.Select(t => TimeSpan.Parse(t))
							.Aggregate((t1, t2) => t1 + t2);

			var multipliers = new[] { 10, 20, 30, 40 };
			var multiplied = multipliers
								 .Aggregate(5, (a, b) => a * b);

			//Output 1200000 ((((5*10)*20)*30)*40)

			var chars = new[] { "a", "b", "c", "", "e", "", "g" };
			var csvFormat = chars.Aggregate(new StringBuilder(), (a, b) =>
			{
				if (a.Length > 0 && b.Length != 0)
					a.Append(",");
				a.Append(b);
				return a;
			});

			var numbers = new[] { 1, 10, 4, 8, 3, 2, 9 };
			var average = numbers.Aggregate(0, (a, b) => a + b, total => total / numbers.Count());
		}
	}

	public class DynamicLinqExample
	{
		public class DummyData
		{
			public int StudentId { get; set; }
			public string StudentName { get; set; }
			public Grade StudentGrade { get; set; }
		}

		public enum Grade
		{
			A,
			B,
			C,
			D
		}

		public DynamicLinqExample()
		{
			var query = SampleData.Where(x => x.StudentId == 1).AsQueryable();
			string parameterQuery = string.Empty;
			parameterQuery = parameterQuery + string.Format("StudentGrade == @0");
			var result = query.Where(parameterQuery, Grade.B);

		}

		public List<DummyData> SampleData
		{
			get
			{
				return new List<DummyData>
					 {
						  new DummyData
						  {
								 StudentId = 1,
								 StudentGrade = Grade.A,
								 StudentName = "Vikas"
						  },
						  new DummyData
						  {
								 StudentId = 1,
								 StudentGrade = Grade.B,
								 StudentName = "Bipin"
						  },
						  new DummyData
						  {
								 StudentId = 3,
								 StudentGrade = Grade.C,
								 StudentName = "Ravi"
						  },
						  new DummyData
						  {
								 StudentId = 4,
								 StudentGrade = Grade.D,
								 StudentName = "Nikhilesh"
						  }
					 };
			}
		}
	}

	public class DummyData
	{
		public class Person
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		public class Pet
		{
			public string Name { get; set; }
			public Person Owner { get; set; }
		}

		Person rakesh = new Person { FirstName = "Rakesh", LastName = "Sharma" };
		Person akshay = new Person { FirstName = "Akshay", LastName = "Kumar" };
		Person pm = new Person { FirstName = "Narendra", LastName = "Modi" };
		Person nsa = new Person { FirstName = "Ajit", LastName = "Doval" };

		public List<Person> People
		{
			get
			{
				return new List<Person>
					 {
						  rakesh,
						  akshay,
						  pm,
						  nsa
					 };
			}
		}

		public List<Pet> Pets
		{
			get
			{
				return new List<Pet>
					 {
						  new Pet { Name = "Mamta", Owner = akshay },
						  new Pet { Name = "Arvind", Owner = pm },
						  new Pet { Name = "Salman", Owner = akshay },
						  new Pet { Name = "Rahul", Owner = pm },
						  new Pet { Name = "Hafij", Owner = nsa },
						  new Pet { Name = "Yechuri", Owner = pm }
					};
			}
		}

		public class Product
		{
			public string Name { get; set; }
			public int CategoryID { get; set; }
		}

		public class Category
		{
			public string Name { get; set; }
			public int ID { get; set; }
		}

		public List<Category> Categories
		{
			get
			{
				return new List<Category>()
					 {
						  new Category(){Name="Beverages", ID=001},
						  new Category(){ Name="Condiments", ID=002},
						  new Category(){ Name="Vegetables", ID=003},
					 };
			}
		}

		public List<Product> Products
		{
			get
			{
				return new List<Product>
					{
						new Product{Name="Tea",  CategoryID=001},
						new Product{Name="Mustard", CategoryID=002},
						new Product{Name="Pickles", CategoryID=002},
						new Product{Name="Carrots", CategoryID=003},
						new Product{Name="Bok Choy", CategoryID=003},
						new Product{Name="Peaches", CategoryID=005},
						new Product{Name="Melons", CategoryID=005},
						new Product{Name="Ice Cream", CategoryID=007},
						new Product{Name="Mackerel", CategoryID=012},
					 };
			}
		}
	}
}