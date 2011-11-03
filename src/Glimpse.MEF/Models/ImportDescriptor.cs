namespace Glimpse.MEF.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an import descriptor.
    /// </summary>
    public class ImportDescriptor
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ImportDescriptor"/>.
        /// </summary>
        public ImportDescriptor()
        {
            Exports = new List<ExportDescriptor>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or set the cardinality.
        /// </summary>
        public string Cardinality { get; set; }

        /// <summary>
        /// Gets or sets the contract name.
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// Gets the set of exports.
        /// </summary>
        public List<ExportDescriptor> Exports { get; private set; }

        /// <summary>
        /// Gets or sets whether the import is a prerequisite.
        /// </summary>
        public bool Prerequisite { get; set; }

        /// <summary>
        /// Gets or sets whether the import is recomposable.
        /// </summary>
        public bool Recomposable { get; set; }
        #endregion
    }
}