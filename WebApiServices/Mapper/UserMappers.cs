using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiServices.Mapper
{
    public static class UserMappers
    {
        public static IEnumerable<Models.User> BuildAPIUserList(this IEnumerable<EFDataStorage.Entities.User> Users)
        {
            return Users.Select(x => x.BuildAPIUser());
        }

        public static Models.User BuildAPIUser(this EFDataStorage.Entities.User User)
        {
            return new Models.User
            {
                Id = User.Id,
                UserName = User.UserName,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Dob = User.Dob.ToString()
            };
        }

        public static EFDataStorage.Entities.User BuildRepositoryUser(this Models.User User)
        {
            return new EFDataStorage.Entities.User
            {
                Id = User.Id,
                UserName = User.UserName,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Dob = DateTime.Parse(User.Dob)
            };
        }
    }
}