using EFDataStorage.Entities;
using EFDataStorage.Helper;
using EFDataStorage.Patterns;
using System;
using System.Collections.Generic;

namespace EFDataStorage.Contracts
{
    public interface IUserRepository :
        ISelect<PagingRequest, IEnumerable<User>>,
        ISelect<int>,
        ISelect<GetUserById, User>,
        IExecute<SaveUser, ExecuteNonQueryResults>,
        IExecute<EditUser, ExecuteNonQueryResults>,
        IExecute<DeleteUser, ExecuteNonQueryResults>
    {}
}
