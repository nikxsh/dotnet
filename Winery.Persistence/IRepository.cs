using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineryStore.Contracts;

namespace WineryStore.Persistence
{
	public interface IWineryRepository
	{
		Task<Response<IEnumerable<Winery>>> GetAllWineriesAsync(Request request);
		Task<Response<Winery>> GetWinerybyIdAsync(Request<Guid> request);
		Task<Response<IEnumerable<Wine>>> GetAllWinesFromWineryAsync(Request<Guid> request);
		Task<Response<Wine>> GetWineFromWineryByIdAsync(Request<Tuple<Guid, Guid>> request);
		Task<Response<Winery>> AddWineryAsync(Winery request);
		Task<Response<Winery>> UpdateWineryAsync(Winery request);
		Task<Response<bool>> RemoveWineryAsync(Request<Guid> request);
		Task<Response<bool>> WineryExistsAsync(Request<Guid> request);
	}
	public interface IWineRepository
	{
		Task<Response<IEnumerable<Wine>>> GetAllWinesAsync(Request request);
		Task<Response<Wine>> GetWineByIdAsync(Request<Guid> request);
		Task<Response<Wine>> AddWineAsync(Wine request);
		Task<Response<Wine>> UpdateWineAsync(Wine request);
		Task<Response<bool>> RemoveWineAsync(Request<Guid> request);
		Task<Response<bool>> WineExistsAsync(Request<Guid> request);
	}
}