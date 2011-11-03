namespace Glimpse.MEF.Sample.Models
{
    /// <summary>
    /// Defines the required contract for implementing name metadata.
    /// </summary>
    public interface INameMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        #endregion
    }
}
