using EasyLOB.Data;
using EasyLOB.Persistence;

namespace EasyLOB.Security.Persistence
{
    public interface ISecurityGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
    }
}