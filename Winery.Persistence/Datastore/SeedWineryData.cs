using Shared;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
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

	public class SeedWineryData
	{
		public static HashSet<Winery> GetWineries()
		{
			return new 
				FileHandler()
				.ReadJsonData<Winery[]>(@"D:\Temp\dotnet\Winery.Persistence\Datastore\wineries.json")
				.ToHashSet();
		}

		public static HashSet<Wine> GetWines()
		{
			return new 
				FileHandler()
				.ReadJsonData<Wine[]>(@"D:\Temp\dotnet\Winery.Persistence\Datastore\wines.json")
				.ToHashSet();
		}
	}
}