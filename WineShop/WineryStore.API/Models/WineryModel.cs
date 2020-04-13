using System;
using System.ComponentModel.DataAnnotations;
using WineryStore.Contracts;

namespace WineryStore.API.Models
{
	public abstract class Base
	{
		public Guid Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
	}

	public class WineryDTO : Base
	{
		[Required]
		[StringLength(50)]
		public string Region { get; set; }
		[Required]
		[StringLength(20)]
		public string Country { get; set; }
	}

	public class WineDTO : Base
	{
		public Guid WineryId { get; set; }
		[Required]
		public WineColor Color { get; set; }
		[StringLength(4)]
		public string Vintage { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string IssueDate { get; set; }
		public string Note { get; set; }
	}

	public enum WineColor
	{
		Blush,
		Champagne,
		Dessert,
		Red,
		Rose,
		Sparkling,
		White
	}
}