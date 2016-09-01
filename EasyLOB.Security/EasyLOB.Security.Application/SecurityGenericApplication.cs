using EasyLOB.Security.Persistence;
using EasyLOB.Application;
using EasyLOB.AuditTrail;
using EasyLOB.Data;
using EasyLOB.Log;

namespace EasyLOB.Security.Application
{
    public class SecurityGenericApplication<TEntity> : GenericApplication<TEntity>, ISecurityGenericApplication<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public SecurityGenericApplication(ISecurityUnitOfWork unitOfWork, IAuditTrailManager auditTrailManager, ILogManager logManager, ISecurityManager securityManager)
            : base(unitOfWork, auditTrailManager, logManager, securityManager)
        {
        }

        #endregion Methods
    }
}