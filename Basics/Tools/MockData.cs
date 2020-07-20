using System.Collections.Generic;
using Tools.Models;

namespace Tools
{
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
}
