using System;
using System.Collections.Generic;
using WineryStore.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WineryStore.Persistence.Datastore;

namespace WineryStore.Persistence
{
	public class WineryRepository : IWineryRepository
	{
		public IWineryDataStore DataStore;
		public IMapper Mapper;

		public WineryRepository(IWineryDataStore dataStore, IMapper mapper)
		{
			DataStore = dataStore;
			Mapper = mapper;
		}

		public async Task<PagedResponse<IEnumerable<Winery>>> GetAllWineriesAsync(Request request)
		{
			try
			{
				var response = new PagedResponse<IEnumerable<Winery>>();

				var wineries = await DataStore.GetAllWineriesAsync();
				var mappedWineries = wineries.Select(x => Mapper.Map<Winery>(x));
				response.Total = mappedWineries.Count();

				mappedWineries = mappedWineries.SearchWineries(request.Token);
				mappedWineries = mappedWineries.FilterWineries(request.Filters);
				mappedWineries = mappedWineries.SortWineries(request.Sort);

				response.Result = mappedWineries.Skip(request.Skip).Take(request.Take).ToList();
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
				var response = new Response<Winery>();

				var winery = await DataStore.GetWineryByIdAsync(request.Data);
				response.Result = Mapper.Map<Winery>(winery);

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<PagedResponse<IEnumerable<Wine>>> GetAllWinesFromWineryAsync(Request<Guid> request)
		{
			try
			{
				var response = new PagedResponse<IEnumerable<Wine>>();

				var wines = await DataStore.GetAllWinesFromWineryAsync(request.Data);
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

		public async Task<Response<Wine>> GetWineFromWineryByIdAsync(Request<Tuple<Guid, Guid>> request)
		{
			try
			{
				var response = new Response<Wine>();

				var wine = await DataStore.GetWineFromWineryByIdAsync(request.Data.Item1, request.Data.Item2);
				response.Result = Mapper.Map<Wine>(wine);

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<Guid>> AddWineryAsync(Winery request)
		{
			try
			{
				var response = new Response<Guid>
				{
					Result = await DataStore.AddWineryAsync(Mapper.Map<WineryContext.Winery>(request))
				};
				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<int>> UpdateWineryAsync(Winery request)
		{
			try
			{
				var response = new Response<int>
				{
					Result = await DataStore.UpdateWineryAsync(Mapper.Map<WineryContext.Winery>(request))
				};
				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<Response<int>> RemoveWineryAsync(Request<Guid> request)
		{
			try
			{
				var response = new Response<int>
				{
					Result = await DataStore.RemoveWineryAsync(request.Data)
				};

				return response;
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