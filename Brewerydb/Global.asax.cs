using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Web.Http;

namespace brewerydb
{
	public class WebApiApplication : System.Web.HttpApplication
    {
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}

		protected void Application_BeginRequest()
		{
			//if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
			//{
			//	Response.Flush();
			//	Response.End();
			//}
		}
	}
}