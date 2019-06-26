using Brewerydb.Models;
using Brewerydb.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brewerydb.Adaptor
{
	public static class BeerInfoAdapterMapper
	{
		public static List<Beer> Search(this List<Beer> lstBeer, string token)
		{
			if (!string.IsNullOrEmpty(token))
				return lstBeer
						.Where(x =>
							x.Id.Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
							(x.Name?.Contains(token, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
							(x.Description != null && x.Description.Contains(token, StringComparison.InvariantCultureIgnoreCase)) ||
							(x.Ibu?.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
							(x.Abv?.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
							(x.Status?.Contains(token, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
							x.CreateDate.ToString(Constants.DateFormat).Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
							x.UpdateDate.ToString(Constants.DateFormat).Contains(token, StringComparison.InvariantCultureIgnoreCase)
						)
						.ToList();

			return lstBeer;
		}

		public static List<Beer> Filter(this List<Beer> lstBeer, Filter[] filters)
		{
			foreach (var filter in filters.EmptyIfNull())
			{
				if (filter == null || string.IsNullOrEmpty(filter.Column) || string.IsNullOrEmpty(filter.Token)) continue;

				switch (filter.Column.ToLower())
				{
					case Constants.Id:
						lstBeer = lstBeer.Where(x => x.Id.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)).ToList();
						break;

					case Constants.Name:
						lstBeer = lstBeer.Where(x => x.Name.Contains(filter.Token, StringComparison.InvariantCultureIgnoreCase)).ToList();
						break;
				}
			}

			return lstBeer;
		}

		public static List<Beer> Sort(this List<Beer> lstBeer, Sort sort)
		{
			if (sort != null && !string.IsNullOrEmpty(sort.Column) && sort.Order != SortOrder.None)
			{
				if (sort.Order == SortOrder.Asc)
				{
					switch (sort.Column.ToLower())
					{
						case Constants.Id:
							lstBeer = lstBeer.OrderBy(x => x.Id).ToList();
							break;

						case Constants.Name:
							lstBeer = lstBeer.OrderBy(x => x.Name).ToList();
							break;
					}
				}
				else if (sort.Order == SortOrder.Desc)
				{
					switch (sort.Column.ToLower())
					{
						case Constants.Id:
							lstBeer = lstBeer.OrderByDescending(x => x.Id).ToList();
							break;

						case Constants.Name:
							lstBeer = lstBeer.OrderByDescending(x => x.Name).ToList();
							break;
					}
				}
			}
			else
				lstBeer = lstBeer.OrderBy(x => x.Id).ToList();

			return lstBeer;
		}
	}
}