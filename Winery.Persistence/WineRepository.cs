using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryStore.Contracts;
using WineryStore.Persistence.Datastore;

namespace WineryStore.Persistence
{
	public class WineRepository : IWineRepository
	{
		public IWineDataStore DataStore;
		public IMapper Mapper;

		public WineRepository(IWineDataStore dataStore, IMapper mapper)
		{
			DataStore = dataStore;
			Mapper = mapper;
		}

		public async Task<Response<IEnumerable<Wine>>> GetAllWinesAsync(Request request)
		{
			try
			{
				var response = new Response<IEnumerable<Wine>>();

				var wines = await DataStore.GetAllWinesAsync();
				response.Result = wines.Select(x => Mapper.Map<Wine>(x));

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
				var response = new Response<Wine>();

				var wine = await DataStore.GetWineByIdAsync(request.Data);

				response.Result = Mapper.Map<Wine>(wine);
				response.Total = 1;

				return response;
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
				var response = new Response<Wine>();

				var addedWine = await DataStore.AddWineAsync(Mapper.Map<WineryContext.Wine>(request));
				response.Result = Mapper.Map<Wine>(request);
				response.Total = 1;

				return response;
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
				var response = new Response<Wine>();

				var updatedWine = await DataStore.UpdateWineAsync(Mapper.Map<WineryContext.Wine>(request));
				response.Result = Mapper.Map<Wine>(request);
				response.Total = 1;

				return response;
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