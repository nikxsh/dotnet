using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public class InMemoryWineryDataStore : IWineryDataStore
	{
		public async Task<IQueryable<Winery>> GetAllWineriesAsync()
		{
			return await Task.FromResult(MockWineryData.Wineries.AsQueryable());
		}

		public Task<IQueryable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId)
		{
			return Task.FromResult(
				from winery in MockWineryData.Wineries.AsQueryable()
				join wine in MockWineryData.Wines.AsQueryable()
				on winery.Id equals wine.WineryId
				where winery.Id == wineryId
				select wine
			);
		}

		public Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId)
		{
			var result = from winery in MockWineryData.Wineries
							 join wine in MockWineryData.Wines
							 on winery.Id equals wine.WineryId
							 where winery.Id == wineryId && wine.Id == wineId
							 select wine;

			return Task.FromResult(result.SingleOrDefault());
		}

		public Task<Winery> GetWineryByIdAsync(Guid id)
		{
			return Task.FromResult(MockWineryData.Wineries.SingleOrDefault(x => x.Id == id));
		}

		public Task<Guid> AddWineryAsync(Winery winery)
		{
			if (MockWineryData.Wineries.Any(x => x.Name.Equals(winery.Name, StringComparison.InvariantCultureIgnoreCase)))
				throw new FormatException($"Winery with {winery.Name} allready exists");

			winery.Id = Guid.NewGuid();
			MockWineryData.Wineries.Add(winery);

			return Task.FromResult(winery.Id);
		}

		public Task<int> UpdateWineryAsync(Winery winery)
		{
			var index = MockWineryData.Wineries.FindIndex(x => x.Id == winery.Id);
			MockWineryData.Wineries[index] = winery;

			return Task.FromResult(1);
		}

		public Task<int> RemoveWineryAsync(Guid wineryId)
		{
			var index = MockWineryData.Wineries.FindIndex(x => x.Id == wineryId);
			MockWineryData.Wineries.RemoveAt(index);

			return Task.FromResult(1);
		}

		public Task<bool> WineryExistsAsync(Guid wineryId)
		{
			return Task.FromResult(
				MockWineryData.Wineries.Any(x => x.Id == wineryId)
			);
		}

		public Task<bool> WineryExistsAsync(string wineryName)
		{
			return Task.FromResult(
				MockWineryData.Wineries.Any(x => x.Name.Equals(wineryName, StringComparison.InvariantCultureIgnoreCase))
			);
		}
	}
}