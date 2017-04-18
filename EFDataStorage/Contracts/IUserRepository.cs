using EFDataStorage.Entities;
using EFDataStorage.Helper;
using System;
using System.Collections.Generic;

namespace EFDataStorage.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(int PageSize, int PageNumber, string keyword);
        IEnumerable<KeyValuePair<Guid, string>> GlobalSearch(string keyword);
        int GetUserCount();
        User GetUserById(Guid Id);
        ExecuteNonQueryResults SaveUser(User User);
        ExecuteNonQueryResults EditUser(User User);
        ExecuteNonQueryResults DeleteUser(Guid UserId);
    }
}
