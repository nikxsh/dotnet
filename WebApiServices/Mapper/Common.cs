using System;
using System.Collections.Generic;
using WebApiServices.Helper;

namespace WebApiServices.Mapper
{
    public static class AdapterResponseMapper
    {
        public static ResponseBase<T> ToAdapterResponseBase<T>(this Exception ex)
        {
            //Logger.LogException(ex);
            return new ResponseBase<T>
            {
                Exception = ex,
                Status = false,
                Errors = new List<Error> { new Error { ErrorMessage = ex.Message } }
            };
        }
    }

}