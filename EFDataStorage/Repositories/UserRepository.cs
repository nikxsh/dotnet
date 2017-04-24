using System;
using System.Collections.Generic;
using System.Linq;
using EFDataStorage.Contracts;
using System.Data.Entity.Migrations;
using EFDataStorage.Entities;
using EFDataStorage.Helper;

namespace EFDataStorage.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUsers(int PageSize, int PageNumber, string keyword)
        {
            try
            {
                using (var context = new UserContext())
                {
                    var records = from user in context.Users
                                  select user;

                    if (!string.IsNullOrEmpty(keyword))
                        records = records.Where(x => x.UserName.Contains(keyword) ||
                                                   x.FirstName.Contains(keyword) ||
                                                   x.LastName.Contains(keyword) ||
                                                   x.Email.Contains(keyword) ||
                                                   x.Dob.ToString().Contains(keyword));

                    return records.OrderBy(x => x.UserName).Take(PageSize * PageNumber).Skip(PageSize * (PageNumber - 1)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<KeyValuePair<Guid, string>> GlobalSearch(string keyword)
        {
            try
            {
                using (var context = new UserContext())
                {
                    var usersearch = context.Users.Where(x => x.UserName.Contains(keyword) ||
                                                   x.FirstName.Contains(keyword) ||
                                                   x.LastName.Contains(keyword) ||
                                                   x.Email.Contains(keyword) ||
                                                   x.Dob.ToString().Contains(keyword))
                                             .ToList();

                    var result = usersearch.Select(y => new KeyValuePair<Guid, string>(y.Id, y.UserName));

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetUserCount()
        {
            try
            {
                using (var context = new UserContext())
                {
                    return context.Users.Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserById(Guid Id)
        {
            try
            {
                using (var context = new UserContext())
                {
                    return context.Users.Where(x => x.Id == Id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExecuteNonQueryResults SaveUser(User User)
        {
            try
            {
                using (var context = new UserContext())
                {
                    context.Users.Add(User);
                    return new ExecuteNonQueryResults { AffectedRecords = context.SaveChanges() };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExecuteNonQueryResults EditUser(User User)
        {
            try
            {
                using (var context = new UserContext())
                {
                    context.Users.AddOrUpdate(User);
                    return new ExecuteNonQueryResults { AffectedRecords = context.SaveChanges() };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExecuteNonQueryResults DeleteUser(Guid UserId)
        {
            try
            {
                using (var context = new UserContext())
                {
                    context.Users.Remove(context.Users.Where(x => x.Id == UserId).FirstOrDefault());
                    return new ExecuteNonQueryResults { AffectedRecords = context.SaveChanges() };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
