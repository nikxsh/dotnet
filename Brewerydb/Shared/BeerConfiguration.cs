using System.Configuration;

namespace Brewerydb.Shared
{
	public class BeerConfiguration
	{
		static BeerConfiguration()
		{
			BaseUrl = ConfigurationManager.AppSettings["BASE_URL"];
			ApiKey = ConfigurationManager.AppSettings["API_KEY"];
		}

		public static string BaseUrl { get; private set; }

		public static string ApiKey { get; private set; }
	}
}