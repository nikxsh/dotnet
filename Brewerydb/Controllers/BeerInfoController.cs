using Brewerydb.Contracts;
using Brewerydb.Shared;
using System.Web.Http;

namespace Brewerydb.Controllers
{
	[RoutePrefix("api")]
	public class BeerInfoController : ApiController
	{
		private readonly IBeerInfoAdapter _iBeerInfoAdaptor;

		public BeerInfoController(IBeerInfoAdapter beerInfoAdaptor)
		{
			_iBeerInfoAdaptor = beerInfoAdaptor;
		}

		[Route("beers")]
		[HttpPost]
		public IHttpActionResult GetBeers([FromBody]RequestBase request)
		{
			var response = _iBeerInfoAdaptor.GetAll(request);
			if (response.Result == null)
				return NotFound();

			return Ok(response);
		}
	}
}
