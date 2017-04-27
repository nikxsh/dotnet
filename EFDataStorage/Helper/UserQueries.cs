using EFDataStorage.Entities;
using EFDataStorage.Patterns;
using System;

namespace EFDataStorage.Helper
{
    public class GetUserById : QueryFor<User>
    {
        public Guid UserId { get; set; }
    }
}
