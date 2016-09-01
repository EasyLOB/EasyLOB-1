using EasyLOB.Library;

namespace EasyLOB.Security
{
    public class SecurityManagerMock : ISecurityManager
    {
        #region Methods

        public SecurityManagerMock()
        {
        }

        public ZIsSecurityOperations GetOperations(string activity)
        {
            ZIsSecurityOperations isSecurityOperations = new ZIsSecurityOperations();

            isSecurityOperations.IsSearch = true;
            isSecurityOperations.IsCreate = true;
            isSecurityOperations.IsRead = true;
            isSecurityOperations.IsUpdate = true;
            isSecurityOperations.IsDelete = true;
            isSecurityOperations.IsExport = true;
            isSecurityOperations.IsImport = true;
            isSecurityOperations.IsExecute = true;

            return isSecurityOperations;
        }

        public bool IsAuthorized(string activity, ZSecurityOperations operation)
        {
            return true;
        }
        
        public bool IsAuthorized(string activity, ZSecurityOperations operation, ZOperationResult operationResult)
        {
            return true;
        }


        #endregion Methods
    }
}
