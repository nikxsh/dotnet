using System;
using System.Collections.Generic;
using System.Linq;
using WebApiServices.Adapter;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Models;

namespace UnitTesting.Fakes
{
    public class FakeUsersRepository : IUserAdapter
    {
        public List<User> FakeUsers = new List<User>
            {
                   new User
                   {
                        Id = new Guid("64FD49DE-6E0C-42DF-BDE9-067896D6937F"),
                        UserName = "Nik",
                        FirstName = "Nikhilesh",
                        LastName = "Shinde",
                        Email = "shinde.nikhilesh@gmail.com",
                        Dob =  DateTime.ParseExact("14/03/1990","dd/MM/yyyy", null).ToString()
                    },
                    new User
                    {
                        Id = new Guid("84A93DE5-E1F2-4847-87C5-43182DCDC781"),
                        UserName = "ravi",
                        FirstName = "Ravi",
                        LastName = "Singh",
                        Email = "ravi.singh@gmail.com",
                        Dob = DateTime.ParseExact("07/07/1988", "dd/MM/yyyy", null).ToString()
                    },
                    new User
                    {
                        Id = new Guid("E1550247-8B86-4199-8EF2-3B243A6D5EC3"),
                        UserName = "nehu",
                        FirstName = "Neha",
                        LastName = "Jain",
                        Email = "neha.jain@gmail.com",
                        Dob = DateTime.ParseExact("11/08/1990", "dd/MM/yyyy", null).ToString()
                    },
                    new User
                    {
                        Id = new Guid("04161963-3620-4DEE-B579-9CD3D79E17C9"),
                        UserName = "Nikki",
                        FirstName = "Nikita",
                        LastName = "khatri",
                        Email = "nikita.khatri@gmail.com",
                        Dob = DateTime.ParseExact("04/01/1990", "dd/MM/yyyy", null).ToString()
                    },
                    new User
                    {
                        Id = new Guid("60E65F71-6A4C-47FE-B2D7-8960F2879DCD"),
                        UserName = "queen",
                        FirstName = "Lagertha",
                        LastName = "Lodbrok",
                        Email = "Lagertha @vikings.com",
                        Dob = DateTime.ParseExact("24/01/1990", "dd/MM/yyyy", null).ToString()
                    },
                    new User
                    {
                        Id = new Guid("11978488-EFBE-43BB-AD21-8AE74AC3F049"),
                        UserName = "Bjorn",
                        FirstName = "Iron",
                        LastName = "Side",
                        Email = "Iron.side@vikings.com",
                        Dob = DateTime.ParseExact("04/09/1990", "dd/MM/yyyy", null).ToString()
                    }
            };

        public ResponseBase DeleteUser(RequestBase<Guid> request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase EditUser(RequestBase<WebApiServices.Models.User> request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<WebApiServices.Models.User> GetUserById(RequestBase<Guid> request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<int> GetUserCount(RequestBase request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<IEnumerable<User>> GetUsers(RequestBase<PagingRequest> request)
        {
            var response = new ResponseBase<IEnumerable<User>>();
            response.ResponseData = FakeUsers.Skip(request.Data.PageNumber * request.Data.PageSize).Take(request.Data.PageSize);
            response.Status = true;
            return response;
        }

        public ResponseBase<IEnumerable<KeyValuePair<Guid, string>>> GlobalSearch(RequestBase<string> request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase SaveUser(RequestBase<WebApiServices.Models.User> request)
        {
            throw new NotImplementedException();
        }
    }
}
