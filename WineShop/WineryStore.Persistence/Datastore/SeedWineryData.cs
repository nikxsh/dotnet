using Shared;
using System.Collections.Generic;
using System.Linq;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public class SeedWineryData
	{
		public static HashSet<Winery> GetWineries()
		{
			return new 
				FileHandler()
				.ReadJsonData<Winery[]>(@"..\..\Files\wineries.json")
				.ToHashSet();
		}

		public static HashSet<Wine> GetWines()
		{
			return new 
				FileHandler()
				.ReadJsonData<Wine[]>(@"..\..\Files\wines.json")
				.ToHashSet();
		}
	}
}