using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public class WineryDataStore : IWineryDataStore, IDisposable
	{
		private bool disposed = false;
		private readonly WineryContext wineryContext;

		public WineryDataStore(WineryContext wineryContext)
		{
			this.wineryContext = wineryContext;
		}

		public Task<bool> WineryExistsAsync(Guid wineryId)
		{
			var exists = wineryContext
						.Wineries
						.Any(x => x.Id == wineryId);

			return Task.FromResult(exists);
		}

		public Task<bool> WineryExistsAsync(string wineryName)
		{
			var exists = wineryContext
						.Wineries
						.Any(x => x.Name.Equals(wineryName, StringComparison.InvariantCultureIgnoreCase));

			return Task.FromResult(exists);
		}

		public Task<IQueryable<Winery>> GetAllWineriesAsync()
		{
			var allWineries = wineryContext
							.Wineries
							.Select(x => x);

			return Task.FromResult(allWineries);
		}

		public Task<Winery> GetWineryByIdAsync(Guid id)
		{
			var winery =  wineryContext
					.Wineries
					.SingleOrDefault(x => x.Id == id);

			return Task.FromResult(winery);
		}

		public Task<IQueryable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId)
		{
			var wineries = from winery in wineryContext.Wineries
							 join wine in wineryContext.Wines
							 on winery.Id equals wine.WineryId
							 where winery.Id == wineryId
							 select wine;

			return Task.FromResult(wineries);
		}

		public Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId)
		{
			var specificWinery = (from winery in wineryContext.Wineries
										 join wine in wineryContext.Wines
										 on winery.Id equals wine.WineryId
										 where winery.Id == wineryId && wine.Id == wineId
										 select wine)
										.SingleOrDefault();

			return Task.FromResult(specificWinery);
		}

		public Task<Guid> AddWineryAsync(Winery winery)
		{
			winery.Id = Guid.NewGuid();
			wineryContext.Wineries.Add(winery);
			var result = wineryContext.SaveChanges() > 0 ? winery.Id : Guid.Empty;
			return Task.FromResult(result);
		}

		public Task<int> UpdateWineryAsync(Winery winery)
		{
			wineryContext.Wineries.Update(winery);
			var saved = wineryContext.SaveChanges();
			return Task.FromResult(saved);
		}

		public Task<int> RemoveWineryAsync(Guid wineryId)
		{
			var winery = GetWineryByIdAsync(wineryId).Result;
			wineryContext.Wineries.Remove(winery);
			var saved = wineryContext.SaveChanges();
			return Task.FromResult(saved);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
				wineryContext.Dispose();

			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}