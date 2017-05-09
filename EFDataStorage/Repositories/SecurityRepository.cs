using EFDataStorage.Contracts;
using EFDataStorage.Helper;
using System;
using System.Linq;

namespace EFDataStorage.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        public LoginResponse Select(LoginRequest query)
        {
            var response = new LoginResponse { IsAuthenticated = false };
            try
            {
                using (var context = new UserContext())
                {
                    var userDetails = context.Users.Where(x => x.UserName.Equals(query.Username, StringComparison.InvariantCultureIgnoreCase) && query.Password.Equals("123", StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
                    if (userDetails != null)
                    {
                        response.UserId = userDetails.Id;
                        response.UserRoles = new System.Collections.Generic.List<string> { "Admin", "Developer" };
                        response.IsAuthenticated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
