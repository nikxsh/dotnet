using System;
using System.Collections.Generic;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Mapper;
using WebApiServices.Models;

namespace WebApiServices.Adapter
{
    public class SecurityAdapter : ISecurityAdapter
    {
        private readonly EFDataStorage.Contracts.ISecurityRepository _iSecurityAdapter;

        public SecurityAdapter(EFDataStorage.Contracts.ISecurityRepository ISecurityAdapter)
        {
            _iSecurityAdapter = ISecurityAdapter;
        }
        public ResponseBase<LoginResponse> Authenticate(RequestBase<LoginRequest> request)
        {
            var response = new ResponseBase<LoginResponse>();

            try
            {
                var result = _iSecurityAdapter.Select(new EFDataStorage.Helper.LoginRequest
                {
                    Username = request.Data.Username,
                    Password = request.Data.Password
                });
                var mapperResult = result.BuildAPILoginResponse();
                return new ResponseBase<LoginResponse> { Status = true, ResponseData = mapperResult };
            }
            catch (Exception ex)
            {
                response = ex.ToAdapterResponseBase<LoginResponse>();
                response.Errors = new List<Error> { new Error { ErrorCode = 520, ErrorMessage = ex.Message } };
                response.Status = false;
            }

            return response;
        }
    }
}