using System;
using System.Collections.Generic;
using Winery.Contracts;
using System.Linq;

namespace Winery.Persistence
{
    public class WineryAdaptor
    {
        public WineryAdaptor()
        {
        }

        public ResponseBase<List<Wine>> GetAll(RequestBase request)
        {
            var response = new ResponseBase<List<Wine>>();

            try
            {
                response.Result = WineData.Get();
                response.Total = response.Result.Count;

                response.Result = response.Result.Search(request.Token);
                response.Result = response.Result.Filter(request.Filters);
                response.Result = response.Result.Sort(request.Sort);
                response.Result = response.Result.Skip(request.Skip).Take(request.Take).ToList();
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<List<Wine>>();
                response.Exception = ex;
            }
            return response;
        }
    }
}
