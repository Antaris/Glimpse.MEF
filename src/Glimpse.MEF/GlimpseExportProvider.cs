namespace Glimpse.MEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;

    /// <summary>
    /// Defines an export provider that supports Glimpse.
    /// </summary>
    public class GlimpseExportProvider : ExportProvider
    {
        #region Fields
        private readonly ExportProvider _provider;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="GlimpseExportProvider"/>.
        /// </summary>
        /// <param name="provider">The inner export provider.</param>
        internal GlimpseExportProvider(ExportProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            _provider = provider;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the available exports the match the specified import definition.
        /// </summary>
        /// <param name="definition">The import definition.</param>
        /// <param name="atomicComposition">The atomic composition.</param>
        /// <returns>The set of matching exports.</returns>
        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            return _provider.GetExports(definition, atomicComposition);
        }
        #endregion
    }
}