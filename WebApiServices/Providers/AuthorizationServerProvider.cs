using EFDataStorage.Repositories;
using Microsoft.Owin.Security.OAuth;
using System;
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
        /// <summary>
        /// we are considering the request valid always, because in our implementation our client 
        /// (AngularJS front-end) is trusted client and we do not need to validate it.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// Responsible for receiving the username and password from the request and validate them against 
        /// our ASP.NET 2.1 Identity system, if the credentials are valid and the email is confirmed we are 
        /// building an identity for the logged in user, this identity will contain all the roles and claims 
        /// for the authenticated user, until now we didn’t cover roles and claims part of the tutorial, but 
        /// for the mean time you can consider all users registered in our system without any roles or claims
        /// mapped to them.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
            identity.AddClaim(new Claim("userId", response.ResponseData.UserId.ToString()));
            identity.AddClaim(new Claim("userName", context.UserName));
            identity.AddClaim(new Claim("userRoles", string.Join(",", response.ResponseData.UserRoles.ToArray())));

            context.Validated(identity);
        }

        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return base.ValidateAuthorizeRequest(context);
        }
    }
}