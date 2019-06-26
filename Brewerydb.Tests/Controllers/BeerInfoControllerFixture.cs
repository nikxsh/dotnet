using Brewerydb.Contracts;
using Brewerydb.Controllers;
using Brewerydb.Models;
using Brewerydb.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Brewerydb.Fakes;

namespace Brewerydb.Tests.Controllers
{
	[TestClass]
	public class BeerInfoControllerFixture
	{
		private readonly IBeerInfoAdapter _iBeerInfoAdaptor;
		private readonly List<Beer> _fakeBeerList;

		public BeerInfoControllerFixture()
		{
			_iBeerInfoAdaptor = Substitute.For<IBeerInfoAdapter>();
			_fakeBeerList = FakeBeerData.GetBeerData().Data;
		}

		[TestMethod]
		public void Get_all_should_return_ok_with_beer_info()
		{
			// Arrange
			var request = new RequestBase
			{
				Skip = 0,
				Take = 100
			};

			_iBeerInfoAdaptor
				.GetAll(Arg.Any<RequestBase>())
				.Returns(new ResponseBase<BeerInfo>
				{
					Result = new BeerInfo()
				});

			var controller = new BeerInfoController(_iBeerInfoAdaptor);

			// Act
			var actionResult = controller.GetBeers(request);

			// Assert
			Assert.IsNotNull(actionResult);
			Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<ResponseBase<BeerInfo>>));
		}

		[TestMethod]
		public void Get_all_should_return_not_found_for_empty_results()
		{
			// Arrange
			var request = new RequestBase
			{
				Skip = 0,
				Take = 100
			};

			_iBeerInfoAdaptor
				.GetAll(Arg.Any<RequestBase>())
				.Returns(new ResponseBase<BeerInfo>
				{
					Result = null
				});

			var controller = new BeerInfoController(_iBeerInfoAdaptor);

			// Act
			IHttpActionResult actionResult = controller.GetBeers(request);

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
		}

		[TestMethod]
		public void Get_all_should_return_requested_number_of_beer_records()
		{
			// Arrange
			var request = new RequestBase
			{
				Skip = 0,
				Take = 100
			};

			var beerList = _fakeBeerList
								.Skip(request.Skip)
								.Take(request.Take)
								.ToList();

			var expected = new ResponseBase<BeerInfo>
			{
				Result = new BeerInfo
				{
					Data = beerList,
					TotalResults = beerList.Count
				}
			};

			_iBeerInfoAdaptor
				.GetAll(Arg.Any<RequestBase>())
				.Returns(expected);

			var controller = new BeerInfoController(_iBeerInfoAdaptor);

			// Act
			var actionResult = controller.GetBeers(request);
			var contentResult = actionResult as OkNegotiatedContentResult<ResponseBase<BeerInfo>>;

			// Assert
			Assert.IsNotNull(contentResult);
			Assert.AreEqual(request.Take, contentResult.Content.Result.TotalResults);
		}
	}
}
