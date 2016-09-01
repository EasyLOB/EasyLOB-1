using EasyLOB.Library;
using EasyLOB.Library.Resources;
using NLog;
using System;

// NLog.config | NLog.xsd

// Off => Trace => Debug => Info => Warn => Error => Fatal

// 6 - Fatal
// 5 - Error
// 4 - Warn
// 3 - Info
// 2 - Debug
// 1 - Trace
// 0 - Off

namespace EasyLOB.Log
{
    /// <summary>
    /// LogManager - EasyLOB - NLog
    /// </summary>
    public partial class LogManager : ILogManager
    {
        #region Properties

        private Logger _nLogLogger;

        /// <summary>
        /// Logger.
        /// </summary>
        public Logger NLogLogger
        {
            get
            {
                if (_nLogLogger == null)
                {
                    _nLogLogger = NLog.LogManager.GetLogger("NLog");
                }

                return _nLogLogger;
            }
        }

        #endregion Properties NLog

        #region Methods

        public void Trace(string message, params object[] args)
        {
            NLogLogger.Trace(message, args);
        }

        public void Debug(string message, params object[] args)
        {
            NLogLogger.Debug(message, args);
        }

        public void Info(string message, params object[] args)
        {
            NLogLogger.Info(message, args);
        }

        public void Warning(string message, params object[] args)
        {
            NLogLogger.Warn(message, args);
        }

        public void Error(string message, params object[] args)
        {
            NLogLogger.Error(message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            NLogLogger.Fatal(message, args);
        }

        public void LogException(Exception exception)
        {
            NLogLogger.Error(exception.ExceptionMessage);
        }

        public void LogOperationResult(ZOperationResult operationResult)
        {
            // Status Message

            if (!String.IsNullOrEmpty(operationResult.StatusCode) || !String.IsNullOrEmpty(operationResult.StatusMessage))
            {
                string text = ErrorResources.Status + " " + (!String.IsNullOrEmpty(operationResult.StatusCode) ? "[ " + operationResult.StatusCode + " ] " : "") +
                    operationResult.StatusMessage;
                Trace(text);
            }

            // Error Message

            if (!String.IsNullOrEmpty(operationResult.ErrorCode) || !String.IsNullOrEmpty(operationResult.ErrorMessage))
            {
                string text = ErrorResources.Error + " " + (!String.IsNullOrEmpty(operationResult.ErrorCode) ? "[ " + operationResult.ErrorCode + " ] " : "") +
                    operationResult.ErrorMessage;
                Error(text);
            }

            // Errors

            foreach (ZOperationError operationError in operationResult.OperationErrors)
            {
                string text = ErrorResources.Error + " " + (!String.IsNullOrEmpty(operationError.ErrorCode) ? "[ " + operationError.ErrorCode + " ] " : "") +
                    operationError.ErrorMessage;
                Error(text);
            }
        }

        #endregion Methods
    }
}