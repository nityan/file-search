using FileSearch.Model;
using Serilog;

namespace FileSearch.Utility
{
	internal class Searcher
	{
		private const string defaultSearchPattern = "*";

		internal static MatchResultReport Search(List<string> args)
		{
			var parameters = ParameterParser.Parse(new List<string>(args));
			var files = new List<string>();

			if (parameters.FileExtensions.Any())
			{
				Log.Information($"File extension(s) supplied: {string.Join(", ", parameters.FileExtensions)}");

				foreach (var fileExtension in parameters.FileExtensions)
				{
					files.AddRange(Directory.EnumerateFiles(parameters.Path, $"*.{fileExtension}", parameters.TopLevelOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories));
				}
			}
			else
			{
				Log.Warning($"No file extensions supplied, will default to '{defaultSearchPattern}', this may impact performance");
				files.AddRange(Directory.EnumerateFiles(parameters.Path, defaultSearchPattern, parameters.TopLevelOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories));
			}

			var matches = new List<MatchResult>();

			foreach (var file in files.OrderBy(c => c))
			{
				var contents = File.ReadAllLines(file);

				for (int i = 0; i < contents.Length; i++)
				{
					if (contents[i].Contains(parameters.Term.ToLowerInvariant()))
					{
						matches.Add(new MatchResult(file, Convert.ToUInt64(i), contents[i]));
					}
				}
			}

			return new MatchResultReport(matches);
		}
	}
}
