using EasyLOB.Data;
using EasyLOB.Persistence;

namespace EasyLOB.Security.Persistence
{
    public class SecurityGenericRepositoryEF<TEntity> : GenericRepositoryEF<TEntity>, ISecurityGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public SecurityGenericRepositoryEF(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Context = (unitOfWork as SecurityUnitOfWorkEF).Context;
        }

        #endregion Methods
    }
}