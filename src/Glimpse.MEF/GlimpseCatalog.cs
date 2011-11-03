namespace Glimpse.MEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Web;

    using Models;

    /// <summary>
    /// Defines a composable part catalog that supports Glimpse.
    /// </summary>
    public class GlimpseCatalog : ComposablePartCatalog
    {
        #region Fields
        private readonly ComposablePartCatalog _catalog;
        private static readonly string CreationPolicyTypeName = typeof(CreationPolicy).FullName;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ComposablePartCatalog"/>.
        /// </summary>
        /// <param name="catalog">The inner catalog.</param>
        internal GlimpseCatalog(ComposablePartCatalog catalog)
        {
            if (catalog == null)
                throw new ArgumentNullException("catalog");

            _catalog = catalog;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the internal catalog.
        /// </summary>
        internal ComposablePartCatalog Catalog { get { return _catalog; } }

        /// <summary>
        /// Gets the set of parts provided by the catalog.
        /// </summary>
        public override IQueryable<ComposablePartDefinition> Parts { get { return _catalog.Parts; } }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the available exports for the specified import definition.
        /// </summary>
        /// <param name="definition">The import definition.</param>
        /// <returns>The set of matching exports.</returns>
        public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
        {
            var definitions = _catalog.GetExports(definition);

            var list = HttpContext.Current.ModelList<ImportDescriptor>("MEF-Imports");

            var model = new ImportDescriptor
                            {
                                Cardinality = definition.Cardinality.ToString(),
                                ContractName = definition.ContractName,
                                Prerequisite = definition.IsPrerequisite,
                                Recomposable = definition.IsRecomposable
                            };

            foreach (var def in definitions)
            {
                var meta = def.Item2.Metadata.Keys
                    .Where(k => k != CreationPolicyTypeName)
                    .ToDictionary(key => key, key => def.Item2.Metadata[key].ToString());

                string policy = null;
                if (def.Item2.Metadata.ContainsKey(CreationPolicyTypeName))
                    policy = def.Item2.Metadata[CreationPolicyTypeName].ToString();

                var export = new ExportDescriptor { CreationPolicy = policy, DisplayName = def.Item1.ToString(), Metadata = meta };

                model.Exports.Add(export);
            }

            list.Add(model);

            return definitions;
        }
        #endregion
    }
}