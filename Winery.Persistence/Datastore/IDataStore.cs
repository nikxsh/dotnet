using System;
using System.Linq;
using System.Threading.Tasks;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence.Datastore
{
	public interface IWineryDataStore
	{
		Task<IQueryable<Winery>> GetAllWineriesAsync();
		Task<Winery> GetWineryByIdAsync(Guid id);
		Task<IQueryable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId);
		Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId);
		Task<Guid> AddWineryAsync(Winery winery);
		Task<int> UpdateWineryAsync(Winery winery);
		Task<int> RemoveWineryAsync(Guid wineryId);
		Task<bool> WineryExistsAsync(Guid wineryId);
		Task<bool> WineryExistsAsync(string wineryName);
	}

	public interface IWineDataStore
	{
		Task<IQueryable<Wine>> GetAllWinesAsync();
		Task<Wine> GetWineByIdAsync(Guid wineId);
		Task<Guid> AddWineAsync(Wine wine);
		Task<int> UpdateWineAsync(Wine wine);
		Task<int> RemoveWineAsync(Guid wineId);
		Task<bool> WineExistsAsync(Guid wineId);
		Task<bool> WineExistsAsync(string wineName);
	}
}