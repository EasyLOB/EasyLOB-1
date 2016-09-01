using EasyLOB.Persistence;

namespace EasyLOB.Security.Persistence
{
    public class SecurityUnitOfWorkNH : UnitOfWorkNH, ISecurityUnitOfWork
    {
        #region Methods

        public SecurityUnitOfWorkNH()
            : base(SecurityFactory.Session)
        {
            Domain = "Security";

            //ISession session = base.Session;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new SecurityGenericRepositoryNH<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods

    }
}