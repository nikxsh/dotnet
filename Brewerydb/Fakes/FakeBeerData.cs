using Brewerydb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Brewerydb.Fakes
{
	public static class FakeBeerData
	{
		public static BeerInfo BeerInfo = new BeerInfo();

		public static BeerInfo GetBeerData()
		{
			BeerInfo.Data = new List<Beer>();

			var random = new Random();

			for (int i = 1, j = i + 1; i <= 1000; i++, j++)
			{
				BeerInfo.Data.Add(new Beer
				{
					Id = (110 + i).ToString(),
					Name = $"Beer {j}",
					Description = $"Description {j}",
					Abv = random.NextDouble().ToString(CultureInfo.InvariantCulture).Substring(0,4),
					Ibu = random.NextDouble().ToString(CultureInfo.InvariantCulture).Substring(0, 4),
					IsOrganic = j % 2 == 0 ? 'Y' : 'N',
					Status = j % 2 == 0 ? "Unverified" : "Verified",
					CreateDate = DateTime.UtcNow.AddDays(i),
					UpdateDate = DateTime.UtcNow.AddDays(j)
				});
			}
			return BeerInfo;
		}
	}
}
