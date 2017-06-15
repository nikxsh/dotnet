using System;
using Microsoft.Owin.Security;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;
using System.IdentityModel.Tokens;

namespace WebApiServices.Providers
{
    public class JWTokenProvider : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer = string.Empty;

        public JWTokenProvider(string issuer)
        {
            _issuer = issuer;
        }

        /// <summary>
        /// As we stated before, this API serves as Resource and Authorization Server at the same time,
        /// so we are fixing the Audience Id and Audience Secret (Resource Server) in web.config file, 
        /// this Audience Id and Secret will be used for HMAC265 and hash the JWT token.
        /// 
        /// Then we prepare the raw data for the JSON Web Token which will be issued to the requester 
        /// by providing the issuer, audience, user claims, issue date, expiry date, and the signing key 
        /// which will sign(hash) the JWT payload.
        /// 
        /// Lastly we serialize the JSON Web Token to a string and return it to the requester.
        /// 
        /// By doing this, the requester for an OAuth 2.0 access token from our API will receive a signed
        /// token which contains claims for an authenticated Resource Owner (User) and this access token 
        /// is intended to certain (Audience) as well.
        /// </summary>
        /// <param name="data"></param>
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


            //var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.Default.GetBytes(symmetricKeyAsBase64));
            //var signingKey = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

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