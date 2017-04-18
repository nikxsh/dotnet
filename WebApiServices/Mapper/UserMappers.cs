using System.Collections.Generic;
using System.Linq;

namespace WebApiServices.Mapper
{
    public static class UserMappers
    {
        public static IEnumerable<Models.User> ToAPIUsers(this IEnumerable<EFDataStorage.Entities.User> Users)
        {
            return Users.Select(x => x.ToAPIUser());
        }

        public static Models.User ToAPIUser(this EFDataStorage.Entities.User User)
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
    }
}