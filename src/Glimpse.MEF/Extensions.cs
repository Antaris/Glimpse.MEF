namespace Glimpse.MEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;

    /// <summary>
    /// Provides shared extension methods.
    /// </summary>
    static class Extensions
    {
        #region Methods
        /// <summary>
        /// Wraps the specfied catalog in a <see cref="GlimpseCatalog"/>.
        /// </summary>
        /// <param name="catalog">The catalog to wrap.</param>
        /// <returns>An instance of <see cref="GlimpseCatalog"/>.</returns>
        public static ComposablePartCatalog Glimpse(this ComposablePartCatalog catalog, bool enabled)
        {
            return enabled ? new GlimpseCatalog(catalog) : catalog;
        }

        /// <summary>
        /// Wraps the specified export provider in a <see cref="GlimpseExportProvider"/>,
        /// </summary>
        /// <param name="provider">The provider to wrap.</param>
        /// <param name="enabled">Flag to specify whether glimpse is enabled.</param>
        /// <returns>An instance of <see cref="GlimpseExportProvider"/>.</returns>
        public static ExportProvider Glimpse(this ExportProvider provider, bool enabled)
        {
            return enabled ? new GlimpseExportProvider(provider) : provider;
        }

        /// <summary>
        /// Wraps the specified set of export providers as a set of  <see cref="GlimpseExportProvider"/>.
        /// </summary>
        /// <param name="providers">The providers to wrap.</param>
        /// <param name="enabled">Flag to specify whether glimpse is enabled.</param>
        /// <returns>The set of wrapped providers.</returns>
        public static IEnumerable<ExportProvider> Glimpse(this IEnumerable<ExportProvider> providers, bool enabled)
        {
            return (providers ?? Enumerable.Empty<ExportProvider>()).Select(e => e.Glimpse(enabled));
        }

        /// <summary>
        /// Wraps the specified array of export providers as an array of  <see cref="GlimpseExportProvider"/>.
        /// </summary>
        /// <param name="providers">The providers to wrap.</param>
        /// <param name="enabled">Flag to specify whether glimpse is enabled.</param>
        /// <returns>The array of wrapped providers.</returns>
        public static ExportProvider[] Glimpse(this ExportProvider[] providers, bool enabled)
        {
            return (providers ?? Enumerable.Empty<ExportProvider>()).Select(e => e.Glimpse(enabled)).ToArray();
        }

        /// <summary>
        /// Gets the model list from the request collection, or creates on if it does not exist.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="context">The current context.</param>
        /// <param name="key">The list key.</param>
        /// <returns>The model list.</returns>
        public static List<T> ModelList<T>(this HttpContext context, string key)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is a required value.");

            if (context.Items.Contains(key))
                return (List<T>)context.Items[key];

            var list = new List<T>();
            context.Items.Add(key, list);
            return list;
        }

        /// <summary>
        /// Gets the model list from the request collection, or creates on if it does not exist.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="context">The current context.</param>
        /// <param name="key">The list key.</param>
        /// <returns>The model list.</returns>
        public static List<T> ModelList<T>(this HttpContextBase context, string key)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is a required value.");

            if (context.Items.Contains(key))
                return (List<T>)context.Items[key];

            var list = new List<T>();
            context.Items.Add(key, list);
            return list;
        }

        /// <summary>
        /// Gets the model list from the runtime cache, or creates on if it does not exist.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="cache">The runtime cache.</param>
        /// <param name="key">The list key.</param>
        /// <returns>The model list.</returns>
        public static List<T> ModelList<T>(this Cache cache, string key)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is a required value.");

            object value = cache.Get(key);
            if (value != null)
                return (List<T>)value;

            var list = new List<T>();
            cache.Insert(key, list);

            return list;
        }

        /// <summary>
        /// Gets the properties for the specified catalog.
        /// </summary>
        /// <param name="catalog">The catalog to get properties for.</param>
        /// <returns>The dictionary of properties.</returns>
        public static Dictionary<string, string> GetProperties(this ComposablePartCatalog catalog)
        {
            var dict = new Dictionary<string, string>();

            // Todo: Find a better way to descript useful properties... reflection?
            var directoryCatalog = catalog as DirectoryCatalog;
            if (directoryCatalog != null)
            {
                dict.Add("Path", directoryCatalog.Path);
                dict.Add("FullPath", directoryCatalog.FullPath);
                dict.Add("SearchPattern", directoryCatalog.SearchPattern);
            }

            return dict;
        }
        #endregion
    }
}