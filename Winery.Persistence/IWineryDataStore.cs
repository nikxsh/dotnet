using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineryStore.Contracts;

namespace WineryStore.Persistence
{
	public interface IWineryDataStore
	{
		Task<IEnumerable<Winery>> GetAllWineriesAsync();
		Task<Winery> GetWineryByIdAsync(Guid id);
		Task<IEnumerable<Wine>> GetAllWinesAsync();
		Task<IEnumerable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId);
		Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId);
		Task<Winery> AddWineryAsync(Winery winery);
		Task<Winery> UpdateWineryAsync(Winery winery);
		Task<bool> RemoveWineryAsync(Guid wineryId);
	}
}
