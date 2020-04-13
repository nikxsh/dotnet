using System;
using System.Linq;
using WineryStore.Contracts.Helper;
using WineryStore.Contracts.Extensions;
using WineryStore.Contracts.Utils;
using static WineryStore.Persistence.Datastore.WineryContext;

namespace WineryStore.Persistence
{
	public static class WineryAdapterMapper
	{
		public static IQueryable<Winery> SearchWineries(this IQueryable<Winery> lstWineries, string token)
		{
			if (!string.IsNullOrEmpty(token))
				return lstWineries
						 .Where(x =>
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
					case "Name":
						lstWineries = lstWineries.Where(x => x.Name.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase));
						break;

					case "Region":
						lstWineries = lstWineries.Where(x => x.Region.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase));
						break;

					case "Country":
						lstWineries = lstWineries.Where(x => x.Country.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase));
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
						case "Name":
							lstWineries = lstWineries.OrderBy(x => x.Name);
							break;

						case "Region":
							lstWineries = lstWineries.OrderBy(x => x.Region);
							break;

						case "Country":
							lstWineries = lstWineries.OrderBy(x => x.Country);
							break;
					}
				}
				else if (sort.Order == SortOrder.Desc)
				{
					switch (sort.Column.ToLower())
					{
						case "Name":
							lstWineries = lstWineries.OrderByDescending(x => x.Name);
							break;

						case "Region":
							lstWineries = lstWineries.OrderByDescending(x => x.Region);
							break;

						case "Country":
							lstWineries = lstWineries.OrderByDescending(x => x.Country);
							break;
					}
				}
			}
			else
				lstWineries = lstWineries.OrderBy(x => x.Country);

			return lstWineries;
		}

		/// <summary>
		/// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/supported-and-unsupported-linq-methods-linq-to-entities?redirectedfrom=MSDN
		/// </summary>
		public static IQueryable<Wine> SearchWines(this IQueryable<Wine> lstWines, string token)
		{
			if (!string.IsNullOrEmpty(token))
			{
				token = token.ToLower();
				var colorTokens = Helper.ToEnumArray<Contracts.WineColor>()
					.Where(x => x.Value.Contains(token, StringComparison.InvariantCultureIgnoreCase))
					?.Select(y => y.Key);

				return lstWines
							 .Where(x =>
									x.Name.ToLower().Contains(token) ||
									colorTokens.Any(tok => tok == (int)x.Color) ||
									x.IssueDate.ToString().Contains(token) ||
									x.Price.ToString().Contains(token) ||
									x.Vintage.ToLower().Contains(token)
							  );
			}
			return lstWines;
		}

		public static IQueryable<Wine> FilterWines(this IQueryable<Wine> lstWines, Filter[] filters)
		{
			foreach (var filter in filters.EmptyIfNull())
			{
				if (filter == null || string.IsNullOrEmpty(filter.Column) || string.IsNullOrEmpty(filter.Token)) continue;

				filter.Token = filter.Token.ToLower();
				var colorTokens = Helper.ToEnumArray<Contracts.WineColor>()
					.Where(x => x.Value.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase))
					?.Select(y => y.Key);

				switch (filter.Column.ToLower())
				{
					case "name":
						lstWines = lstWines.Where(x =>
							x.Name.ToLower().Contains(filter.Token)
						);
						break;

					case "color":
						lstWines = lstWines.Where(x =>
							colorTokens.Any(y => y == (int)x.Color)
						);
						break;

					case "price":
						lstWines = lstWines.Where(x =>
							x.Price.ToString().ToLower().Contains(filter.Token)
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
						case "name":
							lstWines = lstWines.OrderBy(x => x.Name);
							break;

						//case "color":
						//	lstWines = lstWines.OrderBy(x => x.Color);
						//	break;

						case "price":
							lstWines = lstWines.OrderBy(x => x.Price);
							break;

						case "vintage":
							lstWines = lstWines.OrderBy(x => x.Vintage);
							break;
					}
				}
				else if (sort.Order == SortOrder.Desc)
				{
					switch (sort.Column.ToLower())
					{
						case "name":
							lstWines = lstWines.OrderByDescending(x => x.Name);
							break;

						//case "color":
						//	lstWines = lstWines.OrderByDescending(x => x.Color);
						//	break;

						case "price":
							lstWines = lstWines.OrderByDescending(x => x.Price);
							break;

						case "vintage":
							lstWines = lstWines.OrderByDescending(x => x.Vintage);
							break;
					}
				}
			}
			else
				lstWines = lstWines.OrderBy(x => x.Name);

			return lstWines;
		}
	}
}