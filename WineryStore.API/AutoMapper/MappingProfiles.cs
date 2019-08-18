using AutoMapper;
using System;
using WineryStore.API.Models;
using WineryStore.Contracts;

namespace WineryStore.API.AutoMapper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Winery, WineryDTO>().ReverseMap();
			CreateMap<Wine, WineDTO>().ReverseMap();
		}
	}
}
