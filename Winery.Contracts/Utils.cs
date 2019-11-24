using System;

namespace WineryStore.Contracts.Utils
{
	public class Request
	{
		public string Token { get; set; }
		public Filter[] Filters { get; set; }
		public Sort Sort { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; } = 10;
	}

	public class Request<T> : Request
	{
		public T Data { get; set; }
	}

	public class Filter
	{
		public Filter(string column, string token)
		{
			this.Column = column;
			this.Token = token;
		}

		public string Column { get; set; }
		public string Token { get; set; }
	}

	public class Sort
	{
		public Sort(string column, SortOrder order)
		{
			this.Column = column;
			this.Order = order;
		}
		public string Column { get; set; }
		public SortOrder Order { get; set; }
	}

	public enum SortOrder { None, Asc, Desc }

	public class Response<T>
	{
		public T Result { get; set; }
	}

	public class PagedResponse<T> : Response<T>
	{
		public int Total { get; set; }
	}
}