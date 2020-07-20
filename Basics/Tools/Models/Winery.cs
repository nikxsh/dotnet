using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Tools.Models
{
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
