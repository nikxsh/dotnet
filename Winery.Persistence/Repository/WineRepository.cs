using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryStore.Contracts;
using WineryStore.Contracts.Utils;
using WineryStore.Persistence.Datastore;

namespace WineryStore.Persistence
{
	public class WineRepository : IWineRepository
	{
		public IWineDataStore DataStore;

		public WineRepository(IWineDataStore dataStore)
		{
			DataStore = dataStore;
		}

		public async Task<PagedResponse<IEnumerable<Wine>>> GetAllWinesAsync(Request request)
		{
			try
			{
				var response = new PagedResponse<IEnumerable<Wine>>();

				var wines = await DataStore.GetAllWinesAsync();

				response.Total = wines.Count();
				wines = wines.SearchWines(request.Token);
				wines = wines.FilterWines(request.Filters);
				wines = wines.SortWines(request.Sort);
				wines = wines.Skip(request.Skip).Take(request.Take);

				response.Result = wines.Select(x => x.MapToWineContract());

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
				var response = new Response<Wine>();

				var wine = await DataStore.GetWineByIdAsync(request.Data);

				response.Result = wine.MapToWineContract();

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Guid>> AddWineAsync(Wine wine)
		{
			try
			{
				var response = new Response<Guid>
				{
					Result = await DataStore.AddWineAsync(wine.MapToWinePersistence())
				};

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<int>> UpdateWineAsync(Wine wine)
		{
			try
			{
				var response = new Response<int>
				{
					Result = await DataStore.UpdateWineAsync(wine.MapToWinePersistence())
				};

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<int>> RemoveWineAsync(Request<Guid> request)
		{
			try
			{
				return new Response<int>
				{
					Result = await DataStore.RemoveWineAsync(request.Data)
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