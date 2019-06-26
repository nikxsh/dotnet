using Brewerydb.Models;

namespace Brewerydb.Contracts
{
	public interface IWebClient
	{
		BeerInfo GetBeers(int pageNumber = 1);
	}
}
