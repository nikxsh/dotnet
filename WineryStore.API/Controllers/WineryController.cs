using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WineryStore.API.Models;

namespace WineryStore.API.Controllers
{
	[Route("api/winery")]
	[ApiController]
	public class WineryController : ControllerBase
	{
		private readonly Persistence.IWineryRepository wineryRepository;
		private readonly IMapper mapper;

		public WineryController(Persistence.IWineryRepository wineryRepository, IMapper mapper)
		{
			this.wineryRepository = wineryRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get(
			string token = "",
			int skip = 0,
			int take = 10
			)
		{
			var request = new Contracts.Request
			{
				Token = token,
				Skip = skip,
				Take = take
			};

			try
			{
				var wineries = await wineryRepository.GetAllWineriesAsync(request);
				return Ok(wineries);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}


		[HttpGet("{wineryId:Guid}")]
		public async Task<IActionResult> Get(Guid wineryId)
		{
			try
			{
				var winery = await wineryRepository.GetWinerybyIdAsync(
					new Contracts.Request<Guid>
					{
						Data = wineryId
					});

				if (winery.Result == null)
					return NotFound("Given resource does not exists.");
				else
					return Ok(winery);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post(WineryDTO request)
		{
			try
			{
				var createdWinery = await wineryRepository.AddWineryAsync(mapper.Map<Contracts.Winery>(request));
				return Created($"api/winery/{createdWinery.Result.Id}", createdWinery);
			}
			catch (Exception ex)
			{
				if (ex is FormatException)
					return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
				else
					return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put(WineryDTO request)
		{
			try
			{
				var winery = await wineryRepository.WineryExistsAsync(
					new Contracts.Request<Guid>
					{
						Data = request.Id
					});

				if (!winery.Result)
					return NotFound("Given resource does not exists.");

				var updatedWinery = await wineryRepository.UpdateWineryAsync(mapper.Map<Contracts.Winery>(request));
				return Ok(updatedWinery);
			}
			catch (Exception ex)
			{
				if (ex is FormatException)
					return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
				else
					return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPatch("{wineryId}")]
		public async Task<IActionResult> Patch(Guid wineryId, [FromBody] JsonPatchDocument<WineryDTO> wineryPatch)
		{
			try
			{
				var winery = await wineryRepository.WineryExistsAsync(
					new Contracts.Request<Guid>
					{
						Data = wineryId
					});

				if (!winery.Result)
					return NotFound("Given resource does not exists.");

				WineryDTO wineryDTO = mapper.Map<WineryDTO>(winery.Result);
				wineryPatch.ApplyTo(wineryDTO);

				return Ok(wineryDTO);
			}
			catch (Exception ex)
			{
				if (ex is FormatException)
					return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
				else
					return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{wineryId}")]
		public async Task<IActionResult> Delete(Guid wineryId)
		{
			try
			{
				var winery = await wineryRepository.WineryExistsAsync(
					new Contracts.Request<Guid>
					{
						Data = wineryId
					});

				if (!winery.Result)
					return NotFound("Given resource does not exists.");

				var response = await wineryRepository.RemoveWineryAsync(new Contracts.Request<Guid> { Data = wineryId });
				
				return Ok(response);
			}
			catch (Exception ex)
			{
				if (ex is FormatException)
					return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
				else
					return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

	}
}