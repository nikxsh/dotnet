using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
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
			_wineryRepository
				.GetAllWineriesAsync(Arg.Any<Request>())
				.Returns(Task.FromResult(new PagedResponse<IEnumerable<Winery>>())
				);

			// Act
			var okResult = _controller.Get().Result;

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(okResult);
		}

		[TestCase]
		public void Get_should_return_all_wineries()
		{
			// Arrange
			_wineryRepository
				.GetAllWineriesAsync(Arg.Any<Request>())
				.Returns(Task.FromResult(
					new PagedResponse<IEnumerable<Winery>>
					{
						Result = new List<Winery> { new Winery(), new Winery() },
						Total = 2
					})
				);

			// Act
			var okResult = _controller.Get().Result as OkObjectResult;

			// Assert
			Assert.NotNull(okResult);
			var resultSet = okResult.Value as PagedResponse<IEnumerable<Winery>>;
			Assert.NotNull(resultSet);
			Assert.AreEqual(2, resultSet.Result.Count());
			Assert.AreEqual(2, resultSet.Total);
		}

		[TestCase]
		public void Get_by_id_should_return_ok()
		{
			// Arrange
			_wineryRepository
				.GetWinerybyIdAsync(Arg.Any<Request<Guid>>())
				.Returns(Task.FromResult(
					new Response<Winery>
					{
						Result = new Winery()
					})
				);

			// Act
			var okResult = _controller.Get(Guid.Empty).Result;

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(okResult);
		}

		[TestCase]
		public void Get_by_id_should_return_not_found()
		{
			// Arrange
			_wineryRepository
				.GetWinerybyIdAsync(Arg.Any<Request<Guid>>())
				.Returns(Task.FromResult(
					new Response<Winery>
					{
						Result = null
					})
				);

			// Act
			var okResult = _controller.Get(Guid.Empty).Result;

			// Assert
			Assert.IsInstanceOf<NotFoundObjectResult>(okResult);
		}

		[TestCase]
		public void Get_by_id_should_return_winery()
		{
			// Arrange
			_wineryRepository
				.GetWinerybyIdAsync(Arg.Any<Request<Guid>>())
				.Returns(Task.FromResult(
					new Response<Winery>
					{
						Result = new Winery()
					})
				);

			// Act
			var okResult = _controller.Get(Guid.Empty).Result as OkObjectResult;

			// Assert
			Assert.NotNull(okResult);
			var resultSet = okResult.Value as Response<Winery>;
			Assert.NotNull(resultSet);
		}
	}
}