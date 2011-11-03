namespace Glimpse.MEF.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a catalog descriptor.
    /// </summary>
    public class CatalogDescriptor
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="CatalogDescriptor"/>.
        /// </summary>
        public CatalogDescriptor()
        {
            ChildCatalogs = new List<CatalogDescriptor>();
            Parts = new List<ExportDescriptor>();
            Properties = new Dictionary<string, string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether the catalog is an aggreagate catalog.
        /// </summary>
        public bool IsAggregate { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the list of child catalogs.
        /// </summary>
        public List<CatalogDescriptor> ChildCatalogs { get; private set; }

        /// <summary>
        /// Gets the list of available parts.
        /// </summary>
        public List<ExportDescriptor> Parts { get; private set; }

        /// <summary>
        /// Gets the list of properties.
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }
        #endregion
    }
}