using System.Web.Http;
using System.Web.Http.Cors;

namespace brewerydb
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			//Configure cores
			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				 name: "DefaultApi",
				 routeTemplate: "api/{controller}/{id}",
				 defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
