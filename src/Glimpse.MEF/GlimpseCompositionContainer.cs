namespace Glimpse.MEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Configuration;
    using System.Linq;
    using System.Web;

    using Core.Configuration;
    using Models;

    /// <summary>
    /// Defines a composition container that supports Glimpse.
    /// </summary>
    public class GlimpseCompositionContainer : CompositionContainer
    {
        #region Fields
        private static readonly bool _enabled;
        private static readonly string CreationPolicyTypeName = typeof(CreationPolicy).FullName;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialises the <see cref="GlimpseCompositionContainer"/> type.
        /// </summary>
        static GlimpseCompositionContainer()
        {
            _enabled = false;

            var config = ConfigurationManager.GetSection("glimpse") as GlimpseConfiguration;
            if (config != null)
                _enabled = config.Enabled;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="GlimpseCompositionContainer"/>.
        /// </summary>
        public GlimpseCompositionContainer() { BuildDescriptors(); }

        /// <summary>
        /// Initialises a new instance of <see cref="GlimpseCompositionContainer"/>.
        /// </summary>
        /// <param name="providers">The set of export providers.</param>
        public GlimpseCompositionContainer(params ExportProvider[] providers)
            : base(providers.Glimpse(_enabled)) { BuildDescriptors(); }

        /// <summary>
        /// Initialises a new instance of <see cref="GlimpseCompositionContainer"/>.
        /// </summary>
        /// <param name="catalog">The composable part catalog.</param>
        public GlimpseCompositionContainer(ComposablePartCatalog catalog)
            : base(catalog.Glimpse(_enabled)) { BuildDescriptors(); }

        /// <summary>
        /// Initialises a new instance of <see cref="GlimpseCompositionContainer"/>.
        /// </summary>
        /// <param name="catalog">The composable part catalog.</param>
        /// <param name="providers">The set of export providers.</param>
        public GlimpseCompositionContainer(ComposablePartCatalog catalog, params ExportProvider[] providers)
            : base(catalog.Glimpse(_enabled), providers.Glimpse(_enabled)) { BuildDescriptors(); }
        #endregion

        #region Methods
        /// <summary>
        /// Builds the descriptors that are rendered by Glimpse.
        /// </summary>
        internal void BuildDescriptors()
        {
            if (_enabled)
            {
                var catalogDescriptors = HttpRuntime.Cache.ModelList<CatalogDescriptor>("MEF-Catalogs");
                if (!catalogDescriptors.Any())
                    catalogDescriptors.AddRange(GetCatalogDescriptors(Catalog));
            }
        }

        /// <summary>
        /// Gets the catalog descriptors for the specified catalog.
        /// </summary>
        /// <param name="catalog">The part catalog.</param>
        /// <returns>A set of catalog descriptors.</returns>
        private static IEnumerable<CatalogDescriptor> GetCatalogDescriptors(ComposablePartCatalog catalog)
        {
            var result = new List<CatalogDescriptor>();
            CatalogDescriptor descriptor = null;

            var glimpseCatalog = catalog as GlimpseCatalog;
            if (glimpseCatalog != null)
            {
                result.AddRange(GetCatalogDescriptors(glimpseCatalog.Catalog));
                return result;
            }

            var aggregateCatalog = catalog as AggregateCatalog;
            if (aggregateCatalog != null)
            {
                descriptor = new CatalogDescriptor() { Type = aggregateCatalog.GetType().Name, IsAggregate = true};

                foreach (var child in aggregateCatalog.Catalogs)
                    descriptor.ChildCatalogs.AddRange(GetCatalogDescriptors(child));

                result.Add(descriptor);
                return result;
            }

            descriptor = new CatalogDescriptor() { Type = catalog.GetType().Name };
            descriptor.Properties = catalog.GetProperties();

            foreach (var part in catalog.Parts)
            {
                foreach (var def in part.ExportDefinitions)
                {
                    var meta = def.Metadata.Keys
                        .Where(k => k != CreationPolicyTypeName)
                        .ToDictionary(key => key, key => def.Metadata[key].ToString());

                    string policy = null;
                    if (def.Metadata.ContainsKey(CreationPolicyTypeName))
                        policy = def.Metadata[CreationPolicyTypeName].ToString();

                    var export = new ExportDescriptor { CreationPolicy = policy, DisplayName = part.ToString(), Metadata = meta };

                    descriptor.Parts.Add(export);
                }
            }

            result.Add(descriptor);

            return result;
        }
        #endregion
    }
}