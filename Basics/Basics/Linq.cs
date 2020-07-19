using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CSharp
{
    /// <summary>
    /// Language Integrated Query (LINQ)
    /// </summary>
    public class Linq
	{
		public void Play()
		{
			InnerJoin();
			LeftJoin();
			GroupJoin();
			GroupBy();
			GroupInnerJoin();
			NonEquiJoin();
			GetBoardPosition();
			BishopCanMoveTo("c1");
			SelectMany();
			AggregateExample();
		}

		private void InnerJoin()
		{
			var query = from person in MockData.People
							join pet in MockData.Pets
							on person.Id equals pet.OwnerId
							select new
							{
								OwnerName = $"{person.FirstName} {person.LastName}",
								PetName = pet.Name
							};

		}

		private void LeftJoin()
		{
			var query = from person in MockData.People
							join pet in MockData.Pets
							on person.Id equals pet.OwnerId into GroupJoin
							from pet in GroupJoin.DefaultIfEmpty()
							select new
							{
								OwnerName = person.FirstName + " " + person.LastName,
								PetName = pet?.Name ?? string.Empty
							};
		}

		private void GroupJoin()
		{
			var query = from person in MockData.People
							join pet in MockData.Pets
							on person.Id equals pet.OwnerId into Groups
							select new
							{
								OwnerName = $"{person.FirstName} {person.LastName}",
								Pets = Groups
							};
		}
		
		private void GroupBy()
		{
			var query = from concert in MockData.Concerts
							group concert by concert.SingerId;
		}

		private void GroupInnerJoin()
		{
			var query = from person in MockData.People
							join pet in MockData.Pets
							on person.Id equals pet.OwnerId into Groups
							orderby person.FirstName
							select new
							{
								OwnerName = $"{person.FirstName} {person.LastName}",
								Pets = from pet in Groups
										 orderby pet.Name
										 select pet
							};
		}

		private void NonEquiJoin()
		{
			var query = from p in MockData.Products
							let catIds = from c in MockData.Categories
											 select c.ID
							where catIds.Contains(p.CategoryID)
							select new
							{
								Product = p.Name,
								CategoryId = p.CategoryID
							};
		}

		private void GroupJoinToXML()
		{
			var xmlQuery = new XElement(
						"PetOwners",
						from person in MockData.People
						join pet in MockData.Pets
							on person.Id equals pet.OwnerId into Groups
						select new XElement(
								"Person",
								new XAttribute("FirstName", person.FirstName),
								new XAttribute("LastName", person.LastName),
								from Pets in Groups
								select new XElement("PetName", Pets.Name)
							)
			);
		}

		private void GetBoardPosition()
		{
			var result = Enumerable.Range('a', 8)
								.SelectMany(
									x => Enumerable.Range('1', 8),
									(f, r) => string.Format("{0}{1}", (char)f, (char)r)
								);
		}

		private void BishopCanMoveTo(string intialPosition)
		{
			var result = (from row in Enumerable.Range('a', 8)
					  from col in Enumerable.Range('1', 8)
					  let dx = Math.Abs(row - intialPosition[0])
					  let dy = Math.Abs(col - intialPosition[1])
					  where dx == dy && dx != 0
					  select string.Format("{0}{1}", (char)row, (char)col)
					);
		}

		private void SelectMany()
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

			var singerInConcert = MockData.Singers.SelectMany(s => MockData.Concerts
										.Where(c => c.SingerId == s.Id)
										.Select(sc => new
										{
											sc.Year,
											sc.ConcertCount,
											Name = $"{s.FirstName} {s.LastName}"
										})
								);
		}

		public void AggregateExample()
		{
			var albums = "2:54,3:48,4:51,3:32,6:15,4:08,5:17,3:13,4:16,3:55,4:53,5:35,4:24";
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
}