using EasyLOB.Application;
using EasyLOB.Data;

namespace EasyLOB.Security.Application
{
    public interface ISecurityGenericApplication<TEntity> : IGenericApplication<TEntity>
        where TEntity : class, IZDataBase
    {
    }
}