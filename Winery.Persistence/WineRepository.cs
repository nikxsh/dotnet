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

		public async Task<PagedResponse<IEnumerable<Wine>>> GetAllWinesAsync(Request request)
		{
			try
			{
				var response = new PagedResponse<IEnumerable<Wine>>();

				var wines = await DataStore.GetAllWinesAsync();
				var mappedWines = wines.Select(x => Mapper.Map<Wine>(x));
				response.Total = mappedWines.Count();

				mappedWines = mappedWines.SearchWines(request.Token);
				mappedWines = mappedWines.FilterWines(request.Filters);
				mappedWines = mappedWines.SortWines(request.Sort);

				response.Result = mappedWines.Skip(request.Skip).Take(request.Take).ToList();
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

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Guid>> AddWineAsync(Wine request)
		{
			try
			{
				var response = new Response<Guid>
				{
					Result = await DataStore.AddWineAsync(Mapper.Map<WineryContext.Wine>(request))
				};

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<int>> UpdateWineAsync(Wine request)
		{
			try
			{
				var response = new Response<int>
				{
					Result = await DataStore.UpdateWineAsync(Mapper.Map<WineryContext.Wine>(request))
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