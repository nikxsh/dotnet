using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WineryStore.API.Mappers;
using WineryStore.API.Models;
using WineryStore.Contracts.Utils;

namespace WineryStore.API.Controllers
{
	[Route("api/wines")]
	[ApiController]
	public class WineController : ControllerBase
	{
		private readonly Persistence.IWineRepository wineRepository;

		public WineController(Persistence.IWineRepository wineRepository)
		{
			this.wineRepository = wineRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Get(
			Guid WineryId,
			string token = "",
			int skip = 0,
			int take = 10
			)
		{
			try
			{
				var request = new Request<Guid>
				{
					Token = token,
					Skip = skip,
					Take = take,
					Data = WineryId
				};

				var allWines = await wineRepository.GetAllWinesAsync(request);

				return Ok(allWines);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("{wineId:Guid}")]
		public async Task<IActionResult> Get(Guid wineId)
		{
			try
			{
				var request = new Request<Guid>
				{
					Data = wineId
				};

				var existingWine = await wineRepository.WineExistsAsync(request);

				if (!existingWine.Result)
					return NotFound($"Wine with Id {wineId} does not exists.");

				var wine = await wineRepository.GetWineByIdAsync(request);
				return Ok(wine);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post(WineDTO request)
		{
			try
			{
				var response = await wineRepository.AddWineAsync(request.MapToWineContract());

				if (response.Result != Guid.Empty)
				{
					request.Id = response.Result;
					return Created($"api/wines/{request.Id}", request);
				}
				else
					return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Add Wine!");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put(WineDTO request)
		{
			try
			{
				var existingWine = await wineRepository.WineExistsAsync(
					new Request<Guid>
					{
						Data = request.Id
					});

				if (!existingWine.Result)
					return NotFound($"Wine with Id {request.Id} does not exists.");

				var response = await wineRepository.UpdateWineAsync(request.MapToWineContract());

				if (response.Result > 0)
					return Ok(request);
				else
					return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Update Wine!");

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{wineId:Guid}")]
		public async Task<IActionResult> Delete(Guid wineId)
		{
			try
			{
				var existingWine = await wineRepository.GetWineByIdAsync(
					new Request<Guid>
					{
						Data = wineId
					});

				if (existingWine.Result == null)
					return NotFound($"Wine with Id {wineId} does not exists.");

				var response = await wineRepository.RemoveWineAsync(new Request<Guid> { Data = wineId });

				if (response.Result > 0)
					return Ok(wineId);
				else
					return StatusCode(StatusCodes.Status500InternalServerError, "Failed to remove Wine!");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}