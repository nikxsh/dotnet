using System;

namespace WineryStore.Contracts
{
	public class Winery
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Region { get; set; }
		public string Country { get; set; }
	}

	public class Wine
	{
		public Guid Id { get; set; }
		public Guid WineryId { get; set; }
		public string Name { get; set; }
		public WineColor Color { get; set; }
		public string Vintage { get; set; }
		public int Score { get; set; }
		public int Price { get; set; }
		public string IssueDate { get; set; }
		public int Rank { get; set; }
		public string Note { get; set; }
	}

	public enum WineColor
	{
		Red,
		White,
		Rose,
		Champagne,
		Dessert,
		Sparkling
	}
}