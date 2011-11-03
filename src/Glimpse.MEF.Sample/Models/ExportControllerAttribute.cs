namespace Glimpse.MEF.Sample.Models
{
    using System;
    using System.ComponentModel.Composition;
    using System.Web.Mvc;

    /// <summary>
    /// Exports a controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportControllerAttribute : ExportAttribute, INameMetadata
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ExportControllerAttribute"/>.
        /// </summary>
        /// <param name="name">The controller name.</param>
        public ExportControllerAttribute(string name)
            : base(typeof(IController))
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A controller name is required.");

            Name = name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public string Name { get; private set; }
        #endregion
    }
}