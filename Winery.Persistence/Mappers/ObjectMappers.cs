using static WineryStore.Persistence.Datastore.WineryContext;
using Contract = WineryStore.Contracts;

namespace WineryStore.Persistence
{
	public static class ObjectMappers
	{
		public static Contract.Winery MapToWineryContract(this Winery winery)
		{
			return new Contract.Winery
			{
				Id = winery.Id,
				Name = winery.Name,
				Region = winery.Region,
				Country = winery.Country
			};
		}

		public static Contract.Wine MapToWineContract(this Wine wine)
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
				IssueDate = wine.IssueDate,
				Note = wine.Note
			};
		}

		public static Winery MapToWineryPersistence(this Contract.Winery winery)
		{
			return new Winery
			{
				Id = winery.Id,
				Name = winery.Name,
				Region = winery.Region,
				Country = winery.Country
			};
		}

		public static Wine MapToWinePersistence(this Contract.Wine wine)
		{
			return new Wine
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
				IssueDate = wine.IssueDate,
				Note = wine.Note
			};
		}
	}
}