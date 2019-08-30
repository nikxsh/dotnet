using AutoMapper;
using WineryStore.API.Models;

namespace WineryStore.API.AutoMapper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Contracts.Winery, WineryDTO>().ReverseMap();
			CreateMap<Contracts.Wine, WineDTO>().ReverseMap();
			CreateMap<Persistence.Datastore.WineryContext.Winery, Contracts.Winery>().ReverseMap();
			CreateMap<Persistence.Datastore.WineryContext.Wine, Contracts.Wine>().ReverseMap();
		}
	}
}