namespace Glimpse.MEF.Sample.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Web.Mvc;

    /// <summary>
    /// Defines a dependency resolver that supports the managed extensiblity framework.
    /// </summary>
    public class MefDependencyResolver : IDependencyResolver
    {
    #region Fields
    private readonly CompositionContainer _container;
    #endregion

    #region Constructor
    /// <summary>
    /// Initialises a new instance of <see cref="MefDependencyResolver"/>.
    /// </summary>
    /// <param name="container">The current container.</param>
    public MefDependencyResolver(CompositionContainer container)
    {
        if (container == null) throw new ArgumentNullException("container");

        _container = container;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Gets an instance of the service of the specified type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>An instance of the service of the specified type.</returns>
    public object GetService(Type type)
    {
        if (type == null) throw new ArgumentNullException("type");

        string name = AttributedModelServices.GetContractName(type);

        try
        {
            return _container.GetExportedValue<object>(name);
        } catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets all instances of the services of the specified type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>An enumerable of all instances of the services of the specified type.</returns>
    public IEnumerable<object> GetServices(Type type)
    {
        if (type == null) throw new ArgumentNullException("type");

        string name = AttributedModelServices.GetContractName(type);

        try
        {
            return _container.GetExportedValues<object>(name);
        }
        catch
        {
            return null;
        }
    }
    #endregion
    }
}