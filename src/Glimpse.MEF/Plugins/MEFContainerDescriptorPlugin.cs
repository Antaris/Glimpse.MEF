namespace Glimpse.MEF.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Core.Extensibility;
    using Models;

    /// <summary>
    /// Defines a Glimpse plugin that interogates container interactions.
    /// </summary>
    [GlimpsePlugin(ShouldSetupInInit = true)]
    public class MEFContainerDescriptorPlugin : IGlimpsePlugin
    {
        #region Properties
        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        public string Name { get { return "MEF Container"; } }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the data the plugin should provide.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The plugin data.</returns>
        public object GetData(HttpContextBase context)
        {
            var catalogs = HttpRuntime.Cache.ModelList<CatalogDescriptor>("MEF-Catalogs");

            var data = new List<object[]> { new[] { "Type", "Primitive Type", "Properties", "Available Parts" } };

            Func<CatalogDescriptor, object[]> dataFactory = null;
            dataFactory = cd =>
                              {
                                  var exports = new List<object[]> { new object[] { "Name", "Creation Policy", "Metadata" } };
                                  exports.AddRange(cd.Parts.Select(e => new object[] { e.DisplayName, e.CreationPolicy ?? "Shared", e.Metadata }));

                                  string type = "";
                                  for (int i = 0; i < cd.Level; i++)
                                      type += "-";
                                  if (type.Length > 0)
                                      type += " ";
                                  
                                  var d = new object[4];
                                  d[0] = type + cd.Type;
                                  d[1] = "Catalog";
                                  d[2] = (cd.Properties.Count == 0) ? (object)"No Properties" : cd.Properties;
                                  d[3] = (cd.IsAggregate) ? null : exports;

                                  return d;
                              };

            foreach (var catalog in catalogs.SelectMany(FlattenDescriptors))
            {
                data.Add(dataFactory(catalog));
            }

            return data;
        }

        private IEnumerable<CatalogDescriptor> FlattenDescriptors(CatalogDescriptor descriptor, int level = 0)
        {
            var cats = new List<CatalogDescriptor>();
            descriptor.Level = level;
            cats.Add(descriptor);

            foreach (var catalog in descriptor.ChildCatalogs)
                cats.AddRange(FlattenDescriptors(catalog, level + 1));

            return cats;
        }

        /// <summary>
        /// Configures the plugin.
        /// </summary>
        public void SetupInit()
        {
        }
        #endregion
    }
}