using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WineryStore.API.Models
{
	public class WineryDTO
	{
		public Guid Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		[Required]
		[StringLength(20)]
		public string Region { get; set; }
		[Required]
		[StringLength(20)]
		public string Country { get; set; }
	}

	public class WineDTO
	{
		public Guid Id { get; set; }
		[Required]
		public Guid WineryId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public WineColor Color { get; set; }
		[StringLength(4)]
		public string Vintage { get; set; }
		[Range(0.01, 99.99)]
		public int Score { get; set; }
		[Required]
		public int Price { get; set; }
		[Required]
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
