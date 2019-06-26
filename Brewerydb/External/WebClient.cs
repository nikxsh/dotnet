using Brewerydb.Contracts;
using Brewerydb.Models;
using Brewerydb.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brewerydb.External
{
	public class WebClient : IWebClient
	{
		public BeerInfo GetBeers(int pageNumber = 1)
		{
			return GetBeerInfoAsync("v2/beers".ToUri(pageNumber).AbsoluteUri).Result;
		}

		async Task<BeerInfo> GetBeerInfoAsync(string requestUri)
		{
			BeerInfo beerInfo = null;

			using (var client = new HttpClient())
			{
				var response = client.GetAsync(requestUri).Result;

				if (response.IsSuccessStatusCode)
					beerInfo = await response.Content.ReadAsAsync<BeerInfo>();
			}
			return beerInfo;
		}
	}
}