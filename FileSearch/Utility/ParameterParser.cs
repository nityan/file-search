using FileSearch.Attributes;
using FileSearch.Model;
using System.Reflection;

namespace FileSearch.Utility
{
	/// <summary>
	/// Represents a parameter parser.
	/// </summary>
	internal static class ParameterParser
    {
        private static readonly List<string> supportedExtensions = new List<string>
        {
            "txt",
            "js",
            "cs",
            "java",
            "rb",
            "php",
            "css",
            "py",
            "r",
            "ipynb"
        };

        /// <summary>
        /// Parses parameters and returns an instance of <see cref="Parameters"/>.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns an instance of parameters.</returns>
        internal static Parameters Parse(List<string> args)
        {
			var workingParameterMap = args.ToDictionary(k => k.Split('=')[0], v => v.Split('=')[1]);
			var parameters = new Parameters();

			var properties = typeof(Parameters).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(c => c.CanWrite && c.CustomAttributes.Any(x => x.AttributeType == typeof(ParameterAttribute)));

            foreach (var keyValuePair in workingParameterMap)
            {
                var propertyName = properties.FirstOrDefault(c => c.GetCustomAttribute<ParameterAttribute>()?.Key == keyValuePair.Key)?.Name;

                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    continue;
                }

                var parameterPropertyInfo = parameters.GetType().GetProperty(propertyName);

                if (parameterPropertyInfo == null)
                {
                    continue;
                }

				if (parameterPropertyInfo.PropertyType == typeof(List<string>))
				{
					parameterPropertyInfo.SetValue(parameters, new List<string>(keyValuePair.Value.Split(',')));
				}
				else
				{
					parameterPropertyInfo.SetValue(parameters, keyValuePair.Value);
				}
			}

            if (string.IsNullOrWhiteSpace(parameters.Path))
            {
                throw new InvalidOperationException("Unable to set path to perform search");
            }

            var unsupportedExtensions = parameters.FileExtensions.Except(supportedExtensions);

			if (unsupportedExtensions.Any())
            {
                throw new InvalidOperationException($"Unsupported extensions: {string.Join(", ", unsupportedExtensions)} Supported extensions: {string.Join(", ", supportedExtensions)}");
			}

            return parameters;
        }
    }
}
