using Brewerydb.Models;
using Brewerydb.Shared;

namespace Brewerydb.Contracts
{
	public interface IBeerInfoAdapter
	{
		ResponseBase<BeerInfo> GetAll(RequestBase request);
	}
}
