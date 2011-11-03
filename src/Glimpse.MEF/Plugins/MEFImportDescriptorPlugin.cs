namespace Glimpse.MEF.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Core.Extensibility;
    using Models;

    /// <summary>
    /// Defines a Glimpse plugin that interogates import resolutions.
    /// </summary>
    [GlimpsePlugin]
    public class MEFImportDescriptorPlugin : IGlimpsePlugin
    {
        #region Properties
        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        public string Name { get { return "MEF Imports"; } }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the data the plugin should provide.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The plugin data.</returns>
        public object GetData(HttpContextBase context)
        {
            var imports = context.ModelList<ImportDescriptor>("MEF-Imports");

            var data = new List<object[]> { new object[] { "Contract", "Cardinality", "Prerequisite", "Recomposable", "Matching Exports" } };

            foreach (var import in imports)
            {
                var exports = new List<object[]> { new object[] { "Name", "Creation Policy", "Metadata" } };
                exports.AddRange(import.Exports.Select(e => new object[] { e.DisplayName, e.CreationPolicy ?? "Shared", e.Metadata }));

                var importData = new []
                                     {
                                         import.ContractName,
                                         import.Cardinality,
                                         (import.Prerequisite ? "_" + import.Prerequisite + "_" : import.Prerequisite.ToString()),
                                         (import.Recomposable ? "_" + import.Recomposable + "_" : import.Recomposable.ToString()),
                                         (exports.Count == 1) ? (object)"No matching exports." : exports
                                     };

                data.Add(importData);
            }

            return data;
        }

        /// <summary>
        /// Configures the plugin.
        /// </summary>
        public void SetupInit() { }
        #endregion
    }
}
