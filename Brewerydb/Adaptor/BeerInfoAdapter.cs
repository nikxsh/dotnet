using Brewerydb.Contracts;
using Brewerydb.Models;
using Brewerydb.Shared;
using System;
using System.Linq;
using Brewerydb.Fakes;

namespace Brewerydb.Adaptor
{
	public class BeerInfoAdapter : IBeerInfoAdapter
	{
		private readonly IWebClient _webClient;

		public BeerInfoAdapter(IWebClient webClient)
		{
			_webClient = webClient;
		}

		public ResponseBase<BeerInfo> GetAll(RequestBase request)
		{
			var response = new ResponseBase<BeerInfo>();

			try
			{
				response.Result = FakeBeerData.GetBeerData(); //_webClient.GetBeers(request.CurrentPage);

				response.Result.Data = response.Result.Data.Search(request.Token);
				response.Result.Data = response.Result.Data.Filter(request.Filters);
				response.Result.Data = response.Result.Data.Sort(request.Sort);

				response.Result.TotalResults = response.Result.Data.Count();
				response.Result.Data = response.Result.Data.Skip(request.Skip).Take(request.Take).ToList();
			}
			catch (Exception ex)
			{
				response = ex.ToAdapterResponseBase<BeerInfo>();
				response.Exception = ex;
			}
			return response;
		}
	}
}