using EasyLOB.AuditTrail.Data;
using EasyLOB.Persistence;

namespace EasyLOB.AuditTrail.Persistence
{
    public class AuditTrailUnitOfWorkNH : UnitOfWorkNH, IAuditTrailUnitOfWork
    {
        #region Methods
        
        public AuditTrailUnitOfWorkNH()
            : base(AuditTrailFactory.Session)
        {
            Domain = "AuditTrail";

            //ISession session = base.Session;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new AuditTrailGenericRepositoryNH<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }
        
        #endregion Methods        
    }
}

