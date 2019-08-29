using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public class InMemoryWineDataStore : IWineDataStore
	{

		public async Task<IEnumerable<Wine>> GetAllWinesAsync()
		{
			return await Task.FromResult(MockWineryData.Wines);
		}

		public async Task<Wine> GetWineByIdAsync(Guid wineId)
		{
			return await Task.FromResult(MockWineryData.Wines.SingleOrDefault(x => x.Id == wineId));
		}

		public async Task<Wine> AddWineAsync(Wine wine)
		{
			if (MockWineryData.Wines.Any(x => x.Name.Equals(wine.Name, StringComparison.InvariantCultureIgnoreCase)))
				throw new FormatException($"Wine with name {wine.Name} allready exists");

			await Task.Run(() =>
			{
				wine.Id = Guid.NewGuid();
				MockWineryData.Wines.Add(wine);
			});
			return await GetWineByIdAsync(wine.Id);
		}

		public async Task<Wine> UpdateWineAsync(Wine wine)
		{
			await Task.Run(() =>
			{
				var index = MockWineryData.Wines.FindIndex(x => x.Id == wine.Id);
				MockWineryData.Wines[index] = wine;
			});
			return await GetWineByIdAsync(wine.Id);
		}

		public async Task<bool> RemoveWineAsync(Guid wineId)
		{
			await Task.Run(() =>
			{
				var index = MockWineryData.Wines.FindIndex(x => x.Id == wineId);
				MockWineryData.Wines.RemoveAt(index);
			});
			return await WineExistsAsync(wineId);
		}

		public async Task<bool> WineExistsAsync(Guid wineId)
		{
			return await Task.FromResult(
				MockWineryData.Wines.Any(x => x.Id == wineId)
			);
		}

		public async Task<bool> WineExistsAsync(string wineName)
		{
			return await Task.FromResult(
				MockWineryData.Wines.Any(x => x.Name.Equals(wineName, StringComparison.InvariantCultureIgnoreCase))
			);
		}
	}
}