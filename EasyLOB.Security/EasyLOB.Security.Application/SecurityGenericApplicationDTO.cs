using EasyLOB.Security.Persistence;
using EasyLOB.Application;
using EasyLOB.AuditTrail;
using EasyLOB.Data;
using EasyLOB.Log;

namespace EasyLOB.Security.Application
{
    public class SecurityGenericApplicationDTO<TEntityDTO, TEntity> : GenericApplicationDTO<TEntityDTO, TEntity>, ISecurityGenericApplicationDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public SecurityGenericApplicationDTO(ISecurityUnitOfWork unitOfWork, IAuditTrailManager auditTrailManager, ILogManager logManager, ISecurityManager securityManager)
            : base(unitOfWork, auditTrailManager, logManager, securityManager)
        {
        }

        #endregion Methods
    }
}