using EasyLOB.Data;
using EasyLOB.Persistence;

namespace EasyLOB.AuditTrail.Persistence
{
    public class AuditTrailGenericRepositoryEF<TEntity> : GenericRepositoryEF<TEntity>, IAuditTrailGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public AuditTrailGenericRepositoryEF(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Context = (unitOfWork as AuditTrailUnitOfWorkEF).Context;
        }

        #endregion Methods
    }
}

