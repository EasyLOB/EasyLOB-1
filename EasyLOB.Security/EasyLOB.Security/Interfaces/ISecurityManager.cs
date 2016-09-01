using EasyLOB.Library;

namespace EasyLOB.Security
{
    public interface ISecurityManager
    {
        #region Methods

        ZIsSecurityOperations GetOperations(string activity);

        bool IsAuthorized(string activity, ZSecurityOperations operation);

        bool IsAuthorized(string activity, ZSecurityOperations operation, ZOperationResult operationResult);

        #endregion Methods
    }
}