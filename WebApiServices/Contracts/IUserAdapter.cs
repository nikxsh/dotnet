using System;
using System.Collections.Generic;
using WebApiServices.Helper;
using WebApiServices.Models;

namespace WebApiServices.Contracts
{
    public interface IUserAdapter
    {
        ResponseBase<IEnumerable<User>> GetUsers(RequestBase<PagingRequest> request);
        ResponseBase<IEnumerable<KeyValuePair<Guid, string>>> GlobalSearch(RequestBase<string> request);
        ResponseBase<int> GetUserCount(RequestBase request);
        ResponseBase<User> GetUserById(RequestBase<Guid> request);
        ResponseBase SaveUser(RequestBase<User> request);
        ResponseBase EditUser(RequestBase<User> request);
        ResponseBase DeleteUser(RequestBase<Guid> request);
    }
}