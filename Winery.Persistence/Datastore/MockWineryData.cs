using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	class WineDetail
	{
		public string id { get; set; }
		public string winery_full { get; set; }
		public string wine_full { get; set; }
		public string vintage { get; set; }
		public string taster_initials { get; set; }
		public string color { get; set; }
		public string country { get; set; }
		public string region { get; set; }
		public int score { get; set; }
		public decimal price { get; set; }
		public string issue_date { get; set; }
		public int top100_year { get; set; }
		public int top100_rank { get; set; }
		public string note { get; set; }
	}

	class WineryEqualityComparer : IEqualityComparer<Winery>
	{
		public bool Equals([AllowNull] Winery x, [AllowNull] Winery y)
		{
			return x.Name == y.Name;
		}

		public int GetHashCode([DisallowNull] Winery obj)
		{
			return obj.Name.GetHashCode();
		}
	}

	class WineEqualityComparer : IEqualityComparer<Wine>
	{
		public bool Equals([AllowNull] Wine x, [AllowNull] Wine y)
		{
			return x.Name == y.Name;
		}

		public int GetHashCode([DisallowNull] Wine obj)
		{
			return obj.Name.GetHashCode();
		}
	}

	public class MockWineryData
	{
		public static void LoadJsonData()
		{
			var jsonData = new FileHandler().ReadJsonData<WineDetail[]>(@"D:/wines.json");

			var wineries = jsonData.Select(x => new Winery
			{
				Id = Guid.NewGuid(),
				Name = x.winery_full,
				Region = x.region,
				Country = x.country
			}).ToList();

			Wineries.AddRange(wineries.Distinct(new WineryEqualityComparer()));

			var wines = jsonData.Select(x => new Wine
			{
				Id = Guid.NewGuid(),
				WineryId = wineries.Where(y => y.Name.Equals(x.winery_full, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().Id,
				Name = x.wine_full,
				Color = (WineColor)Enum.Parse(typeof(WineColor), x.color.ToUpper(), true),
				Score = x.score,
				Price = x.price,
				Rank = x.top100_rank,
				IssueDate = DateTime.Parse(x.issue_date),
				Vintage = x.vintage,
				Note = x.note
			}).ToList();

			Wines.AddRange(wines.Distinct(new WineEqualityComparer()));
		}

		public static List<Winery> Wineries = new List<Winery>
		{
			new Winery
			{
				Id = new Guid("D2835E85-89AB-4B23-A5D5-48DB93F778DD"),
				Name = "Sula",
				Region = "Nashik",
				Country = "India"
			}
		};

		public static List<Wine> Wines = new List<Wine>
		{
			new Wine
			{
				Id = new Guid("A05164DF-2BDB-4675-9AFA-9F5CBD9E69BD"),
				WineryId = new Guid("D2835E85-89AB-4B23-A5D5-48DB93F778DD"),
				Name = "Rasa Shiraz",
				Color = WineColor.Red,
				Score = 92,
				Price = 20,
				Rank = 36,
				IssueDate = DateTime.Parse("Jun 15, 2018"),
				Vintage = "2015",
				Note = "It is a complex wine, with power and finesse. Crafted from handpicked grapes from our own estate vineyards, Rasa Shiraz is aged for 12 months in premium French oak barrels and then further matured in the bottle before release."
			},
			new Wine
			{
				Id = new Guid("60E79B55-B2E8-45F7-9B89-6F9DE315D12E"),
				WineryId = new Guid("D2835E85-89AB-4B23-A5D5-48DB93F778DD"),
				Name = "Dindori Reserve Viognier",
				Color = WineColor.White,
				Score = 91,
				Price = 20,
				Rank = 75,
				IssueDate = DateTime.Parse("Sep 30, 2017"),
				Vintage = "2014",
				Note = "Dindori Reserve Viognier is an exotic elixir of peach and lychee flavours. Floral, spicy, stunning. Great as an aperitif and terrific with spicy food. Serve well chilled."
			},
			new Wine
			{
				Id = new Guid("8610C9AA-704E-4A10-99F3-086B980A8C4F"),
				WineryId = new Guid("D2835E85-89AB-4B23-A5D5-48DB93F778DD"),
				Name = "Zinfandel Rosé",
				Color = WineColor.Rose,
				Score = 91,
				Price = 16,
				Rank = 100,
				IssueDate = DateTime.Parse("Sep 30, 2016"),
				Vintage = "2015",
				Note = "Zinfandel Rose is ripe, fresh and fruity, with abundant aromas of cranberries and fresh strawberries. A versatile “anytime” wine great for picnics, parties and hot summer days. Lovely with poultry and spicy dishes. Serve well chilled."
			},
			new Wine
			{
				Id = new Guid("D9272018-DAE4-4F1A-BADF-B195C27BB057"),
				WineryId = new Guid("D2835E85-89AB-4B23-A5D5-48DB93F778DD"),
				Name = "Brut",
				Color = WineColor.Sparkling,
				Score = 91,
				Price = 41,
				Rank = 12,
				IssueDate = DateTime.Parse("Sep 30, 2019"),
				Vintage = "2017",
				Note = "A complex blend of Chenin Blanc, Chardonnay, Viognier, Pinot Noir, Riesling and Shiraz. This is one of the few “Méthode Champenoise” wines in the world to be crafted from six different grapes, resulting in something remarkable"
			},
			new Wine
			{
				Id = new Guid("9FB0680A-582C-4AA8-AE7F-21464B5BC5CA"),
				WineryId = new Guid("D2835E85-89AB-4B23-A5D5-48DB93F778DD"),
				Name = "Late Harvest Chenin Blanc",
				Color = WineColor.Dessert,
				Score = 91,
				Price = 15,
				Rank = 5,
				IssueDate = DateTime.Parse("Sep 30, 2019"),
				Vintage = "2019",
				Note = "Abounding with aromas of mango, honey and tropical fruit, our award winning Late Harvest Chenin Blanc is the perfect close to a delicious meal, but is also an elegant aperiti"
			}
		};
	}
}