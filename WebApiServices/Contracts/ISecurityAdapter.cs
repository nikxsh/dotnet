using WebApiServices.Helper;
using WebApiServices.Models;

namespace WebApiServices.Contracts
{
    public interface ISecurityAdapter
    {
        ResponseBase<LoginResponse> Authenticate(RequestBase<LoginRequest> request);
    }
}
