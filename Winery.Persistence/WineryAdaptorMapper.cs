using System;
using System.Linq;
using WineryStore.Contracts;

namespace WineryStore.Persistence
{
	public static class WineryAdapterMapper
	{
		public static IQueryable<Winery> SearchWineries(this IQueryable<Winery> lstWineries, string token)
		{
			if (!string.IsNullOrEmpty(token))
				return lstWineries
						 .Where(x =>
								x.Id.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Name.Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Region.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Country.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase)
						);

			return lstWineries;
		}

		public static IQueryable<Winery> FilterWineries(this IQueryable<Winery> lstWineries, Filter[] filters)
		{
			foreach (var filter in filters.EmptyIfNull())
			{
				if (filter == null || string.IsNullOrEmpty(filter.Column) || string.IsNullOrEmpty(filter.Token)) continue;

				switch (filter.Column.ToLower())
				{
					case "Id":
						lstWineries = lstWineries.Where(x => x.Id.ToString().Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase));
						break;

					case "Name":
						lstWineries = lstWineries.Where(x => x.Name.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase));
						break;
				}
			}

			return lstWineries;
		}

		public static IQueryable<Winery> SortWineries(this IQueryable<Winery> lstWineries, Sort sort)
		{
			if (sort != null && !string.IsNullOrEmpty(sort.Column) && sort.Order != SortOrder.None)
			{
				if (sort.Order == SortOrder.Asc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWineries = lstWineries.OrderBy(x => x.Id);
							break;

						case "Name":
							lstWineries = lstWineries.OrderBy(x => x.Name);
							break;
					}
				}
				else if (sort.Order == SortOrder.Desc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWineries = lstWineries.OrderByDescending(x => x.Id);
							break;

						case "Name":
							lstWineries = lstWineries.OrderByDescending(x => x.Name);
							break;
					}
				}
			}
			else
				lstWineries = lstWineries.OrderBy(x => x.Id);

			return lstWineries;
		}

		public static IQueryable<Wine> SearchWines(this IQueryable<Wine> lstWines, string token)
		{
			if (!string.IsNullOrEmpty(token))
				return lstWines
						 .Where(x =>
								x.Id.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Name.Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Rank.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Score.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.IssueDate.Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Price.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Vintage.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase)
						);

			return lstWines;
		}

		public static IQueryable<Wine> FilterWines(this IQueryable<Wine> lstWines, Filter[] filters)
		{
			foreach (var filter in filters.EmptyIfNull())
			{
				if (filter == null || string.IsNullOrEmpty(filter.Column) || string.IsNullOrEmpty(filter.Token)) continue;

				switch (filter.Column.ToLower())
				{
					case "Id":
						lstWines = lstWines.Where(x =>
							x.Id.ToString().Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)
						);
						break;

					case "Name":
						lstWines = lstWines.Where(x =>
							x.Name.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)
						);
						break;
				}
			}

			return lstWines;
		}

		public static IQueryable<Wine> SortWines(this IQueryable<Wine> lstWines, Sort sort)
		{
			if (sort != null && !string.IsNullOrEmpty(sort.Column) && sort.Order != SortOrder.None)
			{
				if (sort.Order == SortOrder.Asc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWines = lstWines.OrderBy(x => x.Id);
							break;

						case "Name":
							lstWines = lstWines.OrderBy(x => x.Name);
							break;
					}
				}
				else if (sort.Order == SortOrder.Desc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWines = lstWines.OrderByDescending(x => x.Id);
							break;

						case "Name":
							lstWines = lstWines.OrderByDescending(x => x.Name);
							break;
					}
				}
			}
			else
				lstWines = lstWines.OrderBy(x => x.Id);

			return lstWines;
		}
	}
}