using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Shared
{
	public class FileHandler
	{
		public T ReadJsonData<T>(string path)
		{
			using var streamReader = new StreamReader(path);
			string jsonData = streamReader.ReadToEnd();
			return JsonConvert.DeserializeObject<T>(jsonData);
		}

		public void WriteJsonData<T>(string path, T Content)
		{
			var fileContent = JsonConvert.SerializeObject(Content);
			WriteToFile(path, fileContent);
		}

		public void WriteToFile(string path, string content)
		{
			File.WriteAllText(path, content);
		}

		public string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}

		public string[] ReadAllLines(string path)
		{
			return File.ReadAllLines(path);
		}

		public string ReadAllTextFromAllFiles(string directory)
		{
			var allText = string.Empty;
			var files = Directory.EnumerateDirectories(directory);
			foreach (var file in files)
			{
				var allTextFromFile = ReadAllText($"{directory}/{file}");
				allText += allTextFromFile;
			}
			return allText;
		}

		public string[] ReadAllLinesFromAllFiles(string directory, string pattern = "")
		{
			var allLines = new List<string>();
			IEnumerable<string> files;

			if (!string.IsNullOrEmpty(pattern))
				files = Directory.EnumerateDirectories(directory);
			else
				files = Directory.EnumerateDirectories(directory, pattern);

			foreach (var file in files)
			{
				var allLinesFromFile = ReadAllLines($"{directory}/{file}");
				allLines.AddRange(allLines);
			}

			return allLines.ToArray();
		}
	}
}