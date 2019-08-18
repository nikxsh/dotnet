using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryStore.API.Controllers;
using WineryStore.Contracts;
using WineryStore.Persistence;

namespace WineryStore.Tests.API
{
	[TestFixture]
	public class WineryControllerFixture
	{
		private readonly IWineryRepository _wineryRepository;
		private readonly IMapper _mapper;
		private readonly WineryController _controller;


		public WineryControllerFixture()
		{
			_wineryRepository = Substitute.For<IWineryRepository>();
			_mapper = Substitute.For<IMapper>();

			_controller = new WineryController(_wineryRepository, _mapper);
		}

		[TestCase]
		public void Get_should_return_ok()
		{
			// Arrange
			var response = new Response<IEnumerable<Winery>>
			{
				Result = new List<Winery>
				{
					new Winery(),
					new Winery()
				}
			};

			_wineryRepository
				.GetAllWineriesAsync(Arg.Any<Request>())
				.Returns(Task.FromResult(response));

			// Act
			var okResult = _controller.Get().Result;

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(okResult);
		}

		[TestCase]
		public void Get_should_return_all_wineries()
		{
			// Arrange
			var response = new Response<IEnumerable<Winery>>
			{
				Result = new List<Winery>
				{
					new Winery(),
					new Winery()
				}
			};

			_wineryRepository
				.GetAllWineriesAsync(Arg.Any<Request>())
				.Returns(Task.FromResult(response));
			
			// Act
			var okResult = _controller.Get().Result as OkObjectResult;

			// Assert
			Assert.NotNull(okResult);
			var resultSet = okResult.Value as Response<IEnumerable<Winery>>;
			Assert.NotNull(resultSet);
			Assert.AreEqual(2, resultSet.Result.Count());
		}
	}
}
