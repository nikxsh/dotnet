using EFDataStorage.Entities;
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
        void SaveUser(User User);
        void EditUser(User User);
        void DeleteUser(Guid UserId);
    }
}
