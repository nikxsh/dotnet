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

		public async Task<Response<IEnumerable<Winery>>> GetAllWineriesAsync(Request request)
		{
			try
			{
				var response = new Response<IEnumerable<Winery>>();

				var wineries = await DataStore.GetAllWineriesAsync();
				response.Result = wineries.Select(x => Mapper.Map<Winery>(x));

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
				var response = new Response<Winery>();

				var winery = await DataStore.GetWineryByIdAsync(request.Data);
				response.Result = Mapper.Map<Winery>(winery);
				response.Total = 1;

				return response;
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
				var response = new Response<IEnumerable<Wine>>();

				var wineries = await DataStore.GetAllWinesFromWineryAsync(request.Data);
				response.Result = wineries.Select(x => Mapper.Map<Wine>(x));
				response.Total = 1;

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
				response.Total = 1;

				return response;
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
				var response = new Response<Winery>();

				var addedWinery = await DataStore.AddWineryAsync(Mapper.Map<WineryContext.Winery>(request));
				response.Result = Mapper.Map<Winery>(addedWinery);
				response.Total = 1;

				return response;
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
				var response = new Response<Winery>();

				var updatedWinery = await DataStore.UpdateWineryAsync(Mapper.Map<WineryContext.Winery>(request));
				response.Result = Mapper.Map<Winery>(updatedWinery);
				response.Total = 1;

				return response;
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
				var response = new Response<bool>
				{
					Result = await DataStore.RemoveWineryAsync(request.Data),
					Total = 0
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