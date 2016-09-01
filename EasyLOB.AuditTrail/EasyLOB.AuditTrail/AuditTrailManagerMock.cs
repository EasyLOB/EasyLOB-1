using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail
{
    /// <summary>
    /// Audit Trail Manager - Mock
    /// </summary>
    public class AuditTrailManagerMock : IAuditTrailManager
    {
        #region Methods

        public AuditTrailManagerMock()
        {
        }

        public bool AuditTrail(ZOperationResult operationResult, string logDomain, string logEntity, string logOperation, IZDataBase entityBefore, IZDataBase entityAfter)
        {
            return true;
        }

        public bool IsAuditTrail(string logDomain, string logEntity, string logOperation)
        {
            return false;
        }

        #endregion Methods
    }
}