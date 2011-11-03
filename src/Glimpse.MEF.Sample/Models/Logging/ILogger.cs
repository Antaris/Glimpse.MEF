namespace Glimpse.MEF.Sample.Models.Logging
{
    /// <summary>
    /// Defines the required contract for implementing a logger.
    /// </summary>
    public interface ILogger
    {
        #region Methods
        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Log(string message);
        #endregion
    }
}
