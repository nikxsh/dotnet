using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tools
{
	public class JsonHandler
	{
		public static T ReadJsonData<T>(string path)
		{
			using var streamReader = new StreamReader(path);
			string jsonData = streamReader.ReadToEnd();
			return JsonConvert.DeserializeObject<T>(jsonData);
		}

		public static void WriteJsonData<T>(string path, T Content)
		{
			var fileContent = JsonConvert.SerializeObject(Content);
			FileHandler.WriteToFile(path, fileContent);
		}
	}
}
