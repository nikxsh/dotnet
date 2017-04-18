using System;
using System.Collections.Generic;

namespace WebApiServices.Helper
{
    public static class Global
    {
        public static ResponseBase ToErrorResponse(this Exception ex)
        {
            int errorCode = 0;
            if (ex.HResult >= 100 && ex.HResult <= 600)
            {
                errorCode = ex.HResult;
            }
            else
            {
                errorCode = 520;
            }

            //Logger.LogException(ex);

            ResponseBase responseBase = new ResponseBase { Status = false };

            responseBase.Errors = new List<Error>
            {
                new Error
                {
                    ErrorCode = errorCode,
                    ErrorMessage = ex.Message,
                    Header = "Error"
                }
            };
            return responseBase;
        }
    }
}