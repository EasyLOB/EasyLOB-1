using EasyLOB.Library.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EasyLOB.Library
{
    [Serializable]
    public class ZOperationResult
    {
        #region Properties

        /// <summary>
        /// Data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Error Code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error Message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Operation Result Html.
        /// </summary>
        public string Html
        {
            get
            {
                string result = "";

                string labelStatus = "<label class=\"label label-success\">{0}</label>";

                // Status Message

                if (!String.IsNullOrEmpty(StatusCode) || !String.IsNullOrEmpty(StatusMessage))
                {
                    string text =
                        //ErrorResources.Status + ": " +
                        (!String.IsNullOrEmpty(StatusCode) ? "[ " + StatusCode + " ] " : "") +
                        StatusMessage;
                    result += "<br />" + String.Format(labelStatus, text.Trim());
                }

                string labelError = "<label class=\"label label-danger\">{0}</label>";

                // Error Message

                if (!String.IsNullOrEmpty(ErrorCode) || !String.IsNullOrEmpty(ErrorMessage))
                {
                    string text =
                        ErrorResources.Error + ": " +
                        (!String.IsNullOrEmpty(ErrorCode) ? "[ " + ErrorCode + " ] " : "") +
                        ErrorMessage;
                    result += "<br />" + String.Format(labelError, text.Trim());
                }

                // Errors

                foreach (ZOperationError operationError in OperationErrors)
                {
                    string text =
                        ErrorResources.Error + ": " +
                        (!String.IsNullOrEmpty(operationError.ErrorCode) ? "[ " + operationError.ErrorCode + " ] " : "") +
                        operationError.ErrorMessage;
                    result += "<br />" + String.Format(labelError, text.Trim());
                }

                //return String.IsNullOrEmpty(result) ? result : "<br />" + result;
                return result;
            }
        }

        /// <summary>
        /// Successfull ?
        /// </summary>
        public bool Ok
        {
            get { return (String.IsNullOrEmpty(ErrorCode) && String.IsNullOrEmpty(ErrorMessage) && OperationErrors.Count == 0); }
        }

        /// <summary>
        /// Status Code.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Status Message.
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Operation Errors.
        /// </summary>
        public List<ZOperationError> OperationErrors { get; }

        /// <summary>
        /// Operation Result text with "\n".
        /// </summary>
        public string Text
        {
            get
            {
                List<string> result = ToList();

                return String.Join("\n", result);
            }
        }

        #endregion Properties

        #region Methods

        [JsonConstructor]
        public ZOperationResult()
        {
            ErrorCode = "";
            ErrorMessage = "";
            StatusCode = "";
            StatusMessage = "";
            OperationErrors = new List<ZOperationError>();
        }

        public ZOperationResult(string errorCode, string errorMessage)
            : this()
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Add Operation Error.
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="errorMessage">Error message</param>
        public void AddOperationError(string errorCode, string errorMessage)
        {
            OperationErrors.Add(new ZOperationError(errorCode, errorMessage));
        }

        /// <summary>
        /// Add Operation Error.
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="errorMessage">Error message</param>
        /// <param name="member">Member</param>
        public void AddOperationError(string errorCode, string errorMessage, string member)
        {
            OperationErrors.Add(new ZOperationError(errorCode, errorMessage, new List<string> { member }));
        }

        /// <summary>
        /// Add Operation Error.
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="errorMessage">Error message</param>
        /// <param name="members">Members</param>
        public void AddOperationError(string errorCode, string errorMessage, IEnumerable<string> members)
        {
            OperationErrors.Add(new ZOperationError(errorCode, errorMessage, members));
        }

        /// <summary>
        /// Clear.
        /// </summary>
        public void Clear()
        {
            StatusCode = "";
            StatusMessage = "";
            ErrorCode = "";
            ErrorMessage = "";
            OperationErrors.Clear();
        }

        /// <summary>
        /// Parse Exception.
        /// </summary>
        /// <param name="exception">Exception</param>
        public void ParseException(Exception exception)
        {
            AddOperationError("", exception.Message);
            ParseInnerException(exception);
        }

        /// <summary>
        /// Parse Inner Exception.
        /// </summary>
        /// <param name="exception">Exception</param>
        private void ParseInnerException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                AddOperationError("", exception.InnerException.Message);
                ParseInnerException(exception.InnerException);
            }
        }

        /// <summary>
        /// Convert ZOperationResult to List[string]
        /// </summary>
        /// <returns>List</returns>
        public List<string> ToList()
        {
            List<string> result = new List<string>();

            // Status Message

            if (!String.IsNullOrEmpty(StatusCode) || !String.IsNullOrEmpty(StatusMessage))
            {
                string text = ErrorResources.Status + ": " +
                    (!String.IsNullOrEmpty(StatusCode) ? "[ " + StatusCode + " ] " : "") +
                    StatusMessage;
                result.Add(text.Trim());
            }

            // Error Message

            if (!String.IsNullOrEmpty(ErrorCode) || !String.IsNullOrEmpty(ErrorMessage))
            {
                string text = ErrorResources.Error + ": " +
                    (!String.IsNullOrEmpty(ErrorCode) ? "[ " + ErrorCode + " ] " : "") +
                    ErrorMessage;
                result.Add(text.Trim());
            }

            // Errors

            foreach (ZOperationError operationError in OperationErrors)
            {
                string text = ErrorResources.Error + ": " +
                    (!String.IsNullOrEmpty(operationError.ErrorCode) ? "[ " + operationError.ErrorCode + " ] " : "") +
                    operationError.ErrorMessage;
                result.Add(text.Trim());
            }

            return result;
        }

        /// <summary>
        /// Convert ZOperationResult to List[ZOperationMessage]
        /// </summary>
        /// <returns>List</returns>
        public List<ZOperationMessage> ToDataSet()
        {
            List<ZOperationMessage> result = new List<ZOperationMessage>();

            List<string> messages = ToList();
            foreach (string message in messages)
            {
                result.Add(new ZOperationMessage(message));
            }

            return result;
        }

        #endregion Methods
    }
}