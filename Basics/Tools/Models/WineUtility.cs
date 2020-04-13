using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;

namespace Tools.Models
{
	public class WineUtility
	{
		readonly string BaseUrl = "https://top100.winespectator.com/wp-content/themes/top100-theme/src/data";
		readonly string MockyUrl = "http://www.mocky.io/v2";

		public void FetchWines()
		{
			var wineInfos = new List<WineInfo>();
			var range = Enumerable.Range(1988, 32);

			foreach (var item in range)
			{
				if (item == 2014)
				{
					var otraresult = new RestClient().GetJsonData<WineInfo>($"{MockyUrl}/5e94374d310000393c5e2ed2").Result;
					wineInfos.AddRange(otraresult);
					continue;
				}
				var result = new RestClient().GetJsonData<WineInfo>($"{BaseUrl}/{item}.json").Result;
				wineInfos.AddRange(result);
			}



			JsonHandler.WriteJsonData(@"D:\wineinfo.json", wineInfos);

			var wineries = wineInfos.Select(x => new Winery
			{
				Id = Guid.NewGuid(),
				Name = x.winery,
				Region = x.region,
				Country = x.country
			}).Distinct(new WineryEqualityComparer()).ToList();

			JsonHandler.WriteJsonData(@"D:\Temp\dotnet\Files\wineries.json", wineries);

			var wines = wineInfos.Select(x => new Wine
			{
				Id = Guid.NewGuid(),
				WineryId = wineries.FirstOrDefault(y => y.Name.Equals(x.winery, StringComparison.InvariantCultureIgnoreCase)).Id,
				Name = x.name,
				Color = x.color,
				Vintage = x.vintage,
				Price = x.price,
				IssueDate = DateTime.Parse(x.issueDate),
				Note = x.note
			}).Distinct(new WineEqualityComparer()).ToList();

			JsonHandler.WriteJsonData(@"D:\Temp\dotnet\Files\wines.json", wines);

			Console.WriteLine("Completed!");
		}
	}

	public class Winery
	{
		[JsonProperty(Order = -2)]
		public Guid Id { get; set; }
		[JsonProperty(Order = -1)]
		public string Name { get; set; }
		public string Region { get; set; }
		public string Country { get; set; }
	}


	public class Wine
	{
		[JsonProperty(Order = -3)]
		public Guid Id { get; set; }
		[JsonProperty(Order = -2)]
		public string Name { get; set; }
		[JsonProperty(Order = -1)]
		public Guid WineryId { get; set; }
		public string Color { get; set; }
		public string Vintage { get; set; }
		public decimal Price { get; set; }
		public DateTime IssueDate { get; set; }
		public string Note { get; set; }
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

	public class WineInfo
	{
		[JsonPropertyName("winery_full")]
		public string winery { get; set; }
		public string country { get; set; }
		public string region { get; set; }

		[JsonPropertyName("wine_full")]
		public string name { get; set; }
		public string vintage { get; set; }
		public string color { get; set; }
		public int score { get; set; }
		public int price { get; set; }
		[JsonPropertyName("issue_date")]
		public string issueDate { get; set; }
		[JsonPropertyName("top100_year")]
		public int top100Rank { get; set; }
		[JsonPropertyName("top100_rank")]
		public int top100Year { get; set; }
		public string note { get; set; }
	}
}
