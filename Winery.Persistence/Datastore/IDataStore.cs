using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public interface IWineryDataStore
	{
		Task<bool> WineryExistsAsync(Guid wineryId);
		Task<bool> WineryExistsAsync(string wineryName);
		Task<IEnumerable<Winery>> GetAllWineriesAsync();
		Task<Winery> GetWineryByIdAsync(Guid id);
		Task<IEnumerable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId);
		Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId);
		Task<Winery> AddWineryAsync(Winery winery);
		Task<Winery> UpdateWineryAsync(Winery winery);
		Task<bool> RemoveWineryAsync(Guid wineryId);
	}

	public interface IWineDataStore
	{
		Task<bool> WineExistsAsync(Guid wineId);
		Task<bool> WineExistsAsync(string wineName);
		Task<IEnumerable<Wine>> GetAllWinesAsync();
		Task<Wine> GetWineByIdAsync(Guid wineId);
		Task<Wine> AddWineAsync(Wine wine);
		Task<Wine> UpdateWineAsync(Wine wine);
		Task<bool> RemoveWineAsync(Guid wineId);
	}
}
