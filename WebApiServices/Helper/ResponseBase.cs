using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebApiServices.Helper
{
    public class ResponseBase
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public IEnumerable<Error> Errors { get; set; }

        public bool RefreshToken { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }
    }

    public class ResponseBase<T> : ResponseBase
    {
        public T ResponseData { get; set; }
    }
}