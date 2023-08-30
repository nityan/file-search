using System.Xml.Serialization;

namespace FileSearch.Model
{
	/// <summary>
	/// Represents a match result.
	/// </summary>
	[XmlRoot(nameof(MatchResultReport), Namespace = "https://example.com/model")]
	[XmlType(nameof(MatchResultReport), Namespace = "https://example.com/model")]
	public class MatchResultReport
    {
        /// <summary>
        /// The total matches.
        /// </summary>
        private int totalMatches;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchResultReport"/> class.
        /// </summary>
        public MatchResultReport()
        {
			this.Results = new List<MatchResult>();
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="MatchResultReport"/> class.
		/// </summary>
		/// <param name="results">The results.</param>
		public MatchResultReport(IEnumerable<MatchResult> results)
        {
            this.Results = new List<MatchResult>(results);
            this.TotalMatches = this.Results.Count;
        }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        [XmlElement("result")]
        public List<MatchResult> Results { get; set; }

        /// <summary>
        /// Gets or sets the total matches.
        /// </summary>
        [XmlElement("totalMatches")]
        public int TotalMatches
        {
            get => this.totalMatches;
            set => this.totalMatches = this.Results.Count;
        }
    }
}
