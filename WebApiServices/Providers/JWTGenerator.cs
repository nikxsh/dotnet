using System;
using Microsoft.Owin.Security;
using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace WebApiServices.Providers
{
    public class JWTokenGenerator : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer = string.Empty;

        /// <summary>
        /// The constructor of this class accepts the “Issuer” of this JWT which will be our API. 
        /// This API acts as Authorization and Resource Server on the same time, this can be string
        /// or URI, in our case we’ll fix it to URI.
        /// </summary>
        /// <param name="issuer"></param>
        public JWTokenGenerator(string issuer)
        {
            _issuer = issuer;
        }

        /// <summary>
        /// this API serves as Resource and Authorization Server at the same time, 
        /// so we are fixing the Audience Id and Audience Secret (Resource Server) 
        /// in web.config file, this Audience Id and Secret will be used for HMAC265
        /// and hash the JWT token
        /// 
        /// By doing this, the requester for an OAuth 2.0 access token from our API 
        /// will receive a signed token which contains claims for an authenticated
        /// Resource Owner (User) and this access token is intended to certain (Audience)
        /// as well
        /// </summary>
        /// <param name="data">AuthenticationTicket</param>
        /// <returns></returns>
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = ConfigurationManager.AppSettings["AudienceId"];

            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["AudienceSecret"];

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}