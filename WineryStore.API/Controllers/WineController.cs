using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WineryStore.API.Models;

namespace WineryStore.API.Controllers
{
	[Route("api/wines")]
	[ApiController]
	public class WineController : ControllerBase
	{
		private readonly Persistence.IWineRepository wineRepository;
		private readonly IMapper mapper;

		public WineController(Persistence.IWineRepository wineRepository, IMapper mapper)
		{
			this.wineRepository = wineRepository;
			this.mapper = mapper;
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
				var request = new Contracts.Request<Guid>
				{
					Token = token,
					Skip = skip,
					Take = take,
					Data = WineryId
				};
				var winesByWinery = await wineRepository.GetAllWinesAsync(request);
				return Ok(winesByWinery);
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
				var request = new Contracts.Request<Guid>
				{
					Data = wineId
				};
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
				var createdWine = await wineRepository.AddWineAsync(mapper.Map<Contracts.Wine>(request));
				return Created($"api/wines/{createdWine.Result.Id}", createdWine);
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
				var existingWine = await wineRepository.GetWineByIdAsync(
					new Contracts.Request<Guid>
					{
						Data = request.Id
					});

				if (existingWine.Result == null)
					return NotFound("Given resource does not exists.");

				var udatedWine = await wineRepository.UpdateWineAsync(mapper.Map<Contracts.Wine>(request));
				return Ok(udatedWine);
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
					new Contracts.Request<Guid>
					{
						Data = wineId
					});

				if (existingWine.Result == null)
					return NotFound("Given resource does not exists.");

				var response = await wineRepository.RemoveWineAsync(new Contracts.Request<Guid> { Data = wineId });
				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}