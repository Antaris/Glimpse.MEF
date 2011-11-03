namespace Glimpse.MEF.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an export descriptor.
    /// </summary>
    public class ExportDescriptor
    {
        #region Properties
        /// <summary>
        /// Gets or sets the creation policy.
        /// </summary>
        public string CreationPolicy { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the export metadata.
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }
        #endregion
    }
}