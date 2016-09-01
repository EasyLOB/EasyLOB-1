using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EasyLOB.Library
{
    /// <summary>
    /// Z Operation Error
    /// </summary>
    [Serializable]
    public class ZOperationError
    {
        #region Properties

        /// <summary>
        /// Error Code.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Error Message.
        /// </summary>
        public string ErrorMessage { get; }

        public IEnumerable<string> ErrorMembers { get; }

        #endregion Properties

        #region Methods

        public ZOperationError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorMembers = new List<string>();
        }

        [JsonConstructor]
        public ZOperationError(string errorCode, string errorMessage, IEnumerable<string> errorMembers)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorMembers = errorMembers;
        }

        #endregion Methods
    }
}