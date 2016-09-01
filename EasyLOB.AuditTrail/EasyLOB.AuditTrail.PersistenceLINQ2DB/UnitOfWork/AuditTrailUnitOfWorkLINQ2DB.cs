using EasyLOB.AuditTrail.Data;
using EasyLOB.Persistence;

namespace EasyLOB.AuditTrail.Persistence
{
    public class AuditTrailUnitOfWorkLINQ2DB : UnitOfWorkLINQ2DB, IAuditTrailUnitOfWork
    {
        #region Methods

        public AuditTrailUnitOfWorkLINQ2DB()
            : base(new AuditTrailLINQ2DB())
        {
            Domain = "AuditTrail";

            Repositories.Add(typeof(AuditTrailConfiguration), new AuditTrailAuditTrailConfigurationRepositoryLINQ2DB(this));            
            Repositories.Add(typeof(AuditTrailLog), new AuditTrailAuditTrailLogRepositoryLINQ2DB(this));            

            //AuditTrailLINQ2DB connection = (AuditTrailLINQ2DB)base.Connection;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new AuditTrailGenericRepositoryLINQ2DB<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}

