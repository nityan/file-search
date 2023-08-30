namespace FileSearch.Attributes
{
    /// <summary>
    /// Represents a parameter attribute.
    /// </summary>
	internal class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/> class.
        /// </summary>
        public ParameterAttribute(string key, string description)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key), "Value cannot be null");
            }


			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ArgumentNullException(nameof(description), "Value cannot be null");
			}

			this.Key = key;
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
		public string Description { get; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
		public string Key { get; }
    }
}
