using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Brewerydb.Models
{
	public class BeerInfo
	{
		public string Status { get; set; }
		public List<Beer> Data { get; set; }
		public int TotalResults { get; set; }
	}


	public class Beer
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Abv { get; set; }
		public string Ibu { get; set; }
		public char IsOrganic { get; set; }
		public string Status { get; set; }

		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
