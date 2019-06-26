using System;

namespace Brewerydb.Shared
{
	public class ResponseBase
	{
		public string Message { get; set; }

		public Exception Exception { get; set; }
	}

	public class ResponseBase<T> : ResponseBase
	{
		public T Result { get; set; }
	}
}