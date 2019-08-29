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

		public async Task<bool> WineryExistsAsync(Guid wineryId)
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wineries
						.Any(x => x.Id == wineryId);
			});
		}

		public async Task<bool> WineryExistsAsync(string wineryName)
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wineries
						.Any(x => x.Name.Equals(wineryName, StringComparison.InvariantCultureIgnoreCase));
			});
		}

		public async Task<IEnumerable<Winery>> GetAllWineriesAsync()
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wineries
						.ToList();
			});
		}

		public async Task<Winery> GetWineryByIdAsync(Guid id)
		{
			return await Task.Run(() =>
			{
				return wineryContext
						.Wineries
						.SingleOrDefault(x => x.Id == id);
			});
		}

		public async Task<IEnumerable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId)
		{
			return await Task.Run(() =>
			{
				var result = from winery in wineryContext.Wineries
								 join wine in wineryContext.Wines
								 on winery.Id equals wine.WineryId
								 where winery.Id == wineryId
								 select wine;
				return result;
			});
		}

		public async Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId)
		{
			return await Task.Run(() =>
			{
				var result = from winery in wineryContext.Wineries
								 join wine in wineryContext.Wines
								 on winery.Id equals wine.WineryId
								 where winery.Id == wineryId && wine.Id == wineId
								 select wine;
				return result.SingleOrDefault();
			});
		}

		public async Task<Winery> AddWineryAsync(Winery winery)
		{
			await Task.Run(() =>
			{
				wineryContext.Wineries.Add(winery);
				wineryContext.SaveChangesAsync();
			});
			return await GetWineryByIdAsync(winery.Id);
		}

		public async Task<Winery> UpdateWineryAsync(Winery winery)
		{
			await Task.Run(() =>
			{
				wineryContext.Wineries.Update(winery);
				wineryContext.SaveChangesAsync();
			});
			return await GetWineryByIdAsync(winery.Id);
		}

		public async Task<bool> RemoveWineryAsync(Guid wineryId)
		{
			await Task.Run(() =>
			{
				var winery = GetWineryByIdAsync(wineryId).Result;
				wineryContext.Wineries.Remove(winery);
				wineryContext.SaveChangesAsync();
			});
			return await WineryExistsAsync(wineryId);
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