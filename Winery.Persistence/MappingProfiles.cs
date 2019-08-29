using AutoMapper;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Winery, Contracts.Winery>().ReverseMap();
			CreateMap<Wine, Contracts.Wine>().ReverseMap();
		}
	}
}