using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.AuditTrail
{
    public interface IAuditTrailManager
    {
        #region Methods

        bool AuditTrail(ZOperationResult operationResult, string logDomain, string logEntity, string logOperation, IZDataBase entityBefore, IZDataBase entityAfter);

        bool IsAuditTrail(string logDomain, string logEntity, string logOperation);

        #endregion Methods
    }
}