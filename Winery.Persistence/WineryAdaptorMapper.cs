using System;
using System.Collections.Generic;
using System.Linq;
using WineryStore.Contracts;

namespace WineryStore.Persistence
{
	public static class WineAdapterMapper
	{
		public static IEnumerable<Winery> SearchWineries(this IEnumerable<Winery> lstWineries, string token)
		{
			if (!string.IsNullOrEmpty(token))
				return lstWineries
						 .Where(x =>
								 x.Id.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								(x.Name?.Contains(token, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
								x.Region.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Country.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase)
						)
						.ToList();

			return lstWineries;
		}

		public static IEnumerable<Winery> FilterWineries(this IEnumerable<Winery> lstWineries, Filter[] filters)
		{
			foreach (var filter in filters.EmptyIfNull())
			{
				if (filter == null || string.IsNullOrEmpty(filter.Column) || string.IsNullOrEmpty(filter.Token)) continue;

				switch (filter.Column.ToLower())
				{
					case "Id":
						lstWineries = lstWineries.Where(x => x.Id.ToString().Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)).ToList();
						break;

					case "Name":
						lstWineries = lstWineries.Where(x => x.Name.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)).ToList();
						break;
				}
			}

			return lstWineries;
		}

		public static IEnumerable<Winery> SortWineries(this IEnumerable<Winery> lstWineries, Sort sort)
		{
			if (sort != null && !string.IsNullOrEmpty(sort.Column) && sort.Order != SortOrder.None)
			{
				if (sort.Order == SortOrder.Asc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWineries = lstWineries.OrderBy(x => x.Id).ToList();
							break;

						case "Name":
							lstWineries = lstWineries.OrderBy(x => x.Name).ToList();
							break;
					}
				}
				else if (sort.Order == SortOrder.Desc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWineries = lstWineries.OrderByDescending(x => x.Id).ToList();
							break;

						case "Name":
							lstWineries = lstWineries.OrderByDescending(x => x.Name).ToList();
							break;
					}
				}
			}
			else
				lstWineries = lstWineries.OrderBy(x => x.Id).ToList();

			return lstWineries;
		}

		public static IEnumerable<Wine> SearchWines(this IEnumerable<Wine> lstWines, string token)
		{
			if (!string.IsNullOrEmpty(token))
				return lstWines
						 .Where(x =>
								 x.Id.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								(x.Name?.Contains(token, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
								x.Rank.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Score.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.IssueDate.Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Price.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
								x.Vintage.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase)
						)
						.ToList();

			return lstWines;
		}

		public static IEnumerable<Wine> FilterWines(this IEnumerable<Wine> lstWines, Filter[] filters)
		{
			foreach (var filter in filters.EmptyIfNull())
			{
				if (filter == null || string.IsNullOrEmpty(filter.Column) || string.IsNullOrEmpty(filter.Token)) continue;

				switch (filter.Column.ToLower())
				{
					case "Id":
						lstWines = lstWines.Where(x => x.Id.ToString().Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)).ToList();
						break;

					case "Name":
						lstWines = lstWines.Where(x => x.Name.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)).ToList();
						break;
				}
			}

			return lstWines;
		}

		public static IEnumerable<Wine> SortWines(this IEnumerable<Wine> lstWines, Sort sort)
		{
			if (sort != null && !string.IsNullOrEmpty(sort.Column) && sort.Order != SortOrder.None)
			{
				if (sort.Order == SortOrder.Asc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWines = lstWines.OrderBy(x => x.Id).ToList();
							break;

						case "Name":
							lstWines = lstWines.OrderBy(x => x.Name).ToList();
							break;
					}
				}
				else if (sort.Order == SortOrder.Desc)
				{
					switch (sort.Column.ToLower())
					{
						case "Id":
							lstWines = lstWines.OrderByDescending(x => x.Id).ToList();
							break;

						case "Name":
							lstWines = lstWines.OrderByDescending(x => x.Name).ToList();
							break;
					}
				}
			}
			else
				lstWines = lstWines.OrderBy(x => x.Id).ToList();

			return lstWines;
		}
	}
}