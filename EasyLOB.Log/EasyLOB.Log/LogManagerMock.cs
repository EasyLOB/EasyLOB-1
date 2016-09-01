using EasyLOB.Library;
using System;

namespace EasyLOB.Log
{
    /// <summary>
    /// Log Manager Mock
    /// </summary>
    public partial class LogManagerMock : ILogManager
    {
        #region Methods

        public void Trace(string message, params object[] args)
        {
        }

        public void Debug(string message, params object[] args)
        {
        }

        public void Info(string message, params object[] args)
        {
        }

        public void Warning(string message, params object[] args)
        {
        }

        public void Error(string message, params object[] args)
        {
        }

        public void Fatal(string message, params object[] args)
        {
        }

        public void LogException(Exception exception)
        {
        }

        public void LogOperationResult(ZOperationResult operationResult)
        {
        }

        #endregion Methods
    }
}