using EasyLOB.Persistence;

namespace EasyLOB.AuditTrail.Persistence
{
    public class AuditTrailUnitOfWorkEF : UnitOfWorkEF, IAuditTrailUnitOfWork
    {
        #region Methods

        public AuditTrailUnitOfWorkEF()
            : base(new AuditTrailDbContext())
        {
            Domain = "AuditTrail";

            //AuditTrailDbContext context = (AuditTrailDbContext)base.context;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new AuditTrailGenericRepositoryEF<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }
        
        #endregion Methods
    }
}

