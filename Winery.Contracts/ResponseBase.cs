using System;

namespace Winery.Contracts
{
    public class ResponseBase
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public class ResponseBase<T> : ResponseBase
    {
        public T Result { get; set; }
        public int Total { get; set; }
}
}