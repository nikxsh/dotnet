using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CSharp
{
	public static class Utility
	{
		public static Stopwatch Watch = new Stopwatch();
		public static Random random = new Random();
		public static double EllapsedTime(double milliseconds) => TimeSpan.FromMilliseconds(milliseconds).TotalSeconds;
		public static IEnumerable<int> Numbers(int max) => Enumerable.Range(1, max);

		public static IEnumerable<Employee> GetEmployeeMockArray(int length = 10)
		{
			var employees = new List<Employee>();
			foreach (var item in Enumerable.Range(1, length))
			{
				var employee = new Employee
				{
					EmployeeId = item,
					Rank = GetUniqueRandomValue(employees.Select(x => x.Rank), 1, 50),
					Name = GetUniqueRandomValue(employees.Select(x => x.Name), 0, MockData.Names.Length),
					Salary = random.Next(30000, 90000)
				};
				employees.Add(employee);
			}
			return employees;
		}

		public static void PrintployeeMockArray(IEnumerable<Employee> employees)
		{
			Console.WriteLine($"------------------------");
			foreach (var item in employees)
				Console.WriteLine($"{item.Rank,3}:{item.Name,-20}:{item.Salary,3:C}");
			Console.WriteLine();
		}

		public static string GetUniqueRandomValue(IEnumerable<string> existingValues, int start, int max)
		{
			var randomName = GetRandomName(start, max);
			while (existingValues.Any(x => x.Equals(randomName, StringComparison.InvariantCultureIgnoreCase)))
				randomName = GetRandomName(start, max);
			return randomName;
		}

		public static int GetUniqueRandomValue(IEnumerable<int> existingValues, int start, int max)
		{
			var randomNumber = random.Next(start, max);
			while (existingValues.Any(x => x == randomNumber))
				randomNumber = random.Next(start, max);
			return randomNumber;
		}

		private static readonly Func<int, int, string> GetRandomName = (start, max) => MockData.Names[random.Next(start, max)];

		public static void WriteToFile(string line)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\nikxsh.log"))
			{
				file.WriteLine(line);
			}
		}

		public static T Cast<T>(object referenceObject)
		{
			Type objectType = referenceObject.GetType();
			Type target = typeof(T);
			var instance = Activator.CreateInstance(target, false);
			var memberInfos = from source in target.GetMembers().ToList()
									where source.MemberType == MemberTypes.Property
									select source;
			List<MemberInfo> members = memberInfos.Where(memberInfo => memberInfos.Select(c => c.Name)
				.ToList().Contains(memberInfo.Name)).ToList();
			PropertyInfo propertyInfo;
			object value;
			foreach (var memberInfo in members)
			{
				propertyInfo = typeof(T).GetProperty(memberInfo.Name);
				value = referenceObject.GetType().GetProperty(memberInfo.Name).GetValue(referenceObject, null);
				propertyInfo.SetValue(instance, value, null);
			}
			return (T)instance;
		}
	}

	public class MockData
	{
		public static string[] Names = {
			"Gaurav Shukla",
			"Aadish Nigam",
			"Aayushman Magar",
			"Ujwal Mander",
			"Koushtubh Gagrani",
			"Diya Rai",
			"Mukul Mehra",
			"David Doctor",
			"Veena Mane",
			"Riddhi Gour",
			"Alaknanda Raju",
			"Tejaswani Devan",
			"Sapna Mand",
			"Raju Palla",
			"Charu Mody",
			"Azhar Parekh",
			"Labeen Sidhu",
			"Mitesh Raj",
			"Amrit Uppal",
			"Radhe Pandey",
			"Akhil Viswanathan",
			"Rajesh D’Alia",
			"Abbas Saha",
			"Mohit Shenoy",
			"Emran Khare",
			"Amrita Varma",
			"Chitranjan Divan",
			"Kalpit Dixit",
			"Veena Suresh",
			"Drishti Bhatnagar",
			"Javed Chopra",
			"Abhishek Khare",
			"Kasturba Dugal",
			"Leela Pillay",
			"Akhil Sant",
			"Rupesh Sarin",
			"Gowri Bassi",
			"Arpit Kapoor",
			"Sheetal Soman",
			"Kailash Puri",
			"Parminder Khanna",
			"Zahir Rout",
			"Preet Saraf",
			"Biren Vohra",
			"Fardeen Dhingra",
			"Malik Usman",
			"Samir Loyal",
			"Obaid Gola",
			"Kirti Prakash",
			"Wahid Manda",
			"Christian Kling",
			"Aurelio Morissette",
			"Velda Sawayn",
			"Geoffrey Yost",
			"Aryanna Waters",
			"Maximillia Feil",
			"Camren Ratke",
			"Reyes Mertz",
			"Yasmeen Doyle",
			"Trudie Reichel",
			"Enrico Corwin",
			"Casimer Grant",
			"Melody McKenzie",
			"Gilda Von",
			"Isabell Lakin",
			"Weston Stracke",
			"Clementine Langosh",
			"Alfonzo Gutmann",
			"Floy Ankunding",
			"Dante Huels",
			"Precious Feeney",
			"Delores Kuhic",
			"Adriana Hermann",
			"Lacey Lynch",
			"Yessenia Kassulke",
			"Ara Rosenbaum",
			"Luisa Stroman",
			"Roselyn Wilkinson",
			"Enrique O'Conner ",
			"Makenna Romaguera",
			"Demario Kutch",
			"Josefa Rohan",
			"Ashlee Hettinger",
			"Derek Leuschke",
			"Eloisa Spencer",
			"Tony Green",
			"Oren Koss",
			"Myah Wintheiser",
			"Malvina Ortiz",
			"Savanna Oberbrunner",
			"Jany Jakubowski",
			"Carolyn Reichel",
			"Kurt Pfeffer",
			"Ashleigh Schimmel",
			"Johann Ullrich",
			"Margie Keebler",
			"Araceli Pouros",
			"Ewald Rodriguez",
			"Eileen Cummerata",
			"Marquise Marquardt"
		};

		public static IEnumerable<Singer> Singers => new List<Singer>
		{
			new Singer() { Id = 1, FirstName = "Freddie", LastName = "Mercury"},
			new Singer() { Id = 2, FirstName = "Elvis", LastName = "Presley"},
			new Singer() { Id = 3, FirstName = "Chuck", LastName = "Berry"},
			new Singer() { Id = 4, FirstName = "Ray", LastName = "Charles"},
			new Singer() { Id = 5, FirstName = "David", LastName = "Bowie"}
		};

		public static IEnumerable<Concert> Concerts => new List<Concert>
		{
			new Concert() { SingerId = 1, ConcertCount = 53, Year = 1979},
			new Concert() { SingerId = 1, ConcertCount = 74, Year = 1980},
			new Concert() { SingerId = 1, ConcertCount = 38, Year = 1981},
			new Concert() { SingerId = 2, ConcertCount = 43, Year = 1970},
			new Concert() { SingerId = 2, ConcertCount = 64, Year = 1968},
			new Concert() { SingerId = 3, ConcertCount = 32, Year = 1960},
			new Concert() { SingerId = 3, ConcertCount = 51, Year = 1961},
			new Concert() { SingerId = 3, ConcertCount = 95, Year = 1962},
			new Concert() { SingerId = 4, ConcertCount = 42, Year = 1950},
			new Concert() { SingerId = 4, ConcertCount = 12, Year = 1951},
			new Concert() { SingerId = 5, ConcertCount = 53, Year = 1983}
		};

		public static IEnumerable<Person> People => new List<Person>
		{
			new Person { Id = 1, FirstName = "Narendra", LastName = "Modi" }, //0
			new Person { Id = 2, FirstName = "Amit", LastName = "Shah" },
			new Person { Id = 3, FirstName = "Ajit", LastName = "Doval" },
			new Person { Id = 4, FirstName = "Rakesh", LastName = "Sharma" },
			new Person { Id = 5, FirstName = "Akshay", LastName = "Kumar" } //4
		};

		public static IEnumerable<Pet> Pets => new List<Pet>
		{
			new Pet { Name = "Mamta", OwnerId = 2},
			new Pet { Name = "Arvind", OwnerId = 2 },
			new Pet { Name = "Salman", OwnerId = 3 },
			new Pet { Name = "Rahul", OwnerId = 1 },
			new Pet { Name = "Hafij", OwnerId = 4 },
			new Pet { Name = "Yechuri", OwnerId = 2 },
			new Pet { Name = "Imran", OwnerId = 1 }
		};

		public static IEnumerable<Category> Categories => new List<Category>
		{
			new Category() { Name="Beverages", ID=101},
			new Category() { Name="Condiments", ID=102},
			new Category() { Name="Vegetables", ID=103}
		};

		public static IEnumerable<Product> Products => new List<Product>
		{
			new Product { Name="Tea",CategoryID=101},
			new Product { Name="Mustard", CategoryID=102},
			new Product { Name="Pickles", CategoryID=102},
			new Product { Name="Carrots", CategoryID=103},
			new Product { Name="Bok Choy", CategoryID=103},
			new Product { Name="Peaches", CategoryID=105},
			new Product { Name="Melons", CategoryID=105},
			new Product { Name="Ice Cream", CategoryID=107},
			new Product { Name="Mackerel", CategoryID=112}
		};

		public static IEnumerable<Student> Students => new List<Student>
		{
			new Student
			{
				StudentId = 1,
				StudentGrade = Grade.A,
				Name = "Vikas"
			},
			new Student
			{
				StudentId = 1,
				StudentGrade = Grade.B,
				Name = "Bipin"
			},
			new Student
			{
				StudentId = 3,
				StudentGrade = Grade.C,
				Name = "Ravi"
			},
			new Student
			{
				StudentId = 4,
				StudentGrade = Grade.D,
				Name = "Nikhilesh"
			}
		};
	}

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

	interface IStudent
	{
		void Display();
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

	[Help("http://www.dummy.com/100", Topic = "Class A")]
	public class Student : IStudent, IEquatable<Student>, IComparable<Student>
	{
		public int StudentId { get; set; }
		public string Name { get; set; }
		public int Marks { get; set; }
		public Grade StudentGrade { get; set; }
		public int CompareChoice { get; set; }
		public string[] AssignedSubject { get; set; }

		public string this[int index]
		{
			get => AssignedSubject[index];
			set => AssignedSubject[index] = value;
		}

		public void Display()
		{
			Console.WriteLine($"{StudentId,3}:{Name,-20}:{Marks,3:C}:{StudentGrade.ToString(),3:C}");
		}

		//The Deconstruct method of a class, structure, or interface also allows you to retrieve and deconstruct 
		//a specific set of data from an object
		public void Deconstruct(out int studentId, out string name, out int marks, out Grade grade)
		{
			studentId = StudentId;
			name = Name;
			marks = Marks;
			grade = StudentGrade;
		}

		public bool Equals(Student other)
		{
			if (other == null)
				return false;
			else if (Marks == other.Marks && StudentId == other.StudentId)
				return true;
			else
				return false;
		}

		public int CompareTo(Student other)
		{
			if (CompareChoice == 0)
			{
				if (StudentId == other.StudentId)
					return 0;
				else if (StudentId > other.StudentId)
					return 1;
				else
					return -1;
			}
			else
			{
				if (Marks == other.Marks)
					return 0;
				else if (Marks > other.Marks)
					return 1;
				else
					return -1;
			}
		}
	}

	public enum Grade
	{
		A,
		B,
		C,
		D
	}

	public class Employee
	{
		public int EmployeeId { get; set; }
		public string Name { get; set; }
		public int Rank { get; set; }
		public decimal Salary { get; set; }
	}
}