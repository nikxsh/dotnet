using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Models;

namespace Tools
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
}
