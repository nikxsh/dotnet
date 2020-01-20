using System;

namespace WineryStore.Contracts
{
	public abstract class Base
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}

	public class Winery : Base
	{
		public string Region { get; set; }
		public string Country { get; set; }
	}

	public class Wine : Base
	{
		public Guid WineryId { get; set; }
		public WineColor Color { get; set; }
		public string Vintage { get; set; }
		public int Score { get; set; }
		public decimal Price { get; set; }
		public DateTime IssueDate { get; set; }
		public int Rank { get; set; }
		public int RankYear { get; set; }
		public string Note { get; set; }
	}

	public enum WineColor
	{
		Red,
		White,
		Rose,
		Champagne,
		Dessert,
		Sparkling,
		Blush
	}
}