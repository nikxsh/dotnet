using System;
using System.Collections.Generic;
using WineryStore.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace WineryStore.Persistence
{
	public class WineryRepository : IWineryRepository
	{
		public IWineryDataStore DataStore;

		public WineryRepository(IWineryDataStore dataStore)
		{
			DataStore = dataStore;
		}

		public async Task<Response<IEnumerable<Winery>>> GetAllWineriesAsync(Request request)
		{
			try
			{
				var response = new Response<IEnumerable<Winery>>
				{
					Result = await DataStore.GetAllWineriesAsync()
				};

				response.Total = response.Result.Count();
				response.Result = response.Result.SearchWineries(request.Token);
				response.Result = response.Result.FilterWineries(request.Filters);
				response.Result = response.Result.SortWineries(request.Sort);
				response.Result = response.Result.Skip(request.Skip).Take(request.Take).ToList();
				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Winery>> GetWinerybyIdAsync(Request<Guid> request)
		{
			try
			{
				return new Response<Winery>
				{
					Result = await DataStore.GetWineryByIdAsync(request.Data),
					Total = 1
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<IEnumerable<Wine>>> GetAllWinesFromWineryAsync(Request<Guid> request)
		{
			try
			{
				var response =  new Response<IEnumerable<Wine>>
				{
					Result = await DataStore.GetAllWinesFromWineryAsync(request.Data)
				};
				response.Total = response.Result.Count();
				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Wine>> GetWineFromWineryByIdAsync(Request<Tuple<Guid, Guid>> request)
		{
			try
			{
				return new Response<Wine>
				{
					Result = await DataStore.GetWineFromWineryByIdAsync(request.Data.Item1, request.Data.Item2),
					Total = 1
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Winery>> AddWineryAsync(Winery request)
		{
			try
			{
				return new Response<Winery>
				{
					Result = await DataStore.AddWineryAsync(request),
					Total = 1
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Winery>> UpdateWineryAsync(Winery request)
		{
			try
			{
				return new Response<Winery>
				{
					Result = await DataStore.UpdateWineryAsync(request),
					Total = 1
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<bool>> RemoveWineryAsync(Request<Guid> request)
		{
			try
			{
				return new Response<bool>
				{
					Result = await DataStore.RemoveWineryAsync(request.Data),
					Total = 0
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<bool>> WineryExistsAsync(Request<Guid> request)
		{
			try
			{
				return new Response<bool>
				{
					Result = await DataStore.WineryExistsAsync(request.Data)
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}