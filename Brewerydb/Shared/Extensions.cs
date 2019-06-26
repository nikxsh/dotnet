using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brewerydb.Shared
{
	public static class Extensions
	{
		public static Uri ToUri(this string path, int page = 0)
		{
			var uriBuilder = new UriBuilder
			{
				Scheme = "http",
				Host = BeerConfiguration.BaseUrl,
				Path = path
			};

			var query = HttpUtility.ParseQueryString(string.Empty);
			query["key"] = BeerConfiguration.ApiKey;
			query["p"] = page.ToString();
			uriBuilder.Query = query.ToString();
			return uriBuilder.Uri;
		}

		public static bool Contains(this string source, string token, StringComparison culture)
		{
			return source?.IndexOf(token, culture) >= 0;
		}

		public static ResponseBase<T> ToAdapterResponseBase<T>(this Exception ex)
		{
			//We can implement logger here 
			return new ResponseBase<T>
			{
				Exception = ex
			};
		}

		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}
	}
}