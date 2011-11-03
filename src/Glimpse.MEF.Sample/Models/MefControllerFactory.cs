namespace Glimpse.MEF.Sample.Models
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Defines a controller factory that supports the managed extensibility framework.
    /// </summary>
    [Export(typeof(IControllerFactory))]
    public class MefControllerFactory : DefaultControllerFactory
    {
        #region Fields
        private readonly CompositionContainer _container;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="MefControllerFactory"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        [ImportingConstructor]
        public MefControllerFactory(CompositionContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Creates a controller instance for the specified request and name.
        /// </summary>
        /// <param name="requestContext">The current request context.</param>
        /// <param name="controllerName">The controller name.</param>
        /// <returns>An instance of <see cref="IController"/>.</returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = _container.GetExports<IController, INameMetadata>()
                .Where(e => e.Metadata.Name.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => e.Value)
                .FirstOrDefault();

            return (controller ?? base.CreateController(requestContext, controllerName));
        }
        #endregion
    }
}