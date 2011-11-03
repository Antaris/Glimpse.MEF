namespace Glimpse.MEF.Sample.Models.Logging
{
    using System.ComponentModel.Composition;
    using System.Diagnostics;

    /// <summary>
    /// Performs logging to trace.
    /// </summary>
    [Export(typeof(ILogger))]
    public class TraceLogger : ILogger
    {
        #region Methods
        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message)
        {
            Trace.Write(message);
        }
        #endregion
    }
}