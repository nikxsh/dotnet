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
        public IEnumerable<User> Select(PagingRequest query)
        {
            try
            {
                using (var context = new UserContext())
                {
                    var records = from user in context.Users
                                  select user;

                    if (!string.IsNullOrEmpty(query.SearchString))
                        records = records.Where(x => x.UserName.Contains(query.SearchString) ||
                                                   x.FirstName.Contains(query.SearchString) ||
                                                   x.LastName.Contains(query.SearchString) ||
                                                   x.Email.Contains(query.SearchString) ||
                                                   x.Dob.ToString().Contains(query.SearchString));

                    var result = records.OrderBy(x => x.UserName).Skip(query.Skip).Take(query.Take).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Select()
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

        public User Select(GetUserById query)
        {
            try
            {
                using (var context = new UserContext())
                {
                    return context.Users.Where(x => x.Id == query.UserId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExecuteNonQueryResults Execute(SaveUser queryParams)
        {
            try
            {
                using (var context = new UserContext())
                {
                    context.Users.Add(queryParams.NewUser);
                    return new ExecuteNonQueryResults { AffectedRecords = context.SaveChanges() };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExecuteNonQueryResults Execute(EditUser queryParams)
        {

            try
            {
                using (var context = new UserContext())
                {
                    context.Users.AddOrUpdate(queryParams.EditedUser);
                    return new ExecuteNonQueryResults { AffectedRecords = context.SaveChanges() };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExecuteNonQueryResults Execute(DeleteUser queryParams)
        {
            try
            {
                using (var context = new UserContext())
                {
                    context.Users.Remove(context.Users.Where(x => x.Id == queryParams.UserId).FirstOrDefault());
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
