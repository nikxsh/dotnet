using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryStore.Contracts;

namespace WineryStore.Persistence
{
	public class WineRepository : IWineRepository
	{
		public IWineDataStore DataStore;

		public WineRepository(IWineDataStore dataStore)
		{
			DataStore = dataStore;
		}

		public async Task<Response<IEnumerable<Wine>>> GetAllWinesAsync(Request request)
		{
			try
			{
				var response = new Response<IEnumerable<Wine>>
				{
					Result = await DataStore.GetAllWinesAsync()
				};

				response.Total = response.Result.Count();
				response.Result = response.Result.SearchWines(request.Token);
				response.Result = response.Result.FilterWines(request.Filters);
				response.Result = response.Result.SortWines(request.Sort);
				response.Result = response.Result.Skip(request.Skip).Take(request.Take).ToList();
				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Wine>> GetWineByIdAsync(Request<Guid> request)
		{
			try
			{
				return new Response<Wine>
				{
					Result = await DataStore.GetWineByIdAsync(request.Data),
					Total = 1
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Wine>> AddWineAsync(Wine request)
		{
			try
			{
				return new Response<Wine>
				{
					Result = await DataStore.AddWineAsync(request),
					Total = 1
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Wine>> UpdateWineAsync(Wine request)
		{
			try
			{
				return new Response<Wine>
				{
					Result = await DataStore.UpdateWineAsync(request),
					Total = 1
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<bool>> RemoveWineAsync(Request<Guid> request)
		{
			try
			{
				return new Response<bool>
				{
					Result = await DataStore.RemoveWineAsync(request.Data),
					Total = 0
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<bool>> WineExistsAsync(Request<Guid> request)
		{
			try
			{
				return new Response<bool>
				{
					Result = await DataStore.WineExistsAsync(request.Data)
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}