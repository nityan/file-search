using System.Xml.Serialization;

namespace FileSearch.Model
{
	/// <summary>
	/// Represents a match result.
	/// </summary>
	[XmlType(nameof(MatchResult), Namespace = "https://example.com/model")]
	public class MatchResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchResult"/> class.
        /// </summary>
        public MatchResult()
        {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="MatchResult"/> class.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="lineNumber">The line number.</param>
		/// <param name="offendingLine">The offending line.</param>
		public MatchResult(string filename, ulong lineNumber, string offendingLine)
        {
            Filename = filename;
            LineNumber = lineNumber;
            OffendingLine = offendingLine;
        }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        [XmlElement("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
		[XmlElement("lineNumber")]
		public ulong LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the offending line.
        /// </summary>
		[XmlElement("offendingLine")]
		public string OffendingLine { get; set; }
	}
}
