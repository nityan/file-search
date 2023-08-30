using FileSearch.Attributes;

namespace FileSearch.Model
{
    /// <summary>
    /// Represents a parameter.
    /// </summary>
	internal class Parameters
    {
        /// <summary>
        /// Iniitalizes a new instance of the <see cref="Parameters"/> class.
        /// </summary>
        public Parameters()
        {
            FileExtensions = new List<string>();
            TopLevelOnly = false;
        }

        /// <summary>
        /// Gets or sets a list of file extensions to include in the search.
        /// </summary>
        [Parameter("ext", "The list of file extensions to include when performing a search. Defaults to '*'.")]
        public List<string> FileExtensions { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        [Parameter("path", "The path on which to perform the search.")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the term to search.
        /// </summary>
		[Parameter("term", "The term on which to perform the search.")]
        public string Term { get; set; }

        /// <summary>
        /// Gets or sets an indicator as to whether the search should include subdirectories.
        /// </summary>
        [Parameter("top", "A boolean value indicating if the search should include subdirectories. Defaults to false.")]
        public bool TopLevelOnly { get; set; }
    }
}
