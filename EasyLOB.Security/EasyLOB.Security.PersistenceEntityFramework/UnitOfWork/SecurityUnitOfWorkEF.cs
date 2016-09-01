using EasyLOB.Security.Data;
using EasyLOB.Persistence;

namespace EasyLOB.Security.Persistence
{
    public class SecurityUnitOfWorkEF : UnitOfWorkEF, ISecurityUnitOfWork
    {
        #region Methods

        public SecurityUnitOfWorkEF()
            : base(new SecurityDbContext())
        {
            Domain = "Security";

            //SecurityDbContext context = (SecurityDbContext)base.context;

            Repositories.Add(typeof(Role), new SecurityRoleRepository(this));
            Repositories.Add(typeof(User), new SecurityUserRepository(this));
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new SecurityGenericRepositoryEF<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}