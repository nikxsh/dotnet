using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WineryStore.API.Controllers
{
	[Route("api/winery/{wineryId}/wines")]
	[ApiController]
	public class WineController : ControllerBase
	{
		private readonly Persistence.IWineryRepository wineryRepository;
		private readonly IMapper mapper;

		public WineController(Persistence.IWineryRepository wineryRepository, IMapper mapper)
		{
			this.wineryRepository = wineryRepository;
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
				var winesByWinery = await wineryRepository.GetAllWinesFromWineryAsync(request);
				return Ok(winesByWinery);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("{wineId}")]
		public async Task<IActionResult> Get(Guid wineryId, Guid wineId)
		{
			try
			{
				var request = new Contracts.Request<Tuple<Guid, Guid>>
				{
					Data = new Tuple<Guid, Guid>(wineryId, wineId)
				};

				var winesByWinery = await wineryRepository.GetWineFromWineryByIdAsync(request);
				return Ok(winesByWinery);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}