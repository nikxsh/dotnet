using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineryStore.Contracts;
using WineryStore.Contracts.Utils;

namespace WineryStore.Persistence
{
	public interface IWineryRepository
	{
		Task<PagedResponse<IEnumerable<Winery>>> GetAllWineriesAsync(Request request);
		Task<Response<Winery>> GetWinerybyIdAsync(Request<Guid> request);
		Task<PagedResponse<IEnumerable<Wine>>> GetAllWinesFromWineryAsync(Request<Guid> request);
		Task<Response<Wine>> GetWineFromWineryByIdAsync(Request<Tuple<Guid, Guid>> request);
		Task<Response<Guid>> AddWineryAsync(Winery request);
		Task<Response<int>> UpdateWineryAsync(Winery request);
		Task<Response<int>> RemoveWineryAsync(Request<Guid> request);
		Task<Response<bool>> WineryExistsAsync(Request<Guid> request);
	}
	public interface IWineRepository
	{
		Task<PagedResponse<IEnumerable<Wine>>> GetAllWinesAsync(Request request);
		Task<Response<Wine>> GetWineByIdAsync(Request<Guid> request);
		Task<Response<Guid>> AddWineAsync(Wine request);
		Task<Response<int>> UpdateWineAsync(Wine request);
		Task<Response<int>> RemoveWineAsync(Request<Guid> request);
		Task<Response<bool>> WineExistsAsync(Request<Guid> request);
	}
}