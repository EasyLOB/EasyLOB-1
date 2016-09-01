using EasyLOB.Data;
using EasyLOB.Persistence;

namespace EasyLOB.Security.Persistence
{
    public class SecurityGenericRepositoryNH<TEntity> : GenericRepositoryNH<TEntity>, ISecurityGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public SecurityGenericRepositoryNH(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Session = (unitOfWork as SecurityUnitOfWorkNH).Session;
        }

        #endregion Methods
    }
}