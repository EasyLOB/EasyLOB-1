using EasyLOB.Application;
using EasyLOB.Data;

namespace EasyLOB.AuditTrail.Application
{
    public interface IAuditTrailGenericApplication<TEntity> : IGenericApplication<TEntity>
        where TEntity : class, IZDataBase
    {
    }
}