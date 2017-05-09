using Microsoft.Owin;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;
using WebApiServices.App_Start;
using WebApiServices.Providers;

[assembly: OwinStartup(typeof(WebApiServices.Startup))]
namespace WebApiServices
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseNinjectMiddleware(NinjectWebCommon.CreateKernel);
            app.UseNinjectWebApi(config);

            //In case you are not using any dependency resolver
            //app.UseWebApi(config);
        }

        public void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            var oAuthServer = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                //The path for generating tokens will be as :”http://localhost:port/token”.
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                //We’ve specified the implementation on how to validate the credentials for users asking for tokens in custom class named 
                //“SimpleAuthorizationServerProvider”
                Provider = new AuthorizationServerProvider(),
                AccessTokenFormat = new JWTokenProvider(ConfigurationManager.AppSettings["Issuer"])
            };

            //we passed this options to the extension method “UseOAuthAuthorizationServer” so we’ll add the authentication middleware to the pipeline
            app.UseOAuthAuthorizationServer(oAuthServer);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        /// <summary>
        /// This step will configure our API to trust tokens issued by our Authorization server only, in our case the Authorization and 
        /// Resource Server are the same server (http://localhost:5658), notice how we are providing the values for audience, and the 
        /// audience secret we used to generate and issue the JSON Web Token
        /// 
        /// By providing those values to the “JwtBearerAuthentication” middleware, our API will be able to consume only JWT tokens issued
        /// by our trusted Authorization server, any other JWT tokens from any other Authorization server will be rejected.
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            string issuer = ConfigurationManager.AppSettings["Issuer"];

            string audienceId = ConfigurationManager.AppSettings["AudienceId"];

            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["AudienceSecret"];

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                AllowedAudiences = new[] { audienceId },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, keyByteArray)
                }
            });
        }
    }
}