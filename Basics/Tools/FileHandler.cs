using System.Collections.Generic;
using System.IO;

namespace Tools
{
	public class FileHandler
	{
		public static string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}

		public static string[] ReadAllLines(string path)
		{
			return File.ReadAllLines(path);
		}

		public static string ReadAllTextFromAllFiles(string directory)
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

		public static string[] ReadAllLinesFromAllFiles(string directory, string pattern = "")
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

		public static void AppendContentToFile(string line)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\nikxsh.log"))
			{
				file.WriteLine(line);
			}
		}

		public static void WriteToFile(string path, string content)
		{
			//Create new file or ovveride existing one
			File.WriteAllText(path, content);
		}
	}
}
