using System;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public class InMemoryWineDataStore : IWineDataStore
	{

		public Task<IQueryable<Wine>> GetAllWinesAsync()
		{
			return Task.FromResult(MockWineryData.Wines.AsQueryable());
		}

		public async Task<Wine> GetWineByIdAsync(Guid wineId)
		{
			return await Task.FromResult(MockWineryData.Wines.SingleOrDefault(x => x.Id == wineId));
		}

		public Task<Guid> AddWineAsync(Wine wine)
		{
			if (MockWineryData.Wines.Any(x => x.Name.Equals(wine.Name, StringComparison.InvariantCultureIgnoreCase)))
				throw new FormatException($"Wine with name {wine.Name} allready exists");
			
			wine.Id = Guid.NewGuid();
			MockWineryData.Wines.Add(wine);

			return Task.FromResult(wine.Id);
		}

		public Task<int> UpdateWineAsync(Wine wine)
		{
			var index = MockWineryData.Wines.FindIndex(x => x.Id == wine.Id);
			MockWineryData.Wines[index] = wine;

			return Task.FromResult(1);
		}

		public Task<int> RemoveWineAsync(Guid wineId)
		{
			var index = MockWineryData.Wines.FindIndex(x => x.Id == wineId);
			MockWineryData.Wines.RemoveAt(index);

			return Task.FromResult(1);
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