using Brewerydb.Adaptor;
using Brewerydb.Contracts;
using Brewerydb.Fakes;
using Brewerydb.Models;
using Brewerydb.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Brewerydb.Tests
{
	[TestClass]
	public class BeerInfoAdapterFixture
	{
		private readonly BeerInfo _fakeData;
		private readonly IWebClient _iWebClient;

		public BeerInfoAdapterFixture()
		{
			_fakeData = FakeBeerData.GetBeerData();
			_iWebClient = Substitute.For<IWebClient>();
		}


		[TestMethod]
		public void GetAll_method_of_BeerInfoAdapter_should_return_all_records()
		{
			//Arrange
			_iWebClient
				.GetBeers(Arg.Any<int>())
				.Returns(_fakeData);

			var request = new RequestBase
			{
				Skip = 0,
				Take = 100
			};

			//Act
			var adapter = new BeerInfoAdapter(_iWebClient);
			var response = adapter.GetAll(request);

			//assert
			Assert.AreEqual(_fakeData.Data.Count, response.Result.Data.Count);
		}

		[TestMethod]
		public void GetAll_method_of_BeerInfoAdapter_should_return_searched_records()
		{
			//Arrange
			_iWebClient
				.GetBeers(Arg.Any<int>())
				.Returns(_fakeData);

			var request = new RequestBase
			{
				Skip = 0,
				Take = 100,
				Token = "Beer 99"
			};

			var expected = _fakeData.Data.Search(request.Token);

			//Act
			var adapter = new BeerInfoAdapter(_iWebClient);
			var response = adapter.GetAll(request);

			//assert
			Assert.AreEqual(expected.Count, response.Result.Data.Count);
		}

		[TestMethod]
		public void GetAll_method_of_BeerInfoAdapter_should_return_columnwise_filtered_records()
		{
			//Arrange
			_iWebClient
				.GetBeers(Arg.Any<int>())
				.Returns(_fakeData);

			var request = new RequestBase
			{
				Skip = 0,
				Take = 100,
				Filters = new[] {new Filter("id", "55")}
			};

			var expected = _fakeData.Data.Filter(request.Filters);

			//Act
			var adapter = new BeerInfoAdapter(_iWebClient);
			var response = adapter.GetAll(request);

			//assert
			Assert.AreEqual(expected.Count, response.Result.Data.Count);
		}

		[TestMethod]
		public void GetAll_method_of_BeerInfoAdapter_should_return_searched_and_filtered_records()
		{
			//Arrange
			_iWebClient
				.GetBeers(Arg.Any<int>())
				.Returns(_fakeData);

			var request = new RequestBase
			{
				Skip = 0,
				Take = 100,
				Token = "Beer 51", // Beer 5,Beer 51,...,Beer 59 => 11
				Filters = new[] {new Filter("name", "51") } // 51 => 1
			};

			var expected = _fakeData.Data.Search(request.Token).Filter(request.Filters);

			//Act
			var adapter = new BeerInfoAdapter(_iWebClient);
			var response = adapter.GetAll(request) as ResponseBase<BeerInfo>;

			//assert
			Assert.AreEqual(expected.Count, response.Result.Data.Count);
		}

		[TestMethod]
		public void GetAll_method_of_BeerInfoAdapter_should_return_sorted_records_desc()
		{
			//Arrange
			_iWebClient
				.GetBeers(Arg.Any<int>())
				.Returns(_fakeData);

			var request = new RequestBase
			{
				Skip = 0,
				Take = 100,
				Token = "Beer 55",
				Sort = new Sort("id", SortOrder.Desc)
			};

			var expected = _fakeData.Data.Search(request.Token).Sort(request.Sort);

			//Act
			var adapter = new BeerInfoAdapter(_iWebClient);
			var response = adapter.GetAll(request);

			//assert
			Assert.AreEqual(expected.Count, response.Result.Data.Count);
			Assert.IsTrue(string.CompareOrdinal(response.Result.Data[0].Id, response.Result.Data[1].Id) >= 1);
		}

		[TestMethod]
		public void GetAll_method_of_BeerInfoAdapter_should_return_sorted_records_asc()
		{
			//Arrange
			_iWebClient
				.GetBeers(Arg.Any<int>())
				.Returns(_fakeData);

			var request = new RequestBase
			{
				Skip = 0,
				Take = 100,
				Token = "Beer 55",
				Sort = new Sort("id", SortOrder.Asc)
			};

			var expected = _fakeData.Data.Search(request.Token).Sort(request.Sort);

			//Act
			var adapter = new BeerInfoAdapter(_iWebClient);
			var response = adapter.GetAll(request);

			//assert
			Assert.AreEqual(expected.Count, response.Result.Data.Count);
			Assert.IsTrue(string.CompareOrdinal(response.Result.Data[0].Id, response.Result.Data[1].Id) <= -1);
		}
	}
}