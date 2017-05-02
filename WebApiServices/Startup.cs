using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System;
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
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            
            app.UseNinjectMiddleware(NinjectWebCommon.CreateKernel);
            app.UseNinjectWebApi(config);

            //In case you are not using any dependency resolver
            //app.UseWebApi(config);
        }  
        
        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServer = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                //The path for generating tokens will be as :”http://localhost:port/token”.
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //We’ve specified the implementation on how to validate the credentials for users asking for tokens in custom class named 
                //“SimpleAuthorizationServerProvider”
                Provider = new AuthorizationServerProvider()
            };

            //we passed this options to the extension method “UseOAuthAuthorizationServer” so we’ll add the authentication middleware to the pipeline
            app.UseOAuthAuthorizationServer(oAuthServer);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }      
    }
}