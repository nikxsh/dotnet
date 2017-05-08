using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Security.Claims;
using WebApiServices.Providers;

namespace WebApiServices.Helper
{
    public class GlobalHelper
    {
        public static string Issuer
        {
            get
            {
                var _issuer = ConfigurationManager.AppSettings["Issuer"];
                return _issuer;
            }
        }

        public static string GetMappedToken(Guid id, string name)
        {
            var tokenProvider = new JWTokenProvider(Issuer);

            var identity = new ClaimsIdentity("Password");
            identity.AddClaim(new Claim("userid", id.ToString()));
            identity.AddClaim(new Claim("username", name));

            var authProp = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            };

            var ticket = new AuthenticationTicket(identity, authProp);
            return tokenProvider.Protect(ticket);
        }
    }
}