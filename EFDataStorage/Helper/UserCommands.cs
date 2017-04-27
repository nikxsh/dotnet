using EFDataStorage.Entities;
using System;

namespace EFDataStorage.Helper
{
    public class SaveUser
    {
        public User NewUser { get; set; }
    }

    public class EditUser
    {
        public User EditedUser { get; set; }
    }

    public class DeleteUser
    {
        public Guid UserId { get; set; }
    }
}
