using EFDataStorage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using EFDataStorage.Entities;

namespace UnitTesting.Fakes
{
    public class FakeUsersRepository : IUserRepository
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
                        Dob =  DateTime.ParseExact("14/03/1990","dd/MM/yyyy", null)
                    },
                    new User
                    {
                        Id = new Guid("84A93DE5-E1F2-4847-87C5-43182DCDC781"),
                        UserName = "ravi",
                        FirstName = "Ravi",
                        LastName = "Singh",
                        Email = "ravi.singh@gmail.com",
                        Dob = DateTime.ParseExact("07/07/1988", "dd/MM/yyyy", null)
                    },
                    new User
                    {
                        Id = new Guid("E1550247-8B86-4199-8EF2-3B243A6D5EC3"),
                        UserName = "nehu",
                        FirstName = "Neha",
                        LastName = "Jain",
                        Email = "neha.jain@gmail.com",
                        Dob = DateTime.ParseExact("11/08/1990", "dd/MM/yyyy", null)
                    },
                    new User
                    {
                        Id = new Guid("04161963-3620-4DEE-B579-9CD3D79E17C9"),
                        UserName = "Nikki",
                        FirstName = "Nikita",
                        LastName = "khatri",
                        Email = "nikita.khatri@gmail.com",
                        Dob = DateTime.ParseExact("04/01/1990", "dd/MM/yyyy", null)
                    },
                    new User
                    {
                        Id = new Guid("60E65F71-6A4C-47FE-B2D7-8960F2879DCD"),
                        UserName = "queen",
                        FirstName = "Lagertha",
                        LastName = "Lodbrok",
                        Email = "Lagertha @vikings.com",
                        Dob = DateTime.ParseExact("24/01/1990", "dd/MM/yyyy", null)
                    },
                    new User
                    {
                        Id = new Guid("11978488-EFBE-43BB-AD21-8AE74AC3F049"),
                        UserName = "Bjorn",
                        FirstName = "Iron",
                        LastName = "Side",
                        Email = "Iron.side@vikings.com",
                        Dob = DateTime.ParseExact("04/09/1990", "dd/MM/yyyy", null)
                    }
            };

        public string Response { get; set; }

        public void DeleteUser(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User User)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public int GetUserCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers(int PageSize, int PageNumber, string keyword)
        {
            return FakeUsers.Skip(PageNumber * PageSize).Take(PageSize);
        }

        public IEnumerable<KeyValuePair<Guid, string>> GlobalSearch(string keyword)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(User User)
        {
            Response = "Success";
        }
    }
}
