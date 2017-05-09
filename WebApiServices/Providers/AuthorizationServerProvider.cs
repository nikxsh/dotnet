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

            if (!response.ResponseData.IsAuthenticated)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            /// Claim : A statement that one subject makes about itself or another subject. For example, the statement can be about a name, identity, 
            ///        key, group, privilege, or capability. Claims are issued by a provider, and they are given one or more values and then packaged 
            ///        in security tokens that are issued by a security token service (STS). They are also defined by a claim value type and, possibly,
            ///        associated metadata. 
            ///        
            /// Claim is piece of information that describes given identity on some aspect. Take claim as name-value pair. 
            /// Claims are held in authentication token that may have also signature so you can be sure that token is not tampered on its way from 
            /// remote machine to your system. You can think of token as envelop that contains claims about user.
            /// 
            /// Token may contain different claims:
            /// 
            ///     full name of user,
            ///     e - mail address,
            ///     membership in security groups,
            ///     phone number,
            ///     color of eyes.
            ///     
            /// System can use claims to identify and describe given user from more than one aspect.This is something you don’t achieve easily 
            /// with regular username-password based authentication mechanisms.

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