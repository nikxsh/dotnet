using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public class WineDataStore : IWineDataStore, IDisposable
	{
		private bool disposed = false;
		private readonly WineryContext wineryContext;

		public WineDataStore(WineryContext wineryContext)
		{
			this.wineryContext = wineryContext;
		}

		public async Task<bool> WineExistsAsync(Guid wineId)
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wines
						.Any(x => x.Id == wineId);
			});
		}

		public async Task<bool> WineExistsAsync(string wineName)
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wines
						.Any(x => x.Name.Equals(wineName, StringComparison.InvariantCultureIgnoreCase));
			});
		}

		public async Task<IEnumerable<Wine>> GetAllWinesAsync()
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wines
						.ToList();
			});
		}

		public async Task<Wine> GetWineByIdAsync(Guid wineId)
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wines
						.SingleOrDefault(x => x.Id == wineId);
			});
		}

		public async Task<Wine> AddWineAsync(Wine wine)
		{
			await Task.Run(() =>
			{
				wineryContext.Wines.Add(wine);
				wineryContext.SaveChangesAsync();
			});
			return await GetWineByIdAsync(wine.Id);
		}

		public async Task<Wine> UpdateWineAsync(Wine wine)
		{
			await Task.Run(() =>
			{
				wineryContext.Wines.Update(wine);
				wineryContext.SaveChangesAsync();
			});
			return await GetWineByIdAsync(wine.Id);
		}

		public async Task<bool> RemoveWineAsync(Guid wineId)
		{
			await Task.Run(() =>
			{
				var wine = GetWineByIdAsync(wineId).Result;
				wineryContext.Wines.Remove(wine);
				wineryContext.SaveChangesAsync();
			});
			return await WineExistsAsync(wineId);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
				Dispose();

			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}