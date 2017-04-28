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

        public static EFDataStorage.Helper.SaveUser BuildAddUserCommand(this Models.User User)
        {
            return new EFDataStorage.Helper.SaveUser
            {
                NewUser = new EFDataStorage.Entities.User
                {
                    Id = User.Id,
                    UserName = User.UserName,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Email = User.Email,
                    Dob = DateTime.Parse(User.Dob)
                }
            };
        }

        public static EFDataStorage.Helper.EditUser BuildEditUserCommand(this Models.User User)
        {
            return new EFDataStorage.Helper.EditUser
            {
                EditedUser = new EFDataStorage.Entities.User
                {
                    Id = User.Id,
                    UserName = User.UserName,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Email = User.Email,
                    Dob = DateTime.Parse(User.Dob)
                }
            };
        }
        public static EFDataStorage.Helper.PagingRequest ToRepositoryPagingRequest(this Models.PagingRequest request)
        {
            return new EFDataStorage.Helper.PagingRequest
            {
                Skip = request.PageSize * (request.PageNumber - 1),
                Take = request.PageSize,
                SearchString = request.SearchString
            };
        }
    }
}