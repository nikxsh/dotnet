using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public class InMemoryWineryDataStore : IWineryDataStore
	{
		public async Task<IEnumerable<Winery>> GetAllWineriesAsync()
		{
			return await Task.FromResult(MockWineryData.Wineries);
		}

		public async Task<IEnumerable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId)
		{
			return await Task.FromResult(
				from winery in MockWineryData.Wineries
				join wine in MockWineryData.Wines
				on winery.Id equals wine.WineryId
				where winery.Id == wineryId
				select wine
			);
		}

		public async Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId)
		{
			return await Task.Run(() =>
			{
				var result = from winery in MockWineryData.Wineries
								 join wine in MockWineryData.Wines
								 on winery.Id equals wine.WineryId
								 where winery.Id == wineryId && wine.Id == wineId
								 select wine;
				return result.SingleOrDefault();
			});
		}

		public async Task<Winery> GetWineryByIdAsync(Guid id)
		{
			return await Task.FromResult(MockWineryData.Wineries.SingleOrDefault(x => x.Id == id));
		}

		public async Task<Winery> AddWineryAsync(Winery winery)
		{
			if (MockWineryData.Wineries.Any(x => x.Name.Equals(winery.Name, StringComparison.InvariantCultureIgnoreCase)))
				throw new FormatException($"Winery with {winery.Name} allready exists");

			await Task.Run(() =>
			{
				winery.Id = Guid.NewGuid();
				MockWineryData.Wineries.Add(winery);
			});
			return await GetWineryByIdAsync(winery.Id);
		}

		public async Task<Winery> UpdateWineryAsync(Winery winery)
		{
			await Task.Run(() =>
			{
				var index = MockWineryData.Wineries.FindIndex(x => x.Id == winery.Id);
				MockWineryData.Wineries[index] = winery;
			});
			return await GetWineryByIdAsync(winery.Id);
		}

		public async Task<bool> RemoveWineryAsync(Guid wineryId)
		{
			await Task.Run(() =>
			{
				var index = MockWineryData.Wineries.FindIndex(x => x.Id == wineryId);
				MockWineryData.Wineries.RemoveAt(index);
			});
			return await WineryExistsAsync(wineryId);
		}

		public async Task<bool> WineryExistsAsync(Guid wineryId)
		{
			return await Task.FromResult(
				MockWineryData.Wineries.Any(x => x.Id == wineryId)
			);
		}

		public async Task<bool> WineryExistsAsync(string wineryName)
		{
			return await Task.FromResult(
				MockWineryData.Wineries.Any(x => x.Name.Equals(wineryName, StringComparison.InvariantCultureIgnoreCase))
			);
		}
	}
}