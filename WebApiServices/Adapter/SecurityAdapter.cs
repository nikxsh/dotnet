using System;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Models;

namespace WebApiServices.Adapter
{
    public class SecurityAdapter : ISecurityAdapter
    {
        private readonly ISecurityAdapter _iSecurityAdapter;

        public SecurityAdapter(ISecurityAdapter ISecurityAdapter)
        {
            _iSecurityAdapter = ISecurityAdapter;
        }
        public ResponseBase<LoginResponse> Authenticate(RequestBase<LoginRequest> request)
        {
            throw new NotImplementedException();
        }
    }
}