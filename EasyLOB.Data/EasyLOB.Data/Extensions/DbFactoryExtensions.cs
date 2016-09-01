using System.Data.Common;

namespace EasyLOB.Data
{
    /// <summary>
    /// DbCommand Extensions
    /// </summary>
    public static class DbCommandExtensions
    {
        /// <summary>with value
        /// Add parameters
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        public static void AddParameterWithValue(this DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }
    }
}