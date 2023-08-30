using FileSearch.Model;
using FileSearch.Utility;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text;
using System.Xml.Serialization;

namespace FileSearch
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	internal class Program
	{
		/// <summary>
		/// Defines the main entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static async Task Main(string[] args)
		{
			try
			{
				var builder = Host.CreateDefaultBuilder(args);

				builder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

				var app = builder.Build();

				var report = Searcher.Search(new List<string>(args));
				var serializer = new XmlSerializer(typeof(MatchResultReport));

				var reportFile = Path.Combine(Directory.GetCurrentDirectory(), $"report_{DateTime.Now:yyyyMMddhhmmss}.xml");
				using (var streamWriter = File.CreateText(reportFile))
				using (var memoryStream = new MemoryStream())
				{
					serializer.Serialize(memoryStream, new MatchResultReport(report.Results.OrderBy(c => c.Filename).ThenBy(c => c.LineNumber)));
					var data = memoryStream.ToArray();
					await streamWriter.WriteLineAsync(Encoding.UTF8.GetString(data));
					await streamWriter.FlushAsync();
				}

				Log.Information($"Generated report: {reportFile}");
			}
			catch (Exception e)
			{
				Log.Error($"Unable to perform search: {e}");
			}

			Console.ReadKey();
		}
	}
}