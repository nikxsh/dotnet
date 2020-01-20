using System;
using WineryStore.API.Models;
using Contract = WineryStore.Contracts;

namespace WineryStore.API.Mappers
{
	public static class Mappers
	{
		public static Contract.Winery MapToWineryContract(this WineryDTO winery)
		{
			return new Contract.Winery
			{
				Id = winery.Id,
				Name = winery.Name,
				Region = winery.Region,
				Country = winery.Country
			};
		}

		public static Contract.Wine MapToWineContract(this WineDTO wine)
		{
			return new Contract.Wine
			{
				Id = wine.Id,
				WineryId = wine.WineryId,
				Name = wine.Name,
				Price = wine.Price,
				Color = (Contract.WineColor)wine.Color,
				Score = wine.Score,
				Rank = wine.Rank,
				RankYear = wine.RankYear,
				Vintage = wine.Vintage,
				IssueDate = DateTime.Parse(wine.IssueDate),
				Note = wine.Note
			};
		}

		public static WineryDTO MapToWineryPersistence(this Contract.Winery winery)
		{
			return new WineryDTO
			{
				Id = winery.Id,
				Name = winery.Name,
				Region = winery.Region,
				Country = winery.Country
			};
		}

		public static WineDTO MapToWinePersistence(this Contract.Wine wine)
		{
			return new WineDTO
			{
				Id = wine.Id,
				WineryId = wine.WineryId,
				Name = wine.Name,
				Price = wine.Price,
				Color = (WineColor)wine.Color,
				Score = wine.Score,
				Rank = wine.Rank,
				RankYear = wine.RankYear,
				Vintage = wine.Vintage,
				IssueDate = wine.IssueDate.ToString(),
				Note = wine.Note
			};
		}
	}
}