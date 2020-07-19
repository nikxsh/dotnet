using System;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
    public class WineDataStore : IWineDataStore, IDisposable
	{
		private bool disposed = false;
		private readonly WineryContext WineContext;

		public WineDataStore(WineryContext wineContext)
		{
			this.WineContext = wineContext;
		}

		public Task<bool> WineExistsAsync(Guid wineId)
		{
			var wine = WineContext
						.Wines
						.Any(x => x.Id == wineId);

			return Task.FromResult(wine);
		}

		public Task<bool> WineExistsAsync(string wineName)
		{
			var wine = WineContext
						.Wines
						.Any(x => x.Name.Equals(wineName, StringComparison.InvariantCultureIgnoreCase));

			return Task.FromResult(wine);
		}

		public Task<IQueryable<Wine>> GetAllWinesAsync()
		{
			var wines = WineContext
						.Wines
						.Select(x => x);

			return Task.FromResult(wines);
		}

		public Task<Wine> GetWineByIdAsync(Guid wineId)
		{
			var wine = WineContext
						.Wines
						.SingleOrDefault(x => x.Id == wineId);

			return Task.FromResult(wine);
		}

		public Task<Guid> AddWineAsync(Wine wine)
		{
			wine.Id = Guid.NewGuid();
			WineContext.Wines.Add(wine);
			var result = WineContext.SaveChanges() > 0 ? wine.Id : Guid.Empty;
			return Task.FromResult(result);
		}

		public Task<int> UpdateWineAsync(Wine wine)
		{
			WineContext.Wines.Update(wine);
			var result = WineContext.SaveChanges();

			return Task.FromResult(result);
		}

		public Task<int> RemoveWineAsync(Guid wineId)
		{
			var wine = GetWineByIdAsync(wineId).Result;
			WineContext.Wines.Remove(wine);
			var result = WineContext.SaveChanges();

			return Task.FromResult(result);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
				WineContext.Dispose();

			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}