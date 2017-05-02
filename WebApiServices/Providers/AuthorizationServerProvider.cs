using EFDataStorage.Repositories;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiServices.Adapter;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Models;

namespace WebApiServices.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });


            var request = UserManager.PrepareRequest(new RequestBase<LoginRequest>(new LoginRequest { Username = context.UserName, Password = context.Password }));
            ISecurityAdapter _securityAdapter = new SecurityAdapter(new SecurityRepository());
            var response = _securityAdapter.Authenticate(request);

            if(!response.ResponseData.IsAuthenticated)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("userid", response.ResponseData.UserId.ToString()));
            identity.AddClaim(new Claim("username", context.UserName));

            context.Validated(identity);
        }
    }
}