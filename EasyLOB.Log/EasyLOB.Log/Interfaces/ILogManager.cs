using EasyLOB.Library;
using System;

namespace EasyLOB.Log
{
    /// <summary>
    /// ILogManager
    /// </summary>
    public interface ILogManager
    {
        #region Methods

        /// <summary>
        /// Log Trace.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Log Debug.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Log Info.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Log Warning.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        void Warning(string message, params object[] args);

        /// <summary>
        /// Log Error.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Log Fatal Error.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Arguments</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Log Exception.
        /// </summary>
        /// <param name="exception">Exception</param>
        void LogException(Exception exception);

        /// <summary>
        /// Log Operation Result.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        void LogOperationResult(ZOperationResult operationResult);

        #endregion Methods
    }
}