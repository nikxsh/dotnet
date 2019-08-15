using System;
using System.Collections.Generic;
using System.Linq;
using Winery.Contracts;

namespace Winery.Persistence
{
    public static class WineryAdapterMapper
    {
        public static List<Wine> Search(this List<Wine> lstWines, string token)
        {
            if (!string.IsNullOrEmpty(token))
                return lstWines
                        .Where(x =>
                             x.Id.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
                            (x.Name?.Contains(token, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
                            x.Rank.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
                            x.Score.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
                            x.Year.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
                            x.Price.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
                            x.Country.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase) ||
                            x.Type.ToString().Contains(token, StringComparison.InvariantCultureIgnoreCase)
                        )
                        .ToList();

            return lstWines;
        }

        public static List<Wine> Filter(this List<Wine> lstWines, Filter[] filters)
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

        public static List<Wine> Sort(this List<Wine> lstWines, Sort sort)
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